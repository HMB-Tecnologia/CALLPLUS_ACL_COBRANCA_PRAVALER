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
	public partial class ListaAuditoriaDeVendaForm : Form
	{
		public ListaAuditoriaDeVendaForm()
		{
			_logger = LogManager.GetCurrentClassLogger();
			_campanhaService = new CampanhaService();
			_usuarioService = new UsuarioService();
			_statusDeAuditoriaService = new StatusDeAuditoriaService();
			_auditoriaDeOfertaBko = new AuditoriaDeOfertaService();
			_ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
			_usuarioLogado = AdministracaoMDI._usuario;
			_campanha = new Campanha();

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
		private Campanha _campanha;
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
			cmbAuditoria.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

			PreencherCamposIniciais();
			CarregarCampanhas();
			CarregarStatusDeAuditoria();
			CarregarSupervisores();
			CarregarOperadores(idSupervisor: -1);
			CarregarRegrasDoPerfil();
			cmbAuditoria.SelectedIndex = 0;
		}

		private void PreencherCamposIniciais()
		{
			IEnumerable<KeyValuePair<int, string>> listaVazia = new List<KeyValuePair<int, string>>();

			cmbOperador.PreencherComSelecione(listaVazia);
		}

		private void CarregarCampanhas()
		{
			IEnumerable<Campanha> campanhas = _campanhaService.Listar(ativo: true);
			chkCampanha.Preencher(campanhas, x => x.Id, x => x.Nome);
		}

		private void CarregarStatusDeAuditoria()
		{
			IEnumerable<Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria> statusDeAuditoria = _statusDeAuditoriaService.Listar(-1, ativo: true);
			chkAuditoria.Preencher(statusDeAuditoria, x => x.Id, x => x.Nome);
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

		private void CarregarRegrasDoPerfil()
		{
			btnExportar.Enabled = AdministracaoMDI._usuario.PermiteExportacao;

			if (AdministracaoMDI._usuario.perfil.ToString() == "SUPERVISOR")
			{
				cmbSupervisor.SelectedValue = AdministracaoMDI._usuario.Id.ToString();
				cmbSupervisor.Enabled = false;

				CarregarOperadores(-1, AdministracaoMDI._usuario.Id);

				dtpDataInicial.Enabled = true;
				dtpDataFinal.Enabled = true;
			}
		}

		private void CarregarGrid(bool buscaRapida)
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
			string nomeProspect = "";
			string dataFiltro = "";
			int idTipoDaCampanha = 0;

			if (ParametrosPesquisaValidos(buscaRapida))
			{
				if (buscaRapida)
				{
					if (txtBuscaRapida.Text != "")
						idRegistro = int.Parse(txtBuscaRapida.Text);
				}
				else
				{
					idsCampanha = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkCampanha, separador: ",");
					idsAuditoria = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkAuditoria, separador: ",");

					if (idsCampanha.Length != 1)
					{
						idTipoDaCampanha = -2;
					}
					else
					{
						_campanha = _campanhaService.RetornarCampanha(Convert.ToInt32(idsCampanha));
						idTipoDaCampanha = _campanha.idTipoDaCampanha;
					}

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

					if (DateTime.TryParse(dtpDataFinal.Text, out data))
					{
						dataTermino = data;
					}

					nomeProspect = txtNome.Text;
					dataFiltro = cmbAuditoria.Text;
				}

				dgResultado.DataSource = _auditoriaDeOfertaBko.Listar(nomeProspect, dataInicio, dataTermino, idTipoDaCampanha, idsCampanha, idsAuditoria, telefone, cpf, idSupervisor, idOperador, dataFiltro);
				lblTotalRegistros.Text = dgResultado.RowCount + " Registro(s)";

				RealizarAjustesGrid();
			}
		}

		private void RealizarAjustesGrid()
		{
			dgResultado.Columns["Data Oferta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
			dgResultado.Columns["Data Auditoria"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
			dgResultado.Columns["Data Avaliação"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

			for (int i = dgResultado.Columns["Data Avaliação"].Index + 1; i < dgResultado.Columns.Count; i++)
			{
				dgResultado.Columns[i].Visible = false;
			}
		}

		private void IniciarEdicaoRegistro(int linha)
		{
			if (linha >= 0)
			{
				long id = (long)dgResultado.Rows[linha].Cells["Cód Oferta Bko"].Value;
				AuditoriaOferta_PortabilidadeMPForm f = new AuditoriaOferta_PortabilidadeMPForm(id);
				f.Iniciar();


				CarregarGrid(buscaRapida: false);
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
				if (DateTime.TryParse(dtpDataInicial.Text, out dataInicial))
				{

				}
				else
				{
					mensagens.Add("Data Inicial inválida.");
				}

				if (DateTime.TryParse(dtpDataFinal.Text, out dataFinal))
				{

				}
				else
				{
					mensagens.Add("Data Final inválida.");
				}

				if (dataInicial > dataFinal)
				{
					mensagens.Add("Data Final deve ser maior que a data inicial.");
				}

				if (txtTelefone.Text != "")
				{
					if (Texto.TelefonePossuiFormatoValido(txtTelefone.Text) == false)
						mensagens.Add("Telefone informado não é válido");
				}

				if (chkCampanha.PossuiItemSelcionado() == false)
					mensagens.Add("Selecione pelo menos uma campanha");

				if (chkAuditoria.PossuiItemSelcionado() == false)
					mensagens.Add("Selecione pelo menos um Status de Auditoria");
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
			string nomeProspect = "";
			string dataFiltro = "";
			int idTipoDaCampanha = 0;


			if (ParametrosPesquisaValidos(buscaRapida))
			{
				if (buscaRapida)
				{
					if (txtBuscaRapida.Text != "")
						idRegistro = int.Parse(txtBuscaRapida.Text);
				}
				else
				{
					idsCampanha = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkCampanha, separador: ",");
					idsAuditoria = RetornarFiltroDeIdsSelecionadosCheckdListBox(chkAuditoria, separador: ",");

					if (idsCampanha.Length != 1)
					{
						idTipoDaCampanha = -2;
					}
					else
					{
						_campanha = _campanhaService.RetornarCampanha(Convert.ToInt32(idsCampanha));
						idTipoDaCampanha = _campanha.idTipoDaCampanha;
					}

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

					if (DateTime.TryParse(dtpDataFinal.Text, out data))
					{
						dataTermino = data;
					}

					nomeProspect = txtNome.Text;
					dataFiltro = cmbAuditoria.Text;
				}

				_dtExportaVenda = _auditoriaDeOfertaBko.ExportarVenda(nomeProspect, dataInicio, dataTermino, idTipoDaCampanha, idsCampanha, idsAuditoria, telefone, cpf, idSupervisor, idOperador, dataFiltro);
			}
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



		#endregion METODOS

		#region EVENTOS        

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

		private void linkTodos_Campanha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			chkCampanha.SetarTodosRegistros(check: true);
		}

		private void linkNenhum_Campanha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			chkCampanha.SetarTodosRegistros(check: false);
		}

		private void linkTodos_StatusAuditoria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			chkAuditoria.SetarTodosRegistros(check: true);
		}

		private void linkNenhum_StatusAuditoria_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			chkAuditoria.SetarTodosRegistros(check: false);
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

		#endregion EVENTOS
	}
}
