namespace Callplus.CRM.Administracao.App.Qualidade.ScriptDeAtendimento
{
    partial class SimuladorScriptAtendimentoForm
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
            this.scriptDeOfertaControl = new Callplus.CRM.Administracao.App.Controles.ScriptDeOfertaControl();
            this.SuspendLayout();
            // 
            // scriptDeOfertaControl
            // 
            this.scriptDeOfertaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptDeOfertaControl.Location = new System.Drawing.Point(0, 0);
            this.scriptDeOfertaControl.Name = "scriptDeOfertaControl";
            this.scriptDeOfertaControl.Size = new System.Drawing.Size(1058, 404);
            this.scriptDeOfertaControl.TabIndex = 0;
            this.scriptDeOfertaControl.Visible = false;
            // 
            // SimuladorScriptAtendimentoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 404);
            this.Controls.Add(this.scriptDeOfertaControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SimuladorScriptAtendimentoForm";
            this.Text = "Simulador De Script de Atendimento";
            this.Load += new System.EventHandler(this.SimuladorScriptAtendimentoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controles.ScriptDeOfertaControl scriptDeOfertaControl;
    }
}