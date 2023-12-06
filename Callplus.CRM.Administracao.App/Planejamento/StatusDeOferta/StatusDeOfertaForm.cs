using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.StatusDeOferta
{
    public partial class StatusDeOfertaForm : Form
    {
        public StatusDeOfertaForm(int idStatus, string titulo)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _statusDeOfertaService = new StatusDeOfertaService();
            _campanhaService = new CampanhaService();

            if (idStatus > 0)
                _statusDeOferta = _statusDeOfertaService.RetornarStatusDeOferta(idStatus, -1);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly StatusDeOfertaService _statusDeOfertaService;
        private Tabulador.Dominio.Entidades.StatusDeOferta _statusDeOferta;
        private IEnumerable<TipoDeStatusDeOferta> _tipoDeStatusDeOferta;
        private readonly CampanhaService _campanhaService;
        private Tabulador.Dominio.Entidades.Campanha _campanha;

        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void CarregarStatusDeOferta()
        {
            _tipoDeStatusDeOferta = _statusDeOfertaService.ListarTipoDeStatusDeOferta(-1, true);
            cmbTipoDeStatusDeOferta.PreencherComSelecione(_tipoDeStatusDeOferta, tipoDeStatus => tipoDeStatus.Id, tipoDeStatus => tipoDeStatus.Nome);
        }

        private void Gravar()
        {
            if (AtendeRegraDeGravacao())
            {
                bool edicao = true;

                if (_statusDeOferta == null)
                {
                    edicao = false;

                    _statusDeOferta = new Tabulador.Dominio.Entidades.StatusDeOferta();

                    _statusDeOferta.IdCriador = AdministracaoMDI._usuario.Id;
                }


                _statusDeOferta.IdTipoDeStatusDeOferta = int.Parse(cmbTipoDeStatusDeOferta.SelectedValue.ToString());
                _statusDeOferta.Nome = txtNome.Text;
                _statusDeOferta.Ativo = chkAtivo.Checked;
                _statusDeOferta.Observacao = txtObservacao.Text;
                _statusDeOferta.IdModificador = AdministracaoMDI._usuario.Id;

                string idsCampanhas = RetornarCampanhas();

                _statusDeOferta.Id = _statusDeOfertaService.GravarStatusDeOferta(_statusDeOferta, idsCampanhas);

                MessageBox.Show($"Status de atendimento {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Atualizar = true;
            }
        }

        private bool AtendeRegraDeGravacao()
        {
            var mensagens = new List<string>();

            if (Convert.ToInt32(cmbTipoDeStatusDeOferta.SelectedValue) == -1)
                mensagens.Add("[Nome] deve ser informado!");

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser informado.");

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

        private void CarregarConfiguracaoInicial()
        {
            CarregarDados();

            cmbTipoDeStatusDeOferta.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            RealizarAjustes();
        }

        private void CarregarDados()
        {
            if (_statusDeOferta != null)
            {
                CarregarStatusDeOferta();

                cmbTipoDeStatusDeOferta.SelectedValue = _statusDeOferta.IdTipoDeStatusDeOferta.ToString();
                chkAtivo.Checked = _statusDeOferta.Ativo;
                txtNome.Text = _statusDeOferta.Nome;
                txtObservacao.Text = _statusDeOferta.Observacao;
          
                RetornarCampanhasSelecionadas();

            }
            else
                CarregarStatusDeOferta();

        }

        private void CarregarDadosDoStatusDeAtendimento()
        {
            CarregarStatusDeOferta();

            cmbTipoDeStatusDeOferta.SelectedValue = _statusDeOferta.IdTipoDeStatusDeOferta.ToString();
            chkAtivo.Checked = _statusDeOferta.Ativo;
            txtNome.Text = _statusDeOferta.Nome;
            txtObservacao.Text = _statusDeOferta.Observacao;

        }

        private void RetornarCampanhasSelecionadas()
        {
            int idStatusDeOferta = (int)_statusDeOferta.Id;

            IEnumerable<Tabulador.Dominio.Entidades.StatusDeOferta> retorno = _statusDeOfertaService.RetornarCampanhasSelecionadas(idStatusDeOferta);

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
            IEnumerable<Tabulador.Dominio.Entidades.Campanha> campanhas = _campanhaService.Listar(ativo: true);

            clbCampanhas.Items.Clear();

            foreach (var item in campanhas)
            {
                if (item.Selecionado)
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, true);
                else
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, false);
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

        private void RealizarAjustes()
        {
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        #endregion METODOS

        #region EVENTOS

        private void StatusDeAtendimentoForm_Load(object sender, EventArgs e)
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

                MessageBox.Show($"Não foi possível salvar o registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void lnkTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanhas.SetarTodosRegistros(check: true);
        }

        private void lnkNenhum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanhas.SetarTodosRegistros(check: false);
        }

        private void cmbTipoDeStatusDeAtendimento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CarregarCampanhas();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível carregar as Campanahs!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
