namespace Callplus.CRM.Tabulador.App.IntegracaoDiscador.integracaoHuawei
{
    partial class fCopiaDadosHuaweiManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCopiaDadosHuaweiManual));
            this.txtDadosHuawei = new System.Windows.Forms.TextBox();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDadosHuawei
            // 
            this.txtDadosHuawei.Location = new System.Drawing.Point(12, 12);
            this.txtDadosHuawei.Multiline = true;
            this.txtDadosHuawei.Name = "txtDadosHuawei";
            this.txtDadosHuawei.Size = new System.Drawing.Size(460, 279);
            this.txtDadosHuawei.TabIndex = 2;
            // 
            // btnProcessar
            // 
            this.btnProcessar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.check;
            this.btnProcessar.Location = new System.Drawing.Point(12, 297);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(88, 33);
            this.btnProcessar.TabIndex = 3;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click_1);
            // 
            // fCopiaDadosHuaweiManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 340);
            this.Controls.Add(this.btnProcessar);
            this.Controls.Add(this.txtDadosHuawei);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fCopiaDadosHuaweiManual";
            this.Text = "Registrar dados HUAWEI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDadosHuawei;
        private System.Windows.Forms.Button btnProcessar;
    }
}