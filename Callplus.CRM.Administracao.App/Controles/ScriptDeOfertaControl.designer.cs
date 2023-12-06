namespace Callplus.CRM.Administracao.App.Controles
{
    partial class ScriptDeOfertaControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowserEtapa = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnVoltar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblResposta = new System.Windows.Forms.ToolStripLabel();
            this.tsComboRespostas = new System.Windows.Forms.ToolStripComboBox();
            this.toolSeparatorResposta = new System.Windows.Forms.ToolStripSeparator();
            this.btnAvancar = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowserEtapa);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 324);
            this.panel1.TabIndex = 0;
            // 
            // webBrowserEtapa
            // 
            this.webBrowserEtapa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserEtapa.Location = new System.Drawing.Point(0, 0);
            this.webBrowserEtapa.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserEtapa.Name = "webBrowserEtapa";
            this.webBrowserEtapa.Size = new System.Drawing.Size(781, 299);
            this.webBrowserEtapa.TabIndex = 9;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnVoltar,
            this.toolStripSeparator2,
            this.lblResposta,
            this.tsComboRespostas,
            this.toolSeparatorResposta,
            this.btnAvancar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 299);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(781, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnVoltar
            // 
            this.btnVoltar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnVoltar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(41, 22);
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblResposta
            // 
            this.lblResposta.Name = "lblResposta";
            this.lblResposta.Size = new System.Drawing.Size(57, 22);
            this.lblResposta.Text = "Resposta:";
            // 
            // tsComboRespostas
            // 
            this.tsComboRespostas.Name = "tsComboRespostas";
            this.tsComboRespostas.Size = new System.Drawing.Size(121, 25);
            // 
            // toolSeparatorResposta
            // 
            this.toolSeparatorResposta.Name = "toolSeparatorResposta";
            this.toolSeparatorResposta.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAvancar
            // 
            this.btnAvancar.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnAvancar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAvancar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.Size = new System.Drawing.Size(54, 22);
            this.btnAvancar.Text = "Avançar";
            this.btnAvancar.Click += new System.EventHandler(this.btnAvancar_Click);
            // 
            // ScriptDeOfertaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ScriptDeOfertaControl";
            this.Size = new System.Drawing.Size(781, 324);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowserEtapa;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAvancar;
        private System.Windows.Forms.ToolStripButton btnVoltar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblResposta;
        private System.Windows.Forms.ToolStripComboBox tsComboRespostas;
        private System.Windows.Forms.ToolStripSeparator toolSeparatorResposta;
    }
}
