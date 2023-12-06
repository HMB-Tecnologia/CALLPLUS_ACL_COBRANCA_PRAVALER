namespace Callplus.CRM.Tabulador.App.Login
{
    partial class ResetarSenhaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResetarSenhaForm));
            this.lblSenhaAtual = new System.Windows.Forms.Label();
            this.lblSenhaNova = new System.Windows.Forms.Label();
            this.lblNovaSenha2 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtSenhaNova = new System.Windows.Forms.TextBox();
            this.txtSenhaNova2 = new System.Windows.Forms.TextBox();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.cmdResetar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSenhaAtual
            // 
            this.lblSenhaAtual.AutoSize = true;
            this.lblSenhaAtual.Location = new System.Drawing.Point(20, 82);
            this.lblSenhaAtual.Name = "lblSenhaAtual";
            this.lblSenhaAtual.Size = new System.Drawing.Size(104, 13);
            this.lblSenhaAtual.TabIndex = 0;
            this.lblSenhaAtual.Text = "Digite a _senha atual:";
            // 
            // lblSenhaNova
            // 
            this.lblSenhaNova.AutoSize = true;
            this.lblSenhaNova.Location = new System.Drawing.Point(19, 118);
            this.lblSenhaNova.Name = "lblSenhaNova";
            this.lblSenhaNova.Size = new System.Drawing.Size(105, 13);
            this.lblSenhaNova.TabIndex = 1;
            this.lblSenhaNova.Text = "Digite a nova _senha:";
            // 
            // lblNovaSenha2
            // 
            this.lblNovaSenha2.AutoSize = true;
            this.lblNovaSenha2.Location = new System.Drawing.Point(31, 152);
            this.lblNovaSenha2.Name = "lblNovaSenha2";
            this.lblNovaSenha2.Size = new System.Drawing.Size(93, 13);
            this.lblNovaSenha2.TabIndex = 2;
            this.lblNovaSenha2.Text = "Digite novamente:";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(130, 75);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(133, 20);
            this.txtSenha.TabIndex = 3;
            // 
            // txtSenhaNova
            // 
            this.txtSenhaNova.Location = new System.Drawing.Point(130, 111);
            this.txtSenhaNova.Name = "txtSenhaNova";
            this.txtSenhaNova.PasswordChar = '*';
            this.txtSenhaNova.Size = new System.Drawing.Size(133, 20);
            this.txtSenhaNova.TabIndex = 4;
            // 
            // txtSenhaNova2
            // 
            this.txtSenhaNova2.Location = new System.Drawing.Point(130, 149);
            this.txtSenhaNova2.Name = "txtSenhaNova2";
            this.txtSenhaNova2.PasswordChar = '*';
            this.txtSenhaNova2.Size = new System.Drawing.Size(133, 20);
            this.txtSenhaNova2.TabIndex = 5;
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Location = new System.Drawing.Point(188, 219);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(75, 23);
            this.cmdCancelar.TabIndex = 6;
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // cmdResetar
            // 
            this.cmdResetar.Location = new System.Drawing.Point(23, 219);
            this.cmdResetar.Name = "cmdResetar";
            this.cmdResetar.Size = new System.Drawing.Size(75, 23);
            this.cmdResetar.TabIndex = 7;
            this.cmdResetar.Text = "Resetar";
            this.cmdResetar.UseVisualStyleBackColor = true;
            this.cmdResetar.Click += new System.EventHandler(this.cmdResetar_Click);
            // 
            // ResetarSenhaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 265);
            this.Controls.Add(this.cmdResetar);
            this.Controls.Add(this.cmdCancelar);
            this.Controls.Add(this.txtSenhaNova2);
            this.Controls.Add(this.txtSenhaNova);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lblNovaSenha2);
            this.Controls.Add(this.lblSenhaNova);
            this.Controls.Add(this.lblSenhaAtual);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResetarSenhaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resetar Senha";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSenhaAtual;
        private System.Windows.Forms.Label lblSenhaNova;
        private System.Windows.Forms.Label lblNovaSenha2;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtSenhaNova;
        private System.Windows.Forms.TextBox txtSenhaNova2;
        private System.Windows.Forms.Button cmdCancelar;
        private System.Windows.Forms.Button cmdResetar;
    }
}