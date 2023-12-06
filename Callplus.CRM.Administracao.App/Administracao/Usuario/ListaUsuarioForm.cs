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

namespace Callplus.CRM.Administracao.App.Administracao.Usuario
{
    public partial class ListaUsuarioForm : Form
    {
        public ListaUsuarioForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _usuarioService = new UsuarioService();
            _campanhaService = new CampanhaService();
            _perfilService = new PerfilService();

            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly UsuarioService _usuarioService;
        private readonly CampanhaService _campanhaService;
        private readonly PerfilService _perfilService;
        private readonly ILogger _logger;
        private IEnumerable<Campanha> _campanhas;
        private IEnumerable<Perfil> _perfis;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _supervisores;

        private List<int> _idsUsuarios = new List<int>();

        #endregion VARIAVEIS

        #region METODOS

        private void ExibirForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            
            CarregarCampanhas();
            CarregarPerfil();
            CarregarSupervisor();
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void CarregarPerfil()
        {
            _perfis = _perfilService.Listar(true);
            cmbPerfil.PreencherComTodos(_perfis, perfil => perfil.id, perfil => perfil.nome);
        }

        private void CarregarSupervisor()
        {
            _supervisores = _usuarioService.ListarSupervisores(ativo: true, idCampanha: -1);
            //_supervisores = _supervisores.Where(x => x.IdPerfil == 3);
            cmbSupervisor.PreencherComTodos(_supervisores, supervisor => supervisor.Id, supervisor => supervisor.Nome);
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int idCampanha = -1;
            int idPerfil = -1;
            int idSupervisor = -1;
            string nome = "";
            string login = "";
            bool ativo = chkListarAtivos.Checked;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        idRegistro = int.Parse(txtBuscaRapida.Text);
                }
                else
                {
                    idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
                    idPerfil = int.Parse(cmbPerfil.SelectedValue.ToString());
                    idSupervisor = int.Parse(cmbSupervisor.SelectedValue.ToString());
                    nome = txtNome.Text.Trim();
                    login = txtLogin.Text.Trim();
                }

                dgResultado.DataSource = _usuarioService.Listar(idRegistro, idCampanha, idPerfil, idSupervisor, nome, login, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        bool existeEditar = true;
        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            for (int i = dgResultado.Columns["Data"].Index + 1; i < dgResultado.Columns.Count; i++)
            {
                dgResultado.Columns[i].Visible = false;
            }

            for (int i = 0; i < dgResultado.Columns["Data"].Index; i++)
            {
                dgResultado.Columns[i].ReadOnly = true;
            }

            DataGridViewCheckBoxColumn dgvCbcEditar = new DataGridViewCheckBoxColumn();
            if (existeEditar)
            {
                dgvCbcEditar.ValueType = typeof(bool);
                dgvCbcEditar.Name = "dgvCbcEditar";
                dgvCbcEditar.HeaderText = "Editar";
                dgvCbcEditar.ReadOnly = false;
                dgResultado.Columns.Add(dgvCbcEditar);
                dgResultado.AutoResizeColumn(dgResultado.Columns["dgvCbcEditar"].Index);
                existeEditar = false;
            }
           
        }

        private void IniciarNovoRegistro()
        {
            UsuarioForm f = new UsuarioForm("NOVO USUÁRIO", null, _campanhas, _perfis, _supervisores);

            ExibirForm(f);

            if (f.atualizar)
            {
                CarregarSupervisor();
                CarregarGrid(false);
            }
        }

        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                Tabulador.Dominio.Entidades.Usuario usuario = new Tabulador.Dominio.Entidades.Usuario();

                usuario.Protegido = (bool)dgResultado.Rows[linha].Cells["protegido"].Value;

                if(!AdministracaoMDI._usuario.Protegido)
                {
                    if(usuario.Protegido)
                    {
                        MessageBox.Show("Este usuário é protegido pelo sistema e não pode ser editado!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                }

                usuario.Id = (int)dgResultado.Rows[linha].Cells["ID"].Value;
                usuario.IdPerfil = (int)dgResultado.Rows[linha].Cells["idPerfil"].Value;
                usuario.Login = dgResultado.Rows[linha].Cells["Login"].Value.ToString();
                usuario.Nome = dgResultado.Rows[linha].Cells["Nome"].Value.ToString();
                usuario.IdSupervisor = (int)dgResultado.Rows[linha].Cells["idSupervisor"].Value;
                usuario.Email = dgResultado.Rows[linha].Cells["E-mail"].Value.ToString();
                
                if (dgResultado.Rows[linha].Cells["Cpf"].Value.ToString() != "")
                {
                    usuario.CPF = (long)dgResultado.Rows[linha].Cells["Cpf"].Value;
                }

                if (dgResultado.Rows[linha].Cells["DataNascimento"].Value.ToString() != "")
                {
                    usuario.DataNascimento = (DateTime)dgResultado.Rows[linha].Cells["DataNascimento"].Value;
                }

                if (dgResultado.Rows[linha].Cells["idEscalaDeTrabalho"].Value.ToString() != "")
                {
                    usuario.IdEscalaDeTrabalho = (int)dgResultado.Rows[linha].Cells["idEscalaDeTrabalho"].Value;
                }

                if (dgResultado.Rows[linha].Cells["idEmpresa"].Value.ToString() != "")
                {
                    usuario.IdEmpresa = (int)dgResultado.Rows[linha].Cells["idEmpresa"].Value;
                }

                if (dgResultado.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM")
                {
                    usuario.Ativo = true;
                }
                else
                {
                    usuario.Ativo = false;
                }

                if (dgResultado.Rows[linha].Cells["Exportação"].Value.ToString().ToUpper() == "SIM")
                {
                    usuario.PermiteExportacao = true;
                }
                else
                {
                    usuario.PermiteExportacao = false;
                }

                if (dgResultado.Rows[linha].Cells["Avaliação de Qualidade"].Value.ToString().ToUpper() == "SIM")
                {
                    usuario.ReceberAvaliacaoDeQualidade = true;
                }
                else
                {
                    usuario.ReceberAvaliacaoDeQualidade = false;
                }

                usuario.GerarNota = (bool)dgResultado.Rows[linha].Cells["gerarNota"].Value;
                usuario.SenhaExpirada = (bool)dgResultado.Rows[linha].Cells["senhaExpirada"].Value;
                
                
                if (dgResultado.Rows[linha].Cells["Alterar Produto Bko"].Value.ToString().ToUpper() == "SIM")
                {
                    usuario.alterarProdutoBKO = true;
                }
                else
                {
                    usuario.alterarProdutoBKO = false;
                }

                usuario.Observacao = dgResultado.Rows[linha].Cells["observacao"].Value.ToString();

                UsuarioForm f = new UsuarioForm("DETALHES DO USUÁRIO", usuario, _campanhas, _perfis, _supervisores);

                ExibirForm(f);

                if (f.atualizar)
                {
                    CarregarSupervisor();
                    CarregarGrid(false);
                }
            }
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();

            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                {
                    mensagens.Add("[ID] deve ser informado!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        #endregion METODOS

        #region EVENTOS

        private void fListaUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();

                btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscaRapida_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a busca rápida!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarNovoRegistro();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar o novo cadastro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void dgResultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarEdicaoRegistro(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgResultado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            if ((this.dgResultado.RowCount > 0))
            {
                if ((string.Compare(dgResultado.CurrentCell.OwningColumn.Name, "dgvCbcEditar") == 0))
                {
                    this.dgResultado.EndEdit(); //Feche a edição do seu datagrid

                    bool checkBoxStatus = Convert.ToBoolean(dgResultado.CurrentCell.EditedFormattedValue);
                    if (checkBoxStatus == true)
                    {
                        _idsUsuarios.Add(Convert.ToInt32(dgResultado["ID", e.RowIndex].Value));
                    }
                    else if (checkBoxStatus == false)
                    {
                        int idUsuarioSelecionado = Convert.ToInt32(dgResultado["ID", e.RowIndex].Value);
                        if (_idsUsuarios.Contains(idUsuarioSelecionado))
                        {
                            _idsUsuarios.Remove(idUsuarioSelecionado);
                        }
                    }

                    if (_idsUsuarios.Count <= 0)
                    {
                        btnEditarUsuarios.Enabled = false;
                    }
                    else
                    {
                        btnEditarUsuarios.Enabled = true;
                    }
                }
            }
        }

        private void btnEditarUsuarios_Click(object sender, EventArgs e)
        {
            UsuarioFormEdit f = new UsuarioFormEdit("EDITAR USUÁRIOS", _idsUsuarios, _campanhas, _perfis, _supervisores);

            ExibirForm(f);
        }

        private void lnkMarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dtr in dgResultado.Rows)
            {
                ((DataGridViewCheckBoxCell)dtr.Cells[dgResultado.Columns["dgvCbcEditar"].Index]).Value = true;
                _idsUsuarios.Add(Convert.ToInt32(dgResultado["ID", dtr.Index].Value));
            }

            btnEditarUsuarios.Enabled = true;
        }

        private void lnkDesmarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dtr in dgResultado.Rows)
            {
                ((DataGridViewCheckBoxCell)dtr.Cells[dgResultado.Columns["dgvCbcEditar"].Index]).Value = false;
            }

            _idsUsuarios.Clear();
            btnEditarUsuarios.Enabled = false;
        }

        #endregion EVENTOS        
    }
}
