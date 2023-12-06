using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;

namespace Callplus.CRM.Administracao.App.Backoffice.Notificacao
{
    public partial class ListaNotificacaoForm : Form
    {
        public ListaNotificacaoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _usuarioService = new UsuarioService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _mailingService = new MailingService();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _statusDeOfertaService = new StatusDeOfertaService();
            _relatorioService = new RelatorioService();
            _notificacaoService = new NotificacaoService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly UsuarioService _usuarioService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private readonly StatusDeOfertaService _statusDeOfertaService;
        private readonly MailingService _mailingService;
        private readonly RelatorioService _relatorioService;
        private readonly NotificacaoService _notificacaoService;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            ShowIcon = false;
            MaximizeBox = false;
            MinimizeBox = false;

            dtpDataInicial.Value = DateTime.Today.AddDays(-7);
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();

            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                {
                    mensagens.Add("[ID] deve ser informado!");
                }
            }
            else
            {
                if (dtpDataFinal.Value.Date < dtpDataInicial.Value.Date)
                {
                    mensagens.Add("[Data Final] não pode ser menor que a Data Inicial!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            DateTime dataInicio = dtpDataInicial.Value;
            DateTime dataTermino = dtpDataFinal.Value;
            bool ativo = chkListarAtivos.Checked;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        idRegistro = int.Parse(txtBuscaRapida.Text);
                }

                dgResultado.DataSource = _notificacaoService.ListarExibicao(idRegistro, dataInicio, dataTermino, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                NotificacaoForm f = new NotificacaoForm("EDITAR NOTIFICAÇÃO", id);

                f.Iniciar();

                if (f.atualizar)
                {
                    CarregarGrid(false);
                }
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Id"].Width = 35;

            dgResultado.Columns["Data Início"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgResultado.Columns["Data Término"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void IniciarNovoRegistro()
        {
            NotificacaoForm f = new NotificacaoForm("NOVA NOTIFICAÇÃO", 0);

            f.Iniciar();

            if (f.atualizar)
            {
                CarregarGrid(false);
            }
        }

        #endregion METODOS

        #region EVENTOS  

        private void NotificacoesForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();

                btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscaRapida_Click(object sender, System.EventArgs e)
        {
            try
            {
                CarregarGrid(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a busca rápida!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarNovoRegistro();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar o novo cadastro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void dgResultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarEdicaoRegistro(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
        {
            //btnPesquisar_Click(sender, e);
            try
            {
                CarregarGrid(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS        
    }
}
