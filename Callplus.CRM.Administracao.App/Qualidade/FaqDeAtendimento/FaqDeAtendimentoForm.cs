using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Qualidade.FaqDeAtendimento
{
    public partial class FaqDeAtendimentoForm : Form
    {
        public FaqDeAtendimentoForm(Usuario usuario, string titulo, int idFaqAtendimento)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _faqDeAtendimentoService = new FaqDeAtendimentoService();

            _usuarioLogado = usuario;
            
            if (idFaqAtendimento > 0)
            {
                var resultado = _faqDeAtendimentoService.Listar(idFaqAtendimento, -1, ativo: null);
                _faqDeAtendimento = resultado.FirstOrDefault();
            }

            InitializeComponent();

            lblNovoFaq.Text = titulo;
        }

        #region PROPRIEDADES 

        private readonly ILogger _logger;
        private readonly CampanhaService _campanhaService;
        private readonly FaqDeAtendimentoService _faqDeAtendimentoService;
        private Tabulador.Dominio.Entidades.FaqDeAtendimento _faqDeAtendimento;
        private IEnumerable<Campanha> _campanhas;
        private readonly Usuario _usuarioLogado;

        public bool atualizar { get; set; }
        
        #endregion

        #region METODOS

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_faqDeAtendimento == null)
                {
                    edicao = false;
                    _faqDeAtendimento = new Tabulador.Dominio.Entidades.FaqDeAtendimento();

                    _faqDeAtendimento.IdCriador = _usuarioLogado.Id;
                }

                if (!cmbCampanha.TextoEhSelecione())
                    _faqDeAtendimento.IdCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);

                _faqDeAtendimento.Pergunta = txtPergunta.Text;
                _faqDeAtendimento.Resposta = txtResposta.Text;
                _faqDeAtendimento.IdModificador = _usuarioLogado.Id;
                _faqDeAtendimento.Ativo = chkAtivo.Checked;
                _faqDeAtendimento.Id = _faqDeAtendimentoService.GravarFaqDeAtendimento(_faqDeAtendimento);

                MessageBox.Show("Faq " + ((edicao) ? "atualizado" : "incluído") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(cmbCampanha.Text) || cmbCampanha.Text == "SELECIONE...")
            {
                mensagens.Add("Favor Informar [Campanha]");
            }

            if (string.IsNullOrEmpty(txtPergunta.Text))
            {
                mensagens.Add("Favor preencher o campo [Pergunta]");
            }

            if (string.IsNullOrEmpty(txtResposta.Text))
            {
                mensagens.Add("Favor preencher o campo [Resposta]");
            }

            ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }
        
        private void CarregarConfiguracaoInicial()
        {
            if (_faqDeAtendimento != null)
            {
                CarregarDadosDoFaq();
            }
        }

        private void CarregarDadosDoFaq()
        {
            cmbCampanha.SelectedValue = _faqDeAtendimento.IdCampanha.ToString();
            txtPergunta.Text = _faqDeAtendimento.Pergunta;
            txtResposta.Text = _faqDeAtendimento.Resposta;
            chkAtivo.Checked = _faqDeAtendimento.Ativo;
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComSelecione(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        #endregion

        #region EVENTOS

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
                    $"Não foi possível salvar as informações!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FaqDeAtendimentoForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarCampanhas();
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Ocorreu um erro inesperado o carregar os dados do FAQ \n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
