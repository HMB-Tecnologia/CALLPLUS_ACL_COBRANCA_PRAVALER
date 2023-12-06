namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class EnderecoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnderecoForm));
            this.dgLista = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logradouro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bairro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.complemento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pontoReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProspect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsEndereco = new System.Windows.Forms.ToolStrip();
            this.tsEndereco_btnNovo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEndereco_btnEditar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEndereco_btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEndereco_btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEndereco_btnSelecionar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBuscaRapida = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuscaRapida = new System.Windows.Forms.TextBox();
            this.txtSenhaLiberar = new System.Windows.Forms.TextBox();
            this.txtLoginLiberar = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.btnLiberarEdicaoManual = new System.Windows.Forms.Button();
            this.btnPesquisarEndereco = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPontoReferencia = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbUf = new System.Windows.Forms.ComboBox();
            this.txtCep = new System.Windows.Forms.TextBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgLista)).BeginInit();
            this.panel1.SuspendLayout();
            this.tsEndereco.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgLista
            // 
            this.dgLista.AllowUserToAddRows = false;
            this.dgLista.AllowUserToDeleteRows = false;
            this.dgLista.AllowUserToResizeRows = false;
            this.dgLista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLista.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgLista.ColumnHeadersHeight = 25;
            this.dgLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cep,
            this.logradouro,
            this.numero,
            this.bairro,
            this.cidade,
            this.uf,
            this.complemento,
            this.pontoReferencia,
            this.idProspect});
            this.dgLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgLista.EnableHeadersVisualStyles = false;
            this.dgLista.Location = new System.Drawing.Point(0, 0);
            this.dgLista.MultiSelect = false;
            this.dgLista.Name = "dgLista";
            this.dgLista.ReadOnly = true;
            this.dgLista.RowHeadersVisible = false;
            this.dgLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLista.Size = new System.Drawing.Size(665, 136);
            this.dgLista.TabIndex = 0;
            this.dgLista.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLista_CellDoubleClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "Id";
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // cep
            // 
            this.cep.DataPropertyName = "Cep";
            this.cep.HeaderText = "CEP";
            this.cep.Name = "cep";
            this.cep.ReadOnly = true;
            // 
            // logradouro
            // 
            this.logradouro.DataPropertyName = "Logradouro";
            this.logradouro.HeaderText = "Logradouro";
            this.logradouro.Name = "logradouro";
            this.logradouro.ReadOnly = true;
            // 
            // numero
            // 
            this.numero.DataPropertyName = "Numero";
            this.numero.HeaderText = "Numero";
            this.numero.Name = "numero";
            this.numero.ReadOnly = true;
            // 
            // bairro
            // 
            this.bairro.DataPropertyName = "Bairro";
            this.bairro.HeaderText = "Bairro";
            this.bairro.Name = "bairro";
            this.bairro.ReadOnly = true;
            // 
            // cidade
            // 
            this.cidade.DataPropertyName = "Cidade";
            this.cidade.HeaderText = "Cidade";
            this.cidade.Name = "cidade";
            this.cidade.ReadOnly = true;
            // 
            // uf
            // 
            this.uf.DataPropertyName = "Uf";
            this.uf.HeaderText = "UF";
            this.uf.Name = "uf";
            this.uf.ReadOnly = true;
            // 
            // complemento
            // 
            this.complemento.DataPropertyName = "Complemento";
            this.complemento.HeaderText = "Complemento";
            this.complemento.Name = "complemento";
            this.complemento.ReadOnly = true;
            this.complemento.Visible = false;
            // 
            // pontoReferencia
            // 
            this.pontoReferencia.DataPropertyName = "pontoReferencia";
            this.pontoReferencia.HeaderText = "Ponto de Referência";
            this.pontoReferencia.Name = "pontoReferencia";
            this.pontoReferencia.ReadOnly = true;
            this.pontoReferencia.Visible = false;
            // 
            // idProspect
            // 
            this.idProspect.DataPropertyName = "idProspect";
            this.idProspect.HeaderText = "idProspect";
            this.idProspect.Name = "idProspect";
            this.idProspect.ReadOnly = true;
            this.idProspect.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsEndereco);
            this.panel1.Controls.Add(this.btnBuscaRapida);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBuscaRapida);
            this.panel1.Controls.Add(this.txtSenhaLiberar);
            this.panel1.Controls.Add(this.txtLoginLiberar);
            this.panel1.Controls.Add(this.label47);
            this.panel1.Controls.Add(this.btnLiberarEdicaoManual);
            this.panel1.Controls.Add(this.btnPesquisarEndereco);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.txtLogradouro);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.txtPontoReferencia);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txtBairro);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.txtComplemento);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtNumero);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.cmbUf);
            this.panel1.Controls.Add(this.txtCep);
            this.panel1.Controls.Add(this.txtCidade);
            this.panel1.Controls.Add(this.dgLista);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 386);
            this.panel1.TabIndex = 471;
            // 
            // tsEndereco
            // 
            this.tsEndereco.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tsEndereco.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEndereco_btnNovo,
            this.toolStripSeparator4,
            this.tsEndereco_btnEditar,
            this.toolStripSeparator8,
            this.tsEndereco_btnCancelar,
            this.toolStripSeparator5,
            this.tsEndereco_btnSalvar,
            this.toolStripSeparator1,
            this.tsEndereco_btnSelecionar,
            this.toolStripSeparator2});
            this.tsEndereco.Location = new System.Drawing.Point(0, 136);
            this.tsEndereco.Name = "tsEndereco";
            this.tsEndereco.Padding = new System.Windows.Forms.Padding(0);
            this.tsEndereco.Size = new System.Drawing.Size(665, 25);
            this.tsEndereco.TabIndex = 1;
            this.tsEndereco.Text = "toolStrip2";
            // 
            // tsEndereco_btnNovo
            // 
            this.tsEndereco_btnNovo.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.add;
            this.tsEndereco_btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEndereco_btnNovo.Name = "tsEndereco_btnNovo";
            this.tsEndereco_btnNovo.Size = new System.Drawing.Size(56, 22);
            this.tsEndereco_btnNovo.Text = "Novo";
            this.tsEndereco_btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEndereco_btnNovo.Click += new System.EventHandler(this.tsEndereco_btnNovo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEndereco_btnEditar
            // 
            this.tsEndereco_btnEditar.Enabled = false;
            this.tsEndereco_btnEditar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.edit;
            this.tsEndereco_btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEndereco_btnEditar.Name = "tsEndereco_btnEditar";
            this.tsEndereco_btnEditar.Size = new System.Drawing.Size(57, 22);
            this.tsEndereco_btnEditar.Text = "Editar";
            this.tsEndereco_btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEndereco_btnEditar.Click += new System.EventHandler(this.tsEndereco_btnEditar_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEndereco_btnCancelar
            // 
            this.tsEndereco_btnCancelar.Enabled = false;
            this.tsEndereco_btnCancelar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.cancel;
            this.tsEndereco_btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEndereco_btnCancelar.Name = "tsEndereco_btnCancelar";
            this.tsEndereco_btnCancelar.Size = new System.Drawing.Size(73, 22);
            this.tsEndereco_btnCancelar.Text = "Cancelar";
            this.tsEndereco_btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEndereco_btnCancelar.Click += new System.EventHandler(this.tsEndereco_btnCancelar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEndereco_btnSalvar
            // 
            this.tsEndereco_btnSalvar.Enabled = false;
            this.tsEndereco_btnSalvar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.save;
            this.tsEndereco_btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEndereco_btnSalvar.Name = "tsEndereco_btnSalvar";
            this.tsEndereco_btnSalvar.Size = new System.Drawing.Size(58, 22);
            this.tsEndereco_btnSalvar.Text = "Salvar";
            this.tsEndereco_btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEndereco_btnSalvar.Click += new System.EventHandler(this.tsEndereco_btnSalvar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEndereco_btnSelecionar
            // 
            this.tsEndereco_btnSelecionar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsEndereco_btnSelecionar.Enabled = false;
            this.tsEndereco_btnSelecionar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.check;
            this.tsEndereco_btnSelecionar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEndereco_btnSelecionar.Name = "tsEndereco_btnSelecionar";
            this.tsEndereco_btnSelecionar.Size = new System.Drawing.Size(81, 22);
            this.tsEndereco_btnSelecionar.Text = "Selecionar";
            this.tsEndereco_btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEndereco_btnSelecionar.Click += new System.EventHandler(this.tsEndereco_btnSelecionar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnBuscaRapida
            // 
            this.btnBuscaRapida.Enabled = false;
            this.btnBuscaRapida.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.search;
            this.btnBuscaRapida.Location = new System.Drawing.Point(86, 182);
            this.btnBuscaRapida.Name = "btnBuscaRapida";
            this.btnBuscaRapida.Size = new System.Drawing.Size(21, 22);
            this.btnBuscaRapida.TabIndex = 4;
            this.btnBuscaRapida.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnBuscaRapida.UseVisualStyleBackColor = true;
            this.btnBuscaRapida.Click += new System.EventHandler(this.btnBuscaRapida_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Busca rápida";
            // 
            // txtBuscaRapida
            // 
            this.txtBuscaRapida.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtBuscaRapida.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscaRapida.Enabled = false;
            this.txtBuscaRapida.Location = new System.Drawing.Point(14, 183);
            this.txtBuscaRapida.MaxLength = 8;
            this.txtBuscaRapida.Name = "txtBuscaRapida";
            this.txtBuscaRapida.ReadOnly = true;
            this.txtBuscaRapida.Size = new System.Drawing.Size(66, 20);
            this.txtBuscaRapida.TabIndex = 3;
            this.txtBuscaRapida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscaRapida_KeyPress);
            // 
            // txtSenhaLiberar
            // 
            this.txtSenhaLiberar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtSenhaLiberar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenhaLiberar.Enabled = false;
            this.txtSenhaLiberar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaLiberar.Location = new System.Drawing.Point(548, 184);
            this.txtSenhaLiberar.MaxLength = 100;
            this.txtSenhaLiberar.Name = "txtSenhaLiberar";
            this.txtSenhaLiberar.ReadOnly = true;
            this.txtSenhaLiberar.Size = new System.Drawing.Size(51, 21);
            this.txtSenhaLiberar.TabIndex = 8;
            this.txtSenhaLiberar.UseSystemPasswordChar = true;
            // 
            // txtLoginLiberar
            // 
            this.txtLoginLiberar.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLoginLiberar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoginLiberar.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtLoginLiberar.Enabled = false;
            this.txtLoginLiberar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginLiberar.Location = new System.Drawing.Point(470, 184);
            this.txtLoginLiberar.MaxLength = 50;
            this.txtLoginLiberar.Name = "txtLoginLiberar";
            this.txtLoginLiberar.ReadOnly = true;
            this.txtLoginLiberar.Size = new System.Drawing.Size(72, 21);
            this.txtLoginLiberar.TabIndex = 7;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(467, 168);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(150, 13);
            this.label47.TabIndex = 6;
            this.label47.Text = "Liberar edição (Login e senha)";
            // 
            // btnLiberarEdicaoManual
            // 
            this.btnLiberarEdicaoManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLiberarEdicaoManual.Enabled = false;
            this.btnLiberarEdicaoManual.Location = new System.Drawing.Point(604, 183);
            this.btnLiberarEdicaoManual.Name = "btnLiberarEdicaoManual";
            this.btnLiberarEdicaoManual.Size = new System.Drawing.Size(48, 23);
            this.btnLiberarEdicaoManual.TabIndex = 9;
            this.btnLiberarEdicaoManual.Text = "Liberar";
            this.btnLiberarEdicaoManual.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnLiberarEdicaoManual.UseVisualStyleBackColor = true;
            this.btnLiberarEdicaoManual.Click += new System.EventHandler(this.btnLiberarEdicaoManual_Click);
            // 
            // btnPesquisarEndereco
            // 
            this.btnPesquisarEndereco.Enabled = false;
            this.btnPesquisarEndereco.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.search2;
            this.btnPesquisarEndereco.Location = new System.Drawing.Point(112, 182);
            this.btnPesquisarEndereco.Name = "btnPesquisarEndereco";
            this.btnPesquisarEndereco.Size = new System.Drawing.Size(101, 23);
            this.btnPesquisarEndereco.TabIndex = 5;
            this.btnPesquisarEndereco.Text = "Não sei o CEP";
            this.btnPesquisarEndereco.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnPesquisarEndereco.UseVisualStyleBackColor = true;
            this.btnPesquisarEndereco.Click += new System.EventHandler(this.btnPesquisarEndereco_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 335);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(28, 13);
            this.label21.TabIndex = 22;
            this.label21.Text = "UF *";
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLogradouro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogradouro.Enabled = false;
            this.txtLogradouro.Location = new System.Drawing.Point(112, 234);
            this.txtLogradouro.MaxLength = 100;
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.ReadOnly = true;
            this.txtLogradouro.Size = new System.Drawing.Size(540, 20);
            this.txtLogradouro.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(113, 257);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(71, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Complemento";
            // 
            // txtPontoReferencia
            // 
            this.txtPontoReferencia.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtPontoReferencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPontoReferencia.Enabled = false;
            this.txtPontoReferencia.Location = new System.Drawing.Point(112, 352);
            this.txtPontoReferencia.MaxLength = 100;
            this.txtPontoReferencia.Name = "txtPontoReferencia";
            this.txtPontoReferencia.Size = new System.Drawing.Size(540, 20);
            this.txtPontoReferencia.TabIndex = 25;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(109, 218);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Logradouro *";
            // 
            // txtBairro
            // 
            this.txtBairro.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtBairro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBairro.Enabled = false;
            this.txtBairro.Location = new System.Drawing.Point(14, 312);
            this.txtBairro.MaxLength = 100;
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.ReadOnly = true;
            this.txtBairro.Size = new System.Drawing.Size(311, 20);
            this.txtBairro.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 218);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "CEP *";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(328, 296);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 13);
            this.label22.TabIndex = 20;
            this.label22.Text = "Cidade *";
            // 
            // txtComplemento
            // 
            this.txtComplemento.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtComplemento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComplemento.Enabled = false;
            this.txtComplemento.Location = new System.Drawing.Point(112, 273);
            this.txtComplemento.MaxLength = 100;
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(540, 20);
            this.txtComplemento.TabIndex = 17;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(108, 337);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "Ponto de Referência";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 257);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Número *";
            // 
            // txtNumero
            // 
            this.txtNumero.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtNumero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(14, 273);
            this.txtNumero.MaxLength = 10;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(92, 20);
            this.txtNumero.TabIndex = 15;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 296);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 13);
            this.label23.TabIndex = 18;
            this.label23.Text = "Bairro *";
            // 
            // cmbUf
            // 
            this.cmbUf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUf.Enabled = false;
            this.cmbUf.FormattingEnabled = true;
            this.cmbUf.Items.AddRange(new object[] {
            "SELECIONE...",
            "AC",
            "AL",
            "AM",
            "AP",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MG",
            "MS",
            "MT",
            "PA",
            "PB",
            "PE",
            "PI",
            "PR",
            "RJ",
            "RN",
            "RO",
            "RR",
            "RS",
            "SC",
            "SE",
            "SP",
            "TO"});
            this.cmbUf.Location = new System.Drawing.Point(14, 351);
            this.cmbUf.Name = "cmbUf";
            this.cmbUf.Size = new System.Drawing.Size(92, 21);
            this.cmbUf.TabIndex = 23;
            // 
            // txtCep
            // 
            this.txtCep.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCep.Enabled = false;
            this.txtCep.Location = new System.Drawing.Point(14, 234);
            this.txtCep.MaxLength = 8;
            this.txtCep.Name = "txtCep";
            this.txtCep.ReadOnly = true;
            this.txtCep.Size = new System.Drawing.Size(92, 20);
            this.txtCep.TabIndex = 11;
            // 
            // txtCidade
            // 
            this.txtCidade.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCidade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCidade.Enabled = false;
            this.txtCidade.Location = new System.Drawing.Point(331, 312);
            this.txtCidade.MaxLength = 100;
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.ReadOnly = true;
            this.txtCidade.Size = new System.Drawing.Size(321, 20);
            this.txtCidade.TabIndex = 21;
            // 
            // EnderecoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(665, 386);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnderecoForm";
            this.Text = "Endereços";
            this.Load += new System.EventHandler(this.EnderecoForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EnderecoForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dgLista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsEndereco.ResumeLayout(false);
            this.tsEndereco.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgLista;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip tsEndereco;
        private System.Windows.Forms.ToolStripButton tsEndereco_btnNovo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsEndereco_btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsEndereco_btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.Button btnBuscaRapida;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBuscaRapida;
        private System.Windows.Forms.TextBox txtSenhaLiberar;
        private System.Windows.Forms.TextBox txtLoginLiberar;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button btnLiberarEdicaoManual;
        private System.Windows.Forms.Button btnPesquisarEndereco;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPontoReferencia;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbUf;
        private System.Windows.Forms.TextBox txtCep;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.ToolStripButton tsEndereco_btnEditar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsEndereco_btnSelecionar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cep;
        private System.Windows.Forms.DataGridViewTextBoxColumn logradouro;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn bairro;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn uf;
        private System.Windows.Forms.DataGridViewTextBoxColumn complemento;
        private System.Windows.Forms.DataGridViewTextBoxColumn pontoReferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProspect;
    }
}