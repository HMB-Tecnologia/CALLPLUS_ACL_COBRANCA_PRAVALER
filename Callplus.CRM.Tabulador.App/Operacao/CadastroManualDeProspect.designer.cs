﻿namespace Callplus.CRM.Tabulador.App.Operacao
{
    partial class CadastroManualDeProspect
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroManualDeProspect));
			this.lblNome = new System.Windows.Forms.Label();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.lblTelefone01 = new System.Windows.Forms.Label();
			this.txtTelefone01 = new System.Windows.Forms.TextBox();
			this.lblTelefone02 = new System.Windows.Forms.Label();
			this.txtTelefone02 = new System.Windows.Forms.TextBox();
			this.lblTelefone03 = new System.Windows.Forms.Label();
			this.txtTelefone03 = new System.Windows.Forms.TextBox();
			this.btnSalvar = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbCampanha = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCPF = new System.Windows.Forms.TextBox();
			this.lblDdd = new System.Windows.Forms.Label();
			this.cmbDdd = new System.Windows.Forms.ComboBox();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblNome
			// 
			this.lblNome.AutoSize = true;
			this.lblNome.Location = new System.Drawing.Point(135, 16);
			this.lblNome.Name = "lblNome";
			this.lblNome.Size = new System.Drawing.Size(85, 13);
			this.lblNome.TabIndex = 0;
			this.lblNome.Text = "Nome do Cliente";
			// 
			// txtNome
			// 
			this.txtNome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtNome.Location = new System.Drawing.Point(138, 33);
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(271, 20);
			this.txtNome.TabIndex = 1;
			// 
			// lblTelefone01
			// 
			this.lblTelefone01.AutoSize = true;
			this.lblTelefone01.Location = new System.Drawing.Point(104, 56);
			this.lblTelefone01.Name = "lblTelefone01";
			this.lblTelefone01.Size = new System.Drawing.Size(58, 13);
			this.lblTelefone01.TabIndex = 6;
			this.lblTelefone01.Text = "Telefone 1";
			// 
			// txtTelefone01
			// 
			this.txtTelefone01.Location = new System.Drawing.Point(107, 71);
			this.txtTelefone01.Name = "txtTelefone01";
			this.txtTelefone01.Size = new System.Drawing.Size(148, 20);
			this.txtTelefone01.TabIndex = 2;
			this.txtTelefone01.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefone01_KeyPress);
			// 
			// lblTelefone02
			// 
			this.lblTelefone02.AutoSize = true;
			this.lblTelefone02.Location = new System.Drawing.Point(258, 55);
			this.lblTelefone02.Name = "lblTelefone02";
			this.lblTelefone02.Size = new System.Drawing.Size(58, 13);
			this.lblTelefone02.TabIndex = 6;
			this.lblTelefone02.Text = "Telefone 2";
			// 
			// txtTelefone02
			// 
			this.txtTelefone02.Location = new System.Drawing.Point(261, 71);
			this.txtTelefone02.Name = "txtTelefone02";
			this.txtTelefone02.Size = new System.Drawing.Size(148, 20);
			this.txtTelefone02.TabIndex = 3;
			this.txtTelefone02.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefone02_KeyPress);
			// 
			// lblTelefone03
			// 
			this.lblTelefone03.AutoSize = true;
			this.lblTelefone03.Location = new System.Drawing.Point(412, 55);
			this.lblTelefone03.Name = "lblTelefone03";
			this.lblTelefone03.Size = new System.Drawing.Size(58, 13);
			this.lblTelefone03.TabIndex = 6;
			this.lblTelefone03.Text = "Telefone 3";
			// 
			// txtTelefone03
			// 
			this.txtTelefone03.Location = new System.Drawing.Point(415, 71);
			this.txtTelefone03.Name = "txtTelefone03";
			this.txtTelefone03.Size = new System.Drawing.Size(148, 20);
			this.txtTelefone03.TabIndex = 4;
			this.txtTelefone03.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefone03_KeyPress);
			// 
			// btnSalvar
			// 
			this.btnSalvar.Image = global::Callplus.CRM.Tabulador.App.Properties.Resources.save;
			this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSalvar.Location = new System.Drawing.Point(12, 148);
			this.btnSalvar.Name = "btnSalvar";
			this.btnSalvar.Size = new System.Drawing.Size(90, 25);
			this.btnSalvar.TabIndex = 8;
			this.btnSalvar.Text = "Salvar";
			this.btnSalvar.UseVisualStyleBackColor = true;
			this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Campanha";
			// 
			// cmbCampanha
			// 
			this.cmbCampanha.Enabled = false;
			this.cmbCampanha.FormattingEnabled = true;
			this.cmbCampanha.Location = new System.Drawing.Point(9, 32);
			this.cmbCampanha.Name = "cmbCampanha";
			this.cmbCampanha.Size = new System.Drawing.Size(125, 21);
			this.cmbCampanha.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtCPF);
			this.groupBox1.Controls.Add(this.lblDdd);
			this.groupBox1.Controls.Add(this.cmbDdd);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lblNome);
			this.groupBox1.Controls.Add(this.cmbCampanha);
			this.groupBox1.Controls.Add(this.txtNome);
			this.groupBox1.Controls.Add(this.txtTelefone03);
			this.groupBox1.Controls.Add(this.txtTelefone02);
			this.groupBox1.Controls.Add(this.txtTelefone01);
			this.groupBox1.Controls.Add(this.lblTelefone01);
			this.groupBox1.Controls.Add(this.lblTelefone03);
			this.groupBox1.Controls.Add(this.lblTelefone02);
			this.groupBox1.Location = new System.Drawing.Point(12, 37);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(570, 105);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Dados do Cliente";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(412, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 13);
			this.label2.TabIndex = 23;
			this.label2.Text = "CPF";
			// 
			// txtCPF
			// 
			this.txtCPF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtCPF.Location = new System.Drawing.Point(415, 34);
			this.txtCPF.Name = "txtCPF";
			this.txtCPF.Size = new System.Drawing.Size(148, 20);
			this.txtCPF.TabIndex = 24;
			// 
			// lblDdd
			// 
			this.lblDdd.AutoSize = true;
			this.lblDdd.Location = new System.Drawing.Point(7, 56);
			this.lblDdd.Name = "lblDdd";
			this.lblDdd.Size = new System.Drawing.Size(76, 13);
			this.lblDdd.TabIndex = 21;
			this.lblDdd.Text = "DDD Ativação";
			// 
			// cmbDdd
			// 
			this.cmbDdd.FormattingEnabled = true;
			this.cmbDdd.Items.AddRange(new object[] {
            "SELECIONE...",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "24",
            "31",
            "32",
            "33",
            "34",
            "35",
            "37",
            "38",
            "27",
            "28",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "51",
            "53",
            "54",
            "55",
            "68",
            "96",
            "92",
            "97",
            "91",
            "93",
            "94",
            "69",
            "95",
            "63",
            "61",
            "62",
            "64",
            "65",
            "66",
            "67",
            "82",
            "71",
            "73",
            "74",
            "75",
            "77",
            "85",
            "88",
            "98",
            "99",
            "83",
            "81",
            "87",
            "86",
            "89",
            "84",
            "79"});
			this.cmbDdd.Location = new System.Drawing.Point(9, 70);
			this.cmbDdd.MaxLength = 2;
			this.cmbDdd.Name = "cmbDdd";
			this.cmbDdd.Size = new System.Drawing.Size(92, 21);
			this.cmbDdd.TabIndex = 22;
			// 
			// lblTitulo
			// 
			this.lblTitulo.AutoSize = true;
			this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitulo.Location = new System.Drawing.Point(12, 9);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(222, 25);
			this.lblTitulo.TabIndex = 7;
			this.lblTitulo.Text = "CADASTRO MANUAL";
			// 
			// CadastroManualDeProspect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(596, 183);
			this.Controls.Add(this.lblTitulo);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSalvar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CadastroManualDeProspect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cadastro Novo Cliente";
			this.Load += new System.EventHandler(this.CadastroManualDeProspect_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblTelefone01;
        private System.Windows.Forms.TextBox txtTelefone01;
        private System.Windows.Forms.Label lblTelefone02;
        private System.Windows.Forms.TextBox txtTelefone02;
        private System.Windows.Forms.Label lblTelefone03;
        private System.Windows.Forms.TextBox txtTelefone03;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCampanha;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDdd;
        private System.Windows.Forms.ComboBox cmbDdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCPF;
    }
}