namespace Callplus.CRM.Administracao.App.Qualidade.FaqDeAtendimento
{
    partial class FaqDeAtendimentoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaqDeAtendimentoForm));
            this.cmbCampanha = new System.Windows.Forms.ComboBox();
            this.lblCampanha = new System.Windows.Forms.Label();
            this.lblNovoFaq = new System.Windows.Forms.Label();
            this.gpDadosCadastrais = new System.Windows.Forms.GroupBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.txtPergunta = new System.Windows.Forms.TextBox();
            this.lblResposta = new System.Windows.Forms.Label();
            this.txtResposta = new System.Windows.Forms.TextBox();
            this.lblPergunta = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.gpDadosCadastrais.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCampanha
            // 
            this.cmbCampanha.FormattingEnabled = true;
            this.cmbCampanha.Location = new System.Drawing.Point(9, 41);
            this.cmbCampanha.Name = "cmbCampanha";
            this.cmbCampanha.Size = new System.Drawing.Size(356, 21);
            this.cmbCampanha.TabIndex = 0;
            // 
            // lblCampanha
            // 
            this.lblCampanha.AutoSize = true;
            this.lblCampanha.Location = new System.Drawing.Point(6, 25);
            this.lblCampanha.Name = "lblCampanha";
            this.lblCampanha.Size = new System.Drawing.Size(58, 13);
            this.lblCampanha.TabIndex = 2;
            this.lblCampanha.Text = "Campanha";
            // 
            // lblNovoFaq
            // 
            this.lblNovoFaq.AutoSize = true;
            this.lblNovoFaq.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNovoFaq.Location = new System.Drawing.Point(17, 9);
            this.lblNovoFaq.Name = "lblNovoFaq";
            this.lblNovoFaq.Size = new System.Drawing.Size(287, 24);
            this.lblNovoFaq.TabIndex = 39;
            this.lblNovoFaq.Text = "NOVO FAQ DE ATENDIMENTO";
            // 
            // gpDadosCadastrais
            // 
            this.gpDadosCadastrais.Controls.Add(this.chkAtivo);
            this.gpDadosCadastrais.Controls.Add(this.txtPergunta);
            this.gpDadosCadastrais.Controls.Add(this.lblResposta);
            this.gpDadosCadastrais.Controls.Add(this.txtResposta);
            this.gpDadosCadastrais.Controls.Add(this.lblPergunta);
            this.gpDadosCadastrais.Controls.Add(this.lblCampanha);
            this.gpDadosCadastrais.Controls.Add(this.cmbCampanha);
            this.gpDadosCadastrais.Location = new System.Drawing.Point(12, 48);
            this.gpDadosCadastrais.Name = "gpDadosCadastrais";
            this.gpDadosCadastrais.Size = new System.Drawing.Size(376, 285);
            this.gpDadosCadastrais.TabIndex = 0;
            this.gpDadosCadastrais.TabStop = false;
            this.gpDadosCadastrais.Text = "Dados Cadastrais";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(9, 266);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 3;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // txtPergunta
            // 
            this.txtPergunta.Location = new System.Drawing.Point(9, 81);
            this.txtPergunta.Multiline = true;
            this.txtPergunta.Name = "txtPergunta";
            this.txtPergunta.Size = new System.Drawing.Size(356, 79);
            this.txtPergunta.TabIndex = 1;
            // 
            // lblResposta
            // 
            this.lblResposta.AutoSize = true;
            this.lblResposta.Location = new System.Drawing.Point(6, 165);
            this.lblResposta.Name = "lblResposta";
            this.lblResposta.Size = new System.Drawing.Size(52, 13);
            this.lblResposta.TabIndex = 6;
            this.lblResposta.Text = "Resposta";
            // 
            // txtResposta
            // 
            this.txtResposta.Location = new System.Drawing.Point(9, 181);
            this.txtResposta.Multiline = true;
            this.txtResposta.Name = "txtResposta";
            this.txtResposta.Size = new System.Drawing.Size(356, 79);
            this.txtResposta.TabIndex = 2;
            // 
            // lblPergunta
            // 
            this.lblPergunta.AutoSize = true;
            this.lblPergunta.Location = new System.Drawing.Point(6, 65);
            this.lblPergunta.Name = "lblPergunta";
            this.lblPergunta.Size = new System.Drawing.Size(50, 13);
            this.lblPergunta.TabIndex = 4;
            this.lblPergunta.Text = "Pergunta";
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 341);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 1;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FaqDeAtendimentoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 378);
            this.Controls.Add(this.gpDadosCadastrais);
            this.Controls.Add(this.lblNovoFaq);
            this.Controls.Add(this.btnSalvar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FaqDeAtendimentoForm";
            this.Text = "FAQ de atendimento";
            this.Load += new System.EventHandler(this.FaqDeAtendimentoForm_Load);
            this.gpDadosCadastrais.ResumeLayout(false);
            this.gpDadosCadastrais.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.Label lblCampanha;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblNovoFaq;
        private System.Windows.Forms.GroupBox gpDadosCadastrais;
        private System.Windows.Forms.TextBox txtPergunta;
        private System.Windows.Forms.Label lblResposta;
        private System.Windows.Forms.TextBox txtResposta;
        private System.Windows.Forms.Label lblPergunta;
        private System.Windows.Forms.CheckBox chkAtivo;
    }
}