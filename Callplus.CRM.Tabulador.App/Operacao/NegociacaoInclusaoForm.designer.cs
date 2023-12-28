namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class NegociacaoInclusaoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NegociacaoInclusaoForm));
			this.panel3 = new System.Windows.Forms.Panel();
			this.tsAcordo = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.tsAcordo_cmbStatusAcordo = new System.Windows.Forms.ToolStripComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnSalvarNegociacao = new System.Windows.Forms.Button();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.gbDetalheDoAcordo = new System.Windows.Forms.GroupBox();
			this.dtpDataVencimento = new System.Windows.Forms.DateTimePicker();
			this.lblPrazo = new System.Windows.Forms.Label();
			this.cmbPrazo = new System.Windows.Forms.ComboBox();
			this.txtValorPrincipal = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.cmbParcela = new System.Windows.Forms.ComboBox();
			this.txtValorParcelas = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtNomeCliente = new System.Windows.Forms.TextBox();
			this.lblIdContrato = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtIDContrato = new System.Windows.Forms.TextBox();
			this.gbContrato = new System.Windows.Forms.GroupBox();
			this.btnRemoverParcelas = new System.Windows.Forms.Button();
			this.btnAdcionarParcela = new System.Windows.Forms.Button();
			this.dgParcelas = new System.Windows.Forms.DataGridView();
			this.colParcelaParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVencimentoParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colValorPrincipalParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colValordaParcelaParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3.SuspendLayout();
			this.tsAcordo.SuspendLayout();
			this.panel1.SuspendLayout();
			this.gbDetalheDoAcordo.SuspendLayout();
			this.gbContrato.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgParcelas)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.tsAcordo);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(744, 27);
			this.panel3.TabIndex = 189;
			// 
			// tsAcordo
			// 
			this.tsAcordo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tsAcordo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAcordo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tsAcordo_cmbStatusAcordo});
			this.tsAcordo.Location = new System.Drawing.Point(0, 0);
			this.tsAcordo.Name = "tsAcordo";
			this.tsAcordo.Padding = new System.Windows.Forms.Padding(2);
			this.tsAcordo.Size = new System.Drawing.Size(744, 27);
			this.tsAcordo.TabIndex = 1;
			this.tsAcordo.Text = "toolStrip2";
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(91, 20);
			this.toolStripLabel3.Text = "Tipo de Acordo:";
			// 
			// tsAcordo_cmbStatusAcordo
			// 
			this.tsAcordo_cmbStatusAcordo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsAcordo_cmbStatusAcordo.DropDownWidth = 400;
			this.tsAcordo_cmbStatusAcordo.Enabled = false;
			this.tsAcordo_cmbStatusAcordo.IntegralHeight = false;
			this.tsAcordo_cmbStatusAcordo.Name = "tsAcordo_cmbStatusAcordo";
			this.tsAcordo_cmbStatusAcordo.Size = new System.Drawing.Size(400, 23);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnSalvarNegociacao);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 379);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(744, 48);
			this.panel1.TabIndex = 190;
			// 
			// btnSalvarNegociacao
			// 
			this.btnSalvarNegociacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSalvarNegociacao.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.save;
			this.btnSalvarNegociacao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSalvarNegociacao.Location = new System.Drawing.Point(12, 11);
			this.btnSalvarNegociacao.Name = "btnSalvarNegociacao";
			this.btnSalvarNegociacao.Size = new System.Drawing.Size(90, 25);
			this.btnSalvarNegociacao.TabIndex = 11;
			this.btnSalvarNegociacao.Text = "Salvar";
			this.btnSalvarNegociacao.UseVisualStyleBackColor = true;
			this.btnSalvarNegociacao.Click += new System.EventHandler(this.btnSalvarNegociacao_Click);
			// 
			// panel5
			// 
			this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel5.Location = new System.Drawing.Point(0, 369);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(744, 10);
			this.panel5.TabIndex = 194;
			// 
			// panel4
			// 
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 27);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(744, 10);
			this.panel4.TabIndex = 193;
			// 
			// gbDetalheDoAcordo
			// 
			this.gbDetalheDoAcordo.Controls.Add(this.dtpDataVencimento);
			this.gbDetalheDoAcordo.Controls.Add(this.lblPrazo);
			this.gbDetalheDoAcordo.Controls.Add(this.cmbPrazo);
			this.gbDetalheDoAcordo.Controls.Add(this.txtValorPrincipal);
			this.gbDetalheDoAcordo.Controls.Add(this.label13);
			this.gbDetalheDoAcordo.Controls.Add(this.label12);
			this.gbDetalheDoAcordo.Controls.Add(this.cmbParcela);
			this.gbDetalheDoAcordo.Controls.Add(this.txtValorParcelas);
			this.gbDetalheDoAcordo.Controls.Add(this.label14);
			this.gbDetalheDoAcordo.Controls.Add(this.label11);
			this.gbDetalheDoAcordo.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbDetalheDoAcordo.Location = new System.Drawing.Point(0, 94);
			this.gbDetalheDoAcordo.Name = "gbDetalheDoAcordo";
			this.gbDetalheDoAcordo.Size = new System.Drawing.Size(744, 59);
			this.gbDetalheDoAcordo.TabIndex = 196;
			this.gbDetalheDoAcordo.TabStop = false;
			this.gbDetalheDoAcordo.Text = "Detalhes do Acordo/Parcelas";
			// 
			// dtpDataVencimento
			// 
			this.dtpDataVencimento.CustomFormat = "dd/MM/yyyy";
			this.dtpDataVencimento.Location = new System.Drawing.Point(530, 33);
			this.dtpDataVencimento.Name = "dtpDataVencimento";
			this.dtpDataVencimento.Size = new System.Drawing.Size(209, 20);
			this.dtpDataVencimento.TabIndex = 7;
			// 
			// lblPrazo
			// 
			this.lblPrazo.AutoSize = true;
			this.lblPrazo.Location = new System.Drawing.Point(397, 17);
			this.lblPrazo.Name = "lblPrazo";
			this.lblPrazo.Size = new System.Drawing.Size(37, 13);
			this.lblPrazo.TabIndex = 161;
			this.lblPrazo.Text = "Prazo:";
			// 
			// cmbPrazo
			// 
			this.cmbPrazo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPrazo.FormattingEnabled = true;
			this.cmbPrazo.ItemHeight = 13;
			this.cmbPrazo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21"});
			this.cmbPrazo.Location = new System.Drawing.Point(399, 33);
			this.cmbPrazo.Name = "cmbPrazo";
			this.cmbPrazo.Size = new System.Drawing.Size(125, 21);
			this.cmbPrazo.TabIndex = 6;
			// 
			// txtValorPrincipal
			// 
			this.txtValorPrincipal.Location = new System.Drawing.Point(6, 33);
			this.txtValorPrincipal.Name = "txtValorPrincipal";
			this.txtValorPrincipal.ReadOnly = true;
			this.txtValorPrincipal.Size = new System.Drawing.Size(125, 20);
			this.txtValorPrincipal.TabIndex = 3;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(135, 17);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(51, 13);
			this.label13.TabIndex = 159;
			this.label13.Text = "Parcelas:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(265, 17);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(98, 13);
			this.label12.TabIndex = 6;
			this.label12.Text = "Valor das Parcelas:";
			// 
			// cmbParcela
			// 
			this.cmbParcela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParcela.FormattingEnabled = true;
			this.cmbParcela.ItemHeight = 13;
			this.cmbParcela.Location = new System.Drawing.Point(137, 33);
			this.cmbParcela.Name = "cmbParcela";
			this.cmbParcela.Size = new System.Drawing.Size(125, 21);
			this.cmbParcela.TabIndex = 4;
			this.cmbParcela.SelectedIndexChanged += new System.EventHandler(this.cmbParcela_SelectedIndexChanged);
			// 
			// txtValorParcelas
			// 
			this.txtValorParcelas.Location = new System.Drawing.Point(268, 33);
			this.txtValorParcelas.Name = "txtValorParcelas";
			this.txtValorParcelas.ReadOnly = true;
			this.txtValorParcelas.Size = new System.Drawing.Size(125, 20);
			this.txtValorParcelas.TabIndex = 5;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 17);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(77, 13);
			this.label14.TabIndex = 156;
			this.label14.Text = "Valor Principal:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(527, 17);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(107, 13);
			this.label11.TabIndex = 159;
			this.label11.Text = "Data de Vencimento:";
			// 
			// txtNomeCliente
			// 
			this.txtNomeCliente.Location = new System.Drawing.Point(175, 30);
			this.txtNomeCliente.Name = "txtNomeCliente";
			this.txtNomeCliente.ReadOnly = true;
			this.txtNomeCliente.Size = new System.Drawing.Size(319, 20);
			this.txtNomeCliente.TabIndex = 2;
			// 
			// lblIdContrato
			// 
			this.lblIdContrato.AutoSize = true;
			this.lblIdContrato.Location = new System.Drawing.Point(6, 14);
			this.lblIdContrato.Name = "lblIdContrato";
			this.lblIdContrato.Size = new System.Drawing.Size(21, 13);
			this.lblIdContrato.TabIndex = 3;
			this.lblIdContrato.Text = "ID:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(172, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Nome do Cliente";
			// 
			// txtIDContrato
			// 
			this.txtIDContrato.Location = new System.Drawing.Point(9, 30);
			this.txtIDContrato.Name = "txtIDContrato";
			this.txtIDContrato.ReadOnly = true;
			this.txtIDContrato.Size = new System.Drawing.Size(160, 20);
			this.txtIDContrato.TabIndex = 1;
			// 
			// gbContrato
			// 
			this.gbContrato.Controls.Add(this.txtNomeCliente);
			this.gbContrato.Controls.Add(this.lblIdContrato);
			this.gbContrato.Controls.Add(this.label5);
			this.gbContrato.Controls.Add(this.txtIDContrato);
			this.gbContrato.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbContrato.Location = new System.Drawing.Point(0, 37);
			this.gbContrato.Name = "gbContrato";
			this.gbContrato.Size = new System.Drawing.Size(744, 57);
			this.gbContrato.TabIndex = 195;
			this.gbContrato.TabStop = false;
			this.gbContrato.Text = "Contrato";
			// 
			// btnRemoverParcelas
			// 
			this.btnRemoverParcelas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoverParcelas.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.close;
			this.btnRemoverParcelas.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRemoverParcelas.Location = new System.Drawing.Point(651, 6);
			this.btnRemoverParcelas.Name = "btnRemoverParcelas";
			this.btnRemoverParcelas.Size = new System.Drawing.Size(90, 25);
			this.btnRemoverParcelas.TabIndex = 9;
			this.btnRemoverParcelas.Text = "Limpar";
			this.btnRemoverParcelas.UseVisualStyleBackColor = true;
			this.btnRemoverParcelas.Click += new System.EventHandler(this.btnRemoverParcelas_Click);
			// 
			// btnAdcionarParcela
			// 
			this.btnAdcionarParcela.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.add;
			this.btnAdcionarParcela.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnAdcionarParcela.Location = new System.Drawing.Point(6, 6);
			this.btnAdcionarParcela.Name = "btnAdcionarParcela";
			this.btnAdcionarParcela.Size = new System.Drawing.Size(90, 25);
			this.btnAdcionarParcela.TabIndex = 8;
			this.btnAdcionarParcela.Text = "Adicionar";
			this.btnAdcionarParcela.UseVisualStyleBackColor = true;
			this.btnAdcionarParcela.Click += new System.EventHandler(this.btnAdicionarParcela_Click);
			// 
			// dgParcelas
			// 
			this.dgParcelas.AllowUserToAddRows = false;
			this.dgParcelas.AllowUserToDeleteRows = false;
			this.dgParcelas.AllowUserToOrderColumns = true;
			this.dgParcelas.AllowUserToResizeRows = false;
			this.dgParcelas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgParcelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgParcelas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParcelaParcelas,
            this.colVencimentoParcelas,
            this.colValorPrincipalParcelas,
            this.colValordaParcelaParcelas});
			this.dgParcelas.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dgParcelas.Location = new System.Drawing.Point(0, 37);
			this.dgParcelas.Name = "dgParcelas";
			this.dgParcelas.RowHeadersVisible = false;
			this.dgParcelas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgParcelas.Size = new System.Drawing.Size(744, 179);
			this.dgParcelas.TabIndex = 10;
			// 
			// colParcelaParcelas
			// 
			this.colParcelaParcelas.HeaderText = "Parcela";
			this.colParcelaParcelas.Name = "colParcelaParcelas";
			this.colParcelaParcelas.ReadOnly = true;
			// 
			// colVencimentoParcelas
			// 
			this.colVencimentoParcelas.HeaderText = "Vencimento";
			this.colVencimentoParcelas.Name = "colVencimentoParcelas";
			this.colVencimentoParcelas.ReadOnly = true;
			// 
			// colValorPrincipalParcelas
			// 
			this.colValorPrincipalParcelas.HeaderText = "Valor Principal";
			this.colValorPrincipalParcelas.Name = "colValorPrincipalParcelas";
			// 
			// colValordaParcelaParcelas
			// 
			this.colValordaParcelaParcelas.HeaderText = "Valor da Parcela";
			this.colValordaParcelaParcelas.Name = "colValordaParcelaParcelas";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnAdcionarParcela);
			this.panel2.Controls.Add(this.btnRemoverParcelas);
			this.panel2.Controls.Add(this.dgParcelas);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 153);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(744, 216);
			this.panel2.TabIndex = 197;
			// 
			// NegociacaoInclusaoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(744, 427);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.gbDetalheDoAcordo);
			this.Controls.Add(this.gbContrato);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NegociacaoInclusaoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Novo Acordo";
			this.Load += new System.EventHandler(this.negociacaoInclusao_Load);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.tsAcordo.ResumeLayout(false);
			this.tsAcordo.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.gbDetalheDoAcordo.ResumeLayout(false);
			this.gbDetalheDoAcordo.PerformLayout();
			this.gbContrato.ResumeLayout(false);
			this.gbContrato.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgParcelas)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnSalvarNegociacao;
		private System.Windows.Forms.ToolStrip tsAcordo;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripComboBox tsAcordo_cmbStatusAcordo;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.GroupBox gbDetalheDoAcordo;
		private System.Windows.Forms.TextBox txtNomeCliente;
		private System.Windows.Forms.Label lblIdContrato;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtIDContrato;
		private System.Windows.Forms.GroupBox gbContrato;
		private System.Windows.Forms.Button btnRemoverParcelas;
		private System.Windows.Forms.Button btnAdcionarParcela;
		private System.Windows.Forms.ComboBox cmbParcela;
		private System.Windows.Forms.DataGridView dgParcelas;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtValorParcelas;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtValorPrincipal;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.DateTimePicker dtpDataVencimento;
		private System.Windows.Forms.Label lblPrazo;
		private System.Windows.Forms.ComboBox cmbPrazo;
		private System.Windows.Forms.DataGridViewTextBoxColumn colParcelaParcelas;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVencimentoParcelas;
		private System.Windows.Forms.DataGridViewTextBoxColumn colValorPrincipalParcelas;
		private System.Windows.Forms.DataGridViewTextBoxColumn colValordaParcelaParcelas;
	}
}