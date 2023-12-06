namespace Callplus.CRM.Administracao.App.Backoffice.StatusDeAuditoria
{
    partial class StatusDeAuditoriaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusDeAuditoriaForm));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.clbCampanhas = new System.Windows.Forms.CheckedListBox();
            this.lnkNenhum = new System.Windows.Forms.LinkLabel();
            this.lnkTodos = new System.Windows.Forms.LinkLabel();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.chkAprovaOferta = new System.Windows.Forms.CheckBox();
            this.chkPerminitoHumano = new System.Windows.Forms.CheckBox();
            this.chkTrocaStatus = new System.Windows.Forms.CheckBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAuditoriaOperador = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(248, 25);
            this.lblTitulo.TabIndex = 12;
            this.lblTitulo.Text = "STATUS DE AUDITORIA";
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
            this.btnSalvar.Location = new System.Drawing.Point(17, 318);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "Salvar  ";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // clbCampanhas
            // 
            this.clbCampanhas.CheckOnClick = true;
            this.clbCampanhas.FormattingEnabled = true;
            this.clbCampanhas.Location = new System.Drawing.Point(17, 128);
            this.clbCampanhas.Name = "clbCampanhas";
            this.clbCampanhas.Size = new System.Drawing.Size(535, 184);
            this.clbCampanhas.TabIndex = 7;
            // 
            // lnkNenhum
            // 
            this.lnkNenhum.AutoSize = true;
            this.lnkNenhum.Location = new System.Drawing.Point(505, 112);
            this.lnkNenhum.Name = "lnkNenhum";
            this.lnkNenhum.Size = new System.Drawing.Size(47, 13);
            this.lnkNenhum.TabIndex = 6;
            this.lnkNenhum.TabStop = true;
            this.lnkNenhum.Text = "Nenhum";
            this.lnkNenhum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNenhum_LinkClicked);
            // 
            // lnkTodos
            // 
            this.lnkTodos.AutoSize = true;
            this.lnkTodos.Location = new System.Drawing.Point(462, 112);
            this.lnkTodos.Name = "lnkTodos";
            this.lnkTodos.Size = new System.Drawing.Size(37, 13);
            this.lnkTodos.TabIndex = 5;
            this.lnkTodos.TabStop = true;
            this.lnkTodos.Text = "Todos";
            this.lnkTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTodos_LinkClicked);
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAtivo.Location = new System.Drawing.Point(384, 81);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 4;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // chkAprovaOferta
            // 
            this.chkAprovaOferta.AutoSize = true;
            this.chkAprovaOferta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAprovaOferta.Location = new System.Drawing.Point(168, 81);
            this.chkAprovaOferta.Name = "chkAprovaOferta";
            this.chkAprovaOferta.Size = new System.Drawing.Size(92, 17);
            this.chkAprovaOferta.TabIndex = 2;
            this.chkAprovaOferta.Text = "Aprova Oferta";
            this.chkAprovaOferta.UseVisualStyleBackColor = true;
            // 
            // chkPerminitoHumano
            // 
            this.chkPerminitoHumano.AutoSize = true;
            this.chkPerminitoHumano.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPerminitoHumano.Location = new System.Drawing.Point(266, 81);
            this.chkPerminitoHumano.Name = "chkPerminitoHumano";
            this.chkPerminitoHumano.Size = new System.Drawing.Size(112, 17);
            this.chkPerminitoHumano.TabIndex = 3;
            this.chkPerminitoHumano.Text = "Permitido Humano";
            this.chkPerminitoHumano.UseVisualStyleBackColor = true;
            // 
            // chkTrocaStatus
            // 
            this.chkTrocaStatus.AutoSize = true;
            this.chkTrocaStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrocaStatus.Location = new System.Drawing.Point(17, 81);
            this.chkTrocaStatus.Name = "chkTrocaStatus";
            this.chkTrocaStatus.Size = new System.Drawing.Size(140, 17);
            this.chkTrocaStatus.TabIndex = 1;
            this.chkTrocaStatus.Text = "Habilita Troca de Status";
            this.chkTrocaStatus.UseVisualStyleBackColor = true;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(17, 55);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(532, 20);
            this.txtNome.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Campanhas";
            // 
            // chkAuditoriaOperador
            // 
            this.chkAuditoriaOperador.AutoSize = true;
            this.chkAuditoriaOperador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAuditoriaOperador.Location = new System.Drawing.Point(440, 81);
            this.chkAuditoriaOperador.Name = "chkAuditoriaOperador";
            this.chkAuditoriaOperador.Size = new System.Drawing.Size(114, 17);
            this.chkAuditoriaOperador.TabIndex = 58;
            this.chkAuditoriaOperador.Text = "Auditoria Operador";
            this.chkAuditoriaOperador.UseVisualStyleBackColor = true;
            // 
            // StatusDeAuditoriaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 355);
            this.Controls.Add(this.chkAuditoriaOperador);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkAtivo);
            this.Controls.Add(this.chkAprovaOferta);
            this.Controls.Add(this.chkPerminitoHumano);
            this.Controls.Add(this.chkTrocaStatus);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.clbCampanhas);
            this.Controls.Add(this.lnkNenhum);
            this.Controls.Add(this.lnkTodos);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatusDeAuditoriaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Status De Auditoria";
            this.Load += new System.EventHandler(this.StatusDeAuditoriaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.CheckedListBox clbCampanhas;
        private System.Windows.Forms.LinkLabel lnkNenhum;
        private System.Windows.Forms.LinkLabel lnkTodos;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.CheckBox chkAprovaOferta;
        private System.Windows.Forms.CheckBox chkPerminitoHumano;
        private System.Windows.Forms.CheckBox chkTrocaStatus;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAuditoriaOperador;
    }
}