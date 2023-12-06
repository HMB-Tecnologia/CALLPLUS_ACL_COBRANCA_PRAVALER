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
using Callplus.CRM.Tabulador.App.Operacao;
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
        private readonly ConsultaDeProspectForm _consultaDeProspectForm;
        private readonly int _idCampanha;
        private readonly CampanhaService _campanhaService;
        private readonly ProspectService _prospectService;
        long idNovoCliente;

        private IEnumerable<Campanha> _campanhasDoUsuario;

        public bool Atualizar;
        
        #endregion PROPRIEDADES

        #region METODOS

        public long CadastrarNovoCliente(int idCampanha)
        {
            ShowDialog();
            
            return idNovoCliente;
        }

        private void CarregarCmbCampanha()
        {
            _campanhasDoUsuario = _campanhaService.ListarCampanhasDoUsuario(_usuarioLogado.Id);
            cmbCampanha.PreencherComSelecione(_campanhasDoUsuario, x => x.Id, x => x.Nome);
            cmbCampanha.SelectedValue = _idCampanha.ToString();
        }

        private void CarregarDdds()
        {
            cmbDdd.Sorted = true;
            cmbDdd.ResetarComSelecione(habilitar: true);
        }

        private void CarregarConfiguracoesIniciais()
        {
            CarregarCmbCampanha();
            HabilitarCamposTalk();
            CarregarDdds();
        }

        private void HabilitarCamposTalk()
        {
            if (_idCampanha == 13)
            {
                txtCampaing_CD.Enabled = true;
                txtResptracking_CD.Enabled = true;
                txtCoringa1.Enabled = true;
                txtCoringa2.Enabled = true;
                txtCoringa3.Enabled = true;
                txtCoringa4.Enabled = true;
            }
        }

        private void SalvarProspect()
        {
            if (!PodeSalvarNovoProspect()) return;

           var prospect = new Prospect();
            Campanha campanha = _campanhaService.RetornarCampanha(_idCampanha);

            long telefone01 = 0;
            long telefone02 = 0;
            long telefone03 = 0;

            prospect.IdCampanha = _idCampanha;
            prospect.IdMailing = campanha.IdMailingCadastroManual ?? 0;

            prospect.Campo021 = "MALING_INDICACAO";

            //if (campanha.idTipoDaCampanha == 2)
            //{
                //prospect.Campo005 = txtNome.Text.ToUpper();
            //}
            //else
            //{
                prospect.Campo003 = txtNome.Text.ToUpper(); // Alterado para campo003 - Chamado 15979
            //}

            // TODO: Flávio - Chamado 15979 Inserido CPF no Cadastro Manual
            if (!string.IsNullOrEmpty(txtCPF.Text))
                prospect.Campo002 = txtCPF.Text.Replace(".","").Replace("-","") ;

            if (long.TryParse(txtTelefone01.Text, out telefone01))
            {
                prospect.Telefone01 = telefone01;
            }

            if (long.TryParse(txtTelefone02.Text, out telefone02))
            {
                prospect.Telefone02 = telefone02;
            }

            if (long.TryParse(txtTelefone03.Text, out telefone03))
            {
                prospect.Telefone03 = telefone03;
            }

            if (_idCampanha == 0)
            {
                prospect.Campo004 = txtCampaing_CD.Text;
                prospect.Campo039 = txtCoringa1.Text;
                prospect.Campo040 = txtCoringa2.Text;
                prospect.Campo041 = txtCoringa3.Text;
                prospect.Campo042 = txtCoringa4.Text;
                prospect.Campo043 = txtResptracking_CD.Text;
            }

            prospect.Campo080 = cmbDdd.Text;
            

            idNovoCliente = _prospectService.GravarProspect(prospect);

            //_consultaDeProspectForm.CarregarDetalhesDoProspect(idNovoCliente);

            MessageBox.Show("Registro Salvo com sucesso!","Aviso do sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
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

            if (cmbDdd.Text == "SELECIONE...")
                mensagens.Add("[DDD] válido deve ser selecionado");

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