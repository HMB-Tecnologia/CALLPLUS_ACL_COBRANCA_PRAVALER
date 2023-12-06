namespace Callplus.CRM.Administracao.App.Planejamento.Produto
{
    partial class ProdutoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdutoForm));
            this.label8 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtOrdem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.cmbCampanha = new System.Windows.Forms.ComboBox();
            this.lblCampanha = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.cmbTipoDeProduto = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.chkAtivoBko = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbScriptOferta = new System.Windows.Forms.ComboBox();
            this.tabDetalhesProduto = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFaixaDeRecarga = new System.Windows.Forms.ComboBox();
            this.chkAtivoFaixaDeRecarga = new System.Windows.Forms.CheckBox();
            this.txtIdProdutoPermitido = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tsFaixaDeRecarga = new System.Windows.Forms.ToolStrip();
            this.tsFaixadeRecarga_btnNovo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFaixaDeRecarga_btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFaixaDeRecarga_btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgFaixaDeRecarga = new System.Windows.Forms.DataGridView();
            this.colDgFaixaDeRecarga_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDgFaixadeRecarga_IdFaixaDeRecarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDgFaixaDeRecarga_IdProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDgFaixaDeRecarga_Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDgFaixaDeRecarga_Ativo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtIdProduto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabDetalhesProduto.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tsFaixaDeRecarga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFaixaDeRecarga)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Observações";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(12, 297);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(335, 44);
            this.txtObservacao.TabIndex = 10;
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 347);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 27);
            this.btnSalvar.TabIndex = 11;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(7, 5);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(116, 25);
            this.lblTitulo.TabIndex = 7;
            this.lblTitulo.Text = "PRODUTO";
            // 
            // txtOrdem
            // 
            this.txtOrdem.Location = new System.Drawing.Point(12, 256);
            this.txtOrdem.MaxLength = 100;
            this.txtOrdem.Name = "txtOrdem";
            this.txtOrdem.Size = new System.Drawing.Size(56, 20);
            this.txtOrdem.TabIndex = 28;
            this.txtOrdem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApenasValorNumerico);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Ordem";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 172);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(335, 20);
            this.txtNome.TabIndex = 26;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(9, 156);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 25;
            this.lblNome.Text = "Nome";
            // 
            // cmbCampanha
            // 
            this.cmbCampanha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCampanha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCampanha.FormattingEnabled = true;
            this.cmbCampanha.Location = new System.Drawing.Point(12, 132);
            this.cmbCampanha.Name = "cmbCampanha";
            this.cmbCampanha.Size = new System.Drawing.Size(335, 21);
            this.cmbCampanha.TabIndex = 21;
            // 
            // lblCampanha
            // 
            this.lblCampanha.AutoSize = true;
            this.lblCampanha.Location = new System.Drawing.Point(9, 116);
            this.lblCampanha.Name = "lblCampanha";
            this.lblCampanha.Size = new System.Drawing.Size(58, 13);
            this.lblCampanha.TabIndex = 20;
            this.lblCampanha.Text = "Campanha";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(151, 259);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 22;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // cmbTipoDeProduto
            // 
            this.cmbTipoDeProduto.FormattingEnabled = true;
            this.cmbTipoDeProduto.Location = new System.Drawing.Point(12, 92);
            this.cmbTipoDeProduto.Name = "cmbTipoDeProduto";
            this.cmbTipoDeProduto.Size = new System.Drawing.Size(335, 21);
            this.cmbTipoDeProduto.TabIndex = 19;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(9, 76);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(28, 13);
            this.lblTipo.TabIndex = 18;
            this.lblTipo.Text = "Tipo";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(74, 256);
            this.txtValor.MaxLength = 100;
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(71, 20);
            this.txtValor.TabIndex = 32;
            this.txtValor.Enter += new System.EventHandler(this.TirarMascaraMonetaria);
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApenasValorNumerico);
            this.txtValor.Leave += new System.EventHandler(this.RetornarMascaraMonetaria);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(71, 240);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(48, 13);
            this.lblValor.TabIndex = 31;
            this.lblValor.Text = "Valor R$";
            // 
            // chkAtivoBko
            // 
            this.chkAtivoBko.AutoSize = true;
            this.chkAtivoBko.Location = new System.Drawing.Point(207, 259);
            this.chkAtivoBko.Name = "chkAtivoBko";
            this.chkAtivoBko.Size = new System.Drawing.Size(125, 17);
            this.chkAtivoBko.TabIndex = 33;
            this.chkAtivoBko.Text = "Ativo para Backofice";
            this.chkAtivoBko.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Script de Oferta";
            // 
            // cmbScriptOferta
            // 
            this.cmbScriptOferta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbScriptOferta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbScriptOferta.FormattingEnabled = true;
            this.cmbScriptOferta.Location = new System.Drawing.Point(12, 211);
            this.cmbScriptOferta.Name = "cmbScriptOferta";
            this.cmbScriptOferta.Size = new System.Drawing.Size(335, 21);
            this.cmbScriptOferta.TabIndex = 35;
            // 
            // tabDetalhesProduto
            // 
            this.tabDetalhesProduto.Controls.Add(this.tabPage1);
            this.tabDetalhesProduto.Location = new System.Drawing.Point(363, 37);
            this.tabDetalhesProduto.Name = "tabDetalhesProduto";
            this.tabDetalhesProduto.SelectedIndex = 0;
            this.tabDetalhesProduto.Size = new System.Drawing.Size(466, 304);
            this.tabDetalhesProduto.TabIndex = 37;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.tsFaixaDeRecarga);
            this.tabPage1.Controls.Add(this.dgFaixaDeRecarga);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 278);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Faixas de Recarga do Produto";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbFaixaDeRecarga);
            this.groupBox2.Controls.Add(this.chkAtivoFaixaDeRecarga);
            this.groupBox2.Controls.Add(this.txtIdProdutoPermitido);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(3, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 75);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Faixa de Recarga";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Faixa de Recarga";
            // 
            // cmbFaixaDeRecarga
            // 
            this.cmbFaixaDeRecarga.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbFaixaDeRecarga.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFaixaDeRecarga.FormattingEnabled = true;
            this.cmbFaixaDeRecarga.Location = new System.Drawing.Point(68, 36);
            this.cmbFaixaDeRecarga.Name = "cmbFaixaDeRecarga";
            this.cmbFaixaDeRecarga.Size = new System.Drawing.Size(210, 21);
            this.cmbFaixaDeRecarga.TabIndex = 38;
            // 
            // chkAtivoFaixaDeRecarga
            // 
            this.chkAtivoFaixaDeRecarga.AutoSize = true;
            this.chkAtivoFaixaDeRecarga.Checked = true;
            this.chkAtivoFaixaDeRecarga.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivoFaixaDeRecarga.Location = new System.Drawing.Point(284, 37);
            this.chkAtivoFaixaDeRecarga.Name = "chkAtivoFaixaDeRecarga";
            this.chkAtivoFaixaDeRecarga.Size = new System.Drawing.Size(50, 17);
            this.chkAtivoFaixaDeRecarga.TabIndex = 37;
            this.chkAtivoFaixaDeRecarga.Text = "Ativo";
            this.chkAtivoFaixaDeRecarga.UseVisualStyleBackColor = true;
            // 
            // txtIdProdutoPermitido
            // 
            this.txtIdProdutoPermitido.Location = new System.Drawing.Point(6, 37);
            this.txtIdProdutoPermitido.MaxLength = 100;
            this.txtIdProdutoPermitido.Name = "txtIdProdutoPermitido";
            this.txtIdProdutoPermitido.Size = new System.Drawing.Size(56, 20);
            this.txtIdProdutoPermitido.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Id";
            // 
            // tsFaixaDeRecarga
            // 
            this.tsFaixaDeRecarga.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tsFaixaDeRecarga.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFaixadeRecarga_btnNovo,
            this.toolStripSeparator4,
            this.tsFaixaDeRecarga_btnCancelar,
            this.toolStripSeparator1,
            this.tsFaixaDeRecarga_btnSalvar,
            this.toolStripSeparator2});
            this.tsFaixaDeRecarga.Location = new System.Drawing.Point(3, 153);
            this.tsFaixaDeRecarga.Name = "tsFaixaDeRecarga";
            this.tsFaixaDeRecarga.Padding = new System.Windows.Forms.Padding(0);
            this.tsFaixaDeRecarga.Size = new System.Drawing.Size(452, 25);
            this.tsFaixaDeRecarga.TabIndex = 39;
            this.tsFaixaDeRecarga.Text = "toolStrip2";
            // 
            // tsFaixadeRecarga_btnNovo
            // 
            this.tsFaixadeRecarga_btnNovo.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.add;
            this.tsFaixadeRecarga_btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFaixadeRecarga_btnNovo.Name = "tsFaixadeRecarga_btnNovo";
            this.tsFaixadeRecarga_btnNovo.Size = new System.Drawing.Size(56, 22);
            this.tsFaixadeRecarga_btnNovo.Text = "Novo";
            this.tsFaixadeRecarga_btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsFaixadeRecarga_btnNovo.Click += new System.EventHandler(this.tsFaixadeRecarga_btnNovo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsFaixaDeRecarga_btnCancelar
            // 
            this.tsFaixaDeRecarga_btnCancelar.Enabled = false;
            this.tsFaixaDeRecarga_btnCancelar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.cancel;
            this.tsFaixaDeRecarga_btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFaixaDeRecarga_btnCancelar.Name = "tsFaixaDeRecarga_btnCancelar";
            this.tsFaixaDeRecarga_btnCancelar.Size = new System.Drawing.Size(73, 22);
            this.tsFaixaDeRecarga_btnCancelar.Text = "Cancelar";
            this.tsFaixaDeRecarga_btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsFaixaDeRecarga_btnCancelar.Click += new System.EventHandler(this.tsFaixaDeRecarga_btnCancelar_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsFaixaDeRecarga_btnSalvar
            // 
            this.tsFaixaDeRecarga_btnSalvar.Enabled = false;
            this.tsFaixaDeRecarga_btnSalvar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
            this.tsFaixaDeRecarga_btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFaixaDeRecarga_btnSalvar.Name = "tsFaixaDeRecarga_btnSalvar";
            this.tsFaixaDeRecarga_btnSalvar.Size = new System.Drawing.Size(58, 22);
            this.tsFaixaDeRecarga_btnSalvar.Text = "Salvar";
            this.tsFaixaDeRecarga_btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsFaixaDeRecarga_btnSalvar.Click += new System.EventHandler(this.tsFaixaDeRecarga_btnSalvar_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // dgFaixaDeRecarga
            // 
            this.dgFaixaDeRecarga.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFaixaDeRecarga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFaixaDeRecarga.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDgFaixaDeRecarga_Id,
            this.colDgFaixadeRecarga_IdFaixaDeRecarga,
            this.colDgFaixaDeRecarga_IdProduto,
            this.colDgFaixaDeRecarga_Nome,
            this.colDgFaixaDeRecarga_Ativo});
            this.dgFaixaDeRecarga.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgFaixaDeRecarga.EnableHeadersVisualStyles = false;
            this.dgFaixaDeRecarga.Location = new System.Drawing.Point(3, 3);
            this.dgFaixaDeRecarga.MultiSelect = false;
            this.dgFaixaDeRecarga.Name = "dgFaixaDeRecarga";
            this.dgFaixaDeRecarga.ReadOnly = true;
            this.dgFaixaDeRecarga.RowHeadersVisible = false;
            this.dgFaixaDeRecarga.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFaixaDeRecarga.Size = new System.Drawing.Size(452, 150);
            this.dgFaixaDeRecarga.TabIndex = 1;
            this.dgFaixaDeRecarga.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFaixaDeRecarga_CellDoubleClick_1);
            // 
            // colDgFaixaDeRecarga_Id
            // 
            this.colDgFaixaDeRecarga_Id.DataPropertyName = "Id";
            this.colDgFaixaDeRecarga_Id.HeaderText = "Id";
            this.colDgFaixaDeRecarga_Id.Name = "colDgFaixaDeRecarga_Id";
            this.colDgFaixaDeRecarga_Id.ReadOnly = true;
            // 
            // colDgFaixadeRecarga_IdFaixaDeRecarga
            // 
            this.colDgFaixadeRecarga_IdFaixaDeRecarga.DataPropertyName = "IdFaixaDeRecarga";
            this.colDgFaixadeRecarga_IdFaixaDeRecarga.HeaderText = "IdFaixaDeRecarga";
            this.colDgFaixadeRecarga_IdFaixaDeRecarga.Name = "colDgFaixadeRecarga_IdFaixaDeRecarga";
            this.colDgFaixadeRecarga_IdFaixaDeRecarga.ReadOnly = true;
            this.colDgFaixadeRecarga_IdFaixaDeRecarga.Visible = false;
            // 
            // colDgFaixaDeRecarga_IdProduto
            // 
            this.colDgFaixaDeRecarga_IdProduto.DataPropertyName = "IdProduto";
            this.colDgFaixaDeRecarga_IdProduto.HeaderText = "IdProduto";
            this.colDgFaixaDeRecarga_IdProduto.Name = "colDgFaixaDeRecarga_IdProduto";
            this.colDgFaixaDeRecarga_IdProduto.ReadOnly = true;
            this.colDgFaixaDeRecarga_IdProduto.Visible = false;
            // 
            // colDgFaixaDeRecarga_Nome
            // 
            this.colDgFaixaDeRecarga_Nome.DataPropertyName = "FaixaDeRecarga";
            this.colDgFaixaDeRecarga_Nome.HeaderText = "Faixa de Recarga";
            this.colDgFaixaDeRecarga_Nome.Name = "colDgFaixaDeRecarga_Nome";
            this.colDgFaixaDeRecarga_Nome.ReadOnly = true;
            // 
            // colDgFaixaDeRecarga_Ativo
            // 
            this.colDgFaixaDeRecarga_Ativo.DataPropertyName = "Ativo";
            this.colDgFaixaDeRecarga_Ativo.HeaderText = "Ativo";
            this.colDgFaixaDeRecarga_Ativo.Name = "colDgFaixaDeRecarga_Ativo";
            this.colDgFaixaDeRecarga_Ativo.ReadOnly = true;
            // 
            // txtIdProduto
            // 
            this.txtIdProduto.Enabled = false;
            this.txtIdProduto.Location = new System.Drawing.Point(11, 53);
            this.txtIdProduto.MaxLength = 100;
            this.txtIdProduto.Name = "txtIdProduto";
            this.txtIdProduto.Size = new System.Drawing.Size(56, 20);
            this.txtIdProduto.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Id Produto";
            // 
            // ProdutoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(836, 382);
            this.Controls.Add(this.txtIdProduto);
            this.Controls.Add(this.tabDetalhesProduto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbScriptOferta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkAtivoBko);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.txtOrdem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.cmbCampanha);
            this.Controls.Add(this.lblCampanha);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.cmbTipoDeProduto);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProdutoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Produto";
            this.Load += new System.EventHandler(this.ProdutoForm_Load);
            this.tabDetalhesProduto.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tsFaixaDeRecarga.ResumeLayout(false);
            this.tsFaixaDeRecarga.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFaixaDeRecarga)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtOrdem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.Label lblCampanha;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.ComboBox cmbTipoDeProduto;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.CheckBox chkAtivoBko;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbScriptOferta;
        private System.Windows.Forms.TabControl tabDetalhesProduto;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFaixaDeRecarga;
        private System.Windows.Forms.CheckBox chkAtivoFaixaDeRecarga;
        private System.Windows.Forms.TextBox txtIdProdutoPermitido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip tsFaixaDeRecarga;
        private System.Windows.Forms.ToolStripButton tsFaixadeRecarga_btnNovo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsFaixaDeRecarga_btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridView dgFaixaDeRecarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDgFaixaDeRecarga_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDgFaixadeRecarga_IdFaixaDeRecarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDgFaixaDeRecarga_IdProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDgFaixaDeRecarga_Nome;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDgFaixaDeRecarga_Ativo;
        private System.Windows.Forms.TextBox txtIdProduto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsFaixaDeRecarga_btnCancelar;
    }
}