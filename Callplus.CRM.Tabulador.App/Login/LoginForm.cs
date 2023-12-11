using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using Callplus.IntegracaoDiscador;
using CallplusUtil.Environment;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Perfil = Callplus.CRM.Tabulador.Dominio.Tipos.Perfil;

namespace Callplus.CRM.Tabulador.App.Login
{
	public partial class LoginForm : Form
    {
        public LoginForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _discadorService = new DiscadorService();
            _loginService = new LoginService();
            _campanhaService = new CampanhaService();

            InitializeComponent();
        }

        #region variaveis OLOS
        OlosWsAgentControl _olosWsAgentControl;
        public static int AgenteIdOlos { get; private set; }
        #endregion

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private Usuario _usuarioLogado;
        private Discador _discadorConectado;
        private readonly LoginService _loginService; 
        private readonly CampanhaService _campanhaService;
        private readonly DiscadorService _discadorService;
        private string _senha = "";
        private string _login = "";
        private string _modulo = "OPE";
        private string _maquinaUsuario = "";
        private string _enderecoIP = "";
        private string _release = "01.000";

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            Util.ConfiguracaoDeAmbiente.Release = _release;
            lblVersao.Text = "Versão: " + _release;
        }

        private bool PodeAcessar()
        {
            var mensagens = new List<string>();

            _login = txtLogin.Text.Trim();
            _senha = txtSenha.Text.Trim();
            _maquinaUsuario = ConfiguracaoDeAmbiente.HostName;
            _enderecoIP = ConfiguracaoDeAmbiente.RetornarEnderecoIP();

            if (_login.Trim() == "" || _senha.Trim() == "")
            {
                mensagens.Add("Informe o login e senha!");
            }
            else
            {
                mensagens = _loginService.VerificarSeUsuarioPodeAcessarSistema(_login, _senha, _maquinaUsuario, _enderecoIP, _modulo, _release);
            }

            if (mensagens.Any())
            {
                ExibirMensagens(mensagens);
            }
            else
            {
                if (SenhaExpirada())
                {
                    MessageBox.Show("Sua senha expirou!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    var resetarForm = new ResetarSenhaForm();
                    resetarForm.SolicitarAlteracaoDeSenha(_login);

                    txtSenha.Text = string.Empty;
                    txtLogin.Text = string.Empty;
                    return false;
                }
            }
            return !mensagens.Any();
        }

        private void Entrar()
        {
            if (PodeAcessar())
            {
                CarregarUsuarioLogado(_login);

                if (_usuarioLogado.perfil != Perfil.OPERADOR)
                {
                    MessageBox.Show("Não é possível fazer login pois o usuário não possui perfil de Operador", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Campanha campanhaPrincipal = _campanhaService.RetornarCampanhaPrincipalDoUsuario(_usuarioLogado.Id);

                _logger.Info($"Iniciando sistema no módulo operador. Versão: {_release}");
                //TODO - DESCOMENTAR DISCADOR
                CarregarDiscadorConectado(campanhaPrincipal.IdDiscador);

                if (_discadorConectado.TipoDiscador == Dominio.Tipos.TipoDiscador.OlosAPI)
                {
                    LogarOlosWebService(_login, _login, true);
                    return;
                }

                this.Hide();
                AtendimentoForm formulario = new AtendimentoForm(_usuarioLogado, _discadorConectado);
                formulario.Iniciar();
                this.Close();
            }
        }

        private bool SenhaExpirada()
        {
            return _loginService.VerificarSenhaExpirada(_login, _senha);
        }

        private void CarregarUsuarioLogado(string login)
        {
            _usuarioLogado = _loginService.RetornarUsuario(login);
        }

        private void CarregarDiscadorConectado(int idDiscador)
        {
            _discadorConectado = _discadorService.RetornarDiscador(idDiscador);
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void AlterarLabel(Label lib, String text)
        {
            if (lib.InvokeRequired)
            {
                lib.BeginInvoke(new MethodInvoker(() => AlterarLabel(lib, text)));
            }
            else
            {
                lib.Text = text;
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void LoginForm_Load(object sender, EventArgs e)
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

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                Entrar();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);

                MessageBox.Show($"Ocorreu um erro Inesperado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {
            btnEntrar.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
        }

        private void btnEntrar_MouseLeave(object sender, EventArgs e)
        {
            btnEntrar.FlatAppearance.BorderColor = System.Drawing.Color.Empty;
        }

        #endregion EVENTOS        

        #region OLOSWEBSERVICE

        private void LogarOlosWebService(string login, string password, bool forceLogout)
        {
            _logger.Info($"LogarOlosWebService. Versão: {login}");

            if (login == "oohmb")
            {
                _login = "plantestes";
                login = "plantestes";
                password = "plantestes";
            }

            _olosWsAgentControl = new OlosWsAgentControl();
            _olosWsAgentControl.PassCodeEvent += Olos_OnPassCodeEvent;
            _olosWsAgentControl.LoginCCM += Olos_OnLoginCCM;
            _olosWsAgentControl.LoginCCMFail += Olos_OnLoginCCMFail;

            RealizarLoginOlos(login, password, forceLogout);
        }

        private void Olos_OnLoginCCM()
        {
            //_logger.Info($"Login CCM");

            ///parar de monitorar eventos
            _olosWsAgentControl?.FinishAgentMonitoring();

            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    //_logger.Info($"InvokeRequired TRUE: {InvokeRequired}");

                    this.Hide();
                    AtendimentoForm formulario = new AtendimentoForm(_usuarioLogado, _discadorConectado);
                    formulario.Iniciar();

                    //MessageBox.Show($"Chamou LOGIN CCM.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }));

            }
            else
            {
                //_logger.Info($"InvokeRequired FALSO: {InvokeRequired}");

                this.Hide();
                AtendimentoForm formulario = new AtendimentoForm(_usuarioLogado, _discadorConectado);
                formulario.Iniciar();

                //MessageBox.Show($"Chamou LOGIN CCM.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
        }

        private void Olos_OnLoginCCMFail(string reason)
        {
            MessageBox.Show($"Não foi posível realizar login no discador OLOS. Motivo: {reason}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Olos_OnPassCodeEvent(int passCode)
        {
            Invoke((MethodInvoker)delegate { lblPassCodeOlos.Visible = true; });
            string txtPassCode = $"Pass Code Olos: {passCode}";
            AlterarLabel(lblPassCodeOlos, txtPassCode);
        }

        public void RealizarLoginOlos(string login, string password, bool forceLogout)
        {
            _logger.Info($"RealizarLoginOlos: {login}, {password},{forceLogout}");

            int agentId = 0;
            agentId = _olosWsAgentControl.Login(login, password, forceLogout);

            if (agentId > 0)
            {
                AgenteIdOlos = agentId;
                _olosWsAgentControl.StartAgentMonitoring(agentId);
                _logger.Debug($"agente id olos { agentId }");
            }
            else
            {
                AgenteIdOlos = 0;
                MessageBox.Show("Login e/ou Senha inválido(s).", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
