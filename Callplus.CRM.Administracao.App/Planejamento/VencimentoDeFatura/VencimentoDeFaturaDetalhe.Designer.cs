namespace Callplus.CRM.Administracao.App.Planejamento.VencimentoDeFatura
{
    partial class VencimentoDeFaturaDetalhe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VencimentoDeFaturaDetalhe));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDiaDeAtivacao = new System.Windows.Forms.ComboBox();
            this.cmbOrdem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkCicloAtivo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tsEtapa = new System.Windows.Forms.ToolStrip();
            this.tsEtapa_btnNovo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEtapa_btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEtapa_btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsEtapa_btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbVencimento = new System.Windows.Forms.ComboBox();
            this.cmbFechamento = new System.Windows.Forms.ComboBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.cmbCicloDeVencimento = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkVencimentoAtivo = new System.Windows.Forms.CheckBox();
            this.dgResultado = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.tsEtapa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(7, 5);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(276, 25);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "VENCIMENTO DE FATURA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 382);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Dia de Ativação";
            // 
            // cmbDiaDeAtivacao
            // 
            this.cmbDiaDeAtivacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbDiaDeAtivacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDiaDeAtivacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiaDeAtivacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDiaDeAtivacao.FormattingEnabled = true;
            this.cmbDiaDeAtivacao.Items.AddRange(new object[] {
            "SELECIONE...",
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
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cmbDiaDeAtivacao.Location = new System.Drawing.Point(12, 398);
            this.cmbDiaDeAtivacao.Name = "cmbDiaDeAtivacao";
            this.cmbDiaDeAtivacao.Size = new System.Drawing.Size(116, 21);
            this.cmbDiaDeAtivacao.TabIndex = 1;
            // 
            // cmbOrdem
            // 
            this.cmbOrdem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbOrdem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOrdem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrdem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOrdem.FormattingEnabled = true;
            this.cmbOrdem.Items.AddRange(new object[] {
            "SELECIONE...",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbOrdem.Location = new System.Drawing.Point(134, 398);
            this.cmbOrdem.Name = "cmbOrdem";
            this.cmbOrdem.Size = new System.Drawing.Size(116, 21);
            this.cmbOrdem.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ordem";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Ciclo de Vencimento";
            // 
            // chkCicloAtivo
            // 
            this.chkCicloAtivo.AutoSize = true;
            this.chkCicloAtivo.Enabled = false;
            this.chkCicloAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCicloAtivo.Location = new System.Drawing.Point(251, 52);
            this.chkCicloAtivo.Name = "chkCicloAtivo";
            this.chkCicloAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkCicloAtivo.TabIndex = 3;
            this.chkCicloAtivo.Text = "Ativo";
            this.chkCicloAtivo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(126, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Vencimento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Fechamento";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tsEtapa);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.chkCicloAtivo);
            this.panel2.Controls.Add(this.cmbVencimento);
            this.panel2.Controls.Add(this.cmbFechamento);
            this.panel2.Location = new System.Drawing.Point(12, 283);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(401, 84);
            this.panel2.TabIndex = 0;
            // 
            // tsEtapa
            // 
            this.tsEtapa.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tsEtapa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEtapa_btnNovo,
            this.toolStripSeparator4,
            this.tsEtapa_btnExcluir,
            this.toolStripSeparator1,
            this.tsEtapa_btnCancelar,
            this.toolStripSeparator5,
            this.tsEtapa_btnSalvar,
            this.toolStripSeparator2});
            this.tsEtapa.Location = new System.Drawing.Point(0, 0);
            this.tsEtapa.Name = "tsEtapa";
            this.tsEtapa.Padding = new System.Windows.Forms.Padding(0);
            this.tsEtapa.Size = new System.Drawing.Size(399, 25);
            this.tsEtapa.TabIndex = 0;
            this.tsEtapa.Text = "toolStrip2";
            // 
            // tsEtapa_btnNovo
            // 
            this.tsEtapa_btnNovo.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.add;
            this.tsEtapa_btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEtapa_btnNovo.Name = "tsEtapa_btnNovo";
            this.tsEtapa_btnNovo.Size = new System.Drawing.Size(56, 22);
            this.tsEtapa_btnNovo.Text = "Novo";
            this.tsEtapa_btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEtapa_btnNovo.Click += new System.EventHandler(this.TsEtapa_btnNovo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEtapa_btnExcluir
            // 
            this.tsEtapa_btnExcluir.Enabled = false;
            this.tsEtapa_btnExcluir.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.delete;
            this.tsEtapa_btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEtapa_btnExcluir.Name = "tsEtapa_btnExcluir";
            this.tsEtapa_btnExcluir.Size = new System.Drawing.Size(62, 22);
            this.tsEtapa_btnExcluir.Text = "Excluir";
            this.tsEtapa_btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEtapa_btnCancelar
            // 
            this.tsEtapa_btnCancelar.Enabled = false;
            this.tsEtapa_btnCancelar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.cancel;
            this.tsEtapa_btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEtapa_btnCancelar.Name = "tsEtapa_btnCancelar";
            this.tsEtapa_btnCancelar.Size = new System.Drawing.Size(73, 22);
            this.tsEtapa_btnCancelar.Text = "Cancelar";
            this.tsEtapa_btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEtapa_btnCancelar.Click += new System.EventHandler(this.TsEtapa_btnCancelar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsEtapa_btnSalvar
            // 
            this.tsEtapa_btnSalvar.Enabled = false;
            this.tsEtapa_btnSalvar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
            this.tsEtapa_btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEtapa_btnSalvar.Name = "tsEtapa_btnSalvar";
            this.tsEtapa_btnSalvar.Size = new System.Drawing.Size(58, 22);
            this.tsEtapa_btnSalvar.Text = "Salvar";
            this.tsEtapa_btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsEtapa_btnSalvar.Click += new System.EventHandler(this.TsEtapa_btnSalvar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmbVencimento
            // 
            this.cmbVencimento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbVencimento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbVencimento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVencimento.Enabled = false;
            this.cmbVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVencimento.FormattingEnabled = true;
            this.cmbVencimento.Items.AddRange(new object[] {
            "SELECIONE...",
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
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cmbVencimento.Location = new System.Drawing.Point(129, 50);
            this.cmbVencimento.Name = "cmbVencimento";
            this.cmbVencimento.Size = new System.Drawing.Size(116, 21);
            this.cmbVencimento.TabIndex = 2;
            // 
            // cmbFechamento
            // 
            this.cmbFechamento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbFechamento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFechamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFechamento.Enabled = false;
            this.cmbFechamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFechamento.FormattingEnabled = true;
            this.cmbFechamento.Items.AddRange(new object[] {
            "SELECIONE...",
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
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cmbFechamento.Location = new System.Drawing.Point(7, 50);
            this.cmbFechamento.Name = "cmbFechamento";
            this.cmbFechamento.Size = new System.Drawing.Size(116, 21);
            this.cmbFechamento.TabIndex = 1;
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 433);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // cmbCicloDeVencimento
            // 
            this.cmbCicloDeVencimento.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCicloDeVencimento.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCicloDeVencimento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCicloDeVencimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCicloDeVencimento.FormattingEnabled = true;
            this.cmbCicloDeVencimento.Items.AddRange(new object[] {
            "SELECIONE...",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbCicloDeVencimento.Location = new System.Drawing.Point(256, 398);
            this.cmbCicloDeVencimento.Name = "cmbCicloDeVencimento";
            this.cmbCicloDeVencimento.Size = new System.Drawing.Size(101, 21);
            this.cmbCicloDeVencimento.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(253, 382);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Ciclo de Vencimento";
            // 
            // chkVencimentoAtivo
            // 
            this.chkVencimentoAtivo.AutoSize = true;
            this.chkVencimentoAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVencimentoAtivo.Location = new System.Drawing.Point(363, 402);
            this.chkVencimentoAtivo.Name = "chkVencimentoAtivo";
            this.chkVencimentoAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkVencimentoAtivo.TabIndex = 4;
            this.chkVencimentoAtivo.Text = "Ativo";
            this.chkVencimentoAtivo.UseVisualStyleBackColor = true;
            // 
            // dgResultado
            // 
            this.dgResultado.AllowUserToAddRows = false;
            this.dgResultado.AllowUserToDeleteRows = false;
            this.dgResultado.AllowUserToResizeRows = false;
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
            this.dgResultado.Location = new System.Drawing.Point(12, 62);
            this.dgResultado.MultiSelect = false;
            this.dgResultado.Name = "dgResultado";
            this.dgResultado.ReadOnly = true;
            this.dgResultado.RowHeadersVisible = false;
            this.dgResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResultado.Size = new System.Drawing.Size(401, 220);
            this.dgResultado.TabIndex = 26;
            this.dgResultado.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgResultado_CellDoubleClick);
            // 
            // VencimentoDeFaturaDetalhe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(428, 469);
            this.Controls.Add(this.dgResultado);
            this.Controls.Add(this.chkVencimentoAtivo);
            this.Controls.Add(this.cmbCicloDeVencimento);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbOrdem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDiaDeAtivacao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VencimentoDeFaturaDetalhe";
            this.Text = "Detalhes do Vencimento de Fatura";
            this.Load += new System.EventHandler(this.VencimentoDeFatura_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tsEtapa.ResumeLayout(false);
            this.tsEtapa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDiaDeAtivacao;
        private System.Windows.Forms.ComboBox cmbOrdem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkCicloAtivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip tsEtapa;
        private System.Windows.Forms.ToolStripButton tsEtapa_btnNovo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsEtapa_btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsEtapa_btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsEtapa_btnSalvar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.ComboBox cmbCicloDeVencimento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkVencimentoAtivo;
        private System.Windows.Forms.DataGridView dgResultado;
        private System.Windows.Forms.ComboBox cmbVencimento;
        private System.Windows.Forms.ComboBox cmbFechamento;
    }
}