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
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.chkTodosTitulos = new System.Windows.Forms.CheckBox();
			this.dgDetalhesDoTitulo = new System.Windows.Forms.DataGridView();
			this.colSelecioneTitulo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colNumeroDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDataEmissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDataVencimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAtribuicaoRazaoEspecial = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colFormaDePagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMontante = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.txtTotalNegociado = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.lblDebitoTotal = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gbDetalheDoAcordo = new System.Windows.Forms.GroupBox();
			this.mskDataVencimento = new System.Windows.Forms.MaskedTextBox();
			this.btnRemoverParcelas = new System.Windows.Forms.Button();
			this.btnAdcionarParcela = new System.Windows.Forms.Button();
			this.cmbQuantidadeParcela = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbPrazo = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtJuros = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtMulta = new System.Windows.Forms.TextBox();
			this.dgParcelas = new System.Windows.Forms.DataGridView();
			this.colParcelaParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVencimentoParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colValorPrincipalParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMultaParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colJurosParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colValordaParcelaParcelas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label11 = new System.Windows.Forms.Label();
			this.txtNumeroNegociacao = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.txtValorParcelas = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.txtValorPrincipal = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.btnCancelarNegociacao = new System.Windows.Forms.Button();
			this.btnSalvarNegociacao = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtNomeCliente = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCodCliente = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtIDContrato = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbTipoAcordo = new System.Windows.Forms.ComboBox();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgDetalhesDoTitulo)).BeginInit();
			this.gbDetalheDoAcordo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgParcelas)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.chkTodosTitulos);
			this.groupBox4.Controls.Add(this.dgDetalhesDoTitulo);
			this.groupBox4.Controls.Add(this.txtTotalNegociado);
			this.groupBox4.Controls.Add(this.comboBox1);
			this.groupBox4.Controls.Add(this.lblDebitoTotal);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Location = new System.Drawing.Point(12, 84);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(1176, 174);
			this.groupBox4.TabIndex = 175;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Títulos";
			// 
			// chkTodosTitulos
			// 
			this.chkTodosTitulos.AutoSize = true;
			this.chkTodosTitulos.Checked = true;
			this.chkTodosTitulos.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkTodosTitulos.Location = new System.Drawing.Point(228, 152);
			this.chkTodosTitulos.Name = "chkTodosTitulos";
			this.chkTodosTitulos.Size = new System.Drawing.Size(118, 17);
			this.chkTodosTitulos.TabIndex = 6;
			this.chkTodosTitulos.Text = "Marcar todos titulos";
			this.chkTodosTitulos.UseVisualStyleBackColor = true;
			this.chkTodosTitulos.CheckedChanged += new System.EventHandler(this.chkTodosTitulos_CheckedChanged);
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
            this.colSelecioneTitulo,
            this.colID,
            this.colNumeroDocumento,
            this.colDataEmissao,
            this.colDataVencimento,
            this.colAtribuicaoRazaoEspecial,
            this.colTipoDocumento,
            this.colFormaDePagamento,
            this.colMontante});
			this.dgDetalhesDoTitulo.Location = new System.Drawing.Point(10, 19);
			this.dgDetalhesDoTitulo.Name = "dgDetalhesDoTitulo";
			this.dgDetalhesDoTitulo.RowHeadersVisible = false;
			this.dgDetalhesDoTitulo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgDetalhesDoTitulo.Size = new System.Drawing.Size(1149, 125);
			this.dgDetalhesDoTitulo.TabIndex = 5;
			this.dgDetalhesDoTitulo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetalhesDoTitulo_CellContentClick);
			// 
			// colSelecioneTitulo
			// 
			this.colSelecioneTitulo.FillWeight = 80.1F;
			this.colSelecioneTitulo.HeaderText = "";
			this.colSelecioneTitulo.Name = "colSelecioneTitulo";
			// 
			// colID
			// 
			this.colID.FillWeight = 84.03561F;
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.ReadOnly = true;
			// 
			// colNumeroDocumento
			// 
			this.colNumeroDocumento.FillWeight = 84.03561F;
			this.colNumeroDocumento.HeaderText = "Número do Documento";
			this.colNumeroDocumento.Name = "colNumeroDocumento";
			this.colNumeroDocumento.ReadOnly = true;
			// 
			// colDataEmissao
			// 
			this.colDataEmissao.FillWeight = 84.03561F;
			this.colDataEmissao.HeaderText = "Emissão";
			this.colDataEmissao.Name = "colDataEmissao";
			this.colDataEmissao.ReadOnly = true;
			// 
			// colDataVencimento
			// 
			this.colDataVencimento.FillWeight = 87.593F;
			this.colDataVencimento.HeaderText = "Vencimento";
			this.colDataVencimento.Name = "colDataVencimento";
			this.colDataVencimento.ReadOnly = true;
			// 
			// colAtribuicaoRazaoEspecial
			// 
			this.colAtribuicaoRazaoEspecial.FillWeight = 84.03561F;
			this.colAtribuicaoRazaoEspecial.HeaderText = "Atribuição Razão Espécial";
			this.colAtribuicaoRazaoEspecial.Name = "colAtribuicaoRazaoEspecial";
			// 
			// colTipoDocumento
			// 
			this.colTipoDocumento.FillWeight = 84.03561F;
			this.colTipoDocumento.HeaderText = "Tipo de Documento";
			this.colTipoDocumento.Name = "colTipoDocumento";
			this.colTipoDocumento.ReadOnly = true;
			// 
			// colFormaDePagamento
			// 
			this.colFormaDePagamento.FillWeight = 84.03561F;
			this.colFormaDePagamento.HeaderText = "Forma de Pagamento";
			this.colFormaDePagamento.Name = "colFormaDePagamento";
			// 
			// colMontante
			// 
			this.colMontante.FillWeight = 74.13061F;
			this.colMontante.HeaderText = "Montante";
			this.colMontante.Name = "colMontante";
			this.colMontante.ReadOnly = true;
			// 
			// txtTotalNegociado
			// 
			this.txtTotalNegociado.Enabled = false;
			this.txtTotalNegociado.Location = new System.Drawing.Point(102, 150);
			this.txtTotalNegociado.Name = "txtTotalNegociado";
			this.txtTotalNegociado.Size = new System.Drawing.Size(110, 20);
			this.txtTotalNegociado.TabIndex = 2;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Enabled = false;
			this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "SELECIONE..."});
			this.comboBox1.Location = new System.Drawing.Point(6, 275);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(372, 21);
			this.comboBox1.TabIndex = 3;
			// 
			// lblDebitoTotal
			// 
			this.lblDebitoTotal.AutoSize = true;
			this.lblDebitoTotal.Location = new System.Drawing.Point(7, 153);
			this.lblDebitoTotal.Name = "lblDebitoTotal";
			this.lblDebitoTotal.Size = new System.Drawing.Size(89, 13);
			this.lblDebitoTotal.TabIndex = 1;
			this.lblDebitoTotal.Text = "Total Negociado:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 259);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Status do Título";
			// 
			// gbDetalheDoAcordo
			// 
			this.gbDetalheDoAcordo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbDetalheDoAcordo.Controls.Add(this.mskDataVencimento);
			this.gbDetalheDoAcordo.Controls.Add(this.btnRemoverParcelas);
			this.gbDetalheDoAcordo.Controls.Add(this.btnAdcionarParcela);
			this.gbDetalheDoAcordo.Controls.Add(this.cmbQuantidadeParcela);
			this.gbDetalheDoAcordo.Controls.Add(this.label8);
			this.gbDetalheDoAcordo.Controls.Add(this.cmbPrazo);
			this.gbDetalheDoAcordo.Controls.Add(this.label7);
			this.gbDetalheDoAcordo.Controls.Add(this.txtJuros);
			this.gbDetalheDoAcordo.Controls.Add(this.label6);
			this.gbDetalheDoAcordo.Controls.Add(this.txtMulta);
			this.gbDetalheDoAcordo.Controls.Add(this.dgParcelas);
			this.gbDetalheDoAcordo.Controls.Add(this.label11);
			this.gbDetalheDoAcordo.Controls.Add(this.txtNumeroNegociacao);
			this.gbDetalheDoAcordo.Controls.Add(this.label15);
			this.gbDetalheDoAcordo.Controls.Add(this.label14);
			this.gbDetalheDoAcordo.Controls.Add(this.txtValorParcelas);
			this.gbDetalheDoAcordo.Controls.Add(this.label12);
			this.gbDetalheDoAcordo.Controls.Add(this.txtValorPrincipal);
			this.gbDetalheDoAcordo.Controls.Add(this.label13);
			this.gbDetalheDoAcordo.Location = new System.Drawing.Point(12, 267);
			this.gbDetalheDoAcordo.Name = "gbDetalheDoAcordo";
			this.gbDetalheDoAcordo.Size = new System.Drawing.Size(1176, 214);
			this.gbDetalheDoAcordo.TabIndex = 184;
			this.gbDetalheDoAcordo.TabStop = false;
			this.gbDetalheDoAcordo.Text = "Detalhes do Acordo/Parcelas";
			// 
			// mskDataVencimento
			// 
			this.mskDataVencimento.Location = new System.Drawing.Point(166, 39);
			this.mskDataVencimento.Mask = "00/00/0000";
			this.mskDataVencimento.Name = "mskDataVencimento";
			this.mskDataVencimento.Size = new System.Drawing.Size(153, 20);
			this.mskDataVencimento.TabIndex = 2;
			// 
			// btnRemoverParcelas
			// 
			this.btnRemoverParcelas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoverParcelas.Location = new System.Drawing.Point(1094, 112);
			this.btnRemoverParcelas.Name = "btnRemoverParcelas";
			this.btnRemoverParcelas.Size = new System.Drawing.Size(76, 23);
			this.btnRemoverParcelas.TabIndex = 191;
			this.btnRemoverParcelas.Text = "Limpar";
			this.btnRemoverParcelas.UseVisualStyleBackColor = true;
			this.btnRemoverParcelas.Click += new System.EventHandler(this.btnRemoverParcelas_Click);
			// 
			// btnAdcionarParcela
			// 
			this.btnAdcionarParcela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdcionarParcela.Location = new System.Drawing.Point(1094, 83);
			this.btnAdcionarParcela.Name = "btnAdcionarParcela";
			this.btnAdcionarParcela.Size = new System.Drawing.Size(76, 23);
			this.btnAdcionarParcela.TabIndex = 9;
			this.btnAdcionarParcela.Text = "Adicionar";
			this.btnAdcionarParcela.UseVisualStyleBackColor = true;
			this.btnAdcionarParcela.Click += new System.EventHandler(this.btnAdicionarParcela_Click);
			// 
			// cmbQuantidadeParcela
			// 
			this.cmbQuantidadeParcela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbQuantidadeParcela.FormattingEnabled = true;
			this.cmbQuantidadeParcela.ItemHeight = 13;
			this.cmbQuantidadeParcela.Items.AddRange(new object[] {
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
			this.cmbQuantidadeParcela.Location = new System.Drawing.Point(458, 40);
			this.cmbQuantidadeParcela.Name = "cmbQuantidadeParcela";
			this.cmbQuantidadeParcela.Size = new System.Drawing.Size(71, 21);
			this.cmbQuantidadeParcela.TabIndex = 4;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(832, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(37, 13);
			this.label8.TabIndex = 193;
			this.label8.Text = "Prazo:";
			// 
			// cmbPrazo
			// 
			this.cmbPrazo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPrazo.FormattingEnabled = true;
			this.cmbPrazo.ItemHeight = 13;
			this.cmbPrazo.Location = new System.Drawing.Point(833, 38);
			this.cmbPrazo.Name = "cmbPrazo";
			this.cmbPrazo.Size = new System.Drawing.Size(108, 21);
			this.cmbPrazo.TabIndex = 8;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(618, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(55, 13);
			this.label7.TabIndex = 190;
			this.label7.Text = "Juros (R$)";
			// 
			// txtJuros
			// 
			this.txtJuros.Location = new System.Drawing.Point(621, 39);
			this.txtJuros.Name = "txtJuros";
			this.txtJuros.Size = new System.Drawing.Size(80, 20);
			this.txtJuros.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(536, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Multa (R$)";
			// 
			// txtMulta
			// 
			this.txtMulta.Location = new System.Drawing.Point(535, 39);
			this.txtMulta.Name = "txtMulta";
			this.txtMulta.Size = new System.Drawing.Size(80, 20);
			this.txtMulta.TabIndex = 5;
			// 
			// dgParcelas
			// 
			this.dgParcelas.AllowUserToAddRows = false;
			this.dgParcelas.AllowUserToDeleteRows = false;
			this.dgParcelas.AllowUserToOrderColumns = true;
			this.dgParcelas.AllowUserToResizeRows = false;
			this.dgParcelas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgParcelas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgParcelas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgParcelas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParcelaParcelas,
            this.colVencimentoParcelas,
            this.colValorPrincipalParcelas,
            this.colMultaParcelas,
            this.colJurosParcelas,
            this.colValordaParcelaParcelas});
			this.dgParcelas.Location = new System.Drawing.Point(8, 83);
			this.dgParcelas.Name = "dgParcelas";
			this.dgParcelas.RowHeadersVisible = false;
			this.dgParcelas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgParcelas.Size = new System.Drawing.Size(1080, 125);
			this.dgParcelas.TabIndex = 189;
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
			// colMultaParcelas
			// 
			this.colMultaParcelas.FillWeight = 104.2332F;
			this.colMultaParcelas.HeaderText = "Multa";
			this.colMultaParcelas.Name = "colMultaParcelas";
			// 
			// colJurosParcelas
			// 
			this.colJurosParcelas.HeaderText = "Juros";
			this.colJurosParcelas.Name = "colJurosParcelas";
			// 
			// colValordaParcelaParcelas
			// 
			this.colValordaParcelaParcelas.HeaderText = "Valor da Parcela";
			this.colValordaParcelaParcelas.Name = "colValordaParcelaParcelas";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(163, 24);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(107, 13);
			this.label11.TabIndex = 159;
			this.label11.Text = "Data de Vencimento:";
			// 
			// txtNumeroNegociacao
			// 
			this.txtNumeroNegociacao.Location = new System.Drawing.Point(9, 39);
			this.txtNumeroNegociacao.Name = "txtNumeroNegociacao";
			this.txtNumeroNegociacao.Size = new System.Drawing.Size(151, 20);
			this.txtNumeroNegociacao.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(6, 23);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(123, 13);
			this.label15.TabIndex = 158;
			this.label15.Text = "Número da Negociação:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(322, 23);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(77, 13);
			this.label14.TabIndex = 156;
			this.label14.Text = "Valor Principal:";
			// 
			// txtValorParcelas
			// 
			this.txtValorParcelas.Location = new System.Drawing.Point(707, 39);
			this.txtValorParcelas.Name = "txtValorParcelas";
			this.txtValorParcelas.Size = new System.Drawing.Size(120, 20);
			this.txtValorParcelas.TabIndex = 7;
			this.txtValorParcelas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorParcelas_KeyPress);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(705, 25);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(98, 13);
			this.label12.TabIndex = 6;
			this.label12.Text = "Valor das Parcelas:";
			// 
			// txtValorPrincipal
			// 
			this.txtValorPrincipal.Location = new System.Drawing.Point(325, 39);
			this.txtValorPrincipal.Name = "txtValorPrincipal";
			this.txtValorPrincipal.Size = new System.Drawing.Size(127, 20);
			this.txtValorPrincipal.TabIndex = 3;
			this.txtValorPrincipal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorAtualizado_KeyPress);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(456, 25);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(74, 13);
			this.label13.TabIndex = 159;
			this.label13.Text = "Qtd. Parcelas:";
			// 
			// btnCancelarNegociacao
			// 
			this.btnCancelarNegociacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancelarNegociacao.Location = new System.Drawing.Point(1112, 487);
			this.btnCancelarNegociacao.Name = "btnCancelarNegociacao";
			this.btnCancelarNegociacao.Size = new System.Drawing.Size(76, 23);
			this.btnCancelarNegociacao.TabIndex = 185;
			this.btnCancelarNegociacao.Text = "Cancelar";
			this.btnCancelarNegociacao.UseVisualStyleBackColor = true;
			this.btnCancelarNegociacao.Click += new System.EventHandler(this.btnCancelarNegociacao_Click);
			// 
			// btnSalvarNegociacao
			// 
			this.btnSalvarNegociacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSalvarNegociacao.Location = new System.Drawing.Point(1030, 487);
			this.btnSalvarNegociacao.Name = "btnSalvarNegociacao";
			this.btnSalvarNegociacao.Size = new System.Drawing.Size(76, 23);
			this.btnSalvarNegociacao.TabIndex = 8;
			this.btnSalvarNegociacao.Text = "Confirmar";
			this.btnSalvarNegociacao.UseVisualStyleBackColor = true;
			this.btnSalvarNegociacao.Click += new System.EventHandler(this.btnSalvarNegociacao_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtNomeCliente);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtCodCliente);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtIDContrato);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(624, 63);
			this.groupBox1.TabIndex = 187;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Contrato";
			// 
			// txtNomeCliente
			// 
			this.txtNomeCliente.Location = new System.Drawing.Point(228, 32);
			this.txtNomeCliente.Name = "txtNomeCliente";
			this.txtNomeCliente.ReadOnly = true;
			this.txtNomeCliente.Size = new System.Drawing.Size(350, 20);
			this.txtNomeCliente.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(225, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Nome do Cliente";
			// 
			// txtCodCliente
			// 
			this.txtCodCliente.Location = new System.Drawing.Point(125, 32);
			this.txtCodCliente.Name = "txtCodCliente";
			this.txtCodCliente.ReadOnly = true;
			this.txtCodCliente.Size = new System.Drawing.Size(97, 20);
			this.txtCodCliente.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(122, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Código Cliente";
			// 
			// txtIDContrato
			// 
			this.txtIDContrato.Location = new System.Drawing.Point(9, 32);
			this.txtIDContrato.Name = "txtIDContrato";
			this.txtIDContrato.ReadOnly = true;
			this.txtIDContrato.Size = new System.Drawing.Size(110, 20);
			this.txtIDContrato.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(21, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "ID:";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.cmbTipoAcordo);
			this.groupBox2.Location = new System.Drawing.Point(718, 13);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(470, 62);
			this.groupBox2.TabIndex = 188;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Marcação";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Tipo de Acordo:";
			// 
			// cmbTipoAcordo
			// 
			this.cmbTipoAcordo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTipoAcordo.FormattingEnabled = true;
			this.cmbTipoAcordo.Location = new System.Drawing.Point(9, 32);
			this.cmbTipoAcordo.Name = "cmbTipoAcordo";
			this.cmbTipoAcordo.Size = new System.Drawing.Size(436, 21);
			this.cmbTipoAcordo.TabIndex = 0;
			this.cmbTipoAcordo.SelectionChangeCommitted += new System.EventHandler(this.cmbTipoAcordo_SelectionChangeCommitted);
			// 
			// fNegociacao_Inclusao
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 513);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSalvarNegociacao);
			this.Controls.Add(this.btnCancelarNegociacao);
			this.Controls.Add(this.gbDetalheDoAcordo);
			this.Controls.Add(this.groupBox4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "fNegociacao_Inclusao";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Novo Acordo";
			this.Load += new System.EventHandler(this.fNegociacao_Inclusao_Load);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgDetalhesDoTitulo)).EndInit();
			this.gbDetalheDoAcordo.ResumeLayout(false);
			this.gbDetalheDoAcordo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgParcelas)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbDetalheDoAcordo;
        private System.Windows.Forms.TextBox txtTotalNegociado;
        private System.Windows.Forms.Label lblDebitoTotal;
        private System.Windows.Forms.Button btnCancelarNegociacao;
        private System.Windows.Forms.Button btnSalvarNegociacao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIDContrato;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNumeroNegociacao;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtValorParcelas;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtValorPrincipal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dgDetalhesDoTitulo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTipoAcordo;
        private System.Windows.Forms.DataGridView dgParcelas;
        private System.Windows.Forms.Button btnAdcionarParcela;
        private System.Windows.Forms.Button btnRemoverParcelas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJuros;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMulta;
        private System.Windows.Forms.ComboBox cmbQuantidadeParcela;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPrazo;
        private System.Windows.Forms.MaskedTextBox mskDataVencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParcelaParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencimentoParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorPrincipalParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMultaParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJurosParcelas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValordaParcelaParcelas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelecioneTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataEmissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataVencimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAtribuicaoRazaoEspecial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormaDePagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMontante;
        private System.Windows.Forms.CheckBox chkTodosTitulos;
    }
}