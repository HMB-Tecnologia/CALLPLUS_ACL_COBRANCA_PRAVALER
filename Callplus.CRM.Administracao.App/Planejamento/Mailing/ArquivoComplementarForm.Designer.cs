namespace Callplus.CRM.Administracao.App.Planejamento.Mailing
{
	partial class ArquivoComplementarForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArquivoComplementarForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCampanha = new System.Windows.Forms.TextBox();
			this.cmbMailing = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnCarregar = new System.Windows.Forms.Button();
			this.btnCarregarArquivoCaixa = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCaminhoDoArquivoCaixa = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnCarregarArquivoD = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCaminhoDoArquivoD = new System.Windows.Forms.TextBox();
			this.btnSalvar = new System.Windows.Forms.Button();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.tcPrincipal = new System.Windows.Forms.TabControl();
			this.tcPrincipal_tpArquivos = new System.Windows.Forms.TabPage();
			this.tcPrincipal_tpHistorico = new System.Windows.Forms.TabPage();
			this.gbDados = new System.Windows.Forms.GroupBox();
			this.label31 = new System.Windows.Forms.Label();
			this.txtObservacoes_historico = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.txtDataAuditoria_historico = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.txtAuditor_historico = new System.Windows.Forms.TextBox();
			this.dgHistorico = new System.Windows.Forms.DataGridView();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tcPrincipal.SuspendLayout();
			this.tcPrincipal_tpArquivos.SuspendLayout();
			this.tcPrincipal_tpHistorico.SuspendLayout();
			this.gbDados.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgHistorico)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtCampanha);
			this.groupBox1.Controls.Add(this.cmbMailing);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(6, 41);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(452, 113);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Dados Cadastrais";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Campanha";
			// 
			// txtCampanha
			// 
			this.txtCampanha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCampanha.Location = new System.Drawing.Point(18, 81);
			this.txtCampanha.MaxLength = 100;
			this.txtCampanha.Name = "txtCampanha";
			this.txtCampanha.ReadOnly = true;
			this.txtCampanha.Size = new System.Drawing.Size(419, 20);
			this.txtCampanha.TabIndex = 10;
			// 
			// cmbMailing
			// 
			this.cmbMailing.FormattingEnabled = true;
			this.cmbMailing.Location = new System.Drawing.Point(18, 37);
			this.cmbMailing.Name = "cmbMailing";
			this.cmbMailing.Size = new System.Drawing.Size(419, 21);
			this.cmbMailing.TabIndex = 7;
			this.cmbMailing.SelectionChangeCommitted += new System.EventHandler(this.cmbMailing_SelectionChangeCommitted);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 21);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "Mailing";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Controls.Add(this.btnCarregar);
			this.groupBox2.Controls.Add(this.btnCarregarArquivoCaixa);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtCaminhoDoArquivoCaixa);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.btnCarregarArquivoD);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.txtCaminhoDoArquivoD);
			this.groupBox2.Location = new System.Drawing.Point(6, 154);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(452, 312);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Carregamento";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(18, 149);
			this.textBox1.MaxLength = 100;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(419, 157);
			this.textBox1.TabIndex = 57;
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
			this.btnCarregar.Location = new System.Drawing.Point(160, 112);
			this.btnCarregar.Name = "btnCarregar";
			this.btnCarregar.Size = new System.Drawing.Size(125, 25);
			this.btnCarregar.TabIndex = 8;
			this.btnCarregar.Text = "Carregar Arquivos";
			this.btnCarregar.UseVisualStyleBackColor = true;
			this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
			// 
			// btnCarregarArquivoCaixa
			// 
			this.btnCarregarArquivoCaixa.BackColor = System.Drawing.SystemColors.Control;
			this.btnCarregarArquivoCaixa.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCarregarArquivoCaixa.Enabled = false;
			this.btnCarregarArquivoCaixa.FlatAppearance.BorderSize = 0;
			this.btnCarregarArquivoCaixa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnCarregarArquivoCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCarregarArquivoCaixa.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCarregarArquivoCaixa.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search2;
			this.btnCarregarArquivoCaixa.Location = new System.Drawing.Point(406, 84);
			this.btnCarregarArquivoCaixa.Name = "btnCarregarArquivoCaixa";
			this.btnCarregarArquivoCaixa.Size = new System.Drawing.Size(31, 22);
			this.btnCarregarArquivoCaixa.TabIndex = 7;
			this.btnCarregarArquivoCaixa.UseVisualStyleBackColor = true;
			this.btnCarregarArquivoCaixa.Click += new System.EventHandler(this.btnCarregarArquivoCaixa_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(115, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Carregar Arquivo Caixa";
			// 
			// txtCaminhoDoArquivoCaixa
			// 
			this.txtCaminhoDoArquivoCaixa.Location = new System.Drawing.Point(18, 85);
			this.txtCaminhoDoArquivoCaixa.MaxLength = 100;
			this.txtCaminhoDoArquivoCaixa.Name = "txtCaminhoDoArquivoCaixa";
			this.txtCaminhoDoArquivoCaixa.ReadOnly = true;
			this.txtCaminhoDoArquivoCaixa.Size = new System.Drawing.Size(382, 20);
			this.txtCaminhoDoArquivoCaixa.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 133);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Observações";
			// 
			// btnCarregarArquivoD
			// 
			this.btnCarregarArquivoD.BackColor = System.Drawing.SystemColors.Control;
			this.btnCarregarArquivoD.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCarregarArquivoD.Enabled = false;
			this.btnCarregarArquivoD.FlatAppearance.BorderSize = 0;
			this.btnCarregarArquivoD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnCarregarArquivoD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCarregarArquivoD.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCarregarArquivoD.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search2;
			this.btnCarregarArquivoD.Location = new System.Drawing.Point(406, 40);
			this.btnCarregarArquivoD.Name = "btnCarregarArquivoD";
			this.btnCarregarArquivoD.Size = new System.Drawing.Size(31, 22);
			this.btnCarregarArquivoD.TabIndex = 5;
			this.btnCarregarArquivoD.UseVisualStyleBackColor = true;
			this.btnCarregarArquivoD.Click += new System.EventHandler(this.btnCarregarArquivoD_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Carregar Arquivo \"D\"";
			// 
			// txtCaminhoDoArquivoD
			// 
			this.txtCaminhoDoArquivoD.Location = new System.Drawing.Point(18, 41);
			this.txtCaminhoDoArquivoD.MaxLength = 100;
			this.txtCaminhoDoArquivoD.Name = "txtCaminhoDoArquivoD";
			this.txtCaminhoDoArquivoD.ReadOnly = true;
			this.txtCaminhoDoArquivoD.Size = new System.Drawing.Size(382, 20);
			this.txtCaminhoDoArquivoD.TabIndex = 4;
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
			this.btnSalvar.Location = new System.Drawing.Point(12, 516);
			this.btnSalvar.Name = "btnSalvar";
			this.btnSalvar.Size = new System.Drawing.Size(93, 25);
			this.btnSalvar.TabIndex = 18;
			this.btnSalvar.Text = "Salvar  ";
			this.btnSalvar.UseVisualStyleBackColor = true;
			this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
			// 
			// lblTitulo
			// 
			this.lblTitulo.AutoSize = true;
			this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitulo.Location = new System.Drawing.Point(6, 13);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(245, 25);
			this.lblTitulo.TabIndex = 22;
			this.lblTitulo.Text = "CARREGAR ARQUIVOS";
			// 
			// tcPrincipal
			// 
			this.tcPrincipal.Controls.Add(this.tcPrincipal_tpArquivos);
			this.tcPrincipal.Controls.Add(this.tcPrincipal_tpHistorico);
			this.tcPrincipal.Location = new System.Drawing.Point(12, 12);
			this.tcPrincipal.Name = "tcPrincipal";
			this.tcPrincipal.SelectedIndex = 0;
			this.tcPrincipal.Size = new System.Drawing.Size(476, 498);
			this.tcPrincipal.TabIndex = 23;
			// 
			// tcPrincipal_tpArquivos
			// 
			this.tcPrincipal_tpArquivos.BackColor = System.Drawing.Color.White;
			this.tcPrincipal_tpArquivos.Controls.Add(this.lblTitulo);
			this.tcPrincipal_tpArquivos.Controls.Add(this.groupBox1);
			this.tcPrincipal_tpArquivos.Controls.Add(this.groupBox2);
			this.tcPrincipal_tpArquivos.Location = new System.Drawing.Point(4, 22);
			this.tcPrincipal_tpArquivos.Name = "tcPrincipal_tpArquivos";
			this.tcPrincipal_tpArquivos.Padding = new System.Windows.Forms.Padding(3);
			this.tcPrincipal_tpArquivos.Size = new System.Drawing.Size(468, 472);
			this.tcPrincipal_tpArquivos.TabIndex = 0;
			this.tcPrincipal_tpArquivos.Text = "Carregar";
			// 
			// tcPrincipal_tpHistorico
			// 
			this.tcPrincipal_tpHistorico.BackColor = System.Drawing.Color.White;
			this.tcPrincipal_tpHistorico.Controls.Add(this.gbDados);
			this.tcPrincipal_tpHistorico.Controls.Add(this.dgHistorico);
			this.tcPrincipal_tpHistorico.Location = new System.Drawing.Point(4, 22);
			this.tcPrincipal_tpHistorico.Name = "tcPrincipal_tpHistorico";
			this.tcPrincipal_tpHistorico.Padding = new System.Windows.Forms.Padding(3);
			this.tcPrincipal_tpHistorico.Size = new System.Drawing.Size(468, 472);
			this.tcPrincipal_tpHistorico.TabIndex = 1;
			this.tcPrincipal_tpHistorico.Text = "Histórico";
			// 
			// gbDados
			// 
			this.gbDados.Controls.Add(this.label31);
			this.gbDados.Controls.Add(this.txtObservacoes_historico);
			this.gbDados.Controls.Add(this.label26);
			this.gbDados.Controls.Add(this.txtDataAuditoria_historico);
			this.gbDados.Controls.Add(this.label18);
			this.gbDados.Controls.Add(this.txtAuditor_historico);
			this.gbDados.Location = new System.Drawing.Point(6, 269);
			this.gbDados.Name = "gbDados";
			this.gbDados.Size = new System.Drawing.Size(452, 197);
			this.gbDados.TabIndex = 51;
			this.gbDados.TabStop = false;
			this.gbDados.Text = "Dados";
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(18, 58);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(70, 13);
			this.label31.TabIndex = 61;
			this.label31.Text = "Observações";
			// 
			// txtObservacoes_historico
			// 
			this.txtObservacoes_historico.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtObservacoes_historico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtObservacoes_historico.Location = new System.Drawing.Point(18, 74);
			this.txtObservacoes_historico.MaxLength = 100;
			this.txtObservacoes_historico.Multiline = true;
			this.txtObservacoes_historico.Name = "txtObservacoes_historico";
			this.txtObservacoes_historico.ReadOnly = true;
			this.txtObservacoes_historico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtObservacoes_historico.Size = new System.Drawing.Size(422, 117);
			this.txtObservacoes_historico.TabIndex = 62;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(287, 17);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(30, 13);
			this.label26.TabIndex = 59;
			this.label26.Text = "Data";
			// 
			// txtDataAuditoria_historico
			// 
			this.txtDataAuditoria_historico.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtDataAuditoria_historico.ForeColor = System.Drawing.Color.Black;
			this.txtDataAuditoria_historico.Location = new System.Drawing.Point(290, 33);
			this.txtDataAuditoria_historico.MaxLength = 100;
			this.txtDataAuditoria_historico.Name = "txtDataAuditoria_historico";
			this.txtDataAuditoria_historico.ReadOnly = true;
			this.txtDataAuditoria_historico.Size = new System.Drawing.Size(150, 20);
			this.txtDataAuditoria_historico.TabIndex = 60;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(18, 17);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(40, 13);
			this.label18.TabIndex = 57;
			this.label18.Text = "Criador";
			// 
			// txtAuditor_historico
			// 
			this.txtAuditor_historico.BackColor = System.Drawing.Color.WhiteSmoke;
			this.txtAuditor_historico.ForeColor = System.Drawing.Color.Black;
			this.txtAuditor_historico.Location = new System.Drawing.Point(18, 33);
			this.txtAuditor_historico.MaxLength = 100;
			this.txtAuditor_historico.Name = "txtAuditor_historico";
			this.txtAuditor_historico.ReadOnly = true;
			this.txtAuditor_historico.Size = new System.Drawing.Size(269, 20);
			this.txtAuditor_historico.TabIndex = 58;
			// 
			// dgHistorico
			// 
			this.dgHistorico.AllowUserToAddRows = false;
			this.dgHistorico.AllowUserToDeleteRows = false;
			this.dgHistorico.AllowUserToResizeRows = false;
			this.dgHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgHistorico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgHistorico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgHistorico.EnableHeadersVisualStyles = false;
			this.dgHistorico.Location = new System.Drawing.Point(6, 6);
			this.dgHistorico.MultiSelect = false;
			this.dgHistorico.Name = "dgHistorico";
			this.dgHistorico.ReadOnly = true;
			this.dgHistorico.RowHeadersVisible = false;
			this.dgHistorico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgHistorico.Size = new System.Drawing.Size(456, 257);
			this.dgHistorico.TabIndex = 50;
			// 
			// ArquivoComplementarForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(494, 553);
			this.Controls.Add(this.tcPrincipal);
			this.Controls.Add(this.btnSalvar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ArquivoComplementarForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arquivos Complementares";
			this.Load += new System.EventHandler(this.ArquivoComplementarForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tcPrincipal.ResumeLayout(false);
			this.tcPrincipal_tpArquivos.ResumeLayout(false);
			this.tcPrincipal_tpArquivos.PerformLayout();
			this.tcPrincipal_tpHistorico.ResumeLayout(false);
			this.gbDados.ResumeLayout(false);
			this.gbDados.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgHistorico)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbMailing;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCampanha;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnCarregar;
		private System.Windows.Forms.Button btnCarregarArquivoCaixa;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCaminhoDoArquivoCaixa;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnCarregarArquivoD;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCaminhoDoArquivoD;
		private System.Windows.Forms.Button btnSalvar;
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.TabControl tcPrincipal;
		private System.Windows.Forms.TabPage tcPrincipal_tpArquivos;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TabPage tcPrincipal_tpHistorico;
		private System.Windows.Forms.GroupBox gbDados;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.TextBox txtObservacoes_historico;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox txtDataAuditoria_historico;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtAuditor_historico;
		private System.Windows.Forms.DataGridView dgHistorico;
	}
}