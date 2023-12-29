using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Layouts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
	public partial class CobrancaPravalerForm : Form
	{
		public CobrancaPravalerForm(Usuario usuario, long idAcordo, Prospect prospect, ContainerDeLayoutDeCamposDinamicos camposDinamicos, Atendimento atendimento,
		bool bloqueioStatus, bool fecharAoGravar, int? idStatusAcordo = null, bool edicao = true)
		{
			_logger = LogManager.GetCurrentClassLogger();
			_layoutDinamicoService = new LayoutDinamicoService();
			_atendimentoService = new AtendimentoService();
			_campanhaService = new CampanhaService();
			_checklistService = new ChecklistService();
			_cobrancaAtendimentoService = new AcordoDoAtendimentoService();
			_prospectService = new ProspectService();
			_statusDeAcordoService = new StatusDeAcordoService();
			_permissaoService = new PermissaoService();
			_contratoService = new ContratoService();
			_negociacaoService = new NegociacaoService();
			_camposDinamicos = camposDinamicos;

			_atendimento = atendimento;
			_usuario = usuario;
			_acordo = _cobrancaAtendimentoService.RetornarCobrancaAtendimentoPravaler(idAcordo);
			_prospect = prospect;
			_idStatusAcordo = idStatusAcordo;
			_permiteEditar = edicao;
			_fecharAoGravar = fecharAoGravar;
			_bloqueioStatus = bloqueioStatus;

			InitializeComponent();
		}

		#region PROPRIEDADES

		private readonly ILogger _logger;
		private readonly LayoutDinamicoService _layoutDinamicoService;
		private readonly AtendimentoService _atendimentoService;
		private readonly CampanhaService _campanhaService;
		private readonly ChecklistService _checklistService;
		private readonly AcordoDoAtendimentoService _cobrancaAtendimentoService;
		private readonly ProspectService _prospectService;
		private readonly StatusDeAcordoService _statusDeAcordoService;
		private readonly PermissaoService _permissaoService;
		private readonly ContainerDeLayoutDeCamposDinamicos _camposDinamicos;

		private Usuario _usuario;
		private Prospect _prospect;
		private Contrato _contrato;
		private CobrancaAtendimentoPravaler _acordo;
		private bool _bloqueioStatus;
		private int? _idStatusAcordo;
		private bool _fecharAoGravar;
		private bool _permiteEditar;
		private bool _checklistAplicado;

		private List<int> _idsContratos = new List<int>();
		private List<Contrato> _contratosDoCliente;
		private ContratoService _contratoService;
		private NegociacaoService _negociacaoService;
		private Atendimento _atendimento;
		private int _countCheck = 0;

		public bool Atualizar { get; set; }

		#endregion PROPRIEDADES

		#region METODOS

		private void CarregarConfiguracaoInicial()
		{
			Atualizar = false;
			tsAcordo_cmbStatusAcordo.Enabled = false;
			tsAcordo_btnChecklist.Enabled = false;
			txtObservacao.Text = _acordo.Observacao;

			CarregarTipoDeStatusDoAcordo();
			CarregarControleDeEdicao();
			CarregarLayoutDinamicoDaCampanhaDoProspect();

			if (_idStatusAcordo != null && _idStatusAcordo > 0)
				ConfigurarStatusDoAcordo(_idStatusAcordo.Value);

			CarregarContratosDoProspect(_prospect.Id);
			AtualizarGridHistoricoNegociacoesContrato();
		}

		private void CarregarLayoutDinamicoDaCampanhaDoProspect()
		{
			var campanha = _campanhaService.RetornarCampanha(_prospect.IdCampanha);
			if (campanha?.IdLayoutCampoDinamico == null) return;
			int idLayout = campanha.IdLayoutCampoDinamico.Value;
			LayoutDeCampoDinamico layout = _layoutDinamicoService.RetornarLayoutDinamico(idLayout);
			_containerDeLayoutDinamico.Visible = false;
			_containerDeLayoutDinamico.CarregarLayout(layout);

			var valores = _layoutDinamicoService.ListarValoresDeCamposDinamicos(_prospect.Id, campanha.Id);
			_containerDeLayoutDinamico.PreencherCampos(valores);
			_containerDeLayoutDinamico.Visible = true;
		}

		private void CarregarControleDeEdicao()
		{
			if (!_permiteEditar)
			{
				tsAcordo.Enabled = false;
				txtObservacao.Enabled = false;

				foreach (var item in gbDadosPessoais.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
				{
					item.ReadOnly = true;
				}

				foreach (var item in gbDadosPessoais.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
				{
					item.Enabled = false;
				}
			}
		}

		private void CarregarStatusDeAcordo(object tipo)
		{
			tsAcordo_btnChecklist.Enabled = false;
			int idTipo = -1;

			if (tsAcordo_cmbTipoStatusAcordo.Text.ToUpper() == "ACORDO")
				tsAcordo_btnChecklist.Enabled = true;

			int.TryParse(tipo.ToString(), out idTipo);

			var statusDeAcordo = _statusDeAcordoService.ListarStatusDeAcordo(_prospect.IdCampanha, idTipo, true);
			tsAcordo_cmbStatusAcordo.ComboBox.PreencherComSelecione(statusDeAcordo, x => x.Id, x => x.Nome);

			if (idTipo > 0)
				tsAcordo_cmbStatusAcordo.ComboBox.ResetarComSelecione(true);
			else
				tsAcordo_cmbStatusAcordo.ComboBox.ResetarComSelecione(false);
		}

		private void CarregarTipoDeStatusDoAcordo()
		{
			tsAcordo_cmbTipoStatusAcordo.SelectedIndexChanged -= cmbTipoStatusOferta_SelectedIndexChanged;

			IEnumerable<TipoDeStatusDeAcordo> _tipoDeStatusDeOferta = _statusDeAcordoService.ListarTipoDeStatusDeOferta(1, true);
			tsAcordo_cmbTipoStatusAcordo.ComboBox.PreencherComSelecione(_tipoDeStatusDeOferta, x => x.Id, x => x.Nome);

			tsAcordo_cmbTipoStatusAcordo.SelectedIndexChanged += cmbTipoStatusOferta_SelectedIndexChanged;
		}

		private bool AtendeRegrasDeGravacao(bool considerarCheckList)
		{
			foreach (var item in gbDadosPessoais.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
			{
				item.ForeColor = SystemColors.WindowText;
			}

			var mensagens = new List<string>();

			if (tsAcordo_cmbTipoStatusAcordo.ComboBox.TextoEhSelecione())
			{
				mensagens.Add("[Tipo de Status] deve ser informado!");
			}

			if (tsAcordo_cmbStatusAcordo.ComboBox.TextoEhSelecione())
			{
				mensagens.Add("[Status] deve ser informado!");
			}

			if (tsAcordo_cmbTipoStatusAcordo.Text.ToUpper() == "ACEITE")
			{
				CallplusFormsUtil.ExibirMensagens(mensagens);

				return mensagens.Any() == false;
			}

			return true;//mudar depois
		}

		private void Gravar()
		{
			if (AtendeRegrasDeGravacao(true))
			{
				_acordo.IdStatusDoAcordo = Convert.ToInt32(tsAcordo_cmbStatusAcordo.ComboBox.SelectedValue);

				if (!string.IsNullOrEmpty(txtObservacao.Text))
					_acordo.Observacao = txtObservacao.Text;

				_acordo.IdOperador = _usuario.Id;

				_acordo.Id = _cobrancaAtendimentoService.GravarAcordoDoAtendimentoPravaler(_acordo);

				MessageBox.Show("Acordo gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

				Atualizar = true;

				if (_fecharAoGravar)
				{
					this.Close();
				}
			}
		}

		private void CarregarChecklist()
		{
			if (!AtendeRegrasDeGravacao(false)) return;

			_checklistAplicado = true;

			int idCampanha = _prospect.IdCampanha;
			string ddd = _prospect.Telefone01.ToString().Substring(0, 2);

			IEnumerable<Dominio.Entidades.Checklist> retorno = _checklistService.Listar(idCampanha, idProduto: 1, int.Parse(ddd), true);//mudar depois

			Dominio.Entidades.Checklist checklistSelecionado = null;

			if (retorno.Count() > 0)
			{
				string[] palavrasChave = null;

				foreach (var item in retorno)
				{
					if (item.palavraChaveMailing.ToString() != "")
					{
						palavrasChave = item.palavraChaveMailing.ToString().Split(';');

						for (int i = 0; i < palavrasChave.Length; i++)
						{
							if (_prospect.Mailing.Contains(palavrasChave[i]))
							{
								checklistSelecionado = item;
								break;
							}
						}

						if (checklistSelecionado != null)
						{
							break;
						}
					}
				}

				if (checklistSelecionado == null)
				{
					checklistSelecionado = retorno.FirstOrDefault();
				}

				_checklistAplicado = false;

				Checklist.ChecklistForm f = new Checklist.ChecklistForm(checklistSelecionado, this, (int)_acordo.IdTipoDeProduto, _camposDinamicos, _usuario);

				f.StartPosition = FormStartPosition.CenterScreen;
				f.ShowDialog();

				_checklistAplicado = f._checklistRealizado;

				if (_checklistAplicado)
				{
					tsAcordo_cmbTipoStatusAcordo.Enabled = false;
					tsAcordo_cmbStatusAcordo.Enabled = false;
					tsAcordo_btnChecklist.Enabled = false;

					gbDadosPessoais.Enabled = false;
				}
			}
			else
			{
				MessageBox.Show("Nenhum checklist disponível.\nPode prosseguir com a gravação!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void AtualizarGridHistoricoNegociacoesContrato()
		{
			dgAcordo.DataSource = _negociacaoService.RetornarHistoricoNegociacaoPorIdProspect(_prospect.Id);
		}

		private Contrato ObterContratoDoAtendimentoPorId(long idContrato)
		{
			return _contratosDoCliente.ToList()?.FirstOrDefault(x => x?.Id == idContrato);
		}

		string RetornarIds()
		{
			string ids = "";
			foreach (var item in _idsContratos)
			{
				string[] itemSplit = item.ToString().Split('-');

				if (itemSplit.Count() > 0)
					ids += itemSplit[0].Trim() + ",";
			}

			return ids;
		}

		public void NovoAcordo()
		{
			if (PodeIncluirNovaNegociacao() == false) return;
			
			var f = new NegociacaoInclusaoForm(_usuario, _prospect, _idStatusAcordo, _idsContratos);
			f.NovaNegociacao(_contrato);
			if (f.Atualizar)
			{
				BaixarContratosDoAcordo();
				AtualizarGridContratos();
				AtualizarGridHistoricoNegociacoesContrato();
				_contrato = null;
				foreach (var id in _idsContratos.ToList())
					_idsContratos.Remove(id);

				dgContrato.ClearSelection();
				_countCheck = 0;
			}
		}

		private void BaixarContratosDoAcordo()
		{
			var idsContratos = RetornarIds();

			if (string.IsNullOrEmpty(idsContratos))
				idsContratos = _contrato.Id.ToString() + ",";

			_contratoService.BaixarContratosDoAcordo(idsContratos, _prospect.Id);
		}

		private bool PodeIncluirNovaNegociacao()
		{
			var mensagens = new List<string>();

			if (tsAcordo_cmbStatusAcordo.ComboBox.SelectedItem.ToString() == "ACORDO PARCIAL")
			{
				if (dgContrato.Rows.Count == _countCheck)
					mensagens.Add("Para [Acordo Parcial] não é possível selecionar todos os contratos, para isso selecione [Acordo Total].");
			}

			if (_contrato == null)
			{
				mensagens.Add("Selecione um contrato.");
			}
			else
			{
				var idContrato = _contrato.Id;
				var idUsuario = _usuario.Id;
				var msgs = _negociacaoService.PodeIncluirNegociacao(idContrato, idUsuario);
				mensagens.AddRange(msgs);
			}

			var podeContinuar = mensagens.Any() == false;
			if (podeContinuar == false)
			{
				var msgFinal = string.Join("/n", mensagens.ToArray());
				MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return podeContinuar;
		}

		private void tsAcordo_cmbStatusAcordo_SelectedIndexChanged(object sender, EventArgs e)
		{
			_countCheck = 0;
			_idStatusAcordo = Convert.ToInt32(tsAcordo_cmbStatusAcordo.ComboBox.SelectedValue);
			CarregarProduto();
			RealizarAjustesGrid();
		}

		private void ConfigurarStatusDoAcordo(int idStatusAcordo)
		{
			tsAcordo_cmbStatusAcordo.SelectedIndexChanged -= tsAcordo_cmbStatusAcordo_SelectedIndexChanged;

			var statusAcordo = _statusDeAcordoService.RetornarStatusDeAcordo(idStatusAcordo, _prospect.IdCampanha);
			tsAcordo_cmbTipoStatusAcordo.ComboBox.SelectedValue = statusAcordo.IdTipoDeStatusDeOferta.ToString();
			tsAcordo_cmbStatusAcordo.ComboBox.SelectedValue = statusAcordo.Id.ToString();

			tsAcordo_cmbTipoStatusAcordo.Enabled = !_bloqueioStatus;
			tsAcordo_cmbStatusAcordo.Enabled = !_bloqueioStatus;

			tsAcordo_cmbStatusAcordo.SelectedIndexChanged += tsAcordo_cmbStatusAcordo_SelectedIndexChanged;
		}

		private void CarregarProduto()
		{
			int idTipoDeProduto = -1;
			bool ativo = true;
			bool? ativoBko = null;

			//if (_idStatusOferta == 21)
			//{
			//    idTipoDeProduto = 2;
			//    cmbProduto.ResetarComSelecione(habilitar: true);
			//}
			//else
			//{
			//    idTipoDeProduto = 1;
			//    cmbProduto.ResetarComSelecione(habilitar: false);
			//}

			IEnumerable<ProdutoDaOfertaDto> produtos = null;

			//if (_filtraPorFaixaDeRecarga && _campanhaAtual.Id != 16)
			//{
			//    produtos = _produtoService.ListarProdutoDaOfertaPorFaixaDeRecarga(_oferta.IdAtendimento).Where(x => x.idTipo == idTipoDeProduto).Distinct();
			//}            
			//else
			//{
			//produtos = _produtoService.ListarProdutoDaOfertaPorIdProspect(_prospect.IdCampanha, _prospect.Id, ativo, ativoBko).Where(x => x.idTipo == idTipoDeProduto).Distinct();
			////}

			//cmbProduto.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
			//cmbProduto.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
		}

		private void CarregarContratosDoProspect(long idProspect)
		{
			dgContrato.CellClick -= dgContratos_CellClick;

			var dt = _contratoService.ListarExibicao(idProspect, false);
			dgContrato.DataSource = dt;

			ConverteDatatableParaListaDeContratoCliente(dt);

			//AtualizarGridContratos(idProspect, false);

			dgContrato.CellClick += dgContratos_CellClick;

			RealizarAjustesGrid();
		}

		private void ConverteDatatableParaListaDeContratoCliente(DataTable dt)
		{
			_contratosDoCliente = (from rw in dt.AsEnumerable()
								   select new Contrato()
								   {
									   Id = Convert.ToInt32(rw["Id"]),
									   Cpf = Convert.ToString(rw["cpf"]),
									   CodContrato = Convert.ToString(rw["CodContrato"]),
									   Valor = Convert.ToString(rw["Valor"]),
									   Vencimento = Convert.ToString(rw["Vencimento"]),
									   DiasVencimento = Convert.ToString(rw["DiasVencimento"]),
								   }).ToList();
		}

		private void RealizarAjustesGrid()
		{
			dgContrato.Columns["Id"].Width = 35;
			dgContrato.Columns["Vencimento"].DefaultCellStyle.Format = "dd/MM/yyyy";
			dgContrato.Columns["CodContrato"].HeaderCell.Value = "Contrato";
			dgContrato.Columns["DiasVencimento"].HeaderCell.Value = "Dias Vencimento";

			if (tsAcordo_cmbStatusAcordo.ComboBox.SelectedItem.ToString() == "ACORDO PARCIAL")
			{
				DataGridViewCheckBoxColumn dgvCbcEditar = new DataGridViewCheckBoxColumn
				{
					ValueType = typeof(bool),
					Name = "dgvCbcEditar",
					HeaderText = "Selecionar",
					ReadOnly = false
				};

				dgContrato.Columns.Add(dgvCbcEditar);
				dgContrato.AutoResizeColumn(dgContrato.Columns["dgvCbcEditar"].Index);
			}
			else
			{
				if (dgContrato.Columns.Contains("dgvCbcEditar"))
					dgContrato.Columns.Remove("dgvCbcEditar");
			}
		}

		private void dgContratos_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			var indice = e.RowIndex;

			if (indice < 0) return;

			_contrato = null;

			var idContratoGrid = dgContrato.Rows[indice].Cells["Id"].Value?.ToString();
			long.TryParse(idContratoGrid, out long idContratoSelecionado);

			_contrato = ObterContratoDoAtendimentoPorId(idContratoSelecionado);
		}

		private void AtualizarGridContratos()
		{
			dgContrato.DataSource = _contratoService.ListarExibicao(_prospect.Id, false);
		}

		#endregion METODOS

		#region EVENTOS

		private void CobrancaPravalerForm_Load(object sender, EventArgs e)
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

		private void cmbTipoStatusOferta_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				CarregarStatusDeAcordo(tsAcordo_cmbTipoStatusAcordo.ComboBox.SelectedValue);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Não foi possível carregar os status do Atendimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void tsOferta_btnSalvar_Click(object sender, EventArgs e)
		{
			try
			{
				Gravar();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Não foi possível gravar o Acordo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CobrancaPravalerForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Char.IsLower(e.KeyChar))
				e.KeyChar = Char.ToUpper(e.KeyChar);

			if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
			{
				e.Handled = true;
			}
		}

		private void tsOferta_btnChecklist_Click(object sender, EventArgs e)
		{
			try
			{
				CarregarChecklist();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Não foi possível carregar o checklist!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnNovoAcordo_Click(object sender, EventArgs e)
		{
			try
			{
				NovoAcordo();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Não foi possível criar uma Novo Acordo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dgContrato_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			tcAcordo.SelectedTab = tcAcordo_tpAcordo;
		}

		private void btnCopiar_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtCodBarras.Text)) return;
			txtCodBarras.SelectAll();
			txtCodBarras.Copy();
		}

		private void btnCopiar_MouseHover(object sender, EventArgs e)
		{
			toolTip1.Show("Copiar código de barras", btnCopiar);
		}

		private void btnEnviarCodBarras_Click(object sender, EventArgs e)
		{

		}

		private void chkSms_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkWhatsApp_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void chkEmail_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void dgContrato_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dgContrato.RowCount > 0)
			{
				if (string.Compare(dgContrato.CurrentCell.OwningColumn.Name, "dgvCbcEditar") == 0)
				{
					this.dgContrato.EndEdit(); //Feche a edição do seu datagrid

					bool checkBoxStatus = Convert.ToBoolean(dgContrato.CurrentRow.Cells["dgvCbcEditar"].Value);
					if (checkBoxStatus == true)
					{
						_countCheck--;
						dgContrato.CurrentRow.Cells["dgvCbcEditar"].Value = false;
						int idContratoSelecionado = Convert.ToInt32(dgContrato["ID", e.RowIndex].Value);
						if (_idsContratos.Contains(idContratoSelecionado))
							_idsContratos.Remove(idContratoSelecionado);
					}
					else if (checkBoxStatus == false)
					{
						_countCheck++;
						dgContrato.CurrentRow.Cells["dgvCbcEditar"].Value = true;
						_idsContratos.Add(Convert.ToInt32(dgContrato["ID", e.RowIndex].Value));
					}
				}
			}
		}

		#endregion EVENTOS  

	}
}