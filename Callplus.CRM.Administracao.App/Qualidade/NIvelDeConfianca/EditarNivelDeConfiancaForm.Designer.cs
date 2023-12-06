namespace Callplus.CRM.Administracao.App.Qualidade.NivelDeConfianca
{
    partial class EditarNivelDeConfiancaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarNivelDeConfiancaForm));
            this.dgHistoricoNotaAgente = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAgente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numNota = new System.Windows.Forms.NumericUpDown();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgHistoricoNotaProducaoAgente = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExportarHistoricoProducao = new System.Windows.Forms.Button();
            this.btnExportarHistoricoInicial = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoricoNotaAgente)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNota)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoricoNotaProducaoAgente)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgHistoricoNotaAgente
            // 
            this.dgHistoricoNotaAgente.AllowUserToAddRows = false;
            this.dgHistoricoNotaAgente.AllowUserToDeleteRows = false;
            this.dgHistoricoNotaAgente.AllowUserToResizeRows = false;
            this.dgHistoricoNotaAgente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgHistoricoNotaAgente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgHistoricoNotaAgente.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgHistoricoNotaAgente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistoricoNotaAgente.EnableHeadersVisualStyles = false;
            this.dgHistoricoNotaAgente.Location = new System.Drawing.Point(6, 19);
            this.dgHistoricoNotaAgente.MultiSelect = false;
            this.dgHistoricoNotaAgente.Name = "dgHistoricoNotaAgente";
            this.dgHistoricoNotaAgente.ReadOnly = true;
            this.dgHistoricoNotaAgente.RowHeadersVisible = false;
            this.dgHistoricoNotaAgente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistoricoNotaAgente.Size = new System.Drawing.Size(740, 259);
            this.dgHistoricoNotaAgente.TabIndex = 56;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnExportarHistoricoInicial);
            this.groupBox3.Controls.Add(this.dgHistoricoNotaAgente);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(752, 315);
            this.groupBox3.TabIndex = 55;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hístorico Notas Iniciais";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTitulo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAgente);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numNota);
            this.groupBox1.Controls.Add(this.btnPesquisar);
            this.groupBox1.Controls.Add(this.btnSalvar);
            this.groupBox1.Controls.Add(this.btnFechar);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 64);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ações";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(451, 33);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(148, 20);
            this.txtTitulo.TabIndex = 19;
            this.txtTitulo.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(448, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Titulo";
            this.label1.Visible = false;
            // 
            // txtAgente
            // 
            this.txtAgente.Enabled = false;
            this.txtAgente.Location = new System.Drawing.Point(14, 33);
            this.txtAgente.Name = "txtAgente";
            this.txtAgente.Size = new System.Drawing.Size(278, 20);
            this.txtAgente.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Nova Nota Inicial";
            // 
            // numNota
            // 
            this.numNota.DecimalPlaces = 2;
            this.numNota.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numNota.Location = new System.Drawing.Point(298, 32);
            this.numNota.Name = "numNota";
            this.numNota.Size = new System.Drawing.Size(86, 20);
            this.numNota.TabIndex = 13;
            this.numNota.ValueChanged += new System.EventHandler(this.numNota_ValueChanged);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.SystemColors.Control;
            this.btnPesquisar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPesquisar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.search;
            this.btnPesquisar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisar.Location = new System.Drawing.Point(797, 31);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisar.TabIndex = 5;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSalvar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.add;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.Location = new System.Drawing.Point(605, 30);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 25);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
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
            this.btnFechar.Location = new System.Drawing.Point(690, 30);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(80, 25);
            this.btnFechar.TabIndex = 7;
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
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Agente";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 85);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 353);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 327);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hístorico de Nota Inicial";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 327);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Hístorico Nota de Produção";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgHistoricoNotaProducaoAgente
            // 
            this.dgHistoricoNotaProducaoAgente.AllowUserToAddRows = false;
            this.dgHistoricoNotaProducaoAgente.AllowUserToDeleteRows = false;
            this.dgHistoricoNotaProducaoAgente.AllowUserToResizeRows = false;
            this.dgHistoricoNotaProducaoAgente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgHistoricoNotaProducaoAgente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgHistoricoNotaProducaoAgente.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgHistoricoNotaProducaoAgente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistoricoNotaProducaoAgente.EnableHeadersVisualStyles = false;
            this.dgHistoricoNotaProducaoAgente.Location = new System.Drawing.Point(6, 19);
            this.dgHistoricoNotaProducaoAgente.MultiSelect = false;
            this.dgHistoricoNotaProducaoAgente.Name = "dgHistoricoNotaProducaoAgente";
            this.dgHistoricoNotaProducaoAgente.ReadOnly = true;
            this.dgHistoricoNotaProducaoAgente.RowHeadersVisible = false;
            this.dgHistoricoNotaProducaoAgente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistoricoNotaProducaoAgente.Size = new System.Drawing.Size(740, 259);
            this.dgHistoricoNotaProducaoAgente.TabIndex = 56;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnExportarHistoricoProducao);
            this.groupBox2.Controls.Add(this.dgHistoricoNotaProducaoAgente);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 315);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hístorico Notas de Produção";
            // 
            // btnExportarHistoricoProducao
            // 
            this.btnExportarHistoricoProducao.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportarHistoricoProducao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarHistoricoProducao.FlatAppearance.BorderSize = 0;
            this.btnExportarHistoricoProducao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnExportarHistoricoProducao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarHistoricoProducao.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportarHistoricoProducao.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.import;
            this.btnExportarHistoricoProducao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarHistoricoProducao.Location = new System.Drawing.Point(561, 284);
            this.btnExportarHistoricoProducao.Name = "btnExportarHistoricoProducao";
            this.btnExportarHistoricoProducao.Size = new System.Drawing.Size(176, 25);
            this.btnExportarHistoricoProducao.TabIndex = 57;
            this.btnExportarHistoricoProducao.Text = "Exportar Hístorico Produção";
            this.btnExportarHistoricoProducao.UseVisualStyleBackColor = true;
            this.btnExportarHistoricoProducao.Click += new System.EventHandler(this.btnExportarHistoricoProducao_Click);
            // 
            // btnExportarHistoricoInicial
            // 
            this.btnExportarHistoricoInicial.BackColor = System.Drawing.SystemColors.Control;
            this.btnExportarHistoricoInicial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarHistoricoInicial.FlatAppearance.BorderSize = 0;
            this.btnExportarHistoricoInicial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnExportarHistoricoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarHistoricoInicial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExportarHistoricoInicial.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.import;
            this.btnExportarHistoricoInicial.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarHistoricoInicial.Location = new System.Drawing.Point(561, 284);
            this.btnExportarHistoricoInicial.Name = "btnExportarHistoricoInicial";
            this.btnExportarHistoricoInicial.Size = new System.Drawing.Size(176, 25);
            this.btnExportarHistoricoInicial.TabIndex = 58;
            this.btnExportarHistoricoInicial.Text = "Exportar Hístorico Inicial";
            this.btnExportarHistoricoInicial.UseVisualStyleBackColor = true;
            this.btnExportarHistoricoInicial.Click += new System.EventHandler(this.btnExportarHistoricoInicial_Click);
            // 
            // EditarNivelDeConfiancaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditarNivelDeConfiancaForm";
            this.Text = "Hístorico Nota do Agente";
            this.Load += new System.EventHandler(this.EditarNivelDeConfiancaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoricoNotaAgente)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNota)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoricoNotaProducaoAgente)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgHistoricoNotaAgente;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAgente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numNota;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgHistoricoNotaProducaoAgente;
        private System.Windows.Forms.Button btnExportarHistoricoInicial;
        private System.Windows.Forms.Button btnExportarHistoricoProducao;
    }
}