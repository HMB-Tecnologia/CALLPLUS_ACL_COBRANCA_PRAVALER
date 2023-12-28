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
    public partial class StatusDeAcordoForm : Form
    {
        public StatusDeAcordoForm(int idStatus, string titulo)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _statusDeOfertaService = new StatusDeAcordoService();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _campanhaService = new CampanhaService();

            if (idStatus > 0)
                _statusDeOferta = _statusDeOfertaService.RetornarStatusDaOferta(idStatus);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly StatusDeAcordoService _statusDeOfertaService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private Tabulador.Dominio.Entidades.StatusDeAcordo _statusDeOferta;
        private IEnumerable<TipoDeStatusDeAcordo> _tipoDeStatusDeOferta;
        private readonly CampanhaService _campanhaService;
        private Tabulador.Dominio.Entidades.Campanha _campanha;
        private Tabulador.Dominio.Entidades.StatusDeAtendimento _statusDeAtendimento;
        private IEnumerable<Tabulador.Dominio.Entidades.StatusDeAtendimento> _statusDeAtendimentos;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;

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
            _tipoDeStatusDeOferta = _statusDeOfertaService.ListarTipoDeStatus(true);
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

                    _statusDeOferta = new Tabulador.Dominio.Entidades.StatusDeAcordo();

                    _statusDeOferta.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _statusDeOferta.IdTipoDeStatusDeOferta = int.Parse(cmbTipoDeStatusDeOferta.SelectedValue.ToString());
                _statusDeOferta.Nome = txtNome.Text;
                _statusDeOferta.Ativo = chkAtivo.Checked;
                _statusDeOferta.Observacao = txtObservacao.Text;
                _statusDeOferta.IdModificador = AdministracaoMDI._usuario.Id;

                string idsCampanhas = RetornarCampanhas();

                _statusDeOferta.Id = _statusDeOfertaService.GravarStatusDeOferta(_statusDeOferta, idsCampanhas);

                int idProduto = 0;

                int idStatusDeAtendimento = 0;

                if (int.TryParse(cmbStatusDeAtendimento.SelectedValue.ToString(), out idStatusDeAtendimento));

                if (idStatusDeAtendimento > 0)
                    _statusDeOfertaService.GravarConfiguracaoDoStatusDeOferta(_statusDeOferta.Id, idStatusDeAtendimento);

                MessageBox.Show($"Status de Oferta {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();

                Atualizar = true;
            }
        }

        private bool AtendeRegraDeGravacao()
        {
            var mensagens = new List<string>();

            if (Convert.ToInt32(cmbTipoDeStatusDeOferta.SelectedValue) == -1)
                mensagens.Add("[Tipo da Oferta] deve ser informado!");

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
            ShowIcon = true;
            MaximizeBox = false;
            MinimizeBox = false;

            CarregarDados();

            if (_statusDeOferta != null)
            {
                CarregarCampanhas();
            }

            CarregarStatusDeAtendimento();
            ConfigurarStatusDeAtendimento();
            RealizarAjustes();
        }

        private void ConfigurarStatusDeAtendimento()
        {
            if (_statusDeOferta != null)
            {
                var configuracaoDoStatusDeOferta = _statusDeOfertaService.RetornarStatusDeAtendimento(_statusDeOferta);

                if (configuracaoDoStatusDeOferta == null)
                {
                    MessageBox.Show($"Não existe Status de Atendimento configurado para este Status de Oferta!", "Mensagem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cmbStatusDeAtendimento.SelectedValue = configuracaoDoStatusDeOferta.IdStatusDeAtendimentoPadrao.ToString();
                    _statusDeAtendimento = _statusDeAtendimentoService.RetornarStatusDoAtendimento(configuracaoDoStatusDeOferta.IdStatusDeAtendimentoPadrao ?? 0);
                }
            }
        }

        private void CarregarDados()
        {
            if (_statusDeOferta != null)
            {
                CarregarStatusDeOferta();
                CarregarCampos();
            }
            else
                CarregarStatusDeOferta();
        }

        private void CarregarStatusDeAtendimento()
        {

            _statusDeAtendimentos = _statusDeAtendimentoService.Listar(id: null, ativo: true);

            if (Convert.ToInt32(cmbTipoDeStatusDeOferta.SelectedValue.ToString()) >= 0)
                _statusDeAtendimentos = _statusDeAtendimentos.Where(status => status.IdTipoDeStatusDeAtendimento == Convert.ToInt32(cmbTipoDeStatusDeOferta.SelectedValue.ToString()));

            cmbStatusDeAtendimento.PreencherComSelecione(_statusDeAtendimentos, status => status.Id, status => status.Nome);
        }

        private void CarregarCampos()
        {
            cmbTipoDeStatusDeOferta.SelectedValue = _statusDeOferta.IdTipoDeStatusDeOferta.ToString();
            chkAtivo.Checked = _statusDeOferta.Ativo;
            txtNome.Text = _statusDeOferta.Nome;
            txtObservacao.Text = _statusDeOferta.Observacao;
        }

        private void CarregarDadosDoStatusDeAtendimento()
        {
            CarregarStatusDeOferta();

            cmbTipoDeStatusDeOferta.SelectedValue = _statusDeOferta.IdTipoDeStatusDeOferta.ToString();
            chkAtivo.Checked = _statusDeOferta.Ativo;
            txtNome.Text = _statusDeOferta.Nome;
            txtObservacao.Text = _statusDeOferta.Observacao;

        }

        private void CarregarCampanhas()
        {
            int statusDeOferta = (_statusDeOferta == null) ? -1 : (int)_statusDeOferta.Id;

            IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas = _campanhaService.Listar(ativo: true);
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAcordo> _campanhasSelecionadas = _statusDeOfertaService.RetornarCampanhasSelecionadas(statusDeOferta);

            clbCampanhas.Items.Clear();

            if (_campanhas != null)
            {
                foreach (var item in _campanhas)
                {
                    clbCampanhas.Items.Add(item.Id + " - " + item.Nome, _campanhasSelecionadas.Where(x => x.Id == item.Id).Any());
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
            cmbTipoDeStatusDeOferta.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
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

        #endregion EVENTOS

        private void CmbTipoDeStatusDeOferta_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CarregarStatusDeAtendimento();
            CarregarCampanhas();
        }

        private void LklNovoStatusDeAtendimento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja criar um Status de Atendimento com mesmo nome?", "Confirmar criação de Status de Atendimento", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                GravarStatusDeAtendimento();
                CarregarStatusDeAtendimento();
                ConfigurarNovoStatusDeAtendimento();
            }
        }

        private void ConfigurarNovoStatusDeAtendimento()
        {
            cmbStatusDeAtendimento.SelectedValue = _statusDeAtendimento.Id.ToString();
        }

        private void GravarStatusDeAtendimento()
        {
            if (AtendeRegraDeGravacao())
            {
                _statusDeAtendimento = new Tabulador.Dominio.Entidades.StatusDeAtendimento();

                bool edicao = true;

                if (_statusDeAtendimento != null)
                {
                    edicao = false;
                    _statusDeAtendimento.IdModificador = AdministracaoMDI._usuario.Id;
                }

                _statusDeAtendimento.IdTipoDeStatusDeAtendimento = int.Parse(cmbTipoDeStatusDeOferta.SelectedValue.ToString());
                _statusDeAtendimento.Nome = txtNome.Text.ToUpper();
                _statusDeAtendimento.Ativo = chkAtivo.Checked;
                _statusDeAtendimento.Observacao = txtObservacao.Text;
                _statusDeAtendimento.IdCriador = AdministracaoMDI._usuario.Id;

                string idsCampanhas = RetornarCampanhas();

                _statusDeAtendimento.Id = _statusDeAtendimentoService.GravarStatusDeAtendimento(_statusDeAtendimento, idsCampanhas);

                MessageBox.Show($"Status de atendimento {(edicao == true ? "atualizado" : "criado")} com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Atualizar = true;
            }
        }
    }
}
