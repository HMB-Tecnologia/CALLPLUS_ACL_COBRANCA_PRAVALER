using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Backoffice.AuditoriaDeVendas
{
    public partial class ListaAuditoriaDeVendaAgrupadaForm : Form
    {
        public ListaAuditoriaDeVendaAgrupadaForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _campanhaService = new CampanhaService();
            _usuarioService = new UsuarioService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _auditoriaDeOfertaBko = new AuditoriaDeOfertaService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _usuarioLogado = AdministracaoMDI._usuario;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly CampanhaService _campanhaService;
        private readonly UsuarioService _usuarioService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly AuditoriaDeOfertaService _auditoriaDeOfertaBko;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly Usuario _usuarioLogado;
        private DataTable _dtExportaVenda;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            ShowIcon = false;
            MaximizeBox = true;
            MinimizeBox = false;

            cmbOperador.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbSupervisor.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            PreencherCamposIniciais();
            CarregarCampanhas();
            CarregarStatusDeAuditoria();
            CarregarSupervisores();
            CarregarAuditores();
            CarregarRegrasDoPerfil();

            timerAutomacaoColeta.Start();
            timerAutomacaoProcessamento.Start();
        }

        private void PreencherCamposIniciais()
        {
            IEnumerable<KeyValuePair<int, string>> listaVazia = new List<KeyValuePair<int, string>>();

            cmbOperador.PreencherComTodos(listaVazia);
        }

        private void CarregarCampanhas()
        {
            IEnumerable<Campanha> campanhas = _campanhaService.Listar(ativo: true);
            cmbCampanha.PreencherComTodos(campanhas, x => x.Id, x => x.Nome);
        }

        private void CarregarStatusDeAuditoria()
        {
            //IEnumerable<Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria> statusDeAuditoria = _statusDeAuditoriaService.Listar(ativo: true);
            //chkAuditoria.Preencher(statusDeAuditoria, x => x.Id, x => x.Nome);
        }

        private void CarregarOperadores(int idCampanha = -1, int idSupervisor = -1)
        {
            IEnumerable<Usuario> operadores = _usuarioService.ListarOperadores(ativo: true, idCampanha: idCampanha, idSupervisor: idSupervisor);
            cmbOperador.PreencherComTodos(operadores, x => x.Id, x => x.Nome);
        }

        private void CarregarSupervisores()
        {
            IEnumerable<Usuario> supervisores = _usuarioService.ListarSupervisores(ativo: true);
            cmbSupervisor.PreencherComTodos(supervisores, x => x.Id, x => x.Nome);
        }

        private void CarregarAuditores()
        {
            IEnumerable<Usuario> auditores = _usuarioService.ListarAuditores(ativo: true);
            cmbAuditor.PreencherComTodos(auditores, x => x.Id, x => x.Nome);
        }

        private void CarregarRegrasDoPerfil()
        {
            btnExportar.Enabled = AdministracaoMDI._usuario.PermiteExportacao;

            if (AdministracaoMDI._usuario.perfil.ToString() == "SUPERVISOR")
            {
                cmbSupervisor.SelectedValue = AdministracaoMDI._usuario.Id.ToString();
                cmbSupervisor.Enabled = false;

                CarregarOperadores(-1, AdministracaoMDI._usuario.Id);

                dtpDataInicial.Enabled = false;
            }
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int idRegistro = -1;
            long telefone = -1;
            long cpf = -1;
            int idSupervisor = -1;
            int idOperador = -1;
            int idAuditor = -1;
            int idCampanha = -1;
            DateTime dataInicio = dtpDataInicial.Value;
            DateTime dataTermino = dtpDataFinal.Value;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    //if (txtBuscaRapida.Text != "")
                    //    idRegistro = int.Parse(txtBuscaRapida.Text);
                }
                else
                {
                    if (long.TryParse(txtTelefone.Text, out telefone) == false)
                        telefone = -1;

                    if (long.TryParse(txtCpf.Text, out cpf) == false)
                        cpf = -1;

                    if (int.TryParse(cmbSupervisor.SelectedValue.ToString(), out idSupervisor) == false)
                        idSupervisor = -1;

                    if (int.TryParse(cmbOperador.SelectedValue.ToString(), out idOperador) == false)
                        idOperador = -1;

                    if (int.TryParse(cmbAuditor.SelectedValue.ToString(), out idAuditor) == false)
                        idAuditor = -1;

                    if (int.TryParse(cmbCampanha.SelectedValue.ToString(), out idCampanha) == false)
                        idCampanha = -1;
                }

                dgVendaPorStatus.DataSource = _auditoriaDeOfertaBko.ListarVendaAgrupadaPorStatusDeAuditoria(dataInicio, dataTermino, idCampanha, idSupervisor, idOperador, idAuditor, telefone, cpf);

                //dgVendaHoraHora.DataSource = _auditoriaDeOfertaBko.ListarVendaAgrupadaPorHora(dataVenda, idCampanha, idSupervisor, idOperador, idAuditor, telefone, cpf);

                int totalVendas = 0;                
                int totalAgRotalog = 0;
                int totalAgMytracking = 0;
                int totalEntregues = 0;
                int totalAprovadas = 0;
                int totalReprovadas = 0;
                int totalErros = 0;

                foreach (DataGridViewRow item in dgVendaPorStatus.Rows)
                {
                    totalVendas += (int)item.Cells["QTDE"].Value;

                    if (item.Cells["idStatusAuditoria"].Value.ToString() == "100"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "102"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "103"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "105")
                    {
                        totalAgRotalog += (int)item.Cells["QTDE"].Value;
                    }
                    else if (item.Cells["idStatusAuditoria"].Value.ToString() == "200"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "201"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "203")
                    {
                        totalAgMytracking += (int)item.Cells["QTDE"].Value;
                    }
                    else if (item.Cells["idStatusAuditoria"].Value.ToString() == "106"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "204")
                    {
                        totalEntregues += (int)item.Cells["QTDE"].Value;
                    }
                    else if (item.Cells["idStatusAuditoria"].Value.ToString() == "1"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "2")
                    {
                        totalAprovadas += (int)item.Cells["QTDE"].Value;
                    }
                    else if (item.Cells["idStatusAuditoria"].Value.ToString() == "10"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "11"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "12"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "13"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "14"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "15"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "16"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "17"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "18"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "19"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "107"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "205")
                    {
                        totalReprovadas += (int)item.Cells["QTDE"].Value;
                    }
                    else if (item.Cells["idStatusAuditoria"].Value.ToString() == "20"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "101"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "104"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "202"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "300"
                        || item.Cells["idStatusAuditoria"].Value.ToString() == "400")
                    {
                        totalErros += (int)item.Cells["QTDE"].Value;
                    }
                }

                lblTotalVenda.Text = totalVendas + " venda(s)";

                lblResumoTotalVendas.Text = totalVendas.ToString();

                //Ag Rotalog
                lblResumoAgRotalog.Text = totalAgRotalog.ToString();
                decimal dPercAgRotalog = 0;
                if (totalAgRotalog > 0)
                    dPercAgRotalog = (Convert.ToDecimal(totalAgRotalog) / Convert.ToDecimal(totalVendas)) * 100;
                lblResumoPercentualAgRotalog.Text = dPercAgRotalog.ToString("0.00") + "%";

                //Ag Mytracking
                lblResumoAgMyTracking.Text = totalAgMytracking.ToString();
                decimal dPercAgMytracking = 0;
                if (totalAgMytracking > 0)
                    dPercAgMytracking = (Convert.ToDecimal(totalAgMytracking) / Convert.ToDecimal(totalVendas)) * 100;
                lblResumoPercentualAgMyTracking.Text = dPercAgMytracking.ToString("0.00") + "%";

                //canceladas
                lblResumoEntregues.Text = totalEntregues.ToString();
                decimal dPercEntregues = 0;
                if (totalEntregues > 0)
                    dPercEntregues = (Convert.ToDecimal(totalEntregues) / Convert.ToDecimal(totalVendas)) * 100;
                lblResumoPercentualEntregues.Text = dPercEntregues.ToString("0.00") + "%";

                //aprovadas
                lblResumoAprovadas.Text = totalAprovadas.ToString();
                decimal dPercAprovadas = 0;
                if (totalAprovadas > 0)
                    dPercAprovadas = (Convert.ToDecimal(totalAprovadas) / Convert.ToDecimal(totalVendas)) * 100;
                lblResumoPercentualAprovadas.Text = dPercAprovadas.ToString("0.00") + "%";

                //reprovadas
                lblResumoReprovadas.Text = totalReprovadas.ToString();
                decimal dPercReprovadas = 0;
                if (totalReprovadas > 0)
                    dPercReprovadas = (Convert.ToDecimal(totalReprovadas) / Convert.ToDecimal(totalVendas)) * 100;
                lblResumoPercentualReprovadas.Text = dPercReprovadas.ToString("0.00") + "%";

                //erros
                lblResumoErros.Text = totalErros.ToString();
                decimal dPercErros = 0;
                if (totalErros > 0)
                    dPercErros = (Convert.ToDecimal(totalErros) / Convert.ToDecimal(totalVendas)) * 100;
                lblResumoPercentualErros.Text = dPercErros.ToString("0.00") + "%";

                ////em análise
                //lblResumoEmAnalise.Text = totalEmAnalise.ToString();
                //decimal dPercEmAnalise = 0;
                //if (totalEmAnalise > 0)
                //    dPercEmAnalise = (Convert.ToDecimal(totalEmAnalise) / Convert.ToDecimal(totalVendas)) * 100;
                //lblResumoPercentualEmAnalise.Text = dPercEmAnalise.ToString("0.00") + "%";

                RealizarAjustesGrid();
                RealizarAjustesGrid();

                CarregarGridListaVenda(-1);
            }
        }

        private void CarregarGridListaVenda(int linha)
        {
            lblTituloListaVenda.Text = "Lista de Vendas";
            int idStatusDeAuditoria = -1;

            if (linha >= 0)
            {
                string StatusDeAuditoria = dgVendaPorStatus.Rows[linha].Cells["Status"].Value.ToString();

                lblTituloListaVenda.Text = "Lista de Vendas [" + StatusDeAuditoria + "]";

                idStatusDeAuditoria = (int)dgVendaPorStatus.Rows[linha].Cells["idStatusAuditoria"].Value;
            }

            long telefone = -1;
            long cpf = -1;
            int idSupervisor = -1;
            int idOperador = -1;
            int idAuditor = -1;
            int idCampanha = -1;
            DateTime dataInicio = dtpDataInicial.Value;
            DateTime dataTermino = dtpDataFinal.Value;

            if (long.TryParse(txtTelefone.Text, out telefone) == false)
                telefone = -1;

            if (long.TryParse(txtCpf.Text, out cpf) == false)
                cpf = -1;

            if (int.TryParse(cmbSupervisor.SelectedValue.ToString(), out idSupervisor) == false)
                idSupervisor = -1;

            if (int.TryParse(cmbOperador.SelectedValue.ToString(), out idOperador) == false)
                idOperador = -1;

            if (int.TryParse(cmbAuditor.SelectedValue.ToString(), out idAuditor) == false)
                idAuditor = -1;

            if (int.TryParse(cmbCampanha.SelectedValue.ToString(), out idCampanha) == false)
                idCampanha = -1;

            dgListaVenda.DataSource = _auditoriaDeOfertaBko.ListarVendaPorStatusDeAuditoria(dataInicio, dataTermino, idStatusDeAuditoria, idCampanha, idSupervisor, idOperador, idAuditor, telefone, cpf);
            lblTotalListaVenda.Text = dgListaVenda.RowCount + " registro(s)";

            RealizarAjustesGridListaVenda();

            CarregarGridListaHistorico(-1);
        }

        private void CarregarGridListaHistorico(int linha)
        {
            long idOferta = 0;

            lnkVendaSelecionada.Text = "";

            if (linha >= 0)
            {
                idOferta = Convert.ToInt64(dgListaVenda.Rows[linha].Cells["COD. VENDA"].Value.ToString());

                lnkVendaSelecionada.Text = idOferta.ToString();
            }
            
            dgListaHistorico.DataSource = _ofertaDoAtendimentoService.ListarHistoricoDaOfertaDoAtendimentoBKO(idOferta, 4);
            lblTotalHistoricoDaVenda.Text = dgListaHistorico.RowCount + " registro(s)";

            RealizarAjustesGridListaHistorico();
        }

        private void RealizarAjustesGrid()
        {
            dgVendaPorStatus.Columns["QTDE"].Width = 45;
            dgVendaPorStatus.Columns["QTDE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgVendaPorStatus.Columns["Quantidade"].DefaultCellStyle.Padding = new Padding(0, 0, 25, 0);

            for (int i = dgVendaPorStatus.Columns["QTDE"].Index + 1; i < dgVendaPorStatus.Columns.Count; i++)
            {
                dgVendaPorStatus.Columns[i].Visible = false;
            }
        }

        private void RealizarAjustesGridListaVenda()
        {
            //dgVendaPorStatus.Columns["Quantidade"].Width = 80;
            //dgVendaPorStatus.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////dgVendaPorStatus.Columns["Quantidade"].DefaultCellStyle.Padding = new Padding(0, 0, 25, 0);

            //for (int i = dgVendaPorStatus.Columns["Quantidade"].Index + 1; i < dgVendaPorStatus.Columns.Count; i++)
            //{
            //    dgVendaPorStatus.Columns[i].Visible = false;
            //}
        }

        private void RealizarAjustesGridListaHistorico()
        {
            dgListaHistorico.Columns[0].Visible = false;
            dgListaHistorico.Columns["Data Auditoria"].Width = 100;
            dgListaHistorico.Columns["Status"].Width = 300;
            dgListaHistorico.Columns["Auditor"].Width = 200;
        }

        private void IniciarListagemDeVenda(int linha)
        {
            if (linha >= 0)
            {
                CarregarGridListaVenda(linha);
            }
        }

        private void IniciarListagemDeHistorico(int linha)
        {
            if (linha >= 0)
            {
                CarregarGridListaHistorico(linha);
            }
        }

        private bool VerificarSePodeEditarOfertaBko(long idOfertaBko, int idTipoProduto)
        {
            List<string> mensagens = new List<string>();

            int idUsuario = _usuarioLogado.Id;
            mensagens = _ofertaDoAtendimentoService.VerificarSePodeEditarOfertaBKO(idUsuario, idOfertaBko, idTipoProduto).ToList();

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any();
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();
            DateTime dataInicial = new DateTime();

            if (buscaRapida)
            {
                //if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                //{
                //    mensagens.Add("[ID] deve ser informado!");
                //}
            }
            else
            {
                if (dtpDataFinal.Value.Date < dtpDataInicial.Value.Date)
                {
                    mensagens.Add("[Data Final] não pode ser menor que a [Data Início]");
                }

                if (cmbCampanha.TextoEhSelecione())
                {
                    mensagens.Add("[Campanha] deve ser informada");
                }

                if (txtTelefone.Text != "")
                {
                    if (Texto.TelefonePossuiFormatoValido(txtTelefone.Text) == false)
                        mensagens.Add("Telefone informado não é válido");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

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

        private void ExportarRelatorioCsv()
        {
            string nomeArquivo;
            DialogResult retry = DialogResult.Retry;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Exportar CSV";
            nomeArquivo = "CALLPLUS_AUDITORIA_VENDAS " + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";

            saveFileDialog.FileName = nomeArquivo;

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

                            CarregarVendaParaExportar(false);

                            if (_dtExportaVenda.Rows.Count >= 1)
                            {
                                for (int i = 0; i < _dtExportaVenda.Columns.Count; i++)
                                {
                                    sw.Write(_dtExportaVenda.Columns[i].ColumnName.ToString() + ";");
                                }

                                for (int i = 0; i < _dtExportaVenda.Rows.Count; i++)
                                {
                                    sw.WriteLine();

                                    for (int j = 0; j < _dtExportaVenda.Columns.Count; j++)
                                    {
                                        sw.Write(_dtExportaVenda.Rows[i][j].ToString().Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim() + ";");
                                    }
                                }

                                sw.Close();
                                retry = DialogResult.Cancel;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (_dtExportaVenda.Rows.Count >= 1)
                        MessageBox.Show("Arquivo gerado com sucesso!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Aquivo gerado sem dados!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                finally
                {
                    myStream.Close();
                }
            }
        }

        private void CarregarVendaParaExportar(bool buscaRapida)
        {
            int idRegistro = -1;
            long telefone = 0;
            long cpf = 0;
            int idSupervisor = 0;
            int idOperador = 0;
            string idsCampanha = "";
            string idsAuditoria = "";
            DateTime? dataInicio = null;
            DateTime? dataTermino = null;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    //if (txtBuscaRapida.Text != "")
                    //    idRegistro = int.Parse(txtBuscaRapida.Text);
                }
                else
                {
                    //idsCampanha = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkCampanha, separador: ",");
                    //idsAuditoria = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkAuditoria, separador: ",");

                    if (long.TryParse(txtTelefone.Text, out telefone) == false)
                        telefone = -1;

                    if (long.TryParse(txtCpf.Text, out cpf) == false)
                        cpf = -1;

                    if (int.TryParse(cmbSupervisor.SelectedValue.ToString(), out idSupervisor) == false)
                        idSupervisor = -1;

                    if (int.TryParse(cmbOperador.SelectedValue.ToString(), out idOperador) == false)
                        idOperador = -1;

                    DateTime data = DateTime.MaxValue;

                    if (DateTime.TryParse(dtpDataInicial.Text, out data))
                    {
                        dataInicio = data;
                    }

                    //if (DateTime.TryParse(dtpDataFinal.Text, out data))
                    //{
                    //    dataTermino = data;
                    //}
                }

                _dtExportaVenda = _auditoriaDeOfertaBko.ExportarVenda(dataInicio, dataTermino, idsCampanha, idsAuditoria, telefone, cpf, idSupervisor, idOperador);
            }
        }

        private void MonitorarStatusDeColetaDeAutomacao()
        {
            //ConfiguracaoDaAutomacao c = _auditoriaDeOfertaBko.ListarStatusAutomacao();

            //if (c != null)
            //{
            //    if (c.Coletar)
            //    {
            //        btnColetarVenda.Text = "Parar";
            //        btnColetarVenda.Image = Properties.Resources.cancel;
            //    }
            //    else
            //    {
            //        btnColetarVenda.Text = "Ativar";
            //        btnColetarVenda.Image = Properties.Resources.check;
            //    }

            //    btnColetarVenda.Enabled = true;
            //}
        }

        private void MonitorarStatusDeProcessamentoDeAutomacao()
        {
            //ConfiguracaoDaAutomacao c = _auditoriaDeOfertaBko.ListarStatusAutomacao();

            //if (c != null)
            //{
            //    if (c.Processar)
            //    {
            //        btnProcessarVenda.Text = "Parar";
            //        btnProcessarVenda.Image = Properties.Resources.cancel;
            //    }
            //    else
            //    {
            //        btnProcessarVenda.Text = "Ativar";
            //        btnProcessarVenda.Image = Properties.Resources.check;
            //    }

            //    btnProcessarVenda.Enabled = true;
            //}
        }
        
        #endregion METODOS

        #region EVENTOS        

        private void ListaAuditoriaDeVendaAgrupadaForm_Load(object sender, System.EventArgs e)
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

        private void dgVendaPorStatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarListagemDeVenda(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível listar as vendas do status!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSupervisor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupervisor.TextoEhSelecione() == false)
                {
                    int idSupervisor = int.Parse(cmbSupervisor.SelectedValue.ToString());
                    CarregarOperadores(idSupervisor: idSupervisor);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível carregar os operadores\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void dgListaVenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarListagemDeHistorico(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível selecionar o histórico!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkVendaSelecionada_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lnkVendaSelecionada.Text) && !lblTituloListaVenda.Text.Contains("PROCESSAMENTO"))
            {
                long id = Convert.ToInt64(lnkVendaSelecionada.Text);

                AuditoriaOferta_PortabilidadeMPForm f = new AuditoriaOferta_PortabilidadeMPForm(id);
                f.Iniciar();
            }
        }

        private void btnColetarVenda_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    timerAutomacaoColeta.Stop();

            //    btnColetarVenda.Enabled = false;

            //    if (btnColetarVenda.Text == "Ativar")
            //        _auditoriaDeOfertaBko.AtualizarStatusColetaAutomacao(true);
            //    else
            //        _auditoriaDeOfertaBko.AtualizarStatusColetaAutomacao(false);

            //    timerAutomacaoColeta.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro ao alterar o status da coleta.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnProcessarVenda_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    timerAutomacaoProcessamento.Stop();

            //    btnProcessarVenda.Enabled = false;

            //    if (btnProcessarVenda.Text == "Ativar")
            //        _auditoriaDeOfertaBko.AtualizarStatusProcessamentoAutomacao(true);
            //    else
            //        _auditoriaDeOfertaBko.AtualizarStatusProcessamentoAutomacao(false);

            //    timerAutomacaoProcessamento.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro ao alterar o status do processamento.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void timerAutomacao_Tick(object sender, EventArgs e)
        {
            try
            {
                MonitorarStatusDeProcessamentoDeAutomacao();
            }
            catch (Exception ex)
            {

            }
        }

        private void timerAutomacaoColeta_Tick(object sender, EventArgs e)
        {
            try
            {
                MonitorarStatusDeColetaDeAutomacao();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion EVENTOS        
    }
}
