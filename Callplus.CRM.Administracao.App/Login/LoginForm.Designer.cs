namespace Callplus.CRM.Administracao.App.Login
{
    partial class LoginForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.lblVersao = new System.Windows.Forms.Label();
			this.txtSenha = new System.Windows.Forms.TextBox();
			this.txtLogin = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnEntrar = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// lblVersao
			// 
			this.lblVersao.AutoSize = true;
			this.lblVersao.BackColor = System.Drawing.Color.White;
			this.lblVersao.Location = new System.Drawing.Point(12, 289);
			this.lblVersao.Name = "lblVersao";
			this.lblVersao.Size = new System.Drawing.Size(91, 13);
			this.lblVersao.TabIndex = 24;
			this.lblVersao.Text = "VersãoDoCallPlus";
			// 
			// txtSenha
			// 
			this.txtSenha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSenha.Location = new System.Drawing.Point(85, 171);
			this.txtSenha.Name = "txtSenha";
			this.txtSenha.Size = new System.Drawing.Size(140, 16);
			this.txtSenha.TabIndex = 27;
			this.txtSenha.UseSystemPasswordChar = true;
			// 
			// txtLogin
			// 
			this.txtLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
			this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLogin.Location = new System.Drawing.Point(85, 134);
			this.txtLogin.Name = "txtLogin";
			this.txtLogin.Size = new System.Drawing.Size(140, 16);
			this.txtLogin.TabIndex = 26;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(68, 101);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(143, 21);
			this.label1.TabIndex = 29;
			this.label1.Text = "ADMINISTRAÇÃO";
			// 
			// btnEntrar
			// 
			this.btnEntrar.BackColor = System.Drawing.SystemColors.Control;
			this.btnEntrar.BackgroundImage = global::Callplus.CRM.Administracao.App.Properties.Resources.fundoAzul;
			this.btnEntrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEntrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnEntrar.ForeColor = System.Drawing.Color.White;
			this.btnEntrar.Location = new System.Drawing.Point(47, 205);
			this.btnEntrar.Name = "btnEntrar";
			this.btnEntrar.Size = new System.Drawing.Size(182, 32);
			this.btnEntrar.TabIndex = 30;
			this.btnEntrar.Text = "Entrar";
			this.btnEntrar.UseVisualStyleBackColor = false;
			this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
			this.btnEntrar.MouseEnter += new System.EventHandler(this.btnEntrar_MouseEnter);
			this.btnEntrar.MouseLeave += new System.EventHandler(this.btnEntrar_MouseLeave);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(584, 311);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 25;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(47, 255);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(182, 13);
			this.label2.TabIndex = 31;
			this.label2.Text = "PORTABILIDADE";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(47, 240);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(182, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "MERCADO PAGO";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LoginForm
			// 
			this.AcceptButton = this.btnEntrar;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 311);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnEntrar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblVersao);
			this.Controls.Add(this.txtSenha);
			this.Controls.Add(this.txtLogin);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Callplus CRM - HMB Tecnologia";
			this.Load += new System.EventHandler(this.LoginForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}