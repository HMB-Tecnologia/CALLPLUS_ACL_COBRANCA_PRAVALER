using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Login
{
    public partial class ResetSenhaForm : Form
    {
        public ResetSenhaForm(string login)
        {
            _login = login;

            _logger = LogManager.GetCurrentClassLogger();
            _loginService = new LoginService();
            
            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly ILogger _logger;
        private readonly LoginService _loginService;
        private string _senha = "";
        private string _senhaNova = "";
        private string _senhaNova2 = "";
        private string _login;

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            
        }

        private void ResetarSenha()
        {
            _senha = txtSenha.Text.Trim();
            _senhaNova = txtSenhaNova.Text.Trim();
            _senhaNova2 = txtSenhaNova2.Text.Trim();

            if (PodeResetar())
            {
                _loginService.ResetarSenha(_login, _senhaNova);
                MessageBox.Show("Senha atualizada com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                Close();
            }
        }

        private bool PodeResetar()
        {
            var mensagens = new List<string>();

            mensagens = _loginService.VerificarSePodeResetarSenha(_senha, _senhaNova, _senhaNova2, _login);
            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
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

        private void ResetSenhaForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n Erro:{ex.Message}\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetarSenha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível resetar a senha!\n Erro:{ex.Message}\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
