﻿namespace Callplus.CRM.Administracao.App.Administracao.Usuario
{
    partial class ListaUsuarioForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaUsuarioForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chkListarAtivos = new System.Windows.Forms.CheckBox();
            this.txtBuscaRapida = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbPerfil = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSupervisor = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnNovo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.cmbCampanha = new System.Windows.Forms.ComboBox();
            this.btnFechar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.dgResultado = new System.Windows.Forms.DataGridView();
            this.btnBuscaRapida = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTotalRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnEditarUsuarios = new System.Windows.Forms.Button();
            this.lnkMarcarTodos = new System.Windows.Forms.LinkLabel();
            this.lnkDesmarcarTodos = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkListarAtivos
            // 
            this.chkListarAtivos.AutoSize = true;
            this.chkListarAtivos.Checked = true;
            this.chkListarAtivos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkListarAtivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListarAtivos.Location = new System.Drawing.Point(12, 103);
            this.chkListarAtivos.Name = "chkListarAtivos";
            this.chkListarAtivos.Size = new System.Drawing.Size(181, 17);
            this.chkListarAtivos.TabIndex = 13;
            this.chkListarAtivos.Text = "Exibir somente os registros ativos";
            this.chkListarAtivos.UseVisualStyleBackColor = true;
            this.chkListarAtivos.CheckedChanged += new System.EventHandler(this.chkListarAtivos_CheckedChanged);
            // 
            // txtBuscaRapida
            // 
            this.txtBuscaRapida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscaRapida.Location = new System.Drawing.Point(930, 6);
            this.txtBuscaRapida.MaxLength = 10;
            this.txtBuscaRapida.Name = "txtBuscaRapida";
            this.txtBuscaRapida.Size = new System.Drawing.Size(55, 20);
            this.txtBuscaRapida.TabIndex = 10;
            this.txtBuscaRapida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscaRapida_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(854, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Busca Rápida:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "USUÁRIOS";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbPerfil);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbSupervisor);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnNovo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.cmbCampanha);
            this.groupBox1.Controls.Add(this.btnFechar);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1004, 64);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ações";
            // 
            // cmbPerfil
            // 
            this.cmbPerfil.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbPerfil.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPerfil.FormattingEnabled = true;
            this.cmbPerfil.Location = new System.Drawing.Point(224, 32);
            this.cmbPerfil.Name = "cmbPerfil";
            this.cmbPerfil.Size = new System.Drawing.Size(100, 21);
            this.cmbPerfil.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Perfil";
            // 
            // cmbSupervisor
            // 
            this.cmbSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupervisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupervisor.FormattingEnabled = true;
            this.cmbSupervisor.Location = new System.Drawing.Point(330, 32);
            this.cmbSupervisor.Name = "cmbSupervisor";
            this.cmbSupervisor.Size = new System.Drawing.Size(207, 21);
            this.cmbSupervisor.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(662, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Login";
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.BackColor = System.Drawing.SystemColors.Control;
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.FlatAppearance.BorderSize = 0;
            this.btnNovo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNovo.Location = new System.Drawing.Point(833, 29);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(80, 25);
            this.btnNovo.TabIndex = 11;
            this.btnNovo.Text = "Novo ";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(327, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Supervisor";
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.SystemColors.Control;
            this.btnPesquisar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPesquisar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisar.Location = new System.Drawing.Point(764, 30);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisar.TabIndex = 10;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(665, 32);
            this.txtLogin.MaxLength = 30;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(93, 20);
            this.txtLogin.TabIndex = 9;
            // 
            // cmbCampanha
            // 
            this.cmbCampanha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCampanha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCampanha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCampanha.FormattingEnabled = true;
            this.cmbCampanha.Location = new System.Drawing.Point(14, 32);
            this.cmbCampanha.Name = "cmbCampanha";
            this.cmbCampanha.Size = new System.Drawing.Size(204, 21);
            this.cmbCampanha.TabIndex = 1;
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
            this.btnFechar.Location = new System.Drawing.Point(918, 29);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(80, 25);
            this.btnFechar.TabIndex = 12;
            this.btnFechar.Text = "Fechar   ";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Campanha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(540, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(543, 32);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(116, 20);
            this.txtNome.TabIndex = 7;
            // 
            // dgResultado
            // 
            this.dgResultado.AllowUserToAddRows = false;
            this.dgResultado.AllowUserToDeleteRows = false;
            this.dgResultado.AllowUserToResizeRows = false;
            this.dgResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgResultado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dgResultado.Location = new System.Drawing.Point(12, 126);
            this.dgResultado.MultiSelect = false;
            this.dgResultado.Name = "dgResultado";
            this.dgResultado.RowHeadersVisible = false;
            this.dgResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResultado.Size = new System.Drawing.Size(1004, 317);
            this.dgResultado.TabIndex = 14;
            this.dgResultado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgResultado_CellContentClick);
            this.dgResultado.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgResultado_CellDoubleClick);
            // 
            // btnBuscaRapida
            // 
            this.btnBuscaRapida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscaRapida.BackColor = System.Drawing.SystemColors.Control;
            this.btnBuscaRapida.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBuscaRapida.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
            this.btnBuscaRapida.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscaRapida.Location = new System.Drawing.Point(991, 5);
            this.btnBuscaRapida.Name = "btnBuscaRapida";
            this.btnBuscaRapida.Size = new System.Drawing.Size(25, 22);
            this.btnBuscaRapida.TabIndex = 11;
            this.btnBuscaRapida.UseVisualStyleBackColor = true;
            this.btnBuscaRapida.Click += new System.EventHandler(this.btnBuscaRapida_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotalRegistros,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 454);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 24);
            this.statusStrip1.TabIndex = 15;
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
            // btnEditarUsuarios
            // 
            this.btnEditarUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditarUsuarios.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditarUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarUsuarios.Enabled = false;
            this.btnEditarUsuarios.FlatAppearance.BorderSize = 0;
            this.btnEditarUsuarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnEditarUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarUsuarios.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEditarUsuarios.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
            this.btnEditarUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditarUsuarios.Location = new System.Drawing.Point(912, 99);
            this.btnEditarUsuarios.Name = "btnEditarUsuarios";
            this.btnEditarUsuarios.Size = new System.Drawing.Size(104, 25);
            this.btnEditarUsuarios.TabIndex = 13;
            this.btnEditarUsuarios.Text = "Editar Usuários";
            this.btnEditarUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarUsuarios.UseVisualStyleBackColor = true;
            this.btnEditarUsuarios.Click += new System.EventHandler(this.btnEditarUsuarios_Click);
            // 
            // lnkMarcarTodos
            // 
            this.lnkMarcarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkMarcarTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkMarcarTodos.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkMarcarTodos.Location = new System.Drawing.Point(745, 105);
            this.lnkMarcarTodos.Name = "lnkMarcarTodos";
            this.lnkMarcarTodos.Size = new System.Drawing.Size(69, 13);
            this.lnkMarcarTodos.TabIndex = 16;
            this.lnkMarcarTodos.TabStop = true;
            this.lnkMarcarTodos.Text = "Marcar todos";
            this.lnkMarcarTodos.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkMarcarTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMarcarTodos_LinkClicked);
            // 
            // lnkDesmarcarTodos
            // 
            this.lnkDesmarcarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDesmarcarTodos.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkDesmarcarTodos.Location = new System.Drawing.Point(819, 105);
            this.lnkDesmarcarTodos.Name = "lnkDesmarcarTodos";
            this.lnkDesmarcarTodos.Size = new System.Drawing.Size(87, 13);
            this.lnkDesmarcarTodos.TabIndex = 17;
            this.lnkDesmarcarTodos.TabStop = true;
            this.lnkDesmarcarTodos.Text = "Desmarcar todos";
            this.lnkDesmarcarTodos.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkDesmarcarTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDesmarcarTodos_LinkClicked);
            // 
            // ListaUsuarioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 478);
            this.Controls.Add(this.lnkDesmarcarTodos);
            this.Controls.Add(this.lnkMarcarTodos);
            this.Controls.Add(this.btnEditarUsuarios);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkListarAtivos);
            this.Controls.Add(this.txtBuscaRapida);
            this.Controls.Add(this.btnBuscaRapida);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgResultado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListaUsuarioForm";
            this.Text = "Usuários";
            this.Load += new System.EventHandler(this.fListaUsuario_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkListarAtivos;
        private System.Windows.Forms.TextBox txtBuscaRapida;
        private System.Windows.Forms.Button btnBuscaRapida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbPerfil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSupervisor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.DataGridView dgResultado;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalRegistros;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button btnEditarUsuarios;
        private System.Windows.Forms.LinkLabel lnkMarcarTodos;
        private System.Windows.Forms.LinkLabel lnkDesmarcarTodos;
    }
}