using Callplus.CRM.Tabulador.Dominio.Entidades;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Callplus.CRM.Administracao.App.Qualidade.Checklist;
using Callplus.CRM.Administracao.App.Qualidade.FormularioDeQualidade;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Servico.Servicos;
using Callplus.CRM.Administracao.App.Login;
using Callplus.CRM.Administracao.App.Relatorios;
using Callplus.CRM.Administracao.App.Backoffice.AuditoriaDeVendas;
using Callplus.CRM.Administracao.App.Dashboard;

namespace Callplus.CRM.Administracao.App
{
    public partial class AdministracaoMDI : Form
    {
        public AdministracaoMDI(Usuario usuario)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _permissaoService = new PermissaoService();

            _usuario = usuario;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        public static Usuario _usuario;
        private readonly PermissaoService _permissaoService;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            this.Text = this.Text + " (Versão: " + LoginForm.Release + ")";

            mnuUsuarioLogado.Text = _usuario.Login;
            nomeToolStripMenuItem.Text = _usuario.Nome;
            perfilToolStripMenuItem.Text = _usuario.perfil.ToString();

            ConfigurarMenusPorPerfil(_usuario);

            ExibirForm(new PrincipalForm(_usuario));

            this.WindowState = FormWindowState.Maximized;
        }

        public static void ExibirForm(Form form)
        {
            Form fPai = null;

            if (ActiveForm == null)
            {
                foreach (Form item in Application.OpenForms)
                {
                    if (item.IsMdiContainer)
                    {
                        fPai = item;
                        break;
                    }
                }
            }

            if (fPai == null)
            {
                System.Threading.Thread.Sleep(50);

                foreach (Form item in Application.OpenForms)
                {
                    if (item.IsMdiContainer)
                    {
                        fPai = item;
                        break;
                    }
                }
            }

            if (fPai != null)
            {
                form.MdiParent = fPai;
                form.WindowState = FormWindowState.Maximized;
                form.Show();
            }
            else
            {
                MessageBox.Show("Não foi possível carregar a tela de operador! Tente novamente.", "Callplus");
            }
        }

        private void ConfigurarMenusPorPerfil(Usuario usuario)
        {
            var menus = mnuPrincipal;

            var idPerfil = usuario.IdPerfil;
            var idUsuario = usuario.Id;
            var permissoesDeAcessoMenu = _permissaoService.PermissalDeAcessoMenuListar(idPerfil, idUsuario);

            foreach (PermissaoDeAcessoMenuDTO permissaoDeAcessoMenuDto in permissoesDeAcessoMenu)
            {
                var itensMenu = menus.Items.Find(permissaoDeAcessoMenuDto.NomeDoControle, true);

                foreach (ToolStripItem menu in itensMenu)
                {
                    menu.Enabled = permissaoDeAcessoMenuDto.Habilitado;
                    menu.Visible = permissaoDeAcessoMenuDto.Visivel;
                }
            }
        }
        
        #endregion METODOS

        #region EVENTOS

        private void AdministracaoMDI_Load(object sender, System.EventArgs e)
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

        private void btnSair_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente fechar o Callplus?", "Aviso do sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExibirForm(new Administracao.Usuario.ListaUsuarioForm());
        }

        private void mailingsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ExibirForm(new Planejamento.Mailing.ListaMailingForm());
        }

        private void alterarSenhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Login.ResetSenhaForm(mnuUsuarioLogado.Text);
            f.ShowDialog();
        }

        private void scriptsDeAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExibirForm(new Qualidade.ScriptDeAtendimento.ListaScriptAtendimentoForm());
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.Produto.ListaProdutoForm(_usuario));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void layoutsDinâmicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExibirForm(new Planejamento.LayoutDinamico.ListaLayoutDinamico());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void pausasToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void checklistDeVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new ListaChecklistForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void formulariosDeQualidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new ListaFormularioDeQualidadeForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void auditoriaDeVendasToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void avaliaçõesDeAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void feedbacksDeAvaliaçãoToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void planosParaComparaçãoToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void faqDeAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Qualidade.FaqDeAtendimento.ListarFaqDeAtendimento(_usuario));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void trabalhoDaOperaçãoToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void statusDeAuditoriaToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void faixaDeRecargaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.CriarFaixaDeRecarga.ListaFaixaDeRecargaForm(_usuario));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void notificaçõesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void statusDeAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void integraçãoBaseBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //AdministracaoMDI.ExibirForm(new Planejamento.StatusDeAtendimento.ListaStatusDeAtendimentoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void mnuSolicitacaoDeAcesso_Click(object sender, EventArgs e)
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

        private void mnuCampanha_Click(object sender, EventArgs e)
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

        private void mnuStatusDeOferta_Click(object sender, EventArgs e)
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

        private void mnuAparelho_Click(object sender, EventArgs e)
        {
            try
            {
                AdministracaoMDI.ExibirForm(new Planejamento.Aparelho.ListaAparelhoForm());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void mnuVencimentoDeFatura_Click(object sender, EventArgs e)
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

        private void mnuMonitoramentoDeAuditoria_Click(object sender, EventArgs e)
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

        private void mnuCadastroCeps_Click(object sender, EventArgs e)
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

        private void mnuNivelDeConfianca_Click(object sender, EventArgs e)
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

        private void mnuGamificacao_Click(object sender, EventArgs e)
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

		private void mnuRelRankingDaOperacao_Click(object sender, EventArgs e)
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

		private void mnuDashAuditoriaToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void mnuDashOperacoesToolStripMenuItem_Click(object sender, EventArgs e)
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

		#endregion EVENTOS
	}
}
