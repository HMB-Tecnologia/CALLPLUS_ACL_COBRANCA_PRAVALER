using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Checklist
{
    public partial class ChecklistForm : Form
    {
        public ChecklistForm(Dominio.Entidades.Checklist checklist, Form form, int idTipoDeProduto, ContainerDeLayoutDeCamposDinamicos camposDinamicos, Usuario usuario)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _checklistService = new ChecklistService();
            _campanhaService = new CampanhaService();
            _checklist = checklist;
            _form = form;
            _idTipoDeProduto = idTipoDeProduto;
            _camposDinamicos = camposDinamicos;
            _usuario = usuario;

            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly ILogger _logger;

        private readonly ChecklistService _checklistService;

        private Dominio.Entidades.Checklist _checklist;
        private IEnumerable<EtapaDoChecklist> _etapasDoChecklist;
        private IEnumerable<VariavelDoChecklist> _variavelDoChecklist;
        private Campanha campanhaPrincipal;

        private int _etapaAtual;
        private int _idTipoDeProduto;
        private Form _form;
        private Usuario _usuario;
        private ContainerDeLayoutDeCamposDinamicos _camposDinamicos;
        private readonly CampanhaService _campanhaService;

        public int _idAuditor { get; set; }

        public bool _checklistRealizado { get; set; }

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            _etapasDoChecklist = _checklistService.ListarEtapas(_checklist.id, true);
            _variavelDoChecklist = _checklistService.ListarVariaveis(_idTipoDeProduto, _checklist.idCampanha);

            _checklistRealizado = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnFinalizarChecklist.Enabled = false;

            if (_etapasDoChecklist.Count() == 0)
            {                
                btnFinalizarChecklist.Enabled = true;

                lblPagina.Text = "0 DE 0";

                lblTitulo.Text = "SEM TÍTULO";
            }
            else
            {
                CarregarEtapa(1);

                lblTitulo.Text = _checklist.titulo;
            }
        }
        
        private void CarregarEtapa(int etapa)
        {
            string textoChecklist = _etapasDoChecklist.FirstOrDefault(x => x.etapa == etapa).descricaoRtf.Replace("''", "'");

            textoChecklist = AtribuirVariaveisDaEtapa(textoChecklist);

            rtxtChecklist.Rtf = textoChecklist;

            lblPagina.Text = etapa + " DE " + _etapasDoChecklist.Count();

            _etapaAtual = etapa;

            if (etapa < _etapasDoChecklist.Count())
                btnProximo.Enabled = true;
            else
                btnProximo.Enabled = false;

            if (etapa > 1)
                btnAnterior.Enabled = true;
            else
                btnAnterior.Enabled = false;

            if(etapa == _etapasDoChecklist.Count())
            {
                btnFinalizarChecklist.Enabled = true;
            }
        }

        private string RetornarTextoDoControle(string nomeControle)
        {
            string result = null;

            Control[] lista = _form.Controls.Find(nomeControle, true);

            if (lista.Count() == 0 && _camposDinamicos != null)
            {
                lista = _camposDinamicos.Controls.Find(nomeControle, true);
            }

            if (lista.Count() > 0)
            {
                if (lista[0].ToString().ToUpper().Contains("CUSTOM"))
                {
                    result = ((App.CamposDinamicos.CustomTextBox)(lista[0])).TextBoxText;
                }
                else
                {
                    result = lista[0].Text;
                }
            }

            if (!string.IsNullOrEmpty(result))
                result = result.ToUpper().Replace("SELECIONE...", "");

            return result;
        }

        private string AtribuirVariaveisDaEtapa(string descricaoRtf)
        {
            foreach (var item in _variavelDoChecklist)
            {
                string result = RetornarTextoDoControle(item.controleDaTela);

                if (result != null)
                    descricaoRtf = descricaoRtf.Replace(item.nome, RetornarTextoDoControle(item.controleDaTela));
            }

            return descricaoRtf;
        }

        private void FinalizarChecklist()
        {
            campanhaPrincipal = _campanhaService.RetornarCampanhaPrincipalDoUsuario(_usuario.Id);
            //_campanha.TipoAuditoria
            if (campanhaPrincipal.TipoAuditoria == Dominio.Tipos.TipoDeAuditoria.OFFLINE)
            {
                _checklistRealizado = true;

                this.Close();
                return;
            }

            SolicitarPermissaoForm solicitarPemissaoForm = new SolicitarPermissaoForm(_usuario);
            var retorno = solicitarPemissaoForm.SolicitarPermissaoDeUsuario(true, true);

            if (retorno?.PermissaoConfirmada ?? false)
            {
                _checklistRealizado = true;
                _idAuditor = (int)retorno.IdUsuarioPermissao;

                this.Close();
            }


            //_checklistRealizado = true;

            //this.Close();
        }

        #endregion METODOS

        #region EVENTOS

        private void Checklist_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarEtapa(_etapaAtual - 1);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar a etapa anterior!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarEtapa(_etapaAtual + 1);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar a próxima etapa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnFinalizarChecklist_Click(object sender, EventArgs e)
        {
            try
            {
                FinalizarChecklist();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível finalizar o checklist!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        #endregion EVENTOS        
    }
}
