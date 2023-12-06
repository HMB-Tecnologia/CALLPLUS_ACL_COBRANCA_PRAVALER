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

namespace Callplus.CRM.Administracao.App.Qualidade.Checklist
{
    public partial class ListaChecklistForm : Form
    {
        public ListaChecklistForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _checklistService = new ChecklistService();
            _produtoService = new ProdutoService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly ChecklistService _checklistService;
        private readonly ProdutoService _produtoService;

        private IEnumerable<Campanha> _campanhas;

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

            CarregarCampanhas();
            CarregarProdutos(-1);
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }
        
        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int idCampanha = -1;
            int idProduto = -1;
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
                    idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                    idProduto = int.Parse(cmbProduto.SelectedValue.ToString());
                    nome = txtNome.Text.Trim();
                }

                dgResultado.DataSource = _checklistService.Listar(idRegistro, idCampanha, idProduto, nome, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                //RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            for (int i = dgResultado.Columns["Data"].Index + 1; i < dgResultado.Columns.Count; i++)
            {
                dgResultado.Columns[i].Visible = false;
            }
        }

        private void IniciarNovoRegistro()
        {
            ChecklistForm f = new ChecklistForm("NOVO CHECKLIST", -1);

            ExibirForm(f);

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

                ChecklistForm f = new ChecklistForm("DETALHES DO CHECKLIST", id);

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

        private void CarregarProdutos(object idCampanha)
        {
            int id = -1;

            if (!int.TryParse(idCampanha.ToString(), out id)) return;

            IEnumerable<Produto> retorno = _produtoService.Listar(-1, id, -1, true);
            cmbProduto.PreencherComTodos(retorno, x => x.Id, x => x.Nome);
        }

        #endregion METODOS

        #region EVENTOS

        private void ListaChecklistForm_Load(object sender, System.EventArgs e)
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

        private void cmbCampanha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarProdutos(cmbCampanha.SelectedValue);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os produtos da Campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
