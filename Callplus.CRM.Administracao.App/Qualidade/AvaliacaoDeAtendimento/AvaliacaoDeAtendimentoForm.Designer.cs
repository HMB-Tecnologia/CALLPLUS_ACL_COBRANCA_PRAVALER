namespace Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento
{
    partial class AvaliacaoDeAtendimentoForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvaliacaoDeAtendimentoForm));
            this.gbFeedback = new System.Windows.Forms.GroupBox();
            this.gbAuditor = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDataFeedback = new System.Windows.Forms.TextBox();
            this.txtAuditor = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.gbAssinatura = new System.Windows.Forms.GroupBox();
            this.btnFeedback = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtLoginOperador = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDetalheContato = new System.Windows.Forms.Button();
            this.gbAvaliacao = new System.Windows.Forms.GroupBox();
            this.lblPontuacaoDosModulos = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtNomeAvaliador = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtNomeSupervisor = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNomeOperador = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPeso = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblPagina = new System.Windows.Forms.Label();
            this.lblPontuacaoItem = new System.Windows.Forms.Label();
            this.lblPontuacao = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.gvProcedimento = new System.Windows.Forms.DataGridView();
            this.idProcedimento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ok = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nok = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.na = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblModulo = new System.Windows.Forms.Label();
            this.txtObervacaoAvaliador = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.lblTelefone1 = new System.Windows.Forms.Label();
            this.txtDataContato = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIdCodigo = new System.Windows.Forms.TextBox();
            this.txtMailing = new System.Windows.Forms.TextBox();
            this.lblMailing = new System.Windows.Forms.Label();
            this.lblIdContato = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNomeProspect = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnExcluirFeedback = new System.Windows.Forms.Button();
            this.tcOferta = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlOferta = new System.Windows.Forms.Panel();
            this.gbFeedback.SuspendLayout();
            this.gbAuditor.SuspendLayout();
            this.gbAssinatura.SuspendLayout();
            this.gbAvaliacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcedimento)).BeginInit();
            this.tcOferta.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFeedback
            // 
            this.gbFeedback.Controls.Add(this.gbAuditor);
            this.gbFeedback.Controls.Add(this.gbAssinatura);
            this.gbFeedback.Controls.Add(this.label10);
            this.gbFeedback.Location = new System.Drawing.Point(823, 37);
            this.gbFeedback.Name = "gbFeedback";
            this.gbFeedback.Size = new System.Drawing.Size(401, 595);
            this.gbFeedback.TabIndex = 19;
            this.gbFeedback.TabStop = false;
            this.gbFeedback.Text = "Feedback";
            // 
            // gbAuditor
            // 
            this.gbAuditor.Controls.Add(this.label15);
            this.gbAuditor.Controls.Add(this.txtDataFeedback);
            this.gbAuditor.Controls.Add(this.txtAuditor);
            this.gbAuditor.Controls.Add(this.label16);
            this.gbAuditor.Location = new System.Drawing.Point(18, 523);
            this.gbAuditor.Name = "gbAuditor";
            this.gbAuditor.Size = new System.Drawing.Size(367, 62);
            this.gbAuditor.TabIndex = 2;
            this.gbAuditor.TabStop = false;
            this.gbAuditor.Text = "Auditor";
            this.gbAuditor.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(250, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Data";
            // 
            // txtDataFeedback
            // 
            this.txtDataFeedback.Location = new System.Drawing.Point(253, 32);
            this.txtDataFeedback.Name = "txtDataFeedback";
            this.txtDataFeedback.ReadOnly = true;
            this.txtDataFeedback.Size = new System.Drawing.Size(104, 20);
            this.txtDataFeedback.TabIndex = 3;
            // 
            // txtAuditor
            // 
            this.txtAuditor.BackColor = System.Drawing.SystemColors.Control;
            this.txtAuditor.Location = new System.Drawing.Point(12, 32);
            this.txtAuditor.MaxLength = 15;
            this.txtAuditor.Name = "txtAuditor";
            this.txtAuditor.ReadOnly = true;
            this.txtAuditor.Size = new System.Drawing.Size(235, 20);
            this.txtAuditor.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Nome";
            // 
            // gbAssinatura
            // 
            this.gbAssinatura.Controls.Add(this.btnFeedback);
            this.gbAssinatura.Controls.Add(this.label11);
            this.gbAssinatura.Controls.Add(this.txtSenha);
            this.gbAssinatura.Controls.Add(this.txtLoginOperador);
            this.gbAssinatura.Controls.Add(this.label12);
            this.gbAssinatura.Location = new System.Drawing.Point(18, 459);
            this.gbAssinatura.Name = "gbAssinatura";
            this.gbAssinatura.Size = new System.Drawing.Size(367, 62);
            this.gbAssinatura.TabIndex = 1;
            this.gbAssinatura.TabStop = false;
            this.gbAssinatura.Text = "Assinatura";
            // 
            // btnFeedback
            // 
            this.btnFeedback.BackColor = System.Drawing.SystemColors.Control;
            this.btnFeedback.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFeedback.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.check;
            this.btnFeedback.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFeedback.Location = new System.Drawing.Point(253, 29);
            this.btnFeedback.Name = "btnFeedback";
            this.btnFeedback.Size = new System.Drawing.Size(104, 24);
            this.btnFeedback.TabIndex = 4;
            this.btnFeedback.Text = "Confirmar  ";
            this.btnFeedback.UseVisualStyleBackColor = true;
            this.btnFeedback.Click += new System.EventHandler(this.btnFeedback_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(143, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(146, 31);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(101, 20);
            this.txtSenha.TabIndex = 3;
            // 
            // txtLoginOperador
            // 
            this.txtLoginOperador.BackColor = System.Drawing.Color.White;
            this.txtLoginOperador.Enabled = false;
            this.txtLoginOperador.Location = new System.Drawing.Point(11, 31);
            this.txtLoginOperador.MaxLength = 15;
            this.txtLoginOperador.Name = "txtLoginOperador";
            this.txtLoginOperador.ReadOnly = true;
            this.txtLoginOperador.Size = new System.Drawing.Size(129, 20);
            this.txtLoginOperador.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Login";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "FAQ";
            // 
            // btnDetalheContato
            // 
            this.btnDetalheContato.BackColor = System.Drawing.SystemColors.Control;
            this.btnDetalheContato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDetalheContato.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.address_book_blue;
            this.btnDetalheContato.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetalheContato.Location = new System.Drawing.Point(687, 12);
            this.btnDetalheContato.Name = "btnDetalheContato";
            this.btnDetalheContato.Size = new System.Drawing.Size(130, 25);
            this.btnDetalheContato.TabIndex = 23;
            this.btnDetalheContato.Text = "Detalhes do Contato    ";
            this.btnDetalheContato.UseVisualStyleBackColor = true;
            this.btnDetalheContato.Click += new System.EventHandler(this.btnDetalheContato_Click);
            // 
            // gbAvaliacao
            // 
            this.gbAvaliacao.Controls.Add(this.lblPontuacaoDosModulos);
            this.gbAvaliacao.Controls.Add(this.label20);
            this.gbAvaliacao.Controls.Add(this.txtNomeAvaliador);
            this.gbAvaliacao.Controls.Add(this.label19);
            this.gbAvaliacao.Controls.Add(this.txtNomeSupervisor);
            this.gbAvaliacao.Controls.Add(this.label18);
            this.gbAvaliacao.Controls.Add(this.txtNomeOperador);
            this.gbAvaliacao.Controls.Add(this.label2);
            this.gbAvaliacao.Controls.Add(this.lblPeso);
            this.gbAvaliacao.Controls.Add(this.txtDescricao);
            this.gbAvaliacao.Controls.Add(this.lblPagina);
            this.gbAvaliacao.Controls.Add(this.lblPontuacaoItem);
            this.gbAvaliacao.Controls.Add(this.lblPontuacao);
            this.gbAvaliacao.Controls.Add(this.btnCancelar);
            this.gbAvaliacao.Controls.Add(this.btnGravar);
            this.gbAvaliacao.Controls.Add(this.label14);
            this.gbAvaliacao.Controls.Add(this.btnProximo);
            this.gbAvaliacao.Controls.Add(this.btnAnterior);
            this.gbAvaliacao.Controls.Add(this.label13);
            this.gbAvaliacao.Controls.Add(this.gvProcedimento);
            this.gbAvaliacao.Controls.Add(this.lblItem);
            this.gbAvaliacao.Controls.Add(this.lblModulo);
            this.gbAvaliacao.Controls.Add(this.txtObervacaoAvaliador);
            this.gbAvaliacao.Controls.Add(this.txtTelefone);
            this.gbAvaliacao.Controls.Add(this.lblTelefone1);
            this.gbAvaliacao.Controls.Add(this.txtDataContato);
            this.gbAvaliacao.Controls.Add(this.label9);
            this.gbAvaliacao.Controls.Add(this.txtIdCodigo);
            this.gbAvaliacao.Controls.Add(this.txtMailing);
            this.gbAvaliacao.Controls.Add(this.lblMailing);
            this.gbAvaliacao.Controls.Add(this.lblIdContato);
            this.gbAvaliacao.Controls.Add(this.lblNome);
            this.gbAvaliacao.Controls.Add(this.txtNomeProspect);
            this.gbAvaliacao.Location = new System.Drawing.Point(12, 43);
            this.gbAvaliacao.Name = "gbAvaliacao";
            this.gbAvaliacao.Size = new System.Drawing.Size(805, 595);
            this.gbAvaliacao.TabIndex = 0;
            this.gbAvaliacao.TabStop = false;
            this.gbAvaliacao.Text = "Avaliação";
            // 
            // lblPontuacaoDosModulos
            // 
            this.lblPontuacaoDosModulos.AutoSize = true;
            this.lblPontuacaoDosModulos.BackColor = System.Drawing.Color.Red;
            this.lblPontuacaoDosModulos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPontuacaoDosModulos.ForeColor = System.Drawing.Color.White;
            this.lblPontuacaoDosModulos.Location = new System.Drawing.Point(627, 104);
            this.lblPontuacaoDosModulos.Name = "lblPontuacaoDosModulos";
            this.lblPontuacaoDosModulos.Size = new System.Drawing.Size(160, 13);
            this.lblPontuacaoDosModulos.TabIndex = 32;
            this.lblPontuacaoDosModulos.Text = "Pontuação do Modulo: 100";
            this.lblPontuacaoDosModulos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(544, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 15;
            this.label20.Text = "Nome Avaliador";
            // 
            // txtNomeAvaliador
            // 
            this.txtNomeAvaliador.BackColor = System.Drawing.Color.White;
            this.txtNomeAvaliador.Location = new System.Drawing.Point(547, 73);
            this.txtNomeAvaliador.MaxLength = 100;
            this.txtNomeAvaliador.Name = "txtNomeAvaliador";
            this.txtNomeAvaliador.ReadOnly = true;
            this.txtNomeAvaliador.Size = new System.Drawing.Size(249, 20);
            this.txtNomeAvaliador.TabIndex = 16;
            this.txtNomeAvaliador.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(321, 57);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 13;
            this.label19.Text = "Nome Supervisor";
            // 
            // txtNomeSupervisor
            // 
            this.txtNomeSupervisor.BackColor = System.Drawing.Color.White;
            this.txtNomeSupervisor.Location = new System.Drawing.Point(324, 73);
            this.txtNomeSupervisor.MaxLength = 100;
            this.txtNomeSupervisor.Name = "txtNomeSupervisor";
            this.txtNomeSupervisor.ReadOnly = true;
            this.txtNomeSupervisor.Size = new System.Drawing.Size(217, 20);
            this.txtNomeSupervisor.TabIndex = 14;
            this.txtNomeSupervisor.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 57);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "Nome Operador";
            // 
            // txtNomeOperador
            // 
            this.txtNomeOperador.BackColor = System.Drawing.Color.White;
            this.txtNomeOperador.Location = new System.Drawing.Point(8, 73);
            this.txtNomeOperador.MaxLength = 100;
            this.txtNomeOperador.Name = "txtNomeOperador";
            this.txtNomeOperador.ReadOnly = true;
            this.txtNomeOperador.Size = new System.Drawing.Size(310, 20);
            this.txtNomeOperador.TabIndex = 12;
            this.txtNomeOperador.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(486, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Peso:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeso.ForeColor = System.Drawing.Color.Black;
            this.lblPeso.Location = new System.Drawing.Point(522, 410);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(14, 13);
            this.lblPeso.TabIndex = 26;
            this.lblPeso.Text = "0";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.White;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Location = new System.Drawing.Point(8, 153);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.Size = new System.Drawing.Size(788, 94);
            this.txtDescricao.TabIndex = 19;
            this.txtDescricao.TabStop = false;
            // 
            // lblPagina
            // 
            this.lblPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagina.Location = new System.Drawing.Point(89, 407);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(73, 19);
            this.lblPagina.TabIndex = 23;
            this.lblPagina.Text = "1 de 4";
            this.lblPagina.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPontuacaoItem
            // 
            this.lblPontuacaoItem.AutoSize = true;
            this.lblPontuacaoItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPontuacaoItem.ForeColor = System.Drawing.Color.Red;
            this.lblPontuacaoItem.Location = new System.Drawing.Point(653, 410);
            this.lblPontuacaoItem.Name = "lblPontuacaoItem";
            this.lblPontuacaoItem.Size = new System.Drawing.Size(114, 13);
            this.lblPontuacaoItem.TabIndex = 27;
            this.lblPontuacaoItem.Text = "Pontos Perdidos: 0";
            this.lblPontuacaoItem.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPontuacao
            // 
            this.lblPontuacao.AutoSize = true;
            this.lblPontuacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPontuacao.ForeColor = System.Drawing.Color.Blue;
            this.lblPontuacao.Location = new System.Drawing.Point(637, 569);
            this.lblPontuacao.Name = "lblPontuacao";
            this.lblPontuacao.Size = new System.Drawing.Size(130, 13);
            this.lblPontuacao.TabIndex = 0;
            this.lblPontuacao.Text = "Pontuação Total: 100";
            this.lblPontuacao.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancelar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.cancel;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.Location = new System.Drawing.Point(171, 563);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(148, 25);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar Avaliação";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.FlatAppearance.BorderSize = 0;
            this.btnGravar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGravar.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGravar.Location = new System.Drawing.Point(8, 563);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(157, 25);
            this.btnGravar.TabIndex = 4;
            this.btnGravar.Text = "Gravar Avaliação";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 432);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Observações do Avaliador";
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(168, 405);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(75, 23);
            this.btnProximo.TabIndex = 2;
            this.btnProximo.Text = "Próximo";
            this.btnProximo.UseVisualStyleBackColor = true;
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(8, 405);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 1;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 250);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Procedimentos";
            // 
            // gvProcedimento
            // 
            this.gvProcedimento.AllowUserToAddRows = false;
            this.gvProcedimento.AllowUserToDeleteRows = false;
            this.gvProcedimento.AllowUserToResizeRows = false;
            this.gvProcedimento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProcedimento.BackgroundColor = System.Drawing.Color.White;
            this.gvProcedimento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProcedimento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idProcedimento,
            this.numero,
            this.descricao,
            this.ok,
            this.nok,
            this.na});
            this.gvProcedimento.Location = new System.Drawing.Point(8, 266);
            this.gvProcedimento.Name = "gvProcedimento";
            this.gvProcedimento.RowHeadersVisible = false;
            this.gvProcedimento.Size = new System.Drawing.Size(788, 133);
            this.gvProcedimento.TabIndex = 0;
            this.gvProcedimento.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProcedimento_CellContentClick);
            this.gvProcedimento.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProcedimento_CellEndEdit);
            this.gvProcedimento.CurrentCellDirtyStateChanged += new System.EventHandler(this.gvProcedimento_CurrentCellDirtyStateChanged);
            // 
            // idProcedimento
            // 
            this.idProcedimento.DataPropertyName = "idProcedimento";
            this.idProcedimento.HeaderText = "id";
            this.idProcedimento.Name = "idProcedimento";
            this.idProcedimento.Visible = false;
            // 
            // numero
            // 
            this.numero.DataPropertyName = "numero";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numero.DefaultCellStyle = dataGridViewCellStyle1;
            this.numero.FillWeight = 124.8211F;
            this.numero.HeaderText = "Número";
            this.numero.Name = "numero";
            this.numero.ReadOnly = true;
            this.numero.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.numero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // descricao
            // 
            this.descricao.DataPropertyName = "descricao";
            this.descricao.FillWeight = 107.4218F;
            this.descricao.HeaderText = "Descrição";
            this.descricao.Name = "descricao";
            this.descricao.ReadOnly = true;
            this.descricao.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.descricao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ok
            // 
            this.ok.FillWeight = 86.5388F;
            this.ok.HeaderText = "OK";
            this.ok.Name = "ok";
            this.ok.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // nok
            // 
            this.nok.FillWeight = 81.21828F;
            this.nok.HeaderText = "NOK";
            this.nok.Name = "nok";
            this.nok.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // na
            // 
            this.na.HeaderText = "N/A";
            this.na.Name = "na";
            this.na.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.ForeColor = System.Drawing.Color.White;
            this.lblItem.Location = new System.Drawing.Point(8, 126);
            this.lblItem.MaximumSize = new System.Drawing.Size(1000, 20);
            this.lblItem.MinimumSize = new System.Drawing.Size(200, 20);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(788, 20);
            this.lblItem.TabIndex = 18;
            this.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModulo
            // 
            this.lblModulo.BackColor = System.Drawing.Color.Red;
            this.lblModulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.ForeColor = System.Drawing.Color.White;
            this.lblModulo.Location = new System.Drawing.Point(8, 100);
            this.lblModulo.MaximumSize = new System.Drawing.Size(1000, 20);
            this.lblModulo.MinimumSize = new System.Drawing.Size(200, 20);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(788, 20);
            this.lblModulo.TabIndex = 17;
            this.lblModulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtObervacaoAvaliador
            // 
            this.txtObervacaoAvaliador.BackColor = System.Drawing.Color.White;
            this.txtObervacaoAvaliador.ForeColor = System.Drawing.Color.Black;
            this.txtObervacaoAvaliador.Location = new System.Drawing.Point(8, 448);
            this.txtObervacaoAvaliador.MaxLength = 1500;
            this.txtObervacaoAvaliador.Multiline = true;
            this.txtObervacaoAvaliador.Name = "txtObervacaoAvaliador";
            this.txtObervacaoAvaliador.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObervacaoAvaliador.Size = new System.Drawing.Size(788, 109);
            this.txtObervacaoAvaliador.TabIndex = 3;
            // 
            // txtTelefone
            // 
            this.txtTelefone.BackColor = System.Drawing.Color.White;
            this.txtTelefone.Location = new System.Drawing.Point(547, 34);
            this.txtTelefone.MaxLength = 10;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.ReadOnly = true;
            this.txtTelefone.Size = new System.Drawing.Size(104, 20);
            this.txtTelefone.TabIndex = 8;
            this.txtTelefone.TabStop = false;
            // 
            // lblTelefone1
            // 
            this.lblTelefone1.AutoSize = true;
            this.lblTelefone1.Location = new System.Drawing.Point(544, 18);
            this.lblTelefone1.Name = "lblTelefone1";
            this.lblTelefone1.Size = new System.Drawing.Size(49, 13);
            this.lblTelefone1.TabIndex = 7;
            this.lblTelefone1.Text = "Telefone";
            // 
            // txtDataContato
            // 
            this.txtDataContato.BackColor = System.Drawing.Color.White;
            this.txtDataContato.Location = new System.Drawing.Point(657, 34);
            this.txtDataContato.MaxLength = 15;
            this.txtDataContato.Name = "txtDataContato";
            this.txtDataContato.ReadOnly = true;
            this.txtDataContato.Size = new System.Drawing.Size(139, 20);
            this.txtDataContato.TabIndex = 10;
            this.txtDataContato.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(654, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Data do Contato";
            // 
            // txtIdCodigo
            // 
            this.txtIdCodigo.BackColor = System.Drawing.Color.White;
            this.txtIdCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtIdCodigo.Location = new System.Drawing.Point(8, 34);
            this.txtIdCodigo.Name = "txtIdCodigo";
            this.txtIdCodigo.ReadOnly = true;
            this.txtIdCodigo.Size = new System.Drawing.Size(56, 20);
            this.txtIdCodigo.TabIndex = 1;
            this.txtIdCodigo.TabStop = false;
            // 
            // txtMailing
            // 
            this.txtMailing.BackColor = System.Drawing.Color.White;
            this.txtMailing.Location = new System.Drawing.Point(70, 34);
            this.txtMailing.MaxLength = 3000;
            this.txtMailing.Name = "txtMailing";
            this.txtMailing.ReadOnly = true;
            this.txtMailing.Size = new System.Drawing.Size(248, 20);
            this.txtMailing.TabIndex = 3;
            this.txtMailing.TabStop = false;
            // 
            // lblMailing
            // 
            this.lblMailing.AutoSize = true;
            this.lblMailing.Location = new System.Drawing.Point(67, 18);
            this.lblMailing.Name = "lblMailing";
            this.lblMailing.Size = new System.Drawing.Size(40, 13);
            this.lblMailing.TabIndex = 2;
            this.lblMailing.Text = "Mailing";
            // 
            // lblIdContato
            // 
            this.lblIdContato.AutoSize = true;
            this.lblIdContato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdContato.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblIdContato.Location = new System.Drawing.Point(5, 18);
            this.lblIdContato.Name = "lblIdContato";
            this.lblIdContato.Size = new System.Drawing.Size(46, 13);
            this.lblIdContato.TabIndex = 0;
            this.lblIdContato.Text = "Código";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(321, 18);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(80, 13);
            this.lblNome.TabIndex = 4;
            this.lblNome.Text = "Nome Prospect";
            // 
            // txtNomeProspect
            // 
            this.txtNomeProspect.BackColor = System.Drawing.Color.White;
            this.txtNomeProspect.Location = new System.Drawing.Point(324, 34);
            this.txtNomeProspect.MaxLength = 100;
            this.txtNomeProspect.Name = "txtNomeProspect";
            this.txtNomeProspect.ReadOnly = true;
            this.txtNomeProspect.Size = new System.Drawing.Size(217, 20);
            this.txtNomeProspect.TabIndex = 5;
            this.txtNomeProspect.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(7, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(291, 25);
            this.lblTitulo.TabIndex = 20;
            this.lblTitulo.Text = "AVALIAÇÃO DE QUALIDADE";
            // 
            // btnExcluirFeedback
            // 
            this.btnExcluirFeedback.BackColor = System.Drawing.SystemColors.Control;
            this.btnExcluirFeedback.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExcluirFeedback.Image = global::Callplus.CRM.Administracao.App.Properties.Resources.cross;
            this.btnExcluirFeedback.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluirFeedback.Location = new System.Drawing.Point(1107, 12);
            this.btnExcluirFeedback.Name = "btnExcluirFeedback";
            this.btnExcluirFeedback.Size = new System.Drawing.Size(117, 25);
            this.btnExcluirFeedback.TabIndex = 24;
            this.btnExcluirFeedback.Text = "Excluir Feedback";
            this.btnExcluirFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirFeedback.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnExcluirFeedback.UseVisualStyleBackColor = true;
            this.btnExcluirFeedback.Visible = false;
            this.btnExcluirFeedback.Click += new System.EventHandler(this.btnExcluirFeedback_Click);
            // 
            // tcOferta
            // 
            this.tcOferta.Controls.Add(this.tabPage2);
            this.tcOferta.Location = new System.Drawing.Point(823, 12);
            this.tcOferta.Name = "tcOferta";
            this.tcOferta.SelectedIndex = 0;
            this.tcOferta.Size = new System.Drawing.Size(530, 588);
            this.tcOferta.TabIndex = 25;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.pnlOferta);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(522, 562);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detalhes do Contato";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlOferta
            // 
            this.pnlOferta.Location = new System.Drawing.Point(6, 4);
            this.pnlOferta.Name = "pnlOferta";
            this.pnlOferta.Size = new System.Drawing.Size(850, 533);
            this.pnlOferta.TabIndex = 27;
            // 
            // AvaliacaoDeAtendimentoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1239, 639);
            this.Controls.Add(this.tcOferta);
            this.Controls.Add(this.btnExcluirFeedback);
            this.Controls.Add(this.gbFeedback);
            this.Controls.Add(this.btnDetalheContato);
            this.Controls.Add(this.gbAvaliacao);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AvaliacaoDeAtendimentoForm";
            this.Text = "Avaliação de Atendimento";
            this.Load += new System.EventHandler(this.AvaliacaoDeAtendimentoForm_Load);
            this.gbFeedback.ResumeLayout(false);
            this.gbFeedback.PerformLayout();
            this.gbAuditor.ResumeLayout(false);
            this.gbAuditor.PerformLayout();
            this.gbAssinatura.ResumeLayout(false);
            this.gbAssinatura.PerformLayout();
            this.gbAvaliacao.ResumeLayout(false);
            this.gbAvaliacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcedimento)).EndInit();
            this.tcOferta.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExcluirFeedback;
        private System.Windows.Forms.GroupBox gbFeedback;
        private System.Windows.Forms.GroupBox gbAuditor;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDataFeedback;
        private System.Windows.Forms.TextBox txtAuditor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gbAssinatura;
        private System.Windows.Forms.Button btnFeedback;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtLoginOperador;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDetalheContato;
        private System.Windows.Forms.GroupBox gbAvaliacao;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtNomeAvaliador;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtNomeSupervisor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtNomeOperador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblPagina;
        private System.Windows.Forms.Label lblPontuacaoItem;
        private System.Windows.Forms.Label lblPontuacao;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnProximo;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView gvProcedimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProcedimento;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn descricao;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ok;
        private System.Windows.Forms.DataGridViewCheckBoxColumn nok;
        private System.Windows.Forms.DataGridViewCheckBoxColumn na;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.TextBox txtObervacaoAvaliador;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblTelefone1;
        private System.Windows.Forms.TextBox txtDataContato;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIdCodigo;
        private System.Windows.Forms.TextBox txtMailing;
        private System.Windows.Forms.Label lblMailing;
        private System.Windows.Forms.Label lblIdContato;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNomeProspect;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tcOferta;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlOferta;
        private System.Windows.Forms.Label lblPontuacaoDosModulos;
    }
}