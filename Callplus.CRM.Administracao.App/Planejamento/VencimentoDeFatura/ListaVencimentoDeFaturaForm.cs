using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.VencimentoDeFatura
{
    public partial class ListaVencimentoDeFatura : Form
    {
        public ListaVencimentoDeFatura()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _vencimentoService = new VencimentoDeFaturaService();

            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly ILogger _logger;
        private readonly VencimentoDeFaturaService _vencimentoService;
        
        #endregion VARIAVEIS

        #region MÉTODOS

        public static void ExibirForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            cmbDiaAtivacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDiaAtivacao.Text = "TODOS";
        }

        private void IniciarNovoRegistro()
        {
            VencimentoDeFaturaDetalhe f = new VencimentoDeFaturaDetalhe("NOVO VENCIMENTO DE FATURA", 0);

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
                int Id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                VencimentoDeFaturaDetalhe f = new VencimentoDeFaturaDetalhe("EDITAR VENCIMENTO", Id);

                ExibirForm(f);

                if (f.atualizar)
                {

                    CarregarGrid(false);
                }
            }
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int diaDeAtivacao = -1;
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
                    if (!cmbDiaAtivacao.TextoEhTodos())
                        diaDeAtivacao = int.Parse(cmbDiaAtivacao.Text);
                    else
                        diaDeAtivacao = -1;
                }

                dgResultado.DataSource = _vencimentoService.ListarExibicao(idRegistro, diaDeAtivacao, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Id"].Width = 50;
            dgResultado.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultado.Columns["Dia Ativação"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultado.Columns["Ordem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultado.Columns["Fechamento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultado.Columns["Vencimento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultado.Columns["Ativo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgResultado.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        #endregion MÉTODOS

        #region EVENTOS

        private void ListarVencimentoFaturaClaro_Load(object sender, EventArgs e)
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

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        #endregion EVENTOS
    }
}
