using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Campanha
{
    public partial class CampanhaForm : Form
    {
        public CampanhaForm(string titulo, int id, bool? duplicar = null, Tabulador.Dominio.Entidades.Campanha novaCampanha = null)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _discadorService = new DiscadorService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _statusDeOfertaService = new StatusDeAcordoService();
            _mailingService = new MailingService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _tipoDiscagemService = new TipoDiscagemService();
            _verificacaoService = new VerificacaoService();
            _duplicarCampanha = duplicar;
            _idCampanha = id;
            _novaCampanha = novaCampanha;

            if (id > 0 && Convert.ToBoolean(_duplicarCampanha) == false)
            {
                _campanha = _campanhaService.RetornarCampanha(id);
            }
            else if (id > 0 && Convert.ToBoolean(_duplicarCampanha) == true)
            {
                ColetarDadosCampanhaEspelho();

                if (_campanhaBase != null)
                    GravarCampanhaEspelhada();

                CampanhaForm f = new CampanhaForm("DETALHES DA CAMPANHA ESPELHADA", _campanha.Id);
                f.ShowDialog();

                f.Close();
                Atualizar = true;
                espelho = true;
                return;
            }

            InitializeComponent();

            lblTitulo.Text = titulo.ToString();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly DiscadorService _discadorService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private readonly ScriptDeAtendimentoService _scriptDeAtendimentoService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private readonly StatusDeAcordoService _statusDeOfertaService;
        private readonly MailingService _mailingService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly TipoDiscagemService _tipoDiscagemService;
        private Tabulador.Dominio.Entidades.Campanha _campanha;
        private Tabulador.Dominio.Entidades.Mailing _mailing;
        private bool? _duplicarCampanha = false;
        private int _idCampanha;
        private Tabulador.Dominio.Entidades.Campanha _novaCampanha;
        private Tabulador.Dominio.Entidades.Campanha _campanhaBase;
        private readonly VerificacaoService _verificacaoService;
        public bool espelho = false;
        private bool edicao;
        //private string[] bancos;
        //private string[] bancosSelecionados;
        private IEnumerable<Banco> _banco;
        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            CarregarDiscadores();
            CarregarTipoDeDiscagem();
            CarregarLayoutDinamico();
            CarregarScriptDeAtendimento();
            CarregarMailingsIndicacao();
            TravarScrollMouse();

            cmbTipoDeDiscagem.SelectedIndex = 0;

            CarregarBancos();
            CarregarStatusDeOferta();
            CarregarStatusDeAtendimento();
            CarregarFormaDePagamento();
            CarregarStatusDeAuditoria();
            CarregarTipoCampanha();

            if (_campanha != null)
                CarregarDados();
            else if (_campanha == null && Convert.ToBoolean(!_duplicarCampanha))
                CarregarCampoPadrão();

            tcCampanha.TabPages.Remove(tcCampanha_tpVarievalScript);

            cmbTipoCampanha.DropDown += CallplusUtil.Forms.CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbTipoDeDiscagem.DropDown += CallplusUtil.Forms.CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
        }

        private void CarregarCampoPadrão()
        {
            cmbScriptApresentacao.SelectedValue = 1.ToString();
            cmbScriptFinalizacao.SelectedValue = 2.ToString();
            cmbStatusAutomatico.SelectedValue = 3.ToString();
            cmbStatusAutomaticoAceite.SelectedValue = 9.ToString();
        }

        private void CarregarDados()
        {
            txtNome.Text = _campanha.Nome;
            cmbDiscador.SelectedValue = _campanha.IdDiscador.ToString();
            cmbTipoCampanha.SelectedValue = _campanha.idTipoDaCampanha.ToString();
            cmbTipoDeDiscagem.SelectedValue = _campanha.IdTipoDeDiscagem.ToString();
            txtAfterCall.Text = _campanha.AfterCall.ToString();
            txtMetaDeVenda.Text = _campanha.MetaVenda.ToString();

            cmbScriptApresentacao.SelectedValue = _campanha.IdScriptApresentacao.ToString();
            cmbScriptFinalizacao.SelectedValue = _campanha.IdScriptFinalizacao.ToString();

            cmbLayoutCampoDinamicoOperacao.SelectedValue = _campanha.IdLayoutCampoDinamico.ToString();
            cmbLayoutCampoDinamicoBko.SelectedValue = _campanha.IdLayoutCampoDinamicoBko.ToString();

            cmbMailingIndicacao.SelectedValue = _campanha.IdMailingCadastroManual.ToString();
            txtEnderecoInputMailing.Text = _campanha.EnderecoDeImportacaoDoMailing;

            cmbStatusAutomaticoAceite.SelectedValue = _campanha.IdStatusTabulacaoAutomaticaVenda.ToString();
            cmbStatusAutomatico.SelectedValue = _campanha.IdStatusTabulacaoAutomatica.ToString();

            chkAtivo.Checked = _campanha.Ativo;
            chkDiscagemManual.Checked = _campanha.HabilitaDiscagemManual;
            chkCadastroManual.Checked = Convert.ToBoolean(_campanha.HabilitaCadastroManual);
            chkHistorico.Checked = _campanha.HabilitaHistorico;
            chkIndicacao.Checked = _campanha.HabilitaIndicacao;
            chkSimulador.Checked = _campanha.HabilitaComparadorDePlanos;
            chkHabilitaPesquisa.Checked = _campanha.HabilitaPesquisa;
            chkCepExpress.Checked = _campanha.HabilitaCepExpress;
            chkHabilitaRevenda.Checked = _campanha.HabilitaRevenda;
            chkIdTipoDeAuditoria.Checked = _campanha.IdTipoDeAuditoria == 1 ? true : false;
            txtObservacao.Text = _campanha.Observacao;
        }

        private void TravarScrollMouse()
        {
            cmbDiscador.MouseWheel += cmbTrava_MouseWheel;
            cmbTipoDeDiscagem.MouseWheel += cmbTrava_MouseWheel;
            cmbScriptApresentacao.MouseWheel += cmbTrava_MouseWheel;
            cmbScriptFinalizacao.MouseWheel += cmbTrava_MouseWheel;
            cmbLayoutCampoDinamicoOperacao.MouseWheel += cmbTrava_MouseWheel;
            cmbLayoutCampoDinamicoBko.MouseWheel += cmbTrava_MouseWheel;
            cmbMailingIndicacao.MouseWheel += cmbTrava_MouseWheel;
            txtEnderecoInputMailing.MouseWheel += cmbTrava_MouseWheel;
            cmbStatusAutomaticoAceite.MouseWheel += cmbTrava_MouseWheel;
            cmbStatusAutomatico.MouseWheel += cmbTrava_MouseWheel;
        }

        private void CarregarDiscadores()
        {
            IEnumerable<Discador> _discador = _discadorService.Listar(-1, true);
            cmbDiscador.PreencherComSelecione(_discador, x => x.Id, x => x.Nome);
        }

        private void CarregarLayoutDinamico()
        {
            IEnumerable<Tabulador.Dominio.Entidades.LayoutDinamico.LayoutDeCampoDinamico> _layout = _layoutDinamicoService.ListarLayout(-1, true);
            cmbLayoutCampoDinamicoOperacao.PreencherComSelecione(_layout, x => x.Id, x => x.Nome);
            cmbLayoutCampoDinamicoBko.PreencherComSelecione(_layout, x => x.Id, x => x.Nome);
        }
        private void CarregarScriptDeAtendimento()
        {
            IEnumerable<Tabulador.Dominio.Entidades.ScriptAtendimento.ScriptDeAtendimento> _status = _scriptDeAtendimentoService.Listar(-1, true);
            cmbScriptApresentacao.PreencherComSelecione(_status.Where(x => x.Ativo == true), x => x.Id, x => x.Nome);
            cmbScriptFinalizacao.PreencherComSelecione(_status.Where(x => x.Ativo == true), x => x.Id, x => x.Nome);
        }

        private void CarregarTipoDeDiscagem()
        {
            IEnumerable<TipoDiscagem> _tipodiscagem = _tipoDiscagemService.ListarTipoDeDiscagem(-1, true);
            cmbTipoDeDiscagem.PreencherComSelecione(_tipodiscagem, x => x.Id, x => x.Nome);
        }

        private void CarregarStatusDeAtendimento()
        {
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> _statustabulacaoautomatica = _statusDeAtendimentoService.ListarTabulacaoAutomatica(true);
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> _status = _statusDeAtendimentoService.Listar(id: null, ativo: true);

            cmbStatusAutomatico.PreencherComSelecione(_statustabulacaoautomatica, x => x.Id, x => x.Nome);
            cmbStatusAutomaticoAceite.PreencherComSelecione(_statustabulacaoautomatica, x => x.Id, x => x.Nome);

            IEnumerable<CampanhaDoStatusDeAtendimento> _campanhasDoStatusDeAtendimento;

            if (_campanha != null)
                _campanhasDoStatusDeAtendimento = _statusDeAtendimentoService.ListarCampanhaDoStatus(_campanha.Id, true);
            else
                _campanhasDoStatusDeAtendimento = _statusDeAtendimentoService.ListarCampanhaDoStatus(0, true);

            clbStatusDeAtendimento.Items.Clear();

            if (_status != null)
            {
                foreach (var item in _status)
                {
                    clbStatusDeAtendimento.Items.Add(item.Id + " - " + item.Nome, _campanhasDoStatusDeAtendimento.Where(x => x.IdStatusDeAtendimento == item.Id).Any());
                }
            }
        }

        private void CarregarBancos()
        {
            _banco = _campanhaService.ListarBanco(-1, true);
            IEnumerable<Banco> _bancoDaCampanha;

            if (_campanha != null)
                _bancoDaCampanha = _campanhaService.ListarBancoDaCampanha(_campanha.Id, true);
            else
                _bancoDaCampanha = _campanhaService.ListarBancoDaCampanha(0, true);

            clbBanco.Items.Clear();

            if (_banco != null)
            {
                foreach (var item in _banco)
                {
                    clbBanco.Items.Add(item.Id + " - " + item.Nome, _bancoDaCampanha.Where(x => x.Id == item.Id).Any());
                }
            }

            //bancos = clbBanco.Items.OfType<string>().ToArray();
        }

        private void CarregarStatusDeOferta()
        {
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAcordo> _statusDeOferta = _statusDeOfertaService.Listar(-1, true);
            IEnumerable<StatusDeOfertaDaCampanha> _campanhasDoStatusDeOferta;

            if (_campanha != null)
                _campanhasDoStatusDeOferta = _statusDeOfertaService.ListarStatusDaOfertaDaCampanha(_campanha.Id, true);
            else
                _campanhasDoStatusDeOferta = _statusDeOfertaService.ListarStatusDaOfertaDaCampanha(0, true);

            clbStatusDeOferta.Items.Clear();

            if (_statusDeOferta != null)
            {
                foreach (var item in _statusDeOferta)
                {
                    clbStatusDeOferta.Items.Add(item.Id + " - " + item.Nome, _campanhasDoStatusDeOferta.Where(x => x.IdStatusDeOferta == item.Id).Any());
                }
            }
        }

        private void CarregarStatusDeAuditoria()
        {
            IEnumerable<StatusDeAuditoria> _statusDeAuditoria = _statusDeAuditoriaService.Listar(-1, ativo: true);

            var _status = _statusDeAuditoria.Where(x => x.Id != -1).ToList();
            IEnumerable<CampanhaDoStatusDeAuditoria> _campanhasDoStatusDeOferta;

            if (_campanha != null)
                _campanhasDoStatusDeOferta = _statusDeAuditoriaService.ListarStatusDeAuditoriaDaCampanha(_campanha.Id, true);
            else
                _campanhasDoStatusDeOferta = _statusDeAuditoriaService.ListarStatusDeAuditoriaDaCampanha(0, true);

            clbStatusDeAuditoria.Items.Clear();

            if (_status != null)
            {
                foreach (var item in _status)
                {
                    clbStatusDeAuditoria.Items.Add(item.Id + " - " + item.Nome, _campanhasDoStatusDeOferta.Where(x => x.IdStatusDeAuditoria == item.Id).Any());
                }
            }
        }

        private void CarregarFormaDePagamento()
        {
            IEnumerable<FormaDePagamento> _formaDePagamento = _campanhaService.ListarFormasDePagamento(-1, true);
            IEnumerable<FormaDePagamento> _formaDePagamentoDaCampanha;

            if (_campanha != null)
                _formaDePagamentoDaCampanha = _campanhaService.ListarFormasDePagamentoDaCampanha(_campanha.Id, true);
            else
                _formaDePagamentoDaCampanha = _campanhaService.ListarFormasDePagamentoDaCampanha(0, true);

            if (_formaDePagamento != null)
            {
                foreach (var item in _formaDePagamento)
                {
                    clbFormaPagamento.Items.Add(item.Id + " - " + item.Nome, _formaDePagamentoDaCampanha.Where(x => x.Id == item.Id).Any());
                }
            }
        }

        private void GravarCampanha()
        {
            var resultado = MessageBox.Show("Deseja salvar alterações na campanha?", "Alerta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (resultado == DialogResult.No) return;

            edicao = true;

            if (_campanha == null)
            {
                edicao = false;

                if (!PodeCriarDiretorio())
                {
                    MessageBox.Show("Já existe um diretório criado com o seguinte nome: \n"
                        + txtNome.Text.Trim().Replace(" ", "_"), "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _campanha = new Tabulador.Dominio.Entidades.Campanha();

                _campanha.IdCriador = AdministracaoMDI._usuario.Id;
            }

            _campanha.IdModificador = AdministracaoMDI._usuario.Id;

            _campanha.Nome = txtNome.Text.Trim().ToUpper();

            int idDiscador = 0;
            if (int.TryParse(cmbDiscador.SelectedValue.ToString(), out idDiscador))
            {
                _campanha.IdDiscador = idDiscador;
            }

            int idTipoDaCampanha = 0;
            if (int.TryParse(cmbTipoCampanha.SelectedValue.ToString(), out idTipoDaCampanha))
            {
                _campanha.idTipoDaCampanha = idTipoDaCampanha;
            }

            int idTipoDiscagem = 0;
            if (int.TryParse(cmbTipoDeDiscagem.SelectedValue.ToString(), out idTipoDiscagem))
            {
                _campanha.IdTipoDeDiscagem = idTipoDiscagem;
            }

            int afterCall = 0;
            if (int.TryParse(txtAfterCall.Text, out afterCall))
            {
                _campanha.AfterCall = afterCall;
            }

            int metaDeVenda = 0;
            if (int.TryParse(txtMetaDeVenda.Text, out metaDeVenda))
            {
                _campanha.MetaVenda = metaDeVenda;
            }

            int idScriptApresentacao = 0;
            if (int.TryParse(cmbScriptApresentacao.SelectedValue.ToString(), out idScriptApresentacao))
            {
                _campanha.IdScriptApresentacao = idScriptApresentacao;
            }

            int idScriptFinalizacao = 0;
            if (int.TryParse(cmbScriptFinalizacao.SelectedValue.ToString(), out idScriptFinalizacao))
            {
                _campanha.IdScriptFinalizacao = idScriptFinalizacao;
            }

            int idLayoutCampoDinamico = 0;
            if (int.TryParse(cmbLayoutCampoDinamicoOperacao.SelectedValue.ToString(), out idLayoutCampoDinamico))
            {
                _campanha.IdLayoutCampoDinamico = idLayoutCampoDinamico;
            }

            int idLayoutCampoDinamicoBko = 0;
            if (int.TryParse(cmbLayoutCampoDinamicoBko.SelectedValue.ToString(), out idLayoutCampoDinamicoBko))
            {
                _campanha.IdLayoutCampoDinamicoBko = idLayoutCampoDinamicoBko;
            }

            int idMailingIndicacao = 0;
            if (int.TryParse(cmbMailingIndicacao.SelectedValue.ToString(), out idMailingIndicacao))
            {
                _campanha.IdMailingCadastroManual = idMailingIndicacao;
            }

            if (_campanha.EnderecoDeImportacaoDoMailing == null)
            {
                _campanha.EnderecoDeImportacaoDoMailing =  $@"\\{_campanha.EnderecoDeImportacaoDoMailing} + {_campanha.Nome}\";
                    //@"\\10.0.1.106\MAILING\IMPORTAR\" + _campanha.Nome + @"\";
            }
            else
            {
                _campanha.EnderecoDeImportacaoDoMailing = txtEnderecoInputMailing.Text;
            }


            //TODO - COMENTEI TABULAÇÃO AUTOMÁRICA, GERANDO ERRO - Verificar com Rafa Depois, Rei Almeida
            int idStatusTabulacaoAutomaticaAceite = 0;
            if (int.TryParse(cmbStatusAutomaticoAceite.SelectedValue.ToString(), out idStatusTabulacaoAutomaticaAceite))
            {
                _campanha.IdStatusTabulacaoAutomaticaVenda = idStatusTabulacaoAutomaticaAceite;
            }

            int idStatusTabulacaoAutomatica = 0;
            if (int.TryParse(cmbStatusAutomatico.SelectedValue.ToString(), out idStatusTabulacaoAutomatica))
            {
                _campanha.IdStatusTabulacaoAutomatica = idStatusTabulacaoAutomatica;
            }

            _campanha.Ativo = chkAtivo.Checked;
            _campanha.HabilitaDiscagemManual = chkDiscagemManual.Checked;
            _campanha.HabilitarContatoManual = chkDiscagemManual.Checked;
            _campanha.HabilitaCadastroManual = chkCadastroManual.Checked;
            _campanha.HabilitaHistorico = chkHistorico.Checked;
            _campanha.HabilitaIndicacao = chkIndicacao.Checked;
            _campanha.HabilitaComparadorDePlanos = chkSimulador.Checked;
            _campanha.HabilitaPesquisa = chkHabilitaPesquisa.Checked;
            _campanha.HabilitaCepExpress = chkCepExpress.Checked;
            _campanha.HabilitaRevenda = chkHabilitaRevenda.Checked;
            _campanha.IdTipoDeAuditoria = chkIdTipoDeAuditoria.Checked == true ? 1 : 2;
            _campanha.Observacao = txtObservacao.Text;

            _campanha.idBancosDaCampanha = RetornarBancos();
            _campanha.idFormasDePagamento = RetornarFormasDePagaento();
            _campanha.idStatusDeAtendimento = RetornarStatusDeAtendimento();
            _campanha.idStatusDeOferta = RetornarStatusDeOferta();
            _campanha.idStatusDeAuditoria = RetornarStatusDeAuditoria();

            _campanha.Id = _campanhaService.Gravar(_campanha, espelho, _idCampanha);

            VerificarSeCriaNovoMailingIndicacao();

            MessageBox.Show($"Campanha {(edicao == true ? "atualizada" : "criada")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();

            Atualizar = true;
        }

        private void GravarCampanhaEspelhada()
        {
            if (_campanha == null)
            {
                espelho = true;

                _campanha = new Tabulador.Dominio.Entidades.Campanha();
                _campanha.IdCriador = AdministracaoMDI._usuario.Id;
            }

            _campanha.IdModificador = AdministracaoMDI._usuario.Id;

            _campanha.idTipoDaCampanha = _campanhaBase.idTipoDaCampanha;

            if (_campanha.EnderecoDeImportacaoDoMailing == null)
            {
                _campanha.EnderecoDeImportacaoDoMailing = @"\\10.0.1.134\MAILING\IMPORTAR\" + _novaCampanha.EnderecoDeImportacaoDoMailing.ToString() + @"\";
            }

            _campanha.Nome = _novaCampanha.Nome.ToString().ToUpper();

            int idDiscador = 0;
            if (int.TryParse(_campanhaBase.IdDiscador.ToString(), out idDiscador))
            {
                _campanha.IdDiscador = idDiscador;
            }

            int idTipoDiscagem = 0;
            if (int.TryParse(_campanhaBase.IdTipoDeDiscagem.ToString(), out idTipoDiscagem))
            {
                _campanha.IdTipoDeDiscagem = idTipoDiscagem;
            }

            int afterCall = 0;
            if (int.TryParse(_campanhaBase.AfterCall.ToString(), out afterCall))
            {
                _campanha.AfterCall = afterCall;
            }

            int metaDeVenda = 0;
            if (int.TryParse(_campanhaBase.MetaVenda.ToString(), out metaDeVenda))
            {
                _campanha.MetaVenda = metaDeVenda;
            }

            int idScriptApresentacao = 0;
            if (int.TryParse(_campanhaBase.IdScriptApresentacao.ToString(), out idScriptApresentacao))
            {
                _campanha.IdScriptApresentacao = idScriptApresentacao;
            }

            int idScriptFinalizacao = 0;
            if (int.TryParse(_campanhaBase.IdScriptFinalizacao.ToString(), out idScriptFinalizacao))
            {
                _campanha.IdScriptFinalizacao = idScriptFinalizacao;
            }

            int idLayoutCampoDinamico = 0;
            if (int.TryParse(_campanhaBase.IdLayoutCampoDinamico.ToString(), out idLayoutCampoDinamico))
            {
                _campanha.IdLayoutCampoDinamico = idLayoutCampoDinamico;
            }

            int idLayoutCampoDinamicoBko = 0;
            if (int.TryParse(_campanhaBase.IdLayoutCampoDinamicoBko.ToString(), out idLayoutCampoDinamicoBko))
            {
                _campanha.IdLayoutCampoDinamicoBko = idLayoutCampoDinamicoBko;
            }

            int idMailingIndicacao = 0;
            if (int.TryParse(_campanhaBase.IdMailingCadastroManual.ToString(), out idMailingIndicacao))//verificar a questao do mailing manual
            {
                _campanha.IdMailingCadastroManual = idMailingIndicacao;
            }

            int idStatusTabulacaoAutomaticaAceite = 0;
            if (int.TryParse(_campanhaBase.IdStatusTabulacaoAutomaticaVenda.ToString(), out idStatusTabulacaoAutomaticaAceite))
            {
                _campanha.IdStatusTabulacaoAutomaticaVenda = idStatusTabulacaoAutomaticaAceite;
            }

            int idStatusTabulacaoAutomatica = 0;
            if (int.TryParse(_campanhaBase.IdStatusTabulacaoAutomatica.ToString(), out idStatusTabulacaoAutomatica))
            {
                _campanha.IdStatusTabulacaoAutomatica = idStatusTabulacaoAutomatica;
            }

            _campanha.HabilitaDiscagemManual = _campanhaBase.HabilitaDiscagemManual;
            _campanha.HabilitarContatoManual = _campanhaBase.HabilitarContatoManual;
            _campanha.HabilitaCadastroManual = _campanhaBase.HabilitaCadastroManual;
            _campanha.HabilitaHistorico = _campanhaBase.HabilitaHistorico;
            _campanha.HabilitaIndicacao = _campanhaBase.HabilitaIndicacao;
            _campanha.HabilitaComparadorDePlanos = _campanhaBase.HabilitaComparadorDePlanos;
            _campanha.HabilitaPesquisa = _campanhaBase.HabilitaPesquisa;

            if (_campanhaBase.idBancosDaCampanha != null)
                _campanha.idBancosDaCampanha = _campanhaBase.idBancosDaCampanha;

            if (_campanhaBase.idFormasDePagamento != null)
                _campanha.idFormasDePagamento = _campanhaBase.idFormasDePagamento;

            if (_campanhaBase.idStatusDeAtendimento != null)
                _campanha.idStatusDeAtendimento = _campanhaBase.idStatusDeAtendimento;

            if (_campanhaBase.idStatusDeOferta != null)
                _campanha.idStatusDeOferta = _campanhaBase.idStatusDeOferta;

            if (_campanhaBase.idStatusDeAuditoria != null)
                _campanha.idStatusDeAuditoria = _campanhaBase.idStatusDeAuditoria;

            _campanha.Aparelhos = _novaCampanha.Aparelhos;
            _campanha.VariaveisDoScript = _novaCampanha.VariaveisDoScript;
            _campanha.CheckListVenda = _novaCampanha.CheckListVenda;
            _campanha.PlanosComparacao = _novaCampanha.PlanosComparacao;
            _campanha.FormularioQualidade = _novaCampanha.FormularioQualidade;
            _campanha.FaqAtendimento = _novaCampanha.FaqAtendimento;

            _campanha.Observacao = "Mailing de Indicação " + _novaCampanha.Nome.ToString().ToUpper();
            _campanha.Ativo = _novaCampanha.Ativo; //se alguns parametros forem inválidos criar ela inativa //_campanha.Ativo = _parametroValido;

            _campanha.Id = _campanhaService.Gravar(_campanha, espelho, _idCampanha);

            CriarNovoMailingIndicacao();

            MessageBox.Show($"Campanha espelhada com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!espelho)
            {
                Close();
                Atualizar = true;
            }
        }

        //IDS USADOS AO SALVAR UMA NOVA CAMPANHA
        private string RetornarBancos()
        {
            string ids = "";
            foreach (var item in clbBanco.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        private string RetornarFormasDePagaento()
        {
            string ids = "";
            foreach (var item in clbFormaPagamento.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
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

        private string RetornarStatusDeOferta()
        {
            string ids = "";
            foreach (var item in clbStatusDeOferta.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        private string RetornarStatusDeAuditoria()
        {
            string ids = "";
            foreach (var item in clbStatusDeAuditoria.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        private void VerificarSeCriaNovoMailingIndicacao()
        {
            if (cmbMailingIndicacao.TextoEhSelecione() && chkCadastroManual.Checked)
                CriarNovoMailingIndicacao();
        }

        private void CriarNovoMailingIndicacao()
        {
            if (!espelho)
            {
                var resultado = MessageBox.Show("Deseja criar um novo mailing indicação para esta campanha?",
                    "Alerta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultado == DialogResult.No) return;
            }

            _mailing = new Tabulador.Dominio.Entidades.Mailing();

            _mailing.Ativo = true;
            _mailing.Indicacao = true;
            _mailing.IdStatusProcessamento = 3;
            _mailing.IdCriador = AdministracaoMDI._usuario.Id;

            _mailing.IdCampanha = _campanha.Id;

            if (!espelho)
            {
                _mailing.Nome = "MAILING_INDICACAO_" + txtNome.Text.Trim().Replace(" ", "_");
                _mailing.Observacao = "MAILING DE INDICACAO DA CAMPANHA " + txtNome.Text.ToUpper();
                _mailing.NomeArquivo = _campanha.EnderecoDeImportacaoDoMailing + "MALING_DA_CAMPANHA_" + _campanha.Nome.ToUpper().Replace(" ", "_") + ".csv";
            }
            else
            {
                _mailing.Nome = "MAILING_INDICACAO_" + _novaCampanha.Nome.ToString().ToUpper().Replace(" ", "_");
                _mailing.Observacao = "MAILING DE INDICACAO DA CAMPANHA " + _novaCampanha.Nome.ToString().ToUpper().Replace(" ", "_");
                _mailing.NomeArquivo = _campanha.EnderecoDeImportacaoDoMailing + "CADASTRO_MANUAL_CAMPANHA_" + _campanha.Nome.ToUpper().Replace(" ", "_") + ".csv";
            }

            _mailing.Id = _mailingService.Gravar(_mailing);

            var idCampanha = _campanha.Id;

            _campanhaService.AtualizarDadosDeCadastroManual(idCampanha, _mailing.Id);

            if (!espelho)
            {
                CarregarMailingsIndicacao();
                cmbMailingIndicacao.SelectedValue = _mailing.Id.ToString();
            }
        }

        private void ColetarDadosCampanhaEspelho()
        {
            _campanhaBase = _campanhaService.RetornarInformacoesDaCampanha(_idCampanha);
        }

        private void CarregarMailingsIndicacao()
        {
            IEnumerable<Tabulador.Dominio.Entidades.Mailing> _mailings = _mailingService.Listar(-1, true).Where(x => x.Indicacao == true && x.IdStatusProcessamento != 4);
            cmbMailingIndicacao.PreencherComSelecione(_mailings, x => x.Id, x => x.Nome);
        }

        private void CarregarTipoCampanha()
        {
            IEnumerable<Tabulador.Dominio.Entidades.Campanha> tiposCampanhas = _campanhaService.ListarTipoDaCampanha(ativo: true);
            cmbTipoCampanha.PreencherComSelecione(tiposCampanhas, x => x.Id, x => x.Nome);
        }

        private bool PodeSalvar()
        {
            List<string> mensagens = new List<string>();

            bool cadastroManual = chkCadastroManual.Checked && cmbMailingIndicacao.TextoEhSelecione() && _campanha != null;
            var formatoCorreto = new Regex("^[a-zA-Z0-9\\s_-]*$").IsMatch(txtNome.Text); //{2,20} permite espaços no nome

            if (string.IsNullOrEmpty(txtNome.Text))
                mensagens.Add("[Nome] deve ser preenchido");

            if (formatoCorreto == false)
                mensagens.Add("[Nome] não pode conter espaços ou caracteres especiais!");

            if (!string.IsNullOrEmpty(txtNome.Text))
            {
                edicao = true;

                if (_campanha == null)
                    edicao = false;

                if (!edicao)
                    if (!PodeCriarNomeCampanha())
                        mensagens.Add("[Nome] Já existe uma Campanha com esta nomenclatura!");
            }

            if (cmbDiscador.TextoEhSelecione())
                mensagens.Add("[Discador] deve ser selecionado");

            if (cmbTipoDeDiscagem.TextoEhSelecione())
                mensagens.Add("[Tipo de Discagem] deve ser selecionado");

            if (cmbTipoCampanha.TextoEhSelecione())
                mensagens.Add("[Tipo de Campanha] deve ser selecionado");

            if (string.IsNullOrEmpty(txtAfterCall.Text))
                mensagens.Add("[After Call] deve ser preenchido");

            int afterCall = 0;
            if (!int.TryParse(txtAfterCall.Text, out afterCall))
                mensagens.Add("[After Call] deve ser numerico");

            if (string.IsNullOrEmpty(txtMetaDeVenda.Text))
                mensagens.Add("[Meta de Venda] deve ser preenchido");

            int metaDeVenda = 0;
            if (!int.TryParse(txtMetaDeVenda.Text, out metaDeVenda))
                mensagens.Add("[Meta de Venda] deve ser numérico");

            if (cmbScriptApresentacao.TextoEhSelecione())
                mensagens.Add("[Script de Apresentação] deve ser selecionado");

            if (cmbScriptFinalizacao.TextoEhSelecione())
                mensagens.Add("[Script de Apresentação] deve ser selecionado");

            if (cmbLayoutCampoDinamicoOperacao.TextoEhSelecione())
                mensagens.Add("[Campo Dinâmico Operação] deve ser selecionado");

            if (cmbLayoutCampoDinamicoBko.TextoEhSelecione())
                mensagens.Add("[Campo Dinâmico Back-Office] deve ser selecionado");

            if (cmbStatusAutomaticoAceite.TextoEhSelecione())
                mensagens.Add("[Tabulação Automática com Aceite] deve ser selecionado");

            if (cmbStatusAutomatico.TextoEhSelecione())
                mensagens.Add("[Tabulação Automática] deve ser selecionado");

            //if (cadastroManual)
            //    mensagens.Add("[Mailing Indicacao] deve ser selecionado");

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any();
        }

        private bool PodeCriarNomeCampanha()
        {
            var mensagens = new List<string>();
            mensagens = VerificarSePodeCriarNomeCampanha(txtNome.Text.Trim());

            //CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool PodeCriarDiretorio()
        {
            var mensagens = new List<string>();
            mensagens = VerificarSePodeCriarDiretorio(txtNome.Text.Replace(" ", "_"));

            return mensagens.Any() == false;
        }

        private List<string> VerificarSePodeCriarDiretorio(string diretorio)
        {
            var mensagens = _verificacaoService.VerificarSePodeCriarDiretorio(diretorio);
            return mensagens;
        }

        private List<string> VerificarSePodeCriarNomeCampanha(string nomeCampanha)
        {
            var mensagens = _verificacaoService.VerificarSePodeCriarNomeCampanha(nomeCampanha);
            return mensagens;
        }

        private void CarregarStatusDeAtendimento(int? idCampanha, int? idTipoDeCampanha)
        {
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> statusDeAtendiemento =
                _statusDeAtendimentoService.ListarPorTipoCampanha(id: null, idCampanha: idCampanha, idTipoStatus: null, idTipoDeCampanha: idTipoDeCampanha);
            clbStatusDeAtendimento.Preencher(statusDeAtendiemento, x => x.Id, x => x.Nome);
        }

        #endregion METODOS

        #region EVENTOS

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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (PodeSalvar()) return;
                GravarCampanha();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível salvar a campanha!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTrava_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtAfterCall_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtMetaDeVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void lnkTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tcCampanha.SelectedTab.Equals(tcCampanha_tpBancos))
                clbBanco.SetarTodosRegistros(check: true);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_tpFormaPagamento))
                clbFormaPagamento.SetarTodosRegistros(check: true);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_StatusAtendimento))
                clbStatusDeAtendimento.SetarTodosRegistros(check: true);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_tpStatusOferta))
                clbStatusDeOferta.SetarTodosRegistros(check: true);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_tpStatusAuditoria))
                clbStatusDeAuditoria.SetarTodosRegistros(check: true);
        }

        private void lnkNenhum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tcCampanha.SelectedTab.Equals(tcCampanha_tpBancos))
                clbBanco.SetarTodosRegistros(check: false);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_tpFormaPagamento))
                clbFormaPagamento.SetarTodosRegistros(check: false);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_StatusAtendimento))
                clbStatusDeAtendimento.SetarTodosRegistros(check: false);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_tpStatusOferta))
                clbStatusDeOferta.SetarTodosRegistros(check: false);
            else if (tcCampanha.SelectedTab.Equals(tcCampanha_tpStatusAuditoria))
                clbStatusDeAuditoria.SetarTodosRegistros(check: false);
        }

        private void cmbTipoCampanha_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //int.TryParse(cmbTipoCampanha.SelectedValue?.ToString(), out int idTipoDaCampanha);

            //CarregarStatusDeAtendimento(_idCampanha, idTipoDaCampanha);
        }

        //pesquisa dinamica no CheckedListBox atraves de um textBox txtPesquisa.Text
        private void clbBanco_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (clbBanco.SelectedIndex >= 0)
            //{
            //    for (int i = 0; i < clbBanco.Items.Count; i++)
            //    {
            //        if (clbBanco.CheckedItems.Count > 0)
            //            bancosSelecionados = clbBanco.CheckedItems.OfType<string>().ToArray();
            //        else
            //            bancosSelecionados = null;
            //    }
            //}
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            //List<string> filtered = bancos.AsEnumerable().Where(x => x.ToUpper().Contains(txtPesquisa.Text.ToUpper())).ToList();

            //string filter_param = txtPesquisa.Text.ToUpper();
            //var item = clbBanco.Items.Cast<string>().ToList();
            //List<string> filteredItems = item.FindAll(x => x.StartsWith(filter_param));

            //clbBanco.DataSource = filteredItems;

            //clbBanco.DataSource = filtered;

            //if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
            //{
            //    clbBanco.DataSource = bancos;
            //}
        }

        private void lklLimpar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //bancosSelecionados = clbBanco.CheckedItems.OfType<string>().ToArray();
            //txtPesquisa.Text = string.Empty;
            ////clbBanco.DataSource = bancos;


            //if (clbBanco.CheckedItems.Count > 0)
            //{
            //    foreach (var item in bancos)
            //    {
            //        clbBanco.Items.Add(item[0] + " - " + item[1], bancosSelecionados.Where(x => x[0].ToString() == item[1].ToString()).Any());
            //    }
            //}

            //if (clbBanco.CheckedItems.Count > 0)
            //{
            //    foreach (var item in bancos)
            //    {
            //        clbBanco.Items.Add(bancos, bancosSelecionados.Where(x => x[0].ToString() == item[0].ToString()).Any());
            //        //(x => x[0].ToString() == bancos[0].ToString()).Any());
            //    }
            //}
        }

        private void tcCampanha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcCampanha.SelectedTab.Equals(tcCampanha_tpConfiguracoes))
            {
                lnkTodos.Visible = false;
                lnkNenhum.Visible = false;
            }
            else
            {
                lnkTodos.Visible = true;
                lnkNenhum.Visible = true;
            }
        }

        private void chkHabilitaRevenda_Click(object sender, EventArgs e)
        {
            if (chkHabilitaRevenda.Checked)
            {
                var mensagem = _campanhaService.RetornarMensagemDeRevendaHabilitada();
                string texto = string.Empty;

                foreach (var item in mensagem)
                {
                    texto = item;
                }

                var resultado = MessageBox.Show(texto, "Alerta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                    chkHabilitaRevenda.Checked = false;
            }
        }

        #endregion EVENTOS
    }
}