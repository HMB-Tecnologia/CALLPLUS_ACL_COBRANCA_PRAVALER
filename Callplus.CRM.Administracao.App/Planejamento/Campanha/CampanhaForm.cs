using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.TipoDiscagem;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Campanha
{
    public partial class CampanhaForm : Form
    {
        public CampanhaForm(string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _discadorService = new DiscadorService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _mailingService = new MailingService();

            if (id > 0)
                _campanha = _campanhaService.RetornarCampanha(id);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        public bool Atualizar { get; set; }
        private readonly CampanhaService _campanhaService;
        private readonly DiscadorService _discadorService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private readonly ILogger _logger;
        private readonly ScriptDeAtendimentoService _scriptDeAtendimentoService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private readonly MailingService _mailingService;
        private Tabulador.Dominio.Entidades.Campanha _campanha;
        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarBancos()
        {
            IEnumerable<Banco> _banco = _campanhaService.ListarBanco(-1, true);
            IEnumerable<Banco> _bancoDaCampanha = _campanhaService.ListarBancoDaCampanha(_campanha.Id, true);

            if (_banco != null)
            {
                foreach (var item in _banco)
                {
                    clbBanco.Items.Add(item.Nome, _bancoDaCampanha.Where(x => x.Id == item.Id).Any());
                }
            }
        }

        private void CarregarClbStatusDeAtendimento(bool listarMarcados)
        {
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> _status = _statusDeAtendimentoService.Listar(null, true);
            IEnumerable<CampanhaDoStatusDeAtendimento> _campanhasDoStatusDeAtendimento;

            if (_campanha != null)
            {
                _campanhasDoStatusDeAtendimento = _statusDeAtendimentoService.ListarCampanhaDoStatus(_campanha.Id, true);
            }
            else
            {
                _campanhasDoStatusDeAtendimento = _statusDeAtendimentoService.ListarCampanhaDoStatus(0, true);
            }

            clbStatusDeAtendimento.Items.Clear();

            if (_status != null)
            {
                foreach (var item in _status)
                {
                    clbStatusDeAtendimento.Items.Add(item.Id + " - " + item.Nome, _campanhasDoStatusDeAtendimento.Where(x => x.IdStatusDeAtendimento == item.Id).Any());
                }
            }
        }

        private void CarregarComboStatusDeAtendimento()
        {
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> _status = _statusDeAtendimentoService.Listar(null, true);
            cmbStatusAutomatico.PreencherComSelecione(_status, x => x.Id, x => x.Nome);
            cmbStatusAutomaticoAceite.PreencherComSelecione(_status, x => x.Id, x => x.Nome);
        }

        private void CarregarConfiguracaoInicial()
        {
            CarregarDiscadores();
            CarregarTipoDeDiscagem();
            CarregarMailingsIndicacao();
            CarregarLayoutDinamico();
            CarregarComboStatusDeAtendimento();
            RemoverTabsDesncessarias();
            cmbTipoDeDiscagem.SelectedIndex = 0;

            if (_campanha != null)
            {
                CarregarDados();
                CarregarBancos();
                CarregarFormaDePagamento();
                CarregarClbStatusDeAtendimento(listarMarcados: true);
            }
            else
            {
                CarregarClbStatusDeAtendimento(listarMarcados: false);
            }
        }

        private void CarregarMailingsIndicacao()
        {
            IEnumerable<Tabulador.Dominio.Entidades.Mailing> _mailings = _mailingService.Listar(-1, true).Where(x => x.indicacao == true && x.idStatusProcessamento != 4);
            cmbMailingIndicacao.PreencherComSelecione(_mailings, x => x.id, x => x.nome);
        }

        private void CarregarTipoDeDiscagem()
        {
            IEnumerable<Tabulador.Dominio.Entidades.TipoDeDiscagem> _tipoDeDiscagem = _discadorService.TipoDeDiscagemListar(true);
            cmbTipoDeDiscagem.PreencherComSelecione(_tipoDeDiscagem, x => x.Id, x => x.Nome);
        }

        private void CarregarDados()
        {
            txtNome.Text = _campanha.Nome;
            cmbDiscador.SelectedValue = _campanha.IdDiscador.ToString();
            cmbTipoDeDiscagem.SelectedValue = _campanha.IdTipoDeDiscagem.ToString();
            cmbLayoutCampoDinamicoOperacao.SelectedValue = _campanha.IdLayoutCampoDinamico.ToString();
            cmbLayoutCampoDinamicoBko.SelectedValue = _campanha.IdLayoutCampoDinamicoBko.ToString();
            cmbStatusAutomatico.SelectedValue = _campanha.IdStatusTabulacaoAutomatica.ToString();
            if (cmbStatusAutomatico.SelectedValue is null) cmbStatusAutomatico.SelectedValue = "-1";
            cmbStatusAutomaticoAceite.SelectedValue = _campanha.IdStatusTabulacaoAutomaticaVenda.ToString();
            if (cmbStatusAutomaticoAceite.SelectedValue is null) cmbStatusAutomaticoAceite.SelectedValue = "-1";
            txtAfterCall.Text = _campanha.AfterCall.ToString();
            txtMetaDeVenda.Text = _campanha.MetaVenda.ToString();
            chkAtivo.Checked = _campanha.Ativo;
            chkDiscagemManual.Checked = _campanha.HabilitaDiscagemManual;
            chkHistorico.Checked = _campanha.HabilitaHistorico;
            chkIndicacao.Checked = _campanha.HabilitaIndicacao;
            chkSimulador.Checked = _campanha.HabilitaComparadorDePlanos;
            txtObservacao.Text = _campanha.Observacao;
            cmbMailingIndicacao.SelectedValue = _campanha.IdMailingCadastroManual.ToString();
        }

        private void CarregarDiscadores()
        {
            IEnumerable<Discador> _discador = _discadorService.Listar(-1, true);
            cmbDiscador.PreencherComSelecione(_discador, x => x.Id, x => x.Nome);
        }

        private void CarregarFormaDePagamento()
        {
            IEnumerable<FormaDePagamento> _formaDePagamento = _campanhaService.ListarFormasDePagamento(-1, true);
            IEnumerable<FormaDePagamento> _formaDePagamentoDaCampanha = _campanhaService.ListarFormasDePagamentoDaCampanha(_campanha.Id, true);

            if (_formaDePagamento != null)
            {
                foreach (var item in _formaDePagamento)
                {
                    clbFormaPagamento.Items.Add(item.Nome, _formaDePagamentoDaCampanha.Where(x => x.Id == item.Id).Any());
                }
            }
        }

        private void CarregarLayoutDinamico()
        {
            IEnumerable<Tabulador.Dominio.Entidades.LayoutDinamico.LayoutDeCampoDinamico> _layout = _layoutDinamicoService.ListarLayout(-1, true);
            cmbLayoutCampoDinamicoOperacao.PreencherComTodos(_layout, x => x.Id, x => x.Nome);
            cmbLayoutCampoDinamicoBko.PreencherComTodos(_layout, x => x.Id, x => x.Nome);
        }

        private void GravarCampanha()
        {
            if (PodeSalvar()) return;

            bool edicao = true;

            if (_campanha == null)
            {
                edicao = false;

                _campanha = new Tabulador.Dominio.Entidades.Campanha();

                _campanha.IdCriador = AdministracaoMDI._usuario.Id;
            }

            //Endereço de importação único para esta operação
            _campanha.EnderecoDeImportacaoDoMailing = @"\\10.0.1.134\mailing\IMPORTAR\QUINTO_ANDAR\";
            _campanha.Nome = txtNome.Text.Replace(" ", "_");
            _campanha.Ativo = chkAtivo.Checked;
            _campanha.Observacao = txtObservacao.Text;
            _campanha.IdModificador = AdministracaoMDI._usuario.Id;

            if (int.TryParse(cmbDiscador.SelectedValue.ToString(), out int idDiscador))
            {
                _campanha.IdDiscador = idDiscador;
            }

            if (int.TryParse(cmbTipoDeDiscagem.SelectedValue.ToString(), out int idTipoDiscagem))
            {
                _campanha.IdTipoDeDiscagem = idTipoDiscagem;
            }

            if (int.TryParse(txtAfterCall.Text, out int afterCall))
            {
                _campanha.AfterCall = afterCall;
            }

            if (int.TryParse(txtMetaDeVenda.Text, out int metaDeVenda))
            {
                _campanha.MetaVenda = metaDeVenda;
            }

            if (int.TryParse(cmbLayoutCampoDinamicoOperacao.SelectedValue.ToString(), out int idLayoutCampoDinamico))
            {
                _campanha.IdLayoutCampoDinamico = idLayoutCampoDinamico;
            }

            if (int.TryParse(cmbLayoutCampoDinamicoBko.SelectedValue.ToString(), out int idLayoutCampoDinamicoBko))
            {
                _campanha.IdLayoutCampoDinamicoBko = idLayoutCampoDinamicoBko;
            }

            if (int.TryParse(cmbStatusAutomaticoAceite.SelectedValue.ToString(), out int idStatusTabulacaoAutomaticaAceite))
            {
                _campanha.IdStatusTabulacaoAutomaticaVenda = idStatusTabulacaoAutomaticaAceite;
            }

            if (int.TryParse(cmbStatusAutomatico.SelectedValue.ToString(), out int idStatusTabulacaoAutomatica))
            {
                _campanha.IdStatusTabulacaoAutomatica = idStatusTabulacaoAutomatica;
            }

            _campanha.HabilitaCadastroManual = chkCadastroManual.Checked;

            if (int.TryParse(cmbMailingIndicacao.SelectedValue.ToString(), out int idMailingIndicacao))
            {
                _campanha.IdMailingCadastroManual = idMailingIndicacao;
            }
            else
            {
                _campanha.HabilitaCadastroManual = false;
            }

            _campanha.HabilitaDiscagemManual = chkDiscagemManual.Checked;
            _campanha.HabilitaHistorico = chkHistorico.Checked;
            _campanha.HabilitaIndicacao = chkIndicacao.Checked;
            _campanha.HabilitaComparadorDePlanos = chkSimulador.Checked;
            _campanha.HabilitarContatoManual = chkDiscagemManual.Checked;

            string idsStatusDeAtendimento = RetornarStatusDeAtendimento();

            _campanha.Id = _campanhaService.Gravar(_campanha, idsStatusDeAtendimento);

            VerificarSeCriaNovoMailingIndicacao();

            MessageBox.Show($"Campanha {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();

            Atualizar = true;
        }

        private void VerificarSeCriaNovoMailingIndicacao()
        {
            if (cmbMailingIndicacao.TextoEhSelecione() && chkCadastroManual.Checked)
                 CriarNovoMailingIndicacao();
        }

        private bool PodeSalvar()
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtNome.Text))
                mensagens.Add("[Nome] deve ser preenchido");

            var formatoCorreto = new Regex("^[a-zA-Z0-9\\s_-]*$").IsMatch(txtNome.Text); //{2,20} permite espações no nome

            if (formatoCorreto == false)
                mensagens.Add("[Nome] não pode conter espaços ou caracteres especiais!");

            if (cmbDiscador.TextoEhSelecione())
                mensagens.Add("[Discador] deve ser selecionado");

            if (cmbTipoDeDiscagem.TextoEhSelecione())
                mensagens.Add("[Tipo de Discagem] deve ser selecionado");

            if (string.IsNullOrEmpty(txtAfterCall.Text))
                mensagens.Add("[After Call] deve ser preenchido");

            if (!int.TryParse(txtAfterCall.Text, out int afterCall))
                mensagens.Add("[After Call] deve ser numerico");

            if (string.IsNullOrEmpty(txtMetaDeVenda.Text))
                mensagens.Add("[Meta de Venda] deve ser preenchido");

            if (!int.TryParse(txtMetaDeVenda.Text, out int metaDeVenda))
                mensagens.Add("[Meta de Venda] deve ser numerico");

            if (cmbLayoutCampoDinamicoOperacao.TextoEhSelecione())
                mensagens.Add("[Campo Dinâmico Operação] deve ser selecionado");

            if (cmbLayoutCampoDinamicoBko.TextoEhSelecione())
                mensagens.Add("[Campo Dinâmico Back-Office] deve ser selecionado");

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any();
        }

        private void RemoverTabsDesncessarias()
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage4);
            tabControl1.TabPages.Remove(tabPage5);
        }

        private string RetornarStatusDeAtendimento()
        {
            string ids = "";
            foreach (var item in clbStatusDeAtendimento.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        #endregion METODOS

        #region EVENTOS

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarCampanha();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível salvar a campanha!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CampanhaForm_Load(object sender, EventArgs e)
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

        private void txtAfterCall_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMetaDeVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        #endregion EVENTOS

        private void CriarNovoMailingIndicacao()
        {
            var resultado = MessageBox.Show("Deseja criar um novo mailing indicação para esta campanha?",
                "Alerta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (resultado == DialogResult.No) return;

            var mailing = new Tabulador.Dominio.Entidades.Mailing();

            mailing.ativo = true;
            mailing.idCampanha = _campanha.Id;
            mailing.idCriador = AdministracaoMDI._usuario.Id;
            mailing.indicacao = true;
            mailing.nome = "MAILING_INDICACAO_" + txtNome.Text.Trim().Replace(" ", "_");
            mailing.observacao = "MAILING INDICACAO DA CAMPANHA" + txtNome.Text.ToUpper();
            mailing.idStatusProcessamento = 3;
            mailing.nomeArquivo = _campanha.EnderecoDeImportacaoDoMailing + 
                "MALING_DA_CAMPANHA_" + _campanha.Nome.ToUpper() + ".csv";

            var idMailing = _mailingService.Gravar(mailing);

            var idCampanha = _campanha.Id;

            _campanhaService.AtualizarDadosDeCadastroManual(idCampanha, idMailing);


            CarregarMailingsIndicacao();
            cmbMailingIndicacao.SelectedValue = idMailing.ToString();


        }

        private void chkCadastroManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCadastroManual.Checked)
                cmbMailingIndicacao.Enabled = true;
            else
            {
                cmbMailingIndicacao.ResetarComSelecione(true);
                cmbMailingIndicacao.Enabled = false;
            }
        }
    }
}