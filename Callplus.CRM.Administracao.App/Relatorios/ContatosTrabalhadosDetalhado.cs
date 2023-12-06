using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;

namespace Callplus.CRM.Administracao.App.Relatorios
{
    public partial class ContatosTrabalhadosDetalhado : Form
    {
        public ContatosTrabalhadosDetalhado()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _usuarioService = new UsuarioService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _mailingService = new MailingService();
            _statusDeAtendimentoService = new StatusDeAtendimentoService();
            _statusDeOfertaService = new StatusDeOfertaService();
            _relatorioService = new RelatorioService();
            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly CampanhaService _campanhaService;
        private readonly UsuarioService _usuarioService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly StatusDeAtendimentoService _statusDeAtendimentoService;
        private readonly StatusDeOfertaService _statusDeOfertaService;
        private readonly MailingService _mailingService;
        private readonly RelatorioService _relatorioService;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            ShowIcon = false;
            MaximizeBox = true;
            MinimizeBox = false;
            bool listarCampanhasInativas = false;

            cmbOperador.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbSupervisor.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbCampanhas.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbMailing.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;


            PreencherCamposIniciais();
            CarregarCampanhas(listarCampanhasInativas);

            CarregarSupervisores();
            CarregarRegrasDoPerfil();
            CarregarStatusDeAtendimento();

            CarregarOperadores(idSupervisor: -1);
            cmbSupervisor.ResetarComSelecione(habilitar: true);
            cmbMailing.ResetarComSelecione(habilitar: true);
            cmbCampanhas.ResetarComSelecione(habilitar: true);
        }

        private void PreencherCamposIniciais()
        {
            IEnumerable<KeyValuePair<int, string>> listaVazia = new List<KeyValuePair<int, string>>();

            cmbOperador.PreencherComSelecione(listaVazia);
            cmbMailing.PreencherComSelecione(listaVazia);
        }

        private void CarregarCampanhas(bool listarInativos)
        {
            bool? ativo = true;
            if (listarInativos)
                ativo = null;

            IEnumerable<Campanha> campanhas = _campanhaService.Listar(ativo: ativo);
            cmbCampanhas.PreencherComSelecione(campanhas, x => x.Id, x => x.Nome);

        }

        private void CarregarMailings(int idCampanha, bool listarInativos)
        {
            bool? ativo = true;
            if (listarInativos)
                ativo = null;

            IEnumerable<Mailing> mailings = _mailingService.Listar(id: null, idCampanha: idCampanha, ativo: ativo);
            cmbMailing.PreencherComTodosESelecione(mailings, x => x.id, x => x.nome);
            cmbMailing.ResetarComSelecione(habilitar: cmbMailing.Enabled);
        }

        private void CarregarStatusDeOferta(int idCampanha)
        {
            IEnumerable<StatusDeOferta> statusDeOferta = _statusDeOfertaService.ListarStatusDeOferta(idCampanha: idCampanha, idTipoStatus: null, ativo: null);
            chkStatusOferta.Preencher(statusDeOferta, x => x.Id, x => x.Nome);
        }

        private void CarregarStatusDeAtendimento()
        {
            IEnumerable<StatusDeAtendimento> statusDeAtendiemento =
                _statusDeAtendimentoService.Listar(id: null, ativo: null, idCampanha: null, idTipoStatus: null);
            chkStatusDeAtendimento.Preencher(statusDeAtendiemento, x => x.Id, x => x.Nome);
        }

        private void CarregarOperadores(int idCampanha = -1, int idSupervisor = -1)
        {
            IEnumerable<Usuario> operadores = _usuarioService.ListarOperadores(ativo: true, idCampanha: idCampanha, idSupervisor: idSupervisor);
            cmbOperador.PreencherComTodosESelecione(operadores, x => x.Id, x => x.Nome);
            cmbOperador.ResetarComSelecione(habilitar: cmbOperador.Enabled);
        }

        private void CarregarSupervisores()
        {
            IEnumerable<Usuario> supervisores = _usuarioService.ListarSupervisores(ativo: true);
            cmbSupervisor.PreencherComTodosESelecione(supervisores, x => x.Id, x => x.Nome);
        }

        private void CarregarRegrasDoPerfil()
        {
            btnExportar.Enabled = AdministracaoMDI._usuario.PermiteExportacao;

            if (AdministracaoMDI._usuario.perfil.ToString() == "SUPERVISOR")
            {
                cmbSupervisor.SelectedValue = AdministracaoMDI._usuario.Id.ToString();
                cmbSupervisor.Enabled = false;

                CarregarOperadores(-1, AdministracaoMDI._usuario.Id);

                DateTime data = DateTime.Today;
                DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);
                dtpDataInicial.MinDate = primeiroDiaDoMes;

                DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

                dtpDataFinal.MaxDate = ultimoDiaDoMes;
            }
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            int? idSupervisor = 0;
            int? idOperador = 0;
            int idCampanha = 0;
            int idMailing = 0;
            bool filtrarStatusDeAtendimentoNulo = false;
            bool filtrarStatusDeOfertaNulo = false;
            string idsStatusOferta = "";
            string idsStatusAtendimento = "";
            DateTime dataInicio = DateTime.Now;
            DateTime dataTermino = DateTime.Now;


            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        idRegistro = int.Parse(txtBuscaRapida.Text);
                }
                else
                {

                    if (cmbCampanhas.TextoEhSelecione() || cmbCampanhas.TextoEhTodos())
                    {
                        idCampanha = -1;
                    }
                    else
                    {
                        idCampanha = int.Parse(cmbCampanhas.SelectedValue?.ToString());
                    }
                    idsStatusOferta = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkStatusOferta, separador: ",");
                    idsStatusAtendimento = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkStatusDeAtendimento, separador: ",");

                    int idParser = 0;
                    if (int.TryParse(cmbSupervisor.SelectedValue?.ToString(), out idParser))
                        idSupervisor = idParser;

                    if (int.TryParse(cmbOperador.SelectedValue?.ToString(), out idParser))
                        idOperador = idParser;

                    if (cmbSupervisor.TextoEhSelecione() || cmbOperador.TextoEhTodos())
                        idSupervisor = -1;

                    if (cmbOperador.TextoEhSelecione() || cmbOperador.TextoEhTodos())
                        idOperador = -1;


                    if (int.TryParse(cmbMailing.SelectedValue?.ToString(), out idParser))
                        idMailing = idParser;

                    if (cmbMailing.TextoEhSelecione())
                        idMailing = -1;


                    filtrarStatusDeAtendimentoNulo = chkListarStatusAtendimentoNaoInformado.Checked;
                    filtrarStatusDeOfertaNulo = chkListarStatusOfertaNaoInformado.Checked;

                    DateTime data = DateTime.MaxValue;
                    if (DateTime.TryParse($"{dtpDataInicial.Text} {cmbHoraInicial.Text}", out data))
                    {
                        dataInicio = data;
                    }
                    if (DateTime.TryParse($"{dtpDataFinal.Text} {cmbHoraFinal.Text}", out data))
                    {
                        dataTermino = data;
                    }
                }

                dgResultado.DataSource = _relatorioService.RetornarContatosTrabalhados(dataInicio, dataTermino, idCampanha, idOperador, idSupervisor, idMailing, idsStatusAtendimento, idsStatusOferta, filtrarStatusDeAtendimentoNulo, filtrarStatusDeOfertaNulo);
                lblTotalRegistros.Text = dgResultado.RowCount + " Registro(s)";

                RealizarAjustesGrid();
            }
        }

        private void RealizarAjustesGrid()
        {
            dgResultado.Columns[2].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
            dgResultado.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();
            DateTime dataInicial = new DateTime();
            DateTime dataFinal = new DateTime();
            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                {
                    mensagens.Add("[ID] deve ser informado!");
                }
            }
            else
            {
                if (DateTime.TryParse(dtpDataInicial.Text, out dataInicial) == false)
                    mensagens.Add("[Data Inicial] inválida.");

                if (DateTime.TryParse(dtpDataFinal.Text, out dataFinal) == false)
                    mensagens.Add("[Data Final] inválida.");

                if (string.IsNullOrEmpty(cmbHoraInicial.Text))
                    mensagens.Add("[Hora Inicial] deve ser informada");

                if (string.IsNullOrEmpty(cmbHoraFinal.Text))
                    mensagens.Add("[Hora Final] deve ser informada");

                if (dataInicial > dataFinal)
                    mensagens.Add("[Data Final] deve ser maior que [Data Inicial]");

                if (cmbCampanhas.TextoEhSelecione())
                    mensagens.Add("[Campanha] deve ser Selecionado");

                if (cmbOperador.TextoEhSelecione())
                    mensagens.Add("[Operador] deve ser Selecionado");

                if (cmbSupervisor.TextoEhSelecione())
                    mensagens.Add("[Supervisor] deve ser Selecionado");

                if (cmbMailing.TextoEhSelecione())
                    mensagens.Add("[Mailing] deve ser Selecionado");

                if (chkStatusDeAtendimento.PossuiItemSelcionado() == false)
                    mensagens.Add("[Status de Atendimento] deve ser selecionado");

                if (chkStatusOferta.PossuiItemSelcionado() == false)
                    mensagens.Add("[Status de Oferta] deve ser selecionado");

            }



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

            saveFileDialog.FileName = "CALLPLUS_CONTATOS_TRABALHADOS " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

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

                            if (dgResultado.Rows.Count > 1)
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
        private string RetornarFiltroDeIdsSelecionadosCheckdListBox(CheckedListBox checkedListBox, string separador)
        {
            string idsSelecionados = "";
            List<ListItem> lista = new List<ListItem>();

            var collection = checkedListBox.CheckedItems;
            foreach (object item in collection)
            {
                ListItem itemAtual = (ListItem)item;
                lista.Add(itemAtual);

            }
            idsSelecionados = string.Join(separador, lista.Select(x => x.Value));
            return idsSelecionados;
        }


        private void linkTodos_StatusAtendimento_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chkStatusDeAtendimento.SetarTodosRegistros(check: true);
        }

        private void linkTodos_StatusNenhum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chkStatusDeAtendimento.SetarTodosRegistros(check: false);
        }


        private void ListaAuditoriaDeVendaForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();

                //btnPesquisar_Click(sender, e);
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

        private void btnFechar_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void cmbSupervisor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupervisor.TextoEhSelecione() == false)
                {

                    int idSupervisor = 0;
                    int.TryParse(cmbSupervisor.SelectedValue?.ToString(), out idSupervisor);

                    if (cmbSupervisor.TextoEhTodos())
                        idSupervisor = -1;

                    CarregarOperadores(idSupervisor: idSupervisor);
                }
                else
                {
                    cmbOperador.DefinirComoSelecione();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os operadores\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void linkTodos_StatusAuditoria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chkStatusOferta.SetarTodosRegistros(check: true);
        }

        private void linkNenhum_StatusAuditoria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            chkStatusOferta.SetarTodosRegistros(check: false);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(buscaRapida: false);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                MessageBox.Show("Não foi possível carregar", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ParametrosPesquisaValidos(false))
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmbOperador_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkListarAtivos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool listarInativos = chkListarAtivos.Checked;
                int idCampanha = int.Parse(cmbCampanhas.SelectedValue.ToString());
                CarregarMailings(idCampanha, listarInativos);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Não foi possível carregar as campanhas.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }

        }

        private void cmbCampanhas_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool listarInativos = chkListarAtivos.Checked;
                int idCampanha = 0;
                int.TryParse(cmbCampanhas.SelectedValue?.ToString(), out idCampanha);
                CarregarMailings(idCampanha, listarInativos);
                CarregarStatusDeOferta(idCampanha);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ocorreu um erro inesperado: Mensagem {exception.Message};", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.Error(exception);
            }
        }

    }
}
