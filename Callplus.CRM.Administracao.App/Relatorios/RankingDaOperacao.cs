using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Relatorios
{
    public partial class RankingDaOperacao : Form
    {
        public RankingDaOperacao()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _usuarioService = new UsuarioService();
            _campanhaService = new CampanhaService();
            _relatorioService = new RelatorioService();

            InitializeComponent();
        }

        #region VARIAVEIS

        private readonly UsuarioService _usuarioService;
        private readonly CampanhaService _campanhaService;
        private readonly RelatorioService _relatorioService;
        private readonly ILogger _logger;
        private IEnumerable<Campanha> _campanhas;        
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _supervisores;
        private IEnumerable<Tabulador.Dominio.Entidades.Usuario> _operadores;

        #endregion VARIAVEIS

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            
            CarregarCampanhas();
            CarregarSupervisor();
            CarregarOperador();
        }

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
        }

        private void CarregarSupervisor()
        {
            int idCampanha = -1;

            if (!cmbCampanha.TextoEhTodos())
                idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);

            _supervisores = _usuarioService.ListarSupervisores(ativo: false, idCampanha: idCampanha);
            //_supervisores = _supervisores.Where(x => x.IdPerfil == 3);
            cmbSupervisor.PreencherComTodos(_supervisores, supervisor => supervisor.Id, supervisor => supervisor.Nome);
        }

        private void CarregarOperador()
        {
            int idCampanha = -1;
            int idSupervisor = -1;

            if (!cmbCampanha.TextoEhTodos())
                idCampanha = Convert.ToInt32(cmbCampanha.SelectedValue);

            if (!cmbSupervisor.TextoEhTodos())
                idSupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);

            _operadores = _usuarioService.ListarOperadores(ativo: false, idCampanha: idCampanha, idSupervisor: idSupervisor);
            cmbOperador.PreencherComTodos(_operadores, operador => operador.Id, operador => operador.Nome);
        }

        private void CarregarGrid()
        {
            int idCampanha = -1;            
            int idSupervisor = -1;
            int idOperador = -1;
            DateTime data = DateTime.MinValue;

            if (ParametrosPesquisaValidos())
            {
                idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());                
                idSupervisor = int.Parse(cmbSupervisor.SelectedValue.ToString());
                idOperador = int.Parse(cmbOperador.SelectedValue.ToString());
                data = dtpData.Value;

                dgResultado.DataSource = _relatorioService.RetornarRankingDaOperacao(idCampanha, idSupervisor, idOperador, data);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";

                btnExportar.Enabled = dgResultado.RowCount > 0 ? true : false;

            }
        }

        private bool ParametrosPesquisaValidos()
        {
            var mensagens = new List<string>();

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

            saveFileDialog.FileName = "CALLPLUS_RANKING_DA_OPERACAO " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

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


        #endregion METODOS

        #region EVENTOS

        private void RankingDaOperacao_Load(object sender, EventArgs e)
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
                CarregarGrid();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }       

        private void cmbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarOperador();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível atualizar o supervisor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCampanha_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarSupervisor();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível atualizar o supervisor!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ParametrosPesquisaValidos())
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
        #endregion EVENTOS 




    }
}
