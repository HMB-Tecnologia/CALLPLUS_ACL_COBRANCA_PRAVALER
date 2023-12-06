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

namespace Callplus.CRM.Administracao.App.Planejamento.Pausa
{
    public partial class ListaPausaForm : Form
    {
        public ListaPausaForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _pausaService = new PausaService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly CampanhaService _campanhaService;
        private readonly PausaService _pausaService;
        private readonly ILogger _logger;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;

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
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int idCampanha = -1;
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
                    nome = txtNome.Text.Trim();
                }

                dgResultado.DataSource = _pausaService.Listar(idRegistro, nome, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount + " registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data Criação"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgResultado.Columns["Data Início"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgResultado.Columns["Data Término"].DefaultCellStyle.Format = "dd/MM/yyyy";

            //for (int i = dgResultado.Columns["Indicação"].Index + 1; i < dgResultado.Columns.Count; i++)
            //{
            //    dgResultado.Columns[i].Visible = false;
            //}
        }

        private void IniciarNovoRegistro()
        {
            var f = new PausaForm("NOVO ARQUIVO DE PAUSAS", null);

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
                var arquivoDePausa = new ArquivoDePausa();

                arquivoDePausa.id = (int)dgResultado.Rows[linha].Cells["ID"].Value;
                arquivoDePausa.nome = dgResultado.Rows[linha].Cells["Nome"].Value.ToString();
                arquivoDePausa.dataInicio = Convert.ToDateTime(dgResultado.Rows[linha].Cells["Data Início"].Value.ToString());
                arquivoDePausa.dataTermino = Convert.ToDateTime(dgResultado.Rows[linha].Cells["Data Término"].Value.ToString());
                arquivoDePausa.ativo = dgResultado.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;
                arquivoDePausa.observacao = dgResultado.Rows[linha].Cells["Observação"].Value.ToString();
                arquivoDePausa.caminhoArquivo = dgResultado.Rows[linha].Cells["Caminho Arquivo"].Value.ToString();

                var f = new PausaForm("DETALHES DO ARQUIVO", arquivoDePausa);

                ExibirForm(f);

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

            return !mensagens.Any();
        }

        #endregion METODOS

        #region EVENTOS        

        private void ListaMailingForm_Load(object sender, System.EventArgs e)
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

        #endregion EVENTOS

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }
    }
}
