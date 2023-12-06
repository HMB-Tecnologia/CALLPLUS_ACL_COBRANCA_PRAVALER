namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class NotificacaoForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificacaoForm));
            this.lblTituloNotificacao = new System.Windows.Forms.Label();
            this.lblDescricaoNotificacao = new System.Windows.Forms.Label();
            this.btnConfirmarLeitura = new System.Windows.Forms.Button();
            this.lblIdNotificacao = new System.Windows.Forms.Label();
            this.timerLeitura = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTempo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTituloNotificacao
            // 
            this.lblTituloNotificacao.AutoSize = true;
            this.lblTituloNotificacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTituloNotificacao.ForeColor = System.Drawing.Color.Blue;
            this.lblTituloNotificacao.Location = new System.Drawing.Point(4, 12);
            this.lblTituloNotificacao.Name = "lblTituloNotificacao";
            this.lblTituloNotificacao.Size = new System.Drawing.Size(200, 24);
            this.lblTituloNotificacao.TabIndex = 0;
            this.lblTituloNotificacao.Text = "Título da Mensagem";
            // 
            // lblDescricaoNotificacao
            // 
            this.lblDescricaoNotificacao.AutoSize = true;
            this.lblDescricaoNotificacao.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblDescricaoNotificacao.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDescricaoNotificacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoNotificacao.ForeColor = System.Drawing.Color.Black;
            this.lblDescricaoNotificacao.Location = new System.Drawing.Point(7, 42);
            this.lblDescricaoNotificacao.MaximumSize = new System.Drawing.Size(580, 120);
            this.lblDescricaoNotificacao.MinimumSize = new System.Drawing.Size(580, 120);
            this.lblDescricaoNotificacao.Name = "lblDescricaoNotificacao";
            this.lblDescricaoNotificacao.Size = new System.Drawing.Size(580, 120);
            this.lblDescricaoNotificacao.TabIndex = 31;
            this.lblDescricaoNotificacao.Text = "Descrição da Mensagem";
            // 
            // btnConfirmarLeitura
            // 
            this.btnConfirmarLeitura.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnConfirmarLeitura.Enabled = false;
            this.btnConfirmarLeitura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarLeitura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarLeitura.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarLeitura.Location = new System.Drawing.Point(200, 166);
            this.btnConfirmarLeitura.Name = "btnConfirmarLeitura";
            this.btnConfirmarLeitura.Size = new System.Drawing.Size(187, 37);
            this.btnConfirmarLeitura.TabIndex = 32;
            this.btnConfirmarLeitura.Text = "Confirmar Leitura";
            this.btnConfirmarLeitura.UseVisualStyleBackColor = false;
            this.btnConfirmarLeitura.Click += new System.EventHandler(this.btnConfirmarLeitura_Click);
            // 
            // lblIdNotificacao
            // 
            this.lblIdNotificacao.AutoSize = true;
            this.lblIdNotificacao.Location = new System.Drawing.Point(500, 24);
            this.lblIdNotificacao.Name = "lblIdNotificacao";
            this.lblIdNotificacao.Size = new System.Drawing.Size(0, 13);
            this.lblIdNotificacao.TabIndex = 36;
            this.lblIdNotificacao.Visible = false;
            // 
            // timerLeitura
            // 
            this.timerLeitura.Interval = 1000;
            this.timerLeitura.Tick += new System.EventHandler(this.timerLeitura_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTempo);
            this.panel1.Controls.Add(this.lblTituloNotificacao);
            this.panel1.Controls.Add(this.lblDescricaoNotificacao);
            this.panel1.Controls.Add(this.btnConfirmarLeitura);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 215);
            this.panel1.TabIndex = 37;
            // 
            // lblTempo
            // 
            this.lblTempo.AutoSize = true;
            this.lblTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTempo.Location = new System.Drawing.Point(558, 183);
            this.lblTempo.Name = "lblTempo";
            this.lblTempo.Size = new System.Drawing.Size(29, 20);
            this.lblTempo.TabIndex = 33;
            this.lblTempo.Text = "00";
            // 
            // NotificacaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(603, 215);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblIdNotificacao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificacaoForm";
            this.Text = "Notificações";
            this.Load += new System.EventHandler(this.NotificacaoForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTituloNotificacao;
        private System.Windows.Forms.Label lblDescricaoNotificacao;
        private System.Windows.Forms.Button btnConfirmarLeitura;
        private System.Windows.Forms.Label lblIdNotificacao;
        private System.Windows.Forms.Timer timerLeitura;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTempo;
    }
}