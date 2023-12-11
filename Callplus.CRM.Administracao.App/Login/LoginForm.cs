using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Environment;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _loginService = new LoginService();

            InitializeComponent();
        }

        #region VARIAVEIS
        
        private readonly ILogger _logger;
        private Usuario _usuarioLogado;
        private readonly LoginService _loginService;
        private string _senha = "";
        private string _login = "";
        private string _modulo = "ADM";
        private string _maquinaUsuario = "";
        private string _enderecoIP = "";
        private string _release = "01.000" +
            "";
        public static string Release;

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            lblVersao.Text = "Versão: " + _release;
            Release = _release;
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
                mensagens = _loginService.VerificarSeUsuarioPodeAcessarSistema(_login, _senha, _maquinaUsuario, _enderecoIP, _modulo,_release);
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
                    var form = new ResetSenhaForm(_login);
                    form.ShowDialog();
                    
                    txtSenha.Text = string.Empty;
                    txtSenha.Focus();

                    return false;
                }
            }
            return mensagens.Any() == false;
        }

        private void Entrar()
        {
            if (PodeAcessar())
            {
                CarregarUsuarioLogado(_login);

                Hide();

                AdministracaoMDI form = new AdministracaoMDI(_usuarioLogado);
                form.ShowDialog();

                Close();
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
        
        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion METODOS            

        #region EVENTOS

        private void LoginForm_Load(object sender, System.EventArgs e)
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
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar o login!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
