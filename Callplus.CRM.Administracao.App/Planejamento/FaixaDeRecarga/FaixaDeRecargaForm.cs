using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;

namespace Callplus.CRM.Administracao.App.Planejamento.CriarFaixaDeRecarga
{
    public partial class FaixaDeRecargaForm : Form
    {
        public FaixaDeRecargaForm(string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _faixasDeRecargaService = new FaixasDeRecargaService();

            if (id > 0)
                _faixa = _faixasDeRecargaService.Retornar(id);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly FaixasDeRecargaService _faixasDeRecargaService;
        private FaixaDeRecarga _faixa;
        public FaixaDeRecarga FaixaDeRecarga;

        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarDados()
        {
            if (_faixa != null)
            {
                //txtId.Text = _faixa.Id.ToString();
                txtFaixa.Text = _faixa.Nome;
                chkAtivo.Checked = _faixa.Ativo;
            }
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_faixa == null)
                {
                     edicao = false;
                    _faixa = new Tabulador.Dominio.Entidades.FaixaDeRecarga();
                    _faixa.idCriador = AdministracaoMDI._usuario.Id;
                }

                _faixa.Nome = txtFaixa.Text;
                _faixa.Ativo = chkAtivo.Checked;
                _faixa.idModificador = AdministracaoMDI._usuario.Id;

                _faixasDeRecargaService.GravarFaixaDeRecarga(_faixa);

                MessageBox.Show(
                    $"Produto {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Atualizar = true;

                this.Close();
            }
        }
        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();
            if (string.IsNullOrEmpty(txtFaixa.Text.Trim()))
            {
                mensagens.Add("[Faixa] deve ser informado.");
            }

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }
        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CarregarConfiguracaoInicial()
        {
            //this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            CarregarDados();
        }

        #endregion METODOS

        #region EVENTOS

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível salvar o registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CriarFaixaDeRecargaForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS       

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {

        }
    }
}
