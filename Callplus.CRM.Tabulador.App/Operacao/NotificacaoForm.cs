using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class NotificacaoForm : Form
    {
        public NotificacaoForm(Usuario usuario)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _notificacaoService = new NotificacaoService();

            _usuario = usuario;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly NotificacaoService _notificacaoService;

        Usuario _usuario;
        HistoricoLeitura _historicoLeitura;
        IEnumerable<Notificacao> _notificacao = null;

        int tempoLeitura = 0;

        #endregion PROPRIEDADES

        #region MÉTODOS 

        private void CarregarConfiguracaoInicial()
        {
            CarregarNotificacao();
        }

        private void CarregarNotificacao()
        {
            _notificacao = _notificacaoService.VerificarNotificacaoDoUsuario(_usuario.Id);

            if (_notificacao.Count() > 0)
            {
                lblTituloNotificacao.Visible = true;
                lblDescricaoNotificacao.Visible = true;
                btnConfirmarLeitura.Visible = true;

                Notificacao n = _notificacao.FirstOrDefault();

                lblIdNotificacao.Text = n.Id.ToString();
                lblTituloNotificacao.Text = n.Titulo.ToString();
                lblDescricaoNotificacao.Text = n.Mensagem.ToString();

                tempoLeitura = 0;
                btnConfirmarLeitura.Enabled = false;

                lblTempo.Text = "10";
                timerLeitura.Start();
            }
            else
            {
                this.Close();
            }
        }

        #endregion MÉTODOS        

        #region EVENTOS

        private void timerLeitura_Tick(object sender, EventArgs e)
        {
            lblTempo.Text = (int.Parse(lblTempo.Text) - 1).ToString();

            tempoLeitura++;

            if (tempoLeitura >= 10)
            {
                btnConfirmarLeitura.Enabled = true;
                btnConfirmarLeitura.BackColor = Color.CornflowerBlue;

                if (int.Parse(lblTempo.Text) == 0)
                    timerLeitura.Stop();
            }
        }

        private void btnConfirmarLeitura_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblIdNotificacao.Text != "")
                {
                    _historicoLeitura = new HistoricoLeitura();

                    _historicoLeitura.IdNotificacao = int.Parse(lblIdNotificacao.Text);
                    _historicoLeitura.IdUsuario = _usuario.Id;

                    _notificacaoService.GravarHistoricoDeLeitura(_historicoLeitura);

                    lblIdNotificacao.Text = string.Empty;

                    CarregarNotificacao();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível confirmar a leitura!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotificacaoForm_Load(object sender, EventArgs e)
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

        #endregion EVENTOS
    }
}
