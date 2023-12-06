namespace Callplus.CRM.Tabulador.App.Cep
{
    partial class ConsultaDeCepForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultaDeCepForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lklLimpar = new System.Windows.Forms.LinkLabel();
            this.cmbBairro = new System.Windows.Forms.ComboBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.cmbUF = new System.Windows.Forms.ComboBox();
            this.lblUf = new System.Windows.Forms.Label();
            this.grbTipoPesquisa = new System.Windows.Forms.GroupBox();
            this.rbPesquisaEndereco = new System.Windows.Forms.RadioButton();
            this.rbPesquisaCep = new System.Windows.Forms.RadioButton();
            this.lblLogradouro = new System.Windows.Forms.Label();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.lblCidade = new System.Windows.Forms.Label();
            this.cmbCidade = new System.Windows.Forms.ComboBox();
            this.lblCep = new System.Windows.Forms.Label();
            this.txtCep = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgEndereco = new System.Windows.Forms.DataGridView();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grbTipoPesquisa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEndereco)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lklLimpar);
            this.groupBox1.Controls.Add(this.cmbBairro);
            this.groupBox1.Controls.Add(this.lblBairro);
            this.groupBox1.Controls.Add(this.cmbUF);
            this.groupBox1.Controls.Add(this.lblUf);
            this.groupBox1.Controls.Add(this.grbTipoPesquisa);
            this.groupBox1.Controls.Add(this.lblLogradouro);
            this.groupBox1.Controls.Add(this.txtLogradouro);
            this.groupBox1.Controls.Add(this.lblCidade);
            this.groupBox1.Controls.Add(this.cmbCidade);
            this.groupBox1.Controls.Add(this.lblCep);
            this.groupBox1.Controls.Add(this.txtCep);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // lklLimpar
            // 
            this.lklLimpar.AutoSize = true;
            this.lklLimpar.Location = new System.Drawing.Point(593, 11);
            this.lklLimpar.Name = "lklLimpar";
            this.lklLimpar.Size = new System.Drawing.Size(72, 13);
            this.lklLimpar.TabIndex = 12;
            this.lklLimpar.TabStop = true;
            this.lklLimpar.Text = "Limpar Dados";
            this.lklLimpar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklLimpar_LinkClicked);
            // 
            // cmbBairro
            // 
            this.cmbBairro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBairro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBairro.FormattingEnabled = true;
            this.cmbBairro.Items.AddRange(new object[] {
            "SELECIONE..."});
            this.cmbBairro.Location = new System.Drawing.Point(115, 66);
            this.cmbBairro.Name = "cmbBairro";
            this.cmbBairro.Size = new System.Drawing.Size(176, 21);
            this.cmbBairro.TabIndex = 8;
            this.cmbBairro.SelectionChangeCommitted += new System.EventHandler(this.cmbBairro_SelectionChangeCommitted);
            this.cmbBairro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbBairro_KeyPress);
            this.cmbBairro.Leave += new System.EventHandler(this.cmbBairro_Leave);
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.Location = new System.Drawing.Point(112, 51);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(34, 13);
            this.lblBairro.TabIndex = 7;
            this.lblBairro.Text = "Bairro";
            // 
            // cmbUF
            // 
            this.cmbUF.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbUF.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUF.FormattingEnabled = true;
            this.cmbUF.Location = new System.Drawing.Point(200, 29);
            this.cmbUF.Name = "cmbUF";
            this.cmbUF.Size = new System.Drawing.Size(91, 21);
            this.cmbUF.TabIndex = 4;
            this.cmbUF.SelectionChangeCommitted += new System.EventHandler(this.cmbUF_SelectionChangeCommitted);
            this.cmbUF.Leave += new System.EventHandler(this.cmbUF_Leave);
            // 
            // lblUf
            // 
            this.lblUf.AutoSize = true;
            this.lblUf.Location = new System.Drawing.Point(197, 14);
            this.lblUf.Name = "lblUf";
            this.lblUf.Size = new System.Drawing.Size(28, 13);
            this.lblUf.TabIndex = 3;
            this.lblUf.Text = "UF *";
            // 
            // grbTipoPesquisa
            // 
            this.grbTipoPesquisa.Controls.Add(this.rbPesquisaEndereco);
            this.grbTipoPesquisa.Controls.Add(this.rbPesquisaCep);
            this.grbTipoPesquisa.Location = new System.Drawing.Point(6, 14);
            this.grbTipoPesquisa.Name = "grbTipoPesquisa";
            this.grbTipoPesquisa.Size = new System.Drawing.Size(103, 73);
            this.grbTipoPesquisa.TabIndex = 0;
            this.grbTipoPesquisa.TabStop = false;
            this.grbTipoPesquisa.Text = "Tipo de Pesquisa";
            // 
            // rbPesquisaEndereco
            // 
            this.rbPesquisaEndereco.AutoSize = true;
            this.rbPesquisaEndereco.Checked = true;
            this.rbPesquisaEndereco.Location = new System.Drawing.Point(16, 45);
            this.rbPesquisaEndereco.Name = "rbPesquisaEndereco";
            this.rbPesquisaEndereco.Size = new System.Drawing.Size(71, 17);
            this.rbPesquisaEndereco.TabIndex = 1;
            this.rbPesquisaEndereco.TabStop = true;
            this.rbPesquisaEndereco.Text = "Endereço";
            this.rbPesquisaEndereco.UseVisualStyleBackColor = true;
            this.rbPesquisaEndereco.CheckedChanged += new System.EventHandler(this.rbPesquisaEndereco_CheckedChanged);
            // 
            // rbPesquisaCep
            // 
            this.rbPesquisaCep.AutoSize = true;
            this.rbPesquisaCep.Location = new System.Drawing.Point(16, 22);
            this.rbPesquisaCep.Name = "rbPesquisaCep";
            this.rbPesquisaCep.Size = new System.Drawing.Size(46, 17);
            this.rbPesquisaCep.TabIndex = 0;
            this.rbPesquisaCep.Text = "CEP";
            this.rbPesquisaCep.UseVisualStyleBackColor = true;
            this.rbPesquisaCep.CheckedChanged += new System.EventHandler(this.rbPesquisaCep_CheckedChanged);
            // 
            // lblLogradouro
            // 
            this.lblLogradouro.AutoSize = true;
            this.lblLogradouro.Location = new System.Drawing.Point(294, 51);
            this.lblLogradouro.Name = "lblLogradouro";
            this.lblLogradouro.Size = new System.Drawing.Size(61, 13);
            this.lblLogradouro.TabIndex = 9;
            this.lblLogradouro.Text = "Logradouro";
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtLogradouro.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLogradouro.Location = new System.Drawing.Point(297, 67);
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Size = new System.Drawing.Size(293, 20);
            this.txtLogradouro.TabIndex = 10;
            this.txtLogradouro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLogradouro_KeyPress);
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(294, 14);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(47, 13);
            this.lblCidade.TabIndex = 5;
            this.lblCidade.Text = "Cidade *";
            // 
            // cmbCidade
            // 
            this.cmbCidade.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCidade.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCidade.FormattingEnabled = true;
            this.cmbCidade.Items.AddRange(new object[] {
            "SELECIONE..."});
            this.cmbCidade.Location = new System.Drawing.Point(297, 29);
            this.cmbCidade.Name = "cmbCidade";
            this.cmbCidade.Size = new System.Drawing.Size(293, 21);
            this.cmbCidade.TabIndex = 6;
            this.cmbCidade.SelectionChangeCommitted += new System.EventHandler(this.cmbCidade_SelectionChangeCommitted);
            this.cmbCidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCidade_KeyPress);
            this.cmbCidade.Leave += new System.EventHandler(this.cmbCidade_Leave);
            // 
            // lblCep
            // 
            this.lblCep.AutoSize = true;
            this.lblCep.Location = new System.Drawing.Point(112, 14);
            this.lblCep.Name = "lblCep";
            this.lblCep.Size = new System.Drawing.Size(35, 13);
            this.lblCep.TabIndex = 1;
            this.lblCep.Text = "CEP *";
            // 
            // txtCep
            // 
            this.txtCep.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCep.Location = new System.Drawing.Point(115, 30);
            this.txtCep.MaxLength = 8;
            this.txtCep.Name = "txtCep";
            this.txtCep.Size = new System.Drawing.Size(79, 20);
            this.txtCep.TabIndex = 2;
            this.txtCep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCep_KeyPress);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(596, 27);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(69, 61);
            this.btnConsultar.TabIndex = 11;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // dgEndereco
            // 
            this.dgEndereco.AllowUserToAddRows = false;
            this.dgEndereco.AllowUserToDeleteRows = false;
            this.dgEndereco.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEndereco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEndereco.Location = new System.Drawing.Point(12, 113);
            this.dgEndereco.MultiSelect = false;
            this.dgEndereco.Name = "dgEndereco";
            this.dgEndereco.ReadOnly = true;
            this.dgEndereco.RowHeadersVisible = false;
            this.dgEndereco.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEndereco.Size = new System.Drawing.Size(671, 228);
            this.dgEndereco.TabIndex = 1;
            this.dgEndereco.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEndereco_CellClick);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Location = new System.Drawing.Point(608, 347);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(75, 23);
            this.btnSelecionar.TabIndex = 2;
            this.btnSelecionar.Text = "Selecionar";
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // ConsultaDeCepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(695, 376);
            this.Controls.Add(this.btnSelecionar);
            this.Controls.Add(this.dgEndereco);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsultaDeCepForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar CEP";
            this.Load += new System.EventHandler(this.ConsultaDeCepForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConsultaDeCepForm_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbTipoPesquisa.ResumeLayout(false);
            this.grbTipoPesquisa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEndereco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Label lblLogradouro;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.ComboBox cmbCidade;
        private System.Windows.Forms.Label lblCep;
        private System.Windows.Forms.TextBox txtCep;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.GroupBox grbTipoPesquisa;
        private System.Windows.Forms.RadioButton rbPesquisaEndereco;
        private System.Windows.Forms.RadioButton rbPesquisaCep;
        private System.Windows.Forms.ComboBox cmbUF;
        private System.Windows.Forms.Label lblUf;
        private System.Windows.Forms.ComboBox cmbBairro;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.DataGridView dgEndereco;
        private System.Windows.Forms.LinkLabel lklLimpar;
    }
}