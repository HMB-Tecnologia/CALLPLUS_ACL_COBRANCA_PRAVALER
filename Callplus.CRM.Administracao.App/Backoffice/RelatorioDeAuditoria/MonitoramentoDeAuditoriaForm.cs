using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Backoffice.RelatorioDeAuditoria
{
    public partial class MonitoramentoDeAuditoriaForm : Form
    {
        public MonitoramentoDeAuditoriaForm()
        {
            _logger = LogManager.GetCurrentClassLogger();

            _usuarioService = new UsuarioService();
            _ofertaDoAtendimentoService = new AcordoDoAtendimentoService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly UsuarioService _usuarioService;
        private readonly AcordoDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private string _dataInicio;
        private string _dataFim;

        #endregion PROPRIEDADES

        #region METODOS

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {
            ConfigurarForm();
            CarregarAuditores();
        }

        private void CarregarAuditores()
        {
            var auditores = _usuarioService.ListarAuditoresAtivos(ativo: true);
            cmbAuditor.PreencherComTodos(auditores, x => x.Id, x => x.Nome);
        }

        private void ConfigurarForm()
        {
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private bool ParametrosPesquisaValidos()
        {
            var mensagens = new List<string>();

            //TODO: VALIDAÇÔES

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void CarregarResultado()
        {

            if (VerificarSePodePesquisar()) return;

            _dataInicio = dtpInicio.Value.ToString("yyyy-MM-dd");
            _dataFim = dtpFim.Value.ToString("yyyy-MM-dd");
            var idAuditor = Convert.ToInt32(cmbAuditor.SelectedValue.ToString());

            if (ParametrosPesquisaValidos())
            {
                dgRank.DataSource = _statusDeAuditoriaService.ExibirRanking(_dataInicio, _dataFim, idAuditor);

                dgRank.Columns[0].Visible = false;
                dgRank.Columns[2].Width = 70;
                dgRank.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                lblTotalRegistros.Text = dgRank.RowCount + " Registro(s)";
            }
        }

        private bool VerificarSePodePesquisar()
        {
            if (dtpFim.Value > dtpInicio.Value.AddDays(30))
            {
                MessageBox.Show("Não é possível pesquisar périodos maiores que 30 dias", "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CarregarResultado(int linha)
        {
            int idAuditor = Convert.ToInt32(dgRank.Rows[linha].Cells[0].Value);

            dgHistorico.DataSource = null;

            dgResultado.DataSource = _statusDeAuditoriaService.ListarRanking(idAuditor, _dataInicio, _dataFim);

            dgResultado.Columns[0].Width = 70;
        }

        private void CarregarHistorico(int linha)
        {
            long idOfertaBko = Convert.ToInt32(dgResultado.Rows[linha].Cells[0].Value);
            var hist = _ofertaDoAtendimentoService.ListarHistoricoDaOfertaDoAtendimentoBKO(idOfertaBko, 1);
            dgHistorico.DataSource = hist;
        }

        #endregion METODOS

        #region EVENTOS 

        private void ListaStatusDeAuditoriaForm_Load(object sender, System.EventArgs e)
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
                CarregarResultado();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void dgResultado_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarHistorico(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgResultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarResultado(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion EVENTOS 

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (PodeExportar())
                {
                    ExportarRelatorioCsv();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Não foi possível gerar o arquivo. Erro: {exception.Message}", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Fatal(exception);
            }
        }

        private bool PodeExportar()
        {
            var mensagens = new List<string>();

            DateTime dataInicial;
            DateTime dataFinal;

            if (DateTime.TryParse(dtpInicio.Text, out dataInicial) == false)
                mensagens.Add("[Data Inicial] inválida.");

            if (DateTime.TryParse(dtpFim.Text, out dataFinal) == false)
                mensagens.Add("[Data Final] inválida.");

            if (dataInicial > dataFinal)
                mensagens.Add("[Data Final] deve ser maior que [Data Inicial]");


            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void ExportarRelatorioCsv()
        {
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar CSV";

            saveFileDialog.FileName = "CALLPLUS_RELATORIO_AUDITORIA " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

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

                            if (dgResultado.Rows.Count >= 1)
                            {
                                for (int i = 0; i < dgResultado.Columns.Count; i++)
                                {
                                    sw.Write(dgResultado.Columns[i].Name.Trim() + ";");
                                }

                                for (int i = 0; i < dgResultado.Rows.Count; i++)
                                {
                                    sw.WriteLine();

                                    for (int j = 0; j < dgResultado.Columns.Count; j++)
                                    {
                                        sw.Write(dgResultado.Rows[i].Cells[j].Value.ToString().Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + ";");
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
