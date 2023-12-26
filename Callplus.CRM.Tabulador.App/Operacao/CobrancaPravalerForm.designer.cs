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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CobrancaPravalerForm));
			this.tsOferta = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.tsOferta_cmbTipoStatusOferta = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
			this.tsOferta_cmbStatusOferta = new System.Windows.Forms.ToolStripComboBox();
			this.tsOferta_btnSalvar = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsOferta_btnChecklist = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label36 = new System.Windows.Forms.Label();
			this.txtObservacao = new System.Windows.Forms.TextBox();
			this.tcOferta = new System.Windows.Forms.TabControl();
			this.tcOferta_tpDadosDoCliente = new System.Windows.Forms.TabPage();
			this.gbDadosPessoais = new System.Windows.Forms.GroupBox();
			this.tcOferta_tpAcordo = new System.Windows.Forms.TabPage();
			this.gcAcordos = new System.Windows.Forms.GroupBox();
			this.dgAcordo = new System.Windows.Forms.DataGridView();
			this.btnNovoAcordo = new System.Windows.Forms.Button();
			this.gbTop = new System.Windows.Forms.GroupBox();
			this.dgContrato = new System.Windows.Forms.DataGridView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this._containerDeLayoutDinamico = new Callplus.CRM.Tabulador.App.Controles.CamposDinamicos.ContainerDeLayoutDeCamposDinamicos();
			this.tsOferta.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tcOferta.SuspendLayout();
			this.tcOferta_tpDadosDoCliente.SuspendLayout();
			this.gbDadosPessoais.SuspendLayout();
			this.tcOferta_tpAcordo.SuspendLayout();
			this.gcAcordos.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgAcordo)).BeginInit();
			this.gbTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgContrato)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsOferta
			// 
			this.tsOferta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tsOferta.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsOferta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tsOferta_cmbTipoStatusOferta,
            this.toolStripLabel4,
            this.tsOferta_cmbStatusOferta,
            this.tsOferta_btnSalvar,
            this.toolStripSeparator1,
            this.tsOferta_btnChecklist,
            this.toolStripSeparator2});
			this.tsOferta.Location = new System.Drawing.Point(0, 0);
			this.tsOferta.Name = "tsOferta";
			this.tsOferta.Padding = new System.Windows.Forms.Padding(2);
			this.tsOferta.Size = new System.Drawing.Size(911, 27);
			this.tsOferta.TabIndex = 0;
			this.tsOferta.Text = "toolStrip2";
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(42, 20);
			this.toolStripLabel3.Text = "Status:";
			// 
			// tsOferta_cmbTipoStatusOferta
			// 
			this.tsOferta_cmbTipoStatusOferta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsOferta_cmbTipoStatusOferta.Enabled = false;
			this.tsOferta_cmbTipoStatusOferta.Name = "tsOferta_cmbTipoStatusOferta";
			this.tsOferta_cmbTipoStatusOferta.Size = new System.Drawing.Size(121, 23);
			this.tsOferta_cmbTipoStatusOferta.SelectedIndexChanged += new System.EventHandler(this.cmbTipoStatusOferta_SelectedIndexChanged);
			// 
			// toolStripLabel4
			// 
			this.toolStripLabel4.AutoSize = false;
			this.toolStripLabel4.Name = "toolStripLabel4";
			this.toolStripLabel4.Size = new System.Drawing.Size(3, 20);
			// 
			// tsOferta_cmbStatusOferta
			// 
			this.tsOferta_cmbStatusOferta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tsOferta_cmbStatusOferta.DropDownWidth = 400;
			this.tsOferta_cmbStatusOferta.Enabled = false;
			this.tsOferta_cmbStatusOferta.IntegralHeight = false;
			this.tsOferta_cmbStatusOferta.Name = "tsOferta_cmbStatusOferta";
			this.tsOferta_cmbStatusOferta.Size = new System.Drawing.Size(400, 23);
			// 
			// tsOferta_btnSalvar
			// 
			this.tsOferta_btnSalvar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsOferta_btnSalvar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.save;
			this.tsOferta_btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsOferta_btnSalvar.Name = "tsOferta_btnSalvar";
			this.tsOferta_btnSalvar.Size = new System.Drawing.Size(58, 20);
			this.tsOferta_btnSalvar.Text = "Salvar";
			this.tsOferta_btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.tsOferta_btnSalvar.ToolTipText = "Salvar Venda";
			this.tsOferta_btnSalvar.Click += new System.EventHandler(this.tsOferta_btnSalvar_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// tsOferta_btnChecklist
			// 
			this.tsOferta_btnChecklist.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsOferta_btnChecklist.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.check;
			this.tsOferta_btnChecklist.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsOferta_btnChecklist.Name = "tsOferta_btnChecklist";
			this.tsOferta_btnChecklist.Size = new System.Drawing.Size(75, 20);
			this.tsOferta_btnChecklist.Text = "Checklist";
			this.tsOferta_btnChecklist.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.tsOferta_btnChecklist.Click += new System.EventHandler(this.tsOferta_btnChecklist_Click);
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
			// tcOferta
			// 
			this.tcOferta.Controls.Add(this.tcOferta_tpDadosDoCliente);
			this.tcOferta.Controls.Add(this.tcOferta_tpAcordo);
			this.tcOferta.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tcOferta.Location = new System.Drawing.Point(0, 212);
			this.tcOferta.Name = "tcOferta";
			this.tcOferta.SelectedIndex = 0;
			this.tcOferta.Size = new System.Drawing.Size(911, 265);
			this.tcOferta.TabIndex = 2;
			// 
			// tcOferta_tpDadosDoCliente
			// 
			this.tcOferta_tpDadosDoCliente.Controls.Add(this.gbDadosPessoais);
			this.tcOferta_tpDadosDoCliente.Location = new System.Drawing.Point(4, 22);
			this.tcOferta_tpDadosDoCliente.Name = "tcOferta_tpDadosDoCliente";
			this.tcOferta_tpDadosDoCliente.Padding = new System.Windows.Forms.Padding(3);
			this.tcOferta_tpDadosDoCliente.Size = new System.Drawing.Size(903, 239);
			this.tcOferta_tpDadosDoCliente.TabIndex = 0;
			this.tcOferta_tpDadosDoCliente.Text = "Dados do Cliente";
			this.tcOferta_tpDadosDoCliente.UseVisualStyleBackColor = true;
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
			// tcOferta_tpAcordo
			// 
			this.tcOferta_tpAcordo.Controls.Add(this.gcAcordos);
			this.tcOferta_tpAcordo.Location = new System.Drawing.Point(4, 22);
			this.tcOferta_tpAcordo.Name = "tcOferta_tpAcordo";
			this.tcOferta_tpAcordo.Size = new System.Drawing.Size(903, 239);
			this.tcOferta_tpAcordo.TabIndex = 3;
			this.tcOferta_tpAcordo.Text = "Acordos";
			this.tcOferta_tpAcordo.UseVisualStyleBackColor = true;
			// 
			// gcAcordos
			// 
			this.gcAcordos.Controls.Add(this.dgAcordo);
			this.gcAcordos.Controls.Add(this.btnNovoAcordo);
			this.gcAcordos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gcAcordos.Location = new System.Drawing.Point(0, 0);
			this.gcAcordos.Name = "gcAcordos";
			this.gcAcordos.Size = new System.Drawing.Size(903, 239);
			this.gcAcordos.TabIndex = 0;
			this.gcAcordos.TabStop = false;
			this.gcAcordos.Text = "Acordos";
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
			this.btnNovoAcordo.Location = new System.Drawing.Point(820, 10);
			this.btnNovoAcordo.Name = "btnNovoAcordo";
			this.btnNovoAcordo.Size = new System.Drawing.Size(80, 25);
			this.btnNovoAcordo.TabIndex = 187;
			this.btnNovoAcordo.Text = "Novo ";
			this.btnNovoAcordo.UseVisualStyleBackColor = true;
			this.btnNovoAcordo.Click += new System.EventHandler(this.btnNovoAcordo_Click);
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
			this.Controls.Add(this.tcOferta);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.tsOferta);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CobrancaPravalerForm";
			this.Text = "Cobrança Pravaler";
			this.Load += new System.EventHandler(this.CobrancaPravalerForm_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OfertaMigracaoPreControleClaroForm_KeyPress);
			this.tsOferta.ResumeLayout(false);
			this.tsOferta.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tcOferta.ResumeLayout(false);
			this.tcOferta_tpDadosDoCliente.ResumeLayout(false);
			this.gbDadosPessoais.ResumeLayout(false);
			this.gbDadosPessoais.PerformLayout();
			this.tcOferta_tpAcordo.ResumeLayout(false);
			this.gcAcordos.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgAcordo)).EndInit();
			this.gbTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgContrato)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip tsOferta;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripComboBox tsOferta_cmbTipoStatusOferta;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private System.Windows.Forms.ToolStripComboBox tsOferta_cmbStatusOferta;
		private System.Windows.Forms.ToolStripButton tsOferta_btnSalvar;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tcOferta;
		private System.Windows.Forms.TabPage tcOferta_tpDadosDoCliente;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.TextBox txtObservacao;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox gbDadosPessoais;
		private System.Windows.Forms.ToolStripButton tsOferta_btnChecklist;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TabPage tcOferta_tpAcordo;
		private System.Windows.Forms.GroupBox gcAcordos;
		private System.Windows.Forms.Button btnNovoAcordo;
		private System.Windows.Forms.DataGridView dgAcordo;
		private System.Windows.Forms.GroupBox gbTop;
		private System.Windows.Forms.DataGridView dgContrato;
		private System.Windows.Forms.Panel panel1;
		private Controles.CamposDinamicos.ContainerDeLayoutDeCamposDinamicos _containerDeLayoutDinamico;
	}
}