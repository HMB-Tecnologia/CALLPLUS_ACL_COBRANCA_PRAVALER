namespace Callplus.CRM.Administracao.App.Relatorios
{
    partial class ContatosTrabalhadosDetalhado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContatosTrabalhadosDetalhado));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTotalRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtBuscaRapida = new System.Windows.Forms.TextBox();
            this.btnBuscaRapida = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkListarStatusOfertaNaoInformado = new System.Windows.Forms.CheckBox();
            this.chkListarStatusAtendimentoNaoInformado = new System.Windows.Forms.CheckBox();
            this.linkTodos_StatusNenhum = new System.Windows.Forms.LinkLabel();
            this.cmbCampanhas = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkListarAtivos = new System.Windows.Forms.CheckBox();
            this.cmbMailing = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbHoraFinal = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbHoraInicial = new System.Windows.Forms.ComboBox();
            this.linkTodos_StatusAtendimento = new System.Windows.Forms.LinkLabel();
            this.chkStatusDeAtendimento = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnGerarDados = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.linkNenhum_StatusAuditoria = new System.Windows.Forms.LinkLabel();
            this.linkTodos_StatusAuditoria = new System.Windows.Forms.LinkLabel();
            this.cmbOperador = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbSupervisor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkStatusOferta = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgResultado = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotalRegistros});
            this.statusStrip1.Location = new System.Drawing.Point(0, 604);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1370, 24);
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
            // txtBuscaRapida
            // 
            this.txtBuscaRapida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscaRapida.Location = new System.Drawing.Point(1272, 6);
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
            this.btnBuscaRapida.Location = new System.Drawing.Point(1333, 5);
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
            this.label3.Location = new System.Drawing.Point(1196, 10);
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
            this.label2.Size = new System.Drawing.Size(290, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "CONTATOS TRABALHADOS";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkListarStatusOfertaNaoInformado);
            this.groupBox1.Controls.Add(this.chkListarStatusAtendimentoNaoInformado);
            this.groupBox1.Controls.Add(this.linkTodos_StatusNenhum);
            this.groupBox1.Controls.Add(this.cmbCampanhas);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.chkListarAtivos);
            this.groupBox1.Controls.Add(this.cmbMailing);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbHoraFinal);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbHoraInicial);
            this.groupBox1.Controls.Add(this.linkTodos_StatusAtendimento);
            this.groupBox1.Controls.Add(this.chkStatusDeAtendimento);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnExportar);
            this.groupBox1.Controls.Add(this.btnGerarDados);
            this.groupBox1.Controls.Add(this.btnFechar);
            this.groupBox1.Controls.Add(this.linkNenhum_StatusAuditoria);
            this.groupBox1.Controls.Add(this.linkTodos_StatusAuditoria);
            this.groupBox1.Controls.Add(this.cmbOperador);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbSupervisor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dtpDataFinal);
            this.groupBox1.Controls.Add(this.dtpDataInicial);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkStatusOferta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1346, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // chkListarStatusOfertaNaoInformado
            // 
            this.chkListarStatusOfertaNaoInformado.AutoSize = true;
            this.chkListarStatusOfertaNaoInformado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListarStatusOfertaNaoInformado.Location = new System.Drawing.Point(863, 12);
            this.chkListarStatusOfertaNaoInformado.Name = "chkListarStatusOfertaNaoInformado";
            this.chkListarStatusOfertaNaoInformado.Size = new System.Drawing.Size(100, 17);
            this.chkListarStatusOfertaNaoInformado.TabIndex = 162;
            this.chkListarStatusOfertaNaoInformado.Text = "Não informados";
            this.chkListarStatusOfertaNaoInformado.UseVisualStyleBackColor = true;
            // 
            // chkListarStatusAtendimentoNaoInformado
            // 
            this.chkListarStatusAtendimentoNaoInformado.AutoSize = true;
            this.chkListarStatusAtendimentoNaoInformado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListarStatusAtendimentoNaoInformado.Location = new System.Drawing.Point(559, 12);
            this.chkListarStatusAtendimentoNaoInformado.Name = "chkListarStatusAtendimentoNaoInformado";
            this.chkListarStatusAtendimentoNaoInformado.Size = new System.Drawing.Size(100, 17);
            this.chkListarStatusAtendimentoNaoInformado.TabIndex = 161;
            this.chkListarStatusAtendimentoNaoInformado.Text = "Não informados";
            this.chkListarStatusAtendimentoNaoInformado.UseVisualStyleBackColor = true;
            // 
            // linkTodos_StatusNenhum
            // 
            this.linkTodos_StatusNenhum.AutoSize = true;
            this.linkTodos_StatusNenhum.Location = new System.Drawing.Point(720, 14);
            this.linkTodos_StatusNenhum.Name = "linkTodos_StatusNenhum";
            this.linkTodos_StatusNenhum.Size = new System.Drawing.Size(47, 13);
            this.linkTodos_StatusNenhum.TabIndex = 160;
            this.linkTodos_StatusNenhum.TabStop = true;
            this.linkTodos_StatusNenhum.Text = "Nenhum";
            this.linkTodos_StatusNenhum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTodos_StatusNenhum_LinkClicked);
            // 
            // cmbCampanhas
            // 
            this.cmbCampanhas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCampanhas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCampanhas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCampanhas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCampanhas.FormattingEnabled = true;
            this.cmbCampanhas.Location = new System.Drawing.Point(6, 112);
            this.cmbCampanhas.Name = "cmbCampanhas";
            this.cmbCampanhas.Size = new System.Drawing.Size(212, 21);
            this.cmbCampanhas.TabIndex = 159;
            this.cmbCampanhas.SelectedValueChanged += new System.EventHandler(this.cmbCampanhas_SelectedValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(6, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 158;
            this.label13.Text = "Campanha";
            // 
            // chkListarAtivos
            // 
            this.chkListarAtivos.AutoSize = true;
            this.chkListarAtivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListarAtivos.Location = new System.Drawing.Point(55, 139);
            this.chkListarAtivos.Name = "chkListarAtivos";
            this.chkListarAtivos.Size = new System.Drawing.Size(130, 17);
            this.chkListarAtivos.TabIndex = 157;
            this.chkListarAtivos.Text = "Listar ativos e inativos";
            this.chkListarAtivos.UseVisualStyleBackColor = true;
            this.chkListarAtivos.CheckedChanged += new System.EventHandler(this.chkListarAtivos_CheckedChanged);
            // 
            // cmbMailing
            // 
            this.cmbMailing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMailing.FormattingEnabled = true;
            this.cmbMailing.Location = new System.Drawing.Point(6, 156);
            this.cmbMailing.Name = "cmbMailing";
            this.cmbMailing.Size = new System.Drawing.Size(212, 21);
            this.cmbMailing.TabIndex = 156;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 155;
            this.label12.Text = "Mailing";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 154;
            this.label7.Text = "Hora final";
            // 
            // cmbHoraFinal
            // 
            this.cmbHoraFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoraFinal.FormattingEnabled = true;
            this.cmbHoraFinal.Items.AddRange(new object[] {
            "",
            "08:00",
            "08:30",
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:00",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
            "17:00",
            "17:30",
            "18:00",
            "18:30",
            "19:00",
            "19:30",
            "20:00",
            "20:30",
            "21:00",
            "21:30",
            "22:00"});
            this.cmbHoraFinal.Location = new System.Drawing.Point(113, 72);
            this.cmbHoraFinal.Name = "cmbHoraFinal";
            this.cmbHoraFinal.Size = new System.Drawing.Size(105, 21);
            this.cmbHoraFinal.TabIndex = 153;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(110, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 152;
            this.label11.Text = "Hora inicial";
            // 
            // cmbHoraInicial
            // 
            this.cmbHoraInicial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoraInicial.FormattingEnabled = true;
            this.cmbHoraInicial.Items.AddRange(new object[] {
            "",
            "08:00",
            "08:30",
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:00",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
            "17:00",
            "17:30",
            "18:00",
            "18:30",
            "19:00",
            "19:30",
            "20:00",
            "20:30",
            "21:00",
            "21:30",
            "22:00"});
            this.cmbHoraInicial.Location = new System.Drawing.Point(113, 32);
            this.cmbHoraInicial.Name = "cmbHoraInicial";
            this.cmbHoraInicial.Size = new System.Drawing.Size(105, 21);
            this.cmbHoraInicial.TabIndex = 151;
            // 
            // linkTodos_StatusAtendimento
            // 
            this.linkTodos_StatusAtendimento.AutoSize = true;
            this.linkTodos_StatusAtendimento.Location = new System.Drawing.Point(677, 14);
            this.linkTodos_StatusAtendimento.Name = "linkTodos_StatusAtendimento";
            this.linkTodos_StatusAtendimento.Size = new System.Drawing.Size(37, 13);
            this.linkTodos_StatusAtendimento.TabIndex = 24;
            this.linkTodos_StatusAtendimento.TabStop = true;
            this.linkTodos_StatusAtendimento.Text = "Todos";
            this.linkTodos_StatusAtendimento.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTodos_StatusAtendimento_LinkClicked);
            // 
            // chkStatusDeAtendimento
            // 
            this.chkStatusDeAtendimento.CheckOnClick = true;
            this.chkStatusDeAtendimento.FormattingEnabled = true;
            this.chkStatusDeAtendimento.Location = new System.Drawing.Point(442, 31);
            this.chkStatusDeAtendimento.Name = "chkStatusDeAtendimento";
            this.chkStatusDeAtendimento.Size = new System.Drawing.Size(328, 154);
            this.chkStatusDeAtendimento.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(439, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Status de Atendimento";
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportar.Enabled = false;
            this.btnExportar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.import;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.Location = new System.Drawing.Point(1244, 52);
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
            this.btnGerarDados.Location = new System.Drawing.Point(1244, 23);
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
            this.btnFechar.Location = new System.Drawing.Point(1244, 81);
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
            this.linkNenhum_StatusAuditoria.Location = new System.Drawing.Point(1069, 14);
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
            this.linkTodos_StatusAuditoria.Location = new System.Drawing.Point(1026, 14);
            this.linkTodos_StatusAuditoria.Name = "linkTodos_StatusAuditoria";
            this.linkTodos_StatusAuditoria.Size = new System.Drawing.Size(37, 13);
            this.linkTodos_StatusAuditoria.TabIndex = 17;
            this.linkTodos_StatusAuditoria.TabStop = true;
            this.linkTodos_StatusAuditoria.Text = "Todos";
            this.linkTodos_StatusAuditoria.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTodos_StatusAuditoria_LinkClicked);
            // 
            // cmbOperador
            // 
            this.cmbOperador.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbOperador.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOperador.FormattingEnabled = true;
            this.cmbOperador.Location = new System.Drawing.Point(224, 72);
            this.cmbOperador.Name = "cmbOperador";
            this.cmbOperador.Size = new System.Drawing.Size(212, 21);
            this.cmbOperador.TabIndex = 11;
            this.cmbOperador.SelectedIndexChanged += new System.EventHandler(this.cmbOperador_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(221, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Operador";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // cmbSupervisor
            // 
            this.cmbSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupervisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupervisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupervisor.FormattingEnabled = true;
            this.cmbSupervisor.Location = new System.Drawing.Point(224, 32);
            this.cmbSupervisor.Name = "cmbSupervisor";
            this.cmbSupervisor.Size = new System.Drawing.Size(212, 21);
            this.cmbSupervisor.TabIndex = 9;
            this.cmbSupervisor.SelectionChangeCommitted += new System.EventHandler(this.cmbSupervisor_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(221, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Supervisor";
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(6, 73);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(101, 20);
            this.dtpDataFinal.TabIndex = 3;
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(6, 32);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(101, 20);
            this.dtpDataInicial.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 55);
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
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Data Inicial";
            // 
            // chkStatusOferta
            // 
            this.chkStatusOferta.CheckOnClick = true;
            this.chkStatusOferta.FormattingEnabled = true;
            this.chkStatusOferta.Location = new System.Drawing.Point(776, 31);
            this.chkStatusOferta.Name = "chkStatusOferta";
            this.chkStatusOferta.Size = new System.Drawing.Size(340, 154);
            this.chkStatusOferta.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(773, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Status da Oferta";
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
            this.dgResultado.Location = new System.Drawing.Point(12, 233);
            this.dgResultado.MultiSelect = false;
            this.dgResultado.Name = "dgResultado";
            this.dgResultado.ReadOnly = true;
            this.dgResultado.RowHeadersVisible = false;
            this.dgResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResultado.Size = new System.Drawing.Size(1346, 357);
            this.dgResultado.TabIndex = 2;
            // 
            // ContatosTrabalhadosDetalhado
            // 
            this.AcceptButton = this.btnGerarDados;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 628);
            this.Controls.Add(this.txtBuscaRapida);
            this.Controls.Add(this.btnBuscaRapida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgResultado);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContatosTrabalhadosDetalhado";
            this.Text = "Contatos Trabalbalhados";
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
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox chkStatusOferta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGerarDados;
        private System.Windows.Forms.LinkLabel linkNenhum_StatusAuditoria;
        private System.Windows.Forms.LinkLabel linkTodos_StatusAuditoria;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbHoraFinal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbHoraInicial;
        private System.Windows.Forms.ComboBox cmbMailing;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel linkTodos_StatusNenhum;
        private System.Windows.Forms.ComboBox cmbCampanhas;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkListarAtivos;
        private System.Windows.Forms.LinkLabel linkTodos_StatusAtendimento;
        private System.Windows.Forms.CheckedListBox chkStatusDeAtendimento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkListarStatusOfertaNaoInformado;
        private System.Windows.Forms.CheckBox chkListarStatusAtendimentoNaoInformado;
    }
}