namespace Callplus.CRM.Administracao.App.Planejamento.Campanha
{
    partial class CampanhaEspelhoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampanhaEspelhoForm));
            this.btnSalvar = new System.Windows.Forms.Button();
            this.gbCampanhaEspelho = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.gbEspelhar = new System.Windows.Forms.GroupBox();
            this.chkChecklistVenda = new System.Windows.Forms.CheckBox();
            this.chkFormularioQualidade = new System.Windows.Forms.CheckBox();
            this.chkPlanosComparacao = new System.Windows.Forms.CheckBox();
            this.chkVariaveis = new System.Windows.Forms.CheckBox();
            this.chkFaq = new System.Windows.Forms.CheckBox();
            this.chkAparelhos = new System.Windows.Forms.CheckBox();
            this.lblExemploPasta = new System.Windows.Forms.Label();
            this.txtEnderecoImportacao = new System.Windows.Forms.TextBox();
            this.lblEnderecoDeImportacao = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbCampanhaEspelho.SuspendLayout();
            this.gbEspelhar.SuspendLayout();
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
            this.btnSalvar.Location = new System.Drawing.Point(12, 257);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 1;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // gbCampanhaEspelho
            // 
            this.gbCampanhaEspelho.Controls.Add(this.lblInfo);
            this.gbCampanhaEspelho.Controls.Add(this.chkAtivo);
            this.gbCampanhaEspelho.Controls.Add(this.gbEspelhar);
            this.gbCampanhaEspelho.Controls.Add(this.lblExemploPasta);
            this.gbCampanhaEspelho.Controls.Add(this.txtEnderecoImportacao);
            this.gbCampanhaEspelho.Controls.Add(this.lblEnderecoDeImportacao);
            this.gbCampanhaEspelho.Controls.Add(this.txtNome);
            this.gbCampanhaEspelho.Controls.Add(this.lblNome);
            this.gbCampanhaEspelho.Location = new System.Drawing.Point(13, 38);
            this.gbCampanhaEspelho.Name = "gbCampanhaEspelho";
            this.gbCampanhaEspelho.Size = new System.Drawing.Size(344, 213);
            this.gbCampanhaEspelho.TabIndex = 0;
            this.gbCampanhaEspelho.TabStop = false;
            this.gbCampanhaEspelho.Text = "Dados da Nova Campanha";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(216, 109);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(119, 13);
            this.lblInfo.TabIndex = 37;
            this.lblInfo.Text = "* Recomendado marcar";
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(288, 39);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 1;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // gbEspelhar
            // 
            this.gbEspelhar.Controls.Add(this.chkChecklistVenda);
            this.gbEspelhar.Controls.Add(this.chkFormularioQualidade);
            this.gbEspelhar.Controls.Add(this.chkPlanosComparacao);
            this.gbEspelhar.Controls.Add(this.chkVariaveis);
            this.gbEspelhar.Controls.Add(this.chkFaq);
            this.gbEspelhar.Controls.Add(this.chkAparelhos);
            this.gbEspelhar.Location = new System.Drawing.Point(7, 119);
            this.gbEspelhar.Name = "gbEspelhar";
            this.gbEspelhar.Size = new System.Drawing.Size(331, 88);
            this.gbEspelhar.TabIndex = 47;
            this.gbEspelhar.TabStop = false;
            this.gbEspelhar.Text = "Espelhar";
            // 
            // chkChecklistVenda
            // 
            this.chkChecklistVenda.AutoSize = true;
            this.chkChecklistVenda.Location = new System.Drawing.Point(7, 65);
            this.chkChecklistVenda.Name = "chkChecklistVenda";
            this.chkChecklistVenda.Size = new System.Drawing.Size(118, 17);
            this.chkChecklistVenda.TabIndex = 2;
            this.chkChecklistVenda.Text = "Checklist de Venda";
            this.chkChecklistVenda.UseVisualStyleBackColor = true;
            // 
            // chkFormularioQualidade
            // 
            this.chkFormularioQualidade.AutoSize = true;
            this.chkFormularioQualidade.Location = new System.Drawing.Point(180, 42);
            this.chkFormularioQualidade.Name = "chkFormularioQualidade";
            this.chkFormularioQualidade.Size = new System.Drawing.Size(145, 17);
            this.chkFormularioQualidade.TabIndex = 4;
            this.chkFormularioQualidade.Text = "Formulários de Qualidade";
            this.chkFormularioQualidade.UseVisualStyleBackColor = true;
            // 
            // chkPlanosComparacao
            // 
            this.chkPlanosComparacao.AutoSize = true;
            this.chkPlanosComparacao.Location = new System.Drawing.Point(180, 20);
            this.chkPlanosComparacao.Name = "chkPlanosComparacao";
            this.chkPlanosComparacao.Size = new System.Drawing.Size(145, 17);
            this.chkPlanosComparacao.TabIndex = 3;
            this.chkPlanosComparacao.Text = "Planos para Comparação";
            this.chkPlanosComparacao.UseVisualStyleBackColor = true;
            // 
            // chkVariaveis
            // 
            this.chkVariaveis.AutoSize = true;
            this.chkVariaveis.Location = new System.Drawing.Point(7, 43);
            this.chkVariaveis.Name = "chkVariaveis";
            this.chkVariaveis.Size = new System.Drawing.Size(121, 17);
            this.chkVariaveis.TabIndex = 1;
            this.chkVariaveis.Text = "Variáveis do Script *";
            this.chkVariaveis.UseVisualStyleBackColor = true;
            // 
            // chkFaq
            // 
            this.chkFaq.AutoSize = true;
            this.chkFaq.Location = new System.Drawing.Point(180, 65);
            this.chkFaq.Name = "chkFaq";
            this.chkFaq.Size = new System.Drawing.Size(121, 17);
            this.chkFaq.TabIndex = 5;
            this.chkFaq.Text = "Faq de Atendimento";
            this.chkFaq.UseVisualStyleBackColor = true;
            // 
            // chkAparelhos
            // 
            this.chkAparelhos.AutoSize = true;
            this.chkAparelhos.Location = new System.Drawing.Point(7, 20);
            this.chkAparelhos.Name = "chkAparelhos";
            this.chkAparelhos.Size = new System.Drawing.Size(142, 17);
            this.chkAparelhos.TabIndex = 0;
            this.chkAparelhos.Text = "Aparelhos da Campanha";
            this.chkAparelhos.UseVisualStyleBackColor = true;
            // 
            // lblExemploPasta
            // 
            this.lblExemploPasta.AutoSize = true;
            this.lblExemploPasta.ForeColor = System.Drawing.Color.Red;
            this.lblExemploPasta.Location = new System.Drawing.Point(240, 63);
            this.lblExemploPasta.Name = "lblExemploPasta";
            this.lblExemploPasta.Size = new System.Drawing.Size(98, 13);
            this.lblExemploPasta.TabIndex = 46;
            this.lblExemploPasta.Text = "Ex: Claro Aquisicao";
            // 
            // txtEnderecoImportacao
            // 
            this.txtEnderecoImportacao.Location = new System.Drawing.Point(6, 79);
            this.txtEnderecoImportacao.MaxLength = 200;
            this.txtEnderecoImportacao.Name = "txtEnderecoImportacao";
            this.txtEnderecoImportacao.Size = new System.Drawing.Size(332, 20);
            this.txtEnderecoImportacao.TabIndex = 2;
            this.txtEnderecoImportacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnderecoImportacao_KeyPress);
            // 
            // lblEnderecoDeImportacao
            // 
            this.lblEnderecoDeImportacao.AutoSize = true;
            this.lblEnderecoDeImportacao.Location = new System.Drawing.Point(3, 63);
            this.lblEnderecoDeImportacao.Name = "lblEnderecoDeImportacao";
            this.lblEnderecoDeImportacao.Size = new System.Drawing.Size(165, 13);
            this.lblEnderecoDeImportacao.TabIndex = 44;
            this.lblEnderecoDeImportacao.Text = "Pasta para Importação de Mailing";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(6, 37);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(276, 20);
            this.txtNome.TabIndex = 0;
            this.txtNome.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNome_KeyPress);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(3, 21);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(38, 13);
            this.lblNome.TabIndex = 42;
            this.lblNome.Text = "Nome ";
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
            this.btnFechar.Location = new System.Drawing.Point(264, 257);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(93, 25);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "Fechar   ";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Visible = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(249, 25);
            this.lblTitulo.TabIndex = 36;
            this.lblTitulo.Text = "ESPELHAR CAMPANHA";
            // 
            // CampanhaEspelhoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(369, 292);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.gbCampanhaEspelho);
            this.Controls.Add(this.btnSalvar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CampanhaEspelhoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Campanha Espelho";
            this.Load += new System.EventHandler(this.CampanhaEspelho_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CampanhaEspelhoForm_KeyPress);
            this.gbCampanhaEspelho.ResumeLayout(false);
            this.gbCampanhaEspelho.PerformLayout();
            this.gbEspelhar.ResumeLayout(false);
            this.gbEspelhar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.GroupBox gbCampanhaEspelho;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtEnderecoImportacao;
        private System.Windows.Forms.Label lblEnderecoDeImportacao;
        private System.Windows.Forms.Label lblExemploPasta;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.GroupBox gbEspelhar;
        private System.Windows.Forms.CheckBox chkPlanosComparacao;
        private System.Windows.Forms.CheckBox chkVariaveis;
        private System.Windows.Forms.CheckBox chkFaq;
        private System.Windows.Forms.CheckBox chkAparelhos;
        private System.Windows.Forms.CheckBox chkChecklistVenda;
        private System.Windows.Forms.CheckBox chkFormularioQualidade;
        private System.Windows.Forms.Label lblInfo;
    }
}