using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Administracao.SolicitacaoDeAcesso
{
    public partial class ListaSolicitacaoDeAcessoForm : Form
    {
        public ListaSolicitacaoDeAcessoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _usuarioService = new UsuarioService();
            _loginService = new LoginService();

            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly UsuarioService _usuarioService;
        private readonly LoginService _loginService;
        private readonly ILogger _logger;

        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _supervisores;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _operadores;

        #endregion VARIAVEIS

        #region METODOS

        private void ExibirForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            CarregarSupervisor();
            CarregarOperadores(0);

            if (AdministracaoMDI._usuario.perfil.ToString() == "SUPERVISOR")
            {
                cmbSupervisor.SelectedValue = AdministracaoMDI._usuario.Id.ToString();
                cmbSupervisor.Enabled = false;
            }
        }

        private void CarregarSupervisor()
        {
            _supervisores = _usuarioService.ListarSupervisores(ativo: true, idCampanha: -1);

            cmbSupervisor.PreencherComTodos(_supervisores, supervisor => supervisor.Id, supervisor => supervisor.Nome);
        }

        private void CarregarOperadores(int idSupervisor)
        {
            _operadores = _usuarioService.ListarOperadores(true, -1, idSupervisor);

            cmbOperador.PreencherComTodos(_operadores, operador => operador.Id, operador => operador.Nome);
        }

        public void SelecionarSupervisor()
        {
            int idSupervisor = -1;
            
            if (!cmbSupervisor.TextoEhTodos())
            {
                idSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
            }

            CarregarOperadores(idSupervisor);
        }

        private async Task CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;            
            int idSupervisor = -1;
            int idOperador = -1;
            bool ativo = chkListarAtivos.Checked;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        idRegistro = int.Parse(txtBuscaRapida.Text);
                }
                else
                {   
                    idSupervisor = int.Parse(cmbSupervisor.SelectedValue.ToString());
                    idOperador = int.Parse(cmbOperador.SelectedValue.ToString());
                }

                dgResultado.DataSource = await _loginService.ListarSolicitacaoDeAcesso(idRegistro, idSupervisor, idOperador, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data Solicitação"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            dgResultado.Columns["Data Liberação"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();

            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                {
                    mensagens.Add("[ID] deve ser informado!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                string status = dgResultado.Rows[linha].Cells["Status"].Value.ToString();

                if (status.ToUpper() == "PENDENTE")
                {
                    SolicitacaoDeAcessoAoSistema solicitacao = new SolicitacaoDeAcessoAoSistema();

                    solicitacao.id = (int)dgResultado.Rows[linha].Cells["ID"].Value;
                    solicitacao.dataCadastro = (DateTime)dgResultado.Rows[linha].Cells["Data Solicitação"].Value;
                    solicitacao.supervisor = dgResultado.Rows[linha].Cells["Supervisor"].Value.ToString();
                    solicitacao.operador = dgResultado.Rows[linha].Cells["Nome"].Value.ToString();

                    SolicitacaoDeAcessoForm f = new SolicitacaoDeAcessoForm("DETALHES DA SOLICITAÇÃO", solicitacao);

                    ExibirForm(f);

                    if (f.atualizar)
                    {
                        CarregarGrid(false);
                    }
                }
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void ListaSolicitacaoDeAcessoForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();

                btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscaRapida_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a busca rápida!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }
        
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void dgResultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarEdicaoRegistro(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelecionarSupervisor();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados para o Supervisor selecionado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
