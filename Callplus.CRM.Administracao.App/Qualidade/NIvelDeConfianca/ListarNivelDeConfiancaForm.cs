using Callplus.CRM.Tabulador.Servico.Servicos;
using Callplus.CRM.Tabulador.Dominio.Entidades;
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

namespace Callplus.CRM.Administracao.App.Qualidade.NivelDeConfianca
{
    public partial class ListarNivelDeConfiancaForm : Form
    {
        public ListarNivelDeConfiancaForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _nivelDeConfiancaService = new NivelDeConfiancaServico();
            _usuarioService = new UsuarioService();

            InitializeComponent();
        }
        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly CampanhaService _campanhaService;
        private readonly NivelDeConfiancaServico _nivelDeConfiancaService;
        private readonly UsuarioService _usuarioService;
        public bool atualizar { get; set; }

        #endregion PROPRIEDADES
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
        #region METODOS
        private void IniciarNovoRegistro()
        {
            NovoNivelDeConfiancaForm f = new NovoNivelDeConfiancaForm();

            f.Iniciar();

            if (f.atualizar)
            {
                CarregarGrid(false);
            }
        }
        #endregion

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

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;

            CarregarCampanhas();
            CarregarAgentes(-1, -1);
            CarregarAvaliadores(-1, -1);
        }
        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }
        private void CarregarAvaliadores(int idCampanha, int idPerfil)
        {
            IEnumerable<Usuario> _avaliadores = _usuarioService.ListarAvaliadores(true, idPerfil, idCampanha);
            cmbAvaliador.PreencherComTodos(_avaliadores, avaliador => avaliador.Id, avaliador => avaliador.Nome);
        }
        private void CarregarAgentes(int idCampanha, int idPerfil)
        {
            IEnumerable<Usuario> _agente = _usuarioService.ListarAgentes(true, idPerfil, idCampanha);
            cmbAgente.PreencherComTodos(_agente, agente => agente.Id, agente => agente.Nome);
        }
        private void CarregarGrid(bool buscaRapida)
        {
            long idRegistro = -1;
            int idCampanha = -1;
            int idAgente = -1;
            int idAvaliador = -1;

            decimal notaMinima = 0;
            decimal notaMaxima = 0;

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
                    idAgente = int.Parse(cmbAgente.SelectedValue.ToString());
                    idAvaliador = int.Parse(cmbAvaliador.SelectedValue.ToString());

                    notaMinima = numNotaMinima.Value;
                    notaMaxima = numNotaMaxima.Value;
                }

                dgNotas.DataSource = _nivelDeConfiancaService.Listar(idRegistro, idCampanha, dataInicial, dataFinal, idAgente, idAvaliador, notaMinima, notaMaxima);

                lblTotalRegistros.Text = dgNotas.RowCount.ToString() + " Registro(s)";

                RealizarAjustesGrid();
            }
        }
        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();

            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                    mensagens.Add("[ID] deve ser informado!");
            }
            else
            {
                if (txtDataFinal.Value.Date < txtDataInicial.Value.Date)
                    mensagens.Add("[Data Final] não pode ser menor que a Data Inicial!");
            }
            if (buscaRapida)
            {
                if (numNotaMaxima.Value < numNotaMinima.Value)
                    mensagens.Add("[Nota Maxima] não pode ser menor que a Nota Minima!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        private void RealizarAjustesGrid()
        {
            dgNotas.Columns["Id"].DisplayIndex = 0;
            dgNotas.Columns["Agente"].DisplayIndex = 1;
            dgNotas.Columns["Nota Inicial"].DisplayIndex = 2;
            dgNotas.Columns["Nota Produção"].DisplayIndex = 3;
            dgNotas.Columns["Nota Total"].DisplayIndex = 4;
            dgNotas.Columns["Responsavel"].DisplayIndex = 5;
            dgNotas.Columns["Data de Atualização"].DisplayIndex = 6;


            dgNotas.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgNotas.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgNotas.Columns["Nota Inicial"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgNotas.Columns["Nota Inicial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgNotas.Columns["Nota Produção"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgNotas.Columns["Nota Produção"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgNotas.Columns["Nota Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgNotas.Columns["Nota Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgNotas.Columns["Data de Atualização"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgNotas.Columns["Data de Atualização"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            for (int i = dgNotas.Columns["Data de Atualização"].Index + 1; i < dgNotas.Columns.Count; i++)
            {
                dgNotas.Columns[i].Visible = false;
            }
        }

        private void ListarNivelDeConfiancaForm_Load(object sender, EventArgs e)
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

        private void dgNotas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                int id = Convert.ToInt32(dgNotas.Rows[linha].Cells["Id"].Value);
                string nota = dgNotas.Rows[linha].Cells["Nota Inicial"].Value.ToString();
                string agente = dgNotas.Rows[linha].Cells["Agente"].Value.ToString();
                int idAgente = Convert.ToInt32(dgNotas.Rows[linha].Cells["Id Agente"].Value);

                EditarNivelDeConfiancaForm f = new EditarNivelDeConfiancaForm(id, nota, agente, idAgente, AdministracaoMDI._usuario.Id);

                f.Iniciar();

                if (f.atualizar)
                {
                    CarregarGrid(false);
                }
            }
        }
        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarNotaCsv();
        }
        private void ExportarNotaCsv()
        {
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar CSV";

            saveFileDialog.FileName = "CALLPLUS_NOTAS_AGENTES " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream myStream = null;

                try
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                        while (retry == DialogResult.Retry)
                        {
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(myStream, UTF8Encoding.UTF8);

                            if (dgNotas.Rows.Count >= 1)
                            {
                                for (int i = 0; i < dgNotas.Columns.Count; i++)
                                {
                                    sw.Write(dgNotas.Columns[i].Name.Trim() + ";");
                                }

                                for (int i = 0; i < dgNotas.Rows.Count; i++)
                                {
                                    sw.WriteLine();

                                    for (int j = 0; j < dgNotas.Columns.Count; j++)
                                    {
                                        sw.Write(dgNotas.Rows[i].Cells[j].Value.ToString().Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + ";");
                                    }
                                }

                                sw.Close();
                                retry = DialogResult.Cancel;
                            }
                        }

                        MessageBox.Show("Arquivo gerado com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                finally
                {
                    myStream.Close();
                }
            }
        }
    }
}
