namespace Callplus.CRM.Administracao.App.Backoffice.AuditoriaDeVendas
{
    partial class ListaAuditoriaDeVendaForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaAuditoriaDeVendaForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblTotalRegistros = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.txtBuscaRapida = new System.Windows.Forms.TextBox();
			this.btnBuscaRapida = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.cmbAuditoria = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.txtCpf = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.btnExportar = new System.Windows.Forms.Button();
			this.btnGerarDados = new System.Windows.Forms.Button();
			this.btnFechar = new System.Windows.Forms.Button();
			this.linkNenhum_StatusAuditoria = new System.Windows.Forms.LinkLabel();
			this.linkTodos_StatusAuditoria = new System.Windows.Forms.LinkLabel();
			this.linkNenhum_Campanha = new System.Windows.Forms.LinkLabel();
			this.linkTodos_Campanha = new System.Windows.Forms.LinkLabel();
			this.cmbOperador = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.cmbSupervisor = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtTelefone = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
			this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.chkAuditoria = new System.Windows.Forms.CheckedListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.chkCampanha = new System.Windows.Forms.CheckedListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.dgResultado = new System.Windows.Forms.DataGridView();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotalRegistros,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 604);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1008, 24);
			this.statusStrip1.TabIndex = 17;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lblTotalRegistros
			// 
			this.lblTotalRegistros.BackColor = System.Drawing.Color.Transparent;
			this.lblTotalRegistros.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.lblTotalRegistros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.lblTotalRegistros.Name = "lblTotalRegistros";
			this.lblTotalRegistros.Size = new System.Drawing.Size(81, 19);
			this.lblTotalRegistros.Text = "0 Registro(s)";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Transparent;
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
			this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Blue;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(378, 19);
			this.toolStripStatusLabel2.Text = "Para ver os detalhes do registro, dê um clique duplo na linha desejada";
			// 
			// txtBuscaRapida
			// 
			this.txtBuscaRapida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBuscaRapida.Location = new System.Drawing.Point(910, 6);
			this.txtBuscaRapida.MaxLength = 10;
			this.txtBuscaRapida.Name = "txtBuscaRapida";
			this.txtBuscaRapida.Size = new System.Drawing.Size(55, 20);
			this.txtBuscaRapida.TabIndex = 26;
			this.txtBuscaRapida.Visible = false;
			this.txtBuscaRapida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscaRapida_KeyPress);
			// 
			// btnBuscaRapida
			// 
			this.btnBuscaRapida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBuscaRapida.BackColor = System.Drawing.SystemColors.Control;
			this.btnBuscaRapida.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnBuscaRapida.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
			this.btnBuscaRapida.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnBuscaRapida.Location = new System.Drawing.Point(971, 5);
			this.btnBuscaRapida.Name = "btnBuscaRapida";
			this.btnBuscaRapida.Size = new System.Drawing.Size(25, 22);
			this.btnBuscaRapida.TabIndex = 27;
			this.btnBuscaRapida.UseVisualStyleBackColor = true;
			this.btnBuscaRapida.Visible = false;
			this.btnBuscaRapida.Click += new System.EventHandler(this.btnBuscaRapida_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(834, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 13);
			this.label3.TabIndex = 25;
			this.label3.Text = "Busca Rápida:";
			this.label3.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(7, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(256, 25);
			this.label2.TabIndex = 0;
			this.label2.Text = "AUDITORIA DE ACORDO";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.cmbAuditoria);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.txtNome);
			this.groupBox1.Controls.Add(this.txtCpf);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.btnExportar);
			this.groupBox1.Controls.Add(this.btnGerarDados);
			this.groupBox1.Controls.Add(this.btnFechar);
			this.groupBox1.Controls.Add(this.linkNenhum_StatusAuditoria);
			this.groupBox1.Controls.Add(this.linkTodos_StatusAuditoria);
			this.groupBox1.Controls.Add(this.linkNenhum_Campanha);
			this.groupBox1.Controls.Add(this.linkTodos_Campanha);
			this.groupBox1.Controls.Add(this.cmbOperador);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.cmbSupervisor);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.txtTelefone);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.dtpDataFinal);
			this.groupBox1.Controls.Add(this.dtpDataInicial);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.chkAuditoria);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.chkCampanha);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 33);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(984, 180);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Filtro";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.ForeColor = System.Drawing.Color.Black;
			this.label10.Location = new System.Drawing.Point(7, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(30, 13);
			this.label10.TabIndex = 27;
			this.label10.Text = "Data";
			// 
			// cmbAuditoria
			// 
			this.cmbAuditoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAuditoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbAuditoria.FormattingEnabled = true;
			this.cmbAuditoria.Items.AddRange(new object[] {
            "DATA DO ACORDO",
            "DATA DA AUDITORIA"});
			this.cmbAuditoria.Location = new System.Drawing.Point(10, 31);
			this.cmbAuditoria.Name = "cmbAuditoria";
			this.cmbAuditoria.Size = new System.Drawing.Size(134, 21);
			this.cmbAuditoria.TabIndex = 28;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.BackColor = System.Drawing.Color.Transparent;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.ForeColor = System.Drawing.Color.Black;
			this.label14.Location = new System.Drawing.Point(7, 132);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(38, 13);
			this.label14.TabIndex = 25;
			this.label14.Text = "Nome:";
			// 
			// txtNome
			// 
			this.txtNome.BackColor = System.Drawing.Color.White;
			this.txtNome.Location = new System.Drawing.Point(10, 148);
			this.txtNome.MaxLength = 100;
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(372, 20);
			this.txtNome.TabIndex = 26;
			// 
			// txtCpf
			// 
			this.txtCpf.BackColor = System.Drawing.Color.White;
			this.txtCpf.Location = new System.Drawing.Point(269, 73);
			this.txtCpf.MaxLength = 11;
			this.txtCpf.Name = "txtCpf";
			this.txtCpf.Size = new System.Drawing.Size(113, 20);
			this.txtCpf.TabIndex = 7;
			this.txtCpf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCpf_KeyPress);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Black;
			this.label7.Location = new System.Drawing.Point(266, 57);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(27, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = "CPF";
			// 
			// btnExportar
			// 
			this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportar.BackColor = System.Drawing.SystemColors.Control;
			this.btnExportar.Enabled = false;
			this.btnExportar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnExportar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.import;
			this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnExportar.Location = new System.Drawing.Point(882, 58);
			this.btnExportar.Name = "btnExportar";
			this.btnExportar.Size = new System.Drawing.Size(96, 23);
			this.btnExportar.TabIndex = 21;
			this.btnExportar.Text = "Exportar";
			this.btnExportar.UseVisualStyleBackColor = true;
			this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
			// 
			// btnGerarDados
			// 
			this.btnGerarDados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGerarDados.BackColor = System.Drawing.SystemColors.Control;
			this.btnGerarDados.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnGerarDados.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
			this.btnGerarDados.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnGerarDados.Location = new System.Drawing.Point(882, 29);
			this.btnGerarDados.Name = "btnGerarDados";
			this.btnGerarDados.Size = new System.Drawing.Size(96, 23);
			this.btnGerarDados.TabIndex = 20;
			this.btnGerarDados.Text = "Pesquisar";
			this.btnGerarDados.UseVisualStyleBackColor = true;
			this.btnGerarDados.Click += new System.EventHandler(this.btnPesquisar_Click);
			// 
			// btnFechar
			// 
			this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFechar.BackColor = System.Drawing.SystemColors.Control;
			this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnFechar.FlatAppearance.BorderSize = 0;
			this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
			this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFechar.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFechar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.close;
			this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFechar.Location = new System.Drawing.Point(882, 87);
			this.btnFechar.Name = "btnFechar";
			this.btnFechar.Size = new System.Drawing.Size(96, 25);
			this.btnFechar.TabIndex = 22;
			this.btnFechar.Text = "Fechar   ";
			this.btnFechar.UseVisualStyleBackColor = true;
			this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
			// 
			// linkNenhum_StatusAuditoria
			// 
			this.linkNenhum_StatusAuditoria.AutoSize = true;
			this.linkNenhum_StatusAuditoria.Location = new System.Drawing.Point(859, 13);
			this.linkNenhum_StatusAuditoria.Name = "linkNenhum_StatusAuditoria";
			this.linkNenhum_StatusAuditoria.Size = new System.Drawing.Size(47, 13);
			this.linkNenhum_StatusAuditoria.TabIndex = 18;
			this.linkNenhum_StatusAuditoria.TabStop = true;
			this.linkNenhum_StatusAuditoria.Text = "Nenhum";
			this.linkNenhum_StatusAuditoria.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNenhum_StatusAuditoria_LinkClicked);
			// 
			// linkTodos_StatusAuditoria
			// 
			this.linkTodos_StatusAuditoria.AutoSize = true;
			this.linkTodos_StatusAuditoria.Location = new System.Drawing.Point(816, 13);
			this.linkTodos_StatusAuditoria.Name = "linkTodos_StatusAuditoria";
			this.linkTodos_StatusAuditoria.Size = new System.Drawing.Size(37, 13);
			this.linkTodos_StatusAuditoria.TabIndex = 17;
			this.linkTodos_StatusAuditoria.TabStop = true;
			this.linkTodos_StatusAuditoria.Text = "Todos";
			this.linkTodos_StatusAuditoria.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTodos_StatusAuditoria_LinkClicked);
			// 
			// linkNenhum_Campanha
			// 
			this.linkNenhum_Campanha.AutoSize = true;
			this.linkNenhum_Campanha.Location = new System.Drawing.Point(511, 13);
			this.linkNenhum_Campanha.Name = "linkNenhum_Campanha";
			this.linkNenhum_Campanha.Size = new System.Drawing.Size(47, 13);
			this.linkNenhum_Campanha.TabIndex = 14;
			this.linkNenhum_Campanha.TabStop = true;
			this.linkNenhum_Campanha.Text = "Nenhum";
			this.linkNenhum_Campanha.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNenhum_Campanha_LinkClicked);
			// 
			// linkTodos_Campanha
			// 
			this.linkTodos_Campanha.AutoSize = true;
			this.linkTodos_Campanha.Location = new System.Drawing.Point(468, 13);
			this.linkTodos_Campanha.Name = "linkTodos_Campanha";
			this.linkTodos_Campanha.Size = new System.Drawing.Size(37, 13);
			this.linkTodos_Campanha.TabIndex = 13;
			this.linkTodos_Campanha.TabStop = true;
			this.linkTodos_Campanha.Text = "Todos";
			this.linkTodos_Campanha.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTodos_Campanha_LinkClicked);
			// 
			// cmbOperador
			// 
			this.cmbOperador.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.cmbOperador.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbOperador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbOperador.FormattingEnabled = true;
			this.cmbOperador.Location = new System.Drawing.Point(10, 111);
			this.cmbOperador.Name = "cmbOperador";
			this.cmbOperador.Size = new System.Drawing.Size(253, 21);
			this.cmbOperador.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.Black;
			this.label9.Location = new System.Drawing.Point(7, 96);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(51, 13);
			this.label9.TabIndex = 10;
			this.label9.Text = "Operador";
			// 
			// cmbSupervisor
			// 
			this.cmbSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.cmbSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbSupervisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSupervisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbSupervisor.FormattingEnabled = true;
			this.cmbSupervisor.Location = new System.Drawing.Point(10, 73);
			this.cmbSupervisor.Name = "cmbSupervisor";
			this.cmbSupervisor.Size = new System.Drawing.Size(252, 21);
			this.cmbSupervisor.TabIndex = 9;
			this.cmbSupervisor.SelectionChangeCommitted += new System.EventHandler(this.cmbSupervisor_SelectionChangeCommitted);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.Black;
			this.label8.Location = new System.Drawing.Point(7, 57);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(57, 13);
			this.label8.TabIndex = 8;
			this.label8.Text = "Supervisor";
			// 
			// txtTelefone
			// 
			this.txtTelefone.BackColor = System.Drawing.Color.White;
			this.txtTelefone.Location = new System.Drawing.Point(269, 111);
			this.txtTelefone.MaxLength = 11;
			this.txtTelefone.Name = "txtTelefone";
			this.txtTelefone.Size = new System.Drawing.Size(113, 20);
			this.txtTelefone.TabIndex = 5;
			this.txtTelefone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefone_KeyPress);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.Black;
			this.label12.Location = new System.Drawing.Point(266, 95);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(49, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Telefone";
			// 
			// dtpDataFinal
			// 
			this.dtpDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpDataFinal.Location = new System.Drawing.Point(269, 31);
			this.dtpDataFinal.Name = "dtpDataFinal";
			this.dtpDataFinal.Size = new System.Drawing.Size(113, 20);
			this.dtpDataFinal.TabIndex = 3;
			// 
			// dtpDataInicial
			// 
			this.dtpDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpDataInicial.Location = new System.Drawing.Point(150, 31);
			this.dtpDataInicial.Name = "dtpDataInicial";
			this.dtpDataInicial.Size = new System.Drawing.Size(113, 20);
			this.dtpDataInicial.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Location = new System.Drawing.Point(266, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Data Final";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Black;
			this.label6.Location = new System.Drawing.Point(147, 15);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Data Inicial";
			// 
			// chkAuditoria
			// 
			this.chkAuditoria.CheckOnClick = true;
			this.chkAuditoria.FormattingEnabled = true;
			this.chkAuditoria.Location = new System.Drawing.Point(732, 29);
			this.chkAuditoria.Name = "chkAuditoria";
			this.chkAuditoria.Size = new System.Drawing.Size(410, 139);
			this.chkAuditoria.TabIndex = 19;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(729, 13);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Status Auditoria";
			// 
			// chkCampanha
			// 
			this.chkCampanha.CheckOnClick = true;
			this.chkCampanha.FormattingEnabled = true;
			this.chkCampanha.Location = new System.Drawing.Point(407, 29);
			this.chkCampanha.Name = "chkCampanha";
			this.chkCampanha.Size = new System.Drawing.Size(319, 139);
			this.chkCampanha.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(404, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Campanha";
			// 
			// dgResultado
			// 
			this.dgResultado.AllowUserToAddRows = false;
			this.dgResultado.AllowUserToDeleteRows = false;
			this.dgResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgResultado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgResultado.EnableHeadersVisualStyles = false;
			this.dgResultado.Location = new System.Drawing.Point(12, 225);
			this.dgResultado.MultiSelect = false;
			this.dgResultado.Name = "dgResultado";
			this.dgResultado.ReadOnly = true;
			this.dgResultado.RowHeadersVisible = false;
			this.dgResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgResultado.Size = new System.Drawing.Size(984, 365);
			this.dgResultado.TabIndex = 2;
			this.dgResultado.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgResultado_CellDoubleClick);
			// 
			// ListaAuditoriaDeVendaForm
			// 
			this.AcceptButton = this.btnGerarDados;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1008, 628);
			this.Controls.Add(this.txtBuscaRapida);
			this.Controls.Add(this.btnBuscaRapida);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dgResultado);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ListaAuditoriaDeVendaForm";
			this.Text = "Auditoria de Vendas";
			this.Load += new System.EventHandler(this.ListaAuditoriaDeVendaForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgResultado)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalRegistros;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox txtBuscaRapida;
        private System.Windows.Forms.Button btnBuscaRapida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.DataGridView dgResultado;
        private System.Windows.Forms.ComboBox cmbOperador;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbSupervisor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox chkAuditoria;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox chkCampanha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGerarDados;
        private System.Windows.Forms.LinkLabel linkNenhum_Campanha;
        private System.Windows.Forms.LinkLabel linkTodos_Campanha;
        private System.Windows.Forms.LinkLabel linkNenhum_StatusAuditoria;
        private System.Windows.Forms.LinkLabel linkTodos_StatusAuditoria;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.TextBox txtCpf;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbAuditoria;
    }
}