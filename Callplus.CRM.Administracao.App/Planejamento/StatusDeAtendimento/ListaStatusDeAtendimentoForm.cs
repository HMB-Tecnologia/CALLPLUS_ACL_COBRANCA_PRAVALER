using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;

namespace Callplus.CRM.Administracao.App.Planejamento.StatusDeAtendimento
{
    public partial class ListaStatusDeAtendimentoForm : Form
    {
        public ListaStatusDeAtendimentoForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _campanhaService = new CampanhaService();

            InitializeComponent();
        }

        #region PROPRIEDADE

        private readonly ILogger _logger;
        private readonly CampanhaService _campanhaService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private Tabulador.Dominio.Entidades.StatusDeAtendimento _statusDeAtendimento;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;

        #endregion PROPRIEDADE

        #region METODOS

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void RealizarAjusteGrid()
        {
            dgDados.Columns["Id"].Width = 35;

            dgDados.Columns["Data Criacao"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgDados.Columns["Data Modificacao"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgDados.Columns["Nome Status de Atendimento"].Width = 250;

            dgDados.Columns["Ativo"].Width = 50;
            dgDados.Columns["Ativo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void IniciarNovoRegistro()
        {
            StatusDeAtendimentoForm f = new StatusDeAtendimentoForm( 0, "NOVO STATUS DE ATENDIMENTO");

            f.Iniciar();

            if (f.Atualizar)
            {
                CarregarGrid(false);
            }
        }

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

            cmbCampanha.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            CarregarCampanhas();
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idStatus = -1;
            string nome = txtNome.Text;
            int idTipoTipoStatus = -1;
            bool ativo = chkAtivos.Checked;
            int idcampanha = int.Parse(cmbCampanha.SelectedValue.ToString());

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                    {
                        idStatus = int.Parse(txtBuscaRapida.Text);
                    }
                }
                else
                {
                    idStatus = int.Parse(cmbCampanha.SelectedValue.ToString());
                    nome = txtNome.Text.Trim();
                }

                dgDados.DataSource = _statusDeAtendimentoService.ListaStatusDeAtendimento(idcampanha, ativo, nome);

                lblTotalRegistros.Text = dgDados.RowCount.ToString() + " Registro(s)";

                RealizarAjusteGrid();
            }
        }

        private void InicarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgDados.Rows[linha].Cells["Id"].Value;

                StatusDeAtendimentoForm f = new StatusDeAtendimentoForm(id, "EDITAR STATUS DE ATENDIMENTO");

                f.Iniciar();

                if (f.Atualizar)
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
            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        #endregion METODOS

        #region EVENTOS

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
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
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

        private void ListaStatusDeAtendimentoForm_Load(object sender, EventArgs e)
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

        private void dgListaStatusDeAtendimento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                InicarEdicaoRegistro(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                     $"Não foi possível iniciar a edição do registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS
    }
}
