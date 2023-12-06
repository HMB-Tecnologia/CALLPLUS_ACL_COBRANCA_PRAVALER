using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using NLog;


namespace Callplus.CRM.Administracao.App.Planejamento.Aparelho
{
    public partial class AparelhoForm : Form
    {
        public AparelhoForm(int idAparelho, string titulo)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _produtoService = new ProdutoService();
            _campanhaService = new CampanhaService();
            _aparelhoService = new AparelhoService();

            if (idAparelho > 0)
                _aparelho = _aparelhoService.RetornarAparelho(idAparelho);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }


        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly ProdutoService _produtoService;
        private readonly CampanhaService _campanhaService;
        private readonly AparelhoService _aparelhoService;

        public bool atualizar { get; set; }

        private Tabulador.Dominio.Entidades.Aparelho _aparelho;
        private Tabulador.Dominio.Entidades.Campanha _campanha;
        private Tabulador.Dominio.Entidades.AparelhoDaCampanha _aparelhoDaCampanha;
        private Tabulador.Dominio.Entidades.FormaDePagamentoDeAparelho _formaDePagamentoDoAparelho;


        #endregion PROPRIEDADES


        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            if (_aparelho != null)
            {
                txtIdDoAparelho.Text = _aparelho.Id.ToString();
                txtNome.Text = _aparelho.Nome.ToString();
                txtGrupo.Text = _aparelho.Grupo.ToString();
                gbAparelhosDaCampanha.Enabled = false;
                lblTitulo.Text = "Editar Aparelho";

                CarregarCampanhasDoAparelho();
                CarregarCmbCampanha();
                CarregarCmbCampanha2();
                RealizarAjustesGridCampanha();
                PreencherCmbProdutoComSelecione();

            }
            else
            {
                gbCampanhas.Enabled = false;
                groupBox2.Enabled = false;
            }

        }

        private void RealizarAjustesGridCampanha()
        {
            dgCampanhas.Columns["Id"].Width = 20;
            dgCampanhas.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCampanhas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCampanhas.Columns["IdCampanha"].Width = 30;
            dgCampanhas.Columns["IdCampanha"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCampanhas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCampanhas.Columns["Campanha"].Width = 100;
            dgCampanhas.Columns["idAparelho"].Visible = false;
            dgCampanhas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCampanhas.Columns["Ativo"].Width = 50;
            dgCampanhas.Columns["Ativo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgCampanhas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void PreencherCmbProdutoComSelecione()
        {
            var listaFake = new List<KeyValuePair<int, string>>();
            cmbProduto.PreencherComSelecione(listaFake, x => x.Key, x => x.Value);
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void CarregarCampanhasDoAparelho()
        {
            dgCampanhas.DataSource = _aparelhoService.ListarAparelhosDaCampanha(_aparelho.Id);
        }

        private void CarregarCmbCampanha()
        {
            IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanha = _campanhaService.Listar(true);
            cmbCampanha.PreencherComSelecione(_campanha, x => x.Id, x => x.Nome);
        }

        private void CarregarCmbCampanha2()
        {
            IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanha = _campanhaService.ListarCampanhasDoAparelho(_aparelho.Id);
            cmbCampanha2.PreencherComSelecione(_campanha, x => x.Id, x => x.Nome);
        }

        private void CarregarCmbProdutosDaCampanha()
        {

            if (cmbCampanha2.TextoEhSelecione())
            {
                cmbProduto.Enabled = false;
                return;
            }

            ResetarGridPagamanto();

            int idCampanha = int.Parse(cmbCampanha2.SelectedValue.ToString());
            IEnumerable<Tabulador.Dominio.Entidades.Produto> _tipo = _produtoService.ListarProdutosDaCampanha(idCampanha);
            cmbProduto.PreencherComSelecione(_tipo, x => x.Id, x => x.Nome);

        }

        private void DesabilitarControlesTabPagamento()
        {
            tsCampanha_btnNovo.Enabled = false;
            tsCampanha_btnCancelar.Enabled = true;
            tsCampanha_btnSalvar.Enabled = true;
            gbAparelhosDaCampanha.Enabled = true;
            txtIdCampanhaDoAparelho.Enabled = false;
            gbAparelho.Enabled = false;
            txtIdCampanhaDoAparelho.Clear();
            tabControl1.TabPages.Remove(tbPagamento);
        }

        private void ResetarControlesTabCampanha()
        {
            tsCampanha_btnNovo.Enabled = true;
            tsCampanha_btnCancelar.Enabled = false;
            tsCampanha_btnSalvar.Enabled = false;
            gbAparelhosDaCampanha.Enabled = false;
            gbAparelho.Enabled = true;
            txtIdCampanhaDoAparelho.Clear();
            cmbCampanha.ResetarComSelecione(true);
            tabControl1.TabPages.Add(tbPagamento);
        }

        private void HabilitarControlesTabPagamento(bool habilitar)
        {
            cmbCampanha2.Enabled = habilitar;
            cmbProduto.Enabled = habilitar;
            tsPagamento_btnNovo.Enabled = habilitar;
            tsPagamento_btnCancelar.Enabled = !habilitar;
            tsProduto_btnSalvar.Enabled = !habilitar;
            gbAparelho.Enabled = habilitar;
            txtDescricao.Enabled = !habilitar;
            txtValor.Enabled = !habilitar;
            txtIdPagamento.Clear();
            txtValor.Clear();
            txtDescricao.Clear();
            if (!habilitar == true)
            {
                tabControl1.TabPages.Remove(tbCampanhas);
            }
            else
            {
                tabControl1.TabPages.Add(tbCampanhas);
            }

        }

        private bool PodeSalvarCampanha()
        {
            var mensagens = new List<string>();

            if (!string.IsNullOrEmpty(txtIdCampanhaDoAparelho.Text)) return true;

            foreach (DataGridViewRow row in dgCampanhas.Rows)
            {
                if (row.IsNewRow) continue;
                if (cmbCampanha.Text == row.Cells[nameof(Campanha)].Value.ToString())
                {
                    mensagens.Add("Este aparelho já está vinculada a esta campanha.");
                    break;
                }
            }

            if (cmbCampanha.TextoEhSelecione() || string.IsNullOrEmpty(cmbCampanha.Text))
                mensagens.Add("Selecione uma campanha para vincular ao aparelho.");

            ExibirMensagens(mensagens);
            return !mensagens.Any();

        }

        private bool PodeSalvarPagamento()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtDescricao.Text))
                mensagens.Add("Informe uma descrição para salvar o pagamento");

            if (string.IsNullOrEmpty(txtValor.Text))
                mensagens.Add("Informe um valor para salvar o pagamento");

            ExibirMensagens(mensagens);
            return !mensagens.Any();

        }

        private bool PodeSalvarAparelho()
        {
            var mensagens = new List<string>();

            if (String.IsNullOrEmpty(txtNome.Text))
                mensagens.Add("Informe um nome para salvar o aparelho");

            if (String.IsNullOrEmpty(txtGrupo.Text))
                mensagens.Add("Informe um grupo para salvar o grupo");

            ExibirMensagens(mensagens);
            return !mensagens.Any();
        }

        private bool PodeAdicionarNovaCampanha()
        {
            var mensagens = new List<string>();

            if (cmbCampanha2.TextoEhSelecione())
                mensagens.Add("Preenchar o campo NOME para salvar o aparelho");

            if (cmbProduto.TextoEhSelecione())
                mensagens.Add("Preencha o campo GRUPO para salvar o aparelho");

            ExibirMensagens(mensagens);
            return !mensagens.Any();

        }

        private bool ContemNaGrid()
        {
            string idCampanha = cmbCampanha.SelectedValue.ToString();
            bool contem = false;
            foreach (DataGridViewRow campanha in dgCampanhas.Rows)
            {
                if (campanha.Cells[1].Value.ToString() == idCampanha)
                {
                    contem = true;
                }
            }
            return contem;
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ListarPagamentosDoAparelho()
        {

            if (cmbProduto.TextoEhSelecione())
            {
                ResetarGridPagamanto();
                return;
            }

            CarregarDgPagamento();

        }

        private void CarregarDgPagamento()
        {
            int idProduto = int.Parse(cmbProduto.SelectedValue.ToString());
            dgPagamento.DataSource = _aparelhoService.ListarPagamentosDoAparelho(_aparelho.Id, idProduto);

            AjustarGridDgPagamento();

        }

        private void AjustarGridDgPagamento()
        {
            this.dgPagamento.Columns["IdAparelho"].Visible = false;
            this.dgPagamento.Columns["IdProduto"].Visible = false;
        }

        private void ResetarGridPagamanto()
        {
            cmbProduto.ResetarComSelecione(true);
            txtDescricao.Clear();
            txtValor.Clear();
            dgPagamento.DataSource = null;
            dgPagamento.Refresh();
            txtIdPagamento.Clear();
        }

        private void HabilitarControlesTabCampanha(bool habilitar)
        {
            tsCampanha_btnNovo.Enabled = !habilitar;
            tsCampanha_btnCancelar.Enabled = habilitar;
            tsCampanha_btnSalvar.Enabled = habilitar;
            gbAparelhosDaCampanha.Enabled = habilitar;
            gbAparelho.Enabled = !habilitar;
            txtIdCampanhaDoAparelho.Clear();
            cmbCampanha.ResetarComSelecione(habilitar);
            tabControl1.TabPages.Remove(tbPagamento);
        }

        private void CarregarDadosDoAparelhoDaCampanha(int linha)
        {
            if (linha >= 0)
            {
                HabilitarControlesTabCampanha(true);

                int id = (int)dgCampanhas.Rows[linha].Cells["Id"].Value;

                _aparelhoDaCampanha = _aparelhoService.RetornarCampanhaDoAparelho(id);
                txtIdCampanhaDoAparelho.Text = _aparelhoDaCampanha.Id.ToString();
                cmbCampanha.Text = _aparelhoDaCampanha.Campanha;
                chkAtivoAparelhoCampanha.Checked = _aparelhoDaCampanha.Ativo;
                cmbCampanha.Enabled = false;

            }
        }

        private void CarregarDadosDaFormaDePagamentoDoAparelho(int linha)
        {
            if (linha >= 0)
            {
                HabilitarControlesTabPagamento(false);

                int id = (int)dgPagamento.Rows[linha].Cells["Id"].Value;

                _formaDePagamentoDoAparelho = _aparelhoService.RetornarFormaDePagamentoDoAparelho(id);

                tabControl1.TabPages.Remove(tbCampanhas);
                txtDescricao.Text = _formaDePagamentoDoAparelho.Descricao;
                txtValor.Text = _formaDePagamentoDoAparelho.Valor.ToString();
                txtIdPagamento.Text = _formaDePagamentoDoAparelho.Id.ToString();
                chkPagamentoAtivo.Checked = _formaDePagamentoDoAparelho.Ativo;
            }
        }

        private void SalvarAparelho()
        {
            if (PodeSalvarAparelho())
            {
                if (_aparelho == null) _aparelho = new Tabulador.Dominio.Entidades.Aparelho();

                _aparelho.Nome = txtNome.Text;
                _aparelho.Grupo = txtGrupo.Text;
                _aparelho.Ativo = chkAparelhoAtivo.Checked;

                // Homologar
                _aparelhoService.GravarAparelho(_aparelho);

                atualizar = true;
                MessageBox.Show("Salvo com sucesso!");
                Close();

            }
        }

        private void SalvarAparelhoDaCampanha()
        {
            if (PodeSalvarCampanha())
            {
                if (_aparelhoDaCampanha == null) _aparelhoDaCampanha = new AparelhoDaCampanha();

                int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());

                _aparelhoDaCampanha.IdCampanha = idCampanha;
                _aparelhoDaCampanha.IdAparelho = _aparelho.Id;
                _aparelhoDaCampanha.Ativo = chkAtivoAparelhoCampanha.Checked;

                _aparelhoService.GravarCampanhaDoAparelho(_aparelhoDaCampanha);

                MessageBox.Show("Aparelho vinculado/alterado com sucesso.");
                //TODO: RECARREGAR GRID DE APARELHOS DA CAMPANHA 
                CarregarCampanhasDoAparelho();
                //TODO: RECARREGAR COMBO DE CAMPANHAS
                CarregarCmbCampanha();

                ResetarControlesTabCampanha();

            }
        }

        private void PermitirNumeroEhVirgula(object sender, KeyPressEventArgs e)
        {
            // Só permite números, virgula
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != ',')
            {
                e.Handled = true;
            }


            // Só permite um ponto
            if ((e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }


        }

        private void SalvarFormaDePagamento()
        {
            if (PodeSalvarPagamento())
            {
                if (_formaDePagamentoDoAparelho == null) _formaDePagamentoDoAparelho = new FormaDePagamentoDeAparelho();

                _formaDePagamentoDoAparelho.IdProduto = int.Parse(cmbProduto.SelectedValue.ToString());
                _formaDePagamentoDoAparelho.IdAparelho = int.Parse(txtIdDoAparelho.Text);
                _formaDePagamentoDoAparelho.Descricao = txtDescricao.Text;
                _formaDePagamentoDoAparelho.Valor = float.Parse(txtValor.Text.Replace(',', '.'));
                _formaDePagamentoDoAparelho.Ativo = chkPagamentoAtivo.Checked;

                int idPagamento = 0;

                if (int.TryParse(txtIdPagamento.Text, out idPagamento))
                {
                    _formaDePagamentoDoAparelho.Id = idPagamento;
                }

                // Homologar
                //_aparelhoService.GravarFormaDePagamentoDoAparelho(_formaDePagamentoDoAparelho);

                HabilitarControlesTabPagamento(true);

            }
        }

        private void IniciarNovoAparelhoDaCampanha()
        {
            _aparelhoDaCampanha = null;
            DesabilitarControlesTabPagamento();
        }

        private void CancelarAparelhoDaCampanha()
        {
            _aparelhoDaCampanha = null;
            ResetarControlesTabCampanha();
        }

        #endregion METODOS

        #region EVENTOS

        private void AparelhoForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbCampanha2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CarregarCmbProdutosDaCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os produtos da campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsCampanha_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarNovoAparelhoDaCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível habilitar o combo campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsCampanha_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarAparelhoDaCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível desabilitar o combo campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TsCampanha_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarAparelhoDaCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível salvar o aparelho da campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsPagamento_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                if (PodeAdicionarNovaCampanha())
                {
                    HabilitarControlesTabPagamento(false);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível habilitar um novo pagamento!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsPagamento_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarControlesTabPagamento(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível habilitar os controles de pagamento!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsProduto_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarFormaDePagamento();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível salvar a forma de pagamento!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ListarPagamentosDoAparelho();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível listar as formas de pagamento do aparelho!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgCampanhas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                try
                {
                    CarregarDadosDoAparelhoDaCampanha(e.RowIndex);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);

                    MessageBox.Show(
                        $"Não foi possível editar esta campanha do aparelho!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgPagamento_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDaFormaDePagamentoDoAparelho(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível editar esta forma de pagamento do aparelho!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                PermitirNumeroEhVirgula(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível restringir o campo valor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarAparelho();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível salvar o aparelho!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Eventos

    }
}
