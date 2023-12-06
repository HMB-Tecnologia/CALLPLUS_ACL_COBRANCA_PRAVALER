using Callplus.CRM.Tabulador.App.Controles;
using Callplus.CRM.Tabulador.App.Integracoes;
using Callplus.CRM.Tabulador.App.Properties;
using Callplus.CRM.Tabulador.App.Ranking;
using Callplus.CRM.Tabulador.App.Util;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WebSocketHMB;
using WebSocketHMB.Return;
using WebSocketHMB.Return.TabularContrato;
using Olos.SimpleSockets;
using v1Tabulare_z13.IntegracaoDiscador;
using v1Tabulare_z13.IntegracaoDiscador.OlosWebService;
using Callplus.CRM.Tabulador.App.WSAgentEvent;
using Callplus.CRM.Tabulador.App.Login;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class AtendimentoForm : Form
    {
        public AtendimentoForm(Usuario usuarioOperador, Discador discadorConectado)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _atendimentoService = new AtendimentoService();
            _planoParaComparacaoService = new PlanoPorOperadoraParaComparacaoService();
            _pesquisaService = new PesquisaService();
            _campanhaService = new CampanhaService();
            _grauDeParentescoService = new GrauDeParentescoService();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _statusDeOfertaService = new StatusDeOfertaService();
            _prospectService = new ProspectService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _produtoService = new ProdutoService();
            _discadorService = new DiscadorService();
            _campanhasDoUsuario = new List<Campanha>();
            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _faqDeAtendimentoService = new FaqDeAtendimentoService();
            _notificacaoService = new NotificacaoService();

            _ofertasDoAtendimento = new List<OfertaDoAtendimento>();
            _pilhaDeJanelas = new Stack<Form>();

            _usuario = usuarioOperador;
            _discadorConectado = discadorConectado;

            _listaStatusOperadorPadrao = new List<KeyValuePair<int, string>>();
            _listaStatusOperadorPadrao.Add(new KeyValuePair<int, string>(1000, "DISPONÍVEL"));

            _timerPausa = new System.Timers.Timer(1000);
            _timerAferCall = new System.Timers.Timer(1000);
            _timerAferCall.Enabled = false;
            _timerPausa.Enabled = false;

            _timerPausa.Elapsed += timerDuracaoPausa_Tick;
            _timerAferCall.Elapsed += timerAfterCall_Tick;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly AtendimentoService _atendimentoService;
        private readonly PlanoPorOperadoraParaComparacaoService _planoParaComparacaoService;
        private readonly PesquisaService _pesquisaService;
        private readonly CampanhaService _campanhaService;
        private readonly GrauDeParentescoService _grauDeParentescoService;
        private readonly Discador _discadorConectado;
        private readonly DiscadorService _discadorService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private readonly ILogger _logger;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly ProdutoService _produtoService;
        private readonly ProspectService _prospectService;
        private ResultadoDoAtendimento _resultadoDoAtendimentoAtual;
        private readonly ScriptDeAtendimentoService _scriptDeAtendimentoService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private readonly StatusDeOfertaService _statusDeOfertaService;
        private readonly FaqDeAtendimentoService _faqDeAtendimentoService;
        private readonly NotificacaoService _notificacaoService;        
        private readonly Usuario _usuario;
        private Atendimento _atendimentoEmAndamento;
        private Campanha _campanhaAtual;
        private IEnumerable<Campanha> _campanhasDoUsuario;
        private IEnumerable<GrauDeParentesco> _grauDeParentesco;
        private List<OfertaDoAtendimento> _ofertasDoAtendimento;
        private OfertaDoAtendimento _ofertaAtualDoAtendimento;
        private bool _exibindoScript;
        private Prospect _prospectDoAtendimento;
        private StatusDeOferta _statusDeOfertaSelecionado;
        private StatusDeAtendimento _statusDoAtendimentoEmAndamento;
        private System.Timers.Timer _timerPausa;
        private System.Timers.Timer _timerAferCall;

        private TimeSpan _timespanAfterCall;
        private TimeSpan _timespanDuracaoPausa;
        private bool _contagemProgressivaPausa;
        private XCallIntegratorAkiva _xCallAkiva;
        private ListBoxMessage.MessageListBox listbox;
        private List<KeyValuePair<int, string>> _listaStatusOperadorPadrao;
        private Stack<Form> _pilhaDeJanelas;
        private bool _afterCallEncerrado;
        private bool _emAfterCall;

        private event EventHandler FimDoAfterCall;

        private static Integration _integrationCca;
        private bool _ccaLocked;
        private bool _ccaEmPausa;
        private bool _ccaTabulacaoEnviada;
        private string _ccaRamal;
        List<KeyValuePair<int, string>> _ccaListaPausas = new List<KeyValuePair<int, string>>();
        delegate void SetTextCallback(string text);

        SimpleClientSocket client = new SimpleClientSocket();

        public delegate void TCPTerminal_MessageReceivedDel(SimpleClientSocket simpleClientSocket, byte[] bytesReceived, int bytesLength);
        public delegate void TCPTerminal_ConnectDel(SimpleClientSocket simpleClientSocket);
        public delegate void TCPTerminal_DisconnectDel(SimpleClientSocket socket);

        private long _idProspectEmBufferParaCarregamento;
        private long _telefoneEmBufferParaCarregamento;
        private string _ticketEmBufferParaCarregamento;

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        private string sIDGravacaoDiscador = "";

        #endregion PROPRIEDADES

        #region OLOS_WEBSERVICE

        private OlosWsAgentControl _olosWsAgentControl = new OlosWsAgentControl();
        private int _agentIdOlos;

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            RetornarTimeDecorridoPausa();
        }

        int _tempoEmSegundoTotal = 0, segundos = 0, minutos = 0, hora = 0;

        private void RetornarTimeDecorridoPausa()
        {
            _tempoEmSegundoTotal++;

            segundos++;

            if (segundos % 60 == 0)
            {
                segundos = 0;
                minutos++;
            }

            if (minutos == 60)
            {
                hora++;
                minutos = 0;
            }

            var pausa = cmbStatusOperador.SelectedValue.ToString();

            lblTempoDePausa.Text = string.Format("{0:#,0#}:{1:#,0#}:{2:#,0#}", hora, minutos, segundos);
        }

        private void AcessarOlosWebService(string login, string password)
        {
            try
            {
                _agentIdOlos = LoginForm.AgenteIdOlos;

                if (_agentIdOlos > 0)
                {
                    Olos_MonitorarAgente(_agentIdOlos);
                    Olos_CarregarPausas(_agentIdOlos);
                }

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                MessageBox.Show("Ocorreu um erro inesperado ao tentar acessar o discador OLOS", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void Olos_CarregarPausas(int agentIdOlos)
        {
            var listaOlos = _olosWsAgentControl.ListarPausas(agentIdOlos);

            var listaPausa = listaOlos
                ?.Select(x => new KeyValuePair<string, string>(x.ReasonId.ToString(), x.Description?.ToUpper()))
                ?.ToList();

            PreencherComboBoxStatusDoOperador(listaPausa);
        }

        private void PreencherComboBoxStatusDoOperador(List<KeyValuePair<string, string>> status)
        {

            _listaStatusDoOperador.AddRange(status);

            if (cmbStatusOperador.InvokeRequired)
            {
                cmbStatusOperador.BeginInvoke(new MethodInvoker(() =>
                {
                    cmbStatusOperador.DataSource = _listaStatusDoOperador;
                    cmbStatusOperador.DisplayMember = "Value";
                    cmbStatusOperador.ValueMember = "Key";
                }
                ));
            }
            else
            {
                cmbStatusOperador.DataSource = _listaStatusDoOperador;
                cmbStatusOperador.DisplayMember = "Value";
                cmbStatusOperador.ValueMember = "Key";
            }

        }

        private void ConfigurarEventosOlos()
        {
            _olosWsAgentControl.ScreenPopEvent += Olos_OnScreenPop;
            _olosWsAgentControl.AgentStatusEvent += Olos_OnAgentStatus;
            _olosWsAgentControl.WrapEvent += Olos_OnWrapEvent;
            _olosWsAgentControl.WrapWithEndingEvent += Olos_WrapWithEnding;
            _olosWsAgentControl.WrapWithPauseEvent += Olos_OnWrapWithPause;
            _olosWsAgentControl.WrapWithPrivateCallbackEvent += Olos_OnWrapPrivateCallback;
            _olosWsAgentControl.WrapWithManualCallEvent += Olos_OnWrapManualCall;
            _olosWsAgentControl.HangUpEvent += Olos_OnHangUp;
            _olosWsAgentControl.ManualCallRequestFailEvent += Olos_OnManualCallRequestFail;
            _olosWsAgentControl.ManualCallStateEvent += Olos_OnManualCallStateEvent;
        }

        private void Olos_OnHangUp()
        {
            Invoke((MethodInvoker)delegate { IniciarContagemTabulacao(); });
            Invoke((MethodInvoker)delegate { HabilitarBotoesPosLigacao(); });
        }

        private void Olos_OnManualCallStateEvent(ObjManualCallState statusLigacao)
        {
            switch (statusLigacao.CallState)
            {
                //case EnumCallControlStatusId.AgentConnected:
                //case EnumCallControlStatusId.Alerting:
                //case EnumCallControlStatusId.Consulting:
                case EnumCallControlStatusId.CustomerConnected:
                    {
                        sIDGravacaoDiscador = statusLigacao.CallId.ToString();
                    }
                    break;
                case EnumCallControlStatusId.Finished:
                    {
                        if (InvokeRequired)
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                pctAplicarStatusOperador_Click(null, null);
                                IniciarContagemTabulacao();
                                HabilitarBotoesPosLigacao();
                            });
                        }
                        else
                        {
                            pctAplicarStatusOperador_Click(null, null);
                            IniciarContagemTabulacao();
                            HabilitarBotoesPosLigacao();
                        }
                    }
                    break;
                //case EnumCallControlStatusId.Holding:               
                //case EnumCallControlStatusId.Queue:
                //case EnumCallControlStatusId.Routing:
                //case EnumCallControlStatusId.Started:
                //case EnumCallControlStatusId.Transfering:
                //    {
                //        HangUpEvent?.Invoke();
                //    }
                //    break;
                default:
                    break;
            }
        }

        private void Olos_OnManualCallRequestFail()
        {
            MessageBox.Show("Ocorreu um erro ao efetuar a ligação!");
            pctConectar.Enabled = true;
        }

        private void Olos_OnWrapEvent()
        {

        }

        private void Olos_OnWrapPrivateCallback()
        {

        }

        private void Olos_OnWrapManualCall()
        {

        }

        private void Olos_OnWrapWithPause()
        {

        }

        private void Olos_WrapWithEnding()
        {

        }

        private void Olos_OnAgentStatus(int reasonId, ObjAgentChangeStatus status)
        {
            var statusOperador = ObterTextoStatusOlos(status.AgentStatusId);
            AlterarLabel(lblStatusAtendimento, $"Status: {statusOperador}");
        }

        public void AlterarStatusDoOperador()
        {
            var idPausa = cmbStatusOperador.SelectedValue.ToString();
            string nomeStatus = cmbStatusOperador.Text;

            bool removerPausa = cmbStatusOperador.Text == "DISPONÍVEL";

            if (string.IsNullOrEmpty(idPausa) || cmbStatusOperador.Text == "SELECIONE...")
            {
                MessageBox.Show("Selecione uma Pausa!", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int reasonId = int.Parse(idPausa);
            _emPausa = removerPausa == false;

            if (removerPausa)
            {
                Olos_ExitPause(_agentIdOlos);
                PararContagemDePausaOlos(nomeStatus);
            }
            else
            {
                Olos_Pause(_agentIdOlos, reasonId);
                IniciarContagemDePausaOlos(nomeStatus);
            }

        }

        private void IniciarContagemDePausaOlos(string nomePausa)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    MyTimer.Start();
                    cmbCampanha.Enabled = false;

                }));
            }
            else
            {
                MyTimer.Start();
                cmbCampanha.Enabled = false;

            }
        }

        private void PararContagemDePausaOlos(string nomePausa)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    lblTempoDePausa.Text = "TEMPO DE PAUSA: --:--:--";
                    //lblTempoDePausa.ForeColor = Color.Black;
                    // lblTempoDaPausa.Text = "--:--:--";
                    segundos = 0;
                    minutos = 0;
                    hora = 0;
                    _tempoEmSegundoTotal = 0;
                    MyTimer.Stop();
                    MyTimer.Dispose();
                    //grpPausa.Text = $"Status ({nomePausa})";
                    cmbCampanha.Enabled = true;

                }));
            }
            else
            {
                lblTempoDePausa.Text = "TEMPO DE PAUSA: --:--:--";
                //lblTempoDePausa.ForeColor = Color.Black;
                // lblTempoDaPausa.Text = "--:--:--";
                MyTimer.Stop();
                MyTimer.Dispose();
                segundos = 0;
                minutos = 0;
                hora = 0;
                _tempoEmSegundoTotal = 0;
                //grpPausa.Text = $"Status ({nomePausa})";
                cmbCampanha.Enabled = true;

            }
        }

        private void IniciarContagemTabulacao()
        {
            AlterarLabel(lblStatusAtendimento, "Status: Aguardando Tabulação");

            string afterCall = "";

            if (_usuario.afterCall.ToString().Length == 2)
                afterCall = "00:" + _usuario.afterCall;
            else
                afterCall = "00:0" + _usuario.afterCall;

            AlterarLabel(lblTempoTabulacao, afterCall);

            timerTabulacao.Start();
        }

        private void HabilitarBotoesPosLigacao()
        {
            pctConectar.Enabled = true;
            pctDesconectar.Enabled = false;
            //grpPausa.Enabled = true;
        }

        private string ObterTextoStatusOlos(EnumAgentStatusId agentStatusId)
        {
            switch (agentStatusId)
            {
                case EnumAgentStatusId.Idle: return "Disponível";
                case EnumAgentStatusId.Pause: return "Em pausa";
                case EnumAgentStatusId.Talking: return "Em atendimento";
                case EnumAgentStatusId.TalkingWithPause: return "Em atendimento com solicitação de pausa";
                case EnumAgentStatusId.Wrap: return "Em tabulação";
                case EnumAgentStatusId.WrapWithPause: return "Em tabulação com pausa";
                case EnumAgentStatusId.WrapWithEnding: return "Em tabulação com pedido de deslogar";
                case EnumAgentStatusId.WrapWithPrivateCallback: return "Em tabulação com callback privado";
                case EnumAgentStatusId.WrapWithManualCall: return "Em tabulação com ligação manual";
                default: return Enum.GetName(typeof(EnumAgentStatusId), agentStatusId);
            }
        }

        private int Olos_Login(string login, string password, bool forceLogout)
        {
            return _olosWsAgentControl.Login(login, password, forceLogout);
        }

        private void Olos_Logout(int agentId)
        {
            _olosWsAgentControl.Logout(agentId);
        }

        private void Olos_Pause(int agentId, int reasonId)
        {
            _olosWsAgentControl.Pause(agentId, reasonId);
        }

        private void Olos_ExitPause(int agentId)
        {
            _olosWsAgentControl.ExitPause(agentId);
        }

        private void Pause()
        {

        }

        private void Olos_DispositionCall(int agentId, int callId, int intcallId)
        {
            _olosWsAgentControl.DispositionCall(agentId, callId, intcallId);
        }

        private void Olos_HangupRequest(int agentId, int callId)
        {
            _olosWsAgentControl.HangupRequest(agentId, callId);
        }

        private void Olos_MonitorarAgente(int agentId)
        {
            _olosWsAgentControl.StartAgentMonitoring(agentId);
        }

        private void Olos_OnScreenPop(ScreenPopData screenPopData)
        {
            _logger.Info($"Novo Contato Recebido. CallId: {screenPopData.CallId}, CustomerId:{screenPopData.CustomerId}, PhoneNumber: {screenPopData.PhoneNumber} ");

            try
            {
                int idProspect = 0;
                int idGravacaoOlos = 0;

                if (int.TryParse(screenPopData.CustomerId, out idProspect))
                {
                    long telefoneDachamada = 0;

                    long.TryParse(screenPopData.PhoneNumber, out telefoneDachamada);

                    var numeroChamadorDiscador = screenPopData.CallId.ToString();

                    IniciarAtendimento(idProspect, telefoneDachamada, numeroChamadorDiscador, OrigemDeAtendimento.Preditivo);
                    AlterarLabel(lblStatusAtendimento, "Status: Contato recebido do Discador");
                    sIDGravacaoDiscador = screenPopData.CallId.ToString();
                }
                else
                {
                    _logger.Error("Novo Contato Recebido com Prospect Invalido");
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                MessageBox.Show("Ocorreu um erro inesperado ao receber um novo contato.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion OlOS_WEBSERVICE

        #region METODOS

        public void AlterarToolStripLabel(ToolStripLabel lib, String text)
        {
            if (toolStrip1.InvokeRequired)
            {
                toolStrip1.BeginInvoke(new MethodInvoker(() =>
                {
                    lib.Text = text;
                }));
            }
            else
            {
                lib.Text = text;
            }
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            this.ShowDialog();
        }

        private void AlterarLabelIP(string ip)
        {
            tsLabelIp.Text = $"IP: {ip}";
        }

        private void AlterarLabelLogin(string login)
        {
            tsLabelLogin.Text = $"Login: {login}";
        }

        private void AlterarLabelNomeOperador(string nome)
        {
            tsLabelNomeOperador.Text = $"Operador: {nome}";
        }

        private void AlterarLabelStatusDiscador(string mensagem)
        {
            var msg = $"STATUS: {mensagem}".ToUpper();
            AlterarLabel(lblStatusAtendimento, msg);
            AlterarToolStripLabel(tsStatusDiscador, msg);
        }

        public void AlterarLabelStatusCrm(string mensagem)
        {
            var msg = $"STATUS: {mensagem}".ToUpper();
            AlterarToolStripLabel(tsLabelStatusCrm, msg);
        }

        private void AlterarLabelStatusProgramado(bool pendente)
        {
            if (InvokeRequired)
            {
                lblAcaoProgramada.BeginInvoke(new MethodInvoker(() =>
                {
                    lblAcaoProgramada.Visible = pendente;
                }));
            }
            else
            {
                lblAcaoProgramada.Visible = pendente;
            }
        }

        private void AlterarLabelNumeroDeConexoes(int numeroConexoes)
        {
            var msg = $"Conexões: {numeroConexoes}";
            AlterarToolStripLabel(tsLabelNumeroConexoes, msg);
        }

        private void AlterarLabelMensagemDiscador(string mensagem)
        {
            var msg = $"Recebido: {mensagem}";
            AlterarToolStripLabel(tsLabelRecebidoDiscador, msg);
        }

        private static void AlterarLabel(Label label, string texto)
        {
            if (label.InvokeRequired)
            {
                label.BeginInvoke(new MethodInvoker(() =>
                {
                    label.Text = texto;
                }));
            }
            else
            {
                label.Text = texto;
            }
        }

        private void AlterarNomeDiscador(string discador)
        {
            tsLabelNomeDiscador.Text = $"Discador: {discador}";
        }

        private void AplicarCampanhaSelecionadaNoCombo()
        {
            if (VerificarSePodeAlterarCampanha() == false) return;

            var idCampanhaSelecionada = int.Parse(cmbCampanha.SelectedValue.ToString());
            Campanha campanha = _campanhaService.RetornarCampanha(idCampanhaSelecionada);
            ConfigurarCampanha(campanha);
        }

        private async void AlterarStatusDoOperadorAsync()
        {
            bool sucesso = false;
            if (!VerificarSePodeAlterarStatusOperador()) return;

            int idStatusOperador = Convert.ToInt32(cmbStatusOperador.SelectedValue.ToString());
            bool removerPausa = cmbStatusOperador.Text == "DISPONÍVEL";
            bool conectarHabilitado = pctConectar.Enabled;
            string nomePausa = cmbStatusOperador.Text;

            cmbStatusOperador.Desabilitar();
            ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);

            if (_discadorConectado.TipoDiscador == TipoDiscador.Akiva)
            {
                if (removerPausa)
                {
                    int idPausaAtual = XCallAkiva_RetornarIdPausaDoAgenteAsync();
                    sucesso = true;

                    if (idPausaAtual > 0)
                    {
                        sucesso = await XCAllAkiva_RemoverPausaAsync();
                    }
                }
                else
                {
                    sucesso = await XCallAkiva_PausarAsync(idStatusOperador);
                }

                cmbStatusOperador.Habilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);

                if (sucesso == false)
                    cmbStatusOperador.ResetarComSelecione(habilitar: true);
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
            {
                if (removerPausa)
                    CCA_RemoverPausa(idStatusOperador);
                else
                    CCA_Pausar(idStatusOperador, nomePausa);
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.Olos)
            {
                //TODO:
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI)
            {
                AlterarStatusDoOperador();
            }
        }

        private void AtivarControle(Control controle)
        {
            if (controle == null) return;

            controle.Select();
            controle.Focus();
        }

        private void CarregarCampanhaPrincipalDoUsuario(int idUsuario)
        {
            var campanhaPrincipal = _campanhaService.RetornarCampanhaPrincipalDoUsuario(idUsuario);
            if (campanhaPrincipal != null)
            {
                ConfigurarCampanha(campanhaPrincipal);
                cmbCampanha.Text = campanhaPrincipal.Nome;
            }
            else
            {
                MessageBox.Show("Não existe uma campanha principal configurada para este usuário.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CarregarCampanhasDoUsuario(int idUsuario)
        {
            _campanhasDoUsuario = _campanhaService.ListarCampanhasDoUsuario(idUsuario);
        }

        private void CarregarComboGrauDeParentesco(bool ativo)
        {
            _grauDeParentesco = _grauDeParentescoService.ListarParentescos(ativo);
            cmbGrauDeParentesco.PreencherComSelecione(_grauDeParentesco, x => x.id, x => x.grauDeParentesco);
        }

        private void CarregarComboStatusDeAtendimento(int idTipoStatus)
        {
            IEnumerable<KeyValuePair<int, string>> statusAtendimento = _statusDeAtendimentoService.ObterStatusDeAtendimento(idTipoStatus, ativo: true);
            cmbStatus.PreencherComSelecione(statusAtendimento);
        }

        private void CarregarComboStatusDoOperador(IEnumerable<KeyValuePair<int, string>> listaIncluir = null)
        {
            var listaStatus = new List<KeyValuePair<int, string>>();
            if (_discadorConectado.TipoDiscador == TipoDiscador.Akiva)
            {
                if (_xCallAkiva != null)
                {
                    var lista = _xCallAkiva.RetornarPausasOperador();
                    listaStatus = lista
                        .Where(x => x.Exibir)
                        .Select(x => new KeyValuePair<int, string>(x.Id, $"Pausa - {x.Nome}"
                        .ToUpper()))
                        .ToList();
                }
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
            {
                _integrationCca.ListPauseEvent(_usuario.Login);
                //var listaStatusCca = _integrationCca.ListPause(_usuario.Login);

                //var ListaCCa = listaStatusCca?.message?.
                //    Where(x => x.visible == true)
                //    .Select(x => new KeyValuePair<int, string>(x.id, $"Pausa - {x.name}"
                //        .ToUpper()))
                //    .ToList();

                //if (ListaCCa != null)
                //    listaStatus.AddRange(ListaCCa);
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.Olos)
            {
                //TODO:
            }

            //Configura um status padrão para disponível
            //listaRetorno.Add(new KeyValuePair<int, string>(1000, "DISPONÍVEL"));
            if (listaIncluir != null)
                listaStatus.AddRange(listaIncluir);

            listaStatus.AddRange(_listaStatusOperadorPadrao);
            cmbStatusOperador.PreencherComSelecione(listaStatus);
        }

        private void CarregarFAQ(int idCampanha)
        {
            var faq = _faqDeAtendimentoService.Listar(-1, idCampanha, ativo: true);

            listbox.Items.Clear();

            foreach (FaqDeAtendimento item in faq)
            {
                listbox.Items.Add(
                new ListBoxMessage.ParseMessageEventArgs(
                    ListBoxMessage.ParseMessageType.Question,
                    item.Pergunta,
                    item.Resposta));
            }

            listbox.Invalidate();
        }

        private void CarregarFormulario()
        {
            listbox = new ListBoxMessage.MessageListBox();
            listbox.Dock = DockStyle.Fill;
            listbox.AutoScroll = true;
            this.pnlFaq.Controls.Add(listbox);

            List<KeyValuePair<int, string>> listaStatusOperadorDefault = new List<KeyValuePair<int, string>>();
            listaStatusOperadorDefault.Add(new KeyValuePair<int, string>(1000, "DISPONÍVEL"));

            ts_mnuLogin.Text = _usuario.Login;
            nomeToolStripMenuItem.Text = _usuario.Nome;
            lblHidNomeOperador.Text = _usuario.Nome;

            AlterarLabelLogin(_usuario.Login);
            AlterarLabelNomeOperador(_usuario.Nome);
            AlterarLabelIP(ConfiguracaoDeAmbiente.RetornarEnderecoIP());
            AlterarLabelVersaoSistema(ConfiguracaoDeAmbiente.Release);

            if (_discadorConectado != null)
            {
                AlterarNomeDiscador(_discadorConectado.Nome);
            }

            tsTestes.Enabled = _usuario.Protegido;
            tsTestes.Visible = _usuario.Protegido;
            tsAtendimentoIdProspectTeste.Visible = _usuario.Protegido;
            tsAtendimentoIdProspectTeste.Enabled = _usuario.Protegido;

            CarregarCampanhasDoUsuario(_usuario.Id);

            PreencherCombosIniciais();
            CarregarComboStatusDoOperador();
            ConfigurarComboStatusDoOperadorAoIniciar(_discadorConectado);
            ConfigurarMenuDeAtendimento(habilitarFinalizar: false, habilitarAgendamentoAutomatico: false);
            //ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
            ResetarCombosTipoEStatusDeAtendimento(habilitarTipo: false, habilitarStatus: false);
            //ConfigurarBotaoAplicar(btnSalvarOferta, habilitar: false);
            ConfigurarBotoesDeDiscagem(habilitarConectar: false, habilitarDesconectar: false);
            ResetarCombosTipoEStatusDaOferta(habilitarTipo: false, habilitarStatus: false);
            ResetarTodosOsCamposDeVenda();
            ResetarComboCampanha(habilitar: true);
            CarregarCampanhaPrincipalDoUsuario(_usuario.Id);
            ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: false, exibirScriptFinalizacao: false);

            //SIMULAR
            RemoverTabs();
        }

        private void CarregarNotificacao()
        {

            try
            {
                IEnumerable<Notificacao> notificacao = _notificacaoService.VerificarNotificacaoDoUsuario(_usuario.Id);
                
                if (notificacao.Count() > 0)
                {
                    ExibirForm(new NotificacaoForm(_usuario));
                }
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro inesperado ao tentar carregar notificação: {ex.Message}";
                MessageBox.Show(msg);
            }
        }

        private void ExibirForm(Form form)
        {
            form.WindowState = FormWindowState.Normal;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void AlterarLabelVersaoSistema(string release)
        {
            tsLabelVesaoSistema.Text = $"Versão: {release}";
        }

        private void CarregarGridDeOfertaDoAtendimento()
        {
            long idAtendimento = _atendimentoEmAndamento.Id;
            dgOferta.DataSource = _ofertaDoAtendimentoService.ListarOfertaDoAtendimento(idAtendimento);

            RealizarAjustesGrid();
        }

        private void RealizarAjustesGrid()
        {
            for (int i = dgOferta.Columns["Status de Atendimento"].Index + 1; i < dgOferta.Columns.Count; i++)
            {
                dgOferta.Columns[i].Visible = false;
            }
        }

        private void CarregarGridHistoricoDeAtendimento(long idProspect, int idUsuario)
        {
            IEnumerable<HistoricoAtendimentoDto> historico = _atendimentoService.RetornarHistoricoDeAtendimento(idProspect, idUsuario);
            dgHistoricoAtendimento.DataSource = historico;
        }

        private void CarregarGridIndicacaoDoAtendimento(long idAtendimento)
        {
            dgIndicacao.AutoGenerateColumns = false;

            IEnumerable<Dominio.Entidades.IndicacaoDoAtendimento> historico = _atendimentoService.ListarIndicacaoDoAtendimento(idAtendimento);
            dgIndicacao.DataSource = null;
            dgIndicacao.DataSource = historico;
        }

        private void CarregarLayoutDinamico(int idLayout)
        {
            LayoutDeCampoDinamico layout = _layoutDinamicoService.RetornarLayoutDinamico(idLayout);
            _containerDeLayoutDinamico.CarregarLayout(layout);
        }

        private void CarregarValoresDeCamposDinamicos(IEnumerable<ValorDeCampoDinamico> valores)
        {
            if (valores == null) return;

            _containerDeLayoutDinamico.PreencherCampos(valores);
        }

        private void ConfigurarAfterCall(int segundosAfterCall)
        {
            _timespanAfterCall = new TimeSpan(0, 0, 0, segundosAfterCall);
            ConfigurarLabelAferCall(_timespanAfterCall, Color.CornflowerBlue, foreColor: Color.White);
        }

        private void ConfigurarAtendimento(Atendimento atendimento, OrigemDeAtendimento origem, Usuario usuario, Campanha campanha, long? telefoneDaChamada)
        {
            if (atendimento == null)
                throw new ArgumentException("Atendimento não pode ser nulo");

            if (campanha == null)
                throw new ArgumentException("Campanha não pode ser nulo");

            _atendimentoEmAndamento = atendimento;

            ConfigurarCampanha(campanha);

            if (atendimento.CamposDoAtendimento != null)
            {
                CarregarValoresDeCamposDinamicos(atendimento.CamposDoAtendimento);
            }

            ConfigurarTabPage(tcAtendimento, tcAtendimento_tpDadosProspect, selecionarTab: true);

            ConfigurarTabPage(tcAtendimento, tcAtendimento_tpFaq, selecionarTab: false);

            ConfigurarMenuDeAtendimentoParaNovoAtendimento();
            ConfigurarPainelDeAtendimentoParaNovoAtendimento(campanha, origem, atendimento.IdProspect, telefoneDaChamada);
            ConfigurarPainelDeInteracaoParaNovoAtendimento(campanha, atendimento.IdProspect);
            ConfigurarMenuStatusDeOfertaParaNovoAtendimento();
            ConfigurarStatusDeOfertaParaNovoAtendimento(campanha);
            ConfigurarCamposMenuSuperior(habilitarConsultaProspect: false, hablitarPararTempo: true);

            if (campanha != null && campanha.HabilitaHistorico)
            {
                ConfigurarTabPage(tcAtendimento, tcAtendimento_tpHistorico, selecionarTab: false);
                CarregarGridHistoricoDeAtendimento(atendimento.IdProspect, usuario.Id);
            }

            ConfigurarTabPage(tcAtendimento, tcAtendimento_tpOferta, selecionarTab: false);

            if (campanha.HabilitaIndicacao)
            {
                ConfigurarTabPage(tcAtendimento, tcAtendimento_tpIndicacao, selecionarTab: false);
                CarregarGridIndicacaoDoAtendimento(atendimento.Id);
            }

            if (campanha.HabilitaComparadorDePlanos)
            {
                ConfigurarTabPage(tcAtendimento, tcAtendimento_tpComparador, selecionarTab: false);
                CarregarControlesAbaComparadorDePlanos();
            }

            if (campanha.HabilitaPesquisa)
            {
                ConfigurarTabPage(tcAtendimento, tcAtendimento_tpPesquisa, selecionarTab: false);
                CarregarPesquisa();
            }

            if (campanha.IdScriptApresentacao != null)
            {
                ConfigurarTabPageSuperior(exibirScriptAtendimento: true, exibirScriptOferta: false, exibirScriptFinalizacao: false);
                CarregarScriptDeAtendimento(campanha.IdScriptApresentacao.Value, campanha);
            }
        }

        private void CarregarControlesAbaComparadorDePlanos()
        {
            IEnumerable<Operadora> operadora = _planoParaComparacaoService.ListarOperadora();
            cmbOperadora.PreencherComSelecione(operadora, x => x.Id, x => x.Nome);

            IEnumerable<TipoDePlanoPorOperadora> tipoDePlano = _planoParaComparacaoService.ListarTipoDePlanoPorOperadora();
            cmbTipoDePlanoOperadora.PreencherComSelecione(tipoDePlano, x => x.Id, x => x.Nome);
            cmbTipoDePlanoClaro.PreencherComSelecione(tipoDePlano, x => x.Id, x => x.Nome);

            gvPlanoOperadora.DataSource = _planoParaComparacaoService.Listar(0, 0, _campanhaAtual.Id);
            gvPlanoClaro.DataSource = _planoParaComparacaoService.Listar(0, 0, _campanhaAtual.Id);

            LimparDadosComparacao();
        }

        private void CarregarPlanosParaComparacao()
        {
            List<string> mensagens = new List<string>();

            if (cmbOperadora.TextoEhSelecione())
            {
                mensagens.Add("Selecione a Operadora!");
            }
            if (cmbTipoDePlanoOperadora.TextoEhSelecione())
            {
                mensagens.Add("Selecione o Tipo de Plano da Operadora!");
            }
            if (cmbTipoDePlanoClaro.TextoEhSelecione())
            {
                mensagens.Add("Selecione o Tipo de Plano da Claro!");
            }

            if (mensagens.Any())
            {
                ExibirMensagens(mensagens.ToList());
            }
            else
            {
                int idOperadora = Convert.ToInt32(cmbOperadora.SelectedValue);
                int idTipoDePlanoOperadora = Convert.ToInt32(cmbTipoDePlanoOperadora.SelectedValue);
                int idTipoDePlanoClaro = Convert.ToInt32(cmbTipoDePlanoClaro.SelectedValue);

                gvPlanoOperadora.DataSource = _planoParaComparacaoService.Listar(idOperadora, idTipoDePlanoOperadora, _campanhaAtual.Id);
                gvPlanoOperadora.ClearSelection();

                gvPlanoClaro.DataSource = _planoParaComparacaoService.Listar(1, idTipoDePlanoClaro, _campanhaAtual.Id);
                gvPlanoClaro.ClearSelection();

                LimparDadosComparacao();
            }
        }

        private void LimparDadosComparacao()
        {
            lblPrecoAtual_comparador.Text = "";
            lblPrecoClaro_comparador.Text = "";
            lblDiferenca_comparador.Text = "";

            lblInternetAtual_comparador.Text = "";
            lblInternetClaro_comparador.Text = "";
        }

        private void CompararPlanos()
        {
            decimal valorPlanoOperadora = 0;
            decimal valorPlanoClaro = 0;
            decimal valorDiferenca = 0;
            string internetOperadora = "";
            string internetClaro = "";

            if (gvPlanoOperadora.SelectedRows.Count > 0 && gvPlanoClaro.SelectedRows.Count > 0)
            {
                decimal.TryParse(gvPlanoOperadora.SelectedRows[0].Cells["Valor"].Value.ToString(), out valorPlanoOperadora);
                internetOperadora = gvPlanoOperadora.SelectedRows[0].Cells["Pacote Dados Mensal"].Value.ToString();

                decimal.TryParse(gvPlanoClaro.SelectedRows[0].Cells["Valor"].Value.ToString(), out valorPlanoClaro);
                internetClaro = gvPlanoClaro.SelectedRows[0].Cells["Pacote Dados Mensal"].Value.ToString();

                valorDiferenca = valorPlanoClaro - valorPlanoOperadora;

                lblPrecoAtual_comparador.Text = valorPlanoOperadora.ToString("C");
                lblPrecoClaro_comparador.Text = valorPlanoClaro.ToString("C");
                lblDiferenca_comparador.Text = valorDiferenca.ToString("C");

                lblInternetAtual_comparador.Text = internetOperadora;
                lblInternetClaro_comparador.Text = internetClaro;
            }
            else
            {
                LimparDadosComparacao();
            }
        }

        private void CarregarPesquisa()
        {
            gvPesquisa.DataSource = _pesquisaService.CarregarPesquisaDoAtendimento(_atendimentoEmAndamento.Id);

            tsPergunta_btnCancelar.Enabled = false;
            tsPergunta_btnSalvar.Enabled = false;
            cmbResposta.Visible = false;
            lblPergunta.Visible = false;
            lblResposta.Visible = false;
        }

        private void CarregarPergunta(int linha)
        {
            if (linha >= 0)
            {
                long id = (long)gvPesquisa.Rows[linha].Cells["idRespostaDoAtendimento"].Value;
                int idPergunta = (int)gvPesquisa.Rows[linha].Cells["idPergunta"].Value;
                string pergunta = gvPesquisa.Rows[linha].Cells["pergunta"].Value.ToString();
                int idResposta = (int)gvPesquisa.Rows[linha].Cells["idResposta"].Value;
                string resposta = gvPesquisa.Rows[linha].Cells["resposta"].Value.ToString();

                lblIdResposta.Text = id.ToString();
                lblIdPergunta.Text = idPergunta.ToString();
                lblPergunta.Text = pergunta;

                IEnumerable<OpcaoDaPerguntaDaPesquisa> opcao = _pesquisaService.ListarOpcaoDaPerguntaDaPesquisa(idPergunta);
                cmbResposta.PreencherComSelecione(opcao, x => x.Id, x => x.Opcao);

                if (idResposta > 0)
                    cmbResposta.SelectedValue = idResposta.ToString();

                tsPergunta_btnCancelar.Enabled = true;
                tsPergunta_btnSalvar.Enabled = true;
                cmbResposta.Visible = true;
                lblPergunta.Visible = true;
                lblResposta.Visible = true;
            }
        }

        private void GravarRespostaDaPesquisa()
        {
            var mensagens = new List<string>();

            if (cmbResposta.TextoEhSelecione())
            {
                mensagens.Add("[Resposta] deve ser informada!");
            }
            else
            {
                RespostaDaPesquisaDoAtendimento resposta = new RespostaDaPesquisaDoAtendimento();

                resposta.Id = Convert.ToInt64(lblIdResposta.Text);
                resposta.IdAtendimento = _atendimentoEmAndamento.Id;
                resposta.IdPerguntaDaPesquisa = Convert.ToInt32(lblIdPergunta.Text);
                resposta.IdOpcaoRespondida = Convert.ToInt32(cmbResposta.SelectedValue);

                resposta.Id = _pesquisaService.GravarRespostaDoAtendimento(resposta);

                mensagens.Add("Resposta gravada com sucesso!");

                CarregarPesquisa();
            }

            if (mensagens.Count > 0)
                ExibirMensagens(mensagens);
        }

        private void CarregarScriptDeAtendimento(int idScript, Campanha campanha)
        {
            tabInteligenciaAtendimento.Text = "Inteligência de Atendimento - APRESENTAÇÃO";

            long telefone = RetornarTelefoneDaInteracao();

            if (telefone == 0)
                telefone = (long)_prospectDoAtendimento.Telefone01;

            var mensagens = _atendimentoService.VerificarSePodeRealizarVenda(campanha.Id, telefone, _prospectDoAtendimento.Campo003);

            bool podeApresentarOferta = true;

            if (mensagens.Any())
            {
                podeApresentarOferta = false;

                var msg = new List<string>();
                msg.AddRange(mensagens);

                ExibirMensagens(msg);
            }

            ScriptDeAtendimento script = _scriptDeAtendimentoService.RetornarScriptDeAtendimento(idScript);
            scriptDeApresentacaoControl.Iniciar(script, campanha, this, podeApresentarOferta);
        }

        private void CarregarScriptDeFinalizacao(int? idScript, Campanha campanha)
        {
            tabInteligenciaAtendimento.Text = "Inteligência de Atendimento - FINALIZAÇÃO";

            ScriptDeAtendimento script = null;
            if (idScript != null)
            {
                script = _scriptDeAtendimentoService.RetornarScriptDeAtendimento(idScript.Value);
            }

            if (idScript == null || script == null)
            {
                script = _scriptDeAtendimentoService.RetornarScriptDeAtendimento(-3);
            }

            scriptDeFinalizacaoControl.Iniciar(script, campanha);
        }

        private void ConfigurarBotoesDeDiscagem(bool habilitarConectar, bool habilitarDesconectar)
        {
            if (pctConectar.InvokeRequired)
            {
                pctConectar.Invoke(new MethodInvoker(() => pctConectar.Enabled = habilitarConectar));
                pctConectar.Invoke(new MethodInvoker(() => pctConectar.Image = habilitarConectar ? Resources.conectar24x24 : Resources.conectarGray24x24));
                pctConectar.Invoke(new MethodInvoker(() => pctDesconectar.Enabled = habilitarDesconectar));
                pctConectar.Invoke(new MethodInvoker(() => pctDesconectar.Image = habilitarDesconectar ? Resources.desconectar24x24 : Resources.desconectarGray24x24));
            }
            else
            {
                pctConectar.Enabled = habilitarConectar;
                pctConectar.Image = habilitarConectar ? Resources.conectar24x24 : Resources.conectarGray24x24;

                pctDesconectar.Enabled = habilitarDesconectar;
                pctDesconectar.Image = habilitarDesconectar ? Resources.desconectar24x24 : Resources.desconectarGray24x24;
            }
        }

        private void ConfigurarCampanha(Campanha campanha)
        {
            if (campanha != null)
            {
                bool alteracaoDeCampanha = campanha.Id != _campanhaAtual?.Id;

                _campanhaAtual = campanha;

                if (_containerDeLayoutDinamico != null)
                {
                    _containerDeLayoutDinamico.Visible = false;

                    if (alteracaoDeCampanha && campanha.IdLayoutCampoDinamico != null)
                        CarregarLayoutDinamico(campanha.IdLayoutCampoDinamico.Value);

                    _containerDeLayoutDinamico.Visible = true;
                }

                ConfigurarAfterCall(campanha.AfterCall);
                CarregarFAQ(campanha.Id);
                CarregarComboGrauDeParentesco(true);
                bool habilitaConsulta = campanha?.HabilitarContatoManual ?? false;
                ConfigurarCamposMenuSuperior(habilitarConsultaProspect: habilitaConsulta, hablitarPararTempo: null);
            }
        }

        private void ConfigurarCamposMenuSuperior(bool? habilitarConsultaProspect, bool? hablitarPararTempo)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    btnConsultarProspect.Enabled = habilitarConsultaProspect ?? btnConsultarProspect.Enabled;
                    btnPararTempo.Enabled = hablitarPararTempo ?? btnPararTempo.Enabled;

                }));
            }
            else
            {
                btnConsultarProspect.Enabled = habilitarConsultaProspect ?? btnConsultarProspect.Enabled;
                btnPararTempo.Enabled = hablitarPararTempo ?? btnPararTempo.Enabled;
            }
        }

        private void ConfigurarComboTelefoneDaInteracaoParaAtendimento(long idProspect, long? telefoneDaChamada)
        {
            var telefones = _prospectService.ListarTelefoneDoProspect(idProspect, ativo: true);
            cmbTelefoneInteracao.PreencherComSelecione(telefones, tel => tel.Id, tel => tel.Numero.ToString());

            if (telefoneDaChamada == null || telefoneDaChamada == 0)
            { 
                cmbTelefoneInteracao.SelecionarPrimeiroItemDisponivel();
            }

            if (telefoneDaChamada != null)
                cmbTelefoneInteracao.Text = telefoneDaChamada.ToString();
        }

        private void ConfigurarComboTelefoneDoAgendamento(long idProspect)
        {
            var telefones = _prospectService.ListarTelefoneDoProspect(idProspect, ativo: true);
            cmbTelAgendamento.PreencherComSelecione(telefones, tel => tel.Id, tel => tel.Numero.ToString());
        }

        private void ConfigurarDiscador(Discador discadorConectado, Usuario usuario)
        {
            if (discadorConectado == null) return;

            if (discadorConectado.TipoDiscador == TipoDiscador.Akiva)
            {
                AcessarAkivaSocket();
            }

            if (discadorConectado.TipoDiscador == TipoDiscador.CCA)
            {
                CCA_Login();
            }

            if (discadorConectado.TipoDiscador == TipoDiscador.Olos)
            {
                AcessarOlos();
            }

            if (discadorConectado.TipoDiscador == TipoDiscador.OlosAPI)
            {
                AcessarOlosApi();
            }
        }

        private void ConfigurarEventos()
        {
            //AfterCall
            FimDoAfterCall += OnFimDoAfterCall;

            //script de apresentação
            scriptDeApresentacaoControl.ProximaEtapaClick += ScriptDeApresentacaoControl_OnProximaEtapaClick;
            scriptDeApresentacaoControl.VoltarEtapaClick += ScriptDeApresentacaoControl_OnVoltarEtapaClick;
            scriptDeApresentacaoControl.ApresentarOfertaClick += ScriptDeApresentacaoControl_OnApresentarOfertaClick;

            //Script de ofertaDoAtendimento
            scriptDeOfertaControl.OnFinalizarScript += ScriptDeOfertaControl_OnFinalizarScript;
            //scriptAceiteControl.ProximaEtapaClick += ScriptAceiteControl_OnProximaEtapaClick;
            //scriptAceiteControl.VoltarEtapaClick += ScriptAceiteControl_OnVoltarEtapaClick;
            //scriptAceiteControl.ProximaEtapaCommitted += ScriptAceiteControl_OnProximaEtapaCommitted;
            //scriptAceiteControl.VoltarEtapaCommitted += ScriptAceiteControl_OnVoltarEtapaCommitted;
            //scriptAceiteControl.FinalizarScriptClick += ScriptAceiteControl_OnFinalizarScriptClick;

            //Combos
            ConfigurarEventosCombosTipoEStatusDeOferta();

            //if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
            //    ConfigurarEventosCCA(false);
        }

        private void ConfigurarEventosCombosTipoEStatusDeOferta()
        {
            //cmbTipoStatusOferta.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbStatusDeOferta.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //cmbTipoStatusOferta.ComboBox.Width = 200;
            //cmbStatusDeOferta.ComboBox.Width = 400;

            //cmbTipoStatusOferta.ComboBox.SelectionChangeCommitted += cmbTipoStatusOferta_OnSelectionChangeCommitted;
            //cmbStatusDeOferta.ComboBox.SelectionChangeCommitted += cmbStatusDeOFerta_SelectionChangeCommitted;
        }

        private void ConfigurarLabelAferCall(TimeSpan timesPan, Color backColor, Color foreColor)
        {
            //var texto = $"LIMITE TABULAÇÃO: {timesPan.Minutes:00}:{timesPan.Seconds:00}";
            string texto = $"LIMITE TABULAÇÃO: {(timesPan.TotalSeconds < 0 ? "-" : "")}{Math.Abs(timesPan.Minutes):00}:{Math.Abs(timesPan.Seconds):00}";
            AlterarLabel(lblAfterCall, texto);
            lblAfterCall.BackColor = backColor;
            lblAfterCall.ForeColor = foreColor;
        }

        private void ConfigurarLabelDuracaoPausa(TimeSpan timesPan, Color backColor, Color foreColor)
        {
            string texto;
            texto = $"TEMPO DE PAUSA: {(timesPan.TotalSeconds < 0 ? "-" : "")}{Math.Abs(timesPan.Minutes):00}:{Math.Abs(timesPan.Seconds):00}";

            AlterarLabel(lblTempoDePausa, texto);
            lblTempoDePausa.BackColor = backColor;
            lblTempoDePausa.ForeColor = foreColor;
        }

        private void ConfigurarLabelsInferioresAposAtendimento()
        {
            AlterarLabelStatusDiscador("DISPONÍVEL");
            AlterarLabelMensagemDiscador("");
        }

        private void ConfigurarMenuStatusDeOfertaAposAtendimento()
        {
            //ConfigurarBotaoAplicar(btnSalvarOferta, habilitar: false);
            ResetarCombosTipoEStatusDaOferta(habilitarTipo: false, habilitarStatus: false);
        }

        private void ConfigurarMenuStatusDeOfertaParaNovoAtendimento()
        {
            //ConfigurarBotaoAplicar(btnSalvarOferta, habilitar: true);
            ResetarCombosTipoEStatusDaOferta(habilitarTipo: true, habilitarStatus: false);
        }

        private void ConfigurarProximaOfertaElegivel(OfertaDoAtendimento oferta)
        {
            _ofertaAtualDoAtendimento = oferta;

            if (oferta == null)
            {
                List<string> msg = new List<string>();

                msg.Add("Nenhuma oferta disponível para esse cliente.");

                ExibirMensagens(msg);

                return;
            }

            tabInteligenciaAtendimento.Text = "Inteligência de Atendimento" + " - OFERTA " + _ofertaAtualDoAtendimento.NomeDaOferta;

            if (oferta.IdScriptOferta != null)
            {
                ExibirScriptDeOferta(oferta.IdScriptOferta.Value);
            }
            else
            {
                ExibirScriptDeOferta(-2);
            }
        }

        private void ConfigurarPainelDeAtendimentoParaNovoAtendimento(Campanha campanha, OrigemDeAtendimento origemDeAtendimento, long idProspect, long? telefoneDaChamada)
        {
            if (campanha == null) return;
            bool habilitaConectar = false;

            cmbCampanha.Text = campanha.Nome;
            cmbCampanha.Desabilitar();
            ConfigurarPictureBoxAplicar(pctAplicarCampanha, habilitar: false);

            CarregarComboTipoStatusDeAtendimento();
            cmbStatusOperador.Desabilitar();
            ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);

            ConfigurarComboTelefoneDaInteracaoParaAtendimento(idProspect, telefoneDaChamada);

            if (campanha.HabilitaDiscagemManual)
            {
                cmbTelefoneInteracao.Habilitar();
            }

            if (origemDeAtendimento == OrigemDeAtendimento.ConsultaDeCliente)
            {
                habilitaConectar = true;
            }

            ConfigurarBotoesDeDiscagem(habilitarConectar: habilitaConectar, habilitarDesconectar: false);
            ConfigurarComboStatusDoOperadorParaNovoAtendimento(_discadorConectado);

        }

        private void ConfigurarComboStatusDoOperadorParaNovoAtendimento(Discador discador)
        {
            if (discador == null) return;

            if (discador.TipoDiscador == TipoDiscador.Akiva)
            {
                cmbStatusOperador.Desabilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
            }

            if (discador.TipoDiscador == TipoDiscador.CCA)
            {
                cmbStatusOperador.Habilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);
            }

            if (discador.TipoDiscador == TipoDiscador.Olos)
            {
                cmbStatusOperador.Desabilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
            }
        }

        private void ConfigurarMenuDeAtendimento(bool habilitarFinalizar, bool habilitarAgendamentoAutomatico)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    btnFinalizarAtendimento.Enabled = habilitarFinalizar;
                    btnAgendamentoAutomatico.Enabled = habilitarAgendamentoAutomatico;
                }));
            }
            else
            {

                btnFinalizarAtendimento.Enabled = habilitarFinalizar;
                btnAgendamentoAutomatico.Enabled = habilitarAgendamentoAutomatico;
            }

        }

        private void ConfigurarComboStatusDoOperadorAoIniciar(Discador discador)
        {

            if (discador == null)
            {
                cmbStatusOperador.Desabilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
            }

            if (discador.TipoDiscador == TipoDiscador.Akiva)
            {
                cmbStatusOperador.Desabilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
            }

            if (discador.TipoDiscador == TipoDiscador.CCA)
            {
                cmbStatusOperador.Habilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);
            }

            if (discador.TipoDiscador == TipoDiscador.Olos)
            {
                cmbStatusOperador.Desabilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
            }
        }

        private void ConfigurarPainelDeInteracaoParaNovoAtendimento(Campanha campanha, long idProspect)
        {
            if (campanha == null) return;

            cmbTipoStatus.ResetarComSelecione(habilitar: true);
            cmbStatus.ResetarComSelecione(habilitar: false);

            ConfigurarComboTelefoneDoAgendamento(idProspect);
        }

        private void ConfigurarExibicaoScriptDeDeOferta()
        {
            //if (pnlsu.Controls.Contains(scriptDeOfertaControl) == false)
            //{
            //    pnlSuperior.Controls.Add(scriptDeOfertaControl);
            //}
            scriptDeOfertaControl.Visible = true;
            scriptDeOfertaControl.Dock = DockStyle.Fill;
        }

        private void ConfigurarExibicaoScriptDeAtendimento()
        {
            scriptDeApresentacaoControl.Visible = true;
            scriptDeApresentacaoControl.Dock = DockStyle.Fill;
        }

        private void ConfigurarExibicaoScriptDeFinalizacao()
        {
            scriptDeFinalizacaoControl.Visible = true;
            scriptDeFinalizacaoControl.Dock = DockStyle.Fill;
        }

        private void ConfigurarTabPageSuperior(bool exibirScriptAtendimento, bool exibirScriptOferta, bool exibirScriptFinalizacao)
        {
            LimparPainelSuperior();

            if (exibirScriptAtendimento)
            {
                ConfigurarExibicaoScriptDeAtendimento();
            }

            if (exibirScriptOferta)
            {
                ConfigurarExibicaoScriptDeDeOferta();
            }
            if (exibirScriptFinalizacao)
            {
                ConfigurarExibicaoScriptDeFinalizacao();
            }

        }

        private void ConfigurarPictureBoxAplicar(PictureBox pictureBox, bool habilitar)
        {
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke(new MethodInvoker(() => pictureBox.Enabled = habilitar));
                pictureBox.Invoke(new MethodInvoker(() => pictureBox.Image = habilitar ? Resources.aplicar24x24 : Resources.aplicarGray24x24));
            }
            else
            {
                pictureBox.Enabled = habilitar;
                pictureBox.Image = habilitar ? Resources.aplicar24x24 : Resources.aplicarGray24x24;
            }
        }

        private void ConfigurarStatusDeOfertaParaNovoAtendimento(Campanha campanha)
        {
            long idCampanha = campanha.Id;
            IEnumerable<TipoDeStatusDeOferta> tipoDeStatusDeOfertas = _statusDeOfertaService.ListarTipoDeStatusDeOferta(idCampanha, ativo: null);
            //cmbTipoStatusOferta.ComboBox.PreencherComSelecione(tipoDeStatusDeOfertas, x => x.Id, x => x.Nome);
            ResetarCombosTipoEStatusDaOferta(habilitarTipo: true, habilitarStatus: false);
        }

        private void ConfigurarTabDeAtendimentoAposAtendimento()
        {
            cmbCampanha.Habilitar();
            ConfigurarPictureBoxAplicar(pctAplicarCampanha, habilitar: true);

            //cmbStatusOperador.Desabilitar();
            //ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);

            cmbTelefoneInteracao.ResetarComSelecione(habilitar: false);
            ConfigurarBotoesDeDiscagem(habilitarConectar: false, habilitarDesconectar: false);

            //timerAfterCall.Stop();
            EncerrarContagemDePausa();
            ConfigurarAfterCall(_campanhaAtual?.AfterCall ?? 0);

            _timespanDuracaoPausa = new TimeSpan(0);
            ConfigurarLabelDuracaoPausa(_timespanDuracaoPausa, Color.CornflowerBlue, foreColor: Color.White);
        }

        private void ConfigurarTabPage(TabControl tabControl, TabPage tabPage, bool selecionarTab = true)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (tabControl.TabPages.Contains(tabPage) == false)
                        tabControl.TabPages.Add(tabPage);

                    if (selecionarTab)
                        tabControl.SelectTab(tabPage);
                }));
            }
            else
            {
                if (tabControl.TabPages.Contains(tabPage) == false)
                    tabControl.TabPages.Add(tabPage);

                if (selecionarTab)
                    tabControl.SelectTab(tabPage);
            }
        }

        private void ConfigurarTabResultadoDaInteracaoAposAtendimento()
        {
            cmbTipoStatus.ResetarComSelecione(habilitar: false);
            cmbStatus.ResetarComSelecione(habilitar: false);

            txtObservacaoOperador.Text = "";
            LimparCamposAgendamento();
        }

        private void ConfigurarTelaAoReceberLogoff()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(ConfigurarTelaAoReceberLogoff));
            }
            else
            {
                cmbStatusOperador.Desabilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
                ConfigurarMenuDeAtendimento(habilitarFinalizar: false, habilitarAgendamentoAutomatico: false);

                cmbTelefoneInteracao.Desabilitar();
                ConfigurarBotoesDeDiscagem(habilitarConectar: false, habilitarDesconectar: false);
                ResetarCombosTipoEStatusDeAtendimento(habilitarStatus: false, habilitarTipo: false);
                EncerrarContagemDePausa();
                ConfigurarAfterCall(_campanhaAtual?.AfterCall ?? 0);
                tabPainelAtendimento.Enabled = false;
                _timerAferCall.Stop();
            }
        }

        private void ContarTempoAfterCall()
        {
            long ticksAtuais = (long)_timespanAfterCall.TotalSeconds * TimeSpan.TicksPerSecond;

            if (ticksAtuais > 0)
            {
                long novoTempo = ticksAtuais - 1;
                _timespanAfterCall = new TimeSpan(novoTempo);
            }
            ConfigurarLabelAferCall(_timespanAfterCall, backColor: Color.Gold, foreColor: Color.Black);

            if (ticksAtuais == 0)
            {
                _timerAferCall.Stop();
                _afterCallEncerrado = true;
                _emAfterCall = false;

                try
                {
                    ExecutarTabulacaoAutomatica();
                }
                catch (Exception e)
                {
                    var msg = $"Ocorreu um erro inesperado ao tentar realizar a tabulação automática";
                    MessageBox.Show(msg);

                    _logger.Fatal(e);
                }
            }
        }

        private void ContarTempoDuracaoDePausa(bool progressivo)
        {
            long segundosAtuais = (long)_timespanDuracaoPausa.TotalSeconds;

            long novoTempo;
            if (progressivo)
            {
                novoTempo = (segundosAtuais + 1) * TimeSpan.TicksPerSecond;
            }
            else
            {
                novoTempo = (segundosAtuais - 1) * TimeSpan.TicksPerSecond;
            }

            _timespanDuracaoPausa = new TimeSpan(novoTempo);

            ConfigurarLabelDuracaoPausa(_timespanDuracaoPausa, Color.CornflowerBlue, Color.White);

            if (segundosAtuais < 0)
                ConfigurarLabelDuracaoPausa(_timespanDuracaoPausa, Color.Red, Color.Black);
        }

        private ResultadoDoAtendimento CriarResultadoDoAtendimento(Campanha campanha, Atendimento atendimento, StatusDeAtendimento status)
        {
            var resultado = new ResultadoDoAtendimento();

            resultado.Atendimento = atendimento;
            resultado.StatusDoAtendimento = status;
            resultado.Telefone = RetornarTelefoneDaInteracao();

            if (resultado.Telefone == 0)
                resultado.Telefone = (long)_prospectDoAtendimento.Telefone01;

            resultado.Observacao = txtObservacaoOperador.Text;

            if (campanha != null && status != null && (campanha?.IdStatusTabulacaoAutomatica == status.Id || campanha.IdStatusTabulacaoAutomaticaVenda == status.Id))
                resultado.TabulacaoAutomatica = true;

            if (status != null && status.TipoDeStatus == TipoStatusDeAtendimento.Agendamento)
            {
                resultado.DataAgendamento = RetornarDataDoAgendamento();
                resultado.TelefoneAgendamento = RetornarTelefoneDoAgendamento();
            }

            _logger.Debug($"Criando resultado do atendimento. Id do atendimento: {_atendimentoEmAndamento.Id}. Resultado: {resultado.Atendimento.Id}");

            return resultado;
        }

        private void DefinirStatusDeOferta(int idStatusOferta)
        {
            _statusDeOfertaSelecionado = null;

            if (idStatusOferta > 0)
            {
                _statusDeOfertaSelecionado = _statusDeOfertaService.RetornarStatusDeOferta(idStatusOferta, _campanhaAtual.Id);
            }
        }

        private void DefinirStatusDoAtendimento(int idStatusAtendimento)
        {
            _statusDoAtendimentoEmAndamento = null;

            if (idStatusAtendimento > 0)
            {
                _statusDoAtendimentoEmAndamento =
                    _statusDeAtendimentoService
                    .RetornarStatusDoAtendimento(idStatusAtendimento);
            }
        }

        private void DesconectarDiscador(Discador discador)
        {
            if (discador == null) return;

            if (discador.TipoDiscador == TipoDiscador.Akiva)
                XCallAkiva_Logout();

            if (discador.TipoDiscador == TipoDiscador.CCA)
            {
                _integrationCca.Logoff(_usuario.Login);
                _integrationCca.LogOffSoftPhone();
                _integrationCca.EncerrarChamadasAtivaRamal();

            }


            if (discador.TipoDiscador == TipoDiscador.Olos)
                Olos_RealizarLogOff();
        }



        private async void DiscarManual()
        {
            bool podeDiscar = VerificarSePodeRealizarDiscagem();
            bool sucessoDiscagem = false;

            if (podeDiscar)
            {
                string telefone = cmbTelefoneInteracao.Text;

                bool desconectar = pctDesconectar.Enabled;
                ConfigurarBotoesDeDiscagem(habilitarConectar: false, habilitarDesconectar: desconectar);

                if (_discadorConectado.TipoDiscador == TipoDiscador.Akiva)
                {
                    sucessoDiscagem = await XCallAkiva_DiscarAsync(telefone);
                }

                if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
                {
                    CCA_Discar(telefone, _atendimentoEmAndamento.IdProspect);
                }

                if (_discadorConectado.TipoDiscador == TipoDiscador.Olos)
                {
                    sucessoDiscagem = Olos_Discar(telefone);
                }

                if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI)
                {
                    if (!_emPausa)
                    {
                        MessageBox.Show($"Para fazer ligação Manual o operador deve estar em pausa!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    Invoke((MethodInvoker)delegate { timerTabulacao.Stop(); });
                    //statusAutomaticoAkiva = 0;
                    //Discagem Callflex - FALTA TESTAR NO AMBIENTE DO CLIENTE
                    DiscagemManualOLOSAPI(telefone);
                }

                ConfigurarBotoesDeDiscagem(habilitarConectar: true, habilitarDesconectar: desconectar);
            }

            bool habilitarConectar = !sucessoDiscagem;
            ConfigurarBotoesDeDiscagem(habilitarConectar: habilitarConectar, habilitarDesconectar: !habilitarConectar);
        }

        private void DiscagemManualOLOSAPI(string sTelefone)
        {
            string sComando;

            try
            {
                if (string.IsNullOrEmpty(cmbTelefoneInteracao.Text) || cmbTelefoneInteracao.Text == "SELECIONE...")
                {
                    sComando = "Não há telefone para discar..." + Environment.NewLine;
                    RegistrarEventos(sComando);
                    pctConectar.Enabled = true;
                    pctDesconectar.Enabled = false;
                }
                else
                {
                    if (sTelefone.Length == 10 || sTelefone.Length == 11)
                    {
                        string ddd = sTelefone.Substring(0, 2);
                        string telefone = sTelefone.Substring(2, sTelefone.Length - 2);
                        sComando = "Discando para " + sTelefone + "..." + Environment.NewLine;
                        RegistrarEventos(sComando);
                        // _olosWsAgentControl.SendManualCallRequest(_agentIdOlos, ddd, telefone, fLogin.Usuario.IDCampanhaDiscador);
                        byte[] enviarStatusOlos = ConvertStrToBytes("[2:" + sTelefone + "]\n");
                        client.SendBytes(enviarStatusOlos, enviarStatusOlos.Length);
                    }
                    else
                    {
                        sComando = "Telefone incorreto..." + Environment.NewLine;
                        RegistrarEventos(sComando);
                    }
                }
            }
            catch (Exception ex)
            {
                RegistrarEventos(ex.Message);
            }
        }

        private void RegistrarEventos(string evento)
        {
            if (this.listBoxEventos.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(RegistrarEventos);
                this.Invoke(d, new object[] { evento });
            }
            else
            {
                Invoke((MethodInvoker)delegate { listBoxEventos.Items.Insert(0, evento); });
            }
        }

        private void EncerrarContagemDePausa()
        {
            _logger.Debug("Encerrar contagem de pausa");
            _timerPausa.Stop();
            _timerPausa.Enabled = false;
            _timespanDuracaoPausa = new TimeSpan(0);
            ConfigurarLabelDuracaoPausa(_timespanDuracaoPausa, Color.CornflowerBlue, Color.White);
        }

        private void EncerrarDiscagemManual()
        {
            bool sucessoEncerrar = true;
            bool conectarHabilitado = pctConectar.Enabled;

            _logger.Debug("Encerrar discagem manual");

            ConfigurarBotoesDeDiscagem(habilitarConectar: conectarHabilitado, habilitarDesconectar: false);

            if (_discadorConectado.TipoDiscador == TipoDiscador.Akiva)
            {
                sucessoEncerrar = XCallAkiva_EncerrarChamada();
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
            {
                CCA_DesligarLigacao();
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.Olos)
            {
                //TODO:
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI)
            {
                OLOSAPI_DesligarLigacao();
            }

            bool habilitarDesconectar = !sucessoEncerrar;

            _logger.Debug($"Retorno encerrar discagem manual: {sucessoEncerrar}");

            ConfigurarBotoesDeDiscagem(habilitarConectar: conectarHabilitado, habilitarDesconectar: habilitarDesconectar);
        }

        private void OLOSAPI_DesligarLigacao()
        {
            if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI)
            {
                int callId = Convert.ToInt32(sIDGravacaoDiscador);
                _olosWsAgentControl.HangupRequest(_agentIdOlos, callId);
            }
        }

        private async void ExecutarTabulacaoAutomatica()
        {
            if (_campanhaAtual != null)
            {
                if (_campanhaAtual.IdStatusTabulacaoAutomatica == null || _campanhaAtual.IdStatusTabulacaoAutomatica < 0)
                {
                    var msg = $"Não existe um status de tabulação automático associado à campanha [{_campanhaAtual.Nome}]";
                    _logger.Info(msg);
                    MessageBox.Show(msg);
                    return;
                }

                int idStatusTabulacaoAutomatica = 0;

                if (_atendimentoEmAndamento.HouveAceiteDeOferta)
                {
                    idStatusTabulacaoAutomatica = _campanhaAtual.IdStatusTabulacaoAutomaticaVenda ?? 0;
                }
                else
                {
                    idStatusTabulacaoAutomatica = _campanhaAtual.IdStatusTabulacaoAutomatica ?? 0;
                }

                var statusAutomatico = RetornarStatusDoAtendimento(idStatusTabulacaoAutomatica);

                if (statusAutomatico == null)
                {
                    var msg = $"Não foi possível obter o Status para a tabulação automática para a camapanha [{_campanhaAtual.Nome}]";
                    _logger.Info(msg);
                    MessageBox.Show(msg);
                    return;
                }
                _statusDoAtendimentoEmAndamento = statusAutomatico;
                await FecharJanelasAbertasAsync();
                FinalizarAtendimento();
            }
        }

        private void ExibirMensagemDeFimDeAtendimento(long idResultado, string status)
        {
            string mensagem = "Atendimento finalizado com sucesso!\n";
            mensagem += $"\nCódigo do Resultado: {idResultado}";
            mensagem += $"\nStatus: {status}";
            AutoClosingMessageBox.Show(mensagem, "Aviso do sistema", 2000);
        }

        private void ExibirMensagemDiscador(string msg)
        {
            var nomeDiscador = _discadorConectado.Nome;
            var msgFinal = $"Retorno do discador {nomeDiscador}:\n{msg}";
            MessageBox.Show(msgFinal, "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);

                AutoClosingMessageBox.Show(msgFinal, "Aviso do sistema", 10000);

                //MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ExibirTelaDeOferta(StatusDeOferta statusOferta)
        {
            var oferta = _ofertaAtualDoAtendimento;

            if (oferta == null)
            {
                _logger.Error("Não existe uma oferta carregada para ser exibida.");
                MessageBox.Show("Não existe uma oferta sendo trabalhada.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (oferta.IdTipoDeProduto == 1)
            {
                IndicacaoForm formIndicacao = new IndicacaoForm(_usuario, _prospectDoAtendimento, _atendimentoEmAndamento.Id);
                formIndicacao.PararTempoEvent += OnPararTempo;
                _pilhaDeJanelas.Push(formIndicacao);
                formIndicacao.StartPosition = FormStartPosition.CenterScreen;
                formIndicacao.ShowDialog();
                if (_pilhaDeJanelas.Any())
                    _pilhaDeJanelas.Pop();
            }

        }

        private bool ConfigurarStatusDeAtendimentoDeAcordoComOStatusDeOferta(StatusDeOferta statusDaOferta)
        {
            try
            {
                if (!(statusDaOferta?.Id > 0)) return false;

                ConfiguracaoDoStatusDeOferta configuracao = _statusDeOfertaService.RetornarConfiguracaoDoStausDeOferta(idStatusOferta: statusDaOferta.Id, idCampanha: _campanhaAtual.Id);

                if (configuracao?.IdStatusDeAtendimentoPadrao == null) return false;

                var statusDeAtendimento = _statusDeAtendimentoService.RetornarStatusDoAtendimento(configuracao.IdStatusDeAtendimentoPadrao.Value);

                if (statusDeAtendimento == null || statusDeAtendimento.Ativo == false) return false;


                if (statusDeAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Aceite)
                    ConfigurarStatusDeAtendimentoParaAceite();

                if (statusDeAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Recusa)
                    ConfigurarStatusDeAtendimentoParaRecusa();

                if (statusDeAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Agendamento)
                    ConfigurarStatusDeAtendimentoParaAgendamento();

                if (statusDeAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Telefonia)
                    ConfigurarStatusDeAtendimentoParaTelefonia();


                cmbTipoStatus.SelectedValue = ((int)(statusDeAtendimento.TipoDeStatus)).ToString();
                cmbTipoStatus_SelectionChangeCommitted(null, null);
                cmbStatus.SelectedValue = statusDeAtendimento.Id.ToString();
                DefinirStatusDoAtendimento(statusDeAtendimento.Id);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                MessageBox.Show($"Ocorreu um erro inesperado ao tentar configurar O Status de atendimento padrão. Erro: {e.Message}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private void OnPararTempo(int? idUsuario)
        {
            try
            {
                PausarContagemDeAfterCall();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                MessageBox.Show($"Ocorreu um erro inesperado ao tentar pausar a contagem de AfterCall. Erro: {e.Message}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void ExibirScriptDeOferta(int idScript)
        {
            ScriptDeAtendimento script = _scriptDeAtendimentoService.RetornarScriptDeAtendimento(idScript);

            scriptDeOfertaControl.Visible = true;

            scriptDeOfertaControl.Iniciar(script, _campanhaAtual, this);
            ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: true, exibirScriptFinalizacao: false);
        }

        private bool FinalizarAtendimento()
        {
            bool sucessoDiscador = false;
            Campanha campanha = _campanhaAtual;
            StatusDeAtendimento status = _statusDoAtendimentoEmAndamento;
            Atendimento atendimento = _atendimentoEmAndamento;
            Discador discador = _discadorConectado;

            var resultado = CriarResultadoDoAtendimento(campanha, atendimento, status);
            if (!VerificarSePodeFinalizarAtendimento(resultado)) return false;

            ConfigurarMenuDeAtendimento(false, false);

            if (resultado != null)
            {
                _timerAferCall.Stop();

                cmbStatusOperador.Habilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);

                _resultadoDoAtendimentoAtual = resultado;
                _logger.Debug($"Finalizar atendimento no Discador:  {resultado.StatusDoAtendimento.Id}");
                sucessoDiscador = FinalizarAtendimentoNoDiscador(campanha, discador, resultado);
                _logger.Debug($"Resultado ao finalizar atendimento no Discador: {sucessoDiscador}");

                if (resultado.TabulacaoAutomatica)
                {
                    ResetarScriptsDaOfertaAtual();
                    ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: false, exibirScriptFinalizacao: false);
                }

                if (sucessoDiscador)
                {
                    ExecutarProcedimentosSucessoDiscador();

                    CarregarNotificacao();

                    return true;
                }
                else
                {
                    ResetarScriptsDaOfertaAtual();
                    ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: false, exibirScriptFinalizacao: false);
                }
            }

            ConfigurarMenuDeAtendimento(true, true);

            return false;
        }

        private void ExecutarProcedimentosSucessoDiscador()
        {
            BloquearCamposDeAtendimentoAposTabulacao();
            ResetarScriptsDaOfertaAtual();
            ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: false, exibirScriptFinalizacao: false);
            ExecutarTerminoDeAtendimentoPorDiscador(_discadorConectado);
            _emAfterCall = false;

            VerificarTerminoDeTurno();
        }

        private void VerificarTerminoDeTurno()
        {
            if (_usuario.TerminoDeTurno < DateTime.Now)
            {
                string msg = "O seu turno de trabalho encerrou! O sistema será finalizado!";
                AutoClosingMessageBox.Show(msg, "Aviso do sistema", 10000);

                Close();
                Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }

        private void ExecutarTerminoDeAtendimentoPorDiscador(Discador discador)
        {
            if (discador.TipoDiscador == TipoDiscador.Akiva) return;

            if (discador.TipoDiscador == TipoDiscador.CCA) return;

            if (discador.TipoDiscador == TipoDiscador.Olos)
            {
                FinalizarAtendimentoCRM();

                if (_idProspectEmBufferParaCarregamento > 0)
                {
                    IniciarAtendimento(_idProspectEmBufferParaCarregamento, _telefoneEmBufferParaCarregamento, _ticketEmBufferParaCarregamento, OrigemDeAtendimento.Preditivo);
                }
            }

            if (discador.TipoDiscador == TipoDiscador.OlosAPI)
            {
                FinalizarAtendimentoCRM();

                if (_idProspectEmBufferParaCarregamento > 0)
                {
                    IniciarAtendimento(_idProspectEmBufferParaCarregamento, _telefoneEmBufferParaCarregamento, _ticketEmBufferParaCarregamento, OrigemDeAtendimento.Preditivo);
                }
            }
        }

        private void ExecutarAgendamentoAutomatico()
        {
            //O ID ESTÁ FIXO POR SE TRATAR DE UMA ALTERAÇÃO TEMPORARIA
            int idStatusAgendamentoAutomatico = 14;
            int minutosAgendamento = 5;
            var dataAgendamento = DateTime.Now.AddMinutes(minutosAgendamento);

            cmbTipoStatus.SelectedValue = ((int)(TipoStatusDeAtendimento.Agendamento)).ToString();
            cmbTipoStatus_SelectionChangeCommitted(null, null);
            cmbStatus.SelectedValue = ((int)idStatusAgendamentoAutomatico).ToString();
            txtHoraAgendamento.Text = $"{dataAgendamento.Hour:00}:{dataAgendamento.Minute:00}";
            DefinirStatusDoAtendimento(idStatusAgendamentoAutomatico);
            FinalizarAtendimento();
        }

        private void BloquearCamposDeAtendimentoAposTabulacao()
        {
            if (InvokeRequired)
                Invoke(new Action(BloquearCamposDeAtendimentoAposTabulacao));
            else
            {
                cmbTipoStatus.Desabilitar();
                cmbStatus.Desabilitar();
                cmbTelAgendamento.Desabilitar();
                txtObservacaoOperador.Enabled = false;
                dtAgendamento.Enabled = false;
                txtHoraAgendamento.Enabled = false;
                ConfigurarMenuDeAtendimento(false, false);
            }
        }

        private void LiberarCamposDeTabulacaoAposErro()
        {
            if (InvokeRequired)
                Invoke(new Action(LiberarCamposDeTabulacaoAposErro));
            else
            {
                cmbTipoStatus.Habilitar();
                cmbStatus.Habilitar();
                cmbTelAgendamento.Habilitar();
                txtObservacaoOperador.Enabled = true;
                dtAgendamento.Enabled = true;
                txtHoraAgendamento.Enabled = true;
                ConfigurarMenuDeAtendimento(true, true);
            }
        }

        private void FinalizarAtendimentoCRM()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinalizarAtendimentoCRM));
            }
            else
            {
                var resultado = _resultadoDoAtendimentoAtual;

                if (resultado != null)
                {
                    var idResultado = FinalizarAtendimentoNoCrm(resultado);
                    ExibirMensagemDeFimDeAtendimento(idResultado, resultado.StatusDoAtendimento.Nome);
                    ResetarFormularioAposAtendimento();
                    AtualizarDadosDoRankingDoOperador();
                }
                else
                {
                    _logger.Error("Tentativa de finalizar atendimento sem um resultado definido.");
                }
            }
        }

        private void AtualizarDadosDoRankingDoOperador()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(AtualizarDadosDoRankingDoOperador));
            }
            else
            {
                var dadosDoRanking = new DadosDoRanking();
                dadosDoRanking = _atendimentoService.RetornarDadosDosAtendimentos(_usuario.Id, _campanhaAtual.Id);

                AlterarLabel(lblQtdLigacao, dadosDoRanking.QtdLigacao.ToString());
                AlterarLabel(lblQtdVendas, dadosDoRanking.QtdVenda.ToString());
                AlterarLabel(lblMeta, dadosDoRanking.MetaVenda.ToString());
                AlterarLabel(lblQtdFaltando, dadosDoRanking.Faltando.ToString());
                AlterarLabel(linkRanking, dadosDoRanking.Ranking.ToString());
            }
        }

        private void FecharJanelasAbertas()
        {
            try
            {
                foreach (Form janela in _pilhaDeJanelas)
                {
                    janela.Close();
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);

            }
        }

        private async Task FecharJanelasAbertasAsync()
        {
            await Task.Run(() =>
            {
                Invoke((MethodInvoker)FecharJanelasAbertas);
            });
        }

        private long FinalizarAtendimentoNoCrm(ResultadoDoAtendimento resultado)
        {
            return _atendimentoService.FinalizarAtendimento(resultado);
        }

        private bool FinalizarAtendimentoNoDiscador(Campanha campanha, Discador discador, ResultadoDoAtendimento resultado)
        {
            var sucessoAoFinalizar = true;
            var dataAgendamento = string.Empty;
            bool deveEncerrarContato = VerificarSeDeveEncerrarContatoNoDiscador(campanha, discador, resultado);
            long telefoneAgendamento = resultado.TelefoneAgendamento ?? resultado.Telefone;

            if (discador != null && discador.TipoDiscador == TipoDiscador.Akiva)
            {
                var tabulacaoAkiva = string.Empty;
                var idStatus = resultado.StatusDoAtendimento.Id;

                if (resultado.StatusDoAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Agendamento)
                {
                    dataAgendamento = resultado.DataAgendamento?.ToString("yyyy-MM-dd HH:mm") ?? "";
                    tabulacaoAkiva = $"{idStatus}|{dataAgendamento}|{telefoneAgendamento}";
                }
                else
                {
                    tabulacaoAkiva = $"{idStatus}|0|{telefoneAgendamento}";
                }

                AlterarLabelStatusDiscador($"Tabulação Enviada: {tabulacaoAkiva}");
                SendMessagePoolAkiva(tabulacaoAkiva);
                _tabulacaoFoiEnviadaAkivaSocket = true;

                #region dll akiva
                //int idStatus = resultado.StatusDoAtendimento.Id;
                //var tabulacaoAkiva = XCallAkiva_RetornarStatusAkiva(resultado.StatusDoAtendimento.Id);
                //if (tabulacaoAkiva == null)
                //{
                //    _logger.Debug($"Tabulação do Akiva não localizada. IdStatus: {idStatus}");
                //    return false;
                //}

                //int idDisposition = tabulacaoAkiva.Id;
                //string idExterno = resultado.Atendimento.IdProspect.ToString();
                //bool solicitouGravacao = false;
                //string observacao = "";
                //int idAuditor = 0;
                //int rediscagem = 0;
                //int mailingPhoneRedial = 0;
                //int queueClassReason = 0;

                //sucessoAoFinalizar =  XCallAkiva_SetDisposition(idDisposition, dataAgendamento, observacao, solicitouGravacao, idExterno, idAuditor, rediscagem, mailingPhoneRedial, queueClassReason);
                #endregion
            }
            else if (discador != null && discador.TipoDiscador == TipoDiscador.CCA)
            {
                if (resultado.StatusDoAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Agendamento)
                {
                    var data = (DateTime)resultado.DataAgendamento;
                    CCA_AgendarTelefone(resultado.Atendimento.IdProspect, _usuario.Login, resultado.TelefoneAgendamento ?? 0, data.ToString("yyyy-MM-dd HH:mm"));
                }
                else if (deveEncerrarContato)
                {
                    CCA_FinalizarContrato(resultado.Atendimento.IdProspect, _usuario.Login, resultado.StatusDoAtendimento.Nome, resultado.Atendimento.NumeroChamadaDiscador);
                }
                else
                {//apenas atualiza os dados no discador
                    CCA_TabularContrato(resultado.Atendimento.IdProspect, _usuario.Login, resultado.StatusDoAtendimento.Nome, resultado.Atendimento.NumeroChamadaDiscador);
                }

                AlterarLabelStatusDiscador($"Tabulação Enviada: {resultado.StatusDoAtendimento.Id}");
            }
            else if (discador != null && discador.TipoDiscador == TipoDiscador.Olos)
            {
                var tabulacaoOlos = string.Empty;
                var idStatus = resultado.StatusDoAtendimento.Id;
                byte[] enviarStatusOlos = null;

                if (resultado.StatusDoAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Agendamento)
                {
                    dataAgendamento = resultado.DataAgendamento?.ToString("dd/MM/yyyy HHmm") ?? "";

                    if (resultado.StatusDoAtendimento.Nome.Contains("PÚBLICO"))
                    {
                        tabulacaoOlos = $"[3:{idStatus}:{dataAgendamento}:dd/MM/yyyy HHmm:{telefoneAgendamento}:0]\n";
                    }
                    else
                    {
                        tabulacaoOlos = $"[3:{idStatus}:{dataAgendamento}:dd/MM/yyyy HHmm:{telefoneAgendamento}:1]\n";
                    }
                }
                else
                {
                    tabulacaoOlos = $"[3:{idStatus}:]\n";
                }

                if (client != null && client.Connected)
                {
                    enviarStatusOlos = ConvertStrToBytes(tabulacaoOlos);
                    client?.SendBytes(enviarStatusOlos, enviarStatusOlos.Length);
                    AlterarLabelStatusDiscador($"Tabulação Enviada: {tabulacaoOlos}");
                    _tabulacaoFoiEnviadaOlosSocket = true;
                }
            }

            return sucessoAoFinalizar;
        }

        private bool VerificarSeDeveEncerrarContatoNoDiscador(Campanha campanha, Discador discador, ResultadoDoAtendimento resultado)
        {
            bool deveFinalizarContato = false;

            bool ehAceite = resultado.StatusDoAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Aceite
                            || resultado.StatusDoAtendimento.Id == campanha?.IdStatusTabulacaoAutomaticaVenda;

            bool ehRecusa = resultado.StatusDoAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Recusa;

            if (ehAceite) return true;

            if (ehRecusa) return true;

            return deveFinalizarContato;
        }

        private void FinalizarScriptDeOferta(StatusDeOferta statusDaOferta)
        {
            var ofertaAtual = _ofertaAtualDoAtendimento;
            Campanha campanha = _campanhaAtual;
            Atendimento atendimento = _atendimentoEmAndamento;

            if (statusDaOferta != null)
            {

                if (statusDaOferta.TipoStatus == TipoStatusDeOferta.ACEITE)
                {
                    bool podeGravar = AbrirFormIndicacao(); 

                    if (podeGravar)
                    {
                        GravarAtendimento(statusDaOferta, ofertaAtual, campanha);
                    }
                }

                if (statusDaOferta.TipoStatus == TipoStatusDeOferta.RECUSA || statusDaOferta.TipoStatus == TipoStatusDeOferta.TELEFONIA || statusDaOferta.TipoStatus == TipoStatusDeOferta.AGENDAMENTO)
                {
                    if (!_afterCallEncerrado)
                    {
                        GravarAtendimento(statusDaOferta, ofertaAtual, campanha);
                    }
                }

                if (_afterCallEncerrado == false)
                {
                    //CONFIGURAR STATUS DE ATENDIMENO AUTOMATICAMENTE, DE ACORDO COM O STATUS DA OFERTA, CASO EXISTA APENAS UMA OFERTA NO ATENDIMENTO
                    IEnumerable<OfertaDoAtendimento> ofertasDoAtendimento = _ofertaDoAtendimentoService.RetornarOfertasDoAtendimento(_atendimentoEmAndamento.Id);
                    if (ofertasDoAtendimento != null && ofertasDoAtendimento.Count() == 1)
                    {
                        var ofertaDoAtendimento = ofertasDoAtendimento?.First();
                        if (ofertaDoAtendimento.IdStatusDaOferta != null)
                        {
                            var status = _statusDeOfertaService.RetornarStatusDeOferta(ofertaDoAtendimento.IdStatusDaOferta.Value, _campanhaAtual.Id);
                            bool deveFinalizar = ConfigurarStatusDeAtendimentoDeAcordoComOStatusDeOferta(status);
                            if (deveFinalizar)
                            {
                                //FinalizarAtendimento();
                            }
                        }
                    }
                }

            }
        }

        private void GravarAtendimento(StatusDeOferta statusDaOferta, OfertaDoAtendimento ofertaAtual, Campanha campanha)
        {
            GravarStatusDaOferta(ofertaAtual, statusDaOferta);
            ResetarScriptsDaOfertaAtual();
            CarregarGridDeOfertaDoAtendimento();
            ConfigurarTabPageSuperior(exibirScriptOferta: false, exibirScriptAtendimento: false, exibirScriptFinalizacao: true);
            CarregarScriptDeFinalizacao(campanha.IdScriptFinalizacao, campanha);
        }

        private bool AbrirFormIndicacao()
        {
            var formIndicacao = new IndicacaoForm(_usuario,_prospectDoAtendimento, _atendimentoEmAndamento.Id);

            ExibirForm(formIndicacao);

            return formIndicacao.Atualizar;
        }

        private void ConfigurarStatusDeAtendimentoParaAceite()
        {
            var listaTipos = _statusDeAtendimentoService.ListarTipoDeStatusDeAtendimento(ativo: true);
            listaTipos = listaTipos?.Where(x => (TipoStatusDeOferta)x.Key == TipoStatusDeOferta.ACEITE).Select(x => x).ToList();
            cmbTipoStatus.PreencherComSelecione(listaTipos);
            cmbStatus.ResetarComSelecione(habilitar: false);
        }

        private void ConfigurarStatusDeAtendimentoParaRecusa()
        {
            var listaTipos = _statusDeAtendimentoService.ListarTipoDeStatusDeAtendimento(ativo: true);
            listaTipos = listaTipos?.Where(x => (TipoStatusDeOferta)x.Key == TipoStatusDeOferta.RECUSA).Select(x => x).ToList();
            cmbTipoStatus.PreencherComSelecione(listaTipos);
            cmbStatus.ResetarComSelecione(habilitar: false);
        }

        private void ConfigurarStatusDeAtendimentoParaAgendamento()
        {
            var listaTipos = _statusDeAtendimentoService.ListarTipoDeStatusDeAtendimento(ativo: true);
            listaTipos = listaTipos?.Where(x => (TipoStatusDeOferta)x.Key == TipoStatusDeOferta.AGENDAMENTO).Select(x => x).ToList();
            cmbTipoStatus.PreencherComSelecione(listaTipos);
            cmbStatus.ResetarComSelecione(habilitar: false);
        }

        private void ConfigurarStatusDeAtendimentoParaTelefonia()
        {
            var listaTipos = _statusDeAtendimentoService.ListarTipoDeStatusDeAtendimento(ativo: true);
            listaTipos = listaTipos?.Where(x => (TipoStatusDeOferta)x.Key == TipoStatusDeOferta.TELEFONIA).Select(x => x).ToList();
            cmbTipoStatus.PreencherComSelecione(listaTipos);
            cmbStatus.ResetarComSelecione(habilitar: false);
        }

        private void ConsultarProspect()
        {
            var usuario = _usuario;
            var consultarProspect = new ConsultaDeProspectForm(usuario);

            var resultadoDaConsulta = consultarProspect.RealizarPesquisaParaAtendimento();

            if (resultadoDaConsulta?.ProspectLocalizado == null) return;
            if (resultadoDaConsulta.IniciarContatoManual == false) return;

            var prospect = resultadoDaConsulta.ProspectLocalizado;
            IniciarAtendimento(prospect.Id, 0, numeroChamadorDiscador: "", origem: OrigemDeAtendimento.ConsultaDeCliente);
        }

        private OfertaDoAtendimento RetornarProximaOfertaElegivel()
        {
            long idAtendimento = _atendimentoEmAndamento.Id;
            int idCampanha = _campanhaAtual.Id;
            return _ofertaDoAtendimentoService.RetornarOfertaElegivelParaAtendimento(idAtendimento, idCampanha);
        }

        private bool VerificarSeOfertaFoiFinalizada(OfertaDoAtendimento oferta)
        {
            int? idStatusDaOferta = null;

            IEnumerable<OfertaDoAtendimento> ofertas = _ofertaDoAtendimentoService
                .RetornarOfertasDoAtendimento(oferta.IdAtendimento);

            OfertaDoAtendimento ofertaAtual =
                ofertas.FirstOrDefault(x => x.Id == oferta.Id && x.IdTipoDeProduto == oferta.IdTipoDeProduto);

            //if (oferta.IdTipoDeProduto == 1)
            //{
            //    var ofertaMigracao = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoClaroMigracao(oferta.Id);

            //    idStatusDaOferta = ofertaMigracao.IdStatusDaOferta;
            //}
            //else if (oferta.IdTipoDeProduto == 3)
            //{
            //    var ofertaMigracao = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoClaroRentabilizacao(oferta.Id);

            //    idStatusDaOferta = ofertaMigracao.IdStatusDaOferta;
            //}

            idStatusDaOferta = ofertaAtual?.IdStatusDaOferta;
            List<string> mensagens = new List<string>();

            if (_afterCallEncerrado) return true;

            if (idStatusDaOferta == null)
            {
                mensagens.Add("A oferta não foi registrada!");
            }
            else
            {
                var statusOferta = _statusDeOfertaService.RetornarStatusDeOferta((int)idStatusDaOferta, _campanhaAtual.Id);

                if (statusOferta == null)
                {
                    mensagens.Add("A oferta não foi registrada!");
                }
                if (statusOferta?.TipoStatus != TipoStatusDeOferta.ACEITE)
                {
                    mensagens.Add("A oferta não foi finalizada como aceite!");
                }
            }

            if (mensagens.Any())
                CallplusFormsUtil.ExibirMensagens(mensagens.ToList());

            return !mensagens.Any();

        }

        private void ResetarScriptsDaOfertaAtual()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(ResetarScriptsDaOfertaAtual));
            }
            else
            {
                _ofertaAtualDoAtendimento = null;

                if (tabInteligenciaAtendimento.InvokeRequired)
                {
                    tabInteligenciaAtendimento.Invoke(new MethodInvoker(() =>
                        tabInteligenciaAtendimento.Text = "Inteligência de Atendimento"));
                }
                else
                {
                    tabInteligenciaAtendimento.Text = "Inteligência de Atendimento";
                }

                scriptDeOfertaControl.Resetar();
                scriptDeApresentacaoControl.Resetar();
                scriptDeFinalizacaoControl.Resetar();
            }

        }

        private void GravarStatusDaOferta(OfertaDoAtendimento oferta, StatusDeOferta statusDaOferta)
        {
            _ofertaDoAtendimentoService.GravarStatusDaOfertaDoAtendimento(oferta, statusDaOferta);
        }

        private void IniciarAtendimento(long idProspect, long telefoneDaChamada, string numeroChamadorDiscador, OrigemDeAtendimento origem)
        {
            _logger.Debug($"Novo Atendimento. IdProspect: {idProspect};NumeroChamadaDiscador: {numeroChamadorDiscador};Origem: {(int)origem}");

            if (_atendimentoEmAndamento != null)
            {
                _idProspectEmBufferParaCarregamento = idProspect;
                _telefoneEmBufferParaCarregamento = telefoneDaChamada;
                _ticketEmBufferParaCarregamento = numeroChamadorDiscador;

                string idAtendimentoAtual = _atendimentoEmAndamento?.Id.ToString() ?? "sem atendimento";
                _logger.Debug($"Tentativa de atendimento com outro em andamento. IdProspect:{idProspect} - {idAtendimentoAtual}");
                return;
            }

            try
            {
                int idUsuario = _usuario.Id;
                int idSupervisor = _usuario.IdSupervisor;
                string ip = ConfiguracaoDeAmbiente.RetornarEnderecoIP();
                string hostName = ConfiguracaoDeAmbiente.HostName;
                int? idDiscador = _discadorConectado?.Id;
                _afterCallEncerrado = false;
                _prospectDoAtendimento = _prospectService.RetornarProspect(idProspect);

                Campanha campanhaDoProspect = _campanhaService.RetornarCampanha(_prospectDoAtendimento.IdCampanha);

                if (!VerificarSePodeIniciarAtendimentoPreditivo(campanhaDoProspect)) return;

                Atendimento novoAtendimento = _atendimentoService.IniciarAtendimento(idUsuario, idProspect, idSupervisor, idDiscador, numeroChamadorDiscador,
                    ip, hostName, origem);

                if (novoAtendimento != null)
                {
                    if (_idProspectEmBufferParaCarregamento == novoAtendimento.IdProspect)
                    {
                        _idProspectEmBufferParaCarregamento = 0;
                        _telefoneEmBufferParaCarregamento = 0;
                        _ticketEmBufferParaCarregamento = "";
                    }

                    ConfigurarAtendimento(novoAtendimento, origem, _usuario, campanhaDoProspect, telefoneDaChamada);
                }

                CarregarFAQ(_prospectDoAtendimento.IdCampanha);
                CarregarGridDeOfertaDoAtendimento();
                RemoverTabOferta();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao iniciar um novo atendimento preditivo", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(e);
            }
        }

        private void IniciarContagemDeAfterCall()
        {
            if (_atendimentoEmAndamento == null) return;

            AlterarLabelStatusDiscador("PAUSA TABULAÇÃO");

            cmbStatusOperador.Desabilitar();
            ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);

            _logger.Debug($"Iniciar contagem de after Call: {_timespanAfterCall.TotalSeconds} segundos");

            if (_timespanAfterCall.Ticks > 0)
            {
                _timerAferCall.Start();
                _emAfterCall = true;
            }
        }

        private void PausarContagemDeAfterCall()
        {
            _logger.Debug($"Pausa da contagem de after Call: {_timespanAfterCall.TotalSeconds} segundos");

            if (_timespanAfterCall.Ticks > 0)
            {
                _timerAferCall.Stop();
                _emAfterCall = false;
            }
        }

        private void SolicitarPausaDeAfterCall()
        {
            SolicitarPermissaoForm solicitarPermissaoForm = new SolicitarPermissaoForm(_usuario);
            var retorno = solicitarPermissaoForm.SolicitarPermissaoDeUsuario(true, true);

            if (retorno?.PermissaoConfirmada ?? false)
            {
                PausarContagemDeAftercall();
            }
        }

        private void PausarContagemDeAftercall()
        {
            _logger.Debug($"Pausa da contagem de after Call: {_timespanAfterCall.TotalSeconds} segundos");

            if (_timespanAfterCall.Ticks > 0)
            {
                _timerAferCall.Stop();
                _emAfterCall = false;
            }
        }

        private void IniciarContagemDePausa(bool progressivo, long duracaoSegundos = 0)
        {
            _contagemProgressivaPausa = progressivo;
            _logger.Debug($"Iniciar contagem pausa. Progressivo:{progressivo}, Duracao: {0}");
            var totalTicks = duracaoSegundos * TimeSpan.TicksPerSecond;
            _timespanDuracaoPausa = new TimeSpan(totalTicks);
            _timerPausa.Start();
        }

        private void LimparCamposAgendamento()
        {
            if (InvokeRequired)
                Invoke(new Action(LimparCamposAgendamento));
            else
            {
                dtAgendamento.ResetText();
                dtAgendamento.Enabled = false;
                txtHoraAgendamento.ResetText();
                txtHoraAgendamento.Enabled = false;
                cmbTelAgendamento.ResetarComSelecione(habilitar: false);
            }
        }

        private void LimparPainelSuperior()
        {
            if (InvokeRequired)
                this.Invoke(new Action(LimparPainelSuperior));
            else
            {
                scriptDeOfertaControl.Visible = false;
                scriptDeApresentacaoControl.Visible = false;
                scriptDeFinalizacaoControl.Visible = false;
            }
        }

        private void PreencherCombosIniciais()
        {
            IEnumerable<KeyValuePair<int, string>> listaVazia = new List<KeyValuePair<int, string>>();
            cmbTelefoneInteracao.PreencherComSelecione(listaVazia);
            cmbTelAgendamento.PreencherComSelecione(listaVazia);
            cmbTipoStatus.PreencherComSelecione(listaVazia);
            cmbStatusOperador.PreencherComSelecione(listaVazia);
            cmbStatus.PreencherComSelecione(listaVazia);

            if (_campanhasDoUsuario != null)
                cmbCampanha.PreencherComSelecione(_campanhasDoUsuario, campanha => campanha.Id, campanha => campanha.Nome);

            CarregarComboTipoStatusDeAtendimento();
        }

        private void CarregarComboTipoStatusDeAtendimento()
        {
            IEnumerable<KeyValuePair<int, string>> listaVazia = new List<KeyValuePair<int, string>>();
            IEnumerable<KeyValuePair<int, string>> tiposDeStatus = _statusDeAtendimentoService.ListarTipoDeStatusDeAtendimento(ativo: true);
            cmbTipoStatus.PreencherComSelecione(tiposDeStatus);
            cmbStatus.PreencherComSelecione(listaVazia);
        }

        private void RemoverTabs()
        {
            tcAtendimento.TabPages.Clear();

            tcAtendimento.TabPages.Insert(0, tcAtendimento_tpDadosProspect);
            tcAtendimento.TabPages.Insert(1, tcAtendimento_tpFaq);

            //while (tcAtendimento.TabPages.Count > 2)
            //{
            //    if (tcAtendimento.InvokeRequired)
            //    {
            //        tcAtendimento.Invoke(new MethodInvoker(() => tcAtendimento.TabPages.RemoveAt(2)));
            //    }
            //    else
            //    {
            //        tcAtendimento.TabPages.RemoveAt(2);
            //    }
            //}
        }

        private void ResetarAbaOferta_Movel()
        {
            //cmbProduto_movel.ResetarComSelecione(habilitar: true);
            //txtNumeroMigrado_movel.Resetar(habilitar: false, limparTexto: true, readOnly: false);
            //cmbDataVencimento_movel.ResetarComSelecione(habilitar: true);
            //txtPrimeiraFatura_movel.Resetar(habilitar: false, limparTexto: true, readOnly: true);
            //txtCicloFechamento_movel.Resetar(habilitar: false, limparTexto: true, readOnly: true);
            //cmbDesejaFaturaDigital_movel.ResetarComSelecione(habilitar: true);
            //cmbEndereco_oferta_movel.ResetarComSelecione(habilitar: true);
            //txtEmailFaturaDigital_movel.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            //cmbFormaDePagamento_pagamento.ResetarComSelecione(habilitar: true);
        }

        private void ResetarComboCampanha(bool habilitar)
        {
            cmbCampanha.ResetarComSelecione(habilitar: true);
        }

        private void ResetarCombosTipoEStatusDaOferta(bool habilitarTipo, bool habilitarStatus)
        {
            DefinirStatusDeOferta(-1);

            //cmbTipoStatusOferta?.ComboBox.ResetarComSelecione(habilitar: habilitarTipo);
            //cmbStatusDeOferta?.ComboBox.ResetarComSelecione(habilitar: habilitarStatus);

        }

        private void ResetarCombosTipoEStatusDeAtendimento(bool habilitarTipo, bool habilitarStatus)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    cmbTipoStatus.ResetarComSelecione(habilitar: habilitarTipo);
                    cmbStatus.ResetarComSelecione(habilitar: habilitarStatus);
                }));
            }
            else
            {
                cmbTipoStatus.ResetarComSelecione(habilitar: habilitarTipo);
                cmbStatus.ResetarComSelecione(habilitar: habilitarStatus);
            }
        }

        private void ResetarFormularioAposAtendimento()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ResetarFormularioAposAtendimento));
            }
            else
            {
                _prospectDoAtendimento = null;
                _resultadoDoAtendimentoAtual = null;
                _statusDoAtendimentoEmAndamento = null;
                _statusDeOfertaSelecionado = null;
                _atendimentoEmAndamento = null;
                _ofertaAtualDoAtendimento = null;
                _ofertasDoAtendimento.Clear();
                //_afterCallEncerrado = false;
                _pilhaDeJanelas.Clear();

                scriptDeApresentacaoControl.Resetar();
                _containerDeLayoutDinamico.LimparTodos();

                //tabInteligenciaAtendimento.Text = "Inteligência de Atendimento";
                ResetarScriptsDaOfertaAtual();
                ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: false, exibirScriptFinalizacao: false);
                ConfigurarTabResultadoDaInteracaoAposAtendimento();
                ConfigurarTabDeAtendimentoAposAtendimento();
                ConfigurarMenuStatusDeOfertaAposAtendimento();
                ConfigurarCamposMenuSuperior(habilitarConsultaProspect: true, hablitarPararTempo: false);

                if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
                    CCA_VerificarStatusAgente();
                else
                    ConfigurarLabelsInferioresAposAtendimento();

                ConfigurarMenuDeAtendimentoAposAtendimento();
                ResetarTodosOsCamposDeVenda();
                RemoverTabs();
            }
        }

        private void ConfigurarMenuDeAtendimentoAposAtendimento()
        {
            if (InvokeRequired)
                this.Invoke(new Action(ConfigurarMenuDeAtendimentoAposAtendimento));
            else
            {
                ConfigurarMenuDeAtendimento(false, false);
            }
        }

        private void ConfigurarMenuDeAtendimentoParaNovoAtendimento()
        {
            if (InvokeRequired)
                this.Invoke(new Action(ConfigurarMenuDeAtendimentoParaNovoAtendimento));
            else
            {
                ConfigurarMenuDeAtendimento(true, true);
            }
        }

        private void ResetarStatusAtendimento(TipoStatusDeAtendimento tipoStatus)
        {
            dtAgendamento.ResetText();
            dtAgendamento.Enabled = false;
            txtHoraAgendamento.ResetText();
            txtHoraAgendamento.Enabled = false;
            cmbTelAgendamento.ResetarComSelecione(habilitar: false);
            cmbStatus.ResetarComSelecione(habilitar: false);

            CarregarComboStatusDeAtendimento((int)tipoStatus);
            DefinirStatusDoAtendimento(-1);

            if (tipoStatus != TipoStatusDeAtendimento.NaoDefinido)
            {
                cmbStatus.ResetarComSelecione(habilitar: true);
            }

            if (tipoStatus == TipoStatusDeAtendimento.Agendamento)
            {
                int minutosAgendamentoPadrao = 30;
                DateTime dataAgPadrao = DateTime.Now.AddMinutes(minutosAgendamentoPadrao);
                cmbTelAgendamento.Habilitar();
                cmbTelAgendamento.SelecionarPrimeiroItemDisponivel();
                dtAgendamento.Enabled = true;
                dtAgendamento.ResetText();
                txtHoraAgendamento.Enabled = true;
                txtHoraAgendamento.Text = $"{dataAgPadrao.Hour:00}:{dataAgPadrao.Minute:00}";
            }
        }

        private void ResetarTodosOsCamposDeVenda()
        {
            //PAINEL DE ATENDIMENTO
            //ConfigurarTabDeAtendimentoAposAtendimento();

            ////INTERAÇÃO
            //ConfigurarTabResultadoDaInteracaoAposAtendimento();

            //DADOS DO PROSPECT
            if (_containerDeLayoutDinamico != null)
                _containerDeLayoutDinamico.LimparTodos();

            //ABA OFERTA/MOVEL
            ResetarAbaOferta_Movel();
        }

        private DateTime? RetornarDataDoAgendamento()
        {
            var textoData = $"{dtAgendamento.Text} {txtHoraAgendamento.Text}";
            DateTime dataDoAgendamento;
            if (DateTime.TryParse(textoData, out dataDoAgendamento))
            {
                return dataDoAgendamento;
            }

            return null;
        }

        private string RetornarMensagemDiscador(int codRetorno)
        {
            string mensagem = _discadorService.RetornarMensagemDiscador(_discadorConectado.Id, codRetorno);

            if (string.IsNullOrEmpty(mensagem))
            {
                mensagem = $"{codRetorno} - Indefinido";
            }
            else
            {
                mensagem = $"{codRetorno} - {mensagem}";
            }

            return mensagem;
        }

        private string RetornarStatusDiscador(int codigo)
        {
            string mensagem = _discadorService.RetornarStatusDoDiscador(_discadorConectado.Id, codigo);

            if (string.IsNullOrEmpty(mensagem))
            {
                mensagem = $"{codigo} - Indefinido";
            }
            else
            {
                mensagem = $"{codigo} - {mensagem}";
            }

            return mensagem;
        }

        private StatusDeAtendimento RetornarStatusDoAtendimento(int idStatus)
        {
            StatusDeAtendimento status = _statusDeAtendimentoService.RetornarStatusDoAtendimento(idStatus);
            return status;
        }

        private long RetornarTelefoneDaInteracao()
        {
            long telefone = 0;

            if (cmbTelefoneInteracao.InvokeRequired)
            {
                cmbTelefoneInteracao.BeginInvoke(new MethodInvoker(() => long.TryParse(cmbTelefoneInteracao.Text, out telefone)));
            }
            else
            {
                long.TryParse(cmbTelefoneInteracao.Text, out telefone);
            }


            return telefone;
        }

        private long RetornarTelefoneDoAgendamento()
        {
            long telefone = 0;
            long.TryParse(cmbTelAgendamento.Text, out telefone);
            return telefone;
        }

        private bool VerificarSePodeAlterarCampanha()
        {
            var mensagens = new List<string>();

            if (cmbCampanha.TextoEhSelecione())
                mensagens.Add("[Campanha] deve ser informado.");

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool VerificarSePodeAlterarStatusOperador()
        {
            var mensagens = new List<string>();
            var idStatusOperador = int.Parse(cmbStatusOperador.SelectedValue.ToString());

            if (_discadorConectado == null)
                mensagens.Add("Não existe um discador conectado ao CRM");

            if (idStatusOperador == -1)
                mensagens.Add("[Status do operador] deve ser informado");

            if (_discadorConectado.TipoDiscador == TipoDiscador.Akiva)
            {
                mensagens.AddRange(XCallAkiva_VerificarSePodeAlterarStatus(idStatusOperador));
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
            {
                bool removerPausa = cmbStatusOperador.Text == "DISPONÍVEL";

                if (lblStatusAtendimento.Text.ToUpper().Contains("TABULAÇÃO"))
                    mensagens.Add("[Status do operador] não pode ser aplicado, pois existe tabulação pendente!");
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.Olos)
            {
                //TODO:
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI)
            {
                bool removerPausa = cmbStatusOperador.Text == "DISPONÍVEL";

                if (lblStatusAtendimento.Text.ToUpper().Contains("TABULAÇÃO"))
                    mensagens.Add("[Status do operador] não pode ser aplicado, pois existe tabulação pendente!");
            }

            ExibirMensagens(mensagens);
            return !mensagens.Any();
        }

        private bool VerificarSePodeFinalizarAtendimento(ResultadoDoAtendimento resultado)
        {
            var mensagens = new List<string>();
            Atendimento atendimento = resultado?.Atendimento;
            StatusDeAtendimento status = resultado?.StatusDoAtendimento;

            if (resultado == null)
            {
                mensagens.Add("O resultado do atendimento não é válido.");
                ExibirMensagens(mensagens);
                return false;
            }

            if (atendimento == null || status == null)
            {
                if (atendimento == null)
                    mensagens.Add("Não foi possível determinar o atendimento em andamento.");

                if (status == null)
                    mensagens.Add("O Status do Atendimento não foi definido.");

                ExibirMensagens(mensagens);
                return false;
            }

            if (resultado.TabulacaoAutomatica == true)
            {
                return true;
            }

            if (resultado.Telefone <= 0)
                mensagens.Add("Telefone da Interação deve ser informado.");

            if (status.TipoDeStatus == TipoStatusDeAtendimento.Agendamento)
            {
                var msgsAgendamento = VerificarSePodeRealizarAgendamento(atendimento.Id, resultado.TelefoneAgendamento ?? 0, resultado.DataAgendamento, status.idTipoDeAgendamento);
                mensagens.AddRange(msgsAgendamento);
            }

            if (status.TipoDeStatus == TipoStatusDeAtendimento.Aceite ||
                status.TipoDeStatus == TipoStatusDeAtendimento.Recusa ||
                status.TipoDeStatus == TipoStatusDeAtendimento.Telefonia)
            {
                var msgsOferta = VerificarRegrasDeOfertaAoFinalizarContato(atendimento.Id, status);
                mensagens.AddRange(msgsOferta);
            }

            var lista = VerificarSePodeFinalizarAtendimentoDiscador(status.Id);
            mensagens.AddRange(lista);

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private List<string> VerificarRegrasDeOfertaAoFinalizarContato(long idAtendimento, StatusDeAtendimento statusDeAtendimento)
        {
            List<string> mensagens = new List<string>();
            IEnumerable<OfertaDoAtendimento> ofertasDoAtendimento = _ofertaDoAtendimentoService.RetornarOfertasDoAtendimento(idAtendimento);

            if (ofertasDoAtendimento.Any() == false)
                mensagens.Add("O Status da oferta não foi registrado para este atendimento.");

            if (ofertasDoAtendimento.Any(x => x.IdStatusDaOferta == null || x.IdStatusDaOferta == 0))
                mensagens.Add("O Status da oferta não foi registrado para este atendimento.");

            if (statusDeAtendimento != null)
            {
                bool houveAceiteDeOferta = false;
                if (statusDeAtendimento.TipoDeStatus == TipoStatusDeAtendimento.Aceite)
                {
                    foreach (OfertaDoAtendimento oferta in ofertasDoAtendimento)
                    {
                        if (oferta.IdStatusDaOferta != null && oferta.IdStatusDaOferta != 0)
                        {
                            var status = _statusDeOfertaService.RetornarStatusDeOferta(oferta.IdStatusDaOferta.Value, _campanhaAtual.Id);

                            if (status.TipoStatus == TipoStatusDeOferta.ACEITE)
                            {
                                houveAceiteDeOferta = true;
                            }
                        }
                    }

                    if (houveAceiteDeOferta == false)
                    {
                        mensagens.Add("Não houve registro Aceite para as Ofertas do atendimento.\nStatus de Aceite não permitido para a chamada.");
                    }
                }
            }


            return mensagens;
        }

        private List<string> VerificarSePodeFinalizarAtendimentoDiscador(int idStatsAtendimento)
        {
            var mensagens = new List<string>();

            if (_discadorConectado.TipoDiscador == TipoDiscador.Akiva)
            {
                //var operadorEmAtendimento = XCallAkiva_VerificarSeAgenteEstaEmAtendimento();

                //if (operadorEmAtendimento)
                //    mensagens.Add("Não é possível finalizar um atendimento com uma chamada em andamento.");

                //var tabulacaoAkiva = XCallAkiva_RetornarStatusAkiva(idStatsAtendimento);
                //if (tabulacaoAkiva == null)
                //{
                //    mensagens.Add("A tabulação informada não foi localizada no discador.");
                //}
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.CCA)
            {
                if (_integrationCca.ChamadaEmCurso)
                    mensagens.Add("Não é possível finalizar o atendimento enquanto estiver em ligação.");
            }

            if (_discadorConectado.TipoDiscador == TipoDiscador.Olos)
            {
                if (_chamadaEmCursoOlos)
                    mensagens.Add("Não é possível finalizar o atendimento enquanto estiver em ligação.");
            }

            return mensagens;

        }

        private bool VerificarSePodeIniciarAtendimentoPreditivo(Campanha campanhaDoProspect)
        {
            var mensagens = new List<string>();


            if (campanhaDoProspect == null)
            {
                mensagens.Add("A campanha do prospect não pode ser nula");
                ExibirMensagens(mensagens);
                return false;
            }

            bool usuarioPetenceACampanhaDoProspect = _campanhasDoUsuario != null && _campanhasDoUsuario.Any(x => x.Id == campanhaDoProspect.Id);

            if (!usuarioPetenceACampanhaDoProspect)
            {
                mensagens.Add("O usuário não está associado à mesma campanha do Prospect.");
            }

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private IEnumerable<string> VerificarSePodeRealizarAgendamento(long idAtendimento, long telefone, DateTime? dataAgendamento, int idTipodeAgendamento)
        {
            var mensagens = _atendimentoService.VerificarSePodeRealizarAgendamento(idAtendimento, telefone, dataAgendamento, idTipodeAgendamento);
            return mensagens;
        }

        private bool VerificarSePodeRealizarDiscagem()
        {
            var mensagens = new List<string>();

            if (_discadorConectado == null)
                mensagens.Add("Não existe discador configurado para realizar discagem manual.");

            if (cmbTelefoneInteracao.TextoEhSelecione() || string.IsNullOrEmpty(cmbTelefoneInteracao.Text))
                mensagens.Add("[Numero da Interação] deve ser informado.");

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }


        #endregion Metodos

        #region INTEGRACOES

        #region AKIVA_API

        private List<PausaAkiva> _pausasAkiva = new List<PausaAkiva>();

        private void ConfigurarXCallAkiva(Usuario usuario)
        {
            _xCallAkiva = new XCallIntegratorAkiva(usuario.Login, usuario.Login);

            //EVENTOS
            _xCallAkiva.SucessoAoRegistrarAgente += XCallAkiva_OnSucessoAoRegistrarAgente;
            _xCallAkiva.SucessoAoConectar += XCallAkiva_OnSucessoAoConectar;
            _xCallAkiva.NovaChamadaRecebida += XCallAkiva_OnNovaChamadaRecebida;
            _xCallAkiva.FimDaChamada += XCallAkiva_OnFimDaChamada;
            _xCallAkiva.MudancaNoStatusDoAgente += XCallAkiva_OnMudancaNoStatusDoAgente;
            _xCallAkiva.FalhaAoConectar += XCallAkiva_OnFalhaAoConectar;

            XCallAkiva_Registrar();
        }

        private async Task<bool> XCallAkiva_DiscarAsync(string telefone)
        {
            return await Task.Run(() =>
            {
                if (_xCallAkiva == null) return false;
                bool sucesso = true;
                _xCallAkiva.ConfigurarDiscagemManual(entrarEmPausa: true, habilitarDiscagemManual: true);
                int codRetorno = _xCallAkiva.DiscarManual(telefone);
                if (codRetorno != 1)
                {
                    sucesso = false;
                    string msg = RetornarMensagemDiscador(codRetorno);
                    ExibirMensagemDiscador(msg);
                }

                return sucesso;
            });
        }

        private void XCallAkiva_Logout()
        {
            if (_xCallAkiva == null) return;
            bool sucesso = false;
            int codRetorno = _xCallAkiva.LogOut();

            var b = _xCallAkiva.VerificarSessaoAgente();
            codRetorno = _xCallAkiva.LogOut();

            if (codRetorno != 1)
            {
                sucesso = false;
                string msg = RetornarMensagemDiscador(codRetorno);
                ExibirMensagemDiscador(msg);
            }
        }

        private void XCallAkiva_OnFalhaAoConectar(object sender, EventArgs eventArgs)
        {
            string mensagem = "falha ao conectar com o discador.";
            // AlterarLabelStatusDiscador(mensagem);
            _logger.Debug(mensagem);
        }

        private void XCallAkiva_OnFimDaChamada(object sender, EventArgs eventArgs)
        {
            string mensagem = "Chamada encerrada";
            _logger.Debug(mensagem);
            //IniciarContagemDeAfterCall();
        }

        private object lockObject = new object();

        private void XCallAkiva_OnMudancaNoStatusDoAgente(object sender, int idStatus)
        {
            lock (lockObject)
            {
                StatusAgenteAkiva statusAkiva = (StatusAgenteAkiva)idStatus;// XCallAkiva_RetornarStatusAgente();
                string statusDiscador = RetornarStatusDiscador(idStatus);
                AlterarLabelStatusDiscador(statusDiscador);
                string idAtendimentoAtual = _atendimentoEmAndamento?.Id.ToString() ?? "sem atendimento";

                _logger.Debug($"Novo Status: {statusDiscador}; AtendimenoAtual:{idAtendimentoAtual}");

                //EncerrarContagemDePausa();
                Invoke(new MethodInvoker(() => EncerrarContagemDePausa()));

                if (statusAkiva == StatusAgenteAkiva.Atendimento)
                {
                    _logger.Debug($"Iniciar Atendimento: IdProspect:{_xCallAkiva._agente.ClientId}");
                    long idProspect = long.Parse(_xCallAkiva._agente.ClientId);
                    long telefoneChamada = long.Parse(_xCallAkiva._agente.PhoneNumber);
                    string ticket = "";


                    if (long.TryParse(_xCallAkiva._agente.ClientId, out idProspect))
                    {
                        _logger.Debug($"Iniciar Atendimento: IdProspect:{idProspect}");
                        long.TryParse(_xCallAkiva._agente.PhoneNumber, out telefoneChamada);
                        IniciarAtendimento(idProspect, telefoneChamada, ticket, OrigemDeAtendimento.Preditivo);

                    }
                    else
                    {
                        string msg = "Foi recebida uma nova chamada mas o ID do Prospect não foi enviado pelo discador.";
                        _logger.Info(msg);
                    }

                }

                if (statusAkiva == StatusAgenteAkiva.PausaDefinidaPeloAgente)
                {
                    Invoke(new MethodInvoker(() => IniciarContagemDePausa(true, 0)));

                    int idPausa = XCallAkiva_RetornarIdPausaDoAgenteAsync();
                    List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();

                    PausaAkiva pausa = _pausasAkiva.FirstOrDefault(x => x.Id == idPausa);

                    if (pausa != null)
                    {
                        if (pausa.Restritiva)
                        {
                            long duracaoSegundos = 0;
                            try
                            {
                                var array = pausa.Tempo.Split(':');
                                int horas = int.Parse(array[0]);
                                int minutos = int.Parse(array[1]);
                                int segundos = int.Parse(array[2]);
                                var timespan = new TimeSpan(0, horas, minutos, segundos, 0);
                                duracaoSegundos = (long)timespan.TotalSeconds;
                            }
                            catch (Exception e)
                            {
                                _logger.Error($"Não foi possíel determinar a duração da pausa restritiva Id: {pausa.Id}, Nome: {pausa.Nome}, Tempo: {pausa.Tempo}");
                            }

                            Invoke(new MethodInvoker(() => IniciarContagemDePausa(false, duracaoSegundos)));
                            //IniciarContagemDePausa(progressivo: false, duracaoSegundos: duracaoSegundos);
                        }

                        lista.Add(new KeyValuePair<int, string>(pausa.Id, $"Pausa - {pausa.Nome}"));
                    }

                    CarregarComboStatusDoOperador(lista);
                    cmbStatusOperador.SelectedValue = idPausa.ToString();
                }

                if (statusAkiva == StatusAgenteAkiva.PausaPosAtendimentoAguardandoTabulacaoExterna)
                {
                    Invoke(new MethodInvoker(() => IniciarContagemDeAfterCall()));
                }

                if (statusAkiva == StatusAgenteAkiva.Disponivel)
                {
                    Invoke(new MethodInvoker(() => EncerrarContagemDePausa()));

                    //EncerrarContagemDePausa();
                    CarregarComboStatusDoOperador();
                    cmbStatusOperador.Text = "DISPONÍVEL";
                }
            }
        }

        private void XCallAkiva_OnNovaChamadaRecebida(object sender, DadosDaChamada dadosDaChamada)
        {
            var msgLog = $"Chamada recebida - Ticket: {dadosDaChamada.IdTicket ?? ""} - Numero: {dadosDaChamada.Telefone ?? ""} - Prospect: {dadosDaChamada.IdProspect ?? ""}";
            _logger.Info(msgLog);
            Invoke((MethodInvoker)delegate
           {
               long idProspect;
               long telefoneChamada;
               string ticket = dadosDaChamada.IdTicket;
               if (long.TryParse(dadosDaChamada.IdProspect, out idProspect))
               {
                   long.TryParse(dadosDaChamada.Telefone, out telefoneChamada);
                   IniciarAtendimento(idProspect, telefoneChamada, ticket, OrigemDeAtendimento.Preditivo);
               }
               else
               {
                   string msg = "Foi recebida uma nova chamada mas o ID do Prospect não foi enviado pelo discador.";
                   AutoClosingMessageBox.Show(msg, "Aviso do sistema", 5000);
                   _logger.Info(msg);
               }
           });
        }

        private void XCallAkiva_OnSucessoAoConectar(object sender, DadosDaChamada dadosDaChamada)
        {
            string mensagem = "sucesso ao conectar com o discador";
            ///AlterarLabelStatusDiscador(mensagem);
            var msgLog = $"{mensagem} - Ticket: {dadosDaChamada.IdTicket ?? ""} - Numero: {dadosDaChamada.Telefone ?? ""} - Prospect: {dadosDaChamada.IdProspect ?? ""}";
            _logger.Info(msgLog);
        }

        private void XCallAkiva_OnSucessoAoRegistrarAgente(object sender, EventArgs eventArgs)
        {
            string mensagem = $"conectado com o discador";
            // AlterarLabelStatusDiscador(mensagem);
            _logger.Debug(mensagem);
        }

        private bool XCallAkiva_EncerrarChamada()
        {
            //    return await Task.Run(() =>
            //    {
            //        if (_xCallAkiva == null) return false;
            //        bool sucesso = true;
            //        int codRetorno = _xCallAkiva.EncerrarChamada();

            //        if (codRetorno != 1)
            //        {
            //            sucesso = false;
            //            var msg = RetornarMensagemDiscador(codRetorno);
            //            ExibirMensagemDiscador(msg);
            //        }
            //        return sucesso;
            //    });
            return false;
        }

        private async Task<bool> XCallAkiva_PausarAsync(int idPausaAkiva)
        {
            return await Task.Run(() =>
            {
                if (_xCallAkiva == null) return false;

                bool sucesso = true;
                int codRetorno = _xCallAkiva.PausarOperador(idPausaAkiva);

                if (codRetorno != 1)
                {
                    sucesso = false;
                    var msg = RetornarMensagemDiscador(codRetorno);
                    ExibirMensagemDiscador(msg);
                }
                return sucesso;
            });
        }

        private bool XCallAkiva_Registrar()
        {
            if (_xCallAkiva == null) return false;

            bool sucesso = true;
            var retornoSessao = _xCallAkiva.VerificarSessaoAgente();
            int retornoLogout = _xCallAkiva.LogOut();
            int codRetorno = _xCallAkiva.Registrar();

            if (codRetorno != 1)
            {
                sucesso = false;
                var msg = RetornarMensagemDiscador(codRetorno);
                ExibirMensagemDiscador(msg);
            }
            return sucesso;
        }

        private StatusAgenteAkiva XCallAkiva_RetornarStatusAgente()
        {
            return _xCallAkiva.RetornarStatusAgente();
        }

        private bool XCallAkiva_VerificarSeAgenteEstaEmAtendimento()
        {
            if (_xCallAkiva == null) return false;

            var statusAgente = _xCallAkiva.RetornarStatusAgente();

            if (statusAgente == StatusAgenteAkiva.Atendimento || statusAgente == StatusAgenteAkiva.Ativo)
                return true;

            return false;

        }

        private async Task<bool> XCAllAkiva_RemoverPausaAsync()
        {
            return await Task.Run(() =>
            {
                if (_xCallAkiva == null) return false;
                bool sucesso = true;
                int codRetorno = _xCallAkiva.RetirarOperadorDaPausa();

                if (codRetorno != 1)
                {
                    sucesso = false;
                    var msg = RetornarMensagemDiscador(codRetorno);
                    ExibirMensagemDiscador(msg);
                }
                return sucesso;
            });
        }

        private async Task<bool> XCallAkiva_SetDisposition(int idDisposition, string dataAgendamento, string observacao, bool solicitouGravacao, string idExterno, int idAuditor, int rediscagem, int mailingPhoneRedial, int queueClassReason)
        {
            return await Task.Run(() =>
            {
                if (_xCallAkiva == null) return false;
                bool sucesso = true;

                int codRetorno = _xCallAkiva.InserirTabulacaoNoDiscador(idDisposition, dataAgendamento, observacao,
                    solicitouGravacao, idExterno, idAuditor, rediscagem, mailingPhoneRedial, queueClassReason);
                if (codRetorno != 1)
                {
                    sucesso = false;
                    string msg = RetornarMensagemDiscador(codRetorno);
                    ExibirMensagemDiscador(msg);
                }

                return sucesso;
            });
        }

        private int XCallAkiva_RetornarIdPausaDoAgenteAsync()
        {
            int idPausa = _xCallAkiva.RetornarIdPausaDoAgente();
            return idPausa;
        }

        private StatusAkiva XCallAkiva_RetornarStatusAkiva(int idStatus)
        {
            return _xCallAkiva.RetornarListaDeTabulacoes()?.FirstOrDefault(x => x.IdRetorno == idStatus.ToString());
        }

        private IEnumerable<StatusAkiva> XCallAkiva_RetornarListaDeTabulacoes()
        {
            return _xCallAkiva.RetornarListaDeTabulacoes();
        }

        private async Task<string> XCallAkiva_RetornarDescricaoPausaAgenteAsync()
        {
            return await Task.Run(() =>
            {
                string idPausa = _xCallAkiva.RetornarDescricaoDaPausaDoAgente();
                return idPausa;
            });
        }

        private IEnumerable<string> XCallAkiva_VerificarSePodeAlterarStatus(int idStatus)
        {
            var mensagens = new List<string>();

            var pausa = _pausasAkiva.FirstOrDefault(x => x.Id == idStatus);

            if (pausa != null)
            {
                if (!pausa.Exibir)
                    mensagens.Add("Pausa não permitida.");
            }

            return mensagens;
        }

        #endregion AKIVA_API

        #region AKIVA_SOCKET

        private static object lockObjectAkivaSocket = new object();
        private NetworkStream _netWorkStreamAkivaSocket;
        TcpListener _serverCallplusAkiva;
        private Dictionary<Guid, TcpClient> _poolDeConexoesAkivaSocket;
        private int portaServidorTcpAkivaSocket = 3125;
        private int _numeroConexoesAkivaSocket = 0;
        private bool _tabulacaoFoiEnviadaAkivaSocket;

        private void AdicionarTCPAoPool(Guid id, TcpClient client)
        {
            lock (lockObjectAkivaSocket)
            {
                _poolDeConexoesAkivaSocket.Add(id, client);
            }
        }

        private void RemoverConexaoTcpDoPool(Guid id)
        {
            lock (lockObjectAkivaSocket)
            {
                if (_poolDeConexoesAkivaSocket.ContainsKey(id))
                    _poolDeConexoesAkivaSocket.Remove(id);
            }
        }

        private void SendMessagePoolAkiva(string message)
        {
            try
            {
                lock (lockObjectAkivaSocket)
                {
                    foreach (var conjunto in _poolDeConexoesAkivaSocket)
                    {
                        var id = conjunto.Key;
                        var tcpClient = conjunto.Value;
                        _logger.Info($"Enviando mensage para {id}: {message}");

                        if (tcpClient != null && tcpClient.Connected)
                        {
                            var stream = tcpClient.GetStream();
                            WriteToStream(stream, EncodeMessageToSend(message));
                        }
                        else
                        {
                            _logger.Info($"{id} não conectado. Mensagem {message} não enviada");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

        }

        public void AcessarAkivaSocket()
        {
            // Integração socket Akiva
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress localIPsSelecionado = ConfiguracaoDeAmbiente.RetornarIP();
            _serverCallplusAkiva = new TcpListener(localIPsSelecionado, portaServidorTcpAkivaSocket);
            _poolDeConexoesAkivaSocket = new Dictionary<Guid, TcpClient>();
            _netWorkStreamAkivaSocket = null;

            //Iniciar server Socket AKIVA
            ServerCallplusAkiva_Start();
        }

        private void ServerCallplusAkiva_Start()
        {
            _serverCallplusAkiva.Start();
            ServerCallplusAkivaa_AceptConnection(); //accepts incoming connections
        }

        private void ServerCallplusAkivaa_AceptConnection()
        {
            _serverCallplusAkiva.BeginAcceptTcpClient(ServerCallplusAkiva_HandleConnection, _serverCallplusAkiva); //this is called asynchronously and will run in a different thread
        }

        private void ServerCallplusAkiva_HandleConnection(IAsyncResult result) //the parameter is a delegate, used to communicate between threads
        {
            Guid idConexao = Guid.NewGuid();
            try
            {
                ServerCallplusAkivaa_AceptConnection(); //once again, checking for any other incoming connections
                TcpClient client = _serverCallplusAkiva.EndAcceptTcpClient(result); //creates the TcpClient

                if (client.Connected)
                {
                    AdicionarTCPAoPool(idConexao, client);
                    lock (lockObjectAkivaSocket)
                    {
                        _numeroConexoesAkivaSocket = _poolDeConexoesAkivaSocket.Count;
                    }
                    _logger.Debug($"Nova conexão Socket: {idConexao} - Conexões: {_numeroConexoesAkivaSocket}");


                    AlterarLabelStatusDiscador("Conectado com o discador");
                    AlterarLabelNumeroDeConexoes(_numeroConexoesAkivaSocket);

                    while (true)
                    {
                        Thread.Sleep(1);

                        bool socketConectado = false;

                        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                        TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

                        foreach (TcpConnectionInformation c in tcpConnections)
                        {
                            TcpState stateOfConnection = c.State;

                            if (c.LocalEndPoint.Equals(client.Client.LocalEndPoint) &&
                                c.RemoteEndPoint.Equals(client.Client.RemoteEndPoint))
                            {
                                if (stateOfConnection == TcpState.Established)
                                {
                                    socketConectado = true;
                                }
                            }
                        }

                        if (socketConectado)
                        {
                            try
                            {
                                //If connection is already opened then we need to decode the incoming data
                                Byte[] bytes = new Byte[client.Available];

                                String incomingData = "";

                                if (bytes.Length != 0)
                                {
                                    _netWorkStreamAkivaSocket = client.GetStream();
                                    _netWorkStreamAkivaSocket.Read(bytes, 0, bytes.Length);
                                    incomingData = DecodeMessage(bytes, bytes.Length);
                                }

                                incomingData = incomingData.Replace("\r\n\0", "");
                                incomingData = incomingData.Replace("\r", "");
                                incomingData = incomingData.Replace("\n", "");
                                incomingData = incomingData.Replace("\0", "");
                                if (incomingData != "")
                                {
                                    AlterarLabelMensagemDiscador(mensagem: incomingData);

                                    if (incomingData.ToUpper() != "PING")
                                        _logger.Debug($"Mensagem recebida: {incomingData}");
                                }

                                if (incomingData != "")
                                {
                                    _logger.Debug($"Conexao: {idConexao}; Recebido: {incomingData}");
                                    RegistrarUltimoCodigoRecebidoDoDiscador(incomingData);

                                    if (_atendimentoEmAndamento == null)
                                    {
                                        long idProspect = 0;
                                        string ticket = "";
                                        long telefone = 0;

                                        if (incomingData.Contains("P:"))
                                        {
                                            string[] array = incomingData.Split('%');
                                            string idProspectTxt = array[0].Replace("P:", "");
                                            string telefoneTxt = "";

                                            if (array.Length >= 2)
                                                ticket = array[1].Replace("\r\n\0", "");

                                            if (array.Length >= 3)
                                                telefoneTxt = array[2].Replace("\r\n\0", "");

                                            long.TryParse(idProspectTxt, out idProspect);
                                            long.TryParse(telefoneTxt, out telefone);

                                            if (idProspect > 0)
                                            {
                                                AlterarLabelStatusDiscador("Em Atendimento");
                                                Invoke((MethodInvoker)delegate
                                               {
                                                   IniciarAtendimento(idProspect, telefone, ticket, OrigemDeAtendimento.Preditivo);
                                               });
                                            }
                                        }
                                    }

                                    else
                                    {
                                        if (incomingData.Contains("F:"))
                                        {
                                            if (_tabulacaoFoiEnviadaAkivaSocket == false)
                                            {
                                                string codigo = incomingData.Replace("F:", "");
                                                AlterarLabelStatusDiscador("Aguardando tabulação");

                                                if (codigo == "1")
                                                {
                                                    Invoke((MethodInvoker)delegate
                                                    {
                                                        IniciarContagemDeAfterCall();

                                                    });
                                                }
                                            }
                                        }

                                        if (incomingData.Contains("R:"))
                                        {
                                            string retorno = incomingData.Replace("R:", "");
                                            int idRetorno = 0;

                                            if (int.TryParse(retorno, out idRetorno))
                                            {
                                                AlterarLabelStatusDiscador($"Retorno de Tabulação: {retorno}");

                                                if (idRetorno == 1)
                                                {
                                                    _tabulacaoFoiEnviadaAkivaSocket = false;
                                                    Invoke((MethodInvoker)delegate
                                                   {
                                                       FinalizarAtendimentoCRM();

                                                   });
                                                }
                                                else
                                                {
                                                    Invoke((MethodInvoker)delegate
                                                    {
                                                        var erro = idRetorno != 52 || idRetorno != 1;

                                                        if (erro)
                                                        {
                                                            LiberarCamposDeTabulacaoAposErro();
                                                            if (idRetorno != 52)
                                                            {
                                                                string msg = "Retorno Discador:\n" + RetornarMensagemDiscador(idRetorno);
                                                                MessageBox.Show(msg, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                            }
                                                        }

                                                    });
                                                }

                                                if (idRetorno == 1)
                                                {
                                                    client.Close();
                                                    Thread.Sleep(10);
                                                    break;
                                                }
                                                else
                                                {
                                                    _tabulacaoFoiEnviadaAkivaSocket = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Fatal($"Conexao: {idConexao};\nErro: {ex.Message} - StackTrace:{ex.StackTrace}");
                                client.Close();
                                Thread.Sleep(10);
                                break;
                            }
                        }
                        else
                        {
                            _logger.Info($"Conexao: {idConexao}; Encerrando; Conexoes: {_numeroConexoesAkivaSocket}");
                            client.Close();
                            Thread.Sleep(10);
                            break;
                        }
                    }
                }
                else
                {
                    _logger.Debug($"Conexão: {idConexao} - Não conectada - Conexões: {_numeroConexoesAkivaSocket}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Ocorreu um erro inesperado ao receber uma nova conexão Akiva: {ex.Message}");
            }
            finally
            {
                try
                {
                    RemoverConexaoTcpDoPool(idConexao);
                    lock (lockObject)
                    {
                        _numeroConexoesAkivaSocket = _poolDeConexoesAkivaSocket.Count;
                    }

                    _logger.Debug($"Conexao Akiva Encerrada: {idConexao}; Conexões: {_numeroConexoesAkivaSocket}");
                    AlterarLabelNumeroDeConexoes(_numeroConexoesAkivaSocket);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    _logger.Fatal($"Erro com a a Conexão Akiva: {idConexao};\nErro: {ex.Message}\n{ex.StackTrace}");
                    AlterarLabelStatusDiscador("Erro Inesperado");
                }
            }
        }

        private void RegistrarUltimoCodigoRecebidoDoDiscador(string codigo)
        {
            if (codigo.ToUpper() != "PING")
            {
                //_ultimoCodigoValidoRecebidoDoDiscador = codigo;
            }
        }

        private static void WriteToStream(NetworkStream stream, Byte[] bytes)
        {
            try
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0}", ex.Message));
            }
        }

        private static byte[] EncodeMessageToSend(string message)
        {
            return System.Text.Encoding.ASCII.GetBytes(message);
        }

        private static String DecodeMessage(Byte[] bytes, int iRx)
        {
            var chars = new char[iRx + 1];
            var d = Encoding.UTF8.GetDecoder();
            d.GetChars(bytes, 0, iRx, chars, 0);
            var szData = new string(chars);

            return szData;
        }

        #endregion

        #region OLOS_SOCKET

        private bool _tabulacaoFoiEnviadaOlosSocket;
        private bool _chamadaEmCursoOlos;

        private void Client_OnSocketInformation(SimpleSocket simpleSocket, string message)
        {
            //MessageBox.Show(message);
        }

        private void Client_OnDisconnect(SimpleClientSocket simpleClientSocket)
        {
            //MessageBox.Show("Desconectou...");     

            if (InvokeRequired)
            {
                BeginInvoke(new TCPTerminal_DisconnectDel(Client_OnDisconnect), simpleClientSocket);
                return;
            }
            else
            {
                AlterarLabelStatusDiscador("DESCONECTADO");
            }
        }

        private void AcessarOlos()
        {
            client = new SimpleClientSocket();

            client.OnConnect += Client_OnConnect;
            client.OnDisconnect += Client_OnDisconnect;
            client.OnRead += Client_OnRead;
            client.OnSocketError += Client_OnSocketError;
            client.OnSocketInformation += Client_OnSocketInformation;

            client.Host = "127.0.0.1";
            client.Port = 55578;
            client.Open();
        }

        private bool Olos_Discar(string telefone)
        {
            bool sucesso = true;

            if (client != null && client.Connected)
            {
                string comando = $"[2:{telefone}]\n";
                Olos_EnviarComando(comando);
            }
            else
            {
                sucesso = false;
            }
            return sucesso;
        }

        private bool Olos_EncerrarChamada()
        {
            bool sucesso = true;
            if (client != null && client.Connected)
            {
                string comando = $"[18]\n";
                Olos_EnviarComando(comando);
            }
            else
            {
                sucesso = false;
            }
            return sucesso;
        }

        private void Olos_EnviarComando(string mensagem)
        {
            _logger.Info($"Mensagem Enviada para o discador Olos: {mensagem}");
            byte[] mensagemBytes = ConvertStrToBytes(mensagem);
            client?.SendBytes(mensagemBytes, mensagemBytes.Length);
        }

        private void AcessarOlosApi()
        {
            string login = _usuario.Login;
            string password = _usuario.Login;
            AcessarOlosWebService(login, password);
            MyTimer.Interval = (1000);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);

            ConfigurarEventosOlos();
        }

        private void Olos_RealizarLogOff()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    string mensgagemLogout = "[99]\n";
                    byte[] msgBytes = ConvertStrToBytes(mensgagemLogout);
                    client?.SendBytes(msgBytes, msgBytes.Length);
                    _logger.Info($"Menssgem enviada para o Olos: {mensgagemLogout}");
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Erro ao Realizar LogOff no discador Olos", null, e);
                _logger.Error(e);
            }

        }

        private void Client_OnSocketError(SimpleSocket simpleSocket, string errorDescription, int socketError)
        {
            _logger.Error($"Erro Socket Olos. Error Description: {errorDescription}; Socket Error: {socketError}");
            MessageBox.Show("Erro ao conectar ao Toolset Olos: " + errorDescription, "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Client_OnRead(SimpleClientSocket simpleClientSocket, byte[] bytesReceived, int bytesLength)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new TCPTerminal_MessageReceivedDel(Client_OnRead), simpleClientSocket, bytesReceived, bytesLength);
                    return;
                }
                else
                {
                    string mensagem = ConvertBytesToString(bytesReceived, bytesLength);

                    _logger.Debug($"Socket Olos. Mensagem recebida: {mensagem}");


                    if (mensagem.Substring(0, 5) == "[100:")
                    {
                        string msg = mensagem.Replace("[", "").Replace("]", "");

                        string[] msgArray = msg.Split(':');

                        try
                        {
                            long idProspect = 0;

                            if (long.TryParse(msgArray[4], out idProspect))
                            {
                                if (idProspect > 0)
                                {
                                    _tabulacaoFoiEnviadaOlosSocket = false;
                                    _chamadaEmCursoOlos = true;

                                    AlterarLabelStatusDiscador("Em Atendimento");

                                    string ticket = msgArray[2];

                                    long telefone = 0;
                                    long.TryParse(msgArray[5], out telefone);

                                    Invoke((MethodInvoker)delegate
                                    {
                                        IniciarAtendimento(idProspect, telefone, ticket, OrigemDeAtendimento.Preditivo);
                                    });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex);
                        }
                    }
                    else if (mensagem.Contains("[13]"))
                    {
                        if (_tabulacaoFoiEnviadaOlosSocket == false)
                        {
                            AlterarLabelStatusDiscador("Aguardando tabulação");

                            _chamadaEmCursoOlos = false;

                            Invoke((MethodInvoker)delegate
                            {
                                IniciarContagemDeAfterCall();
                            });
                        }
                    }

                    else if (mensagem.Contains("[101:"))
                    {
                        string msg = mensagem.Replace("[", "").Replace("]", "");

                        string[] msgArray = msg.Split(':');

                        if (msgArray.Length > 2)
                        {
                            if (msgArray[1] == "25")
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    _timerAferCall.Stop();
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                throw ex;
            }
        }

        private void Client_OnConnect(SimpleClientSocket simpleClientSocket)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new TCPTerminal_ConnectDel(Client_OnConnect), simpleClientSocket);
                return;
            }
            else
            {
                AlterarLabelStatusDiscador("CONECTADO");

                byte[] login = ConvertStrToBytes("[10:" + _usuario.Login + ":" + _usuario.Login + "]\n");

                client.SendBytes(login, login.Length);
            }
        }

        private byte[] ConvertStrToBytes(string p_mes)
        {
            return System.Text.ASCIIEncoding.ASCII.GetBytes(p_mes.Replace("\0xFF", "\0xFF\0xFF"));
        }

        private string ConvertBytesToString(byte[] bytes, int iRx)
        {
            char[] chars = new char[iRx + 1];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            d.GetChars(bytes, 0, iRx, chars, 0);
            string szData = new string(chars);

            return szData;
        }

        #endregion OLOS_SOCKET

        #region CCA
        private int idPausaAgendamentoCCA = 208;
        private int idPausaTabulacaoCCA = 200;

        private void CCA_OnChangePause(object sender, WebSocketHMB.Events.ChangePause.MudancaDePausa e)
        {
            var novaPausa = e.NovaPausa;
            var pausaAnterior = e.PausaAnterior;

            _ccaEmPausa = true;
            Invoke(new MethodInvoker(() =>
            {
                int idPausa = e.NovaPausa;
                var pausa = _ccaListaPausas?.Where(x => x.Key == e.NovaPausa).FirstOrDefault().Value;

                var mensagem = $"{pausa}";

                if (mensagem == "")
                    mensagem = $"PAUSA {novaPausa}";

                AlterarLabelStatusDiscador(mensagem);
                AlterarLabelStatusProgramado(pendente: false);

                EncerrarContagemDePausa();


                //if (novaPausa != idPausaAgendamentoCCA)
                //{
                //    IniciarContagemDePausa(true, 0);
                //}

                if (idPausa == idPausaAgendamentoCCA)
                {
                    cmbStatusOperador.Desabilitar();
                    ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
                }

            }));
        }

        private void CCA_OnUnpause(object sender, WebSocketHMB.Events.ChangePause.Unpause e)
        {
            _ccaEmPausa = false;
            Invoke(new MethodInvoker(() =>
            {
                EncerrarContagemDePausa();
                AlterarLabelStatusDiscador("DISPONÍVEL");
                AlterarLabelStatusProgramado(pendente: false);

                cmbStatusOperador.Habilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);

            }));
        }

        private void CCA_OnPause(object sender, WebSocketHMB.Events.ChangePause.Pause e)
        {
            if (_emAfterCall) return;


            _ccaEmPausa = true;
            Invoke(new MethodInvoker(() =>
            {
                var pausa = _ccaListaPausas?.Where(x => x.Key == e.IdDaPausa).FirstOrDefault().Value;
                int idPausa = e.IdDaPausa;

                var mensagem = $"{pausa}";

                if (mensagem == "")
                    mensagem = $"PAUSA {idPausa}";

                AlterarLabelStatusDiscador(mensagem);
                AlterarLabelStatusProgramado(pendente: false);
                EncerrarContagemDePausa();

                if (idPausa != idPausaTabulacaoCCA)
                {
                    IniciarContagemDePausa(true, 0);
                }

                //TODO:MOVER PARA UM METODO SEPARADO
                try
                {
                    cmbStatusOperador.SelectedItem = e.IdDaPausa.ToString();
                }
                catch (Exception exception)
                {
                    _logger.Error(exception);
                    cmbStatusOperador.ResetarComSelecione(habilitar: true);
                }

                cmbStatusOperador.Habilitar();
                ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);

                if (idPausa == idPausaAgendamentoCCA || idPausa == idPausaTabulacaoCCA)
                {
                    cmbStatusOperador.Desabilitar();
                    ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
                }

            }));
        }

        private void CCA_Unlock()
        {
            _integrationCca.Unlock(_usuario.Login);
        }

        private void CCA_EndContact()
        {
            _integrationCca.EndContact();
        }

        private void CCA_OnHangupCall(object sender, WebSocketHMB.Events.ReceivedCall.HangUp e)
        {
            var canalDesligado = false;
            long idProspect;

            //TODO:Comparar o prospect do hangup com o prospect em tela. Só fazer ação se for o mesmo.

            if (long.TryParse(e.message, out idProspect))
            {
                if (_prospectDoAtendimento != null)
                {
                    IniciarContagemDeAfterCall();
                    ConfigurarBotoesDeDiscagem(false, false);
                }
            }
            else
            {
                canalDesligado = true;
            }
        }

        private void CCA_OnReceivedCall(object sender, WebSocketHMB.Events.ReceivedCall.DadosDaLigacao e)
        {
            var msgLog = $"Chamada recebida: Ticket: {e.IdDaLigacao}; Numero: {e.NumeroTelefone}; Prospect: {e.IdDoProspect}";
            _logger.Info(msgLog);

            Invoke((MethodInvoker)delegate
            {
                var idProspect = e.IdDoProspect;
                var telefoneChamada = e.NumeroTelefone;
                var ticket = e.IdDaLigacao;

                if (idProspect > 0)
                {
                    IniciarAtendimento(idProspect, telefoneChamada, ticket, OrigemDeAtendimento.Preditivo);
                    var mensagem = "Em atendimento";
                    AlterarLabelStatusDiscador(mensagem);
                    ConfigurarBotoesDeDiscagem(false, true);
                }
                else
                {
                    var msg = "Foi recebida uma nova chamada mas o ID do Prospect não foi enviado pelo discador.";
                    _logger.Info(msg);

                    AutoClosingMessageBox.Show(msg, "Aviso do Sistema", 5000);
                }
            });
        }

        private void CCA_OnUnlocked(object sender, WebSocketHMB.Events.Unlock e)
        {
            _ccaLocked = false;
        }

        private void CCA_OnLocked(object sender, WebSocketHMB.Events.Lock e)
        {
            _ccaLocked = true;
        }

        private void CCA_OnLogoff(object sender, WebSocketHMB.Events.Logoff e)
        {
            var mensagem = "Desconectado";
            AlterarLabelStatusDiscador(mensagem);
            ConfigurarTelaAoReceberLogoff();

            MessageBox.Show("Foi realizado o Logoff no discador. Finalize o CRM.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void CCA_OnLogin(object sender, WebSocketHMB.Events.Login e)
        {
            var mensagem = !string.IsNullOrEmpty(e.LoginAgente) ? "Disponível" : "Não Conectado";

            AlterarLabelStatusDiscador(mensagem);
        }

        public void CCA_FinalizarContrato(long idProspect, string operador, string reason, string idLigacao)
        {
            _integrationCca.Finalize(Convert.ToString(idProspect), operador, reason, idLigacao);
        }

        public void CCA_TabularContrato(long idProspect, string operador, string reason, string idLigacao)
        {
            _integrationCca.TabularContrato(Convert.ToString(idProspect), operador, reason, idLigacao);
        }

        public void CCA_FinalizarTelefone(long idProspect, string operador, string number, string reason, string idLigacao)
        {
            _integrationCca.FinalizeNumber(Convert.ToString(idProspect), operador, number, reason, idLigacao);
        }

        public void CCA_AgendarTelefone(long idProspect, string login, long number, string date)
        {
            _integrationCca.ScheduleCall(idProspect, login, Convert.ToString(number), date);
        }

        public void CCA_DiscarAgendamento(long idProspect, string login)
        {
            //_integrationCca.StartScheduling(idProspect, login);
        }

        private void CCA_RemoverPausa(int idStatusOperador)
        {
            AlterarLabelStatusProgramado(pendente: true);
            _integrationCca.Unpause(_usuario.Login, idStatusOperador);
        }

        private void CCA_Pausar(int idStatusOperador, string nomePausa)
        {
            _logger.Info("Entrando em pausa:");

            _integrationCca.Pause(_usuario.Login, idStatusOperador);
            AlterarLabelStatusProgramado(pendente: true);
        }


        private void CCA_Discar(string telefone, long idProspect)
        {
            _integrationCca.PowerDial(_usuario.Login, telefone, Convert.ToString(idProspect));
        }

        private void CCA_DesligarLigacao()
        {
            if (_integrationCca.ChamadaEmCurso)
            {
                _integrationCca.Hangup(_usuario.Login, _ccaRamal);
                IniciarContagemDeAfterCall();
            }
        }

        private void CCA_Login()
        {
            var clienteCca1 = new Integration();

            clienteCca1.Authentication();

            var propriedades = clienteCca1.GetAgentProperties(_usuario.Login).message[0];
            var ramal = propriedades.peer.ToString();
            var pbx = propriedades.pbx;
            var hub = "wss://" + propriedades.hub + ":4443";

            clienteCca1.Close();
            //clienteCca1.Dispose();
            clienteCca1 = null;

            //PARA TESTES
            //ramal = "3000";
            //pbx = "192.168.15.222";
            //hub = "wss://192.168.15.61:4443";

            _integrationCca = new Integration(hub);

            _ccaRamal = ramal;
            tsRamalCCA.Text = "Ramal: " + _ccaRamal;
            _integrationCca.sipAccountUsername = _ccaRamal;
            _integrationCca.sipAccountIds = _ccaRamal;
            _integrationCca.sipAccountAddresses = pbx;

            _integrationCca.InicializarSoftPhone();
            Thread.Sleep(2000);

            ConfigurarEventosCCA();

            // _integrationCca.Open();
            _integrationCca.Authentication();
        }

        private void ConectarCCA()
        {
            var clienteCca1 = new Integration();
            var clienteCcaPrincipal = new Integration();

            clienteCca1.Authentication();

            var propriedades = clienteCca1.GetAgentProperties(_usuario.Login).message[0];
            var ramal = propriedades.peer.ToString();
            var pbx = propriedades.pbx;
            var hub = "wss://" + propriedades.hub + ":4443";

            clienteCca1.Close();
            //clienteCca1.Dispose();
            clienteCca1 = null;

            clienteCcaPrincipal = new Integration(hub);

            _ccaRamal = ramal;
            tsRamalCCA.Text = "Ramal: " + _ccaRamal;
            clienteCcaPrincipal.sipAccountUsername = _ccaRamal;
            clienteCcaPrincipal.sipAccountIds = _ccaRamal;
            clienteCcaPrincipal.sipAccountAddresses = pbx;

            clienteCcaPrincipal.InicializarSoftPhone();
            Thread.Sleep(2000);

            ConfigurarEventosCCA();

            // _integrationCca.Open();
            clienteCcaPrincipal.Authentication();
        }

        private void CCA_VerificarStatusAgente()
        {
            _integrationCca.CheckAgentPauseEvent(_usuario.Login);
        }

        private void ConfigurarEventosCCA()
        {
            _integrationCca.OnLogin += CCA_OnLogin;
            _integrationCca.OnLogoff += CCA_OnLogoff;
            _integrationCca.OnReceivedCall += CCA_OnReceivedCall;
            _integrationCca.OnHangUpCall += CCA_OnHangupCall;
            _integrationCca.OnPause += CCA_OnPause;
            _integrationCca.OnUnpause += CCA_OnUnpause;
            _integrationCca.OnChangePause += CCA_OnChangePause;
            _integrationCca.OnLocked += CCA_OnLocked;
            _integrationCca.OnUnlocked += CCA_OnUnlocked;
            _integrationCca.OnStartSchedulingCall += CCA_OnStartSchedulingCall;
            _integrationCca.OnStartSchedulingAction += CCA_OnStartSchedulingAction;

            _integrationCca.OnAuthentication += CCA_OnAuthentication;
            _integrationCca.OnPowerDial += CCA_OnPowerDial;
            _integrationCca.OnScheduling += CCA_OnScheduling;
            _integrationCca.OnUpdateContact += CCA_OnUpdateContact;
            _integrationCca.OnFinalizeContact += CCA_OnFinalizeContact;
            _integrationCca.OnListPauseReturn += CCA_OnListPauseReturn;
            _integrationCca.OnStatusAgentReceived += CCA_OnStatusAgentReceived;

            _integrationCca.OnIntegrationFailure += CCA_OnIntegrationFailure;
            _integrationCca.OnConnectionClose += CCA_OnConnectionClose;
        }

        private void CCA_OnConnectionClose(object sender, string e)
        {
            //_logger.Info("Conexão com o CCA fechada. Tentativa de Reconexão.");

            //_integrationCca.Reconectar();
            //_integrationCca.OnAuthentication -= CCA_OnAuthentication;

            //_integrationCca.OnAuthentication += (o, authentication) =>
            //{
            //    _integrationCca.OnAuthentication += CCA_OnAuthentication;
            //    _logger.Debug("Reconectado");
            //};

            //_integrationCca.Authentication();
        }

        private void CCA_OnFinalizeContact(object sender, TabularContrato e)
        {
            var sucesso = e.reason == "success" ? true : false;
            var motivo = e.message;

            if (sucesso)
            {
                if (!_integrationCca.ChamadaEmCurso)
                {
                    FinalizarAtendimentoCRM();
                    CCA_EndContact();
                }
                else
                {
                    _ccaTabulacaoEnviada = true;
                }
            }
            else
            {
                MessageBox.Show($"Não foi finalizar contato no discador. Motivo: {motivo}", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfigurarMenuDeAtendimento(true, false);
            }
        }

        private void CCA_OnStartSchedulingCall(object sender, EventArgs eventArgs)
        {
            AlterarLabelStatusDiscador("DISCAGEM AGENDAMENTO");

            cmbStatusOperador.Habilitar();
            ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: true);
        }

        private void CCA_OnIntegrationFailure(object sender, string message)
        {
            MessageBox.Show($"Ocorreu uma falha no discador CCA. Mensagem: {message}", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CCA_OnStatusAgentReceived(object sender, WebSocketHMB.Return.AgentData.AgentData e)
        {
            var status = e.message[0];

            AlterarLabelStatusProgramado(pendente: false);

            if (status.paused == 1)
            {
                var pausa = _ccaListaPausas?.Where(x => x.Key == status.pauseid).FirstOrDefault().Value;
                int idPausa = status.pauseid ?? 0;

                var mensagem = $"{pausa}";

                if (mensagem == "")
                    mensagem = $"PAUSA {status.pauseid}";

                AlterarLabelStatusDiscador(mensagem);
                ContarTempoDuracaoDePausa(true);

                if (idPausa == idPausaAgendamentoCCA || idPausa == idPausaTabulacaoCCA)
                {
                    cmbStatusOperador.Desabilitar();
                    ConfigurarPictureBoxAplicar(pctAplicarStatusOperador, habilitar: false);
                }

            }
            else
            {
                if (status.logged == 1)
                    AlterarLabelStatusDiscador("DISPONÍVEL");
                else
                    AlterarLabelStatusDiscador("NÃO CONECTADO");
            }
        }

        private void CCA_OnStartSchedulingAction(object sender, StartSchedulingAction startSchedulingAction)
        {

            var sucesso = startSchedulingAction.reason == "success" ? true : false;
            var mensagem = startSchedulingAction?.message ?? "";

            if (sucesso == false)
            {
                var msg = $"Não foi possível realizar a discagem do Agendamento.\nMotivo: {mensagem}";
                _logger.Info(msg);

                if (_atendimentoEmAndamento != null)
                {
                    IniciarContagemDeAfterCall();
                }
                MessageBox.Show(msg, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void CCA_OnListPauseReturn(object sender, WebSocketHMB.Return.ListPause.ListPause e)
        {
            if (e != null)
            {
                var listaStatus = new List<KeyValuePair<int, string>>();

                var ListaCCa = e.message?.
                    Where(x => x.visible == true)
                    .Select(x => new KeyValuePair<int, string>(x.id, $"Pausa {x.name}"
                        .ToUpper()))
                    .ToList();

                var todasAsPausas = e.message?
                    .Select(x => new KeyValuePair<int, string>(x.id, $"Pausa {x.name}"
                        .ToUpper()))
                    .ToList();

                if (ListaCCa != null)
                {
                    var pausaDiscador = new KeyValuePair<int, string>(99, $"Pausa Discador".ToUpper());
                    todasAsPausas.Add(pausaDiscador);

                    var pausaAlmoco = new KeyValuePair<int, string>(207, $"Pausa Almoço".ToUpper());
                    todasAsPausas.Add(pausaAlmoco);

                    var pausaAgendamento = new KeyValuePair<int, string>(208, $"Pausa Agendamento".ToUpper());
                    todasAsPausas.Add(pausaAgendamento);

                    var pausaTabulacao = new KeyValuePair<int, string>(200, $"Pausa Tabulação".ToUpper());
                    todasAsPausas.Add(pausaTabulacao);

                    listaStatus.AddRange(ListaCCa);
                    _ccaListaPausas = todasAsPausas;
                }

                listaStatus.AddRange(_listaStatusOperadorPadrao);
                cmbStatusOperador.PreencherComSelecione(listaStatus);
            }
        }

        private void CCA_OnUpdateContact(object sender, WebSocketHMB.Return.TabularContrato.TabularContrato e)
        {
            var sucesso = e.reason == "success" ? true : false;
            var motivo = e.message;

            if (sucesso)
            {
                if (!_integrationCca.ChamadaEmCurso)
                {
                    FinalizarAtendimentoCRM();
                    CCA_EndContact();
                }
                else
                {
                    _ccaTabulacaoEnviada = true;
                }
            }
            else
            {
                MessageBox.Show($"Não foi possível atualizar o contato no discador. Motivo: {motivo}", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfigurarMenuDeAtendimento(true, false);
            }
        }

        private void CCA_OnScheduling(object sender, WebSocketHMB.Return.Scheduling e)
        {
            var agendamento = e.reason == "success" ? true : false;
            var motivo = e.message;

            var sucesso = e.reason == "success" ? true : false;

            if (sucesso)
            {
                if (_resultadoDoAtendimentoAtual != null)
                {
                    string msg = "Sucesso ao realizar agendamento no CCA. Enviando tabulação.";
                    _logger.Debug(msg);
                    var resultado = _resultadoDoAtendimentoAtual;
                    long idProspect = resultado.Atendimento.IdProspect;
                    string operador = _usuario.Login;

                    CCA_TabularContrato(idProspect, operador, resultado.StatusDoAtendimento.Nome, resultado.Atendimento.NumeroChamadaDiscador);
                }

                //FinalizarAtendimentoCRM();
                ////if (_ccaLocked)
                //CCA_Unlock();
            }
            else
            {
                string msg = $"Não foi possível realizar o agendamento no discador. Motivo: {motivo}";
                _logger.Error(msg);
                MessageBox.Show(msg, "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfigurarMenuDeAtendimento(true, false);
            }
        }

        private void CCA_OnPowerDial(object sender, WebSocketHMB.Return.PowerDial e)
        {
            var sucessoDiscagem = e.reason == "success" ? true : false;

            var habilitarConectar = !sucessoDiscagem;
            ConfigurarBotoesDeDiscagem(habilitarConectar: habilitarConectar, habilitarDesconectar: !habilitarConectar);
        }

        private void CCA_OnAuthentication(object sender, WebSocketHMB.Return.Authentication e)
        {
            var autenticacao = e.reason == "success" ? true : false;

            if (!autenticacao) return;

            _integrationCca.Logoff(_usuario.Login);
            Thread.Sleep(1000);

            _integrationCca.Login(_usuario.Login, _ccaRamal);
            CarregarComboStatusDoOperador();
        }
        #endregion CCA

        #endregion INTEGRACOES

        #region EVENTOS

        private void AtendimentoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DesconectarDiscador(_discadorConectado);
            }
            catch (Exception exception)
            {
                // MessageBox.Show("Ocorreu um erro inesperado ao finalizar a tela.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }
        }

        private void AtendimentoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (true)
            {
                if (Char.IsLower(e.KeyChar))
                    e.KeyChar = Char.ToUpper(e.KeyChar);

                if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
                {
                    e.Handled = true;
                }
            }
        }

        private void AtendimentoForm_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarEventos();
                ConfigurarDiscador(_discadorConectado, _usuario);
                CarregarFormulario();
                CarregarNotificacao();

                CarregarComboStatusDoOperador();
                AtualizarDadosDoRankingDoOperador();
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                MessageBox.Show($"Ocorreu um erro inesperado. Entre em contato com a T.I.\n Erro{exception.Message}\nStacktrace:{exception.StackTrace}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void RemoverTabOferta()
        {
            tcAtendimento.TabPages.Remove(tcAtendimento_tpOferta);
        }

        private void AtendimentoForm_ResizeEnd(object sender, EventArgs e)
        {
            Screen telaAtual = Screen.FromControl(this);
            Rectangle area = telaAtual.WorkingArea;
            if (area.Width <= 1024)
            {
                splitContainer1.SplitterDistance = 939;
            }

            if (area.Width > 1024)
            {
                splitContainer1.SplitterDistance = 1025;
            }
        }

        private void btnFinalizarAtendimento_Click(object sender, EventArgs e)
        {
            try
            {
                FinalizarAtendimento();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao tentar finalizar o atendimento.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Fatal(exception);
            }
        }

        private void btnAgendamentoCincoMinutos_Click(object sender, EventArgs e)
        {
            try
            {
                ExecutarAgendamentoAutomatico();
            }

            catch (Exception exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao tentar executar a operação de agendamento 5 minutos.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Fatal(exception);
            }
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            btnOcultar.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            splitContainerPrincipal.Panel1Collapsed = !splitContainerPrincipal.Panel1Collapsed;
        }

        private void btnScriptAtendimento_Click(object sender, EventArgs e)
        {
            _exibindoScript = !_exibindoScript;
            btnExibirScript.Checked = _exibindoScript;

            bool exibirScript = scriptDeApresentacaoControl.Visible == false;
            ConfigurarTabPageSuperior(exibirScriptAtendimento: false, exibirScriptOferta: exibirScript, exibirScriptFinalizacao: false);

            //Teste();
            //AbrirProspect(10000);
            //TesteScriptAtendimento();
        }

        private void cmbTipoStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int idTipoStatus = int.Parse(cmbTipoStatus.SelectedValue?.ToString() ?? "-1");
                TipoStatusDeAtendimento tipoStatus = (TipoStatusDeAtendimento)idTipoStatus;
                ResetarStatusAtendimento(tipoStatus);
            }
            catch (Exception exception)
            {

                MessageBox.Show($"Ocorreu um erro inesperado ao configurar os Status de atendimento.\n Erro:{exception.Message}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }

        }

        private void cmbTabulacaoPrincipal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int idStatusSelecionado = -1;
                int.TryParse(cmbStatus.SelectedValue.ToString(), out idStatusSelecionado);
                DefinirStatusDoAtendimento(idStatusSelecionado);
            }
            catch (Exception exception)
            {

                MessageBox.Show($"Ocorreu um erro inesperado ao configurar os Status do atendimento.\n Erro:{exception.Message}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }


        }

        private void linkRanking_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var formRanking = new RankingOperadorForm();

            formRanking.ExibirRanking(_campanhaAtual.Id);
        }

        private void OnFimDoAfterCall(object sender, EventArgs eventArgs)
        {
            _afterCallEncerrado = true;

            try
            {
                ExecutarTabulacaoAutomatica();
            }
            catch (Exception e)
            {
                var msg = $"Ocorreu um erro inesperado ao tentar realizar a tabulação automática";
                MessageBox.Show(msg);

                _logger.Fatal(e);
            }
        }

        private void pctAplicarCampanha_Click(object sender, EventArgs e)
        {
            try
            {
                AplicarCampanhaSelecionadaNoCombo();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao aplicar a campanha Selecionada.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error($"Erro: {exception.Message}\nStacktrace: {exception.StackTrace})");
            }
        }

        private void pctAplicarCampanha_MouseEnter(object sender, EventArgs e)
        {
            pctAplicarCampanha.BackColor = Color.PowderBlue;

        }

        private void pctAplicarCampanha_MouseLeave(object sender, EventArgs e)
        {
            pctAplicarCampanha.BackColor = Color.Transparent;
        }

        private void pctAplicarStatusOperador_Click(object sender, EventArgs e)
        {
            try
            {
                AlterarStatusDoOperadorAsync();
            }
            catch (Exception exception)
            {
                cmbStatusOperador.ResetarComSelecione(habilitar: true);
                cmbStatusOperador.Habilitar();

                MessageBox.Show("Ocorreu um erro inesperado ao tentar alterar o Status do operador.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }
        }

        private void pctAplicarStatusOperador_MouseEnter(object sender, EventArgs e)
        {
            pctAplicarStatusOperador.BackColor = Color.PowderBlue;
        }

        private void pctAplicarStatusOperador_MouseLeave(object sender, EventArgs e)
        {
            pctAplicarStatusOperador.BackColor = Color.Transparent;
        }

        private void pctConectar_Click(object sender, EventArgs e)
        {
            try
            {
                DiscarManual();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao tentar realizar a discagem manual.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }


        }

        private void pctConectar_MouseEnter(object sender, EventArgs e)
        {
            pctConectar.BackColor = Color.PowderBlue;
        }

        private void pctConectar_MouseLeave(object sender, EventArgs e)
        {
            pctConectar.BackColor = Color.Transparent;
        }

        private void pctDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                EncerrarDiscagemManual();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ocorreu um erro inesperado ao tentar finalizar a discagem manual.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }
        }

        private void pctDesconectar_MouseEnter(object sender, EventArgs e)
        {
            pctDesconectar.BackColor = Color.PowderBlue;
        }

        private void pctDesconectar_MouseLeave(object sender, EventArgs e)
        {
            pctDesconectar.BackColor = Color.Transparent;
        }

        private void ScriptDeApresentacaoControl_OnProximaEtapaClick(object sender, EtapaChangedEventArgs etapaChangedEventArgs)
        {
        }

        private void ScriptDeApresentacaoControl_OnVoltarEtapaClick(object sender, EtapaChangedEventArgs etapaChangedEventArgs)
        {
        }

        private void ScriptDeApresentacaoControl_OnApresentarOfertaClick(object sender, EventArgs eventArgs)
        {
            var oferta = RetornarProximaOfertaElegivel();
            ConfigurarProximaOfertaElegivel(oferta);
        }

        private void ScriptDeOfertaControl_OnFinalizarScript(StatusDeOferta statusDaOferta)
        {
            FinalizarScriptDeOferta(statusDaOferta);
        }

        private void timerAfterCall_Tick(object sender, EventArgs e)
        {
            ContarTempoAfterCall();
        }

        private void timerDuracaoPausa_Tick(object sender, ElapsedEventArgs e)
        {
            ContarTempoDuracaoDePausa(_contagemProgressivaPausa);
        }

        #endregion EVENTOS

        private void ts_btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente fechar o Callplus?", "Aviso do sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
                Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }

        private void alterarSenhaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tESTEPROSPECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_usuario.Protegido) return;
            try
            {
                var idProspectTxt = tsAtendimentoIdProspectTeste?.TextBox.Text;
                long idProspect = 0;
                idProspect = long.Parse(idProspectTxt);
                string idDiscador = "";
                long telefoneDaChamada = 31985831872;
                IniciarAtendimento(idProspect, telefoneDaChamada, idDiscador, OrigemDeAtendimento.Preditivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:{ex.Message} - Messagem: {ex.StackTrace}", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void testeAfterCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_usuario.Protegido) return;

            IniciarContagemDeAfterCall();
        }

        private void testePaulsaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_usuario.Protegido) return;

            _integrationCca.PowerDial("1010", "31985831872", "76251928");

            // _integrationCca.Close();
            // XCallAkiva_OnMudancaNoStatusDoAgente(null, 7);
        }

        #region INDICACAO

        IndicacaoDoAtendimento _indicacaoDoAtendimento = null;
        private static bool _emPausa = false;
        private List<KeyValuePair<string, string>> _listaStatusDoOperador;

        private void HabilitarEdicaoIndicacao(bool habilitar)
        {
            cmbGrauDeParentesco.ResetarComSelecione(habilitar);
            txtNomeIndicacao.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            txtTelefoneIndicacao.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            cmbGrauDeParentesco.Enabled = true;
            tsIndicacao_btnNovo.Enabled = !habilitar;
            tsIndicacao_btnExcluir.Enabled = habilitar;
            tsIndicacao_btnCancelar.Enabled = habilitar;
            tsIndicacao_btnSalvar.Enabled = habilitar;
        }

        private void IniciarInclusaoDaIndicacao()
        {
            HabilitarEdicaoIndicacao(true);

            tsIndicacao_btnExcluir.Enabled = false;
        }

        private void IniciarEdicaoDaIndicacao(int linha)
        {
            if (linha >= 0)
            {
                HabilitarEdicaoIndicacao(true);

                long id = (long)dgIndicacao.Rows[linha].Cells["idIndicacao"].Value;

                _indicacaoDoAtendimento = _atendimentoService.RetornarIndicacaoDoAtendimento(id, _atendimentoEmAndamento.Id);

                txtNomeIndicacao.Text = _indicacaoDoAtendimento.nome;
                txtTelefoneIndicacao.Text = _indicacaoDoAtendimento.telefone.ToString();
            }
        }

        private void CancelarEdicaoDaIndicacao()
        {
            HabilitarEdicaoIndicacao(false);

            _indicacaoDoAtendimento = null;
        }

        private bool AtendeRegrasDeGravacaoDaIndicacao()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtNomeIndicacao.Text.Trim()))
            {
                mensagens.Add("[Nome] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtTelefoneIndicacao.Text.Trim()))
            {
                mensagens.Add("[Telefone] deve ser informado!");
            }

            if (!string.IsNullOrEmpty(txtTelefoneIndicacao.Text) && !Texto.TelefoneCelularPossuiFormatoValido(txtTelefoneIndicacao.Text))
            {
                mensagens.Add("[Telefone] inválido!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void GravarIndicacao()
        {
            if (AtendeRegrasDeGravacaoDaIndicacao())
            {
                if (_indicacaoDoAtendimento == null)
                {
                    _indicacaoDoAtendimento = new IndicacaoDoAtendimento();
                }

                _indicacaoDoAtendimento.idAtendimento = _atendimentoEmAndamento.Id;
                _indicacaoDoAtendimento.nome = txtNomeIndicacao.Text;
                _indicacaoDoAtendimento.telefone = Convert.ToInt64(txtTelefoneIndicacao.Text);

                _indicacaoDoAtendimento.id = _atendimentoService.GravarIndicacaoDoAtendimento(_indicacaoDoAtendimento);

                CarregarGridIndicacaoDoAtendimento(_atendimentoEmAndamento.Id);

                MessageBox.Show("Indicação gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CancelarEdicaoDaIndicacao();
            }
        }

        private void ExcluirIndicacao()
        {
            _atendimentoService.ExcluirIndicacaoDoAtendimento(_indicacaoDoAtendimento.id);

            CarregarGridIndicacaoDoAtendimento(_atendimentoEmAndamento.Id);

            CancelarEdicaoDaIndicacao();

            MessageBox.Show("Indicação excluída com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsIndicacao_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarInclusaoDaIndicacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a inclusão da Indicação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsIndicacao_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDaIndicacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Indicação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsIndicacao_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirIndicacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir a Indicação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsIndicacao_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarIndicacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível salvar a Indicação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTelefoneIndicacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CallplusUtil.Validacoes.Texto.CaractereNumerico(e.KeyChar);
        }

        private void dgIndicacao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarEdicaoDaIndicacao(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da Indicação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion INDICACAO

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var idProspect = 7585625;
            var telefoneChamada = 11961671481;
            var ticket = "MOC001PBX001-1545399288.2719680";

            IniciarAtendimento(idProspect, telefoneChamada, ticket, OrigemDeAtendimento.Preditivo);
        }

        private void btnPesquisarPlano_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarPlanosParaComparacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os planos para  comparação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvPlanoOperadora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CompararPlanos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível comparar os planos!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvPlanoClaro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CompararPlanos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível comparar os planos!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarPergunta(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar a pergunta!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsPergunta_btnCancelar_Click(object sender, EventArgs e)
        {
            tsPergunta_btnCancelar.Enabled = false;
            tsPergunta_btnSalvar.Enabled = false;
            cmbResposta.Visible = false;
            lblPergunta.Visible = false;
            lblResposta.Visible = false;
        }

        private void tsPergunta_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarRespostaDaPesquisa();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a resposta!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvPesquisa_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            gvPesquisa.Columns["resposta"].Width = 200;
        }

        private void btnConsultarProspect_Click(object sender, EventArgs e)
        {
            ConsultarProspect();
        }

        private void btnPararTempo_Click(object sender, EventArgs e)
        {
            try
            {
                SolicitarPausaDeAfterCall();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Ocorreu um erro ao tentar parar o AfterCall!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}