using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Backoffice.Notificacao
{
    public partial class NotificacaoForm : Form
    {
        public NotificacaoForm(string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _usuarioService = new UsuarioService();
            _notificacaoService = new NotificacaoService();

            if (id > 0)
            {
                _notificacao = _notificacaoService.Retornar(id);
            }

            InitializeComponent();
            
            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        
        private readonly UsuarioService _usuarioService;
        private readonly NotificacaoService _notificacaoService;

        private Tabulador.Dominio.Entidades.Notificacao _notificacao;
        
        List<Usuario> listaUsuarios = new List<Usuario>();

        public bool atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {
            ShowIcon = false;
            MaximizeBox = false;
            MinimizeBox = false;

            cmbSupervisor.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            
            CarregarSupervisores();
            CarregarDados();
            CarregarHistoricoDeLeitura();
        }

        private void CarregarDados()
        {
            if (_notificacao != null)
            {
                txtTitulo.Text = _notificacao.Titulo.ToString();
                txtMensagem.Text = _notificacao.Mensagem.ToString();
                dtpDataInicial.Text = _notificacao.DataInicio.ToString();
                dtpDataFinal.Text = _notificacao.DataTermino.ToString();
                cmbSupervisor.SelectedValue = _notificacao.IdSupervisor.ToString();
                chkAtivo.Checked = _notificacao.Ativo;

                CarregarOperadores();
            }
        }

        private void CarregarSupervisores()
        {
            IEnumerable<Usuario> supervisores = _usuarioService.ListarSupervisores(ativo: true);
            cmbSupervisor.PreencherComTodosESelecione(supervisores, x => x.Id, x => x.Nome);
        }

        private void CarregarOperadores()
        {
            int idNotificacao = (_notificacao == null) ? -1 : (int)_notificacao.Id;
            int idSupervisor = int.Parse(cmbSupervisor.SelectedValue.ToString());
            
            IEnumerable<Usuario> retorno = _notificacaoService.ListarOperadoresNotificados(idNotificacao, idSupervisor);

            clbOperadores.Items.Clear();

            foreach (var item in retorno)
            {
                if (item.Selecionado)
                    clbOperadores.Items.Add(item.Id + " - " + item.Nome, true);
                else
                    clbOperadores.Items.Add(item.Id + " - " + item.Nome, false);
            }
        }

        private void CarregarHistoricoDeLeitura()
        {
            int idNotificacao = (_notificacao == null) ? 0 : (int)_notificacao.Id;

            var hist = _notificacaoService.Listar(idNotificacao);

            dgHistoricoLeitura.DataSource = hist;
        }

        private bool AtendeRegrasDeGravacao()
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtTitulo.Text))
                mensagens.Add("[Título] deve ser informado!");

            if (string.IsNullOrEmpty(txtMensagem.Text))
                mensagens.Add("[Mensagem] deve ser informada!");


            if (dtpDataFinal.Value.Date < dtpDataInicial.Value.Date)
                mensagens.Add("[Data Término] deve ser maior que a data de início!");

            if (clbOperadores.CheckedItems.Count == 0)
                mensagens.Add("[Operadores Notificação] devem ser informados!");

            if (Convert.ToInt32(cmbSupervisor.SelectedValue) == -1)
                mensagens.Add("[Supervisor] deve ser informado!");
            
            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        string RetornarUsuarios()
        {
            string ids = "";
            foreach (var item in clbOperadores.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_notificacao == null)
                {
                    edicao = false;

                    _notificacao = new Tabulador.Dominio.Entidades.Notificacao();

                    _notificacao.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _notificacao.Titulo = txtTitulo.Text;
                _notificacao.Mensagem = txtMensagem.Text.ToString();//"yyyy-MM-dd 23:59:59"
                _notificacao.DataInicio = dtpDataInicial.Value;
                _notificacao.DataTermino = dtpDataFinal.Value;
                _notificacao.IdSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
                _notificacao.Ativo = chkAtivo.Checked;
                _notificacao.IdModificador = AdministracaoMDI._usuario.Id;

                string idsOperadores = RetornarUsuarios();                
                
                _notificacao.Id = _notificacaoService.GravarNotificacao(_notificacao, idsOperadores);

                MessageBox.Show("Notificação " + ((edicao) ? "atualizada" : "incluída") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarHistoricoDeLeitura();

                atualizar = true;
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void ListaNotificacoesForm_Load(object sender, EventArgs e)
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

        private void lnkTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbOperadores.SetarTodosRegistros(check: true);
        }

        private void lnkNenhum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbOperadores.SetarTodosRegistros(check: false);
        }

        private void cmbSupervisor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CarregarOperadores();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível carregar os Operadores do Supervisor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
