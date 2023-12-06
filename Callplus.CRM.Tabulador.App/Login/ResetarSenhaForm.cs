using Callplus.CRM.Tabulador.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Login
{
    public partial class ResetarSenhaForm : Form
    {
        private readonly LoginService _loginService;
        private string _senha = "";
        private string _senhaNova = "";
        private string _senhaNova2 = "";
        private string _login;

        public ResetarSenhaForm()
        {
            _loginService = new LoginService();
            InitializeComponent();
        }
        
        #region Eventos
        private void cmdResetar_Click(object sender, EventArgs e)
        {
            try
            {
                ResetarSenha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao resetar a _senha: " + ex.Message, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        #endregion

        #region Metodos

        public void ResetarSenha()
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

        public void SolicitarAlteracaoDeSenha(string login)
        {
            _login = login;
            StartPosition = FormStartPosition.CenterScreen;
            ShowDialog();
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Hide();
            Close();
        }

       
    }
}
