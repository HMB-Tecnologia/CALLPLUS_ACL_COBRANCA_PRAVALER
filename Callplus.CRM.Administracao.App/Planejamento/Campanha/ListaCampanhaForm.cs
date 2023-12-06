using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Campanha
{
    public partial class ListaCampanhaForm : Form
    {
        public ListaCampanhaForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _discadorService = new DiscadorService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly DiscadorService _discadorService;
        private int _idCampanha;
        private string _nomeCampanha;
        private bool _duplicarCampanha = false;

        #endregion PROPRIEDADES

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

            if (AdministracaoMDI._usuario.Protegido) btnDuplicar.Visible = true;//remover depois

            CarregarDiscadores();
        }

        private void CarregarDiscadores()
        {
            IEnumerable<Discador> _discadores = _discadorService.Listar(-1, true);
            cmbDiscador.PreencherComTodos(_discadores, x => x.Id, x => x.Nome);
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int idDiscador = -1;
            string nome = "";
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
                    idDiscador = int.Parse(cmbDiscador.SelectedValue.ToString());
                    nome = txtNome.Text.Trim();
                }

                dgResultado.DataSource = _campanhaService.Listar(idRegistro, idDiscador, nome, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            //for (int i = dgResultado.Columns["Data"].Index + 1; i < dgResultado.Columns.Count; i++)
            //{
            //    dgResultado.Columns[i].Visible = false;
            //}

            //dgResultado.ClearSelection();
            //dgResultado.CurrentCell = null;
        }

        private void IniciarNovoRegistro()
        {
            CampanhaForm f = new CampanhaForm("NOVA CAMPANHA", 0);

            ExibirForm(f);

            if (f.Atualizar)
            {
                CarregarGrid(false);
            }
        }

        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                CampanhaForm f = new CampanhaForm("DETALHES DA CAMPANHA", id);

                ExibirForm(f);

                if (f.Atualizar)
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

        private void DuplicarCampanha(int idcampanha, bool duplicar, Tabulador.Dominio.Entidades.Campanha _novaCampanha)
        {
            CampanhaForm f = new CampanhaForm("DETALHES DA CAMPANHA", idcampanha, duplicar, _novaCampanha);

            if (!f.espelho)
                ExibirForm(f);

            if (f.Atualizar)
            {
                CarregarGrid(false);
            }
        }

        #endregion METODOS

        #region EVENTOS        

        private void ListaCampanhaForm_Load(object sender, System.EventArgs e)
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

        private void btnBuscaRapida_Click(object sender, System.EventArgs e)
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

        private void btnPesquisar_Click(object sender, System.EventArgs e)
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

        private void chkListarAtivos_CheckedChanged(object sender, System.EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        private void btnNovo_Click(object sender, System.EventArgs e)
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

        private void btnFechar_Click(object sender, System.EventArgs e)
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

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            try
            {
                bool podeDuplicar = _idCampanha > 0 ? true : false;
                if (!podeDuplicar)
                {
                    MessageBox.Show("Selecione a campanha que deseja espelhar!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Deseja Espelhar a Campanha: " + _nomeCampanha, "Aviso do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    CampanhaEspelhoForm f = new CampanhaEspelhoForm();

                    ExibirForm(f);

                    if (f.Cancelar)
                    {
                        return;
                    }
                    else
                    {
                        _duplicarCampanha = true;
                        DuplicarCampanha(_idCampanha, _duplicarCampanha, f._novaCampanha);
                        _idCampanha = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível duplicar campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgResultado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _idCampanha = (int)dgResultado.Rows[e.RowIndex].Cells["Id"].Value;
                _nomeCampanha = (string)dgResultado.Rows[e.RowIndex].Cells["Nome"].Value;
            }
        }

        #endregion EVENTOS
    }
}
