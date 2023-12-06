using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static CallplusUtil.Forms.CallplusFormsUtil;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class CadastroManualDeProspect : Form
    {
        public CadastroManualDeProspect(Usuario usuario, int idCampanha)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _campanhaService = new CampanhaService();
            _prospectService = new ProspectService();

            _idCampanha = idCampanha;

            _usuarioLogado = usuario;
            InitializeComponent();

        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly Usuario _usuarioLogado;
        private readonly int _idCampanha;
        private readonly CampanhaService _campanhaService;
        private readonly ProspectService _prospectService;
        long? idNovoCliente;

        private IEnumerable<Campanha> _campanhasDoUsuario;

        public bool Atualizar;

        #endregion PROPRIEDADES

        #region METODOS

        public long? CadastrarNovoCliente(int idCampanha)
        {
            ShowDialog();

            return idNovoCliente;
        }

        private void CarregarCmbCampanha()
        {
            _campanhasDoUsuario = _campanhaService.ListarCampanhasDoUsuario(_usuarioLogado.Id);
            cmbCampanha.PreencherComSelecione(_campanhasDoUsuario, x => x.Id, x => x.Nome);
            cmbCampanha.SelectedIndex = _idCampanha;
        }

        private void CarregarConfiguracoesIniciais()
        {
            CarregarCmbCampanha();
        }

        private void SalvarProspect()
        {
            if (!PodeSalvarNovoProspect()) return;

            var prospect = new Prospect();

            Campanha campanha = _campanhaService.RetornarCampanha(_idCampanha);

            prospect.IdCampanha = _idCampanha;

            if (campanha.IdMailingCadastroManual is null)
            {
                MessageBox.Show("A campanha " + cmbCampanha.Text + " não possui mailing indicação",
                    "Aviso do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            prospect.IdMailing = campanha.IdMailingCadastroManual ?? 0;

            prospect.Campo001 = txtNome.Text.ToUpper();

            if (long.TryParse(txtTelefone01.Text, out long telefone01))
            {
                prospect.Telefone01 = telefone01;
            }

            if (long.TryParse(txtTelefone02.Text, out long telefone02))
            {
                prospect.Telefone02 = telefone02;
            }

            if (long.TryParse(txtTelefone03.Text, out long telefone03))
            {
                prospect.Telefone03 = telefone03;
            }

            idNovoCliente = _prospectService.GravarProspect(prospect);

            MessageBox.Show("Registro Salvo com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }

        private bool PodeSalvarNovoProspect()
        {
            var mensagens = new List<string>();
            int idUsuario = _usuarioLogado.Id;
            int idCampanha = 0;
            int.TryParse(cmbCampanha.SelectedValue.ToString(), out idCampanha);


            if (cmbCampanha.TextoEhSelecione() || string.IsNullOrEmpty(cmbCampanha.Text))
                mensagens.Add("[Campanha] deve ser informado.");

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
                mensagens.Add("[Nome] deve ser informado.");

            if (string.IsNullOrEmpty(txtTelefone01.Text.Trim()))
                mensagens.Add("[Telefone01] deve ser informado.");

            if (!Texto.TelefonePossuiFormatoValido(txtTelefone01.Text))
            {
                mensagens.Add("[Telefone01] inválido!");
            }

            if (!string.IsNullOrEmpty(txtTelefone02.Text.Trim()) && !Texto.TelefonePossuiFormatoValido(txtTelefone02.Text))
            {
                mensagens.Add("[Telefone02] inválido!");
            }

            if (!string.IsNullOrEmpty(txtTelefone03.Text.Trim()) && !Texto.TelefonePossuiFormatoValido(txtTelefone03.Text))
            {
                mensagens.Add("[Telefone03] inválido!");
            }

            // CHAMADO 10222 
            #region Validacao cpf e data de nascimento
            //if (string.IsNullOrEmpty(txtCPF.Text.Trim()))
            //    mensagens.Add("O [CPF] deve ser informado");
            //else
            //{
            //    if (!Texto.CpfPossuiFormatoValido(txtCPF.Text))
            //        mensagens.Add("O [CPF] não é válido");
            //}

            //if (string.IsNullOrEmpty(txtDataNascimento.Text))
            //{
            //    mensagens.Add("[Data de Nascimento] deve ser informado");
            //}
            //else
            //{
            //    if (Texto.DataEhValida(txtDataNascimento.Text))
            //    {
            //        DateTime dataNascimento = DateTime.Parse(txtDataNascimento.Text);

            //        int idade = RetornarIdade(dataNascimento);

            //        if (idade < 18)
            //        {
            //            mensagens.Add("A [Idade] deve ser maior que 18 anos!");
            //        }
            //        if (RetornarIdade(dataNascimento) > 120)
            //        {
            //            mensagens.Add("A [Idade] deve ser menor que 120 anos!");
            //        }
            //    }
            //    else
            //    {
            //        mensagens.Add("A [Data de Nascimento] não é válida");
            //    }
            //} 
            #endregion

            var msgsValidacao = _prospectService.VerificarSePodeGravar(idUsuario, idCampanha);
            mensagens.AddRange(msgsValidacao);

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        #endregion METODOS

        #region EVENTOS

        private void CadastroManualDeProspect_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracoesIniciais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Ocorreum um erro inesperado ao carregar!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarProspect();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível salvar o contato!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTelefone01_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtTelefone02_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtTelefone03_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        #endregion EVENTOS      

    }
}