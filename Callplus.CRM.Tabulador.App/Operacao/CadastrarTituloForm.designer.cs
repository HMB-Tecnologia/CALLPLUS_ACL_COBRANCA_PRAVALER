namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class CadastrarTituloForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastrarTituloForm));
			this.gbTitulo = new System.Windows.Forms.GroupBox();
			this.btnExcluir = new System.Windows.Forms.Button();
			this.btnIncluir = new System.Windows.Forms.Button();
			this.dgDetalhesDoTitulo = new System.Windows.Forms.DataGridView();
			this.colNumeroDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDataEmissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDataVencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAtribuicaoRazaoEspecial = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colFormaDePagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMontante = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label8 = new System.Windows.Forms.Label();
			this.txtNumeroDocumento = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtTipoDocumento = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtFormaPagamento = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMontante = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.mskDataVencimento = new System.Windows.Forms.MaskedTextBox();
			this.txtAtribuicaoEspecial = new System.Windows.Forms.TextBox();
			this.mskDataEmissao = new System.Windows.Forms.MaskedTextBox();
			this.cmdSalvar = new System.Windows.Forms.Button();
			this.txtCodCliente = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtIDProspect = new System.Windows.Forms.TextBox();
			this.lblIDProspect = new System.Windows.Forms.Label();
			this.gbTitulo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgDetalhesDoTitulo)).BeginInit();
			this.SuspendLayout();
			// 
			// gbTitulo
			// 
			this.gbTitulo.Controls.Add(this.btnExcluir);
			this.gbTitulo.Controls.Add(this.btnIncluir);
			this.gbTitulo.Controls.Add(this.dgDetalhesDoTitulo);
			this.gbTitulo.Controls.Add(this.label8);
			this.gbTitulo.Controls.Add(this.txtNumeroDocumento);
			this.gbTitulo.Controls.Add(this.label7);
			this.gbTitulo.Controls.Add(this.txtTipoDocumento);
			this.gbTitulo.Controls.Add(this.label6);
			this.gbTitulo.Controls.Add(this.txtFormaPagamento);
			this.gbTitulo.Controls.Add(this.label5);
			this.gbTitulo.Controls.Add(this.label10);
			this.gbTitulo.Controls.Add(this.label3);
			this.gbTitulo.Controls.Add(this.txtMontante);
			this.gbTitulo.Controls.Add(this.label2);
			this.gbTitulo.Controls.Add(this.mskDataVencimento);
			this.gbTitulo.Controls.Add(this.txtAtribuicaoEspecial);
			this.gbTitulo.Controls.Add(this.mskDataEmissao);
			this.gbTitulo.Location = new System.Drawing.Point(12, 62);
			this.gbTitulo.Name = "gbTitulo";
			this.gbTitulo.Size = new System.Drawing.Size(849, 242);
			this.gbTitulo.TabIndex = 230;
			this.gbTitulo.TabStop = false;
			this.gbTitulo.Text = "Dados do Titulo";
			// 
			// btnExcluir
			// 
			this.btnExcluir.BackColor = System.Drawing.Color.Beige;
			this.btnExcluir.Enabled = false;
			this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnExcluir.Location = new System.Drawing.Point(765, 94);
			this.btnExcluir.Name = "btnExcluir";
			this.btnExcluir.Size = new System.Drawing.Size(78, 24);
			this.btnExcluir.TabIndex = 306;
			this.btnExcluir.Text = "Excluir";
			this.btnExcluir.UseVisualStyleBackColor = false;
			this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
			// 
			// btnIncluir
			// 
			this.btnIncluir.BackColor = System.Drawing.Color.Beige;
			this.btnIncluir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnIncluir.Location = new System.Drawing.Point(765, 64);
			this.btnIncluir.Name = "btnIncluir";
			this.btnIncluir.Size = new System.Drawing.Size(78, 24);
			this.btnIncluir.TabIndex = 15;
			this.btnIncluir.Text = "Incluir";
			this.btnIncluir.UseVisualStyleBackColor = false;
			this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
			// 
			// dgDetalhesDoTitulo
			// 
			this.dgDetalhesDoTitulo.AllowUserToAddRows = false;
			this.dgDetalhesDoTitulo.AllowUserToDeleteRows = false;
			this.dgDetalhesDoTitulo.AllowUserToOrderColumns = true;
			this.dgDetalhesDoTitulo.AllowUserToResizeRows = false;
			this.dgDetalhesDoTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgDetalhesDoTitulo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgDetalhesDoTitulo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgDetalhesDoTitulo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumeroDocumento,
            this.colDataEmissao,
            this.colDataVencimento,
            this.colAtribuicaoRazaoEspecial,
            this.colTipoDocumento,
            this.colFormaDePagamento,
            this.colMontante});
			this.dgDetalhesDoTitulo.Location = new System.Drawing.Point(6, 64);
			this.dgDetalhesDoTitulo.Name = "dgDetalhesDoTitulo";
			this.dgDetalhesDoTitulo.RowHeadersVisible = false;
			this.dgDetalhesDoTitulo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgDetalhesDoTitulo.Size = new System.Drawing.Size(749, 166);
			this.dgDetalhesDoTitulo.TabIndex = 212;
			// 
			// colNumeroDocumento
			// 
			this.colNumeroDocumento.HeaderText = "Número do Documento";
			this.colNumeroDocumento.Name = "colNumeroDocumento";
			this.colNumeroDocumento.ReadOnly = true;
			// 
			// colDataEmissao
			// 
			this.colDataEmissao.HeaderText = "Emissão";
			this.colDataEmissao.Name = "colDataEmissao";
			this.colDataEmissao.ReadOnly = true;
			// 
			// colDataVencimento
			// 
			this.colDataVencimento.FillWeight = 104.2332F;
			this.colDataVencimento.HeaderText = "Vencimento";
			this.colDataVencimento.Name = "colDataVencimento";
			this.colDataVencimento.ReadOnly = true;
			// 
			// colAtribuicaoRazaoEspecial
			// 
			this.colAtribuicaoRazaoEspecial.HeaderText = "Atribuição Razão Espécial";
			this.colAtribuicaoRazaoEspecial.Name = "colAtribuicaoRazaoEspecial";
			// 
			// colTipoDocumento
			// 
			this.colTipoDocumento.HeaderText = "Tipo de Documento";
			this.colTipoDocumento.Name = "colTipoDocumento";
			this.colTipoDocumento.ReadOnly = true;
			// 
			// colFormaDePagamento
			// 
			this.colFormaDePagamento.HeaderText = "Forma de Pagamento";
			this.colFormaDePagamento.Name = "colFormaDePagamento";
			// 
			// colMontante
			// 
			this.colMontante.FillWeight = 88.21333F;
			this.colMontante.HeaderText = "Montante";
			this.colMontante.Name = "colMontante";
			this.colMontante.ReadOnly = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(545, 17);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(111, 13);
			this.label8.TabIndex = 228;
			this.label8.Text = "Forma de Pagamento:";
			// 
			// txtNumeroDocumento
			// 
			this.txtNumeroDocumento.ForeColor = System.Drawing.Color.Black;
			this.txtNumeroDocumento.Location = new System.Drawing.Point(6, 33);
			this.txtNumeroDocumento.Name = "txtNumeroDocumento";
			this.txtNumeroDocumento.Size = new System.Drawing.Size(110, 20);
			this.txtNumeroDocumento.TabIndex = 8;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(429, 17);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 13);
			this.label7.TabIndex = 227;
			this.label7.Text = "Tipo de Documento:";
			// 
			// txtTipoDocumento
			// 
			this.txtTipoDocumento.ForeColor = System.Drawing.Color.Black;
			this.txtTipoDocumento.Location = new System.Drawing.Point(432, 33);
			this.txtTipoDocumento.Name = "txtTipoDocumento";
			this.txtTipoDocumento.Size = new System.Drawing.Size(110, 20);
			this.txtTipoDocumento.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(313, 17);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93, 13);
			this.label6.TabIndex = 226;
			this.label6.Text = "Atribuição Espec.:";
			// 
			// txtFormaPagamento
			// 
			this.txtFormaPagamento.ForeColor = System.Drawing.Color.Black;
			this.txtFormaPagamento.Location = new System.Drawing.Point(548, 33);
			this.txtFormaPagamento.Name = "txtFormaPagamento";
			this.txtFormaPagamento.Size = new System.Drawing.Size(110, 20);
			this.txtFormaPagamento.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(218, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 13);
			this.label5.TabIndex = 225;
			this.label5.Text = "Data Vencimento:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(663, 17);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(55, 13);
			this.label10.TabIndex = 218;
			this.label10.Text = "Montante:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(119, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 13);
			this.label3.TabIndex = 224;
			this.label3.Text = "Data Emissão:";
			// 
			// txtMontante
			// 
			this.txtMontante.Location = new System.Drawing.Point(664, 33);
			this.txtMontante.Name = "txtMontante";
			this.txtMontante.Size = new System.Drawing.Size(91, 20);
			this.txtMontante.TabIndex = 14;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 13);
			this.label2.TabIndex = 223;
			this.label2.Text = "N° Documento:";
			// 
			// mskDataVencimento
			// 
			this.mskDataVencimento.Location = new System.Drawing.Point(221, 33);
			this.mskDataVencimento.Mask = "00/00/0000";
			this.mskDataVencimento.Name = "mskDataVencimento";
			this.mskDataVencimento.Size = new System.Drawing.Size(89, 20);
			this.mskDataVencimento.TabIndex = 10;
			// 
			// txtAtribuicaoEspecial
			// 
			this.txtAtribuicaoEspecial.ForeColor = System.Drawing.Color.Black;
			this.txtAtribuicaoEspecial.Location = new System.Drawing.Point(316, 33);
			this.txtAtribuicaoEspecial.Name = "txtAtribuicaoEspecial";
			this.txtAtribuicaoEspecial.Size = new System.Drawing.Size(110, 20);
			this.txtAtribuicaoEspecial.TabIndex = 11;
			// 
			// mskDataEmissao
			// 
			this.mskDataEmissao.Location = new System.Drawing.Point(122, 33);
			this.mskDataEmissao.Mask = "00/00/0000";
			this.mskDataEmissao.Name = "mskDataEmissao";
			this.mskDataEmissao.Size = new System.Drawing.Size(93, 20);
			this.mskDataEmissao.TabIndex = 9;
			// 
			// cmdSalvar
			// 
			this.cmdSalvar.BackColor = System.Drawing.Color.Beige;
			this.cmdSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cmdSalvar.Location = new System.Drawing.Point(777, 318);
			this.cmdSalvar.Name = "cmdSalvar";
			this.cmdSalvar.Size = new System.Drawing.Size(78, 24);
			this.cmdSalvar.TabIndex = 231;
			this.cmdSalvar.Text = "Salvar";
			this.cmdSalvar.UseVisualStyleBackColor = false;
			this.cmdSalvar.Click += new System.EventHandler(this.cmdSalvar_Click);
			// 
			// txtCodCliente
			// 
			this.txtCodCliente.Enabled = false;
			this.txtCodCliente.ForeColor = System.Drawing.Color.Black;
			this.txtCodCliente.Location = new System.Drawing.Point(128, 26);
			this.txtCodCliente.Name = "txtCodCliente";
			this.txtCodCliente.Size = new System.Drawing.Size(110, 20);
			this.txtCodCliente.TabIndex = 232;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Enabled = false;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(125, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 234;
			this.label1.Text = "Cod. Cliente";
			// 
			// txtIDProspect
			// 
			this.txtIDProspect.Enabled = false;
			this.txtIDProspect.ForeColor = System.Drawing.Color.Black;
			this.txtIDProspect.Location = new System.Drawing.Point(12, 26);
			this.txtIDProspect.Name = "txtIDProspect";
			this.txtIDProspect.ReadOnly = true;
			this.txtIDProspect.Size = new System.Drawing.Size(110, 20);
			this.txtIDProspect.TabIndex = 237;
			// 
			// lblIDProspect
			// 
			this.lblIDProspect.AutoSize = true;
			this.lblIDProspect.Enabled = false;
			this.lblIDProspect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIDProspect.ForeColor = System.Drawing.Color.Black;
			this.lblIDProspect.Location = new System.Drawing.Point(9, 10);
			this.lblIDProspect.Name = "lblIDProspect";
			this.lblIDProspect.Size = new System.Drawing.Size(43, 13);
			this.lblIDProspect.TabIndex = 236;
			this.lblIDProspect.Text = "Código:";
			// 
			// CadastrarTituloForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(873, 354);
			this.Controls.Add(this.txtIDProspect);
			this.Controls.Add(this.lblIDProspect);
			this.Controls.Add(this.txtCodCliente);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdSalvar);
			this.Controls.Add(this.gbTitulo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CadastrarTituloForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cadastrar Titulo";
			this.Load += new System.EventHandler(this.cadastrarTitulo_Load);
			this.gbTitulo.ResumeLayout(false);
			this.gbTitulo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgDetalhesDoTitulo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTitulo;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.DataGridView dgDetalhesDoTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataEmissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataVencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAtribuicaoRazaoEspecial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormaDePagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMontante;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtNumeroDocumento;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtTipoDocumento;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtFormaPagamento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMontante;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mskDataVencimento;
        public System.Windows.Forms.TextBox txtAtribuicaoEspecial;
        private System.Windows.Forms.MaskedTextBox mskDataEmissao;
        private System.Windows.Forms.Button cmdSalvar;
        public System.Windows.Forms.TextBox txtCodCliente;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtIDProspect;
        private System.Windows.Forms.Label lblIDProspect;
    }
}