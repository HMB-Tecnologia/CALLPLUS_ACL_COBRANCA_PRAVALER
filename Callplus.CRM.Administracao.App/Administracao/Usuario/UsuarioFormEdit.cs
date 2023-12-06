using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Administracao.Usuario
{
    public partial class UsuarioFormEdit : Form
    {
        public UsuarioFormEdit(string titulo, List<int> idsUsuarios, IEnumerable<Campanha> campanhas,
            IEnumerable<Perfil> perfis, IEnumerable<Tabulador.Dominio.Entidades.Usuario> supervisores)
        {            
            _campanhas = campanhas;
            _perfis = perfis;
            _idsUsuarios = idsUsuarios;
            _supervisores = supervisores;

            _logger = LogManager.GetCurrentClassLogger();
            _usuarioService = new UsuarioService();
            _campanhaService = new CampanhaService();

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region VARIAVEIS

        private readonly ILogger _logger;

        private readonly UsuarioService _usuarioService;
        private readonly CampanhaService _campanhaService;
                
        private IEnumerable<Campanha> _campanhas;
        private IEnumerable<Campanha> _campanhasDoUsuario;
        private IEnumerable<Perfil> _perfis;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _supervisores;

        private List<int> _idsUsuarios;

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            CarregarPerfil();
            CarregarCampanhas();
            CarregarCampanhaPrincipal();
            CarregarSupervisor();

            CarregarEstadoInicialDosControles();        
        }

        private void CarregarEstadoInicialDosControles()
        {
            clbCampanha.SetarTodosRegistros(false);
            cmbCampanhaPrincipal.ResetarComSelecione(false);
            cmbSupervisor.ResetarComSelecione(false);

            clbCampanha.Enabled = false;
            lnkMarcarTodos.Enabled = false;
            lnkDesmarcarTodos.Enabled = false;
            
            gbDadosAcesso.Enabled = false;
            btnSalvar.Enabled = false;
        }

        private void CarregarCampanhas()
        {
            if (_campanhas != null)
            {
                foreach (var item in _campanhas)
                {
                    clbCampanha.Items.Add(item.Nome, false);
                }
            }
        }

        private void CarregarCampanhaPrincipal()
        {
            if (_campanhas != null)
                cmbCampanhaPrincipal.PreencherComSelecione(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void CarregarPerfil()
        {
            if (_perfis != null)
                cmbPerfil.PreencherComSelecione(_perfis, perfil => perfil.id, perfil => perfil.nome);
        }

        private void CarregarSupervisor()
        {
            if (_supervisores != null)
                cmbSupervisor.PreencherComSelecione(_supervisores, supervisor => supervisor.Id, supervisor => supervisor.Nome);
        }

        private void ConfigurarPerfil()
        {
            CarregarEstadoInicialDosControles();

            if (cmbPerfil.Text.ToUpper() == "ADMINISTRADOR")
            {
                clbCampanha.SetarTodosRegistros(true);
                
                gbDadosAcesso.Enabled = true;                
                btnSalvar.Enabled = true;
            }
            else if (cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                clbCampanha.Enabled = true;
                lnkMarcarTodos.Enabled = true;
                lnkDesmarcarTodos.Enabled = true;                
                cmbCampanhaPrincipal.ResetarComSelecione(true);
                cmbSupervisor.ResetarComSelecione(true);

                gbDadosAcesso.Enabled = true;                
                btnSalvar.Enabled = true;
            }
            else if (cmbPerfil.Text.ToUpper() == "ADM OPERACAO")
            {
                clbCampanha.SetarTodosRegistros(true);
                                
                gbDadosAcesso.Enabled = true;                
                btnSalvar.Enabled = true;
            }
            else if (!cmbPerfil.TextoEhSelecione())
            {
                clbCampanha.Enabled = true;
                lnkMarcarTodos.Enabled = true;
                lnkDesmarcarTodos.Enabled = true;
                
                gbDadosAcesso.Enabled = true;                
                btnSalvar.Enabled = true;
            }
        }
                
        private bool AtendeRegrasDeGravacao()
        {
            bool result = true;
            StringBuilder mensagem = new StringBuilder();
            List<string> mensagens = new List<string>();

            if (clbCampanha.CheckedItems.Count == 0 && cmbPerfil.Text.ToUpper() != "ADMINISTRADOR")
            {
                mensagens.Add("Selecione a(s) Campanha(s).");
                result = false;
            }

            if ((cmbCampanhaPrincipal.Text == "SELECIONE..." || cmbCampanhaPrincipal.SelectedValue == null) && cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                mensagens.Add("Informe a Campanha Principal.");
                result = false;
            }

            if (!clbCampanha.CheckedItems.Contains(cmbCampanhaPrincipal.Text) && cmbPerfil.Text.ToUpper() == "OPERADOR" && cmbCampanhaPrincipal.Text != "SELECIONE...")
            {
                mensagens.Add("A Campanha Principal deve pertencer à lista de Campanhas.");
                result = false;
            }

            if ((cmbSupervisor.Text == "SELECIONE..." || cmbSupervisor.SelectedValue == null) && cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                mensagens.Add("Informe o Supervisor.");
                result = false;
            }

            if (mensagens.Any())
            {
                result = false;
            }

            if (result == false)
                MessageBox.Show(String.Join("\n", mensagens.ToArray()), "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {                
                int IdPerfil = Convert.ToInt32(cmbPerfil.SelectedValue);
                bool Ativo = chkAtivo.Checked;
                bool SenhaExpirada = chkSenhaExpirada.Checked;
                int IdModificador = AdministracaoMDI._usuario.Id;
                int IdSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);

                string campanha = "";

                //List<string> campanhasSelecionadas = new List<string>();
                foreach (var itemChecked in clbCampanha.CheckedItems)
                {
                    foreach (var item in _campanhas)
                    {
                        if (itemChecked.ToString() == item.Nome.ToString())
                        {
                            campanha = campanha + item.Id.ToString() + ",";
                            break;
                        }
                    }
                }

                int idCampanhaPrincipal = Convert.ToInt32(cmbCampanhaPrincipal.SelectedValue);

                _usuarioService.GravarEmMassa(string.Join(",", _idsUsuarios), IdPerfil, Ativo, SenhaExpirada, IdModificador, campanha, idCampanhaPrincipal, IdSupervisor);
                                
                MessageBox.Show("Usuários atualizados com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void UsuarioForm_Load(object sender, EventArgs e)
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
                    $"Não foi possível salvar o(s) registro(s)!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCampanhaPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clbCampanha.Items.Count; i++)
            {
                if (cmbCampanhaPrincipal.Text == clbCampanha.Items[i].ToString())
                {
                    clbCampanha.SetItemChecked(i, true);

                    break;
                }
            }
        }

        private void cmbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConfigurarPerfil();                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível aplicar as configurações do Perfil!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
               
        private void lnkMarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanha.SetarTodosRegistros(true);
        }

        private void lnkDesmarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanha.SetarTodosRegistros(false);
        }

        private void txtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        #endregion EVENTOS        
    }
}
