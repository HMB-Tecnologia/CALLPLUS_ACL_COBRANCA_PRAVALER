using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
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

namespace Callplus.CRM.Administracao.App.Backoffice.StatusDeAuditoria
{
    public partial class StatusDeAuditoriaForm : Form
    {
        public StatusDeAuditoriaForm(string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _campanhaService = new CampanhaService();

            if (id > 0)
            {
                _status = _statusDeAuditoriaService.Retornar(id);
            }

            InitializeComponent();

            lblTitulo.Text = titulo;


        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private Tabulador.Dominio.Entidades.StatusDeAuditoria _status;
        private readonly CampanhaService _campanhaService;
        private Campanha _campanha;

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
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            CarregarDados();
        }

        private void CarregarDados()
        {
            if (_status != null)
            {
                txtNome.Text = _status.Nome.ToString();
                chkTrocaStatus.Checked = _status.HabilitaTrocaDeStatus;
                chkAprovaOferta.Checked = _status.AprovaOferta;
                chkPerminitoHumano.Checked = _status.PermitidoHumano;
                chkAtivo.Checked = _status.Ativo;

                RetornarCampanhasSelecionadas();
            }
               
        }

        private void RetornarCampanhasSelecionadas()
        {
            int idStatusAuditoria = (int)_status.Id;

            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAuditoria> retorno = _statusDeAuditoriaService.RetornarCampanhasSelecionadas(idStatusAuditoria);

            clbCampanhas.Items.Clear();

            foreach (var item in retorno)
            {
                if (item.Selecionado)
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, true);
                else
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, false);
            }
        }

        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> campanhas = _campanhaService.Listar(ativo: true);

            clbCampanhas.Items.Clear();

            foreach (var item in campanhas)
            {
                if (item.Selecionado)
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, true);
                else
                    clbCampanhas.Items.Add(item.Id + " - " +item.Nome, false);
            }
        }

        string RetornarCampanhas()
        {
            string ids = "";
            foreach (var item in clbCampanhas.CheckedItems)
            {
                string[] itemSplit = item.ToString().Split('-');

                if (itemSplit.Count() > 0)
                    ids += itemSplit[0].Trim() + ",";
            }

            return ids;
        }

        private void Gravar()
        {
            bool edicao = true;

            if (AtendeRegraDeGravacao())
            {
                if (_status == null)
                {
                    edicao = false;

                    _status = new Tabulador.Dominio.Entidades.StatusDeAuditoria();

                    _status.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _status.Nome = txtNome.Text;
                _status.HabilitaTrocaDeStatus = chkTrocaStatus.Checked;
                _status.AprovaOferta = chkAprovaOferta.Checked;
                _status.PermitidoHumano = chkPerminitoHumano.Checked;
                _status.IdCriador = AdministracaoMDI._usuario.Id;
                _status.Ativo = chkAtivo.Checked;

                string idsCampanhas = RetornarCampanhas();

                _status.Id = _statusDeAuditoriaService.GravarNotificacao(_status, idsCampanhas);

                MessageBox.Show("Notificação " + ((edicao) ? "atualizada" : "incluída") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                atualizar = true;
            }
        }

        private bool AtendeRegraDeGravacao()
        {
            var mensagens = new List<string>();

            if (clbCampanhas.CheckedItems.Count == 0)
                mensagens.Add("[Campanha] deve ser informada!");

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion MÉTODOS

        #region EVENTOS

        private void StatusDeAuditoriaForm_Load(object sender, EventArgs e)
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

        private void txtNome_Leave(object sender, EventArgs e)
        {
            try
            {
                CarregarCampanhas();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível carregar as Campanhas!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanhas.SetarTodosRegistros(check: true);
        }

        private void lnkNenhum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanhas.SetarTodosRegistros(check: false);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        #endregion EVENTOS

    }
}
