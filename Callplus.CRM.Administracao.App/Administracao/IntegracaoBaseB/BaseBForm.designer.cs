namespace Callplus.CRM.Administracao.App.IntegracaoBaseB
{
    partial class fBaseB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fBaseB));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFechar = new System.Windows.Forms.Button();
            this.grbContatosNaoTrabalhados = new System.Windows.Forms.GroupBox();
            this.chkVirgens = new System.Windows.Forms.CheckBox();
            this.chkRetornoClaro = new System.Windows.Forms.CheckBox();
            this.btnExportCSV = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgResultado = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCampanha = new System.Windows.Forms.ComboBox();
            this.rbCodMailing = new System.Windows.Forms.RadioButton();
            this.rbData = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCodMailing = new System.Windows.Forms.Label();
            this.cmdPesquisar = new System.Windows.Forms.Button();
            this.txtcodMailing = new System.Windows.Forms.TextBox();
            this.lblDataFinal = new System.Windows.Forms.Label();
            this.datDataInicial = new System.Windows.Forms.DateTimePicker();
            this.lblDataInicial = new System.Windows.Forms.Label();
            this.datDataFinal = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTotalTrabalhado = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalEnvio = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3.SuspendLayout();
            this.grbContatosNaoTrabalhados.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 25);
            this.label2.TabIndex = 74;
            this.label2.Text = "BASE B";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnFechar);
            this.groupBox3.Controls.Add(this.grbContatosNaoTrabalhados);
            this.groupBox3.Controls.Add(this.btnExportCSV);
            this.groupBox3.Location = new System.Drawing.Point(616, 48);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 152);
            this.groupBox3.TabIndex = 189;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Exportação";
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
            this.btnFechar.Location = new System.Drawing.Point(508, 120);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(112, 25);
            this.btnFechar.TabIndex = 186;
            this.btnFechar.Text = "Fechar   ";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // grbContatosNaoTrabalhados
            // 
            this.grbContatosNaoTrabalhados.Controls.Add(this.chkVirgens);
            this.grbContatosNaoTrabalhados.Controls.Add(this.chkRetornoClaro);
            this.grbContatosNaoTrabalhados.Location = new System.Drawing.Point(6, 19);
            this.grbContatosNaoTrabalhados.Name = "grbContatosNaoTrabalhados";
            this.grbContatosNaoTrabalhados.Size = new System.Drawing.Size(335, 122);
            this.grbContatosNaoTrabalhados.TabIndex = 185;
            this.grbContatosNaoTrabalhados.TabStop = false;
            this.grbContatosNaoTrabalhados.Text = "Exportação de Contatos Não Trabalhados";
            // 
            // chkVirgens
            // 
            this.chkVirgens.AutoSize = true;
            this.chkVirgens.Location = new System.Drawing.Point(6, 22);
            this.chkVirgens.Name = "chkVirgens";
            this.chkVirgens.Size = new System.Drawing.Size(178, 17);
            this.chkVirgens.TabIndex = 177;
            this.chkVirgens.Text = "Retornar Tabulação Randômica";
            this.chkVirgens.UseVisualStyleBackColor = true;
            // 
            // chkRetornoClaro
            // 
            this.chkRetornoClaro.AutoSize = true;
            this.chkRetornoClaro.Location = new System.Drawing.Point(6, 45);
            this.chkRetornoClaro.Name = "chkRetornoClaro";
            this.chkRetornoClaro.Size = new System.Drawing.Size(125, 17);
            this.chkRetornoClaro.TabIndex = 179;
            this.chkRetornoClaro.Text = "Retornar Código Fixo";
            this.chkRetornoClaro.UseVisualStyleBackColor = true;
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCSV.FlatAppearance.BorderSize = 0;
            this.btnExportCSV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnExportCSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCSV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportCSV.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
            this.btnExportCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportCSV.Location = new System.Drawing.Point(508, 19);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(112, 25);
            this.btnExportCSV.TabIndex = 176;
            this.btnExportCSV.Text = "Exportar Arquivos";
            this.btnExportCSV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgResultado);
            this.groupBox2.Location = new System.Drawing.Point(12, 206);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1230, 390);
            this.groupBox2.TabIndex = 188;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultado";
            // 
            // dgResultado
            // 
            this.dgResultado.AllowUserToAddRows = false;
            this.dgResultado.AllowUserToDeleteRows = false;
            this.dgResultado.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dgResultado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgResultado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgResultado.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgResultado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResultado.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgResultado.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgResultado.Location = new System.Drawing.Point(9, 19);
            this.dgResultado.MultiSelect = false;
            this.dgResultado.Name = "dgResultado";
            this.dgResultado.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dgResultado.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgResultado.ShowCellErrors = false;
            this.dgResultado.ShowEditingIcon = false;
            this.dgResultado.ShowRowErrors = false;
            this.dgResultado.Size = new System.Drawing.Size(1212, 365);
            this.dgResultado.TabIndex = 168;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCampanha);
            this.groupBox1.Controls.Add(this.rbCodMailing);
            this.groupBox1.Controls.Add(this.rbData);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblCodMailing);
            this.groupBox1.Controls.Add(this.cmdPesquisar);
            this.groupBox1.Controls.Add(this.txtcodMailing);
            this.groupBox1.Controls.Add(this.lblDataFinal);
            this.groupBox1.Controls.Add(this.datDataInicial);
            this.groupBox1.Controls.Add(this.lblDataInicial);
            this.groupBox1.Controls.Add(this.datDataFinal);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 160);
            this.groupBox1.TabIndex = 187;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // cmbCampanha
            // 
            this.cmbCampanha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCampanha.FormattingEnabled = true;
            this.cmbCampanha.Location = new System.Drawing.Point(86, 55);
            this.cmbCampanha.Name = "cmbCampanha";
            this.cmbCampanha.Size = new System.Drawing.Size(229, 21);
            this.cmbCampanha.TabIndex = 185;
            // 
            // rbCodMailing
            // 
            this.rbCodMailing.AutoSize = true;
            this.rbCodMailing.Location = new System.Drawing.Point(86, 18);
            this.rbCodMailing.Name = "rbCodMailing";
            this.rbCodMailing.Size = new System.Drawing.Size(128, 17);
            this.rbCodMailing.TabIndex = 184;
            this.rbCodMailing.TabStop = true;
            this.rbCodMailing.Text = "Por Código do Mailing";
            this.rbCodMailing.UseVisualStyleBackColor = true;
            this.rbCodMailing.CheckedChanged += new System.EventHandler(this.rbCodMailing_CheckedChanged);
            // 
            // rbData
            // 
            this.rbData.AutoSize = true;
            this.rbData.Location = new System.Drawing.Point(13, 18);
            this.rbData.Name = "rbData";
            this.rbData.Size = new System.Drawing.Size(67, 17);
            this.rbData.TabIndex = 183;
            this.rbData.TabStop = true;
            this.rbData.Text = "Por Data";
            this.rbData.UseVisualStyleBackColor = true;
            this.rbData.CheckedChanged += new System.EventHandler(this.rbData_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 186;
            this.label6.Text = "Campanha:";
            // 
            // lblCodMailing
            // 
            this.lblCodMailing.AutoSize = true;
            this.lblCodMailing.Location = new System.Drawing.Point(12, 85);
            this.lblCodMailing.Name = "lblCodMailing";
            this.lblCodMailing.Size = new System.Drawing.Size(68, 13);
            this.lblCodMailing.TabIndex = 182;
            this.lblCodMailing.Text = "Cód. Mailing:";
            // 
            // cmdPesquisar
            // 
            this.cmdPesquisar.BackColor = System.Drawing.SystemColors.Control;
            this.cmdPesquisar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPesquisar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
            this.cmdPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPesquisar.Location = new System.Drawing.Point(220, 14);
            this.cmdPesquisar.Name = "cmdPesquisar";
            this.cmdPesquisar.Size = new System.Drawing.Size(95, 25);
            this.cmdPesquisar.TabIndex = 93;
            this.cmdPesquisar.Text = "Pesquisar";
            this.cmdPesquisar.UseVisualStyleBackColor = true;
            this.cmdPesquisar.Click += new System.EventHandler(this.cmdPesquisar_Click);
            // 
            // txtcodMailing
            // 
            this.txtcodMailing.Location = new System.Drawing.Point(86, 82);
            this.txtcodMailing.Name = "txtcodMailing";
            this.txtcodMailing.Size = new System.Drawing.Size(229, 20);
            this.txtcodMailing.TabIndex = 181;
            // 
            // lblDataFinal
            // 
            this.lblDataFinal.AutoSize = true;
            this.lblDataFinal.BackColor = System.Drawing.Color.Transparent;
            this.lblDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataFinal.ForeColor = System.Drawing.Color.Black;
            this.lblDataFinal.Location = new System.Drawing.Point(18, 134);
            this.lblDataFinal.Name = "lblDataFinal";
            this.lblDataFinal.Size = new System.Drawing.Size(58, 13);
            this.lblDataFinal.TabIndex = 45;
            this.lblDataFinal.Text = "Data Final:";
            // 
            // datDataInicial
            // 
            this.datDataInicial.Location = new System.Drawing.Point(86, 108);
            this.datDataInicial.Name = "datDataInicial";
            this.datDataInicial.Size = new System.Drawing.Size(229, 20);
            this.datDataInicial.TabIndex = 162;
            // 
            // lblDataInicial
            // 
            this.lblDataInicial.AutoSize = true;
            this.lblDataInicial.Location = new System.Drawing.Point(17, 108);
            this.lblDataInicial.Name = "lblDataInicial";
            this.lblDataInicial.Size = new System.Drawing.Size(63, 13);
            this.lblDataInicial.TabIndex = 159;
            this.lblDataInicial.Text = "Data Inicial:";
            // 
            // datDataFinal
            // 
            this.datDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datDataFinal.Location = new System.Drawing.Point(86, 134);
            this.datDataFinal.Name = "datDataFinal";
            this.datDataFinal.Size = new System.Drawing.Size(229, 20);
            this.datDataFinal.TabIndex = 46;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotalTrabalhado,
            this.lblTotalEnvio});
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1284, 24);
            this.statusStrip1.TabIndex = 190;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTotalTrabalhado
            // 
            this.lblTotalTrabalhado.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTrabalhado.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTotalTrabalhado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalTrabalhado.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalTrabalhado.Name = "lblTotalTrabalhado";
            this.lblTotalTrabalhado.Size = new System.Drawing.Size(246, 19);
            this.lblTotalTrabalhado.Text = "Total de contatos trabalhados na operação:";
            // 
            // lblTotalEnvio
            // 
            this.lblTotalEnvio.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalEnvio.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTotalEnvio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalEnvio.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalEnvio.Name = "lblTotalEnvio";
            this.lblTotalEnvio.Size = new System.Drawing.Size(277, 19);
            this.lblTotalEnvio.Text = "Total de contatos válidos para envio para a WCA:";
            // 
            // fBaseB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 624);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fBaseB";
            this.Text = "Base B - Claro";
            this.Load += new System.EventHandler(this.fBaseB_Load);
            this.groupBox3.ResumeLayout(false);
            this.grbContatosNaoTrabalhados.ResumeLayout(false);
            this.grbContatosNaoTrabalhados.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grbContatosNaoTrabalhados;
        private System.Windows.Forms.CheckBox chkVirgens;
        private System.Windows.Forms.CheckBox chkRetornoClaro;
        private System.Windows.Forms.Button btnExportCSV;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgResultado;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.RadioButton rbCodMailing;
        private System.Windows.Forms.RadioButton rbData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCodMailing;
        private System.Windows.Forms.Button cmdPesquisar;
        private System.Windows.Forms.TextBox txtcodMailing;
        private System.Windows.Forms.Label lblDataFinal;
        private System.Windows.Forms.DateTimePicker datDataInicial;
        private System.Windows.Forms.Label lblDataInicial;
        private System.Windows.Forms.DateTimePicker datDataFinal;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalTrabalhado;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalEnvio;
    }
}