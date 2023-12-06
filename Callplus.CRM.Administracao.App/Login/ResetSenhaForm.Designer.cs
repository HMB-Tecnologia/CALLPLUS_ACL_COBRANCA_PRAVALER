namespace Callplus.CRM.Administracao.App.Login
{
    partial class ResetSenhaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResetSenhaForm));
            this.btnReset = new System.Windows.Forms.Button();
            this.txtSenhaNova2 = new System.Windows.Forms.TextBox();
            this.txtSenhaNova = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblNovaSenha2 = new System.Windows.Forms.Label();
            this.lblSenhaNova = new System.Windows.Forms.Label();
            this.lblSenhaAtual = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(15, 138);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(162, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Atualizar";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtSenhaNova2
            // 
            this.txtSenhaNova2.Location = new System.Drawing.Point(15, 109);
            this.txtSenhaNova2.MaxLength = 20;
            this.txtSenhaNova2.Name = "txtSenhaNova2";
            this.txtSenhaNova2.PasswordChar = '*';
            this.txtSenhaNova2.Size = new System.Drawing.Size(162, 20);
            this.txtSenhaNova2.TabIndex = 5;
            this.txtSenhaNova2.UseSystemPasswordChar = true;
            // 
            // txtSenhaNova
            // 
            this.txtSenhaNova.Location = new System.Drawing.Point(15, 67);
            this.txtSenhaNova.MaxLength = 20;
            this.txtSenhaNova.Name = "txtSenhaNova";
            this.txtSenhaNova.PasswordChar = '*';
            this.txtSenhaNova.Size = new System.Drawing.Size(162, 20);
            this.txtSenhaNova.TabIndex = 3;
            this.txtSenhaNova.UseSystemPasswordChar = true;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(15, 25);
            this.txtSenha.MaxLength = 20;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(162, 20);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // lblNovaSenha2
            // 
            this.lblNovaSenha2.AutoSize = true;
            this.lblNovaSenha2.Location = new System.Drawing.Point(12, 93);
            this.lblNovaSenha2.Name = "lblNovaSenha2";
            this.lblNovaSenha2.Size = new System.Drawing.Size(106, 13);
            this.lblNovaSenha2.TabIndex = 4;
            this.lblNovaSenha2.Text = "Repita a nova senha";
            // 
            // lblSenhaNova
            // 
            this.lblSenhaNova.AutoSize = true;
            this.lblSenhaNova.Location = new System.Drawing.Point(12, 51);
            this.lblSenhaNova.Name = "lblSenhaNova";
            this.lblSenhaNova.Size = new System.Drawing.Size(102, 13);
            this.lblSenhaNova.TabIndex = 2;
            this.lblSenhaNova.Text = "Digite a nova senha";
            // 
            // lblSenhaAtual
            // 
            this.lblSenhaAtual.AutoSize = true;
            this.lblSenhaAtual.Location = new System.Drawing.Point(12, 9);
            this.lblSenhaAtual.Name = "lblSenhaAtual";
            this.lblSenhaAtual.Size = new System.Drawing.Size(101, 13);
            this.lblSenhaAtual.TabIndex = 0;
            this.lblSenhaAtual.Text = "Digite a senha atual";
            // 
            // ResetSenhaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 174);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtSenhaNova2);
            this.Controls.Add(this.txtSenhaNova);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lblNovaSenha2);
            this.Controls.Add(this.lblSenhaNova);
            this.Controls.Add(this.lblSenhaAtual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResetSenhaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualização de Senha";
            this.Load += new System.EventHandler(this.ResetSenhaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtSenhaNova2;
        private System.Windows.Forms.TextBox txtSenhaNova;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblNovaSenha2;
        private System.Windows.Forms.Label lblSenhaNova;
        private System.Windows.Forms.Label lblSenhaAtual;
    }
}