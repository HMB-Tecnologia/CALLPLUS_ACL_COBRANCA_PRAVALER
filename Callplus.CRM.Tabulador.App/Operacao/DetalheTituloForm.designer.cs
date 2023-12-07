namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class DetalheTituloForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalheTituloForm));
            this.cmbStatusTitulo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtValorTitulo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTipoDocumento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDataVencimentTitulo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDataEmissaoTitulo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAtribuicaoEspecial = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumeroDocumento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtDataVencimento = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.dtDataFuturaNegociar = new System.Windows.Forms.DateTimePicker();
            this.txtValorBoleto = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtValorParcelas = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtQtdParcela = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtValorAtualizado = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNumeroNegociacao = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtDataVencimentoAtualizada = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbStatusTitulo
            // 
            this.cmbStatusTitulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusTitulo.FormattingEnabled = true;
            this.cmbStatusTitulo.Location = new System.Drawing.Point(9, 32);
            this.cmbStatusTitulo.Name = "cmbStatusTitulo";
            this.cmbStatusTitulo.Size = new System.Drawing.Size(436, 21);
            this.cmbStatusTitulo.TabIndex = 0;
            this.cmbStatusTitulo.SelectionChangeCommitted += new System.EventHandler(this.cmbStatusTitulo_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status do Título:*";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(389, 440);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(308, 440);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtValorTitulo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTipoDocumento);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtDataVencimentTitulo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDataEmissaoTitulo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtAtribuicaoEspecial);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNumeroDocumento);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 107);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Título";
            // 
            // txtValorTitulo
            // 
            this.txtValorTitulo.Location = new System.Drawing.Point(168, 32);
            this.txtValorTitulo.Name = "txtValorTitulo";
            this.txtValorTitulo.ReadOnly = true;
            this.txtValorTitulo.Size = new System.Drawing.Size(165, 20);
            this.txtValorTitulo.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(165, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Valor:";
            // 
            // txtTipoDocumento
            // 
            this.txtTipoDocumento.Location = new System.Drawing.Point(341, 71);
            this.txtTipoDocumento.Name = "txtTipoDocumento";
            this.txtTipoDocumento.ReadOnly = true;
            this.txtTipoDocumento.Size = new System.Drawing.Size(104, 20);
            this.txtTipoDocumento.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(338, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Tipo de Documento:";
            // 
            // txtDataVencimentTitulo
            // 
            this.txtDataVencimentTitulo.Location = new System.Drawing.Point(168, 71);
            this.txtDataVencimentTitulo.Name = "txtDataVencimentTitulo";
            this.txtDataVencimentTitulo.ReadOnly = true;
            this.txtDataVencimentTitulo.Size = new System.Drawing.Size(165, 20);
            this.txtDataVencimentTitulo.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Vencimento:";
            // 
            // txtDataEmissaoTitulo
            // 
            this.txtDataEmissaoTitulo.Location = new System.Drawing.Point(9, 71);
            this.txtDataEmissaoTitulo.Name = "txtDataEmissaoTitulo";
            this.txtDataEmissaoTitulo.ReadOnly = true;
            this.txtDataEmissaoTitulo.Size = new System.Drawing.Size(151, 20);
            this.txtDataEmissaoTitulo.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Emissão:";
            // 
            // txtAtribuicaoEspecial
            // 
            this.txtAtribuicaoEspecial.Location = new System.Drawing.Point(341, 32);
            this.txtAtribuicaoEspecial.Name = "txtAtribuicaoEspecial";
            this.txtAtribuicaoEspecial.ReadOnly = true;
            this.txtAtribuicaoEspecial.Size = new System.Drawing.Size(104, 20);
            this.txtAtribuicaoEspecial.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Atribuiç.Razão Esp.:";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.Location = new System.Drawing.Point(9, 32);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.ReadOnly = true;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(151, 20);
            this.txtNumeroDocumento.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Número do Documento";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbStatusTitulo);
            this.groupBox2.Location = new System.Drawing.Point(12, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 120);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Marcação";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.dtDataVencimento);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.dtDataFuturaNegociar);
            this.groupBox3.Controls.Add(this.txtValorBoleto);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtValorParcelas);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtQtdParcela);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtValorAtualizado);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtNumeroNegociacao);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(12, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(452, 238);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Negociação";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Data de Vencimento:";
            // 
            // dtDataVencimento
            // 
            this.dtDataVencimento.Enabled = false;
            this.dtDataVencimento.Location = new System.Drawing.Point(9, 116);
            this.dtDataVencimento.Name = "dtDataVencimento";
            this.dtDataVencimento.Size = new System.Drawing.Size(233, 20);
            this.dtDataVencimento.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 145);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Data Futura a Negociar:";
            // 
            // dtDataFuturaNegociar
            // 
            this.dtDataFuturaNegociar.Enabled = false;
            this.dtDataFuturaNegociar.Location = new System.Drawing.Point(8, 161);
            this.dtDataFuturaNegociar.Name = "dtDataFuturaNegociar";
            this.dtDataFuturaNegociar.Size = new System.Drawing.Size(234, 20);
            this.dtDataFuturaNegociar.TabIndex = 7;
            // 
            // txtValorBoleto
            // 
            this.txtValorBoleto.Enabled = false;
            this.txtValorBoleto.Location = new System.Drawing.Point(166, 32);
            this.txtValorBoleto.Name = "txtValorBoleto";
            this.txtValorBoleto.Size = new System.Drawing.Size(146, 20);
            this.txtValorBoleto.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(165, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Valor Boleto:";
            // 
            // txtValorParcelas
            // 
            this.txtValorParcelas.Enabled = false;
            this.txtValorParcelas.Location = new System.Drawing.Point(86, 73);
            this.txtValorParcelas.Name = "txtValorParcelas";
            this.txtValorParcelas.Size = new System.Drawing.Size(120, 20);
            this.txtValorParcelas.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(83, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Valor das Parcelas:";
            // 
            // txtQtdParcela
            // 
            this.txtQtdParcela.Enabled = false;
            this.txtQtdParcela.Location = new System.Drawing.Point(9, 73);
            this.txtQtdParcela.Name = "txtQtdParcela";
            this.txtQtdParcela.Size = new System.Drawing.Size(71, 20);
            this.txtQtdParcela.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Qtd. Parcelas:";
            // 
            // txtValorAtualizado
            // 
            this.txtValorAtualizado.Enabled = false;
            this.txtValorAtualizado.Location = new System.Drawing.Point(318, 32);
            this.txtValorAtualizado.Name = "txtValorAtualizado";
            this.txtValorAtualizado.Size = new System.Drawing.Size(127, 20);
            this.txtValorAtualizado.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(315, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Valor Atualizado:";
            // 
            // txtNumeroNegociacao
            // 
            this.txtNumeroNegociacao.Enabled = false;
            this.txtNumeroNegociacao.Location = new System.Drawing.Point(9, 32);
            this.txtNumeroNegociacao.Name = "txtNumeroNegociacao";
            this.txtNumeroNegociacao.Size = new System.Drawing.Size(151, 20);
            this.txtNumeroNegociacao.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Número da Negociação:";
            // 
            // dtDataVencimentoAtualizada
            // 
            this.dtDataVencimentoAtualizada.CustomFormat = "";
            this.dtDataVencimentoAtualizada.Enabled = false;
            this.dtDataVencimentoAtualizada.Location = new System.Drawing.Point(20, 398);
            this.dtDataVencimentoAtualizada.Name = "dtDataVencimentoAtualizada";
            this.dtDataVencimentoAtualizada.Size = new System.Drawing.Size(233, 20);
            this.dtDataVencimentoAtualizada.TabIndex = 12;
            this.dtDataVencimentoAtualizada.ValueChanged += new System.EventHandler(this.dtDataVencimentoAtualizada_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Data de Vencimento atualizada:";
            // 
            // fDetalheTitulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 474);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtDataVencimentoAtualizada);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fDetalheTitulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes do Título";
            this.Load += new System.EventHandler(this.fDetalheTitulo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStatusTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTipoDocumento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDataVencimentTitulo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDataEmissaoTitulo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAtribuicaoEspecial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNumeroDocumento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtValorTitulo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtValorBoleto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtValorParcelas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtQtdParcela;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtValorAtualizado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNumeroNegociacao;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtDataVencimento;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtDataFuturaNegociar;
        private System.Windows.Forms.DateTimePicker dtDataVencimentoAtualizada;
        private System.Windows.Forms.Label label2;
    }
}