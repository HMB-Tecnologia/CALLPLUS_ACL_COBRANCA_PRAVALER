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

namespace Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento
{
    public partial class ListaFeedbackDeAvaliacaoForm : Form
    {
        public ListaFeedbackDeAvaliacaoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _feedbackDaAvaliacaoDeAtendimentoService = new FeedbackDaAvaliacaoDeAtendimentoService();
            _usuarioService = new UsuarioService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly FeedbackDaAvaliacaoDeAtendimentoService _feedbackDaAvaliacaoDeAtendimentoService;
        private readonly UsuarioService _usuarioService;

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
            CarregarAuditores();

            cmbStatus.Text = "PENDENTE";
        }

        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }
        
        private void CarregarAuditores()
        {
            IEnumerable<Usuario> _avaliadores = _usuarioService.ListarAvaliadores(true, -1, -1);
            cmbAuditor.PreencherComTodos(_avaliadores, avaliador => avaliador.Id, avaliador => avaliador.Nome);
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int idCampanha = -1;
            int idAuditor = -1;

            DateTime dataInicial = txtDataInicial.Value.Date;
            DateTime dataFinal = txtDataFinal.Value.Date.AddDays(1);

            int idStatus = -1;

            if (cmbStatus.Text == "PENDENTE")
                idStatus = 0;
            else if (cmbStatus.Text == "REALIZADO")
                idStatus = 1;

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
                }

                dgResultado.DataSource = _feedbackDaAvaliacaoDeAtendimentoService.Listar(idRegistro, idCampanha, dataInicial, dataFinal, idStatus, idAuditor);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data Avaliação"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgResultado.Columns["Data Feedback"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            for (int i = dgResultado.Columns["Data Feedback"].Index + 1; i < dgResultado.Columns.Count; i++)
            {
                dgResultado.Columns[i].Visible = false;
            }
        }
                
        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int idAvaliacao = Convert.ToInt32(dgResultado.Rows[linha].Cells["Id"].Value);
                int idFormulario = Convert.ToInt32(dgResultado.Rows[linha].Cells["Id Formulário"].Value);
                string avaliador = dgResultado.Rows[linha].Cells["Avaliador"].Value.ToString();
                string auditor = dgResultado.Rows[linha].Cells["Auditor"].Value.ToString();
                string dataFeedback = dgResultado.Rows[linha].Cells["Data Feedback"].Value.ToString();

                AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("DETALHES DA AVALIAÇÃO", idAvaliacao, idFormulario, avaliador, auditor, dataFeedback);

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
            else
            {
                if (txtDataFinal.Value.Date < txtDataInicial.Value.Date)
                {
                    mensagens.Add("[Data Final] não pode ser menor que a Data Inicial!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        #endregion METODOS

        #region EVENTOS        

        private void ListaFeedbackDeAvaliacaoForm_Load(object sender, System.EventArgs e)
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
        
        #endregion EVENTOS        
    }
}
