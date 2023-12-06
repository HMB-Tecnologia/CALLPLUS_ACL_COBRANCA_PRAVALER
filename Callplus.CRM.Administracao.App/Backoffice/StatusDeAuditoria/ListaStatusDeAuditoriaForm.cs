using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;

namespace Callplus.CRM.Administracao.App.Backoffice.StatusDeAuditoria
{
    public partial class ListaStatusDeAuditoriaForm : Form
    {
        public ListaStatusDeAuditoriaForm()
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _statusDeAuditoria = new StatusDeAuditoriaService();

            InitializeComponent();
        }


        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly CampanhaService _campanhaService;
        private readonly StatusDeAuditoriaService _statusDeAuditoria;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;

        #endregion PROPRIEDADES


        #region METODOS

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            CarregarCampanhas();
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void IniciarNovoRegistro()
        {
            StatusDeAuditoriaForm f = new StatusDeAuditoriaForm("NOVO STATUS DE AUDITORIA", 0);

            f.Iniciar();

            if (f.atualizar)
            {
                CarregarGrid(false);
            }
        }

        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                StatusDeAuditoriaForm f = new StatusDeAuditoriaForm("EDITAR STATUS AUDITORIA", id);

                f.Iniciar();

                if (f.atualizar)
                {
                    CarregarGrid(false);
                }
            }
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

        private void CarregarGrid(bool buscaRapida)
        {
            int id = -1;
            int idCampanha = -1;
            string nome = "";
            bool ativo = chkListarAtivos.Checked;

            idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        id = int.Parse(txtBuscaRapida.Text);
                }
                else
                {
                    idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                    nome = txtNome.Text;
                }

                dgResultado.DataSource = _statusDeAuditoria.ListarExibicao(id, nome, ativo, idCampanha);

                lblTotalRegistros.Text = dgResultado.RowCount + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Id"].Width = 35;
            dgResultado.Columns["Nome"].Width = 300;
        }

        #endregion METODOS

        #region EVENTOS 

        private void ListaStatusDeAuditoriaForm_Load(object sender, System.EventArgs e)
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarNovoRegistro();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar o novo cadastro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        #endregion EVENTOS 

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
    }
}
