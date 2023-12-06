using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using NLog;

namespace Callplus.CRM.Administracao.App.Planejamento.LayoutDinamico
{
    public partial class LayoutDinamicoForm : Form
    {
        public LayoutDinamicoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            InitializeComponent();
            
        }

        #region VARIAVEIS

        private readonly ILogger _logger;
        private LayoutDeCampoDinamico _layoutDeCampoDinamico;
        public bool Atualizar = false;

        #endregion VARIAVEIS

        #region Eventos
        private void MailingForm_Load(object sender, EventArgs e)
        {
            try
            {
                DesabilitarCampos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Ocorreu erro ao carregar os dados!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, System.EventArgs e)
        {
            try
            {
                SalvarMailing();
                //Atualizar = true;
                //this.Hide();
                //this.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao salvar o Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        #endregion Eventos

        #region Metodos

        public void Editar(LayoutDeCampoDinamico layout)
        {
            if(layout == null)
                throw new ArgumentException(nameof(layout));

            _layoutDeCampoDinamico = layout;
            CarregarDadosLayout();
            containerDeLayoutDeCamposDinamicos1.CarregarLayout(layout);

            ShowDialog();
        }

        private void DesabilitarCampos()
        {
            if (_layoutDeCampoDinamico != null)
            {
                
            }
        }
        private void CarregarDadosLayout()
        {
            if (_layoutDeCampoDinamico != null)
            {
                txtNome.Text = _layoutDeCampoDinamico.Nome;
                chkAtivo.Checked = _layoutDeCampoDinamico.Ativo;
            }
        }

        private void SalvarMailing()
        {
            if (PodeSalvar())
            {
                Atualizar = true;
                this.Hide();
                this.Close();
            }
        }

        private bool PodeSalvar()
        {
            var mensagens = new List<string>();


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

        #endregion Metodos  

        private void tsEtapa_btnNovo_Click(object sender, EventArgs e)
        {

        }

        private void tsEtapa_btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void tsEtapa_btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void tsEtapa_btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}
