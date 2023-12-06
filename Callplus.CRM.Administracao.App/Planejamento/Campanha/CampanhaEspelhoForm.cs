using Callplus.CRM.Tabulador.App.Util;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Campanha
{
    public partial class CampanhaEspelhoForm : Form
    {
        public CampanhaEspelhoForm()
        {

            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _verificacaoService = new VerificacaoService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly CampanhaService _campanhaService;
        public Tabulador.Dominio.Entidades.Campanha _novaCampanha;
        private readonly VerificacaoService _verificacaoService;
        public bool Cancelar { get; set; } = true;

        #endregion PROPRIEDADES

        #region MÉTODOS

        private void CarregarConfiguracaoInicial()
        {
            txtNome.Text = string.Empty;
            txtEnderecoImportacao.Text = string.Empty;
        }

        private void Gravar()
        {
            if (PodeSalvar())
            {
                if (PodeCriarNomeCampanha())
                {
                    if (PodeCriarDiretorio())
                    {
                        if (_novaCampanha == null)
                        {
                            Cancelar = false;

                            _novaCampanha = new Tabulador.Dominio.Entidades.Campanha();
                        }

                        _novaCampanha.Nome = txtNome.Text.Trim().ToUpper();
                        _novaCampanha.EnderecoDeImportacaoDoMailing = txtEnderecoImportacao.Text.Trim().Replace(" ", "_").ToUpper();
                        _novaCampanha.Aparelhos = chkAtivo.Checked;
                        _novaCampanha.VariaveisDoScript = chkVariaveis.Checked;
                        _novaCampanha.CheckListVenda = chkChecklistVenda.Checked;
                        _novaCampanha.PlanosComparacao = chkPlanosComparacao.Checked;
                        _novaCampanha.FormularioQualidade = chkFormularioQualidade.Checked;
                        _novaCampanha.FaqAtendimento = chkFaq.Checked;
                        _novaCampanha.Ativo = chkAtivo.Checked;

                        this.Close();
                    }
                }
            }
        }

        private bool PodeCriarNomeCampanha()
        {
            var mensagens = new List<string>();
            mensagens = VerificarSePodeCriarNomeCampanha(txtNome.Text.Trim());

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool PodeCriarDiretorio()
        {
            var mensagens = new List<string>();
            mensagens = VerificarSePodeCriarDiretorio(txtEnderecoImportacao.Text.Trim().Replace(" ", "_"));

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private List<string> VerificarSePodeCriarNomeCampanha(string nomeCampanha)
        {
            var mensagens = _verificacaoService.VerificarSePodeCriarNomeCampanha(nomeCampanha);
            return mensagens;
        }

        private List<string> VerificarSePodeCriarDiretorio(string diretorio)
        {
            var mensagens = _verificacaoService.VerificarSePodeCriarDiretorio(diretorio);
            return mensagens;
        }

        private bool PodeSalvar()
        {
            List<string> mensagens = new List<string>();
            string regex = "^[a-zA-Z0-9\\s]*$";

            var formatoCorreto = new Regex(regex).IsMatch(txtNome.Text) ? true : false;
            var formatoCorretoEndereco = new Regex(regex).IsMatch(txtEnderecoImportacao.Text) ? true : false;

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser preenchido");

            if (string.IsNullOrEmpty(txtEnderecoImportacao.Text.Trim()))
                mensagens.Add("[Pasta de Importação] deve ser preenchido");

            if (!string.IsNullOrEmpty(txtNome.Text.Trim()))
                if (!formatoCorreto)
                    mensagens.Add("[Nome] não pode conter caracteres especiais!");

            if (!string.IsNullOrEmpty(txtEnderecoImportacao.Text.Trim()))
                if (!formatoCorretoEndereco)
                    mensagens.Add("[Pasta de Importação] não pode conter caracteres especiais!");

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private void ExibirForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        #endregion MÉTODOS

        #region EVENTOS

        private void CampanhaEspelho_Load(object sender, EventArgs e)
        {
            CarregarConfiguracaoInicial();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
            Hide();

            Cancelar = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível criar a campanha espelhada!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CampanhaEspelhoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtEnderecoImportacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        #endregion EVENTOS
    }
}
