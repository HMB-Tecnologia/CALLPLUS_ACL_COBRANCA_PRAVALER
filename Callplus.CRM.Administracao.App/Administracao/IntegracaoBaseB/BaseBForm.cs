using Callplus.CRM.Administracao.App.Planejamento.Mailing;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
//using controller;
//using model.objetos;

namespace Callplus.CRM.Administracao.App.IntegracaoBaseB
{
    public partial class fBaseB : Form
    {
        public fBaseB()
        {
            InitializeComponent();

            CarregarCampanhas();

            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _mailingService = new MailingService();
        }

        #region PROPRIEDADES

        private readonly CampanhaService _campanhaService;
        private readonly MailingService _mailingService;
        private readonly ILogger _logger;
        private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;

        #endregion PROPRIEDADES


        #region METODOS

        private void CarregarCampanhas()
        {
            _campanhas = _campanhaService.Listar(true);
            cmbCampanha.PreencherComTodos(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);

            cmbCampanha.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        //private void ExportarRelatorioCSV()
        //{
        //    try
        //    {
        //        var caminho = string.Empty;
        //        var CMailing = new MailingService();
        //        var dataInicio = string.IsNullOrEmpty(datDataInicial.Text) ? System.DateTime.Now : Convert.ToDateTime(datDataInicial.Text);
        //        var dataTermino = string.IsNullOrEmpty(datDataFinal.Text) ? DateTime.MaxValue : Convert.ToDateTime(datDataFinal.Text);
        //        var codMailing = txtcodMailing.Text;
        //        var tipoExportacaoContatosNaoTrabalhados = chkRetornoClaro.Checked ? 2 : 1;
        //        var idCampanha = 0;
        //        idCampanha = 0;
        //        int.TryParse(cmbCampanha.SelectedValue.ToString(), out idCampanha);

        //        var mailings = _mailingService.RetornoMailingsTrabalhadosDia(dataInicio, dataTermino, codMailing, idCampanha);

        //        var folder = new FolderBrowserDialog();
        //        folder.Description = "Selecione o caminho para exportar os arquivos de retorno.";

        //        if (folder.ShowDialog() == DialogResult.OK)
        //            caminho = folder.SelectedPath;
        //        else
        //            return;

        //        foreach (Tabulador.Dominio.Entidades.Mailing mail in mailings)
        //        {
        //            arquivo de telefonia
        //            var nomeArquivo = "RTEL_" + mail.nomeArquivo;
        //            caminho = folder.SelectedPath;
        //            caminho += "\\" + nomeArquivo + ".txt";

        //            var listaTelefonia = _mailingService.RetornoTelefoniaPorDiaIdMailing(dataInicio, dataTermino, mail.nomeArquivo, mail.idCampanha, codMailing, tipoExportacaoContatosNaoTrabalhados);
        //            var writerTelefonia = new System.IO.StreamWriter(caminho);

        //            foreach (string linha in listaTelefonia)
        //                writerTelefonia.WriteLine(linha);

        //            writerTelefonia.Close();

        //            nomeArquivo = "ROCR_" + mail.nomeArquivo;
        //            caminho = folder.SelectedPath;
        //            caminho += "\\" + nomeArquivo + ".txt";

        //            var listaOcorrencias = _mailingService.RetornoOcorrenciasPorDiaIdMailing(dataInicio, dataTermino, mail.nomeArquivo, mail.idCampanha, codMailing);
        //            var writerOcorrencia = new System.IO.StreamWriter(caminho);

        //            foreach (string linha in listaOcorrencias)
        //                writerOcorrencia.WriteLine(linha);

        //            writerOcorrencia.Close();
        //        }
        //        MessageBox.Show("Processo finalizado!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro:" + ex.Message);
        //    }
        //}

        #endregion METODOS

        #region EVENTOS

        private void rbData_CheckedChanged(object sender, EventArgs e)
        {
            dgResultado.DataSource = null;
            if (rbData.Checked)
            {
                grbContatosNaoTrabalhados.Enabled = false;
                datDataInicial.Enabled = true;
                datDataFinal.Enabled = true;
            }
            else
            {
                grbContatosNaoTrabalhados.Enabled = true;
                datDataInicial.Enabled = false;
                datDataFinal.Enabled = false;

                txtcodMailing.Text = string.Empty;
            }
        }

        private void rbCodMailing_CheckedChanged(object sender, EventArgs e)
        {
            dgResultado.DataSource = null;
            if (rbCodMailing.Checked)
            {
                datDataInicial.Enabled = false;
                datDataFinal.Enabled = false;
                grbContatosNaoTrabalhados.Enabled = true;

            }
            else
            {
                grbContatosNaoTrabalhados.Enabled = false;
                datDataInicial.Enabled = true;
                datDataFinal.Enabled = true;
            }
        }

        private void cmdPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                string dataInicio = "", dataTermino = "";

                string codMailing = txtcodMailing.Text;

                lblTotalTrabalhado.Text = "Total de Registros: 0";
                lblTotalEnvio.Text = "Total de Registros: 0";

                string nomeCampanha = cmbCampanha.Text;

                dgResultado.DataSource = null;

                if (rbData.Checked)
                {
                    dataInicio = datDataInicial.Value.ToString("dd/MM/yyyy 00:00:00");
                    dataTermino = datDataFinal.Value.ToString("dd/MM/yyyy 23:59:59");

                    if (Convert.ToDateTime(dataInicio) > Convert.ToDateTime(dataTermino))
                    {
                        MessageBox.Show("A data final deve ser maior ou igual a data inicial", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (rbCodMailing.Checked)
                {
                    if (!string.IsNullOrEmpty(txtcodMailing.Text))
                    {
                        codMailing = txtcodMailing.Text;
                    }
                    else
                    {
                        MessageBox.Show("Informe o código Mailing", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dataInicio = string.Empty;
                    dataTermino = string.Empty;
                }
                else
                {
                    MessageBox.Show("Informe o filtro da Pesquisa", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MailingService _mailingService = new MailingService();

                if (string.IsNullOrEmpty(dataInicio)) dataInicio = DateTime.Now.ToString("yyyy-MM-dd");
                if (string.IsNullOrEmpty(dataTermino)) dataTermino = DateTime.Now.ToString("yyyy-MM-dd");

                bool todos = true;
                //DataTable _dt = _mailingService.RetornarBaseB(codMailing, dataInicio, dataTermino, todos, nomeCampanha);

                int totalRegistros = 0;
                int totalValido = 0;

                //for (int i = 0; i < _dt.Rows.Count; i++)
                //{
                //    totalRegistros += (int)_dt.Rows[i][5];

                //    if (_dt.Rows[i][4].ToString().Trim() != "0" && _dt.Rows[i][5].ToString().Trim() != "")
                //    {
                //        totalValido += (int)_dt.Rows[i][5];
                //    }
                //}

                //dgResultado.DataSource = _dt;

                lblTotalTrabalhado.Text = "Total de contatos trabalhados na operação: " + totalRegistros;
                lblTotalEnvio.Text = "Total de contatos válidos: " + totalValido;
                //txtcodMailing.Text = string.Empty;
            }
            catch (Exception myException)
            {
                MessageBox.Show("Erro ao gerar base B. " + myException.Message, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            if (dgResultado.RowCount > 1)
            {
                if (rbCodMailing.Checked && (!chkVirgens.Checked && !chkRetornoClaro.Checked))
                {
                    MessageBox.Show("Informe uma opção para exportação dos contatos não trabalhados", "Aviso");
                }
                else
                {
                    //ExportarRelatorioCSV();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        #endregion EVENTOS

        private void fBaseB_Load(object sender, EventArgs e)
        {

        }
    }
}
