namespace Callplus.CRM.Administracao.App.Planejamento.VencimentoDeFatura
{
    partial class ListaVencimentoDeFatura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaVencimentoDeFatura));
            this.dgResultado = new System.Windows.Forms.DataGridView();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDiaAtivacao = new System.Windows.Forms.Label();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.gbAcoes = new System.Windows.Forms.GroupBox();
            this.cmbDiaAtivacao = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTotalRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtBuscaRapida = new System.Windows.Forms.TextBox();
            this.btnBuscaRapida = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chkListarAtivos = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
            this.gbAcoes.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.dgResultado.ReadOnly = true;
            this.dgResultado.RowHeadersVisible = false;
            this.dgResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResultado.Size = new System.Drawing.Size(1004, 317);
            this.dgResultado.TabIndex = 3;
            this.dgResultado.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgResultado_CellDoubleClick);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(7, 5);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(276, 25);
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "VENCIMENTO DE FATURA";
            // 
            // lblDiaAtivacao
            // 
            this.lblDiaAtivacao.AutoSize = true;
            this.lblDiaAtivacao.Location = new System.Drawing.Point(11, 16);
            this.lblDiaAtivacao.Name = "lblDiaAtivacao";
            this.lblDiaAtivacao.Size = new System.Drawing.Size(68, 13);
            this.lblDiaAtivacao.TabIndex = 19;
            this.lblDiaAtivacao.Text = "Dia Ativação";
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.SystemColors.Control;
            this.btnPesquisar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPesquisar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisar.Location = new System.Drawing.Point(131, 31);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisar.TabIndex = 1;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovo.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.add;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNovo.Location = new System.Drawing.Point(833, 29);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(80, 25);
            this.btnNovo.TabIndex = 2;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.close;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFechar.Location = new System.Drawing.Point(918, 29);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(80, 25);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "Fechar   ";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // gbAcoes
            // 
            this.gbAcoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAcoes.Controls.Add(this.cmbDiaAtivacao);
            this.gbAcoes.Controls.Add(this.btnFechar);
            this.gbAcoes.Controls.Add(this.lblDiaAtivacao);
            this.gbAcoes.Controls.Add(this.btnNovo);
            this.gbAcoes.Controls.Add(this.btnPesquisar);
            this.gbAcoes.Location = new System.Drawing.Point(12, 33);
            this.gbAcoes.Name = "gbAcoes";
            this.gbAcoes.Size = new System.Drawing.Size(1004, 64);
            this.gbAcoes.TabIndex = 2;
            this.gbAcoes.TabStop = false;
            this.gbAcoes.Text = "Ações";
            // 
            // cmbDiaAtivacao
            // 
            this.cmbDiaAtivacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiaAtivacao.FormattingEnabled = true;
            this.cmbDiaAtivacao.Items.AddRange(new object[] {
            "TODOS",
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
            this.cmbDiaAtivacao.Location = new System.Drawing.Point(14, 32);
            this.cmbDiaAtivacao.Name = "cmbDiaAtivacao";
            this.cmbDiaAtivacao.Size = new System.Drawing.Size(111, 21);
            this.cmbDiaAtivacao.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotalRegistros,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 454);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 24);
            this.statusStrip1.TabIndex = 25;
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
            this.txtBuscaRapida.Location = new System.Drawing.Point(930, 6);
            this.txtBuscaRapida.MaxLength = 10;
            this.txtBuscaRapida.Name = "txtBuscaRapida";
            this.txtBuscaRapida.Size = new System.Drawing.Size(55, 20);
            this.txtBuscaRapida.TabIndex = 0;
            this.txtBuscaRapida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscaRapida_KeyPress);
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
            this.btnBuscaRapida.TabIndex = 1;
            this.btnBuscaRapida.UseVisualStyleBackColor = true;
            this.btnBuscaRapida.Click += new System.EventHandler(this.btnBuscaRapida_Click);
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
            this.label3.TabIndex = 26;
            this.label3.Text = "Busca Rápida:";
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
            this.chkListarAtivos.TabIndex = 29;
            this.chkListarAtivos.Text = "Exibir somente os registros ativos";
            this.chkListarAtivos.UseVisualStyleBackColor = true;
            this.chkListarAtivos.CheckedChanged += new System.EventHandler(this.chkListarAtivos_CheckedChanged);
            // 
            // ListaVencimentoDeFatura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1028, 478);
            this.Controls.Add(this.chkListarAtivos);
            this.Controls.Add(this.txtBuscaRapida);
            this.Controls.Add(this.btnBuscaRapida);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbAcoes);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgResultado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListaVencimentoDeFatura";
            this.Text = "Vencimento de Fatura";
            this.Load += new System.EventHandler(this.ListarVencimentoFaturaClaro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).EndInit();
            this.gbAcoes.ResumeLayout(false);
            this.gbAcoes.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgResultado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDiaAtivacao;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.GroupBox gbAcoes;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalRegistros;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TextBox txtBuscaRapida;
        private System.Windows.Forms.Button btnBuscaRapida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkListarAtivos;
        private System.Windows.Forms.ComboBox cmbDiaAtivacao;
    }
}