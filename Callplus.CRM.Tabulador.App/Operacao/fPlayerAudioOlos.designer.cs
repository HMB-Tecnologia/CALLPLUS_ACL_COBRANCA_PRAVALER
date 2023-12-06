namespace v1Tabulare_z13.operador
{
    partial class fPlayerAudioOlos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPlayerAudioOlos));
            this.btnPlayResume = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timerDuracao = new System.Windows.Forms.Timer(this.components);
            this.timerAtualizarTela = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnPlayResume
            // 
            this.btnPlayResume.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayResume.Image")));
            this.btnPlayResume.Location = new System.Drawing.Point(12, 12);
            this.btnPlayResume.Name = "btnPlayResume";
            this.btnPlayResume.Size = new System.Drawing.Size(75, 23);
            this.btnPlayResume.TabIndex = 0;
            this.btnPlayResume.UseVisualStyleBackColor = true;
            this.btnPlayResume.Click += new System.EventHandler(this.btnPlayResume_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(93, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(174, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timerDuracao
            // 
            this.timerDuracao.Interval = 1000;
            this.timerDuracao.Tick += new System.EventHandler(this.timerDuracao_Tick);
            // 
            // timerAtualizarTela
            // 
            this.timerAtualizarTela.Interval = 1000;
            this.timerAtualizarTela.Tick += new System.EventHandler(this.timerAtualizarTela_Tick);
            // 
            // fPlayerAudioOlos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 47);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlayResume);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPlayerAudioOlos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reproduzir";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fPlayerAudioOlos_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlayResume;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timerDuracao;
        private System.Windows.Forms.Timer timerAtualizarTela;
    }
}