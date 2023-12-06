using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.App.Login;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using Olos.SimpleSockets;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class ConsultaDeProspectForm : Form
    {
        private readonly ConsultaDeProspectService _consultaDeProspectService;
        private readonly ProspectService _prospectService;
        private readonly HistoricoAtendimentoDto _historicoAtendimentoDto;
        private readonly AtendimentoService _atendimentoService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private readonly CampanhaService _campanhaService;
        private readonly ConfiguracaoDeCampoDoLayoutDinamicoService _configuracaoDeCampoDoLayoutDinamicoService;
        private readonly Usuario _usuarioLogado;
        private Campanha _campanhaParaCadastroManual;
        private readonly ResultadoDeConsultaDeProspect _resultadoDaConsultaDeProspect;
        private readonly VerificacaoService _VerificacaoService;
        private Prospect _prospectSelecionado;
        private IEnumerable<Campanha> _campanhasDoUsuario;
        private IEnumerable<ConfiguracaoDeCampoDoLayoutDinamico> _configuracoesDeCampoDoLayoutDinamico;
        SimpleClientSocket client = new SimpleClientSocket();

        public ConsultaDeProspectForm(Usuario usuario, long ultimoProspectTrabalhado, bool ConectadoOlos)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _consultaDeProspectService = new ConsultaDeProspectService();
            _historicoAtendimentoDto = new HistoricoAtendimentoDto();
            _prospectService = new ProspectService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _campanhaService = new CampanhaService();
            _atendimentoService = new AtendimentoService();
            _configuracaoDeCampoDoLayoutDinamicoService = new ConfiguracaoDeCampoDoLayoutDinamicoService();
            _resultadoDaConsultaDeProspect = new ResultadoDeConsultaDeProspect();
            _VerificacaoService = new VerificacaoService();
            _usuarioLogado = usuario;
            _idUltimoProspectTrabalhado = ultimoProspectTrabalhado;
            _conectadoOlos = ConectadoOlos;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private long _idUltimoProspectTrabalhado;
        private bool _conectadoOlos; 


        public int? IdUsuarioPermissao { get; private set; }

        #endregion PROPRIEDADES

        #region METODOS

        private bool AtendeRegrasDePesquisa()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtTelefone.Text) && string.IsNullOrEmpty(txtIdProspect.Text) && string.IsNullOrEmpty(txtCpf.Text))
            {
                mensagens.Add("Um dos campos deve ser preenchido para realizar a consulta.");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return !mensagens.Any();
        }

        private void CarregarConfiguracaoInicial()
        {
            //btnNovoContato.Enabled = false;

            if (_idUltimoProspectTrabalhado > 0)
                btnUltimoProspectTrabalhado.Enabled = true;
            else
                btnUltimoProspectTrabalhado.Enabled = false;

            CarregarCampanhas();

            cmbCampanhaDoNovoCliente.Enabled = false;

            AtivarBtnNovoProspect();

            if (_conectadoOlos)
                btnNovoProspect.Enabled = true;
        }

        private void CarregarCampanhas()
        {
            _campanhasDoUsuario = _campanhaService.ListarCampanhasDoUsuario(_usuarioLogado.Id);
            cmbCampanhas.PreencherComSelecione(_campanhasDoUsuario, x => x.Id, x => x.Nome);
            cmbCampanhaDoNovoCliente.PreencherComSelecione(_campanhasDoUsuario, x => x.Id, x => x.Nome);

            foreach (var item in _campanhasDoUsuario)
            {
                if(item.Principal)
                {
                    cmbCampanhaDoNovoCliente.Text = item.Nome;
                    break;
                }
            }
        }

        private void CarregarFiltrosDaCampanha()
        {
            if (VerificarSeTextoDoCmbFiltrosDaCampanhaEhSelecione()) return;
            int? idLayout = RetornarIdLayoutDinamicoDaCampanha();
            _configuracoesDeCampoDoLayoutDinamico = _configuracaoDeCampoDoLayoutDinamicoService.ListarConfiguracaoDeCampoDoLayoutDinamico(idLayout.Value);
            cmbFiltrosDaCampanha.PreencherComSelecione(_configuracoesDeCampoDoLayoutDinamico, x => x.idCampo, x => x.Label);
        }

        public bool VerificarSeTextoDoCmbFiltrosDaCampanhaEhSelecione()
        {
            if (cmbCampanhas.TextoEhSelecione())
            {
                HabilitarOuDesabilitarCmbFiltroDaCampanha(false);
                HabilitarOuDesabilitarTxtFiltroEhBtnConsultaPersonalizada(false);
                return true;
            }
            else
            {
                HabilitarOuDesabilitarCmbFiltroDaCampanha(true);
                return false;
            }
        }

        public void HabilitarOuDesabilitarCmbFiltroDaCampanha(bool habilitar)
        {
            cmbFiltrosDaCampanha.ResetarComSelecione(habilitar);
            cmbFiltrosDaCampanha.Enabled = habilitar;
        }

        public void HabilitarOuDesabilitarTxtFiltroEhBtnConsultaPersonalizada(bool habilitar)
        {
            txtFitroSelecionado.Enabled = habilitar;
            btnConsultaPersonalizada.Enabled = habilitar;
            txtFitroSelecionado.Clear();
        }

        private int? RetornarIdLayoutDinamicoDaCampanha()
        {
            int idCampanha = int.Parse(cmbCampanhas.SelectedValue.ToString());
            Campanha campanha = _campanhasDoUsuario.Where(x => x.Id == idCampanha).FirstOrDefault();
            return campanha.IdLayoutCampoDinamico;
        }

        private void RealizarConsultaRapida()
        {
            if (!AtendeRegrasDePesquisa()) return;

            int idUsuario = _usuarioLogado?.Id ?? 0;
            long telefone = -1;
            long idProspect = -1;
            string cpf = "";

            if (!string.IsNullOrEmpty(txtTelefone.Text))
                telefone = long.Parse(txtTelefone.Text);

            if (!string.IsNullOrEmpty(txtIdProspect.Text))
                idProspect = long.Parse(txtIdProspect.Text);

            if (!string.IsNullOrEmpty(txtCpf.Text))
            {
                cpf = txtCpf.Text;
            }

            ResetarControles();

            var datatable = _consultaDeProspectService.PesquisarProspects(idUsuario, cpf , telefone, idProspect);
            dgResultadoPesquisa.DataSource = datatable;

            //MessageBox.Show($"Concluído!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CarregarDetalhesDoProspect(long idProspect)
        {
            LimparHistorico();

            var prospect = _prospectService.RetornarProspect(idProspect);

            if (prospect == null) return;

            _prospectSelecionado = prospect;
            Campanha campanha = _campanhaService.RetornarCampanha(prospect.IdCampanha);
            CarregarLayoutDinamicoDaCampanhaDoProspect(prospect.Id, campanha);
            CarregarHistoricoDeAtendimentosDoProspect(prospect.Id, _usuarioLogado.Id);

            ConfigurarControles(prospect, campanha);
        }

        private void ConfigurarControles(Prospect prospect, Campanha campanha)
        {
            btnNovoContato.Enabled = campanha?.HabilitarContatoManual ?? false;
        }

        private void ResetarControles()
        {
            _containerDeLayoutDinamico.LimparTodos();
            btnNovoContato.Enabled = false;
            dgResultadoPesquisa.DataSource = null;

            LimparHistorico();
        }

        private void LimparHistorico()
        {
            dgHistoricoAtendimento.DataSource = null;
            txtHistorico_operador.Text = "";
            txtHistorico_dataAtendimento.Text = "";
            txtHistorico_dataAgendamento.Text = "";
            txtHistorico_observacao.Text = "";
            txtHistorico_telefoneConectado.Text = "";
            txtHistorico_telefoneAgendamento.Text = "";
            txtHistorico_statusAtendimento.Text = "";
        }

        private void CarregarHistoricoDeAtendimentosDoProspect(long idProspect, int idUsuario)
        {
            IEnumerable<HistoricoAtendimentoDto> historico = _atendimentoService.RetornarHistoricoDeAtendimento(idProspect, idUsuario);
            dgHistoricoAtendimento.DataSource = null;
            dgHistoricoAtendimento.DataSource = historico;
        }

        private void ExbirDetalhesDoHistoricoDeAtendimento(int indice)
        {
            var historico = new HistoricoAtendimentoDto();

            historico = (HistoricoAtendimentoDto)dgHistoricoAtendimento.Rows[indice].DataBoundItem;

            txtHistorico_operador.Text = historico?.NomeOperador;
            txtHistorico_dataAtendimento.Text = historico?.DataAtendimento?.ToString("dd/MM/yyyy HH:mm:ss");
            txtHistorico_dataAgendamento.Text = historico?.DataAgendamento?.ToString("dd/MM/yyyy HH:mm:ss");
            txtHistorico_observacao.Text = historico?.Observacao;
            txtHistorico_telefoneConectado.Text = historico?.Telefone.ToString();
            txtHistorico_telefoneAgendamento.Text = historico?.TelefoneAgendamento.ToString();
            txtHistorico_statusAtendimento.Text = historico?.ResultadoInteracao;
        }

        private void CarregarLayoutDinamicoDaCampanhaDoProspect(long idProspect, Campanha campanha)
        {
            if (campanha?.IdLayoutCampoDinamico == null) return;

            int idLayout = campanha.IdLayoutCampoDinamico.Value;
            LayoutDeCampoDinamico layout = _layoutDinamicoService.RetornarLayoutDinamico(idLayout);
            _containerDeLayoutDinamico.Visible = false;
            _containerDeLayoutDinamico.CarregarLayout(layout);

            var valores = _layoutDinamicoService.ListarValoresDeCamposDinamicos(idProspect, campanha.Id);
            _containerDeLayoutDinamico.PreencherCampos(valores);
            _containerDeLayoutDinamico.Visible = true;
        }

        private void RecarregarGridAposCriarNovoProspect(long idNovoCliente)
        {
            if (!(idNovoCliente > 0)) return;

            int idUsuario = _usuarioLogado?.Id ?? 0;
            long telefone = -1;
            string cpf = "";

            if (!string.IsNullOrEmpty(txtTelefone.Text))
                telefone = long.Parse(txtTelefone.Text);

            ResetarControles();

            var datatable = _consultaDeProspectService.PesquisarProspects(idUsuario, cpf, telefone, idNovoCliente);
            dgResultadoPesquisa.DataSource = datatable;

        }

        public class ResultadoDeConsultaDeProspect
        {
            public Prospect ProspectLocalizado { get; set; }
            public bool IniciarContatoManual { get; set; }
        }

        public ResultadoDeConsultaDeProspect RealizarPesquisaParaAtendimento()
        {
            this.ShowDialog();
            return _resultadoDaConsultaDeProspect;
        }

        private void AbrirFormNovoProspect(Campanha campanha)
        {
            CadastroManualDeProspect CadastroManualDeProspect = new CadastroManualDeProspect(_usuarioLogado, campanha.Id);
            long idNovoCliente = CadastroManualDeProspect.CadastrarNovoCliente(campanha.Id);

            RecarregarGridAposCriarNovoProspect(idNovoCliente);
        }
        
        private void ValidarOlos()
        {
            var mensagens = new List<string>();

            if (!cmbCampanhaDoNovoCliente.TextoEhSelecione())
            {
                var idCampanha = Convert.ToInt32(cmbCampanhaDoNovoCliente.SelectedValue);

                if (idCampanha == 13 && _conectadoOlos) 
                {
                    btnNovoProspect.Enabled = false;
                    mensagens.Add("Não é possível adicionar um novo cliente nessa campanha.");
                }
               
                if (!_conectadoOlos && idCampanha != 13)
                {
                    btnNovoProspect.Enabled = false;
                    mensagens.Add("Só é possível adicionar um novo cliente para campanha CLARO AQUISIÇÃO TALK.");
                    CallplusFormsUtil.ExibirMensagens(mensagens);
                }
            }
        }

        private void AtivarBtnNovoProspect()
        {
            int idCampanha = 0;

            if (!cmbCampanhaDoNovoCliente.TextoEhSelecione())
            {
                idCampanha = Convert.ToInt32(cmbCampanhaDoNovoCliente.SelectedValue);
                _campanhaParaCadastroManual = _campanhaService.RetornarCampanha(idCampanha);

                if (_campanhaParaCadastroManual?.HabilitaCadastroManual == true)
                {
                    btnNovoProspect.Enabled = true;
                }
            }
            else
            {
                btnNovoProspect.Enabled = false;
            }
        }

        private void ConfigurarNovoContatoManual()
        {
            if (PodeIniciarAtendimentoManual())
            {
                _resultadoDaConsultaDeProspect.IniciarContatoManual = true;
                _resultadoDaConsultaDeProspect.ProspectLocalizado = _prospectSelecionado;

                this.Close();
            }
            else
            {
                _resultadoDaConsultaDeProspect.IniciarContatoManual = false;
            }
        }

        private bool PodeIniciarAtendimentoManual()
        {
            var mensagens = new List<string>();

            if (_prospectSelecionado == null)
            {
                mensagens.Add("É necessário selecionar um prospect para iniciar um novo atendimento.");
                CallplusFormsUtil.ExibirMensagens(mensagens);
                return false;
            }

            long idProspect = _prospectSelecionado.Id;
            int idCampanha = _prospectSelecionado.IdCampanha;
            int idUsuario = _usuarioLogado.Id;

            var msgsServ = _atendimentoService.VerificarSePodeRealizarAtendimentoManual(idProspect, idCampanha, idUsuario);

            mensagens.AddRange(msgsServ);

            var deveSolicitarPermicao = msgsServ.Any();

            if (deveSolicitarPermicao)
            {
                var podeIniciarAtendimentoManual = false;
                var resposta = MessageBox.Show(string.Join("/n", msgsServ) + ". Deseja solicitar autorização?", "Alerta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resposta == DialogResult.Yes)
                {
                    podeIniciarAtendimentoManual = _VerificacaoService.PermitirContatoManual(_usuarioLogado);
                    IdUsuarioPermissao = _VerificacaoService.IdUsuarioPermissao;
                }

                return podeIniciarAtendimentoManual;
            }
            else
            {
                return !mensagens.Any();
            }

        }

        private void VerificarSePodePesquisar()
        {
            if (cmbFiltrosDaCampanha.TextoEhSelecione())
            {
                HabilitarOuDesabilitarTxtFiltroEhBtnConsultaPersonalizada(false);
            }
            else
            {
                HabilitarOuDesabilitarTxtFiltroEhBtnConsultaPersonalizada(true);
            }
        }

        private void RealizarConsultaPersonalizada()
        {
            if (!AtendeRegrasDePesquisaPersonalizada()) return;

            int idUsuario = _usuarioLogado?.Id ?? 0;
            int idCampanha = int.Parse(cmbCampanhas.SelectedValue.ToString());
            int idCampoPesquisa = int.Parse(cmbFiltrosDaCampanha.SelectedValue.ToString());
            string valor = txtFitroSelecionado.Text;

            ResetarControles();

            var datatable = _consultaDeProspectService.PesquisarProspectsPersonalizado(idUsuario, idCampanha, idCampoPesquisa, valor);
            dgResultadoPesquisa.DataSource = datatable;
        }

        private bool AtendeRegrasDePesquisaPersonalizada()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtFitroSelecionado.Text))
            {
                mensagens.Add("O campo valor da pesquisa deve ser preenchido.");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return !mensagens.Any();
        }

        private void ConfigurarUltimoProspectTrabalhado()
        {
            if (_idUltimoProspectTrabalhado > 0)
            {
                int idUsuario = _usuarioLogado?.Id ?? 0;
                long telefone = -1;
                long idProspect = _idUltimoProspectTrabalhado;
                string cpf = "";

                if (!string.IsNullOrEmpty(txtTelefone.Text))
                    telefone = long.Parse(txtTelefone.Text);

                if (!string.IsNullOrEmpty(txtIdProspect.Text))
                    idProspect = long.Parse(txtIdProspect.Text);

                ResetarControles();

                var datatable = _consultaDeProspectService.PesquisarProspects(idUsuario, cpf, telefone, idProspect);

                dgResultadoPesquisa.DataSource = datatable;
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void ConsultaDeProspect_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Ocorreu um erro ao carregar!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                RealizarConsultaRapida();
                //MessageBox.Show($"Concluído!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }


        private void dgResultadoPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                long idProspect = long.Parse(dgResultadoPesquisa.Rows[e.RowIndex]?.Cells["Id"]?.Value.ToString());
                CarregarDetalhesDoProspect(idProspect);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os detalhes do Prospect!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovoContato_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigurarNovoContatoManual();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os detalhes do Prospect!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgHistoricoAtendimento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                ExbirDetalhesDoHistoricoDeAtendimento(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os detalhes do Histórico do Prospect!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnNovoProspect_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormNovoProspect(_campanhaParaCadastroManual);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível validar o botão de novo cadastro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtIdProspect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void cmbCampanhas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CarregarFiltrosDaCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os filtros da campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultaRapida_Click(object sender, EventArgs e)
        {
            try
            {
                RealizarConsultaRapida();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível realizar a pesquisa rápida!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultaPersonalizada_Click(object sender, EventArgs e)
        {
            try
            {
                //RealizarConsultaPersonalizada();
                //MessageBox.Show($"Concluído!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível realizar a pesquisa personalizada!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFiltrosDaCampanha_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                VerificarSePodePesquisar();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível selecionar filtro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCampanhaDoNovoCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                AtivarBtnNovoProspect();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Erro ao selecionar campanha!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUltimoProspectTrabalhado_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigurarUltimoProspectTrabalhado();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os detalhes do Prospect!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    #endregion EVENTOS

}
