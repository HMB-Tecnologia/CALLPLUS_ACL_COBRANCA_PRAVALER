using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Backoffice.Gamificacao
{
    public partial class ListarGamificacaoForm : Form
    {
        public ListarGamificacaoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            //_usuarioService = new UsuarioService();
            _gamificacaoService = new GamificacaoService();


            InitializeComponent();
        }
        #region PROPRIEDADES

        private readonly Logger _logger;
        private readonly CampanhaService _campanhaService;
        private readonly GamificacaoService _gamificacaoService;


        #endregion
        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }
        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            CarregarCampanhas();
            //CarregarAgentes(-1, -1);
            //CarregarAvaliadores(-1, -1);
        }

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ListarGamificacaoForm_Load(object sender, EventArgs e)
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
        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int idCampanha = -1;
            string titulo = "";
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
                    titulo = txtTitulo.Text.Trim();
                }

                dgResultado.DataSource = _gamificacaoService.Listar(idRegistro, idCampanha, titulo, ativo);

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
        private void IniciarNovoRegistro()
        {
            GamificacaoForm f = new GamificacaoForm("NOVA GAMIFICAÇÃO", -1);

            f.Iniciar();

            if (f.atualizar)
            {
                CarregarGrid(false);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
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
        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                GamificacaoForm f = new GamificacaoForm("DETALHES DA GAMIFICAÇÂO", id);

                f.Iniciar();

                if (f.atualizar)
                {
                    CarregarGrid(false);
                }
            }
        }
    }
}
