namespace Callplus.CRM.Administracao.App.Planejamento.Mailing
{
    partial class MailingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailingForm));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIndicacao = new System.Windows.Forms.CheckBox();
            this.cmbCampanha = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.cmdCarregarArquivo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCaminhoDoArquivo = new System.Windows.Forms.TextBox();
            this.lblDiscador = new System.Windows.Forms.Label();
            this.cmdEnviarParaDiscador = new System.Windows.Forms.Button();
            this.cmdExportarArquivo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkIndicacao);
            this.groupBox3.Controls.Add(this.cmbCampanha);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.chkAtivo);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtNome);
            this.groupBox3.Location = new System.Drawing.Point(12, 37);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(452, 143);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dados Cadastrais";
            // 
            // chkIndicacao
            // 
            this.chkIndicacao.AutoSize = true;
            this.chkIndicacao.Location = new System.Drawing.Point(74, 115);
            this.chkIndicacao.Name = "chkIndicacao";
            this.chkIndicacao.Size = new System.Drawing.Size(73, 17);
            this.chkIndicacao.TabIndex = 6;
            this.chkIndicacao.Text = "Indicação";
            this.chkIndicacao.UseVisualStyleBackColor = true;
            // 
            // cmbCampanha
            // 
            this.cmbCampanha.FormattingEnabled = true;
            this.cmbCampanha.Location = new System.Drawing.Point(18, 39);
            this.cmbCampanha.Name = "cmbCampanha";
            this.cmbCampanha.Size = new System.Drawing.Size(419, 21);
            this.cmbCampanha.TabIndex = 1;
            this.cmbCampanha.SelectionChangeCommitted += new System.EventHandler(this.cmbCampanha_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Campanha";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(18, 115);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 5;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(18, 84);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(419, 20);
            this.txtNome.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(9, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(97, 25);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "MAILING";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtObservacao);
            this.groupBox1.Controls.Add(this.cmdCarregarArquivo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCaminhoDoArquivo);
            this.groupBox1.Location = new System.Drawing.Point(12, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 284);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Carregamento";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Análise do Arquivo";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(18, 84);
            this.txtObservacao.MaxLength = 100;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(419, 182);
            this.txtObservacao.TabIndex = 10;
            // 
            // cmdCarregarArquivo
            // 
            this.cmdCarregarArquivo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCarregarArquivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCarregarArquivo.Enabled = false;
            this.cmdCarregarArquivo.FlatAppearance.BorderSize = 0;
            this.cmdCarregarArquivo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.cmdCarregarArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCarregarArquivo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdCarregarArquivo.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search2;
            this.cmdCarregarArquivo.Location = new System.Drawing.Point(406, 40);
            this.cmdCarregarArquivo.Name = "cmdCarregarArquivo";
            this.cmdCarregarArquivo.Size = new System.Drawing.Size(31, 22);
            this.cmdCarregarArquivo.TabIndex = 8;
            this.cmdCarregarArquivo.UseVisualStyleBackColor = true;
            this.cmdCarregarArquivo.Click += new System.EventHandler(this.cmdCarregarArquivo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Arquivo";
            // 
            // txtCaminhoDoArquivo
            // 
            this.txtCaminhoDoArquivo.Location = new System.Drawing.Point(18, 41);
            this.txtCaminhoDoArquivo.MaxLength = 100;
            this.txtCaminhoDoArquivo.Name = "txtCaminhoDoArquivo";
            this.txtCaminhoDoArquivo.ReadOnly = true;
            this.txtCaminhoDoArquivo.Size = new System.Drawing.Size(382, 20);
            this.txtCaminhoDoArquivo.TabIndex = 7;
            // 
            // lblDiscador
            // 
            this.lblDiscador.AutoSize = true;
            this.lblDiscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscador.Location = new System.Drawing.Point(9, 522);
            this.lblDiscador.Name = "lblDiscador";
            this.lblDiscador.Size = new System.Drawing.Size(198, 17);
            this.lblDiscador.TabIndex = 13;
            this.lblDiscador.Text = "Discador da Campanha: Akiva";
            // 
            // cmdEnviarParaDiscador
            // 
            this.cmdEnviarParaDiscador.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEnviarParaDiscador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdEnviarParaDiscador.Enabled = false;
            this.cmdEnviarParaDiscador.FlatAppearance.BorderSize = 0;
            this.cmdEnviarParaDiscador.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.cmdEnviarParaDiscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEnviarParaDiscador.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdEnviarParaDiscador.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.Sync;
            this.cmdEnviarParaDiscador.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdEnviarParaDiscador.Location = new System.Drawing.Point(226, 549);
            this.cmdEnviarParaDiscador.Name = "cmdEnviarParaDiscador";
            this.cmdEnviarParaDiscador.Size = new System.Drawing.Size(238, 25);
            this.cmdEnviarParaDiscador.TabIndex = 15;
            this.cmdEnviarParaDiscador.Text = "Enviar automaticamente para o discador   ";
            this.cmdEnviarParaDiscador.UseVisualStyleBackColor = true;
            this.cmdEnviarParaDiscador.Click += new System.EventHandler(this.cmdEnviarParaDiscador_Click);
            // 
            // cmdExportarArquivo
            // 
            this.cmdExportarArquivo.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExportarArquivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdExportarArquivo.Enabled = false;
            this.cmdExportarArquivo.FlatAppearance.BorderSize = 0;
            this.cmdExportarArquivo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.cmdExportarArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportarArquivo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdExportarArquivo.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.import;
            this.cmdExportarArquivo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExportarArquivo.Location = new System.Drawing.Point(12, 549);
            this.cmdExportarArquivo.Name = "cmdExportarArquivo";
            this.cmdExportarArquivo.Size = new System.Drawing.Size(208, 25);
            this.cmdExportarArquivo.TabIndex = 14;
            this.cmdExportarArquivo.Text = "Exportar para layout do discador   ";
            this.cmdExportarArquivo.UseVisualStyleBackColor = true;
            this.cmdExportarArquivo.Click += new System.EventHandler(this.cmdExportarArquivo_Click);
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 476);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 10;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // MailingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(476, 584);
            this.Controls.Add(this.cmdEnviarParaDiscador);
            this.Controls.Add(this.cmdExportarArquivo);
            this.Controls.Add(this.lblDiscador);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailingForm";
            this.Text = "Mailing";
            this.Load += new System.EventHandler(this.MailingForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.CheckBox chkIndicacao;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button cmdCarregarArquivo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCaminhoDoArquivo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button cmdEnviarParaDiscador;
        private System.Windows.Forms.Button cmdExportarArquivo;
        private System.Windows.Forms.Label lblDiscador;
    }
}