using Callplus.CRM.Administracao.App.Properties;
using NLog;
using System;
using System.Drawing;
using System.Windows.Forms;
using Callplus.CRM.Administracao.App.Login;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using Callplus.CRM.Administracao.App.Relatorios;
using Callplus.CRM.Administracao.App.Dashboard;
using Callplus.CRM.Administracao.App.Backoffice.AuditoriaDeVendas;

namespace Callplus.CRM.Administracao.App
{
    public partial class PrincipalForm : Form
    {
        private Usuario _usuarioLogado;
        private PermissaoService _permissaoService;
        private BancoDeDadosService _bancoDeDadosService;

        public PrincipalForm(Usuario usuarioLogado)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _usuarioLogado = usuarioLogado;
            _permissaoService = new PermissaoService();
            _bancoDeDadosService = new BancoDeDadosService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            lblNome.Text = AdministracaoMDI._usuario.Nome;
            lblPerfil.Text = AdministracaoMDI._usuario.perfil.ToString();
            lblEmpresa.Text = AdministracaoMDI._usuario.Empresa.ToString();
            lblVersao.Text = "Versão: " + LoginForm.Release;
            lblBancoDeDados.Text = _bancoDeDadosService.RetornarNomeDoBanco();

			if (AdministracaoMDI._usuario.Empresa.Contains("VGX"))
            {
                ptbLogoEmpresa.Image = Resources.VGX_logo;
            }
            else if (AdministracaoMDI._usuario.Empresa.Contains("QUALITY"))
            {
                ptbLogoEmpresa.Image = Resources.Quality_logo;
            }

            AjustarPaineisIniciais();
            ConfigurarMenusPorPerfil(_usuarioLogado);
        }

        private void ConfigurarMenusPorPerfil(Usuario usuario)
        {
            var idPerfil = usuario.IdPerfil;
            var idUsuario = usuario.Id;
            var permissoesDeAcessoMenu = _permissaoService.PermissalDeAcessoMenuListar(idPerfil, idUsuario);

            foreach (PermissaoDeAcessoMenuDTO permissaoDeAcessoMenuDto in permissoesDeAcessoMenu)
            {
                var controles = this.Controls.Find(permissaoDeAcessoMenuDto.NomeDoControle, true);

                foreach (Control controle in  controles)
                {
                    controle.Enabled = permissaoDeAcessoMenuDto.Habilitado;
                    controle.Visible = permissaoDeAcessoMenuDto.Visivel;
                }
            }

        }

        private void AjustarPaineisIniciais()
        {
            int alturaValida = (this.Height - 55) - (pnlResumo.Location.Y + pnlResumo.Size.Height);

            int larguraValida = this.Width;

            pnlAdministracao.Location = new Point(30, (pnlResumo.Location.Y + pnlResumo.Size.Height + (alturaValida / 2) + 10));
            pnlAdministracao.Height = (this.Height - 55) - pnlAdministracao.Location.Y - 20;
            pnlAdministracao.Width = (larguraValida - 100) / 3;

            pnlRelatorio.Location = new Point(pnlAdministracao.Location.X + pnlAdministracao.Width + 20, pnlAdministracao.Location.Y);
            pnlRelatorio.Size = pnlAdministracao.Size;

            pnlDashboard.Location = new Point(pnlRelatorio.Location.X + pnlRelatorio.Width + 20, pnlRelatorio.Location.Y);
            pnlDashboard.Size = pnlRelatorio.Size;

            pnlPlanejamento.Location = new Point(30, (pnlResumo.Location.Y + pnlResumo.Size.Height + 20));
            pnlPlanejamento.Height = pnlAdministracao.Location.Y - pnlPlanejamento.Location.Y - 20;
            pnlPlanejamento.Width = pnlAdministracao.Width;

            pnlQualidade.Location = new Point(pnlPlanejamento.Location.X + pnlPlanejamento.Width + 20, pnlPlanejamento.Location.Y);
            pnlQualidade.Size = pnlPlanejamento.Size;

            pnlBackoffice.Location = new Point(pnlQualidade.Location.X + pnlQualidade.Width + 20, pnlQualidade.Location.Y);
            pnlBackoffice.Size = pnlQualidade.Size;
        }

        #endregion METODOS

        #region EVENTOS

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrincipalForm_Resize(object sender, EventArgs e)
        {
            try
            {
                AjustarPaineisIniciais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklMailing_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.Mailing.ListaMailingForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklCampanha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.Campanha.ListaCampanhaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklFaqDeAtendimento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.FaqDeAtendimento.ListarFaqDeAtendimento(_usuarioLogado));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklProduto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.Produto.ListaProdutoForm(_usuarioLogado));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklStatusAtendimento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.StatusDeAtendimento.ListaStatusDeAtendimentoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklStatusOferta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.StatusDeOferta.ListaStatusDeOfertaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklScriptAtendimento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.ScriptDeAtendimento.ListaScriptAtendimentoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklChecklistVenda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.Checklist.ListaChecklistForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklFormularioQualidade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.FormularioDeQualidade.ListaFormularioDeQualidadeForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklAvaliacaoAtendimento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.AvaliacaoDeAtendimento.ListaAvaliacaoDeAtendimentoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklFeedbackAvaliacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.AvaliacaoDeAtendimento.ListaFeedbackDeAvaliacaoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklAuditoriaVendas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Backoffice.AuditoriaDeVendas.ListaAuditoriaDeVendaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklUsuario_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Administracao.Usuario.ListaUsuarioForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklWorkForce_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.Pausa.ListaPausaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklSolicitacaoAcesso_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Administracao.SolicitacaoDeAcesso.ListaSolicitacaoDeAcessoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklPlanoParaComparacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.PlanoParaComparacao.ListaPlanoParaComparacaoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklRelTrabalhoDaOperacao1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new ContatosTrabalhadosDetalhado());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Backoffice.Notificacao.ListaNotificacaoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklStatusDeAuditoria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Backoffice.StatusDeAuditoria.ListaStatusDeAuditoriaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklFaixaDeRecarga_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.CriarFaixaDeRecarga.ListaFaixaDeRecargaForm(_usuarioLogado));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklIntegracaoBaseB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //AdministracaoMDI.ExibirForm(new Planejamento.CriarFaixaDeRecarga.ListaFaixaDeRecargaForm(_usuarioLogado));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklVencimentoDeFatura_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.VencimentoDeFatura.ListaVencimentoDeFatura());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklAuditoriaRelatorio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Backoffice.RelatorioDeAuditoria.MonitoramentoDeAuditoriaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklCadastroCep_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.CadastroCep.ListaCepForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklAlertaDeVenda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lklNivelDeConfianca_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.NivelDeConfianca.ListarNivelDeConfiancaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklNivelDeConfianca_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lklGamificacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lklGamificacao_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Backoffice.Gamificacao.ListarGamificacaoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklRelRankingDaOperacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new RankingDaOperacao());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklDashOperacoes2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new DashOperacaoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void lklDashAuditoria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new ListaAuditoriaDeVendaAgrupadaForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

		#endregion EVENTOS
	}
}
