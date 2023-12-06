using Callplus.CRM.Tabulador.Servico.Servicos;
using Callplus.CRM.Tabulador.Dominio.Entidades;
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
using CallplusUtil.Extensions;

namespace Callplus.CRM.Administracao.App.Qualidade.NivelDeConfianca
{
    public partial class EditarNivelDeConfiancaForm : Form
    {
        public EditarNivelDeConfiancaForm(int id, string nota, string agente, int idAgente, int idAtualizador)
        {
            this.id = id;
            this.nota = decimal.Parse(nota);
            this.agente = agente;
            this.idAgente = idAgente;
            this.idAtualizador = idAtualizador;

            _nivelDeConfiancaService = new NivelDeConfiancaServico();


            InitializeComponent();
        }
        public bool atualizar { get; set; }
        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly NivelDeConfiancaServico _nivelDeConfiancaService;

        int id = 0;
        //string titulo = "";
        string agente = "";
        decimal nota;
        int idAgente;
        int idAtualizador;

        #endregion
        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }
        private void CarregarConfiguracaoInicial()
        {
            txtAgente.Text = agente;
            numNota.Value = nota;

            CarregarGrid(false);
        }
        private void CarregarGrid(bool buscaRapida)
        {
            dgHistoricoNotaAgente.DataSource = _nivelDeConfiancaService.ListarHistorico(idAgente);
            dgHistoricoNotaProducaoAgente.DataSource = _nivelDeConfiancaService.ListarHistoricoProducao(idAgente);

            RealizarAjustesGrid();
        }
        private void RealizarAjustesGrid()
        {
            dgHistoricoNotaAgente.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgHistoricoNotaAgente.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgHistoricoNotaProducaoAgente.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgHistoricoNotaProducaoAgente.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgHistoricoNotaAgente.Columns["Nota Inicial"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgHistoricoNotaAgente.Columns["Nota Inicial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgHistoricoNotaProducaoAgente.Columns["Nota Produção"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgHistoricoNotaProducaoAgente.Columns["Nota Produção"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgHistoricoNotaAgente.Columns["Data de Atualização"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            dgHistoricoNotaProducaoAgente.Columns["Data de Atualização"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";


            for (int i = dgHistoricoNotaAgente.Columns["Data de Atualização"].Index + 1; i < dgHistoricoNotaAgente.Columns.Count; i++)
            {
                dgHistoricoNotaAgente.Columns[i].Visible = false;
            }
            for (int i = dgHistoricoNotaProducaoAgente.Columns["Data de Atualização"].Index + 1; i < dgHistoricoNotaProducaoAgente.Columns.Count; i++)
            {
                dgHistoricoNotaProducaoAgente.Columns[i].Visible = false;
            }
        }

        private void EditarNivelDeConfiancaForm_Load(object sender, EventArgs e)
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
        private void btnFechar_Click(object sender, EventArgs e)
        {
            FecharTela();
        }
        private void FecharTela()
        {
            this.Hide();
            this.Close();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            nota = numNota.Value;
            try
            {
                int idNota = _nivelDeConfiancaService.Editar(id, nota, idAgente, AdministracaoMDI._usuario.Id);
                MessageBox.Show($"Nota Atualizada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                atualizar = true;
                FecharTela();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível editar esta Nota!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numNota_ValueChanged(object sender, EventArgs e)
        {
            if (numNota.Value != nota)
                btnSalvar.Enabled = true;
            else
                btnSalvar.Enabled = false;
        }

        private void btnExportarHistoricoInicial_Click(object sender, EventArgs e)
        {
            ExportarHistoricoInicialCsv();
        }
        private void ExportarHistoricoInicialCsv()
        {
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar CSV";

            saveFileDialog.FileName = "CALLPLUS_HISTORICO_NOTAS_INCIAIS " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

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

                            if (dgHistoricoNotaAgente.Rows.Count >= 1)
                            {
                                for (int i = 0; i < dgHistoricoNotaAgente.Columns.Count; i++)
                                {
                                    sw.Write(dgHistoricoNotaAgente.Columns[i].Name.Trim() + ";");
                                }

                                for (int i = 0; i < dgHistoricoNotaAgente.Rows.Count; i++)
                                {
                                    sw.WriteLine();

                                    for (int j = 0; j < dgHistoricoNotaAgente.Columns.Count; j++)
                                    {
                                        sw.Write(dgHistoricoNotaAgente.Rows[i].Cells[j].Value.ToString().Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + ";");
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

        private void btnExportarHistoricoProducao_Click(object sender, EventArgs e)
        {

            ExportarHistoricoProducaoCsv();
        }
        private void ExportarHistoricoProducaoCsv()
        {
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar CSV";

            saveFileDialog.FileName = "CALLPLUS_HISTORICO_NOTAS_PRODUCAO " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

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

                            if (dgHistoricoNotaProducaoAgente.Rows.Count >= 1)
                            {
                                for (int i = 0; i < dgHistoricoNotaProducaoAgente.Columns.Count; i++)
                                {
                                    sw.Write(dgHistoricoNotaProducaoAgente.Columns[i].Name.Trim() + ";");
                                }

                                for (int i = 0; i < dgHistoricoNotaProducaoAgente.Rows.Count; i++)
                                {
                                    sw.WriteLine();

                                    for (int j = 0; j < dgHistoricoNotaProducaoAgente.Columns.Count; j++)
                                    {
                                        sw.Write(dgHistoricoNotaProducaoAgente.Rows[i].Cells[j].Value.ToString().Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + ";");
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
