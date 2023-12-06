namespace Callplus.CRM.Administracao.App.Planejamento.Campanha
{
    partial class CampanhaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampanhaForm));
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.cmbScriptApresentacao = new System.Windows.Forms.ComboBox();
            this.lblScript = new System.Windows.Forms.Label();
            this.cmbTipoDeDiscagem = new System.Windows.Forms.ComboBox();
            this.lblCampanha = new System.Windows.Forms.Label();
            this.cmbDiscador = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.cmbScriptFinalizacao = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLayoutCampoDinamicoOperacao = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLayoutCampoDinamicoBko = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStatusAutomaticoAceite = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStatusAutomatico = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAfterCall = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMetaDeVenda = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEnderecoInputMailing = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tcCampanha = new System.Windows.Forms.TabControl();
            this.tcCampanha_tpConfiguracoes = new System.Windows.Forms.TabPage();
            this.pnlOpcoes2 = new System.Windows.Forms.Panel();
            this.pnlOpcoes1 = new System.Windows.Forms.Panel();
            this.chkHabilitaRevenda = new System.Windows.Forms.CheckBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.chkCepExpress = new System.Windows.Forms.CheckBox();
            this.chkHabilitaPesquisa = new System.Windows.Forms.CheckBox();
            this.chkHistorico = new System.Windows.Forms.CheckBox();
            this.chkDiscagemManual = new System.Windows.Forms.CheckBox();
            this.chkIndicacao = new System.Windows.Forms.CheckBox();
            this.chkSimulador = new System.Windows.Forms.CheckBox();
            this.chkCadastroManual = new System.Windows.Forms.CheckBox();
            this.tcCampanha_tpBancos = new System.Windows.Forms.TabPage();
            this.clbBanco = new System.Windows.Forms.CheckedListBox();
            this.tcCampanha_tpFormaPagamento = new System.Windows.Forms.TabPage();
            this.clbFormaPagamento = new System.Windows.Forms.CheckedListBox();
            this.tcCampanha_StatusAtendimento = new System.Windows.Forms.TabPage();
            this.clbStatusDeAtendimento = new System.Windows.Forms.CheckedListBox();
            this.tcCampanha_tpStatusOferta = new System.Windows.Forms.TabPage();
            this.clbStatusDeOferta = new System.Windows.Forms.CheckedListBox();
            this.tcCampanha_tpStatusAuditoria = new System.Windows.Forms.TabPage();
            this.clbStatusDeAuditoria = new System.Windows.Forms.CheckedListBox();
            this.tcCampanha_tpVarievalScript = new System.Windows.Forms.TabPage();
            this.clbVariavelScript = new System.Windows.Forms.CheckedListBox();
            this.cmbMailingIndicacao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTipoCampanha = new System.Windows.Forms.ComboBox();
            this.lblTipoCampanha = new System.Windows.Forms.Label();
            this.lnkNenhum = new System.Windows.Forms.LinkLabel();
            this.lnkTodos = new System.Windows.Forms.LinkLabel();
            this.chkIdTipoDeAuditoria = new System.Windows.Forms.CheckBox();
            this.tcCampanha.SuspendLayout();
            this.tcCampanha_tpConfiguracoes.SuspendLayout();
            this.pnlOpcoes2.SuspendLayout();
            this.pnlOpcoes1.SuspendLayout();
            this.tcCampanha_tpBancos.SuspendLayout();
            this.tcCampanha_tpFormaPagamento.SuspendLayout();
            this.tcCampanha_StatusAtendimento.SuspendLayout();
            this.tcCampanha_tpStatusOferta.SuspendLayout();
            this.tcCampanha_tpStatusAuditoria.SuspendLayout();
            this.tcCampanha_tpVarievalScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(11, 57);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(215, 20);
            this.txtNome.TabIndex = 1;
            this.txtNome.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNome_KeyPress);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(8, 41);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 40;
            this.lblNome.Text = "Nome";
            // 
            // cmbScriptApresentacao
            // 
            this.cmbScriptApresentacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbScriptApresentacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbScriptApresentacao.FormattingEnabled = true;
            this.cmbScriptApresentacao.Location = new System.Drawing.Point(11, 96);
            this.cmbScriptApresentacao.Name = "cmbScriptApresentacao";
            this.cmbScriptApresentacao.Size = new System.Drawing.Size(350, 21);
            this.cmbScriptApresentacao.TabIndex = 7;
            // 
            // lblScript
            // 
            this.lblScript.AutoSize = true;
            this.lblScript.Location = new System.Drawing.Point(8, 80);
            this.lblScript.Name = "lblScript";
            this.lblScript.Size = new System.Drawing.Size(118, 13);
            this.lblScript.TabIndex = 38;
            this.lblScript.Text = "Script de Apresentação";
            // 
            // cmbTipoDeDiscagem
            // 
            this.cmbTipoDeDiscagem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbTipoDeDiscagem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTipoDeDiscagem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoDeDiscagem.FormattingEnabled = true;
            this.cmbTipoDeDiscagem.Items.AddRange(new object[] {
            "PREDITIVO"});
            this.cmbTipoDeDiscagem.Location = new System.Drawing.Point(478, 56);
            this.cmbTipoDeDiscagem.Name = "cmbTipoDeDiscagem";
            this.cmbTipoDeDiscagem.Size = new System.Drawing.Size(105, 21);
            this.cmbTipoDeDiscagem.TabIndex = 4;
            // 
            // lblCampanha
            // 
            this.lblCampanha.AutoSize = true;
            this.lblCampanha.Location = new System.Drawing.Point(475, 40);
            this.lblCampanha.Name = "lblCampanha";
            this.lblCampanha.Size = new System.Drawing.Size(93, 13);
            this.lblCampanha.TabIndex = 35;
            this.lblCampanha.Text = "Tipo de Discagem";
            // 
            // cmbDiscador
            // 
            this.cmbDiscador.FormattingEnabled = true;
            this.cmbDiscador.Location = new System.Drawing.Point(232, 56);
            this.cmbDiscador.Name = "cmbDiscador";
            this.cmbDiscador.Size = new System.Drawing.Size(129, 21);
            this.cmbDiscador.TabIndex = 2;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(229, 40);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(49, 13);
            this.lblTipo.TabIndex = 33;
            this.lblTipo.Text = "Discador";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 525);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Observações";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(12, 540);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(705, 60);
            this.txtObservacao.TabIndex = 25;
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
            this.btnSalvar.Location = new System.Drawing.Point(11, 606);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 25);
            this.btnSalvar.TabIndex = 26;
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
            this.lblTitulo.Size = new System.Drawing.Size(131, 25);
            this.lblTitulo.TabIndex = 29;
            this.lblTitulo.Text = "CAMPANHA";
            // 
            // cmbScriptFinalizacao
            // 
            this.cmbScriptFinalizacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbScriptFinalizacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbScriptFinalizacao.FormattingEnabled = true;
            this.cmbScriptFinalizacao.Location = new System.Drawing.Point(367, 96);
            this.cmbScriptFinalizacao.Name = "cmbScriptFinalizacao";
            this.cmbScriptFinalizacao.Size = new System.Drawing.Size(350, 21);
            this.cmbScriptFinalizacao.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Script de Finalização";
            // 
            // cmbLayoutCampoDinamicoOperacao
            // 
            this.cmbLayoutCampoDinamicoOperacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbLayoutCampoDinamicoOperacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLayoutCampoDinamicoOperacao.FormattingEnabled = true;
            this.cmbLayoutCampoDinamicoOperacao.Location = new System.Drawing.Point(11, 136);
            this.cmbLayoutCampoDinamicoOperacao.Name = "cmbLayoutCampoDinamicoOperacao";
            this.cmbLayoutCampoDinamicoOperacao.Size = new System.Drawing.Size(350, 21);
            this.cmbLayoutCampoDinamicoOperacao.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Layout de Campo Dinâmico Operação";
            // 
            // cmbLayoutCampoDinamicoBko
            // 
            this.cmbLayoutCampoDinamicoBko.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbLayoutCampoDinamicoBko.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLayoutCampoDinamicoBko.FormattingEnabled = true;
            this.cmbLayoutCampoDinamicoBko.Location = new System.Drawing.Point(367, 136);
            this.cmbLayoutCampoDinamicoBko.Name = "cmbLayoutCampoDinamicoBko";
            this.cmbLayoutCampoDinamicoBko.Size = new System.Drawing.Size(350, 21);
            this.cmbLayoutCampoDinamicoBko.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Layout de Campo Dinâmico Backoffice";
            // 
            // cmbStatusAutomaticoAceite
            // 
            this.cmbStatusAutomaticoAceite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbStatusAutomaticoAceite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStatusAutomaticoAceite.FormattingEnabled = true;
            this.cmbStatusAutomaticoAceite.Location = new System.Drawing.Point(11, 215);
            this.cmbStatusAutomaticoAceite.Name = "cmbStatusAutomaticoAceite";
            this.cmbStatusAutomaticoAceite.Size = new System.Drawing.Size(350, 21);
            this.cmbStatusAutomaticoAceite.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Status de Tabulação Automática com Aceite";
            // 
            // cmbStatusAutomatico
            // 
            this.cmbStatusAutomatico.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbStatusAutomatico.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStatusAutomatico.FormattingEnabled = true;
            this.cmbStatusAutomatico.Location = new System.Drawing.Point(367, 215);
            this.cmbStatusAutomatico.Name = "cmbStatusAutomatico";
            this.cmbStatusAutomatico.Size = new System.Drawing.Size(350, 21);
            this.cmbStatusAutomatico.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(364, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Status de Tabulação Automática";
            // 
            // txtAfterCall
            // 
            this.txtAfterCall.Location = new System.Drawing.Point(588, 57);
            this.txtAfterCall.MaxLength = 100;
            this.txtAfterCall.Name = "txtAfterCall";
            this.txtAfterCall.Size = new System.Drawing.Size(46, 20);
            this.txtAfterCall.TabIndex = 5;
            this.txtAfterCall.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAfterCall_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(585, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "After Call";
            // 
            // txtMetaDeVenda
            // 
            this.txtMetaDeVenda.Location = new System.Drawing.Point(640, 57);
            this.txtMetaDeVenda.MaxLength = 100;
            this.txtMetaDeVenda.Name = "txtMetaDeVenda";
            this.txtMetaDeVenda.Size = new System.Drawing.Size(77, 20);
            this.txtMetaDeVenda.TabIndex = 6;
            this.txtMetaDeVenda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMetaDeVenda_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(637, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Meta de Venda";
            // 
            // txtEnderecoInputMailing
            // 
            this.txtEnderecoInputMailing.Location = new System.Drawing.Point(367, 176);
            this.txtEnderecoInputMailing.MaxLength = 100;
            this.txtEnderecoInputMailing.Name = "txtEnderecoInputMailing";
            this.txtEnderecoInputMailing.ReadOnly = true;
            this.txtEnderecoInputMailing.Size = new System.Drawing.Size(350, 20);
            this.txtEnderecoInputMailing.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(364, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(175, 13);
            this.label10.TabIndex = 58;
            this.label10.Text = "Endereço de Importação de Mailing";
            // 
            // tcCampanha
            // 
            this.tcCampanha.Controls.Add(this.tcCampanha_tpConfiguracoes);
            this.tcCampanha.Controls.Add(this.tcCampanha_tpBancos);
            this.tcCampanha.Controls.Add(this.tcCampanha_tpFormaPagamento);
            this.tcCampanha.Controls.Add(this.tcCampanha_StatusAtendimento);
            this.tcCampanha.Controls.Add(this.tcCampanha_tpStatusOferta);
            this.tcCampanha.Controls.Add(this.tcCampanha_tpStatusAuditoria);
            this.tcCampanha.Controls.Add(this.tcCampanha_tpVarievalScript);
            this.tcCampanha.Location = new System.Drawing.Point(11, 258);
            this.tcCampanha.Name = "tcCampanha";
            this.tcCampanha.SelectedIndex = 0;
            this.tcCampanha.Size = new System.Drawing.Size(706, 265);
            this.tcCampanha.TabIndex = 22;
            this.tcCampanha.SelectedIndexChanged += new System.EventHandler(this.tcCampanha_SelectedIndexChanged);
            // 
            // tcCampanha_tpConfiguracoes
            // 
            this.tcCampanha_tpConfiguracoes.Controls.Add(this.pnlOpcoes2);
            this.tcCampanha_tpConfiguracoes.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_tpConfiguracoes.Name = "tcCampanha_tpConfiguracoes";
            this.tcCampanha_tpConfiguracoes.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_tpConfiguracoes.TabIndex = 6;
            this.tcCampanha_tpConfiguracoes.Text = "Configurações";
            this.tcCampanha_tpConfiguracoes.UseVisualStyleBackColor = true;
            // 
            // pnlOpcoes2
            // 
            this.pnlOpcoes2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOpcoes2.Controls.Add(this.pnlOpcoes1);
            this.pnlOpcoes2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOpcoes2.Location = new System.Drawing.Point(0, 0);
            this.pnlOpcoes2.Name = "pnlOpcoes2";
            this.pnlOpcoes2.Size = new System.Drawing.Size(698, 239);
            this.pnlOpcoes2.TabIndex = 23;
            // 
            // pnlOpcoes1
            // 
            this.pnlOpcoes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOpcoes1.Controls.Add(this.chkIdTipoDeAuditoria);
            this.pnlOpcoes1.Controls.Add(this.chkHabilitaRevenda);
            this.pnlOpcoes1.Controls.Add(this.chkAtivo);
            this.pnlOpcoes1.Controls.Add(this.chkCepExpress);
            this.pnlOpcoes1.Controls.Add(this.chkHabilitaPesquisa);
            this.pnlOpcoes1.Controls.Add(this.chkHistorico);
            this.pnlOpcoes1.Controls.Add(this.chkDiscagemManual);
            this.pnlOpcoes1.Controls.Add(this.chkIndicacao);
            this.pnlOpcoes1.Controls.Add(this.chkSimulador);
            this.pnlOpcoes1.Controls.Add(this.chkCadastroManual);
            this.pnlOpcoes1.Location = new System.Drawing.Point(3, 2);
            this.pnlOpcoes1.Name = "pnlOpcoes1";
            this.pnlOpcoes1.Size = new System.Drawing.Size(690, 232);
            this.pnlOpcoes1.TabIndex = 25;
            // 
            // chkHabilitaRevenda
            // 
            this.chkHabilitaRevenda.AutoSize = true;
            this.chkHabilitaRevenda.Location = new System.Drawing.Point(4, 118);
            this.chkHabilitaRevenda.Name = "chkHabilitaRevenda";
            this.chkHabilitaRevenda.Size = new System.Drawing.Size(108, 17);
            this.chkHabilitaRevenda.TabIndex = 23;
            this.chkHabilitaRevenda.Text = "Habilita Revenda";
            this.chkHabilitaRevenda.UseVisualStyleBackColor = true;
            this.chkHabilitaRevenda.Click += new System.EventHandler(this.chkHabilitaRevenda_Click);
            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(4, 153);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(50, 17);
            this.chkAtivo.TabIndex = 15;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            // 
            // chkCepExpress
            // 
            this.chkCepExpress.AutoSize = true;
            this.chkCepExpress.Location = new System.Drawing.Point(4, 102);
            this.chkCepExpress.Name = "chkCepExpress";
            this.chkCepExpress.Size = new System.Drawing.Size(123, 17);
            this.chkCepExpress.TabIndex = 22;
            this.chkCepExpress.Text = "Habilita Cep Express";
            this.chkCepExpress.UseVisualStyleBackColor = true;
            // 
            // chkHabilitaPesquisa
            // 
            this.chkHabilitaPesquisa.AutoSize = true;
            this.chkHabilitaPesquisa.Location = new System.Drawing.Point(4, 86);
            this.chkHabilitaPesquisa.Name = "chkHabilitaPesquisa";
            this.chkHabilitaPesquisa.Size = new System.Drawing.Size(107, 17);
            this.chkHabilitaPesquisa.TabIndex = 21;
            this.chkHabilitaPesquisa.Text = "Habilita Pesquisa";
            this.chkHabilitaPesquisa.UseVisualStyleBackColor = true;
            // 
            // chkHistorico
            // 
            this.chkHistorico.AutoSize = true;
            this.chkHistorico.Location = new System.Drawing.Point(4, 36);
            this.chkHistorico.Name = "chkHistorico";
            this.chkHistorico.Size = new System.Drawing.Size(105, 17);
            this.chkHistorico.TabIndex = 18;
            this.chkHistorico.Text = "Habilita Histórico";
            this.chkHistorico.UseVisualStyleBackColor = true;
            // 
            // chkDiscagemManual
            // 
            this.chkDiscagemManual.AutoSize = true;
            this.chkDiscagemManual.Location = new System.Drawing.Point(4, 3);
            this.chkDiscagemManual.Name = "chkDiscagemManual";
            this.chkDiscagemManual.Size = new System.Drawing.Size(149, 17);
            this.chkDiscagemManual.TabIndex = 16;
            this.chkDiscagemManual.Text = "Habilita Discagem Manual";
            this.chkDiscagemManual.UseVisualStyleBackColor = true;
            // 
            // chkIndicacao
            // 
            this.chkIndicacao.AutoSize = true;
            this.chkIndicacao.Location = new System.Drawing.Point(4, 52);
            this.chkIndicacao.Name = "chkIndicacao";
            this.chkIndicacao.Size = new System.Drawing.Size(111, 17);
            this.chkIndicacao.TabIndex = 19;
            this.chkIndicacao.Text = "Habilita Indicação";
            this.chkIndicacao.UseVisualStyleBackColor = true;
            // 
            // chkSimulador
            // 
            this.chkSimulador.AutoSize = true;
            this.chkSimulador.Location = new System.Drawing.Point(4, 69);
            this.chkSimulador.Name = "chkSimulador";
            this.chkSimulador.Size = new System.Drawing.Size(171, 17);
            this.chkSimulador.TabIndex = 20;
            this.chkSimulador.Text = "Habilita Comparador de Planos";
            this.chkSimulador.UseVisualStyleBackColor = true;
            // 
            // chkCadastroManual
            // 
            this.chkCadastroManual.AutoSize = true;
            this.chkCadastroManual.Location = new System.Drawing.Point(4, 20);
            this.chkCadastroManual.Name = "chkCadastroManual";
            this.chkCadastroManual.Size = new System.Drawing.Size(144, 17);
            this.chkCadastroManual.TabIndex = 17;
            this.chkCadastroManual.Text = "Habilita Cadastro Manual";
            this.chkCadastroManual.UseVisualStyleBackColor = true;
            // 
            // tcCampanha_tpBancos
            // 
            this.tcCampanha_tpBancos.Controls.Add(this.clbBanco);
            this.tcCampanha_tpBancos.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_tpBancos.Name = "tcCampanha_tpBancos";
            this.tcCampanha_tpBancos.Padding = new System.Windows.Forms.Padding(3);
            this.tcCampanha_tpBancos.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_tpBancos.TabIndex = 0;
            this.tcCampanha_tpBancos.Text = "Bancos";
            this.tcCampanha_tpBancos.UseVisualStyleBackColor = true;
            // 
            // clbBanco
            // 
            this.clbBanco.CheckOnClick = true;
            this.clbBanco.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbBanco.FormattingEnabled = true;
            this.clbBanco.Location = new System.Drawing.Point(3, 3);
            this.clbBanco.Name = "clbBanco";
            this.clbBanco.Size = new System.Drawing.Size(692, 233);
            this.clbBanco.TabIndex = 0;
            this.clbBanco.SelectedValueChanged += new System.EventHandler(this.clbBanco_SelectedValueChanged);
            // 
            // tcCampanha_tpFormaPagamento
            // 
            this.tcCampanha_tpFormaPagamento.Controls.Add(this.clbFormaPagamento);
            this.tcCampanha_tpFormaPagamento.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_tpFormaPagamento.Name = "tcCampanha_tpFormaPagamento";
            this.tcCampanha_tpFormaPagamento.Padding = new System.Windows.Forms.Padding(3);
            this.tcCampanha_tpFormaPagamento.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_tpFormaPagamento.TabIndex = 1;
            this.tcCampanha_tpFormaPagamento.Text = "Formas de Pagamento";
            this.tcCampanha_tpFormaPagamento.UseVisualStyleBackColor = true;
            // 
            // clbFormaPagamento
            // 
            this.clbFormaPagamento.CheckOnClick = true;
            this.clbFormaPagamento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbFormaPagamento.FormattingEnabled = true;
            this.clbFormaPagamento.Location = new System.Drawing.Point(3, 3);
            this.clbFormaPagamento.Name = "clbFormaPagamento";
            this.clbFormaPagamento.Size = new System.Drawing.Size(692, 233);
            this.clbFormaPagamento.TabIndex = 1;
            // 
            // tcCampanha_StatusAtendimento
            // 
            this.tcCampanha_StatusAtendimento.Controls.Add(this.clbStatusDeAtendimento);
            this.tcCampanha_StatusAtendimento.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_StatusAtendimento.Name = "tcCampanha_StatusAtendimento";
            this.tcCampanha_StatusAtendimento.Padding = new System.Windows.Forms.Padding(3);
            this.tcCampanha_StatusAtendimento.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_StatusAtendimento.TabIndex = 2;
            this.tcCampanha_StatusAtendimento.Text = "Status de Atendimento";
            this.tcCampanha_StatusAtendimento.UseVisualStyleBackColor = true;
            // 
            // clbStatusDeAtendimento
            // 
            this.clbStatusDeAtendimento.CheckOnClick = true;
            this.clbStatusDeAtendimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbStatusDeAtendimento.FormattingEnabled = true;
            this.clbStatusDeAtendimento.Location = new System.Drawing.Point(3, 3);
            this.clbStatusDeAtendimento.Name = "clbStatusDeAtendimento";
            this.clbStatusDeAtendimento.Size = new System.Drawing.Size(692, 233);
            this.clbStatusDeAtendimento.TabIndex = 2;
            // 
            // tcCampanha_tpStatusOferta
            // 
            this.tcCampanha_tpStatusOferta.Controls.Add(this.clbStatusDeOferta);
            this.tcCampanha_tpStatusOferta.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_tpStatusOferta.Name = "tcCampanha_tpStatusOferta";
            this.tcCampanha_tpStatusOferta.Padding = new System.Windows.Forms.Padding(3);
            this.tcCampanha_tpStatusOferta.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_tpStatusOferta.TabIndex = 3;
            this.tcCampanha_tpStatusOferta.Text = "Status de Oferta";
            this.tcCampanha_tpStatusOferta.UseVisualStyleBackColor = true;
            // 
            // clbStatusDeOferta
            // 
            this.clbStatusDeOferta.CheckOnClick = true;
            this.clbStatusDeOferta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbStatusDeOferta.FormattingEnabled = true;
            this.clbStatusDeOferta.Location = new System.Drawing.Point(3, 3);
            this.clbStatusDeOferta.Name = "clbStatusDeOferta";
            this.clbStatusDeOferta.Size = new System.Drawing.Size(692, 233);
            this.clbStatusDeOferta.TabIndex = 2;
            // 
            // tcCampanha_tpStatusAuditoria
            // 
            this.tcCampanha_tpStatusAuditoria.Controls.Add(this.clbStatusDeAuditoria);
            this.tcCampanha_tpStatusAuditoria.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_tpStatusAuditoria.Name = "tcCampanha_tpStatusAuditoria";
            this.tcCampanha_tpStatusAuditoria.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_tpStatusAuditoria.TabIndex = 5;
            this.tcCampanha_tpStatusAuditoria.Text = "Status de Auditoria";
            this.tcCampanha_tpStatusAuditoria.UseVisualStyleBackColor = true;
            // 
            // clbStatusDeAuditoria
            // 
            this.clbStatusDeAuditoria.CheckOnClick = true;
            this.clbStatusDeAuditoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbStatusDeAuditoria.FormattingEnabled = true;
            this.clbStatusDeAuditoria.Location = new System.Drawing.Point(0, 0);
            this.clbStatusDeAuditoria.Name = "clbStatusDeAuditoria";
            this.clbStatusDeAuditoria.Size = new System.Drawing.Size(698, 239);
            this.clbStatusDeAuditoria.TabIndex = 3;
            // 
            // tcCampanha_tpVarievalScript
            // 
            this.tcCampanha_tpVarievalScript.Controls.Add(this.clbVariavelScript);
            this.tcCampanha_tpVarievalScript.Location = new System.Drawing.Point(4, 22);
            this.tcCampanha_tpVarievalScript.Name = "tcCampanha_tpVarievalScript";
            this.tcCampanha_tpVarievalScript.Padding = new System.Windows.Forms.Padding(3);
            this.tcCampanha_tpVarievalScript.Size = new System.Drawing.Size(698, 239);
            this.tcCampanha_tpVarievalScript.TabIndex = 4;
            this.tcCampanha_tpVarievalScript.Text = "Variável de Script";
            this.tcCampanha_tpVarievalScript.UseVisualStyleBackColor = true;
            // 
            // clbVariavelScript
            // 
            this.clbVariavelScript.CheckOnClick = true;
            this.clbVariavelScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbVariavelScript.FormattingEnabled = true;
            this.clbVariavelScript.Location = new System.Drawing.Point(3, 3);
            this.clbVariavelScript.Name = "clbVariavelScript";
            this.clbVariavelScript.Size = new System.Drawing.Size(692, 233);
            this.clbVariavelScript.TabIndex = 2;
            // 
            // cmbMailingIndicacao
            // 
            this.cmbMailingIndicacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbMailingIndicacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMailingIndicacao.FormattingEnabled = true;
            this.cmbMailingIndicacao.Location = new System.Drawing.Point(11, 175);
            this.cmbMailingIndicacao.Name = "cmbMailingIndicacao";
            this.cmbMailingIndicacao.Size = new System.Drawing.Size(350, 21);
            this.cmbMailingIndicacao.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Mailing Indicação";
            // 
            // cmbTipoCampanha
            // 
            this.cmbTipoCampanha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbTipoCampanha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTipoCampanha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCampanha.FormattingEnabled = true;
            this.cmbTipoCampanha.Items.AddRange(new object[] {
            "PREDITIVO"});
            this.cmbTipoCampanha.Location = new System.Drawing.Point(367, 56);
            this.cmbTipoCampanha.Name = "cmbTipoCampanha";
            this.cmbTipoCampanha.Size = new System.Drawing.Size(105, 21);
            this.cmbTipoCampanha.TabIndex = 3;
            this.cmbTipoCampanha.SelectionChangeCommitted += new System.EventHandler(this.cmbTipoCampanha_SelectionChangeCommitted);
            // 
            // lblTipoCampanha
            // 
            this.lblTipoCampanha.AutoSize = true;
            this.lblTipoCampanha.Location = new System.Drawing.Point(364, 40);
            this.lblTipoCampanha.Name = "lblTipoCampanha";
            this.lblTipoCampanha.Size = new System.Drawing.Size(97, 13);
            this.lblTipoCampanha.TabIndex = 71;
            this.lblTipoCampanha.Text = "Tipo de Campanha";
            // 
            // lnkNenhum
            // 
            this.lnkNenhum.AutoSize = true;
            this.lnkNenhum.Location = new System.Drawing.Point(666, 242);
            this.lnkNenhum.Name = "lnkNenhum";
            this.lnkNenhum.Size = new System.Drawing.Size(47, 13);
            this.lnkNenhum.TabIndex = 24;
            this.lnkNenhum.TabStop = true;
            this.lnkNenhum.Text = "Nenhum";
            this.lnkNenhum.Visible = false;
            this.lnkNenhum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNenhum_LinkClicked);
            // 
            // lnkTodos
            // 
            this.lnkTodos.AutoSize = true;
            this.lnkTodos.Location = new System.Drawing.Point(623, 242);
            this.lnkTodos.Name = "lnkTodos";
            this.lnkTodos.Size = new System.Drawing.Size(37, 13);
            this.lnkTodos.TabIndex = 23;
            this.lnkTodos.TabStop = true;
            this.lnkTodos.Text = "Todos";
            this.lnkTodos.Visible = false;
            this.lnkTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTodos_LinkClicked);
            // 
            // chkIdTipoDeAuditoria
            // 
            this.chkIdTipoDeAuditoria.AutoSize = true;
            this.chkIdTipoDeAuditoria.Location = new System.Drawing.Point(4, 136);
            this.chkIdTipoDeAuditoria.Name = "chkIdTipoDeAuditoria";
            this.chkIdTipoDeAuditoria.Size = new System.Drawing.Size(90, 17);
            this.chkIdTipoDeAuditoria.TabIndex = 24;
            this.chkIdTipoDeAuditoria.Text = "Venda Online";
            this.chkIdTipoDeAuditoria.UseVisualStyleBackColor = true;
            // 
            // CampanhaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(728, 637);
            this.Controls.Add(this.lnkNenhum);
            this.Controls.Add(this.lnkTodos);
            this.Controls.Add(this.cmbTipoCampanha);
            this.Controls.Add(this.lblTipoCampanha);
            this.Controls.Add(this.cmbMailingIndicacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tcCampanha);
            this.Controls.Add(this.txtEnderecoInputMailing);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMetaDeVenda);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAfterCall);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbStatusAutomaticoAceite);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbStatusAutomatico);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbLayoutCampoDinamicoBko);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbLayoutCampoDinamicoOperacao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbScriptFinalizacao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.cmbScriptApresentacao);
            this.Controls.Add(this.lblScript);
            this.Controls.Add(this.cmbTipoDeDiscagem);
            this.Controls.Add(this.lblCampanha);
            this.Controls.Add(this.cmbDiscador);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CampanhaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Campanha";
            this.Load += new System.EventHandler(this.CampanhaForm_Load);
            this.tcCampanha.ResumeLayout(false);
            this.tcCampanha_tpConfiguracoes.ResumeLayout(false);
            this.pnlOpcoes2.ResumeLayout(false);
            this.pnlOpcoes1.ResumeLayout(false);
            this.pnlOpcoes1.PerformLayout();
            this.tcCampanha_tpBancos.ResumeLayout(false);
            this.tcCampanha_tpFormaPagamento.ResumeLayout(false);
            this.tcCampanha_StatusAtendimento.ResumeLayout(false);
            this.tcCampanha_tpStatusOferta.ResumeLayout(false);
            this.tcCampanha_tpStatusAuditoria.ResumeLayout(false);
            this.tcCampanha_tpVarievalScript.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.ComboBox cmbScriptApresentacao;
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.ComboBox cmbTipoDeDiscagem;
        private System.Windows.Forms.Label lblCampanha;
        private System.Windows.Forms.ComboBox cmbDiscador;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ComboBox cmbScriptFinalizacao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLayoutCampoDinamicoOperacao;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLayoutCampoDinamicoBko;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStatusAutomaticoAceite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbStatusAutomatico;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAfterCall;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMetaDeVenda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEnderecoInputMailing;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tcCampanha;
        private System.Windows.Forms.TabPage tcCampanha_tpBancos;
        private System.Windows.Forms.TabPage tcCampanha_tpFormaPagamento;
        private System.Windows.Forms.TabPage tcCampanha_StatusAtendimento;
        private System.Windows.Forms.TabPage tcCampanha_tpStatusOferta;
        private System.Windows.Forms.TabPage tcCampanha_tpVarievalScript;
        private System.Windows.Forms.CheckedListBox clbBanco;
        private System.Windows.Forms.CheckedListBox clbFormaPagamento;
        private System.Windows.Forms.CheckedListBox clbStatusDeAtendimento;
        private System.Windows.Forms.CheckedListBox clbStatusDeOferta;
        private System.Windows.Forms.CheckedListBox clbVariavelScript;
        private System.Windows.Forms.ComboBox cmbMailingIndicacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tcCampanha_tpStatusAuditoria;
        private System.Windows.Forms.ComboBox cmbTipoCampanha;
        private System.Windows.Forms.Label lblTipoCampanha;
        private System.Windows.Forms.LinkLabel lnkNenhum;
        private System.Windows.Forms.LinkLabel lnkTodos;
        private System.Windows.Forms.CheckedListBox clbStatusDeAuditoria;
        private System.Windows.Forms.TabPage tcCampanha_tpConfiguracoes;
        private System.Windows.Forms.Panel pnlOpcoes1;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.CheckBox chkCepExpress;
        private System.Windows.Forms.CheckBox chkHabilitaPesquisa;
        private System.Windows.Forms.CheckBox chkHistorico;
        private System.Windows.Forms.CheckBox chkDiscagemManual;
        private System.Windows.Forms.CheckBox chkIndicacao;
        private System.Windows.Forms.CheckBox chkSimulador;
        private System.Windows.Forms.CheckBox chkCadastroManual;
        private System.Windows.Forms.Panel pnlOpcoes2;
        private System.Windows.Forms.CheckBox chkHabilitaRevenda;
        private System.Windows.Forms.CheckBox chkIdTipoDeAuditoria;
    }
}