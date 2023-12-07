using Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento;
using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Qualidade.NovaAvaliacaoDeAtendimentoForm
{
	public partial class NovaAvaliacaoDeAtendimento : Form
    {
        public NovaAvaliacaoDeAtendimento(string titulo, long idAvaliacao, int? idFormulario, string avaliador, bool feedbackRealizado, long idOfertaBko = 0, int idCampanha = 0)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _campanhaService = new CampanhaService();
            _feedbackDaAvaliacaoDeAtendimentoService = new FeedbackDaAvaliacaoDeAtendimentoService();
            _formularioDeQualidadeService = new FormularioDeQualidadeService();
            _loginService = new LoginService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeOfertaService();
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

        public NovaAvaliacaoDeAtendimento(string titulo, long idAvaliacao, int idFormulario, string avaliador, string auditor, string dataFeedback)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _campanhaService = new CampanhaService();
            _feedbackDaAvaliacaoDeAtendimentoService = new FeedbackDaAvaliacaoDeAtendimentoService();
            _formularioDeQualidadeService = new FormularioDeQualidadeService();
            _loginService = new LoginService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeOfertaService();
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
        
        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
        private readonly CampanhaService _campanhaService;
        private readonly FeedbackDaAvaliacaoDeAtendimentoService _feedbackDaAvaliacaoDeAtendimentoService;
        private readonly FormularioDeQualidadeService _formularioDeQualidadeService;
        private readonly LoginService _loginService;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly ProspectService _prospectService;
        private readonly StatusDeOfertaService _statusDeOfertaService;
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

        public bool atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS
        
        private void CarregarConfiguracaoInicial()
        {
            lblTitulo.Text = titulo;

            //gbAvaliacao.Location = new Point(12, 37);

            if (!modoFeedback)
            {
                this.Width = 845;
                //gbFeedback.Visible = false;
            }

            listbox = new ListBoxMessage.MessageListBox();
            listbox.Size = new Size(367, 409);
            listbox.Location = new Point(18, 34);
            listbox.AutoScroll = true;
            //this.gbFeedback.Controls.Add(listbox);

            if (auditor != "")
            {
                //if (AdministracaoMDI._usuario.IdPerfil == 5)
                //    btnExcluirFeedback.Visible = true;

                //gbAuditor.Visible = true;
                //txtAuditor.Text = auditor;
                //txtDataFeedback.Text = dataFeedback;

                //gbAssinatura.Enabled = false;
            }

            //gvProcedimento.AutoGenerateColumns = false;

            //RealizarAjustesGrid();

            if (idAvaliacao > 0)
            {
                //gbFiltro.Visible = false;
                //btnCancelar.Visible = false;

                //txtNomeAvaliador.Text = avaliador;

                CarregarAvaliacao();
            }
            else
            {
                //LimparAvaliacao();

                CarregarTipo();
                CarregarCampanhas(false);
                CarregarSupervisores(0);
                CarregarOperadores(0, 0);
                CarregarStatusDeOferta(0);
            }

            if (_idOfertaBKO > 0)
            {                
                //btnCancelar.Visible = false;
                //btnDetalheContato.Visible = false;

                if(idAvaliacao == 0)
                {
                    EncontrarContato();
                }
            }
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
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

                dataTableProcedimentoFormularioQualidade = _formularioDeQualidadeService.RetornarProcedimento(idFormulario, idCampanha);

                CarregarRespostasDaAvaliacao();

                //CarregarInicioDaAvaliacao(idCampanha, dataTable);

                if (AdministracaoMDI._usuario.IdPerfil != 1 && AdministracaoMDI._usuario.IdPerfil != 5)
                {
                    //gvProcedimento.ReadOnly = true;
                    //txtObervacaoAvaliador.ReadOnly = true;
                    //btnGravar.Visible = false;
                }

                if (modoFeedback || feedbackRealizado)
                {
                    //gvProcedimento.ReadOnly = true;
                    //txtObervacaoAvaliador.ReadOnly = true;
                    //btnGravar.Visible = false;
                    //btnDetalheContato.Visible = false;
                }

                //if (modoFeedback)
                    //ListarFAQ(int.Parse(idItemFormulario));
            }
            else
            {
                MessageBox.Show("A Avaliação não foi encontrada.", "AVISO DO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void CarregarTipo()
        {
            IEnumerable<TipoDeAvaliacaoDeAtendimento> _tipos = _avaliacaoDeAtendimentoService.ListarTipo(true);
            
            cmbTipo.PreencherComSelecione(_tipos, tipo => tipo.Id, tipo => tipo.Nome);
        }

        private void CarregarCampanhas(bool preencher)
        {
            IEnumerable<Campanha> _campanhas = _campanhaService.Listar(true);

            if (!preencher)
                _campanhas = _campanhas.Where(x => x.Id == 0);

            cmbCampanha.PreencherComSelecione(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void CarregarSupervisores(int idCampanha)
        {
            IEnumerable<Usuario> _supervisores = _usuarioService.ListarSupervisores(true, idCampanha);
            
            cmbSupervisor.PreencherComTodos(_supervisores, supervisor => supervisor.Id, supervisor => supervisor.Nome);
        }

        private void CarregarOperadores(int idCampanha, int idSupervisor)
        {
            IEnumerable<Usuario> _operadores = _usuarioService.ListarOperadores(true, idCampanha, idSupervisor);

            cmbOperador.PreencherComTodos(_operadores, operador => operador.Id, operador => operador.Nome);
        }
        
        private void CarregarStatusDeOferta(int idCampanha)
        {
            IEnumerable<StatusDeOferta> _status = _statusDeOfertaService.ListarStatusDeOferta(idCampanha, null, true);

            if (cmbTipo.Text == "VENDA")
                _status = _status.Where(x => x.IdTipoDeStatusDeOferta == 1);
            else
                _status = _status.Where(x => x.IdTipoDeStatusDeOferta != 1);

            clbStatus.Preencher(_status, x => x.Id, x => "(" + x.Id + ") " + x.Nome);
        }

        private bool ParametrosPesquisaValidos()
        {
            var mensagens = new List<string>();

            if (_idOfertaBKO == 0)
            {
                if (cmbTipo.TextoEhSelecione())
                    mensagens.Add("[Tipo] deve ser informado!");

                if (cmbCampanha.TextoEhSelecione())
                    mensagens.Add("[Campanha] deve ser informada!");

                if (dtpDataFinal.Value.Date < dtpDataInicial.Value.Date)
                    mensagens.Add("[Data Final] não pode ser menor que a Data Inicial!");

                if (clbStatus.CheckedItems.Count == 0)
                    mensagens.Add("[Status] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        
        private void EncontrarContato()
        {
            if (ParametrosPesquisaValidos())
                InstanciarNovaAvaliacao();
        }

        private void InstanciarNovaAvaliacao()
        {
            int idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);
            int idOperador = Convert.ToInt32(cmbOperador.SelectedValue);
            int idSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
            int IDTipo = Convert.ToInt32(cmbTipo.SelectedValue);
            string dataInicial = dtpDataInicial.Value.ToString("yyyy-MM-dd");
            string dataFinal = dtpDataFinal.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            int IDCampanha = idCampanha;
            int IDOperador = idOperador;
            int IDSupervisor = idSupervisor;
            string DTInicial = dataInicial;
            string DTFinal = dataFinal;
            string idStatus = "";

            foreach (object itemChecked in clbStatus.CheckedItems)
            {
                idStatus += itemChecked.ToString().Substring(1, itemChecked.ToString().IndexOf(")") - 1) + ",";
            }

            string IDStatus = idStatus;

            if (idCampanha <= 0)
                idCampanha = _idCampanhaBKO;

            DataTable dataTable = BuscarOferta();

            dataTableFormularioQualidade = _formularioDeQualidadeService.RetornarEstrutura(-1, idCampanha);

            if (dataTableFormularioQualidade.Rows.Count > 0)
            {
                idFormulario = (int)dataTableFormularioQualidade.Rows[0]["idFormulario"];

                if (dataTable.Rows.Count > 0)
                {
                    idProspect = (long)dataTable.Rows[0]["idProspect"];
                    idAtendimento = (long)dataTable.Rows[0]["idAtendimento"];

                    dataTableProcedimentoFormularioQualidade = _formularioDeQualidadeService.RetornarProcedimento(-1, idCampanha);

                    AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("NOVA AVALIAÇÃO", _idOfertaBKO, IDCampanha, IDOperador, IDSupervisor, DTInicial, DTFinal, IDStatus, IDTipo, null);

                    f.Iniciar();
                }
                else
                {
                    MessageBox.Show("Nenhum contato encontrado para os filtros informados!", "AVISO DO SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Não foi encontrado nenhum Formulário de Qualidade para os parâmetros informados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private DataTable BuscarOferta()
        {
            int idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);
            int idOperador = Convert.ToInt32(cmbOperador.SelectedValue);
            int idSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
            string dataInicial = dtpDataInicial.Value.ToString("yyyy-MM-dd");
            string dataFinal = dtpDataFinal.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            
            string idStatus = "";
            foreach (object itemChecked in clbStatus.CheckedItems)
            {
                idStatus += itemChecked.ToString().Substring(1, itemChecked.ToString().IndexOf(")") - 1) + ",";
            }

            if (idStatus == "") idStatus = "0,";

            DataTable dataTable = null;

            if (_idOfertaBKO == 0)
            {
                dataTable = _ofertaDoAtendimentoService.RetornarOfertaParaAvaliacao(idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus, null);
            }
            else
            {
                dataTable = _ofertaDoAtendimentoService.RetornarOfertaParaAvaliacaoBKO(_idOfertaBKO);
            }

            return dataTable;
        }

        public void SelecionarTipo()
        {
            cmbCampanha.SelectedIndexChanged -= cmbCampanha_SelectedIndexChanged;

            if (!cmbTipo.TextoEhSelecione())
            {
                CarregarCampanhas(true);
            }
            else
            {
                CarregarCampanhas(false);
                CarregarSupervisores(0);
                CarregarStatusDeOferta(0);
            }

            cmbCampanha.SelectedIndexChanged += cmbCampanha_SelectedIndexChanged;
        }

        public void SelecionarCampanha()
        {
            int idCampanha = 0;

            if (!cmbCampanha.TextoEhSelecione())
            {
                idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);
            }

            cmbSupervisor.SelectedIndexChanged -= cmbSupervisor_SelectedIndexChanged;

            CarregarSupervisores(idCampanha);

            cmbSupervisor.SelectedIndexChanged += cmbSupervisor_SelectedIndexChanged;

            CarregarOperadores(idCampanha, -1);

            CarregarStatusDeOferta(idCampanha);
        }

        public void SelecionarSupervisor()
        {
            int idCampanha = 0;
            int idSupervisor = -1;

            if (!cmbCampanha.TextoEhSelecione())
            {
                idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);
            }

            if (!cmbSupervisor.TextoEhTodos())
            {
                idSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
            }

            CarregarOperadores(idCampanha, idSupervisor);
        }

        private void CarregarDetalhesDoContato()
        {
            Prospect p = _prospectService.RetornarProspect(idProspect);

            IEnumerable<OfertaDoAtendimento> ofertas = _ofertaDoAtendimentoService.RetornarOfertasDoAtendimento(idAtendimento);

            if(ofertas != null)
            {
                OfertaDoAtendimento oferta = ofertas.FirstOrDefault();

                if(oferta.IdTipoDeProduto == 1 || oferta.IdTipoDeProduto == 2)
                {
                    OfertaMigracaoPreControleTimForm form = new OfertaMigracaoPreControleTimForm(AdministracaoMDI._usuario, oferta.Id, p, null, null, false, true, oferta.IdStatusDaOferta, null , false, exibirTodasAsDatasVencimento: true);

                    form.ShowDialog();
                }
                else if (oferta.IdTipoDeProduto == 3)
                {
                    OfertaRentabilizacaoTimForm form = new OfertaRentabilizacaoTimForm(AdministracaoMDI._usuario, oferta.Id, p, false, true, oferta.IdStatusDaOferta, null, false);

                    form.ShowDialog();
                }
                else if (oferta.IdTipoDeProduto == 4)
                {

                    OfertaPortabilidadeMPForm form = new OfertaPortabilidadeMPForm(AdministracaoMDI._usuario, oferta.Id, p, false, true, null, null, "", oferta.IdStatusDaOferta, null, false);

                    form.ShowDialog();
                }
                else if (oferta.IdTipoDeProduto == 5)
                {
                    OfertaNETPTVForm form = new OfertaNETPTVForm(AdministracaoMDI._usuario, oferta.Id, p, false, true, null, null, oferta.IdStatusDaOferta, null, false);

                    form.ShowDialog();
                }
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void NovaAvaliacaoDeAtendimentoForm_Load(object sender, EventArgs e)
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
        
        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SelecionarTipo();
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
                SelecionarCampanha();
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
                SelecionarSupervisor();
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
                EncontrarContato();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar uma nova Avaliação!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                
        private void btnDetalheContato_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarDetalhesDoContato();
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
            clbStatus.SetarTodosRegistros(false);
        }

        private void btnMarcarTodos_Click(object sender, EventArgs e)
        {
            clbStatus.SetarTodosRegistros(true);
        }

        #endregion EVENTOS
    }
}
