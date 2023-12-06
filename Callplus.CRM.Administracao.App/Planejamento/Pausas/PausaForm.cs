using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Pausa
{
    public partial class PausaForm : Form
    {
        public PausaForm(string titulo, ArquivoDePausa arquivoDePausa)
        {
            _arquivoDePausa = arquivoDePausa;

            _logger = LogManager.GetCurrentClassLogger();
            _pausaService = new PausaService();

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region VARIAVEIS
        private readonly ILogger _logger;
        private PausaService _pausaService;
        private string _arquivoDestino = "";
        private string _nomeArquivo;
        private string _caminhoServidorProcessamento = @"\\192.168.25.201\h$\HMB\PAUSAS\IMPORTAR\";
        private string _arquivoOrigem = "";
        private ArquivoDePausa _arquivoDePausa;

        public bool atualizar = false;
        public static int idLayout;
        public static string tableLayout;

        #endregion VARIAVEIS

        #region Eventos
        private void MailingForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarDadosDoMailing();
                DesabilitarCampos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao carregar os dados do Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, System.EventArgs e)
        {
            try
            {
                SalvarArquivoDePausas();
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
            if (_arquivoDePausa != null)
            {
                cmdCarregarArquivo.Enabled = false;
                txtCaminhoDoArquivo.Enabled = false;
            }
        }

        private bool PodeExportarEnviarMailing()
        {
            var mensagens = new List<string>();

            if (_arquivoDePausa != null)
            {
                if (!_pausaService.VerificarSeMailingEstaProcessadoComSucesso(_arquivoDePausa.id))
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
            if (_arquivoDePausa != null)
            {
                txtNome.Text = _arquivoDePausa.nome;
                txtObservacao.Text = _arquivoDePausa.observacao;
                chkAtivo.Checked = _arquivoDePausa.ativo;
                dtpDataInicio.Text = _arquivoDePausa.dataInicio.Date.ToString();
                dtpDataTermino.Text = _arquivoDePausa.dataTermino.Date.ToString();
                txtCaminhoDoArquivo.Text = _arquivoDePausa.caminhoArquivo;
                ConsultarTipoEnvioDadosDiscador();
            }
            else
            {
                dtpDataInicio.MinDate = DateTime.Now;
                dtpDataTermino.MinDate = DateTime.Now;
                cmdCarregarArquivo.Enabled = true;
            }
        }

        private void SalvarArquivoDePausas()
        {
            if (PodeSalvar())
            {
                if (_arquivoDePausa != null) { MessageBox.Show("Atualizado!"); return; }

                if (_arquivoDePausa == null)
                    _arquivoDePausa = new ArquivoDePausa();

                TransferirArquivoParaServidorCallplus();

                _arquivoDePausa.nome = txtNome.Text.Trim();
                _arquivoDePausa.ativo = chkAtivo.Checked;
                _arquivoDePausa.idCriador = AdministracaoMDI._usuario.Id;
                _arquivoDePausa.idModificador = AdministracaoMDI._usuario.Id;
                _arquivoDePausa.observacao = txtObservacao.Text.Trim();
                _arquivoDePausa.nomeArquivo = _nomeArquivo;
                _arquivoDePausa.caminhoArquivo = _arquivoDestino;
                _arquivoDePausa.dataInicio = Convert.ToDateTime(dtpDataInicio.Text);
                _arquivoDePausa.dataTermino = Convert.ToDateTime(dtpDataTermino.Text);

                _pausaService.Gravar(_arquivoDePausa);

                atualizar = true;
                this.Hide();
                this.Close();
            }
        }

        private void TransferirArquivoParaServidorCallplus()
        {
            if (_arquivoDePausa.id > 0) return;

            if (!Directory.Exists(_caminhoServidorProcessamento))
                Directory.CreateDirectory(_caminhoServidorProcessamento);

            File.Copy(_arquivoOrigem, _arquivoDestino, true);
        }

        private bool PodeSalvar()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser informado.");

            if (_arquivoDePausa == null)
            {
                if (string.IsNullOrEmpty(_arquivoOrigem.Trim()))
                    mensagens.Add("Você deve selecionar o arquivo a ser importado!");
            }
            else if (_arquivoDePausa.id == 0)
            {
                if (string.IsNullOrEmpty(_arquivoOrigem.Trim()))
                    mensagens.Add("Você deve selecionar o arquivo a ser importado!");
            }

            if (ExisteNomeDoArquivo())
                mensagens.Add("Já existe um mailing com esse nome!");

            ExibirMensagens(mensagens);
            return !mensagens.Any();

        }

        private bool ExisteNomeDoArquivo()
        {
            if (_arquivoDePausa != null) return false;
            return _pausaService.VerificarSeExisteNomeDoArquivo(txtNome.Text.Trim());
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
            FileDialog.Filter = "Arquivos Textos (*.txt)|*.txt";
            if (DialogResult.Cancel == FileDialog.ShowDialog()) return;
            _arquivoOrigem = FileDialog.FileName;
            var nome = FileDialog.SafeFileName;
            _arquivoDestino = _caminhoServidorProcessamento + nome;
            _nomeArquivo = nome;

            if (string.IsNullOrEmpty(_arquivoOrigem))
                MessageBox.Show("[Arquivo] inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                txtCaminhoDoArquivo.Text = _arquivoOrigem;
        }

        private void ConsultarTipoEnvioDadosDiscador()
        {
            //Discador discador = new Discador();

            //if (cmbCampanha.TextoEhSelecione()) return;
            //int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
            //discador = _discadorService.RetornarTipoEnvioDadosDiscador(idCampanha);

            //cmdEnviarParaDiscador.Enabled = discador.spEnvioAutomaticoMailing != "" && _mailing != null;
            //cmdExportarArquivo.Enabled = discador.spExportacaoMailing != "" && _mailing != null;
            //lblDiscador.Text = "Discador: " + discador.Nome;
        }

        //private void ExportarArquivo()
        //{
        //    DialogResult retry = DialogResult.Retry;
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Filter = "txt files (*.txt)|*.txt";
        //    saveFileDialog.FilterIndex = 0;
        //    saveFileDialog.RestoreDirectory = true;
        //    saveFileDialog.Title = "Exportar Mailing";

        //    saveFileDialog.FileName = "CALLPLUS-DISCADOR-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        Stream myStream = null;

        //        try
        //        {
        //            if ((myStream = saveFileDialog.OpenFile()) != null)
        //            {
        //                while (retry == DialogResult.Retry)
        //                {
        //                    try
        //                    {
        //                        var sw = new StreamWriter(myStream);

        //                        var _dt = _mailingService.ExportarMailing(_mailing.id);

        //                        for (int i = 0; i < _dt.Rows.Count; i++)
        //                        {
        //                            if (!string.IsNullOrEmpty(_dt.Rows[i][0].ToString().Trim()))
        //                                sw.WriteLine(_dt.Rows[i][0].ToString());
        //                        }
        //                        sw.Close();
        //                        retry = DialogResult.Cancel;
        //                    }
        //                    catch (Exception myException)
        //                    {
        //                        retry = MessageBox.Show(myException.Message, "Erro Exportação", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
        //                    }
        //                }
        //                MessageBox.Show("Arquivo gerado com sucesso!", "Callplus Software");
        //            }
        //        }
        //        finally
        //        {
        //            myStream.Close();
        //        }
        //    }
        //}
        #endregion Metodos  

    }
}
