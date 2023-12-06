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
using System.Text.RegularExpressions;

namespace Callplus.CRM.Administracao.App.Planejamento.CadastroCep
{
    public partial class CadastroCepForm : Form
    {
        public CadastroCepForm(string titulo, int Id, IEnumerable<Tabulador.Dominio.Entidades.Campanha> campanhas)
        {
            _campanhas = campanhas;

            _logger = LogManager.GetCurrentClassLogger();
            _cadastroCepService = new CepExpressService();
            _campanhaService = new CampanhaService();

            if (Id > 0)
                _cadastroCepExpress = _cadastroCepService.RetornarCepExpress(Id);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region VARIAVEIS

        private readonly ILogger _logger;
        private readonly CepExpressService _cadastroCepService;
        private readonly CampanhaService _campanhaService;
        private string _arquivoDestino = string.Empty;
        private string _arquivoOrigem = string.Empty;
        private CepExpress _cadastroCepExpress;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;
        private string _caminhoServidorProcessamento = string.Empty;

        public bool atualizar = false;
        public static int idLayout;
        public static string tableLayout;

        #endregion VARIAVEIS


        #region METODOS

        private void DesabilitarCampos()
        {
            if (_cadastroCepExpress != null)
            {
                cmbCampanha.Enabled = false;
                cmdCarregarArquivo.Enabled = false;
                txtCaminhoDoArquivo.Enabled = false;
            }
        }

        private void CarregarDadosDoCadastroCep()
        {
            if (_cadastroCepExpress != null)
            {
                cmbCampanha.SelectedValue = _cadastroCepExpress.IdCampanha.ToString();
                txtNome.Text = _cadastroCepExpress.Nome;
                txtObservacao.Text = _cadastroCepExpress.Observacao;
                chkAtivo.Checked = _cadastroCepExpress.Ativo;

                ConsultarCaminhoDoServidor();
            }
        }

        private void SalvarCadastroCep()
        {
            if (PodeSalvar())
            {
                if (_cadastroCepExpress == null)
                {
                    _cadastroCepExpress = new CepExpress();

                    _cadastroCepExpress.IdCriador = AdministracaoMDI._usuario.Id;
                }

                TransferirArquivoParaServidorCallplus();

                _cadastroCepExpress.IdCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                _cadastroCepExpress.Nome = txtNome.Text.Trim();
                _cadastroCepExpress.IdModificador = AdministracaoMDI._usuario.Id;

                _cadastroCepExpress.Observacao = txtObservacao.Text.Trim();
                _cadastroCepExpress.NomeArquivo = _arquivoDestino;
                _cadastroCepExpress.Ativo = chkAtivo.Checked;

                _cadastroCepExpress.Id = _cadastroCepService.Gravar(_cadastroCepExpress);

                atualizar = true;

                this.Hide();
                this.Close();
            }
        }

        private void TransferirArquivoParaServidorCallplus()
        {
            if (_cadastroCepExpress.Id > 0) return;

            string importar = _caminhoServidorProcessamento.ToUpper();
            string importado = _caminhoServidorProcessamento.ToUpper().Replace("IMPORTAR", "IMPORTADO");
            string erro = _caminhoServidorProcessamento.ToUpper().Replace("IMPORTAR", "ERRO");

            if (!Directory.Exists(importar))
                Directory.CreateDirectory(importar);

            if (!Directory.Exists(importado))
                Directory.CreateDirectory(importado);

            if (!Directory.Exists(erro))
                Directory.CreateDirectory(erro);

            File.Copy(_arquivoOrigem, _arquivoDestino, true);
        }

        private bool PodeSalvar()
        {
            var mensagens = new List<string>();

            string nomeDoArquivo = string.Empty;

            if (cmbCampanha.TextoEhSelecione() || string.IsNullOrEmpty(cmbCampanha.Text))
                mensagens.Add("Favor selecionar a campanha!");

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser informado.");

            if (_cadastroCepExpress != null)
            {
                if (_cadastroCepExpress.Id == 0 && ExisteNomeDoCadastroCep())
                    mensagens.Add("Já existe um arquivo com esse nome!");
            }

            var regex = new Regex("^[\\s-a-zA-Z0-9_-]*$");
            string nomeDaCampanha = txtNome.Text;

            var formatoCorreto = regex.IsMatch(nomeDaCampanha);

            if (formatoCorreto == false)
            {
                mensagens.Add("Nome do arquivo não pode conter caracteres especiais!");
            }

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool ExisteNomeDoCadastroCep()
        {
            if (_cadastroCepExpress == null) return false;
            return _cadastroCepService.VerificarSeExisteNomeDoCadastroCep(txtNome.Text.Trim());
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
            cmbCampanha.PreencherComSelecione(_campanhas.Where(x => x.HabilitaCepExpress == true), campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void cmbCampanha_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ConsultarCaminhoDoServidor();
        }

        private void ConsultarCaminhoDoServidor()
        {
            if (cmbCampanha.TextoEhSelecione()) return;

            int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
            string caminho = _campanhaService.RetornarCaminhoDoServidor(idCampanha);

            _caminhoServidorProcessamento = caminho.ToUpper().Replace("MAILING", "CEPS_ELEGIVEIS");

            if (string.IsNullOrEmpty(caminho) == false)
                cmdCarregarArquivo.Enabled = true;
            else
                cmdCarregarArquivo.Enabled = false;

        }

        #endregion METODOS  

        #region EVENTOS
        private void CadastroCepForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarCampanhas();
                CarregarDadosDoCadastroCep();
                DesabilitarCampos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao carregar os dados do CadastroCep!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, System.EventArgs e)
        {
            try
            {
                SalvarCadastroCep();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao salvar o CadastroCep!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion EVENTOS

    }
}
