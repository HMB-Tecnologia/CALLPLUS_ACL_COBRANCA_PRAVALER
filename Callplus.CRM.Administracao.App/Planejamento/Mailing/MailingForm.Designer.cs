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
			this.cmbCampanha = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.chkAtivo = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbCampanhaArquivoMarcacoes = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCarregar = new System.Windows.Forms.Button();
			this.pbProcessar = new System.Windows.Forms.ProgressBar();
			this.btnCarregarArquivoMarcacoes = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCaminhoDoArquivoMarcacoes = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtObservacao = new System.Windows.Forms.TextBox();
			this.btnCarregarArquivoMailing = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCaminhoDoArquivoMailing = new System.Windows.Forms.TextBox();
			this.lblDiscador = new System.Windows.Forms.Label();
			this.cmdEnviarParaDiscador = new System.Windows.Forms.Button();
			this.cmdExportarArquivo = new System.Windows.Forms.Button();
			this.btnSalvar = new System.Windows.Forms.Button();
			this.lblPorcentagemMailing = new System.Windows.Forms.Label();
			this.lblPorcentagemMarcacoesMailing = new System.Windows.Forms.Label();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cmbCampanha);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.chkAtivo);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txtNome);
			this.groupBox3.Location = new System.Drawing.Point(12, 37);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(452, 113);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Dados Cadastrais";
			// 
			// cmbCampanha
			// 
			this.cmbCampanha.FormattingEnabled = true;
			this.cmbCampanha.Location = new System.Drawing.Point(18, 43);
			this.cmbCampanha.Name = "cmbCampanha";
			this.cmbCampanha.Size = new System.Drawing.Size(419, 21);
			this.cmbCampanha.TabIndex = 1;
			this.cmbCampanha.SelectionChangeCommitted += new System.EventHandler(this.cmbCampanha_SelectionChangeCommitted);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 27);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(58, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Campanha";
			// 
			// chkAtivo
			// 
			this.chkAtivo.AutoSize = true;
			this.chkAtivo.Location = new System.Drawing.Point(387, 85);
			this.chkAtivo.Name = "chkAtivo";
			this.chkAtivo.Size = new System.Drawing.Size(50, 17);
			this.chkAtivo.TabIndex = 3;
			this.chkAtivo.Text = "Ativo";
			this.chkAtivo.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Nome";
			// 
			// txtNome
			// 
			this.txtNome.Location = new System.Drawing.Point(18, 83);
			this.txtNome.MaxLength = 100;
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(363, 20);
			this.txtNome.TabIndex = 2;
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
			this.groupBox1.Controls.Add(this.cmbCampanhaArquivoMarcacoes);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnCarregar);
			this.groupBox1.Controls.Add(this.pbProcessar);
			this.groupBox1.Controls.Add(this.btnCarregarArquivoMarcacoes);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtCaminhoDoArquivoMarcacoes);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtObservacao);
			this.groupBox1.Controls.Add(this.btnCarregarArquivoMailing);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtCaminhoDoArquivoMailing);
			this.groupBox1.Location = new System.Drawing.Point(12, 156);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(452, 303);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Carregamento";
			// 
			// cmbCampanhaArquivoMarcacoes
			// 
			this.cmbCampanhaArquivoMarcacoes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.cmbCampanhaArquivoMarcacoes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbCampanhaArquivoMarcacoes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCampanhaArquivoMarcacoes.Enabled = false;
			this.cmbCampanhaArquivoMarcacoes.FormattingEnabled = true;
			this.cmbCampanhaArquivoMarcacoes.Location = new System.Drawing.Point(18, 129);
			this.cmbCampanhaArquivoMarcacoes.Name = "cmbCampanhaArquivoMarcacoes";
			this.cmbCampanhaArquivoMarcacoes.Size = new System.Drawing.Size(419, 21);
			this.cmbCampanhaArquivoMarcacoes.TabIndex = 9;
			this.cmbCampanhaArquivoMarcacoes.SelectionChangeCommitted += new System.EventHandler(this.cmbCampanhaArquivoMarcacoes_SelectionChangeCommitted);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 113);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(183, 13);
			this.label1.TabIndex = 17;
			this.label1.Text = "Campanha do Arquivo de Marcações";
			// 
			// btnCarregar
			// 
			this.btnCarregar.BackColor = System.Drawing.SystemColors.Control;
			this.btnCarregar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCarregar.Enabled = false;
			this.btnCarregar.FlatAppearance.BorderSize = 0;
			this.btnCarregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnCarregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCarregar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCarregar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.STodos;
			this.btnCarregar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCarregar.Location = new System.Drawing.Point(160, 156);
			this.btnCarregar.Name = "btnCarregar";
			this.btnCarregar.Size = new System.Drawing.Size(125, 25);
			this.btnCarregar.TabIndex = 8;
			this.btnCarregar.Text = "Carregar Arquivos";
			this.btnCarregar.UseVisualStyleBackColor = true;
			this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
			// 
			// pbProcessar
			// 
			this.pbProcessar.Location = new System.Drawing.Point(18, 270);
			this.pbProcessar.Name = "pbProcessar";
			this.pbProcessar.Size = new System.Drawing.Size(419, 23);
			this.pbProcessar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbProcessar.TabIndex = 11;
			// 
			// btnCarregarArquivoMarcacoes
			// 
			this.btnCarregarArquivoMarcacoes.BackColor = System.Drawing.SystemColors.Control;
			this.btnCarregarArquivoMarcacoes.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCarregarArquivoMarcacoes.Enabled = false;
			this.btnCarregarArquivoMarcacoes.FlatAppearance.BorderSize = 0;
			this.btnCarregarArquivoMarcacoes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnCarregarArquivoMarcacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCarregarArquivoMarcacoes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCarregarArquivoMarcacoes.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search2;
			this.btnCarregarArquivoMarcacoes.Location = new System.Drawing.Point(406, 84);
			this.btnCarregarArquivoMarcacoes.Name = "btnCarregarArquivoMarcacoes";
			this.btnCarregarArquivoMarcacoes.Size = new System.Drawing.Size(31, 22);
			this.btnCarregarArquivoMarcacoes.TabIndex = 7;
			this.btnCarregarArquivoMarcacoes.UseVisualStyleBackColor = true;
			this.btnCarregarArquivoMarcacoes.Click += new System.EventHandler(this.cmdCarregarArquivoMarcacoes_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(157, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Carregar Arquivo de Marcações";
			// 
			// txtCaminhoDoArquivoMarcacoes
			// 
			this.txtCaminhoDoArquivoMarcacoes.Location = new System.Drawing.Point(18, 85);
			this.txtCaminhoDoArquivoMarcacoes.MaxLength = 100;
			this.txtCaminhoDoArquivoMarcacoes.Name = "txtCaminhoDoArquivoMarcacoes";
			this.txtCaminhoDoArquivoMarcacoes.ReadOnly = true;
			this.txtCaminhoDoArquivoMarcacoes.Size = new System.Drawing.Size(382, 20);
			this.txtCaminhoDoArquivoMarcacoes.TabIndex = 6;
			this.txtCaminhoDoArquivoMarcacoes.TextChanged += new System.EventHandler(this.txtCaminhoDoArquivoMarcacoes_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 189);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Análise do Arquivo";
			// 
			// txtObservacao
			// 
			this.txtObservacao.Location = new System.Drawing.Point(18, 205);
			this.txtObservacao.MaxLength = 100;
			this.txtObservacao.Multiline = true;
			this.txtObservacao.Name = "txtObservacao";
			this.txtObservacao.ReadOnly = true;
			this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtObservacao.Size = new System.Drawing.Size(419, 59);
			this.txtObservacao.TabIndex = 10;
			// 
			// btnCarregarArquivoMailing
			// 
			this.btnCarregarArquivoMailing.BackColor = System.Drawing.SystemColors.Control;
			this.btnCarregarArquivoMailing.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCarregarArquivoMailing.Enabled = false;
			this.btnCarregarArquivoMailing.FlatAppearance.BorderSize = 0;
			this.btnCarregarArquivoMailing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnCarregarArquivoMailing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCarregarArquivoMailing.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCarregarArquivoMailing.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search2;
			this.btnCarregarArquivoMailing.Location = new System.Drawing.Point(406, 40);
			this.btnCarregarArquivoMailing.Name = "btnCarregarArquivoMailing";
			this.btnCarregarArquivoMailing.Size = new System.Drawing.Size(31, 22);
			this.btnCarregarArquivoMailing.TabIndex = 5;
			this.btnCarregarArquivoMailing.UseVisualStyleBackColor = true;
			this.btnCarregarArquivoMailing.Click += new System.EventHandler(this.cmdCarregarArquivoMailing_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(137, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Carregar Arquivo de Mailing";
			// 
			// txtCaminhoDoArquivoMailing
			// 
			this.txtCaminhoDoArquivoMailing.Location = new System.Drawing.Point(18, 41);
			this.txtCaminhoDoArquivoMailing.MaxLength = 100;
			this.txtCaminhoDoArquivoMailing.Name = "txtCaminhoDoArquivoMailing";
			this.txtCaminhoDoArquivoMailing.ReadOnly = true;
			this.txtCaminhoDoArquivoMailing.Size = new System.Drawing.Size(382, 20);
			this.txtCaminhoDoArquivoMailing.TabIndex = 4;
			// 
			// lblDiscador
			// 
			this.lblDiscador.AutoSize = true;
			this.lblDiscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDiscador.Location = new System.Drawing.Point(11, 502);
			this.lblDiscador.Name = "lblDiscador";
			this.lblDiscador.Size = new System.Drawing.Size(164, 17);
			this.lblDiscador.TabIndex = 13;
			this.lblDiscador.Text = "Discador da Campanha: ";
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
			this.cmdEnviarParaDiscador.Location = new System.Drawing.Point(226, 522);
			this.cmdEnviarParaDiscador.Name = "cmdEnviarParaDiscador";
			this.cmdEnviarParaDiscador.Size = new System.Drawing.Size(238, 25);
			this.cmdEnviarParaDiscador.TabIndex = 14;
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
			this.cmdExportarArquivo.Location = new System.Drawing.Point(12, 522);
			this.cmdExportarArquivo.Name = "cmdExportarArquivo";
			this.cmdExportarArquivo.Size = new System.Drawing.Size(208, 25);
			this.cmdExportarArquivo.TabIndex = 13;
			this.cmdExportarArquivo.Text = "Exportar para layout do discador   ";
			this.cmdExportarArquivo.UseVisualStyleBackColor = true;
			this.cmdExportarArquivo.Click += new System.EventHandler(this.cmdExportarArquivo_Click);
			// 
			// btnSalvar
			// 
			this.btnSalvar.BackColor = System.Drawing.SystemColors.Control;
			this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSalvar.Enabled = false;
			this.btnSalvar.FlatAppearance.BorderSize = 0;
			this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSalvar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSalvar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
			this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSalvar.Location = new System.Drawing.Point(12, 466);
			this.btnSalvar.Name = "btnSalvar";
			this.btnSalvar.Size = new System.Drawing.Size(93, 25);
			this.btnSalvar.TabIndex = 12;
			this.btnSalvar.Text = "Salvar  ";
			this.btnSalvar.UseVisualStyleBackColor = true;
			this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
			// 
			// lblPorcentagemMailing
			// 
			this.lblPorcentagemMailing.AutoSize = true;
			this.lblPorcentagemMailing.Location = new System.Drawing.Point(334, 466);
			this.lblPorcentagemMailing.Name = "lblPorcentagemMailing";
			this.lblPorcentagemMailing.Size = new System.Drawing.Size(98, 13);
			this.lblPorcentagemMailing.TabIndex = 16;
			this.lblPorcentagemMailing.Text = "Carregando Mailing";
			this.lblPorcentagemMailing.Visible = false;
			// 
			// lblPorcentagemMarcacoesMailing
			// 
			this.lblPorcentagemMarcacoesMailing.AutoSize = true;
			this.lblPorcentagemMarcacoesMailing.Location = new System.Drawing.Point(314, 487);
			this.lblPorcentagemMarcacoesMailing.Name = "lblPorcentagemMarcacoesMailing";
			this.lblPorcentagemMarcacoesMailing.Size = new System.Drawing.Size(118, 13);
			this.lblPorcentagemMarcacoesMailing.TabIndex = 17;
			this.lblPorcentagemMarcacoesMailing.Text = "Carregando Marcações";
			this.lblPorcentagemMarcacoesMailing.Visible = false;
			// 
			// MailingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(476, 553);
			this.Controls.Add(this.lblPorcentagemMarcacoesMailing);
			this.Controls.Add(this.lblPorcentagemMailing);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnCarregarArquivoMailing;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCaminhoDoArquivoMailing;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button cmdEnviarParaDiscador;
        private System.Windows.Forms.Button cmdExportarArquivo;
        private System.Windows.Forms.Label lblDiscador;
		private System.Windows.Forms.Button btnCarregarArquivoMarcacoes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCaminhoDoArquivoMarcacoes;
		private System.Windows.Forms.ProgressBar pbProcessar;
		private System.Windows.Forms.Button btnCarregar;
		private System.Windows.Forms.Label lblPorcentagemMailing;
		private System.Windows.Forms.Label lblPorcentagemMarcacoesMailing;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbCampanha;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbCampanhaArquivoMarcacoes;
	}
}