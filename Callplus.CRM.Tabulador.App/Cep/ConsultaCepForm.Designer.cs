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
            this.cmbBairro = new System.Windows.Forms.ComboBox();
            this.label60 = new System.Windows.Forms.Label();
            this.cmbUF = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.grbTipoPesquisa = new System.Windows.Forms.GroupBox();
            this.rbPesquisaEndereco = new System.Windows.Forms.RadioButton();
            this.rbPesquisaCep = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCidade = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCep = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgEndereco = new System.Windows.Forms.DataGridView();
            this.Cep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Logradouro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bairro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grbTipoPesquisa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEndereco)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBairro);
            this.groupBox1.Controls.Add(this.label60);
            this.groupBox1.Controls.Add(this.cmbUF);
            this.groupBox1.Controls.Add(this.label62);
            this.groupBox1.Controls.Add(this.grbTipoPesquisa);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLogradouro);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbCidade);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCep);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // cmbBairro
            // 
            this.cmbBairro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbBairro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBairro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBairro.FormattingEnabled = true;
            this.cmbBairro.Items.AddRange(new object[] {
            "SELECIONE..."});
            this.cmbBairro.Location = new System.Drawing.Point(115, 66);
            this.cmbBairro.Name = "cmbBairro";
            this.cmbBairro.Size = new System.Drawing.Size(176, 21);
            this.cmbBairro.TabIndex = 8;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(112, 52);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(34, 13);
            this.label60.TabIndex = 7;
            this.label60.Text = "Bairro";
            // 
            // cmbUF
            // 
            this.cmbUF.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbUF.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUF.FormattingEnabled = true;
            this.cmbUF.Items.AddRange(new object[] {
            "SELECIONE...",
            "AC",
            "AL",
            "AM",
            "AP",
            "BA",
            "CE",
            "DF",
            "ES",
            "GO",
            "MA",
            "MG",
            "MS",
            "MT",
            "PA",
            "PB",
            "PE",
            "PI",
            "PR",
            "RJ",
            "RN",
            "RO",
            "RR",
            "RS",
            "SC",
            "SE",
            "SP",
            "TO"});
            this.cmbUF.Location = new System.Drawing.Point(200, 29);
            this.cmbUF.Name = "cmbUF";
            this.cmbUF.Size = new System.Drawing.Size(91, 21);
            this.cmbUF.TabIndex = 4;
            this.cmbUF.SelectionChangeCommitted += new System.EventHandler(this.cmbUF_SelectionChangeCommitted);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(197, 16);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(21, 13);
            this.label62.TabIndex = 3;
            this.label62.Text = "UF";
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
            this.rbPesquisaEndereco.Location = new System.Drawing.Point(16, 45);
            this.rbPesquisaEndereco.Name = "rbPesquisaEndereco";
            this.rbPesquisaEndereco.Size = new System.Drawing.Size(71, 17);
            this.rbPesquisaEndereco.TabIndex = 1;
            this.rbPesquisaEndereco.TabStop = true;
            this.rbPesquisaEndereco.Text = "Endereço";
            this.rbPesquisaEndereco.UseVisualStyleBackColor = true;
            // 
            // rbPesquisaCep
            // 
            this.rbPesquisaCep.AutoSize = true;
            this.rbPesquisaCep.Location = new System.Drawing.Point(16, 22);
            this.rbPesquisaCep.Name = "rbPesquisaCep";
            this.rbPesquisaCep.Size = new System.Drawing.Size(46, 17);
            this.rbPesquisaCep.TabIndex = 0;
            this.rbPesquisaCep.TabStop = true;
            this.rbPesquisaCep.Text = "CEP";
            this.rbPesquisaCep.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Logradouro";
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLogradouro.Location = new System.Drawing.Point(297, 67);
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Size = new System.Drawing.Size(293, 20);
            this.txtLogradouro.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cidade";
            // 
            // cmbCidade
            // 
            this.cmbCidade.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCidade.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCidade.FormattingEnabled = true;
            this.cmbCidade.Location = new System.Drawing.Point(297, 29);
            this.cmbCidade.Name = "cmbCidade";
            this.cmbCidade.Size = new System.Drawing.Size(293, 21);
            this.cmbCidade.TabIndex = 6;
            this.cmbCidade.SelectionChangeCommitted += new System.EventHandler(this.cmbCidade_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CEP";
            // 
            // txtCep
            // 
            this.txtCep.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCep.Location = new System.Drawing.Point(115, 29);
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
            this.dgEndereco.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cep,
            this.Logradouro,
            this.Bairro,
            this.Cidade,
            this.Uf});
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
            // Cep
            // 
            this.Cep.DataPropertyName = "Cep";
            this.Cep.HeaderText = "Cep";
            this.Cep.Name = "Cep";
            this.Cep.ReadOnly = true;
            // 
            // Logradouro
            // 
            this.Logradouro.DataPropertyName = "Logradouro";
            this.Logradouro.HeaderText = "Logradouro";
            this.Logradouro.Name = "Logradouro";
            this.Logradouro.ReadOnly = true;
            // 
            // Bairro
            // 
            this.Bairro.DataPropertyName = "Bairro";
            this.Bairro.HeaderText = "Bairro";
            this.Bairro.Name = "Bairro";
            this.Bairro.ReadOnly = true;
            // 
            // Cidade
            // 
            this.Cidade.DataPropertyName = "Cidade";
            this.Cidade.HeaderText = "Cidade";
            this.Cidade.Name = "Cidade";
            this.Cidade.ReadOnly = true;
            // 
            // Uf
            // 
            this.Uf.DataPropertyName = "Estado";
            this.Uf.HeaderText = "UF";
            this.Uf.Name = "Uf";
            this.Uf.ReadOnly = true;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCidade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCep;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.GroupBox grbTipoPesquisa;
        private System.Windows.Forms.RadioButton rbPesquisaEndereco;
        private System.Windows.Forms.RadioButton rbPesquisaCep;
        private System.Windows.Forms.ComboBox cmbUF;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.ComboBox cmbBairro;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.DataGridView dgEndereco;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Logradouro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bairro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uf;
    }
}