﻿using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento
{
	public partial class AvaliacaoDeAtendimentoForm : Form
    {
        public AvaliacaoDeAtendimentoForm(string titulo, long idAvaliacao, int? idFormulario, string avaliador, bool feedbackRealizado, long idOfertaBko = 0, int idCampanha = 0)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _campanhaService = new CampanhaService();
            _feedbackDaAvaliacaoDeAtendimentoService = new FeedbackDaAvaliacaoDeAtendimentoService();
            _formularioDeQualidadeService = new FormularioDeQualidadeService();
            _loginService = new LoginService();
            _ofertaDoAtendimentoService = new AcordoDoAtendimentoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeAcordoService();
            _usuarioService = new UsuarioService();

            this.modoFeedback = false;

            this.titulo = titulo;
            this.idAvaliacao = idAvaliacao;
            this.idFormulario = idFormulario;
            this.avaliador = avaliador;
            this.feedbackRealizado = feedbackRealizado;

            _idOfertaBKO = idOfertaBko;
            _idCampanhaBKO = (int)idCampanha;

            InitializeComponent();
        }

        public AvaliacaoDeAtendimentoForm(string titulo, long idAvaliacao, int idFormulario, string avaliador, string auditor, string dataFeedback)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _campanhaService = new CampanhaService();
            _feedbackDaAvaliacaoDeAtendimentoService = new FeedbackDaAvaliacaoDeAtendimentoService();
            _formularioDeQualidadeService = new FormularioDeQualidadeService();
            _loginService = new LoginService();
            _ofertaDoAtendimentoService = new AcordoDoAtendimentoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeAcordoService();
            _usuarioService = new UsuarioService();

            this.modoFeedback = true;

            this.titulo = titulo;
            this.idAvaliacao = idAvaliacao;
            this.idFormulario = idFormulario;
            this.avaliador = avaliador;
            this.auditor = auditor;
            this.dataFeedback = dataFeedback;

            InitializeComponent();
        }

        public AvaliacaoDeAtendimentoForm(string titulo, long idOfertaBKO, int iDCampanha, int iDOperador, int iDSupervisor, string dTInicial, string dTFinal, string iDStatus, int iDTipo, long? idOferta)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _campanhaService = new CampanhaService();
            _feedbackDaAvaliacaoDeAtendimentoService = new FeedbackDaAvaliacaoDeAtendimentoService();
            _formularioDeQualidadeService = new FormularioDeQualidadeService();
            _loginService = new LoginService();
            _ofertaDoAtendimentoService = new AcordoDoAtendimentoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeAcordoService();
            _usuarioService = new UsuarioService();

            this.titulo = titulo;
            _idOfertaBKO = idOfertaBKO;
            this.iDCampanha = iDCampanha;
            this.iDOperador = iDOperador;
            this.iDSupervisor = iDSupervisor;
            this.dTInicial = dTInicial;
            this.dTFinal = dTFinal;
            this.iDStatus = iDStatus;
            this.iDTipo = iDTipo;
            this.iDOferta = idOferta;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
        private readonly CampanhaService _campanhaService;
        private readonly FeedbackDaAvaliacaoDeAtendimentoService _feedbackDaAvaliacaoDeAtendimentoService;
        private readonly FormularioDeQualidadeService _formularioDeQualidadeService;
        private readonly LoginService _loginService;
        private readonly AcordoDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly ProspectService _prospectService;
        private readonly StatusDeAcordoService _statusDeOfertaService;
        private readonly UsuarioService _usuarioService;

        private CobrancaAtendimentoPravaler _oferta;

        bool modoFeedback = false;
        bool feedbackRealizado = false;

        int? idFormulario = 0;
        int idOperadorDoContato = 0;
        int idsupervisorDoOperador = 0;

        long idAvaliacao = 0;
        long idProspect = 0;
        long idAtendimento = 0;

        string titulo = "";
        string idItemFormulario = "";
        string motivoNCG = "";
        string avaliador = "";
        string auditor = "";
        string dataFeedback = "";

        private ListBoxMessage.MessageListBox listbox;

        DataTable dataTableFormularioQualidade;
        DataTable dataTableProcedimentoFormularioQualidade;
        DataTable dataTableRespostaAvaliacao;

        List<KeyValuePair<string, string>> ListaDeProcedimentosOK = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> ListaDeProcedimentosNOK = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, string>> ListaDeProcedimentosNA = new List<KeyValuePair<string, string>>();
        List<KeyValuePair<string, int>> ListaPontuacaoPerdida = new List<KeyValuePair<string, int>>();

        private long _idOfertaBKO = 0;
        private int _idCampanhaBKO = 0;
        private int iDCampanha;
        private int iDOperador;
        private int iDSupervisor;
        private string dTInicial;
        private string dTFinal;
        private string iDStatus;
        private int iDTipo;
        private long? iDOferta;
        private List<ModuloDoFormularioDeQualidade> _moduloList;

        public bool atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            lblTitulo.Text = titulo;
            pnlOferta.Visible = false;

            gbAvaliacao.Location = new Point(12, 37);

            if (!modoFeedback)
            {
                this.Width = 845;
                gbFeedback.Visible = false;
            }
            else
            {
                this.Location = new Point(0, 0);
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                this.Height = Screen.PrimaryScreen.Bounds.Width - 20;
                this.Height = Screen.PrimaryScreen.Bounds.Height - 80;
            }

            listbox = new ListBoxMessage.MessageListBox();
            listbox.Size = new Size(367, 409);
            listbox.Location = new Point(18, 34);
            listbox.AutoScroll = true;
            this.gbFeedback.Controls.Add(listbox);
            tcOferta.Visible = false;
            pnlOferta.Visible = false;

            if (auditor != "")
            {
                if (AdministracaoMDI._usuario.IdPerfil == 5)
                    btnExcluirFeedback.Visible = true;

                gbAuditor.Visible = true;
                txtAuditor.Text = auditor;
                txtDataFeedback.Text = dataFeedback;

                gbAssinatura.Enabled = false;
            }

            gvProcedimento.AutoGenerateColumns = false;

            RealizarAjustesGrid();

            if (idAvaliacao > 0)
            {
                btnCancelar.Visible = false;

                txtNomeAvaliador.Text = avaliador;

                CarregarAvaliacao();
            }
            else
            {
                LimparAvaliacao();
            }

            if (_idOfertaBKO > 0)
            {
                if (idAvaliacao == 0)
                {
                    CarregarNovaAvaliacao();
                }
            }

            if (idAvaliacao == 0)
            {
                CarregarNovaAvaliacao();
            }
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void RealizarAjustesGrid()
        {
            gvProcedimento.Columns["numero"].Width = 55;
            gvProcedimento.Columns["ok"].Width = 40;
            gvProcedimento.Columns["nok"].Width = 40;
            gvProcedimento.Columns["na"].Width = 40;

            gvProcedimento.Columns["numero"].Width = 55;
            gvProcedimento.Columns["ok"].Width = 40;
            gvProcedimento.Columns["nok"].Width = 40;
            gvProcedimento.Columns["na"].Width = 40;
        }

        private void CarregarAvaliacao()
        {
            DataTable dataTable = _avaliacaoDeAtendimentoService.Retornar(idAvaliacao);

            if (dataTable.Rows.Count > 0)
            {
                int idCampanha = -1;
                int idTipo = (int)dataTable.Rows[0]["idTipo"];
                int idFormulario = (int)dataTable.Rows[0]["Id Formulário"];

                idProspect = (long)dataTable.Rows[0]["idProspect"];
                idAtendimento = (long)dataTable.Rows[0]["idAtendimento"];

                dataTableFormularioQualidade = _formularioDeQualidadeService.RetornarEstrutura(idFormulario, idCampanha);

                PreencherModulosDeQualidade();

                lblPontuacaoDosModulos.Visible = true;
                dataTableProcedimentoFormularioQualidade = _formularioDeQualidadeService.RetornarProcedimento(idFormulario, idCampanha);

                CarregarRespostasDaAvaliacao();

                CarregarInicioDaAvaliacao(idCampanha, dataTable);

                if (AdministracaoMDI._usuario.IdPerfil != 1 && AdministracaoMDI._usuario.IdPerfil != 5)
                {
                    gvProcedimento.ReadOnly = true;
                    txtObervacaoAvaliador.ReadOnly = true;
                    btnGravar.Visible = false;
                }

                if (modoFeedback || feedbackRealizado)
                {
                    gvProcedimento.ReadOnly = true;
                    txtObervacaoAvaliador.ReadOnly = true;
                    btnGravar.Visible = false;
                    btnDetalheContato.Visible = false;
                }

                if (modoFeedback)
                    ListarFAQ(int.Parse(idItemFormulario));

                AtualizarListaDePontuaçãoPerdida();
                CalcularPontosPerdidosPorModulo();
                AtualizarPontuacao();
            }
            else
            {
                MessageBox.Show("A Avaliação não foi encontrada.", "AVISO DO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void PreencherModulosDeQualidade()
        {
            List<ModuloDoFormularioDeQualidade> moduloList = new List<ModuloDoFormularioDeQualidade>();
            List<ItemDoModuloDoFormularioDeQualidade> itemList = new List<ItemDoModuloDoFormularioDeQualidade>();

            for (int i = 0; i < dataTableFormularioQualidade.Rows.Count; i++)
            {
                var modulo = new ModuloDoFormularioDeQualidade()
                {
                    Id = Convert.ToInt32(dataTableFormularioQualidade.Rows[i][0].ToString()),
                    Valor = 100,
                    Nome = dataTableFormularioQualidade.Rows[i][2].ToString(),
                    ItemList = new List<ItemDoModuloDoFormularioDeQualidade>(),
                    Peso = Convert.ToInt32(dataTableFormularioQualidade.Rows[i][7].ToString())
                };

                var item = new ItemDoModuloDoFormularioDeQualidade()
                {
                    Id = Convert.ToInt32(dataTableFormularioQualidade.Rows[i][1].ToString()),
                    IdModuloDoFormularioDeQualidade = Convert.ToInt32(dataTableFormularioQualidade.Rows[i][0].ToString()),
                    Peso = Convert.ToInt32(dataTableFormularioQualidade.Rows[i][5].ToString())
                };

                itemList.Add(item);

                if (moduloList.Exists(x => x.Id == modulo.Id)) continue;
                moduloList.Add(modulo);
            }

            foreach (var modulo in moduloList)
            {
                foreach (var item in itemList)
                {
                    if (modulo.Id == item.IdModuloDoFormularioDeQualidade)
                        modulo.ItemList.Add(item);
                }
            }

            _moduloList = moduloList;
        }

        private void CarregarRespostasDaAvaliacao()
        {
            dataTableRespostaAvaliacao = _avaliacaoDeAtendimentoService.ListarRespostaDaAvaliacao(idAvaliacao);

            string idItem = "";

            foreach (DataRow item in dataTableRespostaAvaliacao.Rows)
            {
                idItem = item["idItem"].ToString();

                if (item["ok"].ToString() == "True")
                {
                    ListaDeProcedimentosOK.Add(new KeyValuePair<string, string>(idItem, item["idProcedimento"].ToString()));
                }
                else if (item["ok"].ToString() == "False")
                {
                    ListaDeProcedimentosNOK.Add(new KeyValuePair<string, string>(idItem, item["idProcedimento"].ToString()));

                    ListaPontuacaoPerdida.RemoveAll(x => x.Key.Equals(idItem));
                    ListaPontuacaoPerdida.Add(new KeyValuePair<string, int>(idItem, (int)item["peso"]));
                }
                else
                {
                    ListaDeProcedimentosNA.Add(new KeyValuePair<string, string>(idItem, item["idProcedimento"].ToString()));
                }
            }
        }

        private void CarregarNovaAvaliacao()
        {
            int idCampanha = iDCampanha;

            if (idCampanha <= 0)
                idCampanha = _idCampanhaBKO;

            dataTableFormularioQualidade = _formularioDeQualidadeService.RetornarEstrutura(-1, idCampanha);

            PreencherModulosDeQualidade();

            if (dataTableFormularioQualidade.Rows.Count > 0)
            {
                idFormulario = (int)dataTableFormularioQualidade.Rows[0]["idFormulario"];

                DataTable dataTable = BuscarOferta();

                if (dataTable.Rows.Count > 0)
                {
                    idProspect = (long)dataTable.Rows[0]["idProspect"];
                    idAtendimento = (long)dataTable.Rows[0]["idAtendimento"];

                    dataTableProcedimentoFormularioQualidade = _formularioDeQualidadeService.RetornarProcedimento(-1, idCampanha);

                    CarregarInicioDaAvaliacao(idCampanha, dataTable);

                    if (_idOfertaBKO == 0)
                        btnCancelar.Visible = true;

                    gbAvaliacao.Visible = true;
                }
                else
                {
                    MessageBox.Show("Nenhum contato encontrado para os filtros informados!", "AVISO DO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CarregarInicioDaAvaliacao(int idCampanha, DataTable dataTable)
        {
            lblPontuacao.Text = "Pontuação Total: 100";
            lblPontuacao.Visible = true;

            txtIdCodigo.Text = dataTable.Rows[0]["idProspect"].ToString();
            txtMailing.Text = dataTable.Rows[0]["mailing"].ToString();
            txtNomeProspect.Text = dataTable.Rows[0]["nome"].ToString();
            txtTelefone.Text = dataTable.Rows[0]["telefone"].ToString();
            txtDataContato.Text = dataTable.Rows[0]["dataCriacao"].ToString();
            txtNomeOperador.Text = dataTable.Rows[0]["operador"].ToString();
            txtNomeSupervisor.Text = dataTable.Rows[0]["supervisor"].ToString();

            if (idAvaliacao == 0)
            {
                idOperadorDoContato = (int)dataTable.Rows[0]["idOperador"];
                idsupervisorDoOperador = (int)dataTable.Rows[0]["idSupervisor"];
            }
            else
            {
                txtObervacaoAvaliador.Text = dataTable.Rows[0]["observacao"].ToString();
                txtLoginOperador.Text = dataTable.Rows[0]["login"].ToString();
            }

            btnAnterior.Enabled = false;

            if (dataTableFormularioQualidade.Rows.Count == 0)
            {
                btnProximo.Enabled = false;
                btnGravar.Enabled = true;

                lblPagina.Text = "0 DE 0";

                lblModulo.Text = "[NENHUM MÓDULO ENCONTRADO]";
            }
            else if (dataTableFormularioQualidade.Rows.Count == 1)
            {
                btnProximo.Enabled = false;
                btnGravar.Enabled = true;

                CarregarDadosDoItem(0);

                lblPagina.Text = "1 DE 1";
            }
            else if (dataTableFormularioQualidade.Rows.Count > 1)
            {
                btnProximo.Enabled = true;
                btnGravar.Enabled = false;

                CarregarDadosDoItem(0);

                lblPagina.Text = "1 DE " + dataTableFormularioQualidade.Rows.Count;
            }

            btnDetalheContato.Visible = true;

            gbAvaliacao.Enabled = true;
        }

        private DataTable BuscarOferta()
        {
            int idCampanha = iDCampanha;
            int idOperador = iDOperador;
            int idSupervisor = iDSupervisor;
            string dataInicial = dTInicial;
            string dataFinal = dTFinal;
            string idStatus = iDStatus + ",";

            if (idStatus == "") idStatus = "0,";

            DataTable dataTable = null;

            if (_idOfertaBKO == 0)
            {
                dataTable = _ofertaDoAtendimentoService.RetornarOfertaParaAvaliacao(idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus, iDOferta);
            }
            else
            {
                dataTable = _ofertaDoAtendimentoService.RetornarOfertaParaAvaliacaoBKO(_idOfertaBKO);
            }

            return dataTable;
        }

        private void CarregarProcedimentoAnterior()
        {
            string[] splitPagina = lblPagina.Text.Split(' ');

            int ant = int.Parse(splitPagina[0]) - 1;

            string idItemCorrente = dataTableFormularioQualidade.Rows[ant]["idItem"].ToString();

            bool respondeuTudo = VerificarPreenchimento(idItemCorrente);

            if (!respondeuTudo)
            {
                string sMensagem = "Antes de sair do item corrente todos os procedimentos devem ser respondidos.";
                MessageBox.Show(sMensagem, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int pontuacaoItem = int.Parse(lblPontuacaoItem.Text.Replace("Pontos Perdidos: ", ""));
            ListaPontuacaoPerdida.Add(new KeyValuePair<string, int>(idItemCorrente, pontuacaoItem));

            btnProximo.Enabled = true;
            btnGravar.Enabled = false;

            CarregarDadosDoItem(ant - 1);

            lblPagina.Text = ant + " DE " + dataTableFormularioQualidade.Rows.Count;

            if (ant == 1)
            {
                btnAnterior.Enabled = false;
            }

            if (modoFeedback)
                ListarFAQ(int.Parse(idItemFormulario));
        }

        private void CarregarProcedimentoProximo()
        {
            string[] splitPagina = lblPagina.Text.Split(' ');

            int prox = int.Parse(splitPagina[0]) + 1;

            string idItemCorrente = dataTableFormularioQualidade.Rows[prox - 2]["idItem"].ToString();

            bool respondeuTudo = VerificarPreenchimento(idItemCorrente);

            if (!respondeuTudo)
            {
                string sMensagem = "Antes de sair do item corrente todos os procedimentos devem ser respondidos.";
                MessageBox.Show(sMensagem, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int pontuacaoItem = int.Parse(lblPontuacaoItem.Text.Replace("Pontos Perdidos: ", ""));
            ListaPontuacaoPerdida.Add(new KeyValuePair<string, int>(idItemCorrente, pontuacaoItem));

            btnAnterior.Enabled = true;

            CarregarDadosDoItem(prox - 1);

            lblPagina.Text = prox + " DE " + dataTableFormularioQualidade.Rows.Count;

            if (prox == dataTableFormularioQualidade.Rows.Count)
            {
                btnProximo.Enabled = false;
                btnGravar.Enabled = true;
            }

            if (modoFeedback)
                ListarFAQ(int.Parse(idItemFormulario));
        }

        private void CarregarDadosDoItem(int indice)
        {
            lblPontuacaoItem.Text = "Pontos Perdidos: 0";

            lblModulo.Text = dataTableFormularioQualidade.Rows[indice]["modulo"].ToString();
            lblItem.Text = dataTableFormularioQualidade.Rows[indice]["item"].ToString();
            txtDescricao.Text = dataTableFormularioQualidade.Rows[indice]["descricao"].ToString();
            idItemFormulario = dataTableFormularioQualidade.Rows[indice]["idItem"].ToString();
            lblPeso.Text = dataTableFormularioQualidade.Rows[indice]["peso"].ToString();

            if (dataTableProcedimentoFormularioQualidade.Select("idItem = " + idItemFormulario).Count() > 0)
            {
                gvProcedimento.DataSource = dataTableProcedimentoFormularioQualidade.Select("idItem = " + idItemFormulario).CopyToDataTable();

                foreach (DataGridViewRow row in gvProcedimento.Rows)
                {
                    DataGridViewCheckBoxCell chkOk = (DataGridViewCheckBoxCell)row.Cells[3];
                    DataGridViewCheckBoxCell chkNOk = (DataGridViewCheckBoxCell)row.Cells[4];
                    DataGridViewCheckBoxCell chkNa = (DataGridViewCheckBoxCell)row.Cells[5];



                    foreach (var item in ListaDeProcedimentosNA.Where(x => x.Key == idItemFormulario))
                    {
                        if (row.Cells[0].Value.ToString() == item.Value.ToString())
                            chkNa.Value = true;
                    }

                    foreach (var item in ListaDeProcedimentosOK.Where(x => x.Key == idItemFormulario))
                    {
                        if (row.Cells[0].Value.ToString() == item.Value.ToString())
                            chkOk.Value = true;
                    }

                    foreach (var item in ListaDeProcedimentosNOK.Where(x => x.Key == idItemFormulario))
                    {
                        if (row.Cells[0].Value.ToString() == item.Value.ToString())
                        {
                            chkNOk.Value = true;
                            lblPontuacaoItem.Text = "Pontos Perdidos: " + lblPeso.Text;
                        }
                    }

                }
            }
            else
                gvProcedimento.DataSource = null;

            RealizarAjustesGrid();

            AtualizarPontuacao();
        }

        private void AtualizarPontuacao()
        {
            decimal pontuacaoTotalCorrigida = 0.0m;

            foreach (var modulo in _moduloList)
            {
                pontuacaoTotalCorrigida += modulo.Valor * (modulo.Peso/100.0m);
            }


            var moduloExibido = _moduloList.Single(x => x.Nome == lblModulo.Text);

            lblPontuacaoDosModulos.Text = "Pontuação do Modulo: " + moduloExibido.Valor;

            lblPontuacao.Text = "Pontuação Total: " + pontuacaoTotalCorrigida;



        }

        private bool VerificarPreenchimento(string idItemCorrente)
        {
            bool respondeuTudo = true;

            ListaDeProcedimentosOK.RemoveAll(item => item.Key.Equals(idItemCorrente));
            ListaDeProcedimentosNOK.RemoveAll(item => item.Key.Equals(idItemCorrente));
            ListaDeProcedimentosNA.RemoveAll(item => item.Key.Equals(idItemCorrente));
            ListaPontuacaoPerdida.RemoveAll(item => item.Key.Equals(idItemCorrente));

            foreach (DataGridViewRow row in gvProcedimento.Rows)
            {
                string idProcedimento = row.Cells[0].Value.ToString();
                DataGridViewCheckBoxCell chkOk = (DataGridViewCheckBoxCell)row.Cells[3];
                DataGridViewCheckBoxCell chkNOk = (DataGridViewCheckBoxCell)row.Cells[4];
                DataGridViewCheckBoxCell chkNa = (DataGridViewCheckBoxCell)row.Cells[5];

                if ((chkOk.Value == null || chkOk.Value.ToString() == "False") && (chkNOk.Value == null || chkNOk.Value.ToString() == "False") && (chkNa.Value == null || chkNa.Value.ToString() == "False"))
                {
                    respondeuTudo = false;
                }
                else
                {
                    if (chkOk.Value != null)
                    {
                        if (chkOk.Value.ToString() == "True")
                            ListaDeProcedimentosOK.Add(new KeyValuePair<string, string>(idItemCorrente, idProcedimento));
                    }

                    if (chkNOk.Value != null)
                    {
                        if (chkNOk.Value.ToString() == "True")
                            ListaDeProcedimentosNOK.Add(new KeyValuePair<string, string>(idItemCorrente, idProcedimento));
                    }

                    if (chkNa.Value != null)
                    {
                        if (chkNa.Value.ToString() == "True")
                            ListaDeProcedimentosNA.Add(new KeyValuePair<string, string>(idItemCorrente, idProcedimento));
                    }
                }
            }

            return respondeuTudo;
        }

        public void ListarFAQ(int idItem)
        {
            DataTable dtFaq = _formularioDeQualidadeService.ListarFaqDoProcedimento(idAvaliacao, idItem);

            listbox.Items.Clear();

            string procedimento = "";

            foreach (DataRow item in dtFaq.Rows)
            {
                procedimento = item["procedimento"].ToString();

                if (procedimento.Length > 55)
                    procedimento = procedimento.Substring(0, 55) + "...";

                listbox.Items.Add(
                new ListBoxMessage.ParseMessageEventArgs(
                    ListBoxMessage.ParseMessageType.Question,
                    procedimento,
                    item["descricao"].ToString()));
            }

            listbox.Invalidate();
        }

        public void LimparAvaliacao()
        {
            txtIdCodigo.Text = "";
            txtMailing.Text = "";
            txtNomeProspect.Text = "";
            txtTelefone.Text = "";
            txtDataContato.Text = "";

            lblModulo.Text = "";
            lblItem.Text = "";
            txtDescricao.Text = "";
            gvProcedimento.DataSource = null;
            txtObervacaoAvaliador.Text = "";

            lblPontuacaoItem.Text = "Pontos Perdidos: 0";
            lblPontuacao.Visible = false;

            ListaDeProcedimentosOK.Clear();
            ListaDeProcedimentosNOK.Clear();
            ListaDeProcedimentosNA.Clear();
            ListaPontuacaoPerdida.Clear();

            btnCancelar.Visible = false;

            btnDetalheContato.Visible = false;
        }

        private void Gravar()
        {
            bool respondeuTudo = VerificarPreenchimento(idItemFormulario);

            if (!respondeuTudo)
            {
                string sMensagem = "Antes de finalizar a Avaliação todos os procedimentos devem ser respondidos.";
                MessageBox.Show(sMensagem, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            float pontuacao = float.Parse(lblPontuacao.Text.Replace("Pontuação Total: ", ""));
            string procedimentosOK = "";
            string procedimentosNOK = "";
            string procedimentosNA = "";

            foreach (var item in ListaDeProcedimentosOK)
            {
                procedimentosOK += item.Value + ",";
            }

            foreach (var item in ListaDeProcedimentosNOK)
            {
                procedimentosNOK += item.Value + ",";
            }

            foreach (var item in ListaDeProcedimentosNA)
            {
                procedimentosNA += item.Value + ",";
            }

            int idTipo = iDTipo;
            int idUsuario = AdministracaoMDI._usuario.Id;
            string observacaoAvaliacao = txtObervacaoAvaliador.Text.Replace("'", "''");

            Tabulador.Dominio.Entidades.AvaliacaoDeAtendimento _avaliacao = new Tabulador.Dominio.Entidades.AvaliacaoDeAtendimento();
            _avaliacao.id = idAvaliacao;
            _avaliacao.idTipoDeAvaliacaoDeAtendimento = (_idOfertaBKO > 0) ? 1 : idTipo;
            _avaliacao.idFormularioDeQualidade = idFormulario;
            _avaliacao.idAtendimento = idAtendimento;
            _avaliacao.pontuacao = pontuacao;
            _avaliacao.idCriador = idUsuario;
            _avaliacao.observacao = observacaoAvaliacao;

            int idRetorno = _avaliacaoDeAtendimentoService.Gravar(_avaliacao, procedimentosOK, procedimentosNOK, procedimentosNA);

            string mensagem = "";
            string retorno = "";

            atualizar = true;

            if (idAvaliacao == 0)
            {
                mensagem = "Avaliação [" + idRetorno + "] gravada com sucesso!";

                if (AdministracaoMDI._usuario.perfil.ToString().ToUpper() != "SUPERVISOR")
                {
                    //retorno = EnviarEmail();

                    //if (retorno == "SUCESSO")
                    //    mensagem = mensagem + "\r\nE-mail enviado com sucesso!";
                    //else
                    //    mensagem = mensagem + "\r\nNão foi possível enviar o e-mail!";
                }

                MessageBox.Show(mensagem, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_idOfertaBKO == 0)
                {
                    LimparAvaliacao();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    this.Hide();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Avaliação [" + idRetorno + "] atualizada com sucesso!", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                this.Close();
            }
        }

        private void GravarFeedback()
        {
            string login = txtLoginOperador.Text;
            string senha = txtSenha.Text;

            int idUsuario = _loginService.VerificarUsuarioPorLoginSenha(login, senha);

            if (idUsuario != 0)
            {
                FeedbackDaAvaliacaoDeAtendimento feedback = new FeedbackDaAvaliacaoDeAtendimento();
                feedback.idAvaliacaoDeAtendimento = idAvaliacao;
                feedback.idCriador = AdministracaoMDI._usuario.Id;

                long idFeedback = _feedbackDaAvaliacaoDeAtendimentoService.Gravar(feedback);

                gbAuditor.Visible = true;
                txtAuditor.Text = AdministracaoMDI._usuario.Nome;
                txtDataFeedback.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
                gbAssinatura.Enabled = false;

                MessageBox.Show("Feedback assinado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Login e/ou Senha inválido(s).", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Text = "";
                txtSenha.Focus();
            }
        }

        private void ExcluirFeedback()
        {
            if (MessageBox.Show("Deseja realmente excluir este feedback?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _feedbackDaAvaliacaoDeAtendimentoService.Excluir(idAvaliacao);

                MessageBox.Show("Feedback excluído com sucesso!", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                this.Close();
            }
        }

        private void CarregarDetalhesDoContato()
        {
            Prospect p = _prospectService.RetornarProspect(idProspect);

            IEnumerable<OfertaDoAtendimento> ofertas = _ofertaDoAtendimentoService.RetornarOfertasDoAtendimento(idAtendimento);

            if (ofertas != null)
            {
                OfertaDoAtendimento oferta = ofertas.FirstOrDefault();
                _oferta = _ofertaDoAtendimentoService.RetornarCobrancaAtendimentoPravaler(oferta.Id);

                if (oferta.IdTipoDeProduto == 1 || oferta.IdTipoDeProduto == 2)
                {
                    pnlOferta.Visible = true;

                    this.Location = new Point(0, 0);
                    this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                    this.Height = Screen.PrimaryScreen.Bounds.Height - 80;
                    //OfertaMigracaoPreControleTimForm form = new OfertaMigracaoPreControleTimForm(AdministracaoMDI._usuario, oferta.Id, p, null, null, false, true, oferta.IdStatusDoAcordo, null, false, exibirTodasAsDatasVencimento: true);
                    //form.TopLevel = false;
                    //form.AutoScroll = true;
                    //pnlOferta.Controls.Add(form);
                    //form.FormBorderStyle = FormBorderStyle.None;
                    //form.Show();
                }
                else if (oferta.IdTipoDeProduto == 3)
                {
                    //OfertaRentabilizacaoTimForm form = new OfertaRentabilizacaoTimForm(AdministracaoMDI._usuario, oferta.Id, p, false, true, oferta.IdStatusDoAcordo, null, false);
                    //form.ShowDialog();
                }
                else if (oferta.IdTipoDeProduto == 4)
                {
                    pnlOferta.Visible = true;

                    this.Location = new Point(0, 0);
                    this.Size = Screen.PrimaryScreen.WorkingArea.Size;
 
                    //OfertaPortabilidadeMPForm form = new OfertaPortabilidadeMPForm(AdministracaoMDI._usuario, oferta.Id, p, false, true, null, null, "", oferta.IdStatusDoAcordo, null, false);
                    //form.TopLevel = false;
                    //form.AutoScroll = true;
                    //pnlOferta.Controls.Add(form);
                    //form.FormBorderStyle = FormBorderStyle.None;
                    //form.Show();
                }
                else if (oferta.IdTipoDeProduto == 5)
                {
                    //OfertaNETPTVForm form = new OfertaNETPTVForm(AdministracaoMDI._usuario, oferta.Id, p, false, true, null, null, oferta.IdStatusDaOferta, false);
                    //form.ShowDialog();
                }
            }
        }

        private string EnviarEmail()
        {
            string result = "SUCESSO";

            try
            {
                int idUsuario = idOperadorDoContato;
                string nomeOperador = txtNomeOperador.Text;
                string site = "";
                string dataAvaliacao = txtDataContato.Text.Split(' ')[0];
                string NomeSupervisor = txtNomeSupervisor.Text;
                string pontuacao = lblPontuacao.Text.Replace("Pontuação Total: ", "");

                if (NomeSupervisor.Length > 25)
                {
                    NomeSupervisor = NomeSupervisor.Substring(0, 25);
                }

                DataTable dt = _avaliacaoDeAtendimentoService.ListarDadosNotificacao(idUsuario);

                if (dt.Rows.Count > 0)
                {
                    site = dt.Rows[0][0].ToString();
                }
                else
                {
                    result = "SITE ERRO";
                    return result;
                }

                if (nomeOperador.Length > 25)
                {
                    nomeOperador = nomeOperador.Substring(0, 25);
                }

                string Emails = AdministracaoMDI._usuario.Email + ";";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Emails = Emails + dt.Rows[i][1].ToString().Trim() + ";";
                }

                String[] DestinoEmails = Emails.Split(';');

                var smtp = new SmtpClient
                {
                    Host = "192.168.19.249",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("quality.coach@qualitycoach.net.br", "qc2017+-*"),
                    Timeout = 1000000
                };

                string htmlEmail = string.Empty;

                AlternateView avHtml = null;

                LinkedResource image = null;

                htmlEmail = "<html>" +
                        "   <body background=\"cid:IMG1\" style=\"background: url('cid:IMG1')\">";

                ///////////////////////////////////////////////////////////////////////////////////////////NOTA VERDE
                if (Convert.ToInt32(pontuacao) == 100)
                {
                    htmlEmail += "<table width=\"750\" height=\"490\">";
                    htmlEmail += "	<tbody>";
                    htmlEmail += "		<tr height=\"106\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr><td></td></tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr height=\"29\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr>";
                    htmlEmail += "							<td width=\"190\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"540\"><span style=\"font-size: 70%;\">" + nomeOperador + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += " ";
                    htmlEmail += "		<tr height=\"24\" style=\"\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr height=\"22\">";
                    htmlEmail += "							<td width=\"190\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"250\"><span style=\"font-size: 70%; white-space: nowrap;\">" + NomeSupervisor + "</span></td>";
                    htmlEmail += "							<td width=\"125\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"130\"><span style=\"font-size: 72%;\">" + txtTelefone.Text + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += " ";
                    htmlEmail += "		<tr height=\"24\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr height=\"22\">";
                    htmlEmail += "							<td width=\"190\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"218\"><span style=\"font-size: 72%; white-space: nowrap;\"></span></td>";
                    htmlEmail += "							<td width=\"156\">&nbsp;</td>";
                    htmlEmail += "							<td><span style=\"font-size: 72%; white-space: nowrap;\">" + dataAvaliacao + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr height=\"18\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr><td></td></tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr>";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr>";
                    htmlEmail += "							<td width=\"78\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"588\"><span style=\"font-size: 72%;\">" + txtObervacaoAvaliador.Text + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr height=\"200\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr><td></td></tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "	</tbody>";
                    htmlEmail += "</table>";

                    htmlEmail += ("</body></html>");

                    avHtml = AlternateView.CreateAlternateViewFromString
                    (htmlEmail, null, MediaTypeNames.Text.Html);

                    // Create a LinkedResource object for each embedded image
                    image = new LinkedResource("notaVerde.png", MediaTypeNames.Image.Jpeg);
                    image.ContentId = "IMG1";
                    avHtml.LinkedResources.Add(image);

                }
                /////////////////////////////////////////////////////////////////////////////////////////NOTA VERMELHA
                else if (Convert.ToInt32(pontuacao) == 0 && motivoNCG.Replace("\r\n", "") != "Suspeita de fraude")
                {
                    htmlEmail += "	<table width=\"750\" height=\"490\">";
                    htmlEmail += "		<tbody>";
                    htmlEmail += "			<tr height=\"107\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr><td></td></tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"28\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr>";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"483\"><span style=\"font-size: 72%;\">" + nomeOperador + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "";
                    htmlEmail += "			<tr height=\"22\" style=\"\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr height=\"22\">";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"222\"><span style=\"font-size: 72%; white-space: nowrap;\">" + NomeSupervisor + "</span></td>";
                    htmlEmail += "								<td width=\"116\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"132\"><span style=\"font-size: 72%;\">" + txtTelefone.Text + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "";
                    htmlEmail += "			<tr height=\"22\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr height=\"22\">";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"222\"><span style=\"font-size: 72%; white-space: nowrap;\"></span></td>";
                    htmlEmail += "								<td width=\"159\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"96\"><span style=\"font-size: 72%; white-space: nowrap;\">" + dataAvaliacao + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "";
                    htmlEmail += "";
                    htmlEmail += "			<tr height=\"10\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr height=\"10\">";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"483\"><span style=\"font-size: 72%; white-space: nowrap;\">" + motivoNCG.Replace("?", "") + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"16\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr><td></td></tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"39\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr>";
                    htmlEmail += "								<td width=\"78\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"588\"><span style=\"font-size: 72%; color: white;\">" + txtObervacaoAvaliador.Text + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"200\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr><td></td></tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "		</tbody>";
                    htmlEmail += "	</table>";

                    htmlEmail += ("</body></html>");

                    avHtml = AlternateView.CreateAlternateViewFromString
                    (htmlEmail, null, MediaTypeNames.Text.Html);

                    // Create a LinkedResource object for each embedded image
                    image = new LinkedResource("notaVermelha.png", MediaTypeNames.Image.Jpeg);
                    image.ContentId = "IMG1";
                    avHtml.LinkedResources.Add(image);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////NOTA PRETA
                else if (Convert.ToInt32(pontuacao) == 0 && motivoNCG.Replace("\r\n", "") == "Suspeita de fraude")
                {
                    htmlEmail += "	<table width=\"750\" height=\"490\">";
                    htmlEmail += "		<tbody>";
                    htmlEmail += "			<tr height=\"107\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr><td></td></tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"28\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr>";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"483\"><span style=\"font-size: 72%;\">" + nomeOperador + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "";
                    htmlEmail += "			<tr height=\"22\" style=\"\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr height=\"22\">";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"222\"><span style=\"font-size: 72%; white-space: nowrap;\">" + NomeSupervisor + "</span></td>";
                    htmlEmail += "								<td width=\"116\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"132\"><span style=\"font-size: 72%;\">" + txtTelefone.Text + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "";
                    htmlEmail += "			<tr height=\"22\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr height=\"22\">";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"222\"><span style=\"font-size: 72%; white-space: nowrap;\"></span></td>";
                    htmlEmail += "								<td width=\"159\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"96\"><span style=\"font-size: 72%; white-space: nowrap;\">" + dataAvaliacao + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "";
                    htmlEmail += "";
                    htmlEmail += "			<tr height=\"16\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr height=\"10\">";
                    htmlEmail += "								<td width=\"191\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"191\"><span style=\"font-size: 72%; white-space: nowrap;\">" + motivoNCG.Replace("?", "") + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"16\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr><td></td></tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"65\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr>";
                    htmlEmail += "								<td width=\"78\">&nbsp;</td>";
                    htmlEmail += "								<td width=\"586\"><span style=\"font-size: 72%; color: white;\">" + txtObervacaoAvaliador.Text + "</span></td>";
                    htmlEmail += "							</tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "			<tr height=\"200\">";
                    htmlEmail += "				<td>";
                    htmlEmail += "					<table>";
                    htmlEmail += "						<tbody>";
                    htmlEmail += "							<tr><td></td></tr>";
                    htmlEmail += "						</tbody>";
                    htmlEmail += "					</table>";
                    htmlEmail += "				</td>";
                    htmlEmail += "			</tr>";
                    htmlEmail += "		</tbody>";
                    htmlEmail += "	</table>";

                    htmlEmail += ("</body></html>");

                    avHtml = AlternateView.CreateAlternateViewFromString
                    (htmlEmail, null, MediaTypeNames.Text.Html);

                    // Create a LinkedResource object for each embedded image
                    image = new LinkedResource("notaPreta.png", MediaTypeNames.Image.Jpeg);
                    image.ContentId = "IMG1";
                    avHtml.LinkedResources.Add(image);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////NOTA AZUL
                else if (Convert.ToInt32(pontuacao) > 0 && Convert.ToInt32(pontuacao) < 100)
                {
                    htmlEmail += "<table width=\"750\" height=\"490\">";
                    htmlEmail += "	<tbody>";
                    htmlEmail += "		<tr height=\"106\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr><td></td></tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr height=\"29\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr>";
                    htmlEmail += "							<td width=\"186\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"250\"><span style=\"font-size: 70%; white-space: nowrap;\">" + nomeOperador + "</span></td>";
                    htmlEmail += "                          <td width=\"165\">&nbsp;</td>";
                    htmlEmail += "                          <td width=\"100\"><span style=\"font-size: 92%; color: white; font-weight: bold;\" >" + lblPontuacao.Text + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += " ";
                    htmlEmail += "		<tr height=\"24\" style=\"\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr height=\"22\">";
                    htmlEmail += "							<td width=\"186\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"250\"><span style=\"font-size: 70%; white-space: nowrap;\">" + NomeSupervisor + "</span></td>";
                    htmlEmail += "							<td width=\"125\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"130\"><span style=\"font-size: 72%;\">" + txtTelefone.Text + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += " ";
                    htmlEmail += "		<tr height=\"24\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr height=\"22\">";
                    htmlEmail += "							<td width=\"190\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"218\"><span style=\"font-size: 72%; white-space: nowrap;\"></span></td>";
                    htmlEmail += "							<td width=\"156\">&nbsp;</td>";
                    htmlEmail += "							<td><span style=\"font-size: 72%; white-space: nowrap;\">" + dataAvaliacao + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr height=\"18\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr><td></td></tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr>";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr>";
                    htmlEmail += "							<td width=\"75\">&nbsp;</td>";
                    htmlEmail += "							<td width=\"580\"><span style=\"font-size: 72%; color: white;\">" + txtObervacaoAvaliador.Text + "</span></td>";
                    htmlEmail += "						</tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "		<tr height=\"200\">";
                    htmlEmail += "			<td>";
                    htmlEmail += "				<table>";
                    htmlEmail += "					<tbody>";
                    htmlEmail += "						<tr><td></td></tr>";
                    htmlEmail += "					</tbody>";
                    htmlEmail += "				</table>";
                    htmlEmail += "			</td>";
                    htmlEmail += "		</tr>";
                    htmlEmail += "	</tbody>";
                    htmlEmail += "</table>";

                    htmlEmail += ("</body></html>");

                    avHtml = AlternateView.CreateAlternateViewFromString
                    (htmlEmail, null, MediaTypeNames.Text.Html);

                    // Create a LinkedResource object for each embedded image
                    image = new LinkedResource("notaAzul.png", MediaTypeNames.Image.Jpeg);
                    image.ContentId = "IMG1";
                    avHtml.LinkedResources.Add(image);
                }

                //var toadress = new MailAddress("rafael.morais@hmbtecnologia.com.br", "Rafael Morais");

                using (var message = new MailMessage()
                {
                    From = new MailAddress("quality.coach@qualitycoach.net.br"),
                    Subject = "Nota: " + lblPontuacao.Text + " - Operador: " + nomeOperador + " - Site: " + site,
                    Body = htmlEmail
                })
                {
                    for (int i = 0; i < DestinoEmails.Length; i++)
                    {
                        if (DestinoEmails[i] != "")
                            message.To.Add(DestinoEmails[i]);
                    }

                    //message.To.Add("rafael.morais@hmbtecnologia.com.br");

                    message.AlternateViews.Add(avHtml);
                    //message.To.Add(toadress);
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                result = "ERRO";
            }

            return result;
        }

        #endregion METODOS

        #region EVENTOS

        private void AvaliacaoDeAtendimentoForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarProcedimentoAnterior();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar o procedimento anterior!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarProcedimentoProximo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar o próximo procedimento!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvProcedimento_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewCheckBoxCell chkOK = (DataGridViewCheckBoxCell)gvProcedimento.Rows[e.RowIndex].Cells[3];
                    DataGridViewCheckBoxCell chkNOK = (DataGridViewCheckBoxCell)gvProcedimento.Rows[e.RowIndex].Cells[4];
                    DataGridViewCheckBoxCell chkNA = (DataGridViewCheckBoxCell)gvProcedimento.Rows[e.RowIndex].Cells[5];

                    if (e.ColumnIndex == 3)
                    {
                        if (chkOK.Value != null)
                        {
                            if (chkOK.Value.ToString() == "True")
                            {
                                chkNOK.Value = false;
                                chkNA.Value = false;
                            }
                        }
                    }
                    else if (e.ColumnIndex == 4)
                    {
                        if (chkNOK.Value != null)
                        {
                            if (chkNOK.Value.ToString() == "True")
                            {
                                chkOK.Value = false;
                                chkNA.Value = false;
                            }
                        }
                    }
                    else if (e.ColumnIndex == 5)
                    {
                        if (chkNA.Value != null)
                        {
                            if (chkNA.Value.ToString() == "True")
                            {
                                chkOK.Value = false;
                                chkNOK.Value = false;
                            }
                        }
                    }

                    bool itemNOK = false;

                    foreach (DataGridViewRow row in gvProcedimento.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[4];

                        if (chk.Value != null)
                        {
                            if (chk.Value.ToString() == "True")
                            {
                                itemNOK = true;
                                break;
                            }
                        }
                    }

                    if (itemNOK)
                        lblPontuacaoItem.Text = "Pontos Perdidos: " + lblPeso.Text;
                    else
                        lblPontuacaoItem.Text = "Pontos Perdidos: " + "0";

                    AtualizarListaDePontuaçãoPerdida();

                    CalcularPontosPerdidosPorModulo();

                    AtualizarPontuacao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível setar o Procedimento", "ERRO DO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarListaDePontuaçãoPerdida()
        {
            ListaPontuacaoPerdida.RemoveAll(item => item.Key.Equals(idItemFormulario));
            int pontuacaoItem = int.Parse(lblPontuacaoItem.Text.Replace("Pontos Perdidos: ", ""));
            ListaPontuacaoPerdida.Add(new KeyValuePair<string, int>(idItemFormulario, pontuacaoItem));
        }

        private void CalcularPontosPerdidosPorModulo()
        {
            var pontuacaoTotal = 0;

            foreach (var modulo in _moduloList)
            {
                foreach (var item in modulo.ItemList)
                {
                    if (ListaPontuacaoPerdida.Select(x => x.Key).Contains(item.Id.ToString()))
                        item.Valor = ListaPontuacaoPerdida.Single(x => x.Key == item.Id.ToString()).Value;
                }
            }

            foreach (var modulo in _moduloList)
            {
                foreach (var item in modulo.ItemList)
                {
                    pontuacaoTotal += item.Peso - item.Valor;
                }
                modulo.Valor = pontuacaoTotal;
                pontuacaoTotal = 0;
            }


        }

        private void gvProcedimento_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvProcedimento.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gvProcedimento_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gvProcedimento.CurrentCell is DataGridViewCheckBoxCell)
            {
                gvProcedimento.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //SelecionarTipo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados para o Tipo selecionado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCampanha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //SelecionarCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados para a Campanha selecionada!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //SelecionarSupervisor();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados para o Supervisor selecionado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarNovaAvaliacao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar uma nova Avaliação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                //LimparAvaliacao();
                this.Hide();
                this.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a Avaliação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a Avaliação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluirFeedback_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirFeedback();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir o Feedback!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            try
            {
                GravarFeedback();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o Feedback!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetalheContato_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlOferta.Visible == false)
                {
                    pnlOferta.Visible = true;
                    tcOferta.Visible = true;
                    CarregarDetalhesDoContato();
                }
                else
                {
                    pnlOferta.Visible = false;
                    tcOferta.Visible = false;
                    //this.AutoSize = true;
                    //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    this.Width = 845;
                    //this.StartPosition = FormStartPosition.CenterScreen;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os detalhes do Contato!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesmarcarTodos_Click(object sender, EventArgs e)
        {
            //clbStatus.SetarTodosRegistros(false);
        }

        private void btnMarcarTodos_Click(object sender, EventArgs e)
        {
            //clbStatus.SetarTodosRegistros(true);
        }

        #endregion EVENTOS
    }
}
