namespace Callplus.CRM.Administracao.App.Planejamento.Produto
{
    partial class ProdutoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProdutoForm));
            this.label8 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtOrdem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.cmbCampanha = new System.Windows.Forms.ComboBox();
            this.lblCampanha = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.cmbTipoDeProduto = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.chkAtivoBko = new System.Windows.Forms.CheckBox();
            this.tabDetalhesProduto = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.clbFaixaDeRecarga = new System.Windows.Forms.CheckedListBox();
            this.lnkNenhum = new System.Windows.Forms.LinkLabel();
            this.lnkTodos = new System.Windows.Forms.LinkLabel();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.tabDetalhesProduto.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Observações";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(12, 227);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(430, 107);
            this.txtObservacao.TabIndex = 7;
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 347);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 27);
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
            this.lblTitulo.Size = new System.Drawing.Size(116, 25);
            this.lblTitulo.TabIndex = 7;
            this.lblTitulo.Text = "PRODUTO";
            // 
            // txtOrdem
            // 
            this.txtOrdem.Location = new System.Drawing.Point(12, 186);
            this.txtOrdem.MaxLength = 100;
            this.txtOrdem.Name = "txtOrdem";
            this.txtOrdem.Size = new System.Drawing.Size(110, 20);
            this.txtOrdem.TabIndex = 3;
            this.txtOrdem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApenasValorNumerico);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Ordem";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 142);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(430, 20);
            this.txtNome.TabIndex = 2;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(9, 126);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 25;
            this.lblNome.Text = "Nome";
            // 
            // cmbCampanha
            // 
            this.cmbCampanha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCampanha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCampanha.FormattingEnabled = true;
            this.cmbCampanha.Location = new System.Drawing.Point(12, 102);
            this.cmbCampanha.Name = "cmbCampanha";
            this.cmbCampanha.Size = new System.Drawing.Size(430, 21);
            this.cmbCampanha.TabIndex = 1;
            // 
            // lblCampanha
            // 
            this.lblCampanha.AutoSize = true;
            this.lblCampanha.Location = new System.Drawing.Point(9, 86);
            this.lblCampanha.Name = "lblCampanha";
            this.lblCampanha.Size = new System.Drawing.Size(58, 13);
            this.lblCampanha.TabIndex = 20;
            this.lblCampanha.Text = "Campanha";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(241, 189);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 5;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // cmbTipoDeProduto
            // 
            this.cmbTipoDeProduto.FormattingEnabled = true;
            this.cmbTipoDeProduto.Location = new System.Drawing.Point(12, 62);
            this.cmbTipoDeProduto.Name = "cmbTipoDeProduto";
            this.cmbTipoDeProduto.Size = new System.Drawing.Size(430, 21);
            this.cmbTipoDeProduto.TabIndex = 0;
            this.cmbTipoDeProduto.SelectionChangeCommitted += new System.EventHandler(this.cmbTipoDeProduto_SelectionChangeCommitted);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(9, 46);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(28, 13);
            this.lblTipo.TabIndex = 18;
            this.lblTipo.Text = "Tipo";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(128, 186);
            this.txtValor.MaxLength = 100;
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(107, 20);
            this.txtValor.TabIndex = 4;
            this.txtValor.Enter += new System.EventHandler(this.TirarMascaraMonetaria);
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ApenasValorNumerico);
            this.txtValor.Leave += new System.EventHandler(this.RetornarMascaraMonetaria);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(125, 170);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(48, 13);
            this.lblValor.TabIndex = 31;
            this.lblValor.Text = "Valor R$";
            // 
            // chkAtivoBko
            // 
            this.chkAtivoBko.AutoSize = true;
            this.chkAtivoBko.Location = new System.Drawing.Point(317, 189);
            this.chkAtivoBko.Name = "chkAtivoBko";
            this.chkAtivoBko.Size = new System.Drawing.Size(125, 17);
            this.chkAtivoBko.TabIndex = 6;
            this.chkAtivoBko.Text = "Ativo para Backofice";
            this.chkAtivoBko.UseVisualStyleBackColor = true;
            // 
            // tabDetalhesProduto
            // 
            this.tabDetalhesProduto.Controls.Add(this.tabPage1);
            this.tabDetalhesProduto.Location = new System.Drawing.Point(448, 37);
            this.tabDetalhesProduto.Name = "tabDetalhesProduto";
            this.tabDetalhesProduto.SelectedIndex = 0;
            this.tabDetalhesProduto.Size = new System.Drawing.Size(381, 304);
            this.tabDetalhesProduto.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clbFaixaDeRecarga);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(373, 278);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Faixas de Recarga do Produto";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // clbFaixaDeRecarga
            // 
            this.clbFaixaDeRecarga.CheckOnClick = true;
            this.clbFaixaDeRecarga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbFaixaDeRecarga.FormattingEnabled = true;
            this.clbFaixaDeRecarga.Location = new System.Drawing.Point(3, 3);
            this.clbFaixaDeRecarga.Name = "clbFaixaDeRecarga";
            this.clbFaixaDeRecarga.Size = new System.Drawing.Size(367, 272);
            this.clbFaixaDeRecarga.TabIndex = 0;
            this.clbFaixaDeRecarga.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbFaixaDeRecarga_ItemCheck);
            // 
            // lnkNenhum
            // 
            this.lnkNenhum.AutoSize = true;
            this.lnkNenhum.Location = new System.Drawing.Point(775, 21);
            this.lnkNenhum.Name = "lnkNenhum";
            this.lnkNenhum.Size = new System.Drawing.Size(47, 13);
            this.lnkNenhum.TabIndex = 9;
            this.lnkNenhum.TabStop = true;
            this.lnkNenhum.Text = "Nenhum";
            this.lnkNenhum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNenhum_LinkClicked);
            // 
            // lnkTodos
            // 
            this.lnkTodos.AutoSize = true;
            this.lnkTodos.Location = new System.Drawing.Point(732, 21);
            this.lnkTodos.Name = "lnkTodos";
            this.lnkTodos.Size = new System.Drawing.Size(37, 13);
            this.lnkTodos.TabIndex = 8;
            this.lnkTodos.TabStop = true;
            this.lnkTodos.Text = "Todos";
            this.lnkTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTodos_LinkClicked_1);
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalRegistros.Location = new System.Drawing.Point(745, 344);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(77, 15);
            this.lblTotalRegistros.TabIndex = 40;
            this.lblTotalRegistros.Text = "0 Registro(s)";
            // 
            // ProdutoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(836, 382);
            this.Controls.Add(this.lblTotalRegistros);
            this.Controls.Add(this.lnkNenhum);
            this.Controls.Add(this.lnkTodos);
            this.Controls.Add(this.tabDetalhesProduto);
            this.Controls.Add(this.chkAtivoBko);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.txtOrdem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.cmbCampanha);
            this.Controls.Add(this.lblCampanha);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.cmbTipoDeProduto);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProdutoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Produto";
            this.Load += new System.EventHandler(this.ProdutoForm_Load);
            this.tabDetalhesProduto.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtOrdem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.Label lblCampanha;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.ComboBox cmbTipoDeProduto;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.CheckBox chkAtivoBko;
        private System.Windows.Forms.TabControl tabDetalhesProduto;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckedListBox clbFaixaDeRecarga;
        private System.Windows.Forms.LinkLabel lnkNenhum;
        private System.Windows.Forms.LinkLabel lnkTodos;
        private System.Windows.Forms.Label lblTotalRegistros;
    }
}