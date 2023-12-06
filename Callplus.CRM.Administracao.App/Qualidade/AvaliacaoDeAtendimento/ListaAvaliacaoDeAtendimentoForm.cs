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

namespace Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento
{
    public partial class ListaAvaliacaoDeAtendimentoForm : Form
    {
        public ListaAvaliacaoDeAtendimentoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _perfilService = new PerfilService();
            _usuarioService = new UsuarioService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
        private readonly PerfilService _perfilService;
        private readonly UsuarioService _usuarioService;
        
        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            CarregarCampanhas();
            CarregarPerfisAvaliadores();
            CarregarAvaliadores(-1, -1);
        }

        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void CarregarPerfisAvaliadores()
        {
            IEnumerable<Perfil> _perfis = _perfilService.Listar(true).Where(x => (x.id != 2 && x.id != 4)).Distinct();
            cmbPerfilAvaliador.PreencherComTodos(_perfis, perfil => perfil.id, perfil => perfil.nome);
        }

        private void CarregarAvaliadores(int idCampanha, int idPerfil)
        {
            IEnumerable<Usuario> _avaliadores = _usuarioService.ListarAvaliadores(true, idPerfil, idCampanha);
            cmbAvaliador.PreencherComTodos(_avaliadores, avaliador => avaliador.Id, avaliador => avaliador.Nome);
        }

        private void CarregarGrid(bool buscaRapida)
        {
            long idRegistro = -1;
            int idCampanha = -1;
            int idPerfil = -1;
            int idAvaliador = -1;

            DateTime dataInicial = txtDataInicial.Value.Date;
            DateTime dataFinal = txtDataFinal.Value.Date.AddDays(1);

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
                    idPerfil = int.Parse(cmbPerfilAvaliador.SelectedValue.ToString());
                    idAvaliador = int.Parse(cmbAvaliador.SelectedValue.ToString());
                }

                dgResultado.DataSource = _avaliacaoDeAtendimentoService.Listar(idRegistro, idCampanha, dataInicial, dataFinal, idPerfil, idAvaliador);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns["Data"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            for (int i = dgResultado.Columns["Data"].Index + 1; i < dgResultado.Columns.Count; i++)
            {
                dgResultado.Columns[i].Visible = false;
            }
        }

        private void IniciarNovoRegistro()
        {
            AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("NOVA AVALIAÇÃO", 0, 0, "", false);

            f.Iniciar();

            if (f.atualizar)
            {
                CarregarGrid(false);
            }
        }

        private void IniciarEdicaoRegistro(int linha)
        {
            bool feedbackRealizado = false;

            if (linha >= 0)
            {
                int id = Convert.ToInt32(dgResultado.Rows[linha].Cells["Id"].Value);
                int idFormulario = Convert.ToInt32(dgResultado.Rows[linha].Cells["Id Formulário"].Value);
                string avaliador = dgResultado.Rows[linha].Cells["Avaliador"].Value.ToString();

                if (dgResultado.Rows[linha].Cells["idFeedback"].Value.ToString() != "")
                    feedbackRealizado = true;
                
                AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("DETALHES DA AVALIAÇÃO", id, idFormulario, avaliador, feedbackRealizado);

                f.Iniciar();

                if (f.atualizar)
                {
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
            else
            {
                if (txtDataFinal.Value.Date < txtDataInicial.Value.Date)
                {
                    mensagens.Add("[Data Final] não pode ser menor que a Data Inicial!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        #endregion METODOS

        #region EVENTOS        

        private void ListaAvaliacaoDeAtendimentoForm_Load(object sender, System.EventArgs e)
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

        private void btnBuscaRapida_Click(object sender, System.EventArgs e)
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

        private void btnPesquisar_Click(object sender, System.EventArgs e)
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

        private void btnNovo_Click(object sender, System.EventArgs e)
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

        private void btnFechar_Click(object sender, System.EventArgs e)
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

        private void cmbPerfilAvaliador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCampanha = -1;
                int idPerfil = -1;

                if (int.TryParse(cmbCampanha.SelectedValue.ToString(), out idCampanha) && int.TryParse(cmbPerfilAvaliador.SelectedValue.ToString(), out idPerfil))
                {
                    CarregarAvaliadores(idCampanha, idPerfil);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os avaliadores!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS        
    }
}
