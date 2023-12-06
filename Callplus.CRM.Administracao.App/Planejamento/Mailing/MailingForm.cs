using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Text;
using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Administracao.App.Planejamento.Mailing
{
    public partial class MailingForm : Form
    {
        public MailingForm(string titulo, Tabulador.Dominio.Entidades.Mailing mailing, IEnumerable<Tabulador.Dominio.Entidades.Campanha> campanhas)
        {
            _mailing = mailing;
            _campanhas = campanhas;

            _logger = LogManager.GetCurrentClassLogger();
            _mailingService = new MailingService();
            _campanhaService = new CampanhaService();
            _discadorService = new DiscadorService();

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region VARIAVEIS

        private readonly ILogger _logger;
        private readonly MailingService _mailingService;
        private readonly CampanhaService _campanhaService;
        private readonly DiscadorService _discadorService;
        private string _arquivoDestino = "";
        private string _caminhoServidorProcessamento = "";
        private string _arquivoOrigem = "";
        private Tabulador.Dominio.Entidades.Mailing _mailing;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;

        public bool atualizar = false;
        public static int idLayout;
        public static string tableLayout;

        #endregion VARIAVEIS

        #region Eventos
        private void MailingForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarCampanhas();
                CarregarDadosDoMailing();
                DesabilitarCampos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao carregar os dados do Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdExportarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (PodeExportarEnviarMailing())
                    ExportarArquivo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao exportar os dados do Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdEnviarParaDiscador_Click(object sender, EventArgs e)
        {
            try
            {
                if (PodeExportarEnviarMailing())
                {
                    EnviarDadosParaDiscador();
                    MessageBox.Show("Processo finalizado.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Ocorreu erro ao enviar dados do Mailing ao discador!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnviarDadosParaDiscador()
        {
            _mailingService.ExportarMailingDiscador(_mailing.id);
        }

        private void btnSalvar_Click(object sender, System.EventArgs e)
        {
            try
            {
                SalvarMailing();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao salvar o Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdCarregarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                LocalizarArquivo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Ocorreu erro ao Carregar o arquivo!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Eventos

        #region Metodos
        private void DesabilitarCampos()
        {
            if (_mailing != null)
            {
                cmbCampanha.Enabled = false;
                chkIndicacao.Enabled = false;
                cmdCarregarArquivo.Enabled = false;
                txtCaminhoDoArquivo.Enabled = false;
            }
        }

        private bool PodeExportarEnviarMailing()
        {
            var mensagens = new List<string>();

            if (_mailing != null)
            {
                if (!_mailingService.VerificarSeMailingEstaProcessadoComSucesso(_mailing.id))
                    mensagens.Add("O processamento do mailing ainda não foi finalizado!!");
            }
            else
            {
                mensagens.Add("Não há mailing selecionado!");
            }

            ExibirMensagens(mensagens);
            return !mensagens.Any();
        }

        private void CarregarDadosDoMailing()
        {
            if (_mailing != null)
            {
                cmbCampanha.SelectedValue = _mailing.idCampanha.ToString();
                txtNome.Text = _mailing.nome;
                txtObservacao.Text = _mailing.observacao;
                chkAtivo.Checked = _mailing.ativo;
                chkIndicacao.Checked = _mailing.indicacao;
                ConsultarCaminhoDoServidor();
                ConsultarTipoEnvioDadosDiscador();
            }
        }

        private void SalvarMailing()
        {
            if (PodeSalvar())
            {
                if (_mailing == null) _mailing = new Tabulador.Dominio.Entidades.Mailing();

                TransferirArquivoParaServidorCallplus();
                _mailing.idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                _mailing.nome = txtNome.Text.Trim();
                _mailing.ativo = chkAtivo.Checked;
                _mailing.indicacao = chkIndicacao.Checked;
                _mailing.idCriador = AdministracaoMDI._usuario.Id;
                _mailing.idModificador = AdministracaoMDI._usuario.Id;
                _mailing.observacao = txtObservacao.Text.Trim();
                _mailing.nomeArquivo = _arquivoDestino;

                _mailingService.Gravar(_mailing);

                atualizar = true;
                this.Hide();
                this.Close();
            }
        }

        private void TransferirArquivoParaServidorCallplus()
        {
            if (_mailing.id > 0) return;

            if (!Directory.Exists(_caminhoServidorProcessamento))
            {
                Directory.CreateDirectory(_caminhoServidorProcessamento);
            }
            File.Copy(_arquivoOrigem, _arquivoDestino, true);
        }

        private bool PodeSalvar()
        {
            var mensagens = new List<string>();

            if (cmbCampanha.TextoEhSelecione() || string.IsNullOrEmpty(cmbCampanha.Text))
                mensagens.Add("Favor selecionar a campanha!");

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser informado.");

            if (_mailing == null)
            {
                if (string.IsNullOrEmpty(_arquivoOrigem.Trim()))
                    mensagens.Add("Você deve selecionar o arquivo a ser importado!");
            }
            else if (_mailing.id == 0)
            {
                if (string.IsNullOrEmpty(_arquivoOrigem.Trim()))
                    mensagens.Add("Você deve selecionar o arquivo a ser importado!");
            }

            if (ExisteNomeDoMailing())
                mensagens.Add("Já existe um mailing com esse nome!");

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;

        }

        private bool ExisteNomeDoMailing()
        {
            if (_mailing != null) return false;
            return _mailingService.VerificarSeExisteNomeDoMailing(txtNome.Text.Trim());
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void LocalizarArquivo()
        {
            var FileDialog = new OpenFileDialog();

            FileDialog.Title = "Abrir Como";
            FileDialog.Filter = "Arquivos Textos (*.CSV)|*.CSV";
            if (DialogResult.Cancel == FileDialog.ShowDialog()) return;
            _arquivoOrigem = FileDialog.FileName;
            string nome = FileDialog.SafeFileName;
            _arquivoDestino = _caminhoServidorProcessamento + nome;

            if (string.IsNullOrEmpty(_arquivoOrigem))
            {
                MessageBox.Show("[Arquivo] inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtCaminhoDoArquivo.Text = _arquivoOrigem;
            }
        }

        private void CarregarCampanhas()
        {
            cmbCampanha.PreencherComSelecione(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void cmbCampanha_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ConsultarCaminhoDoServidor();
            ConsultarTipoEnvioDadosDiscador();
        }

        private void ConsultarTipoEnvioDadosDiscador()
        {
            Discador discador = new Discador();

            if (cmbCampanha.TextoEhSelecione()) return;
            int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
            discador = _discadorService.RetornarTipoEnvioDadosDiscador(idCampanha);

            cmdEnviarParaDiscador.Enabled = discador.spEnvioAutomaticoMailing != "" && _mailing != null;
            cmdExportarArquivo.Enabled = discador.spExportacaoMailing != "" && _mailing != null;
            lblDiscador.Text = "Discador: " + discador.Nome;
        }

        private void ConsultarCaminhoDoServidor()
        {
            if (cmbCampanha.TextoEhSelecione()) return;

            int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
            _caminhoServidorProcessamento = _campanhaService.RetornarCaminhoDoServidor(idCampanha);

            if (string.IsNullOrEmpty(_caminhoServidorProcessamento) == false)
                cmdCarregarArquivo.Enabled = true;
            else
                cmdCarregarArquivo.Enabled = false;

        }

        private void ExportarArquivo()
        {
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar Mailing";

            saveFileDialog.FileName = "CALLPLUS-DISCADOR-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream myStream = null;

                try
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                        while (retry == DialogResult.Retry)
                        {
                            try
                            {
                                var sw = new StreamWriter(myStream);

                                var _dt = _mailingService.ExportarMailing(_mailing.id);

                                for (int i = 0; i < _dt.Rows.Count; i++)
                                {
                                    if (!string.IsNullOrEmpty(_dt.Rows[i][0].ToString().Trim()))
                                        sw.WriteLine(_dt.Rows[i][0].ToString());
                                }
                                sw.Close();
                                retry = DialogResult.Cancel;
                            }
                            catch (Exception myException)
                            {
                                retry = MessageBox.Show(myException.Message, "Erro Exportação", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
                            }
                        }
                        MessageBox.Show("Arquivo gerado com sucesso!", "Callplus Software");
                    }
                }
                finally
                {
                    myStream.Close();
                }
            }

        }
        #endregion Metodos  

    }
}
