namespace Callplus.CRM.Tabulador.App.Operacao
{
	partial class CobrancaPravalerForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CobrancaPravalerForm));
			this.tsAcordo = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.tsAcordo_cmbTipoStatusAcordo = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.tsAcordo_cmbStatusAcordo = new System.Windows.Forms.ToolStripComboBox();
			this.tsAcordo_btnSalvar = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsAcordo_btnChecklist = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label36 = new System.Windows.Forms.Label();
			this.txtObservacao = new System.Windows.Forms.TextBox();
			this.tcAcordo = new System.Windows.Forms.TabControl();
			this.tcAcordo_tpDadosDoProspect = new System.Windows.Forms.TabPage();
			this.gbDadosPessoais = new System.Windows.Forms.GroupBox();
			this.tcAcordo_tpAcordo = new System.Windows.Forms.TabPage();
			this.gcAcordo = new System.Windows.Forms.GroupBox();
			this.dgAcordo = new System.Windows.Forms.DataGridView();
			this.btnNovoAcordo = new System.Windows.Forms.Button();
			this.tcAcordo_tpDadosAux = new System.Windows.Forms.TabPage();
			this.gbDadosAux = new System.Windows.Forms.GroupBox();
			this.chkEmail = new System.Windows.Forms.CheckBox();
			this.btnCopiar = new System.Windows.Forms.Button();
			this.chkWhatsApp = new System.Windows.Forms.CheckBox();
			this.chkSms = new System.Windows.Forms.CheckBox();
			this.btnEnviarCodBarras = new System.Windows.Forms.Button();
			this.lblCodBarras = new System.Windows.Forms.Label();
			this.txtCodBarras = new System.Windows.Forms.TextBox();
			this.gbTop = new System.Windows.Forms.GroupBox();
			this.dgContrato = new System.Windows.Forms.DataGridView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this._containerDeLayoutDinamico = new Callplus.CRM.Tabulador.App.Controles.CamposDinamicos.ContainerDeLayoutDeCamposDinamicos();
			this.tsAcordo.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tcAcordo.SuspendLayout();
			this.tcAcordo_tpDadosDoProspect.SuspendLayout();
			this.gbDadosPessoais.SuspendLayout();
			this.tcAcordo_tpAcordo.SuspendLayout();
			this.gcAcordo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgAcordo)).BeginInit();
			this.tcAcordo_tpDadosAux.SuspendLayout();
			this.gbDadosAux.SuspendLayout();
			this.gbTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgContrato)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsAcordo
			// 
			this.tsAcordo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tsAcordo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAcordo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tsAcordo_cmbTipoStatusAcordo,
            this.toolStripLabel4,
            this.tsAcordo_cmbStatusAcordo,
            this.tsAcordo_btnSalvar,
            this.toolStripSeparator1,
            this.tsAcordo_btnChecklist,
            this.toolStripSeparator2});
			this.tsAcordo.Location = new System.Drawing.Point(0, 0);
			this.tsAcordo.Name = "tsAcordo";
			this.tsAcordo.Padding = new System.Windows.Forms.Padding(2);
			this.tsAcordo.Size = new System.Drawing.Size(911, 27);
			this.tsAcordo.TabIndex = 0;
			this.tsAcordo.Text = "toolStrip2";
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(42, 20);
			this.toolStripLabel3.Text = "Status:";
			// 
			// tsAcordo_cmbTipoStatusAcordo
			// 
			this.tsAcordo_cmbTipoStatusAcordo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsAcordo_cmbTipoStatusAcordo.Enabled = false;
			this.tsAcordo_cmbTipoStatusAcordo.Name = "tsAcordo_cmbTipoStatusAcordo";
			this.tsAcordo_cmbTipoStatusAcordo.Size = new System.Drawing.Size(121, 23);
			this.tsAcordo_cmbTipoStatusAcordo.SelectedIndexChanged += new System.EventHandler(this.cmbTipoStatusOferta_SelectedIndexChanged);
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.AutoSize = false;
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(3, 20);
			// 
			// tsAcordo_cmbStatusAcordo
			// 
			this.tsAcordo_cmbStatusAcordo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsAcordo_cmbStatusAcordo.DropDownWidth = 400;
			this.tsAcordo_cmbStatusAcordo.Enabled = false;
			this.tsAcordo_cmbStatusAcordo.IntegralHeight = false;
			this.tsAcordo_cmbStatusAcordo.Name = "tsAcordo_cmbStatusAcordo";
			this.tsAcordo_cmbStatusAcordo.Size = new System.Drawing.Size(400, 23);
			this.tsAcordo_cmbStatusAcordo.SelectedIndexChanged += new System.EventHandler(this.tsAcordo_cmbStatusAcordo_SelectedIndexChanged);
			// 
			// tsAcordo_btnSalvar
			// 
			this.tsAcordo_btnSalvar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsAcordo_btnSalvar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.save;
			this.tsAcordo_btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAcordo_btnSalvar.Name = "tsAcordo_btnSalvar";
			this.tsAcordo_btnSalvar.Size = new System.Drawing.Size(58, 20);
			this.tsAcordo_btnSalvar.Text = "Salvar";
			this.tsAcordo_btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.tsAcordo_btnSalvar.ToolTipText = "Salvar Venda";
			this.tsAcordo_btnSalvar.Click += new System.EventHandler(this.tsOferta_btnSalvar_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// tsAcordo_btnChecklist
			// 
			this.tsAcordo_btnChecklist.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsAcordo_btnChecklist.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.check;
			this.tsAcordo_btnChecklist.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAcordo_btnChecklist.Name = "tsAcordo_btnChecklist";
			this.tsAcordo_btnChecklist.Size = new System.Drawing.Size(75, 20);
			this.tsAcordo_btnChecklist.Text = "Checklist";
			this.tsAcordo_btnChecklist.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.tsAcordo_btnChecklist.Click += new System.EventHandler(this.tsOferta_btnChecklist_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label36);
			this.panel2.Controls.Add(this.txtObservacao);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 477);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(911, 90);
			this.panel2.TabIndex = 3;
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(9, 2);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(70, 13);
			this.label36.TabIndex = 0;
			this.label36.Text = "Observações";
			// 
			// txtObservacao
			// 
			this.txtObservacao.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.txtObservacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtObservacao.Location = new System.Drawing.Point(12, 19);
			this.txtObservacao.MaxLength = 300;
			this.txtObservacao.Multiline = true;
			this.txtObservacao.Name = "txtObservacao";
			this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtObservacao.Size = new System.Drawing.Size(892, 59);
			this.txtObservacao.TabIndex = 1;
			// 
			// tcAcordo
			// 
			this.tcAcordo.Controls.Add(this.tcAcordo_tpDadosDoProspect);
			this.tcAcordo.Controls.Add(this.tcAcordo_tpAcordo);
			this.tcAcordo.Controls.Add(this.tcAcordo_tpDadosAux);
			this.tcAcordo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tcAcordo.Location = new System.Drawing.Point(0, 212);
			this.tcAcordo.Name = "tcAcordo";
			this.tcAcordo.SelectedIndex = 0;
			this.tcAcordo.Size = new System.Drawing.Size(911, 265);
			this.tcAcordo.TabIndex = 2;
			// 
			// tcAcordo_tpDadosDoProspect
			// 
			this.tcAcordo_tpDadosDoProspect.Controls.Add(this.gbDadosPessoais);
			this.tcAcordo_tpDadosDoProspect.Location = new System.Drawing.Point(4, 22);
			this.tcAcordo_tpDadosDoProspect.Name = "tcAcordo_tpDadosDoProspect";
			this.tcAcordo_tpDadosDoProspect.Padding = new System.Windows.Forms.Padding(3);
			this.tcAcordo_tpDadosDoProspect.Size = new System.Drawing.Size(903, 239);
			this.tcAcordo_tpDadosDoProspect.TabIndex = 0;
			this.tcAcordo_tpDadosDoProspect.Text = "Dados do Prospect";
			this.tcAcordo_tpDadosDoProspect.UseVisualStyleBackColor = true;
			// 
			// gbDadosPessoais
			// 
			this.gbDadosPessoais.Controls.Add(this._containerDeLayoutDinamico);
			this.gbDadosPessoais.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbDadosPessoais.Location = new System.Drawing.Point(3, 3);
			this.gbDadosPessoais.Name = "gbDadosPessoais";
			this.gbDadosPessoais.Size = new System.Drawing.Size(897, 233);
			this.gbDadosPessoais.TabIndex = 0;
			this.gbDadosPessoais.TabStop = false;
			this.gbDadosPessoais.Text = "Dados Pessoais";
			// 
			// tcAcordo_tpAcordo
			// 
			this.tcAcordo_tpAcordo.Controls.Add(this.gcAcordo);
			this.tcAcordo_tpAcordo.Location = new System.Drawing.Point(4, 22);
			this.tcAcordo_tpAcordo.Name = "tcAcordo_tpAcordo";
			this.tcAcordo_tpAcordo.Size = new System.Drawing.Size(903, 239);
			this.tcAcordo_tpAcordo.TabIndex = 3;
			this.tcAcordo_tpAcordo.Text = "Acordo do Prospect";
			this.tcAcordo_tpAcordo.UseVisualStyleBackColor = true;
			// 
			// gcAcordo
			// 
			this.gcAcordo.Controls.Add(this.dgAcordo);
			this.gcAcordo.Controls.Add(this.btnNovoAcordo);
			this.gcAcordo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gcAcordo.Location = new System.Drawing.Point(0, 0);
			this.gcAcordo.Name = "gcAcordo";
			this.gcAcordo.Size = new System.Drawing.Size(903, 239);
			this.gcAcordo.TabIndex = 0;
			this.gcAcordo.TabStop = false;
			this.gcAcordo.Text = "Acordo";
			// 
			// dgAcordo
			// 
			this.dgAcordo.AllowUserToAddRows = false;
			this.dgAcordo.AllowUserToDeleteRows = false;
			this.dgAcordo.AllowUserToOrderColumns = true;
			this.dgAcordo.AllowUserToResizeRows = false;
			this.dgAcordo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgAcordo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgAcordo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dgAcordo.Location = new System.Drawing.Point(3, 51);
			this.dgAcordo.MultiSelect = false;
			this.dgAcordo.Name = "dgAcordo";
			this.dgAcordo.ReadOnly = true;
			this.dgAcordo.RowHeadersVisible = false;
			this.dgAcordo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgAcordo.Size = new System.Drawing.Size(897, 185);
			this.dgAcordo.TabIndex = 188;
			// 
			// btnNovoAcordo
			// 
			this.btnNovoAcordo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNovoAcordo.BackColor = System.Drawing.SystemColors.Control;
			this.btnNovoAcordo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnNovoAcordo.FlatAppearance.BorderSize = 0;
			this.btnNovoAcordo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnNovoAcordo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNovoAcordo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNovoAcordo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovoAcordo.Image")));
			this.btnNovoAcordo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNovoAcordo.Location = new System.Drawing.Point(3, 19);
			this.btnNovoAcordo.Name = "btnNovoAcordo";
			this.btnNovoAcordo.Size = new System.Drawing.Size(80, 25);
			this.btnNovoAcordo.TabIndex = 187;
			this.btnNovoAcordo.Text = "Novo ";
			this.btnNovoAcordo.UseVisualStyleBackColor = true;
			this.btnNovoAcordo.Click += new System.EventHandler(this.btnNovoAcordo_Click);
			// 
			// tcAcordo_tpDadosAux
			// 
			this.tcAcordo_tpDadosAux.Controls.Add(this.gbDadosAux);
			this.tcAcordo_tpDadosAux.Location = new System.Drawing.Point(4, 22);
			this.tcAcordo_tpDadosAux.Name = "tcAcordo_tpDadosAux";
			this.tcAcordo_tpDadosAux.Size = new System.Drawing.Size(903, 239);
			this.tcAcordo_tpDadosAux.TabIndex = 4;
			this.tcAcordo_tpDadosAux.Text = "Dados Auxiliares";
			this.tcAcordo_tpDadosAux.UseVisualStyleBackColor = true;
			// 
			// gbDadosAux
			// 
			this.gbDadosAux.Controls.Add(this.chkEmail);
			this.gbDadosAux.Controls.Add(this.btnCopiar);
			this.gbDadosAux.Controls.Add(this.chkWhatsApp);
			this.gbDadosAux.Controls.Add(this.chkSms);
			this.gbDadosAux.Controls.Add(this.btnEnviarCodBarras);
			this.gbDadosAux.Controls.Add(this.lblCodBarras);
			this.gbDadosAux.Controls.Add(this.txtCodBarras);
			this.gbDadosAux.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbDadosAux.Location = new System.Drawing.Point(0, 0);
			this.gbDadosAux.Name = "gbDadosAux";
			this.gbDadosAux.Size = new System.Drawing.Size(903, 239);
			this.gbDadosAux.TabIndex = 0;
			this.gbDadosAux.TabStop = false;
			this.gbDadosAux.Text = "Dados Auxiliares";
			// 
			// chkEmail
			// 
			this.chkEmail.AutoSize = true;
			this.chkEmail.Location = new System.Drawing.Point(149, 41);
			this.chkEmail.Name = "chkEmail";
			this.chkEmail.Size = new System.Drawing.Size(54, 17);
			this.chkEmail.TabIndex = 4;
			this.chkEmail.Text = "E-mail";
			this.chkEmail.UseVisualStyleBackColor = true;
			this.chkEmail.CheckedChanged += new System.EventHandler(this.chkEmail_CheckedChanged);
			// 
			// btnCopiar
			// 
			this.btnCopiar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.copiar2;
			this.btnCopiar.Location = new System.Drawing.Point(380, 62);
			this.btnCopiar.Name = "btnCopiar";
			this.btnCopiar.Size = new System.Drawing.Size(23, 23);
			this.btnCopiar.TabIndex = 1;
			this.btnCopiar.UseVisualStyleBackColor = true;
			this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
			this.btnCopiar.MouseHover += new System.EventHandler(this.btnCopiar_MouseHover);
			// 
			// chkWhatsApp
			// 
			this.chkWhatsApp.AutoSize = true;
			this.chkWhatsApp.Location = new System.Drawing.Point(67, 41);
			this.chkWhatsApp.Name = "chkWhatsApp";
			this.chkWhatsApp.Size = new System.Drawing.Size(76, 17);
			this.chkWhatsApp.TabIndex = 3;
			this.chkWhatsApp.Text = "WhatsApp";
			this.chkWhatsApp.UseVisualStyleBackColor = true;
			this.chkWhatsApp.CheckedChanged += new System.EventHandler(this.chkWhatsApp_CheckedChanged);
			// 
			// chkSms
			// 
			this.chkSms.AutoSize = true;
			this.chkSms.Location = new System.Drawing.Point(12, 41);
			this.chkSms.Name = "chkSms";
			this.chkSms.Size = new System.Drawing.Size(49, 17);
			this.chkSms.TabIndex = 2;
			this.chkSms.Text = "SMS";
			this.chkSms.UseVisualStyleBackColor = true;
			this.chkSms.CheckedChanged += new System.EventHandler(this.chkSms_CheckedChanged);
			// 
			// btnEnviarCodBarras
			// 
			this.btnEnviarCodBarras.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.enviar3;
			this.btnEnviarCodBarras.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnEnviarCodBarras.Location = new System.Drawing.Point(12, 89);
			this.btnEnviarCodBarras.Name = "btnEnviarCodBarras";
			this.btnEnviarCodBarras.Size = new System.Drawing.Size(75, 23);
			this.btnEnviarCodBarras.TabIndex = 5;
			this.btnEnviarCodBarras.Text = "Enviar";
			this.btnEnviarCodBarras.UseVisualStyleBackColor = true;
			this.btnEnviarCodBarras.Click += new System.EventHandler(this.btnEnviarCodBarras_Click);
			// 
			// lblCodBarras
			// 
			this.lblCodBarras.AutoSize = true;
			this.lblCodBarras.Location = new System.Drawing.Point(9, 18);
			this.lblCodBarras.Name = "lblCodBarras";
			this.lblCodBarras.Size = new System.Drawing.Size(88, 13);
			this.lblCodBarras.TabIndex = 1;
			this.lblCodBarras.Text = "Código de Barras";
			// 
			// txtCodBarras
			// 
			this.txtCodBarras.Location = new System.Drawing.Point(12, 63);
			this.txtCodBarras.Name = "txtCodBarras";
			this.txtCodBarras.ReadOnly = true;
			this.txtCodBarras.Size = new System.Drawing.Size(365, 20);
			this.txtCodBarras.TabIndex = 0;
			// 
			// gbTop
			// 
			this.gbTop.Controls.Add(this.dgContrato);
			this.gbTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbTop.Location = new System.Drawing.Point(0, 0);
			this.gbTop.Name = "gbTop";
			this.gbTop.Size = new System.Drawing.Size(911, 163);
			this.gbTop.TabIndex = 3;
			this.gbTop.TabStop = false;
			this.gbTop.Text = "Contratos";
			// 
			// dgContrato
			// 
			this.dgContrato.AllowUserToAddRows = false;
			this.dgContrato.AllowUserToDeleteRows = false;
			this.dgContrato.AllowUserToResizeRows = false;
			this.dgContrato.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgContrato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgContrato.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dgContrato.Location = new System.Drawing.Point(3, 19);
			this.dgContrato.MultiSelect = false;
			this.dgContrato.Name = "dgContrato";
			this.dgContrato.ReadOnly = true;
			this.dgContrato.RowHeadersVisible = false;
			this.dgContrato.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgContrato.Size = new System.Drawing.Size(905, 141);
			this.dgContrato.TabIndex = 185;
			this.dgContrato.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgContrato_CellClick);
			this.dgContrato.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgContrato_CellContentClick);
			// 
			// panel3
			// 
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 27);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(911, 10);
			this.panel3.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gbTop);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 37);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(911, 169);
			this.panel1.TabIndex = 4;
			// 
			// _containerDeLayoutDinamico
			// 
			this._containerDeLayoutDinamico.AutoScroll = true;
			this._containerDeLayoutDinamico.AutoSize = true;
			this._containerDeLayoutDinamico.Dock = System.Windows.Forms.DockStyle.Fill;
			this._containerDeLayoutDinamico.Location = new System.Drawing.Point(3, 16);
			this._containerDeLayoutDinamico.Name = "_containerDeLayoutDinamico";
			this._containerDeLayoutDinamico.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._containerDeLayoutDinamico.Size = new System.Drawing.Size(891, 214);
			this._containerDeLayoutDinamico.TabIndex = 1;
			// 
			// CobrancaPravalerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(911, 567);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tcAcordo);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.tsAcordo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CobrancaPravalerForm";
			this.Text = "Cobrança Pravaler";
			this.Load += new System.EventHandler(this.CobrancaPravalerForm_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CobrancaPravalerForm_KeyPress);
			this.tsAcordo.ResumeLayout(false);
			this.tsAcordo.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tcAcordo.ResumeLayout(false);
			this.tcAcordo_tpDadosDoProspect.ResumeLayout(false);
			this.gbDadosPessoais.ResumeLayout(false);
			this.gbDadosPessoais.PerformLayout();
			this.tcAcordo_tpAcordo.ResumeLayout(false);
			this.gcAcordo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgAcordo)).EndInit();
			this.tcAcordo_tpDadosAux.ResumeLayout(false);
			this.gbDadosAux.ResumeLayout(false);
			this.gbDadosAux.PerformLayout();
			this.gbTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgContrato)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip tsAcordo;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripComboBox tsAcordo_cmbTipoStatusAcordo;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripComboBox tsAcordo_cmbStatusAcordo;
		private System.Windows.Forms.ToolStripButton tsAcordo_btnSalvar;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tcAcordo;
		private System.Windows.Forms.TabPage tcAcordo_tpDadosDoProspect;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.TextBox txtObservacao;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox gbDadosPessoais;
		private System.Windows.Forms.ToolStripButton tsAcordo_btnChecklist;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TabPage tcAcordo_tpAcordo;
		private System.Windows.Forms.GroupBox gcAcordo;
		private System.Windows.Forms.Button btnNovoAcordo;
		private System.Windows.Forms.DataGridView dgAcordo;
		private System.Windows.Forms.GroupBox gbTop;
		private System.Windows.Forms.DataGridView dgContrato;
		private System.Windows.Forms.Panel panel1;
		private Controles.CamposDinamicos.ContainerDeLayoutDeCamposDinamicos _containerDeLayoutDinamico;
		private System.Windows.Forms.TabPage tcAcordo_tpDadosAux;
		private System.Windows.Forms.GroupBox gbDadosAux;
		private System.Windows.Forms.Button btnEnviarCodBarras;
		private System.Windows.Forms.Label lblCodBarras;
		private System.Windows.Forms.TextBox txtCodBarras;
		private System.Windows.Forms.CheckBox chkWhatsApp;
		private System.Windows.Forms.CheckBox chkSms;
		private System.Windows.Forms.CheckBox chkEmail;
		private System.Windows.Forms.Button btnCopiar;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}