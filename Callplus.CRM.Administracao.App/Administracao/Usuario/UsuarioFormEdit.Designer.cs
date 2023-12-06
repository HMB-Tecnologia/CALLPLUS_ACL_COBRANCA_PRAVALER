namespace Callplus.CRM.Administracao.App.Administracao.Usuario
{
    partial class UsuarioFormEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsuarioFormEdit));
            this.gpConfiguracoes = new System.Windows.Forms.GroupBox();
            this.cmbCampanhaPrincipal = new System.Windows.Forms.ComboBox();
            this.lnkMarcarTodos = new System.Windows.Forms.LinkLabel();
            this.lblCampanhaPrincipal = new System.Windows.Forms.Label();
            this.lnkDesmarcarTodos = new System.Windows.Forms.LinkLabel();
            this.clbCampanha = new System.Windows.Forms.CheckedListBox();
            this.cmbPerfil = new System.Windows.Forms.ComboBox();
            this.lblCampanhas = new System.Windows.Forms.Label();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.gbDadosAcesso = new System.Windows.Forms.GroupBox();
            this.chkSenhaExpirada = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.gbDadosPessoais = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSupervisor = new System.Windows.Forms.ComboBox();
            this.gpConfiguracoes.SuspendLayout();
            this.gbDadosAcesso.SuspendLayout();
            this.gbDadosPessoais.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpConfiguracoes
            // 
            this.gpConfiguracoes.Controls.Add(this.cmbCampanhaPrincipal);
            this.gpConfiguracoes.Controls.Add(this.lnkMarcarTodos);
            this.gpConfiguracoes.Controls.Add(this.lblCampanhaPrincipal);
            this.gpConfiguracoes.Controls.Add(this.lnkDesmarcarTodos);
            this.gpConfiguracoes.Controls.Add(this.clbCampanha);
            this.gpConfiguracoes.Controls.Add(this.cmbPerfil);
            this.gpConfiguracoes.Controls.Add(this.lblCampanhas);
            this.gpConfiguracoes.Controls.Add(this.lblPerfil);
            this.gpConfiguracoes.Location = new System.Drawing.Point(15, 40);
            this.gpConfiguracoes.Name = "gpConfiguracoes";
            this.gpConfiguracoes.Size = new System.Drawing.Size(388, 277);
            this.gpConfiguracoes.TabIndex = 1;
            this.gpConfiguracoes.TabStop = false;
            this.gpConfiguracoes.Text = "Configurações";
            // 
            // cmbCampanhaPrincipal
            // 
            this.cmbCampanhaPrincipal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCampanhaPrincipal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCampanhaPrincipal.FormattingEnabled = true;
            this.cmbCampanhaPrincipal.Location = new System.Drawing.Point(18, 75);
            this.cmbCampanhaPrincipal.Name = "cmbCampanhaPrincipal";
            this.cmbCampanhaPrincipal.Size = new System.Drawing.Size(335, 21);
            this.cmbCampanhaPrincipal.TabIndex = 3;
            this.cmbCampanhaPrincipal.SelectedIndexChanged += new System.EventHandler(this.cmbCampanhaPrincipal_SelectedIndexChanged);
            // 
            // lnkMarcarTodos
            // 
            this.lnkMarcarTodos.AutoSize = true;
            this.lnkMarcarTodos.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkMarcarTodos.Location = new System.Drawing.Point(176, 102);
            this.lnkMarcarTodos.Name = "lnkMarcarTodos";
            this.lnkMarcarTodos.Size = new System.Drawing.Size(69, 13);
            this.lnkMarcarTodos.TabIndex = 5;
            this.lnkMarcarTodos.TabStop = true;
            this.lnkMarcarTodos.Text = "Marcar todos";
            this.lnkMarcarTodos.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkMarcarTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMarcarTodos_LinkClicked);
            // 
            // lblCampanhaPrincipal
            // 
            this.lblCampanhaPrincipal.AutoSize = true;
            this.lblCampanhaPrincipal.Location = new System.Drawing.Point(15, 59);
            this.lblCampanhaPrincipal.Name = "lblCampanhaPrincipal";
            this.lblCampanhaPrincipal.Size = new System.Drawing.Size(101, 13);
            this.lblCampanhaPrincipal.TabIndex = 2;
            this.lblCampanhaPrincipal.Text = "Campanha Principal";
            // 
            // lnkDesmarcarTodos
            // 
            this.lnkDesmarcarTodos.AutoSize = true;
            this.lnkDesmarcarTodos.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkDesmarcarTodos.Location = new System.Drawing.Point(266, 102);
            this.lnkDesmarcarTodos.Name = "lnkDesmarcarTodos";
            this.lnkDesmarcarTodos.Size = new System.Drawing.Size(87, 13);
            this.lnkDesmarcarTodos.TabIndex = 6;
            this.lnkDesmarcarTodos.TabStop = true;
            this.lnkDesmarcarTodos.Text = "Desmarcar todos";
            this.lnkDesmarcarTodos.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkDesmarcarTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDesmarcarTodos_LinkClicked);
            // 
            // clbCampanha
            // 
            this.clbCampanha.CheckOnClick = true;
            this.clbCampanha.FormattingEnabled = true;
            this.clbCampanha.Location = new System.Drawing.Point(18, 118);
            this.clbCampanha.Name = "clbCampanha";
            this.clbCampanha.Size = new System.Drawing.Size(335, 139);
            this.clbCampanha.TabIndex = 7;
            // 
            // cmbPerfil
            // 
            this.cmbPerfil.FormattingEnabled = true;
            this.cmbPerfil.Location = new System.Drawing.Point(18, 35);
            this.cmbPerfil.Name = "cmbPerfil";
            this.cmbPerfil.Size = new System.Drawing.Size(335, 21);
            this.cmbPerfil.TabIndex = 1;
            this.cmbPerfil.SelectedIndexChanged += new System.EventHandler(this.cmbPerfil_SelectedIndexChanged);
            // 
            // lblCampanhas
            // 
            this.lblCampanhas.AutoSize = true;
            this.lblCampanhas.Location = new System.Drawing.Point(15, 102);
            this.lblCampanhas.Name = "lblCampanhas";
            this.lblCampanhas.Size = new System.Drawing.Size(63, 13);
            this.lblCampanhas.TabIndex = 4;
            this.lblCampanhas.Text = "Campanhas";
            // 
            // lblPerfil
            // 
            this.lblPerfil.AutoSize = true;
            this.lblPerfil.Location = new System.Drawing.Point(15, 19);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(30, 13);
            this.lblPerfil.TabIndex = 0;
            this.lblPerfil.Text = "Perfil";
            // 
            // gbDadosAcesso
            // 
            this.gbDadosAcesso.Controls.Add(this.chkSenhaExpirada);
            this.gbDadosAcesso.Controls.Add(this.chkAtivo);
            this.gbDadosAcesso.Location = new System.Drawing.Point(409, 142);
            this.gbDadosAcesso.Name = "gbDadosAcesso";
            this.gbDadosAcesso.Size = new System.Drawing.Size(373, 72);
            this.gbDadosAcesso.TabIndex = 3;
            this.gbDadosAcesso.TabStop = false;
            this.gbDadosAcesso.Text = "Dados de Acesso";
            // 
            // chkSenhaExpirada
            // 
            this.chkSenhaExpirada.AutoSize = true;
            this.chkSenhaExpirada.Location = new System.Drawing.Point(142, 35);
            this.chkSenhaExpirada.Name = "chkSenhaExpirada";
            this.chkSenhaExpirada.Size = new System.Drawing.Size(167, 17);
            this.chkSenhaExpirada.TabIndex = 6;
            this.chkSenhaExpirada.Text = "Alterar senha no próximo login";
            this.chkSenhaExpirada.UseVisualStyleBackColor = true;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(18, 35);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 5;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(282, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "EDITAR DADOS USUÁRIOS";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSalvar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.Location = new System.Drawing.Point(14, 323);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // gbDadosPessoais
            // 
            this.gbDadosPessoais.Controls.Add(this.label10);
            this.gbDadosPessoais.Controls.Add(this.cmbSupervisor);
            this.gbDadosPessoais.Location = new System.Drawing.Point(409, 40);
            this.gbDadosPessoais.Name = "gbDadosPessoais";
            this.gbDadosPessoais.Size = new System.Drawing.Size(373, 72);
            this.gbDadosPessoais.TabIndex = 7;
            this.gbDadosPessoais.TabStop = false;
            this.gbDadosPessoais.Text = "Dados Pessoais";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Supervisor";
            // 
            // cmbSupervisor
            // 
            this.cmbSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupervisor.FormattingEnabled = true;
            this.cmbSupervisor.Location = new System.Drawing.Point(18, 35);
            this.cmbSupervisor.Name = "cmbSupervisor";
            this.cmbSupervisor.Size = new System.Drawing.Size(335, 21);
            this.cmbSupervisor.TabIndex = 7;
            // 
            // UsuarioFormEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(806, 358);
            this.Controls.Add(this.gbDadosPessoais);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.gpConfiguracoes);
            this.Controls.Add(this.gbDadosAcesso);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsuarioFormEdit";
            this.Text = "Editar Usuários";
            this.Load += new System.EventHandler(this.UsuarioForm_Load);
            this.gpConfiguracoes.ResumeLayout(false);
            this.gpConfiguracoes.PerformLayout();
            this.gbDadosAcesso.ResumeLayout(false);
            this.gbDadosAcesso.PerformLayout();
            this.gbDadosPessoais.ResumeLayout(false);
            this.gbDadosPessoais.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.GroupBox gpConfiguracoes;
        private System.Windows.Forms.CheckedListBox clbCampanha;
        private System.Windows.Forms.ComboBox cmbPerfil;
        private System.Windows.Forms.Label lblCampanhas;
        private System.Windows.Forms.Label lblPerfil;
        private System.Windows.Forms.GroupBox gbDadosAcesso;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.LinkLabel lnkMarcarTodos;
        private System.Windows.Forms.LinkLabel lnkDesmarcarTodos;
        private System.Windows.Forms.ComboBox cmbCampanhaPrincipal;
        private System.Windows.Forms.Label lblCampanhaPrincipal;
        private System.Windows.Forms.CheckBox chkSenhaExpirada;
        private System.Windows.Forms.GroupBox gbDadosPessoais;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbSupervisor;
    }
}