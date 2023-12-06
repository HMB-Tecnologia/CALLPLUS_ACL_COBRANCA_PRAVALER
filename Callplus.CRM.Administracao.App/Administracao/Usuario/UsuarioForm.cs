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
    public partial class UsuarioForm : Form
    {
        public UsuarioForm(string titulo, Tabulador.Dominio.Entidades.Usuario usuario, IEnumerable<Campanha> campanhas,
            IEnumerable<Perfil> perfis, IEnumerable<Tabulador.Dominio.Entidades.Usuario> supervisores)
        {
            _usuario = usuario;
            _campanhas = campanhas;
            _perfis = perfis;
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

        private Tabulador.Dominio.Entidades.Usuario _usuario;

        private IEnumerable<Campanha> _campanhas;
        private IEnumerable<Campanha> _campanhasDoUsuario;
        private IEnumerable<Perfil> _perfis;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _supervisores;

        public bool atualizar { get; set; }

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            cmbPerfil.SelectedIndexChanged -= cmbPerfil_SelectedIndexChanged;
            cmbCampanhaPrincipal.SelectedIndexChanged -= cmbCampanhaPrincipal_SelectedIndexChanged;

            CarregarPerfil();
            //CarregarCampanhas();
            CarregarCampanhaPrincipal();
            CarregarSupervisor();
            CarregarEmpresa();
            CarregarEscalaDeTrabalho();
            
            if (_usuario != null)
            {
                cmbPerfil.SelectedValue = _usuario.IdPerfil.ToString();
                ConfigurarPerfil();
                CarregarDadosDoUsuario();
            }

            cmbPerfil.SelectedIndexChanged += cmbPerfil_SelectedIndexChanged;
            cmbCampanhaPrincipal.SelectedIndexChanged += cmbCampanhaPrincipal_SelectedIndexChanged;
        }

        private void CarregarEstadoInicialDosControles()
        {
            clbCampanha.SetarTodosRegistros(false);
            cmbCampanhaPrincipal.ResetarComSelecione(false);
            cmbEscalaDeTrabalho.ResetarComSelecione(false);
            chkExportarRelatorio.Checked = false;
            chkReceberAvaliacaoQualidade.Checked = false;

            txtNome.Resetar(false, true);
            txtEmail.Resetar(false, true);
            cmbSupervisor.ResetarComSelecione(false);

            clbCampanha.Enabled = false;
            lnkMarcarTodos.Enabled = false;
            lnkDesmarcarTodos.Enabled = false;
            chkExportarRelatorio.Enabled = false;
            chkReceberAvaliacaoQualidade.Enabled = false;

            gbDadosAcesso.Enabled = false;
            txtObservacao.Resetar(false, true);
            btnSalvar.Enabled = false;
        }

        private void CarregarCampanhas()
        {
            clbCampanha.Items.Clear();

            if (_campanhas != null)
            {
                Campanha c = _campanhas.Where(x => x.Id == Convert.ToInt32(cmbCampanhaPrincipal.SelectedValue.ToString())).FirstOrDefault();

                foreach (var item in _campanhas)
                {
                    if (item.IdDiscador == c.IdDiscador && item.idTipoDaCampanha == c.idTipoDaCampanha && item.Id != c.Id)
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

        private void CarregarEmpresa()
        {
            IEnumerable<Empresa> retorno = _usuarioService.ListarEmpresa(true);
            cmbEmpresa.PreencherComSelecione(retorno, x => x.id, x => x.nome);
        }

        private void CarregarEscalaDeTrabalho()
        {
            IEnumerable<EscalaDeTrabalho> retorno = _usuarioService.ListarEscalaDeTrabalho(true);
            cmbEscalaDeTrabalho.PreencherComSelecione(retorno, x => x.id, x => x.nome);
        }

        private void CarregarDadosDoUsuario()
        {
            txtNome.Text = _usuario.Nome;
            txtLogin.Text = _usuario.Login;
            cmbEmpresa.SelectedValue = _usuario.IdEmpresa.ToString();

            if (_usuario.CPF != null)
                txtCpf.Text = _usuario.CPF.ToString().PadLeft(11, '0');

            if (_usuario.DataNascimento != null)
                dtpDataNascimento.Value = (DateTime)_usuario.DataNascimento;
            else
                dtpDataNascimento.Value = dtpDataNascimento.MinDate;

            if (_usuario.Ativo)
            {
                chkAtivo.Checked = true;
            }
            else
            {
                chkAtivo.Checked = false;
            }

            chkSenhaExpirada.Checked = _usuario.SenhaExpirada;

            txtObservacao.Text = _usuario.Observacao;

            chkAlterarProdutoBko.Checked = _usuario.alterarProdutoBKO;

            if (cmbPerfil.Text.ToUpper() == "ADMINISTRADOR")
            {
                if (_usuario.PermiteExportacao)
                    chkExportarRelatorio.Checked = true;
                else
                    chkExportarRelatorio.Checked = false;

                if (_usuario.ReceberAvaliacaoDeQualidade)
                    chkReceberAvaliacaoQualidade.Checked = true;
                else
                    chkReceberAvaliacaoQualidade.Checked = false;

                if (_usuario.GerarNota)
                    chkGerarNotaConfianca.Checked = true;
                else
                    chkGerarNotaConfianca.Checked = false;

                txtEmail.Text = _usuario.Email;
            }
            else if (cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                _campanhasDoUsuario = _campanhaService.ListarCampanhasDoUsuario(_usuario.Id);

                var campanhaPrincipal = _campanhasDoUsuario.FirstOrDefault(x => x.Principal == true);

                if (campanhaPrincipal != null)
                {
                    cmbCampanhaPrincipal.SelectedValue = campanhaPrincipal?.Id.ToString();

                    CarregarCampanhas();

                    CarregarCampanhaDoUsuario();
                }

                cmbSupervisor.SelectedValue = _usuario.IdSupervisor.ToString();

                if (_usuario.IdEscalaDeTrabalho != null)
                {
                    cmbEscalaDeTrabalho.SelectedValue = _usuario.IdEscalaDeTrabalho.ToString();
                }
            }
            else
            {
                CarregarCampanhaDoUsuario();

                if (_usuario.PermiteExportacao)
                {
                    chkExportarRelatorio.Checked = true;
                }
                else
                {
                    chkExportarRelatorio.Checked = false;
                }

                if (_usuario.ReceberAvaliacaoDeQualidade)
                {
                    chkReceberAvaliacaoQualidade.Checked = true;
                }
                else
                {
                    chkReceberAvaliacaoQualidade.Checked = false;
                }

                txtEmail.Text = _usuario.Email;
            }
        }

        private void CarregarCampanhaDoUsuario()
        {
            clbCampanha.SetarTodosRegistros(false);

            for (int i = 0; i < this.clbCampanha.Items.Count; ++i)
            {
                foreach (var item in _campanhasDoUsuario)
                {
                    if (clbCampanha.Items[i].ToString() == item.Nome.ToString())
                        this.clbCampanha.SetItemChecked(i, true);
                }
            }
        }

        private void ConfigurarPerfil()
        {
            CarregarEstadoInicialDosControles();

            if (cmbPerfil.Text.ToUpper() == "ADMINISTRADOR")
            {
                clbCampanha.SetarTodosRegistros(true);
                chkExportarRelatorio.Enabled = true;
                chkReceberAvaliacaoQualidade.Enabled = true;
                chkAlterarProdutoBko.Enabled = true;
                chkGerarNotaConfianca.Enabled = true;

                txtNome.Enabled = true;
                txtEmail.Enabled = true;

                gbDadosAcesso.Enabled = true;
                txtObservacao.Enabled = true;
                btnSalvar.Enabled = true;
            }
            else if (cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                clbCampanha.Enabled = true;
                lnkMarcarTodos.Enabled = true;
                lnkDesmarcarTodos.Enabled = true;
                chkAlterarProdutoBko.Enabled = false;
                chkGerarNotaConfianca.Enabled = false;
                cmbCampanhaPrincipal.ResetarComSelecione(true);
                cmbEscalaDeTrabalho.ResetarComSelecione(true);

                txtNome.Enabled = true;
                cmbSupervisor.ResetarComSelecione(true);

                gbDadosAcesso.Enabled = true;
                txtObservacao.Enabled = true;
                btnSalvar.Enabled = true;
            }
            else if (cmbPerfil.Text.ToUpper() == "ADM OPERACAO")
            {
                clbCampanha.SetarTodosRegistros(true);
                chkExportarRelatorio.Enabled = true;
                chkReceberAvaliacaoQualidade.Enabled = true;
                chkAlterarProdutoBko.Enabled = false;
                chkGerarNotaConfianca.Enabled = false;

                txtNome.Enabled = true;
                txtEmail.Enabled = true;

                gbDadosAcesso.Enabled = true;
                txtObservacao.Enabled = true;
                btnSalvar.Enabled = true;
            }
            else if (!cmbPerfil.TextoEhSelecione())
            {
                clbCampanha.Enabled = true;
                lnkMarcarTodos.Enabled = true;
                lnkDesmarcarTodos.Enabled = true;
                chkExportarRelatorio.Enabled = true;
                chkReceberAvaliacaoQualidade.Enabled = true;
                chkAlterarProdutoBko.Enabled = true;
                chkGerarNotaConfianca.Enabled = true;

                txtNome.Enabled = true;
                txtEmail.Enabled = true;

                gbDadosAcesso.Enabled = true;
                txtObservacao.Enabled = true;
                btnSalvar.Enabled = true;
            }
        }

        private bool AtendeRegrasDeGravacao()
        {
            bool result = true;
            StringBuilder mensagem = new StringBuilder();
            List<string> mensagens = new List<string>();

            //if (clbCampanha.CheckedItems.Count == 0 && cmbPerfil.Text.ToUpper() != "ADMINISTRADOR")
            //{
            //    mensagens.Add("Selecione a(s) Campanha(s).");
            //    result = false;
            //}

            if ((cmbCampanhaPrincipal.Text == "SELECIONE..." || cmbCampanhaPrincipal.SelectedValue == null) && cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                mensagens.Add("Informe a Campanha Principal.");
                result = false;
            }

            //if (!clbCampanha.CheckedItems.Contains(cmbCampanhaPrincipal.Text) && cmbPerfil.Text.ToUpper() == "OPERADOR" && cmbCampanhaPrincipal.Text != "SELECIONE...")
            //{
            //    mensagens.Add("A Campanha Principal deve pertencer à lista de Campanhas.");
            //    result = false;
            //}

            if (txtNome.Text.Trim() == "")
            {
                mensagens.Add("Informe o Nome.");
                result = false;
            }

            if (txtEmail.Text.Trim() == "" && cmbPerfil.Text.ToUpper() != "OPERADOR")
            {
                mensagens.Add("Informe o E-mail.");
                result = false;
            }

            string regexMail = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                            + "@"
                                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, regexMail) && txtEmail.Text.Trim() != "")
            {
                mensagens.Add("E-mail Inválido.");
                result = false;
            }

            if (cmbEmpresa.TextoEhSelecione())
            {
                mensagens.Add("Informe a Empresa.");
                result = false;
            }

            if ((cmbSupervisor.Text == "SELECIONE..." || cmbSupervisor.SelectedValue == null) && cmbPerfil.Text.ToUpper() == "OPERADOR")
            {
                mensagens.Add("Informe o Supervisor.");
                result = false;
            }

            //bool vinculadoNET = false;

            //foreach (var item in clbCampanha.CheckedItems)
            //{
            //    if (item.ToString().Contains("NET "))
            //    {
            //        vinculadoNET = true;
            //        break;
            //    }
            //}

            if (string.IsNullOrEmpty(txtCpf.Text))
            {
                mensagens.Add("Informe o CPF.");
                result = false;
            }

            if (!string.IsNullOrEmpty(txtCpf.Text) && !Texto.CpfPossuiFormatoValido(txtCpf.Text))
            {
                mensagens.Add("CPF inválido.");
                result = false;
            }

            if (dtpDataNascimento.Value == dtpDataNascimento.MinDate)
            {
                mensagens.Add("Data de Nascimento inválida."); ;
                result = false;
            }

            if (txtLogin.Text.Trim() == "")
            {
                mensagens.Add("Informe o Login.");
                result = false;
            }

            if (txtSenha.Text.Trim() == "" && (_usuario == null || chkSenha.Checked))
            {
                mensagens.Add("Informe a Senha.");
                result = false;
            }

            var verificarSePodeGravar = _usuarioService.VerificarSeLoginExiste(txtLogin.Text, _usuario?.Id);


            mensagens.AddRange(verificarSePodeGravar);

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
                bool edicao = true;

                if (_usuario == null)
                {
                    edicao = false;
                    _usuario = new Tabulador.Dominio.Entidades.Usuario();

                    _usuario.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _usuario.IdPerfil = Convert.ToInt32(cmbPerfil.SelectedValue);
                _usuario.Nome = txtNome.Text.ToUpper();
                _usuario.Email = txtEmail.Text.ToLower();
                _usuario.IdEmpresa = Convert.ToInt32(cmbEmpresa.SelectedValue);
                _usuario.IdSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
                _usuario.IdEscalaDeTrabalho = Convert.ToInt32(cmbEscalaDeTrabalho.SelectedValue);

                _usuario.Login = txtLogin.Text;
                _usuario.Senha = txtSenha.Text;
                _usuario.PermiteExportacao = chkExportarRelatorio.Checked;
                _usuario.ReceberAvaliacaoDeQualidade = chkReceberAvaliacaoQualidade.Checked;
                _usuario.Ativo = chkAtivo.Checked;
                _usuario.SenhaExpirada = chkSenhaExpirada.Checked;

                _usuario.GerarNota = chkGerarNotaConfianca.Checked;


                if (txtCpf.Text != "")
                {
                    long cpf = 0;
                    long.TryParse(txtCpf.Text, out cpf);
                    _usuario.CPF = cpf;
                }
                else
                    _usuario.CPF = null;

                _usuario.DataNascimento = dtpDataNascimento.Value;

                _usuario.Observacao = txtObservacao.Text;

                _usuario.IdModificador = AdministracaoMDI._usuario.Id;
                _usuario.alterarProdutoBKO = chkAlterarProdutoBko.Checked;

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

                campanha = campanha + idCampanhaPrincipal + ",";

                _usuario.Id = _usuarioService.Gravar(_usuario, campanha, idCampanhaPrincipal);

                if (edicao)
                {
                    MessageBox.Show("Usuário [" + _usuario.Nome + "] atualizado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Usuário [" + _usuario.Nome + "] criado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                atualizar = true;
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
                    $"Não foi possível salvar o registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCampanhaPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbCampanhaPrincipal.TextoEhSelecione())
                CarregarCampanhas();

            //for (int i = 0; i < clbCampanha.Items.Count; i++)
            //{
            //    if (cmbCampanhaPrincipal.Text == clbCampanha.Items[i].ToString())
            //    {
            //        clbCampanha.SetItemChecked(i, true);

            //        break;    wssw
            //    }
            //}
        }

        private void cmbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConfigurarPerfil();

                if (_usuario != null)
                {
                    CarregarDadosDoUsuario();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível aplicar as configurações do Perfil!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSenha.Checked)
            {
                txtSenha.ReadOnly = false;
                txtSenha.Text = "";
            }
            else
            {
                txtSenha.ReadOnly = true;
                txtSenha.Text = "";
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
