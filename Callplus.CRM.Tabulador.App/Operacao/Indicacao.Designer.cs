namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class IndicacaoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndicacaoForm));
            this.label36 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txtQuantidadeDeIndicacoes = new System.Windows.Forms.TextBox();
            this.lblIndicacacao = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsOferta_bntPararTempo_Click = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(12, 32);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(70, 13);
            this.label36.TabIndex = 4;
            this.label36.Text = "Observações";
            // 
            // txtObservacao
            // 
            this.txtObservacao.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtObservacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservacao.Location = new System.Drawing.Point(12, 48);
            this.txtObservacao.MaxLength = 300;
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(423, 59);
            this.txtObservacao.TabIndex = 5;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.save;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.Location = new System.Drawing.Point(360, 122);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 24);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar    ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtQuantidadeDeIndicacoes
            // 
            this.txtQuantidadeDeIndicacoes.Location = new System.Drawing.Point(12, 126);
            this.txtQuantidadeDeIndicacoes.Name = "txtQuantidadeDeIndicacoes";
            this.txtQuantidadeDeIndicacoes.Size = new System.Drawing.Size(129, 20);
            this.txtQuantidadeDeIndicacoes.TabIndex = 7;
            this.txtQuantidadeDeIndicacoes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidadeDeIndicacoes_KeyPress);
            // 
            // lblIndicacacao
            // 
            this.lblIndicacacao.AutoSize = true;
            this.lblIndicacacao.Location = new System.Drawing.Point(9, 110);
            this.lblIndicacacao.Name = "lblIndicacacao";
            this.lblIndicacacao.Size = new System.Drawing.Size(132, 13);
            this.lblIndicacacao.TabIndex = 4;
            this.lblIndicacacao.Text = "Quantidade de Indicações";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOferta_bntPararTempo_Click});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(451, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsOferta_bntPararTempo_Click
            // 
            this.tsOferta_bntPararTempo_Click.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.cancel;
            this.tsOferta_bntPararTempo_Click.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsOferta_bntPararTempo_Click.Name = "tsOferta_bntPararTempo_Click";
            this.tsOferta_bntPararTempo_Click.Size = new System.Drawing.Size(93, 22);
            this.tsOferta_bntPararTempo_Click.Text = "Parar Tempo ";
            this.tsOferta_bntPararTempo_Click.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsOferta_bntPararTempo_Click.Click += new System.EventHandler(this.tsOferta_bntPararTempo_Click_Click);
            // 
            // IndicacaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(451, 158);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtQuantidadeDeIndicacoes);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblIndicacacao);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.txtObservacao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndicacaoForm";
            this.Text = "Dados Adicionais";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtQuantidadeDeIndicacoes;
        private System.Windows.Forms.Label lblIndicacacao;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tsOferta_bntPararTempo_Click;
    }
}