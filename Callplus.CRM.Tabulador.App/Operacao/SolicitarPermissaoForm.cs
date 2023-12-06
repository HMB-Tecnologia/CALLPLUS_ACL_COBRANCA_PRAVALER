using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class SolicitarPermissaoForm : Form
    {
        public SolicitarPermissaoForm(Usuario usuarioLogado)
        {
            _permissaoservice = new PermissaoService();
            _loginService = new LoginService();
            _usuarioLogado = usuarioLogado;

            InitializeComponent();
        }

        private readonly ILogger _logger;
        private string _login = "";
        private string _senha = "";
        private bool _verificarPerfilSupervisor = false;
        private bool _permitePerfilAdministrador = false;
        private Usuario _usuarioLogado;
        private RetornoSolicitacaoPermissaoUsuario _retornoPermissao;
        private readonly PermissaoService _permissaoservice;
        private Button btnCancelar;
        private Button btnConfirmar;
        private Label label3;
        private Label label2;
        private TextBox txtSenha;
        private TextBox txtLogin;
        private Label label1;
        private readonly LoginService _loginService;


        #region METODOS

        private void SolicitarPermissao()
        {
            int idUsuario = _usuarioLogado.Id;
            _retornoPermissao = new RetornoSolicitacaoPermissaoUsuario();

            _retornoPermissao.PermissaoConfirmada = false;
            if (PodeSolicitarPermissao(idUsuario) == false) return;

            string login = _login.Trim();
            string senha = _senha.Trim();
            int idUsuarioPermissao = _loginService.VerificarUsuarioPorLoginSenha(_login, _senha);

            if (idUsuarioPermissao > 0)
            {
                _retornoPermissao.PermissaoConfirmada = true;
                _retornoPermissao.IdUsuarioPermissao = idUsuarioPermissao;
                this.Close();
            }

            if (idUsuarioPermissao == 0)
            {
                MessageBox.Show("Não foi possível obter o usuário para permissão", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        

        private void CancelarPermissao()
        {
            _retornoPermissao.PermissaoConfirmada = false;
            _retornoPermissao.IdUsuarioPermissao = null;
            this.Close();
        }

        public RetornoSolicitacaoPermissaoUsuario SolicitarPermissaoDeUsuario(bool verificarPerfilSupervisor, bool permitePerfilAdministrador)
        {
            _permitePerfilAdministrador = permitePerfilAdministrador;
            _verificarPerfilSupervisor = verificarPerfilSupervisor;
            _retornoPermissao = new RetornoSolicitacaoPermissaoUsuario();
            this.ShowDialog();
            return _retornoPermissao;
        }

        private bool PodeSolicitarPermissao(int idUsuario)
        {
            var mensagens = new List<string>();

            _login = txtLogin.Text.Trim();
            _senha = txtSenha.Text.Trim();


            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_senha))
            {
                mensagens.Add("informe o login e senha!");
            }
            else
            {
                var msgsValidacao = _permissaoservice.VerificarPermissaoPorLoginESenha(idUsuario, _login, _senha, _verificarPerfilSupervisor, _permitePerfilAdministrador);
                mensagens.AddRange(msgsValidacao);
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return !mensagens.Any();

        }



        #endregion

        #region EVENTOS

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }


        #endregion



        public RetornoSolicitacaoPermissaoUsuario SolicitarPermissaoDeUsuario(bool verificarPerfilSupervisor, object verificarPerfilAdministrador)
        {
            return _retornoPermissao;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolicitarPermissaoForm));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(66, 110);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 23);
            this.btnCancelar.TabIndex = 21;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(162, 110);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(68, 23);
            this.btnConfirmar.TabIndex = 20;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Senha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Login:";
            // 
            // txtSenha
            // 
            this.txtSenha.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(66, 68);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(164, 17);
            this.txtSenha.TabIndex = 17;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.Location = new System.Drawing.Point(66, 45);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(164, 17);
            this.txtLogin.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 21);
            this.label1.TabIndex = 15;
            this.label1.Text = "SOLICITAR PERMISSÃO";
            // 
            // SolicitarPermissaoForm
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(256, 159);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SolicitarPermissaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitar Permissão";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarPermissao();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                MessageBox.Show($"Ocorreu um erro Inesperado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            try
            {
                SolicitarPermissao();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                MessageBox.Show($"Ocorreu um erro Inesperado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SolicitarPermissaoForm_Load(object sender, EventArgs e)
        {

        }

    }


    public class RetornoSolicitacaoPermissaoUsuario
    {
        public bool PermissaoConfirmada { get; set; }
        public int? IdUsuarioPermissao { get; set; }
    }
}

