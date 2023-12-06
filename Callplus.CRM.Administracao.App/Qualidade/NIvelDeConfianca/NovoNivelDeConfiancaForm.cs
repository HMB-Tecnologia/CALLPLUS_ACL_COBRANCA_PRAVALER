using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento;
using System.IO;
using System.Text.RegularExpressions;

namespace Callplus.CRM.Administracao.App.Qualidade.NivelDeConfianca
{
    public partial class NovoNivelDeConfiancaForm : Form
    {
        public NovoNivelDeConfiancaForm()
        {
            _nivelDeConfiancaService = new NivelDeConfiancaServico();
            _campanhaService = new CampanhaService();

            InitializeComponent();
        }
        #region VARIAVEIS

        private readonly ILogger _logger;
        private DataTable dgAgentes;
        public bool atualizar { get; set; }

        private int qtdeRegistro = 0;
        private int _idArquivo;
        private string _idsAgentes;
        private decimal _notas;
        private decimal _numero;
        private string _arquivoOrigem = "";

        public static int idLayout;
        public static string tableLayout;

        #endregion VARIAVEIS

        #region PROPRIEDADES

        private readonly NivelDeConfiancaServico _nivelDeConfiancaService;
        private readonly CampanhaService _campanhaService;

        #endregion PROPRIEDADES

        #region METODOS

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }
        #endregion

        private void cmdCarregarArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                LocalizarArquivo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Ocorreu erro ao Carregar o arquivo!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LocalizarArquivo()
        {
            var FileDialog = new OpenFileDialog();

            FileDialog.Title = "Abrir Como";
            FileDialog.Filter = "Arquivos Textos (*.CSV)|*.CSV";
            if (DialogResult.Cancel == FileDialog.ShowDialog()) return;
            _arquivoOrigem = FileDialog.FileName;
            string nome = FileDialog.SafeFileName;

            if (string.IsNullOrEmpty(_arquivoOrigem))
            {
                btnSalvar.Enabled = false;
                MessageBox.Show("[Arquivo] inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                btnSalvar.Enabled = true;
                txtCaminhoDoArquivo.Text = _arquivoOrigem;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarNivelDeConfianca();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Ocorreu erro ao salvar o Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarCsv()
        {
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar CSV";

            saveFileDialog.FileName = "CALLPLUS_AGENTES_" + DateTime.Now.ToString("yyyy_MM_dd_HHmm") + ".csv";

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

                            if (dgAgentes.Rows.Count >= 1)
                            {

                                for (int i = 0; i < dgAgentes.Columns.Count; i++)
                                {
                                    sw.Write(dgAgentes.Columns[i].ColumnName.Trim() + ";");
                                }

                                sw.Write("Nota");

                                for (int i = 0; i < dgAgentes.Rows.Count; i++)
                                {
                                    sw.WriteLine();

                                    for (int j = 0; j < dgAgentes.Columns.Count; j++)
                                    {
                                        sw.Write(dgAgentes.Rows[i].ItemArray[j].ToString().Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + ";");
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
        private void SalvarNivelDeConfianca()
        {
            if (PodeSalvar())
            {
                StreamReader StreamReader;

                System.Globalization.CultureInfo pt = System.Globalization.CultureInfo.GetCultureInfo("pt-BR");
                StreamReader = new StreamReader(_arquivoOrigem, Encoding.GetEncoding(pt.TextInfo.ANSICodePage), true);

                _idArquivo = _nivelDeConfiancaService.ArquivoGravar(txtTitulo.Text.ToUpper(), AdministracaoMDI._usuario.Id);

                string sLinha = StreamReader.ReadLine();

                var nivelDeConfianca = new List<string>();
                while (sLinha != string.Empty)
                {
                    nivelDeConfianca.Add(sLinha);

                    qtdeRegistro++;

                    sLinha = StreamReader.ReadLine();

                    if (sLinha == null) break;
                }

                _idsAgentes = string.Empty;
                _numero = 0;
                foreach (var item in nivelDeConfianca)
                {
                    string[] itemSplit = item.ToString().Split(';');
                    if (itemSplit.Count() > 0)
                    {
                        bool conversao = decimal.TryParse(itemSplit[2].Trim(), out _numero);
                        if (conversao)
                        {
                            _idsAgentes = itemSplit[0].Trim();
                            _notas = Convert.ToDecimal(itemSplit[2].Replace(".", ",").Trim());
                            int idNota = _nivelDeConfiancaService.Gravar(_idArquivo, Convert.ToInt32(_idsAgentes), _notas, AdministracaoMDI._usuario.Id);
                        }
                    }
                }
            MessageBox.Show($"Lote de Nota Atualizada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            this.Close();
            }
            atualizar = true;
        }

        private bool PodeSalvar()
        {
            var mensagens = new List<string>();

            string nomeDoArquivo = "";

            if (string.IsNullOrEmpty(txtTitulo.Text.Trim()))
                mensagens.Add("[Titulo] deve ser informado.");

            var indiceBarra = txtCaminhoDoArquivo.Text.LastIndexOf(@"\") + 1;
            var indicePonto = txtCaminhoDoArquivo.Text.LastIndexOf(".");


            if (string.IsNullOrEmpty(_arquivoOrigem.Trim()))
                mensagens.Add("Você deve selecionar o arquivo a ser importado!");
            else
                nomeDoArquivo = (txtCaminhoDoArquivo.Text.Remove(indicePonto).Substring(indiceBarra));

            var regex = new Regex("^[a-zA-Z0-9_-]*$");
            var formatoCorreto = regex.IsMatch(nomeDoArquivo);

            if (formatoCorreto == false)
            {
                mensagens.Add("Nome do arquivo não pode conter espaços ou caracteres especiais!");
            }
            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private void cmdExportarArquivo_Click(object sender, EventArgs e)
        {
            int idCampanha = -1;

            idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());

            dgAgentes = _nivelDeConfiancaService.ListarAgente(idCampanha);
            ExportarCsv();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        private void Cancelar()
        {
            txtTitulo.Text = "";
            txtCaminhoDoArquivo.Text = "";
            _arquivoOrigem = string.Empty;

            CarregarCampanhas();

            btnSalvar.Enabled = false;

        }
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
        }

        private void NovoNivelDeConfiancaForm_Load(object sender, EventArgs e)
        {
            CarregarConfiguracaoInicial();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
