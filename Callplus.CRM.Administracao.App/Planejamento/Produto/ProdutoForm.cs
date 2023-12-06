using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
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

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComSelecione(_campanhas, x => x.Id, x => x.Nome);
        }

        private void CarregarConfiguracaoInicial()
        {
            CarregarTipos();
            CarregarCampanhas();
            CarregarFaixasDeRecarga(null, true);
            CarregarScriptDeOferta(-1, true);
            ConfigurarCamposFaixaDeRecargaDoProduto(habilitarId: false, habilitarComboFaixa: false, habilitarAtivo: false);
            ConfigurarTabsDeDetalhes();

            if (_produto != null)
            {
                CarregarDadosDoProduto();
                CarregarFaixasDeRecargaDoProduto(_produto.Id);
                DesabilitarCampos();
            }
        }

        private void ConfigurarTabsDeDetalhes()
        {
            bool edicao = _produto != null;

            tabDetalhesProduto.Enabled = (edicao == true);

        }

        private void CarregarDadosDoProduto()
        {
            txtIdProduto.Text = _produto.Id.ToString();
            cmbTipoDeProduto.SelectedValue = _produto.IdTipoDeProduto.ToString();
            cmbCampanha.SelectedValue = _produto.IdCampanha.ToString();
            txtNome.Text = _produto.Nome;
            txtOrdem.Text = _produto.Ordem.ToString();
            txtValor.Text = _produto.Valor.ToString();
            if (_produto.IdScriptOferta != null)
                cmbScriptOferta.SelectedValue = _produto.IdScriptOferta.ToString();
            chkAtivo.Checked = _produto.Ativo;
            chkAtivoBko.Checked = (_produto.AtivoBko ?? false);
            txtObservacao.Text = _produto.Observacao;
        }

        private void CarregarFaixasDeRecarga(int? id, bool? ativo)
        {
            _faixasDeRecarga = _faixasDeRecargaService.ListarFaixasDeRecarga(id, ativo);
            cmbFaixaDeRecarga.PreencherComSelecione(_faixasDeRecarga, x => x.Id, x => x.Nome);
        }

        private void CarregarFaixasDeRecargaDoProduto(long idProduto)
        {
            _faixasDeRecargaDoProduto = _faixasDeRecargaService.ListarFaixasDeRecargaDoProduto(idProduto);
            dgFaixaDeRecarga.DataSource = _faixasDeRecargaDoProduto;
        }

        private void CarregarScriptDeOferta(int? id, bool ativo)
        {
            _scriptsdeAtendimento = _scriptDeAtendimentoService.Listar(id, ativo);
            cmbScriptOferta.PreencherComSelecione(_scriptsdeAtendimento, x => x.Id, x => $"{x.Id} - {x.Nome}");
        }

        private void ConfigurarBotoesFaixaDeRecarga(bool habilitarNovo, bool habilitarCancelar, bool habilitarSalvar)
        {
            tsFaixadeRecarga_btnNovo.Enabled = habilitarNovo;
            tsFaixaDeRecarga_btnCancelar.Enabled = habilitarCancelar;
            tsFaixaDeRecarga_btnSalvar.Enabled = habilitarSalvar;
        }

        private void ConfigurarCamposFaixaDeRecargaDoProduto(bool habilitarId, bool habilitarComboFaixa, bool habilitarAtivo)
        {
            txtIdProdutoPermitido.Resetar(habilitar: habilitarId, limparTexto: true);
            chkAtivoFaixaDeRecarga.Enabled = habilitarAtivo;
            cmbFaixaDeRecarga.ResetarComSelecione(habilitar: habilitarComboFaixa);
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
                if (cmbScriptOferta.TextoEhSelecione() == false && string.IsNullOrEmpty(cmbScriptOferta.Text) == false)
                    _produto.IdScriptOferta = int.Parse(cmbScriptOferta.SelectedValue.ToString());
                _produto.Nome = txtNome.Text.Trim();
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

                _produtoService.Gravar(_produto);

                MessageBox.Show($"Produto {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Atualizar = true;

            }

        }

        private void NovaFaixaDeRecarga()
        {
            ConfigurarBotoesFaixaDeRecarga(habilitarNovo: false, habilitarCancelar: true, habilitarSalvar: true);
            ConfigurarCamposFaixaDeRecargaDoProduto(habilitarId: false, habilitarComboFaixa: true, habilitarAtivo: true);
        }

        private void IniciarEdicaoFaixaDeRecargaDoProduto(int linha)
        {
            if (linha >= 0)
            {
                ConfigurarBotoesFaixaDeRecarga(habilitarNovo: false, habilitarCancelar: true, habilitarSalvar: true);
                ConfigurarCamposFaixaDeRecargaDoProduto(habilitarId: false, habilitarComboFaixa: false, habilitarAtivo: true);

                int id = (int)dgFaixaDeRecarga.Rows[linha].Cells[nameof(colDgFaixaDeRecarga_Id)].Value;

                var faixaDoProduto = _faixasDeRecargaDoProduto.FirstOrDefault(x => x.Id == id);

                txtIdProdutoPermitido.Text = faixaDoProduto.Id.ToString();
                cmbFaixaDeRecarga.SelectedValue = faixaDoProduto.IdFaixaDeRecarga.ToString();
                chkAtivoFaixaDeRecarga.Checked = faixaDoProduto.Ativo;
            }

        }

        private void RetornarMascaraMonetaria(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text) == false)
                txt.Text = double.Parse(txt.Text).ToString("N2");
        }

        private void SalvarFaixaDeRecargaDoProduto()
        {
            if (VerificarSePodeSalvarFaixaDeRecargaDoProduto())
            {
                var produtoPermitido = new ProdutoPermitidoParaFaixaDeRecarga();

                int idProdutoPermitido = 0;
                bool edicao = false;

                if (int.TryParse(txtIdProdutoPermitido.Text, out idProdutoPermitido))
                {
                    produtoPermitido.Id = idProdutoPermitido; //edição
                    edicao = true;
                }

                produtoPermitido.IdProduto = _produto.Id;
                produtoPermitido.IdFaixaDeRecarga = int.Parse(cmbFaixaDeRecarga.SelectedValue.ToString());
                produtoPermitido.Ativo = chkAtivoFaixaDeRecarga.Checked;
                produtoPermitido.FaixaDeRecarga = cmbFaixaDeRecarga.Text;

                _faixasDeRecargaService.Salvar(produtoPermitido);

                CarregarFaixasDeRecargaDoProduto(_produto.Id);

                MessageBox.Show($"Faixa de recarga {(edicao ? "editada" : "associada")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ConfigurarBotoesFaixaDeRecarga(habilitarNovo: true, habilitarCancelar: false, habilitarSalvar: false);
                ConfigurarCamposFaixaDeRecargaDoProduto(habilitarId: false, habilitarComboFaixa: false, habilitarAtivo: false);
            }
        }

        private void TirarMascaraMonetaria(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.Replace("R$", "").Trim();
        }

        private bool VerificarSePodeSalvarFaixaDeRecargaDoProduto()
        {

            var mensagens = new List<string>();

            if (cmbFaixaDeRecarga.TextoEhSelecione() || string.IsNullOrEmpty(cmbFaixaDeRecarga.Text))
                mensagens.Add("Favor selecionar a faixa de recarga.");
            else
            {
                int idFaixaSelecionada = 0;
                int.TryParse(cmbFaixaDeRecarga.SelectedValue.ToString(), out idFaixaSelecionada);

                if (idFaixaSelecionada == 0)//inclusão
                {

                    if (_faixasDeRecargaDoProduto.Any(x => x.IdFaixaDeRecarga == idFaixaSelecionada))
                        mensagens.Add("A Faixa de Recarga Selecionada já está vinculada a este produto");
                }
            }

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
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

        private void tsFaixadeRecarga_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                NovaFaixaDeRecarga();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível incluir uma nova faixa de recarga!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFaixaDeRecarga_btnSalvar_Click_1(object sender, EventArgs e)
        {
            try
            {
                SalvarFaixaDeRecargaDoProduto();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível salvar faixa de recarga!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFaixaDeRecarga_btnCancelar_Click_1(object sender, EventArgs e)
        {
            ConfigurarBotoesFaixaDeRecarga(habilitarNovo: true, habilitarCancelar: false, habilitarSalvar: false);
            ConfigurarCamposFaixaDeRecargaDoProduto(habilitarId: false, habilitarComboFaixa: false, habilitarAtivo: false);

        }

        private void dgFaixaDeRecarga_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarEdicaoFaixaDeRecargaDoProduto(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da faixa de recarga!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion EVENTOS
    }
}
