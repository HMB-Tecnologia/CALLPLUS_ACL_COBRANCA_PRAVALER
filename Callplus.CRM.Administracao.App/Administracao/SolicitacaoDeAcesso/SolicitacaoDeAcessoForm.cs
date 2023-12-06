using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System.Windows.Forms;
using NLog;
using Callplus.CRM.Tabulador.Servico.Servicos;

namespace Callplus.CRM.Administracao.App.Administracao.SolicitacaoDeAcesso
{
    public partial class SolicitacaoDeAcessoForm : Form
    {
        public SolicitacaoDeAcessoForm(string titulo, SolicitacaoDeAcessoAoSistema solicitacao)
        {
            _solicitacao = solicitacao;

            _logger = LogManager.GetCurrentClassLogger();
            _loginService = new LoginService();
            
            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region VARIAVEIS

        private readonly ILogger _logger;

        private readonly LoginService _loginService;

        private SolicitacaoDeAcessoAoSistema _solicitacao;
        
        public bool atualizar { get; set; }

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            txtId.Text = _solicitacao.id.ToString();
            txtData.Text = _solicitacao.dataCadastro.ToString("dd/MM/yyyy hh:mm:ss");
            txtSupervisor.Text = _solicitacao.supervisor;
            txtOperador.Text = _solicitacao.operador;
        }

        private void Gravar()
        {
            _solicitacao.observacao = txtObservacao.Text;
            _solicitacao.idUsuarioLiberacao = AdministracaoMDI._usuario.Id;

            _solicitacao.id = _loginService.GravarSolicitacaoDeAcesso(_solicitacao);

            MessageBox.Show("Solicitação liberada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            atualizar = true;

            this.Hide();
            this.Close();
        }

        #endregion METODOS

        #region EVENTOS

        private void SolicitacaoDeAcessoForm_Load(object sender, EventArgs e)
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

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a liberação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
