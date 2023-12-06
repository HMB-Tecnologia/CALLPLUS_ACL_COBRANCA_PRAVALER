using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Qualidade.PlanoParaComparacao
{
    public partial class PlanoParaComparacaoForm : Form
    {
        public PlanoParaComparacaoForm(string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _planoService = new PlanoPorOperadoraParaComparacaoService();

            if (id > 0)
                _plano = _planoService.Retornar(id);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly PlanoPorOperadoraParaComparacaoService _planoService;

        private PlanoPorOperadoraParaComparacao _plano;

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
            CarregarOperadoras();
            CarregarTiposDePlano();
            CarregarCampanhas();

            if (_plano != null)
            {
                CarregarDadosDoRegistro();
            }
        }

        private void CarregarOperadoras()
        {
            IEnumerable<Operadora> retorno = _planoService.ListarOperadora();
            cmbOperadora.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarCampanhas()
        {
            int idPlano = 0;

            if(_plano != null)
            {
                idPlano = _plano.id;
            }

            IEnumerable<Campanha> retorno = _campanhaService.ListarCampanhasDoPlanoParaComparacao(idPlano);

            if (retorno != null)
            {
                foreach (var item in retorno)
                {
                    if (item.Ativo)
                    {
                        clbCampanha.Items.Add(item.Id + " - " + item.Nome, true);
                    }
                    else
                    {
                        clbCampanha.Items.Add(item.Id + " - " + item.Nome, false);
                    }
                }
            }
        }

        private void CarregarTiposDePlano()
        {
            IEnumerable<TipoDePlanoPorOperadora> retorno = _planoService.ListarTipoDePlanoPorOperadora();
            cmbTipoDePlano.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarDadosDoRegistro()
        {
            cmbOperadora.SelectedValue = _plano.idOperadora.ToString();
            cmbTipoDePlano.SelectedValue = _plano.idTipoDePlano.ToString();
            txtValor.Text = _plano.valor.ToString();
            txtPlano.Text = _plano.plano;
            chkAtivo.Checked = _plano.ativo;
            txtPacoteDadosMensal.Text = _plano.pacoteDadosMensal;
            txtOfertaRedesSociais.Text = _plano.ofertaRedesSociais;
            txtVoz.Text = _plano.voz;
            txtTorpedos.Text = _plano.torpedos;
        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (cmbOperadora.TextoEhSelecione())
            {
                mensagens.Add("[Operadora] deve ser informada!");
            }

            if (cmbTipoDePlano.TextoEhSelecione())
            {
                mensagens.Add("[Tipo] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtValor.Text.Trim()))
            {
                mensagens.Add("[Valor] deve ser informado!");
            }

            decimal valor;

            if (!string.IsNullOrEmpty(txtValor.Text.Trim()) && !decimal.TryParse(txtValor.Text.Trim(), out valor))
            {
                mensagens.Add("[Valor] inválido!");
            }

            if (string.IsNullOrEmpty(txtPlano.Text.Trim()))
            {
                mensagens.Add("[Plano] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtPacoteDadosMensal.Text.Trim()))
            {
                mensagens.Add("[Pacote Dados Mensal] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtOfertaRedesSociais.Text.Trim()))
            {
                mensagens.Add("[Oferta de Redes Sociais] deve ser informada!");
            }

            if (string.IsNullOrEmpty(txtVoz.Text.Trim()))
            {
                mensagens.Add("[Voz] deve ser informada!");
            }

            if (string.IsNullOrEmpty(txtTorpedos.Text.Trim()))
            {
                mensagens.Add("[Torpedos] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_plano == null)
                {
                    edicao = false;

                    _plano = new PlanoPorOperadoraParaComparacao();

                    _plano.idCriador = AdministracaoMDI._usuario.Id;
                }

                _plano.idOperadora = Convert.ToInt32(cmbOperadora.SelectedValue);
                _plano.idTipoDePlano = Convert.ToInt32(cmbTipoDePlano.SelectedValue);
                _plano.valor = Convert.ToDecimal(txtValor.Text);
                _plano.plano = txtPlano.Text;
                _plano.ativo = chkAtivo.Checked;
                _plano.pacoteDadosMensal = txtPacoteDadosMensal.Text;
                _plano.ofertaRedesSociais = txtOfertaRedesSociais.Text;
                _plano.voz = txtVoz.Text;
                _plano.torpedos = txtTorpedos.Text;
                _plano.idModificador = AdministracaoMDI._usuario.Id;

                string campanhas = "";
                string[] splitCampanha;
                int idCampanha = 0;

                foreach (var item in clbCampanha.CheckedItems)
                {
                    splitCampanha = item.ToString().Split('-');

                    if(splitCampanha.Count() > 1)
                    {
                        if(int.TryParse(splitCampanha[0].Trim(), out idCampanha))
                        {
                            campanhas += idCampanha.ToString() + ",";
                        }
                    }
                }

                _plano.id = _planoService.Gravar(_plano, campanhas);

                if (edicao)
                {
                    MessageBox.Show("Plano [" + _plano.plano + "] atualizado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Plano [" + _plano.plano + "] criado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                atualizar = true;
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void PlanoParaComparacaoForm_Load(object sender, EventArgs e)
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
                    $"Não foi possível gravar o Plano!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
