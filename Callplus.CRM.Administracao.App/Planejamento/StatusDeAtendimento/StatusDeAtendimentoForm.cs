using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.StatusDeAtendimento
{
    public partial class StatusDeAtendimentoForm : Form
    {
        public StatusDeAtendimentoForm(int idStatus, string titulo)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _campanhaService = new CampanhaService();

            if (idStatus > 0)
                _statusDeAtendimento = _statusDeAtendimentoService.RetornarStatusDoAtendimento(idStatus);
            else
                _statusDeAtendimento = new Tabulador.Dominio.Entidades.StatusDeAtendimento();


            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private Tabulador.Dominio.Entidades.StatusDeAtendimento _statusDeAtendimento;
        private IEnumerable<TipoDeStatusDeAtendimento> _tipoDeStatusDeAtendimento;
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

        private void CarregarStatusDeAtendimento()
        {
            _tipoDeStatusDeAtendimento = _statusDeAtendimentoService.ListarTipoDeStatus(true);
            cmbTipoDeStatusDeAtendimento.PreencherComSelecione(_tipoDeStatusDeAtendimento, tipoDeStatus => tipoDeStatus.Id, tipoDeStatus => tipoDeStatus.Nome);
        }

        private void Gravar()
        {
            if (AtendeRegraDeGravacao())
            {
                bool edicao = true;

                if (_statusDeAtendimento == null)
                {
                    edicao = false;

                    _statusDeAtendimento = new Tabulador.Dominio.Entidades.StatusDeAtendimento();

                    _statusDeAtendimento.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _statusDeAtendimento.IdTipoDeStatusDeAtendimento = int.Parse(cmbTipoDeStatusDeAtendimento.SelectedValue.ToString());
                _statusDeAtendimento.Nome = txtNome.Text;
                _statusDeAtendimento.Ativo = chkAtivo.Checked;
                _statusDeAtendimento.Observacao = txtObservacao.Text;
                _statusDeAtendimento.IdModificador = AdministracaoMDI._usuario.Id;

                string idsCampanhas = RetornarCampanhas();

                _statusDeAtendimento.Id = _statusDeAtendimentoService.GravarStatusDeAtendimento(_statusDeAtendimento, idsCampanhas);

                MessageBox.Show($"Status de atendimento {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();

                Atualizar = true;
            }
        }

        private bool AtendeRegraDeGravacao()
        {
            var mensagens = new List<string>();

            if (Convert.ToInt32(cmbTipoDeStatusDeAtendimento.SelectedValue) == -1)
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

            cmbTipoDeStatusDeAtendimento.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            RealizarAjustes();
        }

        private void CarregarDados()
        {
            if (_statusDeAtendimento != null && _statusDeAtendimento.Id > 0)
            {
                CarregarStatusDeAtendimento();

                cmbTipoDeStatusDeAtendimento.SelectedValue = _statusDeAtendimento.IdTipoDeStatusDeAtendimento.ToString();
                chkAtivo.Checked = _statusDeAtendimento.Ativo;
                txtNome.Text = _statusDeAtendimento.Nome;
                txtObservacao.Text = _statusDeAtendimento.Observacao;

                CarregarCampanhas();
            }
            else
                CarregarStatusDeAtendimento();

        }

        private void CarregarDadosDoStatusDeAtendimento()
        {
            CarregarStatusDeAtendimento();

            cmbTipoDeStatusDeAtendimento.SelectedValue = _statusDeAtendimento.IdTipoDeStatusDeAtendimento.ToString();
            chkAtivo.Checked = _statusDeAtendimento.Ativo;
            txtNome.Text = _statusDeAtendimento.Nome;
            txtObservacao.Text = _statusDeAtendimento.Observacao;

        }

        private void CarregarCampanhas()
        {
            int idStatusDeAtendimento = (int)_statusDeAtendimento.Id;

            IEnumerable<Tabulador.Dominio.Entidades.Campanha> campanhas = _campanhaService.Listar(ativo: true);
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> retorno = _statusDeAtendimentoService.RetornarCampanhasSelecionadas(idStatusDeAtendimento);

            clbCampanhas.Items.Clear();

            if (campanhas != null)
            {
                foreach (var item in campanhas)
                {
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, retorno.Where(x => x.Id == item.Id).Any());
                }
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
            //this.ShowIcon = false;
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

                MessageBox.Show($"Não foi possível carregar as Campanhas!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
