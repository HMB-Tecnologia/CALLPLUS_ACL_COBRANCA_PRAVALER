namespace Callplus.CRM.Administracao.App.Planejamento.StatusDeOferta
{
    partial class StatusDeAcordoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusDeAcordoForm));
			this.txtObservacao = new System.Windows.Forms.TextBox();
			this.lblCampanha = new System.Windows.Forms.Label();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.chkAtivo = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSalvar = new System.Windows.Forms.Button();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.cmbTipoDeStatusDeOferta = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.clbCampanhas = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lnkNenhum = new System.Windows.Forms.LinkLabel();
			this.lnkTodos = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbStatusDeAtendimento = new System.Windows.Forms.ComboBox();
			this.lklNovoStatusDeAtendimento = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// txtObservacao
			// 
			this.txtObservacao.Location = new System.Drawing.Point(12, 157);
			this.txtObservacao.Multiline = true;
			this.txtObservacao.Name = "txtObservacao";
			this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtObservacao.Size = new System.Drawing.Size(535, 66);
			this.txtObservacao.TabIndex = 3;
			// 
			// lblCampanha
			// 
			this.lblCampanha.AutoSize = true;
			this.lblCampanha.Location = new System.Drawing.Point(9, 106);
			this.lblCampanha.Name = "lblCampanha";
			this.lblCampanha.Size = new System.Drawing.Size(35, 13);
			this.lblCampanha.TabIndex = 44;
			this.lblCampanha.Text = "Nome";
			// 
			// lblTitulo
			// 
			this.lblTitulo.AutoSize = true;
			this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitulo.Location = new System.Drawing.Point(7, 9);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(95, 25);
			this.lblTitulo.TabIndex = 52;
			this.lblTitulo.Text = "STATUS";
			// 
			// chkAtivo
			// 
			this.chkAtivo.AutoSize = true;
			this.chkAtivo.Checked = true;
			this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAtivo.Location = new System.Drawing.Point(500, 62);
			this.chkAtivo.Name = "chkAtivo";
			this.chkAtivo.Size = new System.Drawing.Size(50, 17);
			this.chkAtivo.TabIndex = 1;
			this.chkAtivo.Text = "Ativo";
			this.chkAtivo.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 141);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 13);
			this.label3.TabIndex = 61;
			this.label3.Text = "Observação";
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
			this.btnSalvar.Location = new System.Drawing.Point(12, 442);
			this.btnSalvar.Name = "btnSalvar";
			this.btnSalvar.Size = new System.Drawing.Size(93, 27);
			this.btnSalvar.TabIndex = 9;
			this.btnSalvar.Text = "Salvar  ";
			this.btnSalvar.UseVisualStyleBackColor = true;
			this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
			// 
			// txtNome
			// 
			this.txtNome.Location = new System.Drawing.Point(12, 106);
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(535, 20);
			this.txtNome.TabIndex = 2;
			// 
			// cmbTipoDeStatusDeOferta
			// 
			this.cmbTipoDeStatusDeOferta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbTipoDeStatusDeOferta.FormattingEnabled = true;
			this.cmbTipoDeStatusDeOferta.Location = new System.Drawing.Point(12, 60);
			this.cmbTipoDeStatusDeOferta.Name = "cmbTipoDeStatusDeOferta";
			this.cmbTipoDeStatusDeOferta.Size = new System.Drawing.Size(482, 21);
			this.cmbTipoDeStatusDeOferta.TabIndex = 0;
			this.cmbTipoDeStatusDeOferta.SelectionChangeCommitted += new System.EventHandler(this.CmbTipoDeStatusDeOferta_SelectionChangeCommitted);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Black;
			this.label6.Location = new System.Drawing.Point(9, 44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 13);
			this.label6.TabIndex = 68;
			this.label6.Text = "Tipo de Acordo";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(9, 90);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(135, 13);
			this.label1.TabIndex = 70;
			this.label1.Text = "Nome do Status de Acordo";
			// 
			// clbCampanhas
			// 
			this.clbCampanhas.CheckOnClick = true;
			this.clbCampanhas.FormattingEnabled = true;
			this.clbCampanhas.Location = new System.Drawing.Point(12, 252);
			this.clbCampanhas.Name = "clbCampanhas";
			this.clbCampanhas.Size = new System.Drawing.Size(535, 124);
			this.clbCampanhas.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 236);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 75;
			this.label2.Text = "Campanhas";
			// 
			// lnkNenhum
			// 
			this.lnkNenhum.AutoSize = true;
			this.lnkNenhum.Location = new System.Drawing.Point(503, 236);
			this.lnkNenhum.Name = "lnkNenhum";
			this.lnkNenhum.Size = new System.Drawing.Size(47, 13);
			this.lnkNenhum.TabIndex = 5;
			this.lnkNenhum.TabStop = true;
			this.lnkNenhum.Text = "Nenhum";
			this.lnkNenhum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNenhum_LinkClicked);
			// 
			// lnkTodos
			// 
			this.lnkTodos.AutoSize = true;
			this.lnkTodos.Location = new System.Drawing.Point(460, 236);
			this.lnkTodos.Name = "lnkTodos";
			this.lnkTodos.Size = new System.Drawing.Size(37, 13);
			this.lnkTodos.TabIndex = 4;
			this.lnkTodos.TabStop = true;
			this.lnkTodos.Text = "Todos";
			this.lnkTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTodos_LinkClicked);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(9, 388);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(164, 13);
			this.label4.TabIndex = 68;
			this.label4.Text = "Status de Atendimento Vinculado";
			// 
			// cmbStatusDeAtendimento
			// 
			this.cmbStatusDeAtendimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbStatusDeAtendimento.FormattingEnabled = true;
			this.cmbStatusDeAtendimento.Location = new System.Drawing.Point(12, 404);
			this.cmbStatusDeAtendimento.Name = "cmbStatusDeAtendimento";
			this.cmbStatusDeAtendimento.Size = new System.Drawing.Size(535, 21);
			this.cmbStatusDeAtendimento.TabIndex = 8;
			// 
			// lklNovoStatusDeAtendimento
			// 
			this.lklNovoStatusDeAtendimento.AutoSize = true;
			this.lklNovoStatusDeAtendimento.Location = new System.Drawing.Point(457, 388);
			this.lklNovoStatusDeAtendimento.Name = "lklNovoStatusDeAtendimento";
			this.lklNovoStatusDeAtendimento.Size = new System.Drawing.Size(90, 13);
			this.lklNovoStatusDeAtendimento.TabIndex = 7;
			this.lklNovoStatusDeAtendimento.TabStop = true;
			this.lklNovoStatusDeAtendimento.Text = "Criar Novo Status";
			this.lklNovoStatusDeAtendimento.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LklNovoStatusDeAtendimento_LinkClicked);
			// 
			// StatusDeAcordoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(556, 481);
			this.Controls.Add(this.lklNovoStatusDeAtendimento);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lnkNenhum);
			this.Controls.Add(this.lnkTodos);
			this.Controls.Add(this.clbCampanhas);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbStatusDeAtendimento);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmbTipoDeStatusDeOferta);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtNome);
			this.Controls.Add(this.btnSalvar);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.chkAtivo);
			this.Controls.Add(this.lblTitulo);
			this.Controls.Add(this.lblCampanha);
			this.Controls.Add(this.txtObservacao);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StatusDeAcordoForm";
			this.Text = "Configuração do Status de Acordo";
			this.Load += new System.EventHandler(this.StatusDeAtendimentoForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Label lblCampanha;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ComboBox cmbTipoDeStatusDeOferta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbCampanhas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkNenhum;
        private System.Windows.Forms.LinkLabel lnkTodos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStatusDeAtendimento;
        private System.Windows.Forms.LinkLabel lklNovoStatusDeAtendimento;
    }
}