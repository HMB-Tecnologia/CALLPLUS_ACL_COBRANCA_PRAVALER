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

namespace Callplus.CRM.Administracao.App.Qualidade.PlanoParaComparacao
{
    public partial class ListaPlanoParaComparacaoForm : Form
    {
        public ListaPlanoParaComparacaoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _planoService = new PlanoPorOperadoraParaComparacaoService();
            
            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly PlanoPorOperadoraParaComparacaoService _planoService;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            CarregarOperadoras();
            CarregarTiposDePlano();
            CarregarCampanhas();
        }

        private void CarregarOperadoras()
        {
            IEnumerable<Operadora> retorno = _planoService.ListarOperadora();
            cmbOperadora.PreencherComTodos(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarTiposDePlano()
        {
            IEnumerable<TipoDePlanoPorOperadora> retorno = _planoService.ListarTipoDePlanoPorOperadora();
            cmbTipoDePlano.PreencherComTodos(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> retorno = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int id = -1;
            int idOperadora = -1;
            int idTipoDePlano = -1;
            int idCampanha = -1;
            bool ativo = chkListarAtivos.Checked;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        id = int.Parse(txtBuscaRapida.Text);
                }
                else
                {
                    idOperadora = int.Parse(cmbOperadora.SelectedValue.ToString());
                    idTipoDePlano = int.Parse(cmbTipoDePlano.SelectedValue.ToString());
                    idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                }

                dgResultado.DataSource = _planoService.Listar(id, idOperadora, idTipoDePlano, idCampanha, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }
        
        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                PlanoParaComparacaoForm f = new PlanoParaComparacaoForm("DETALHES DO PLANO", id);

                f.Iniciar();

                if (f.atualizar)
                {
                    CarregarGrid(false);
                }
            }
        }

        private void IniciarNovoRegistro()
        {
            PlanoParaComparacaoForm f = new PlanoParaComparacaoForm("NOVO PLANO", -1);

            f.Iniciar();

            if (f.atualizar)
            {
                CarregarGrid(false);
            }
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();

            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                {
                    mensagens.Add("[Id] deve ser informado!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            for (int i = dgResultado.Columns["Data"].Index + 1; i < dgResultado.Columns.Count; i++)
            {
                dgResultado.Columns[i].Visible = false;
            }
        }

        #endregion METODOS

        #region EVENTOS

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

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
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

        private void ListaPlanoParaComparacaoForm_Load(object sender, EventArgs e)
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

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        #endregion EVENTOS
    }
}
