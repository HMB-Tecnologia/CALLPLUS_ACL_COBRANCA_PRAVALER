using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Produto
{
    public partial class ProdutoForm : Form
    {
        public ProdutoForm(Usuario usuarioLogado, string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _produtoService = new ProdutoService();
            _faixasDeRecargaService = new FaixasDeRecargaService();
            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();
            _usuarioLogado = usuarioLogado;

            if (id > 0)
                _produto = _produtoService.RetornarProduto(id);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly Usuario _usuarioLogado;
        private readonly FaixasDeRecargaService _faixasDeRecargaService;
        private readonly ProdutoService _produtoService;
        private readonly ScriptDeAtendimentoService _scriptDeAtendimentoService;

        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;
        private IEnumerable<FaixaDeRecarga> _faixasDeRecarga;
        private IEnumerable<ProdutoPermitidoParaFaixaDeRecarga> _faixasDeRecargaDoProduto;
        private IEnumerable<ScriptDeAtendimento> _scriptsdeAtendimento;

        private Tabulador.Dominio.Entidades.Produto _produto;
        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            ShowIcon = true;
            MaximizeBox = false;
            MinimizeBox = false;

            cmbCampanha.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbTipoDeProduto.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            CarregarTipos();
            CarregarCampanhas();
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (_produto != null)
            {
                cmbTipoDeProduto.SelectedValue = _produto.IdTipoDeProduto.ToString();
                cmbCampanha.SelectedValue = _produto.IdCampanha.ToString();
                txtNome.Text = _produto.Nome;
                txtOrdem.Text = _produto.Ordem.ToString();
                txtValor.Text = _produto.Valor.ToString();
                chkAtivo.Checked = _produto.Ativo;
                chkAtivoBko.Checked = (_produto.AtivoBko ?? false);
                txtObservacao.Text = _produto.Observacao;

                CarregarFaixas();
                DesabilitarCampos();
            }
            else
                CarregarFaixasDeRecarga();
        }

        private void ApenasValorNumerico(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txt.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComSelecione(_campanhas, x => x.Id, x => x.Nome);
        }

        private void CarregarFaixas()
        {
            long idProduto = (_produto == null) ? -1 : (long)_produto.Id;

            IEnumerable<FaixaDeRecarga> _faixasDeRecarga = _faixasDeRecargaService.ListarFaixasDeRecarga(-1, true);
            IEnumerable<ProdutoPermitidoParaFaixaDeRecarga> _faixasDeRecargaDoProduto = _faixasDeRecargaService.ListarFaixasDeRecargaDoProduto(idProduto);

            if (_faixasDeRecarga != null)
            {
                foreach (var item in _faixasDeRecarga)
                {
                    clbFaixaDeRecarga.Items.Add(item.Id + " - " + item.Nome, _faixasDeRecargaDoProduto.Where(x => x.IdFaixaDeRecarga == item.Id).Any());
                }
            }
        }

        private void CarregarFaixasDeRecarga()
        {
            IEnumerable<FaixaDeRecarga> _faixasDeRecarga = _faixasDeRecargaService.ListarFaixasDeRecarga(-1, true);

            clbFaixaDeRecarga.Items.Clear();

            foreach (var item in _faixasDeRecarga)
            {
                clbFaixaDeRecarga.Items.Add(item.Id + " - " + item.Nome);
            }

            lblTotalRegistros.Text = "0 Registro(s)";
        }

        private void CarregarTipos()
        {
            IEnumerable<TipoDeProduto> tiposDeProdutos = _produtoService.ListarTipoDeProduto(true);
            cmbTipoDeProduto.PreencherComSelecione(tiposDeProdutos, x => x.Id, x => x.Nome);
        }

        private void DesabilitarCampos()
        {
            if (_produto != null)
            {
                cmbTipoDeProduto.Enabled = false;
                cmbCampanha.Enabled = false;
            }
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_produto == null)
                {
                    edicao = false;
                    _produto = new Tabulador.Dominio.Entidades.Produto();
                    _produto.Idcriador = _usuarioLogado.Id;
                }

                _produto.Ativo = chkAtivo.Checked;
                _produto.AtivoBko = chkAtivoBko.Checked;
                _produto.IdCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                _produto.IdTipoDeProduto = int.Parse(cmbTipoDeProduto.SelectedValue.ToString());
                _produto.Nome = txtNome.Text.Trim().ToUpper();
                _produto.Observacao = txtObservacao.Text.Trim();
                _produto.Ordem = int.Parse(txtOrdem.Text.Trim());
                _produto.IdModificador = _usuarioLogado.Id;

                decimal valor = 0;
                if (decimal.TryParse(txtValor.Text.Trim(), NumberStyles.Currency, CultureInfo.CurrentCulture, out valor))
                {
                    if (valor > 0)
                    {
                        _produto.Valor = decimal.Parse(txtValor.Text.ToString());
                    }
                }

                _produto.Id = _produtoService.Gravar(_produto);

                var produtoPermitido = new ProdutoPermitidoParaFaixaDeRecarga();

                produtoPermitido.IdProduto = _produto.Id;
                string idsFaixasDeRecarga = RetornarFaixaDeRecarga();

                produtoPermitido.Id = _faixasDeRecargaService.GravarProdutoPermitidoParaFaixaDeRecarga(produtoPermitido, idsFaixasDeRecarga);

                MessageBox.Show($"Produto {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Atualizar = true;

                this.Close();
            }

        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (cmbTipoDeProduto.TextoEhSelecione() || string.IsNullOrEmpty(cmbTipoDeProduto.Text))
                mensagens.Add("Favor selecionar o tipo de produto!");

            if (cmbCampanha.TextoEhSelecione() || string.IsNullOrEmpty(cmbCampanha.Text))
                mensagens.Add("Favor selecionar a campanha!");

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser informado.");

            int ordem = 0;
            if (int.TryParse(txtOrdem.Text, out ordem))
            {
                if (ordem < 0)
                    mensagens.Add("Informe uma ordem válida!");
            }
            else
                mensagens.Add("[Ordem] deve ser informado.");

            if (string.IsNullOrEmpty(txtValor.Text) == false)
            {
                decimal valor = 0;
                if (decimal.TryParse(txtValor.Text, out valor) == false)
                {
                    if (valor <= 0)
                        mensagens.Add("[Valor] deve ser válido!");
                }
            }

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        string RetornarFaixaDeRecarga()
        {
            string ids = "";
            foreach (var item in clbFaixaDeRecarga.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        private void RetornarMascaraMonetaria(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text) == false)
                txt.Text = double.Parse(txt.Text).ToString("N2");
        } 

        private void TirarMascaraMonetaria(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.Replace("R$", "").Trim();
        }

        #endregion METODOS

        #region EVENTOS

        private void ProdutoForm_Load(object sender, EventArgs e)
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível salvar o registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lnkTodos_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbFaixaDeRecarga.SetarTodosRegistros(check: true);
        }

        private void lnkNenhum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbFaixaDeRecarga.SetarTodosRegistros(check: false);
        }

        private void cmbTipoDeProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CarregarFaixasDeRecarga();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível carregar os Operadores do Supervisor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clbFaixaDeRecarga_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox clb = (CheckedListBox)sender;

            clb.ItemCheck -= clbFaixaDeRecarga_ItemCheck;
            clb.SetItemCheckState(e.Index, e.NewValue);
            clb.ItemCheck += clbFaixaDeRecarga_ItemCheck;

            lblTotalRegistros.Text = clbFaixaDeRecarga.CheckedItems.Count.ToString() + " Registro(s)";
        }

        #endregion EVENTOS

    }
}
