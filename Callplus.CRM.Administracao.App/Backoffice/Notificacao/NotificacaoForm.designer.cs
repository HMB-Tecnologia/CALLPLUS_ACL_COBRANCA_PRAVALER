namespace Callplus.CRM.Administracao.App.Backoffice.Notificacao
{
    partial class NotificacaoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificacaoForm));
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabHistorico = new System.Windows.Forms.TabControl();
            this.tabDetalhe = new System.Windows.Forms.TabPage();
            this.lnkNenhum = new System.Windows.Forms.LinkLabel();
            this.lnkTodos = new System.Windows.Forms.LinkLabel();
            this.clbOperadores = new System.Windows.Forms.CheckedListBox();
            this.cmbSupervisor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.dgHistoricoLeitura = new System.Windows.Forms.DataGridView();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabHistorico.SuspendLayout();
            this.tabDetalhe.SuspendLayout();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoricoLeitura)).BeginInit();
            this.SuspendLayout();
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 552);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 11;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(7, 5);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(305, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "DETALHES DA NOTIFICAÇÃO";
            // 
            // tabHistorico
            // 
            this.tabHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabHistorico.Controls.Add(this.tabDetalhe);
            this.tabHistorico.Controls.Add(this.tabLog);
            this.tabHistorico.Location = new System.Drawing.Point(12, 249);
            this.tabHistorico.Name = "tabHistorico";
            this.tabHistorico.SelectedIndex = 0;
            this.tabHistorico.Size = new System.Drawing.Size(678, 297);
            this.tabHistorico.TabIndex = 10;
            // 
            // tabDetalhe
            // 
            this.tabDetalhe.Controls.Add(this.lnkNenhum);
            this.tabDetalhe.Controls.Add(this.lnkTodos);
            this.tabDetalhe.Controls.Add(this.clbOperadores);
            this.tabDetalhe.Controls.Add(this.cmbSupervisor);
            this.tabDetalhe.Controls.Add(this.label6);
            this.tabDetalhe.Controls.Add(this.label4);
            this.tabDetalhe.Location = new System.Drawing.Point(4, 22);
            this.tabDetalhe.Name = "tabDetalhe";
            this.tabDetalhe.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetalhe.Size = new System.Drawing.Size(670, 271);
            this.tabDetalhe.TabIndex = 0;
            this.tabDetalhe.Text = "Operadores";
            this.tabDetalhe.UseVisualStyleBackColor = true;
            // 
            // lnkNenhum
            // 
            this.lnkNenhum.AutoSize = true;
            this.lnkNenhum.Location = new System.Drawing.Point(614, 47);
            this.lnkNenhum.Name = "lnkNenhum";
            this.lnkNenhum.Size = new System.Drawing.Size(47, 13);
            this.lnkNenhum.TabIndex = 4;
            this.lnkNenhum.TabStop = true;
            this.lnkNenhum.Text = "Nenhum";
            this.lnkNenhum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNenhum_LinkClicked);
            // 
            // lnkTodos
            // 
            this.lnkTodos.AutoSize = true;
            this.lnkTodos.Location = new System.Drawing.Point(571, 47);
            this.lnkTodos.Name = "lnkTodos";
            this.lnkTodos.Size = new System.Drawing.Size(37, 13);
            this.lnkTodos.TabIndex = 3;
            this.lnkTodos.TabStop = true;
            this.lnkTodos.Text = "Todos";
            this.lnkTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTodos_LinkClicked);
            // 
            // clbOperadores
            // 
            this.clbOperadores.CheckOnClick = true;
            this.clbOperadores.FormattingEnabled = true;
            this.clbOperadores.Location = new System.Drawing.Point(7, 63);
            this.clbOperadores.Name = "clbOperadores";
            this.clbOperadores.Size = new System.Drawing.Size(654, 199);
            this.clbOperadores.TabIndex = 5;
            // 
            // cmbSupervisor
            // 
            this.cmbSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupervisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupervisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupervisor.FormattingEnabled = true;
            this.cmbSupervisor.Location = new System.Drawing.Point(7, 19);
            this.cmbSupervisor.Name = "cmbSupervisor";
            this.cmbSupervisor.Size = new System.Drawing.Size(654, 21);
            this.cmbSupervisor.TabIndex = 1;
            this.cmbSupervisor.SelectionChangeCommitted += new System.EventHandler(this.cmbSupervisor_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Supervisor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Operadores a Serem Notificados";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.dgHistoricoLeitura);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(670, 271);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Histórico de Leitura";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // dgHistoricoLeitura
            // 
            this.dgHistoricoLeitura.AllowUserToAddRows = false;
            this.dgHistoricoLeitura.AllowUserToDeleteRows = false;
            this.dgHistoricoLeitura.AllowUserToResizeRows = false;
            this.dgHistoricoLeitura.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgHistoricoLeitura.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgHistoricoLeitura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgHistoricoLeitura.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgHistoricoLeitura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHistoricoLeitura.Location = new System.Drawing.Point(3, 3);
            this.dgHistoricoLeitura.MultiSelect = false;
            this.dgHistoricoLeitura.Name = "dgHistoricoLeitura";
            this.dgHistoricoLeitura.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgHistoricoLeitura.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgHistoricoLeitura.RowHeadersVisible = false;
            this.dgHistoricoLeitura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistoricoLeitura.Size = new System.Drawing.Size(664, 265);
            this.dgHistoricoLeitura.TabIndex = 120;
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(122, 219);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(101, 20);
            this.dtpDataFinal.TabIndex = 8;
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(15, 219);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(101, 20);
            this.dtpDataInicial.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Título";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(15, 55);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(671, 20);
            this.txtTitulo.TabIndex = 2;
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAtivo.Location = new System.Drawing.Point(229, 222);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 9;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(119, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Término Vigência";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Início Vigência";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(15, 96);
            this.txtMensagem.MaxLength = 8000;
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensagem.Size = new System.Drawing.Size(671, 104);
            this.txtMensagem.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mensagem";
            // 
            // NotificacaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(699, 589);
            this.Controls.Add(this.dtpDataFinal);
            this.Controls.Add(this.tabHistorico);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.dtpDataInicial);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificacaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes da Notificação";
            this.Load += new System.EventHandler(this.ListaNotificacoesForm_Load);
            this.tabHistorico.ResumeLayout(false);
            this.tabDetalhe.ResumeLayout(false);
            this.tabDetalhe.PerformLayout();
            this.tabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHistoricoLeitura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabHistorico;
        private System.Windows.Forms.TabPage tabDetalhe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.DataGridView dgHistoricoLeitura;
        private System.Windows.Forms.LinkLabel lnkNenhum;
        private System.Windows.Forms.LinkLabel lnkTodos;
        private System.Windows.Forms.CheckedListBox clbOperadores;
        private System.Windows.Forms.ComboBox cmbSupervisor;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}