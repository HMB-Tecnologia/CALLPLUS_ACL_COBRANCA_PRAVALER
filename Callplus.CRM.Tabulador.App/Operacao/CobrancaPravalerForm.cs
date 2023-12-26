﻿using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
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
			_statusDeOfertaService = new StatusDeOfertaService();
			_permissaoService = new PermissaoService();
			_contratoService = new ContratoService();
			_negociacaoService = new NegociacaoService();
			_camposDinamicos = camposDinamicos;

			_atendimento = atendimento;
			_usuario = usuario;
			_acordo = _cobrancaAtendimentoService.RetornarCobrancaAtendimentoPravaler(idAcordo);
			_prospect = prospect;
			_idStatusOferta = idStatusAcordo;
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
		private readonly StatusDeOfertaService _statusDeOfertaService;
		private readonly PermissaoService _permissaoService;
		private readonly ContainerDeLayoutDeCamposDinamicos _camposDinamicos;

		private Usuario _usuario;
		private Prospect _prospect;
		private Contrato _contrato;
		private CobrancaAtendimentoPravaler _acordo;
		private bool _bloqueioStatus;
		private int? _idStatusOferta;
		private bool _fecharAoGravar;
		private bool _permiteEditar;
		private bool _checklistAplicado;

		private List<Contrato> _contratosDoCliente;
		private ContratoService _contratoService;
		private NegociacaoService _negociacaoService;
		private Atendimento _atendimento;

		public bool Atualizar { get; set; }

		#endregion PROPRIEDADES

		#region METODOS

		private void CarregarConfiguracaoInicial()
		{
			Atualizar = false;
			tsOferta_cmbStatusOferta.Enabled = false;
			tsOferta_btnChecklist.Enabled = false;
			txtObservacao.Text = _acordo.Observacao;

			CarregarTipoDeStatusDeOferta();
			CarregarControleDeEdicao();
			CarregarContratosDoProspect(_prospect.Id);

			if (_idStatusOferta != null && _idStatusOferta > 0)
			{
				ConfigurarStatusDaOferta(_idStatusOferta.Value);
			}
			CarregarLayoutDinamicoDaCampanhaDoProspect();
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
				tsOferta.Enabled = false;
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

		private void CarregarStatusDeOferta(object tipo)
		{
			tsOferta_btnChecklist.Enabled = false;
			int idTipo = -1;

			if (tsOferta_cmbTipoStatusOferta.Text.ToUpper() == "ACORDO")
				tsOferta_btnChecklist.Enabled = true;

			int.TryParse(tipo.ToString(), out idTipo);

			var statusDeOferta = _statusDeOfertaService.ListarStatusDeOferta(_prospect.IdCampanha, idTipo, true);
			tsOferta_cmbStatusOferta.ComboBox.PreencherComSelecione(statusDeOferta, x => x.Id, x => x.Nome);

			if (idTipo > 0)
				tsOferta_cmbStatusOferta.ComboBox.ResetarComSelecione(true);
			else
				tsOferta_cmbStatusOferta.ComboBox.ResetarComSelecione(false);
		}

		private void CarregarTipoDeStatusDeOferta()
		{
			tsOferta_cmbTipoStatusOferta.SelectedIndexChanged -= cmbTipoStatusOferta_SelectedIndexChanged;

			IEnumerable<TipoDeStatusDeOferta> _tipoDeStatusDeOferta = _statusDeOfertaService.ListarTipoDeStatusDeOferta(1, true);
			tsOferta_cmbTipoStatusOferta.ComboBox.PreencherComSelecione(_tipoDeStatusDeOferta, x => x.Id, x => x.Nome);

			tsOferta_cmbTipoStatusOferta.SelectedIndexChanged += cmbTipoStatusOferta_SelectedIndexChanged;
		}

		private bool AtendeRegrasDeGravacao(bool considerarCheckList)
		{
			foreach (var item in gbDadosPessoais.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
			{
				item.ForeColor = SystemColors.WindowText;
			}

			var mensagens = new List<string>();

			if (tsOferta_cmbTipoStatusOferta.ComboBox.TextoEhSelecione())
			{
				mensagens.Add("[Tipo de Status] deve ser informado!");
			}

			if (tsOferta_cmbStatusOferta.ComboBox.TextoEhSelecione())
			{
				mensagens.Add("[Status] deve ser informado!");
			}

			if (tsOferta_cmbTipoStatusOferta.Text.ToUpper() == "ACEITE")
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
				_acordo.IdStatusDoAcordo = Convert.ToInt32(tsOferta_cmbStatusOferta.ComboBox.SelectedValue);

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
					tsOferta_cmbTipoStatusOferta.Enabled = false;
					tsOferta_cmbStatusOferta.Enabled = false;
					tsOferta_btnChecklist.Enabled = false;

					gbDadosPessoais.Enabled = false;
				}
			}
			else
			{
				MessageBox.Show("Nenhum checklist disponível.\nPode prosseguir com a gravação!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void RecarregarDadosAtualizadosDosContratosDoProspect()
		{
			if (_prospect == null) return;

			var dt = _contratoService.ListarExibicao(_prospect.Id, false);
			ConverteDatatableParaListaDeContratoCliente(dt);

			if (_contratosDoCliente != null)
			{
				_contrato = ObterContratoDoAtendimentoPorId(_contrato.Id);
				//AtualizarGridTitulos(_contrato.Titulos);
				AtualizarGridHistoricoNegociacoesContrato(_contrato.Id);
			}
		}

		private void AtualizarGridHistoricoNegociacoesContrato(long idContrato)
		{
			dgAcordo.DataSource = _negociacaoService.RetornarHistoricoNegociacaoPorIdContrato(idContrato);
		}

		private Contrato ObterContratoDoAtendimentoPorId(long idContrato)
		{
			return _contratosDoCliente.ToList()?.FirstOrDefault(x => x?.Id == idContrato);
		}

		private void AtualizarGridTitulos(List<Titulo> titulos)
		{
			//decimal valorTotalTitulos = 0;
			//dgDetalhesDoTitulo.Rows.Clear();
			//foreach (var titulo in titulos)
			//{
			//	var indice = dgDetalhesDoTitulo.Rows.Add();
			//	valorTotalTitulos += titulo.Montante;

			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colID_detalhesTitulo)].Value = titulo.IDTitulo;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colNumeroDocumento)].Value = titulo.NumeroDocumento;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colEmissao_detalhesTitulo)].Value = titulo.DataEmissao.ToString("dd/MM/yyyy");
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colVencimento_detalhesTitulo)].Value = titulo.DataVencimento.ToString("dd/MM/yyyy");
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colValor_detalhesTiulo)].Value = titulo.Montante;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colAtribuicaoRazaoEspecial)].Value = titulo.AtribuicaoEspecial;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colTipoDocumento_detalhesTitulo)].Value = titulo.TipoDocumento;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colFormaDePagamento)].Value = titulo.FormaPagamento;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colCodNegociacao_detalhesTitulo)].Value = titulo.IDNegociacao;
			//	dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colStatusTitulo_detalhesTiulo)].Value = titulo.Status;
			//}
			//txtValorTotalTitulosDetalhe.Text = $"R$ {valorTotalTitulos:N2}";
		}

		public void NovoAcordo()
		{
			if (PodeIncluirNovaNegociacao() == false) return;

			var f = new NegociacaoInclusaoForm(_usuario);
			f.NovaNegociacao(_contrato);

			RecarregarDadosAtualizadosDosContratosDoProspect();
		}

		private bool PodeIncluirNovaNegociacao()
		{
			var mensagens = new List<string>();
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

		private void tsOferta_cmbStatusOferta_SelectedIndexChanged(object sender, EventArgs e)
		{
			CarregarProduto();
		}

		private void ConfigurarStatusDaOferta(int idStatusOferta)
		{
			tsOferta_cmbStatusOferta.SelectedIndexChanged -= tsOferta_cmbStatusOferta_SelectedIndexChanged;

			var statusOferta = _statusDeOfertaService.RetornarStatusDeOferta(idStatusOferta, _prospect.IdCampanha);
			tsOferta_cmbTipoStatusOferta.ComboBox.SelectedValue = statusOferta.IdTipoDeStatusDeOferta.ToString();
			tsOferta_cmbStatusOferta.ComboBox.SelectedValue = statusOferta.Id.ToString();

			tsOferta_cmbTipoStatusOferta.Enabled = !_bloqueioStatus;
			tsOferta_cmbStatusOferta.Enabled = !_bloqueioStatus;

			tsOferta_cmbStatusOferta.SelectedIndexChanged += tsOferta_cmbStatusOferta_SelectedIndexChanged;
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

		private Titulo ObterTituloDoAtendimentoPorId(long idContrato, long idTitulo)
		{
			//var contrato = ObterContratoDoAtendimentoPorId(idContrato);

			//var titulos = contrato?.Titulos;

			return null;// titulos?.First(titulo => titulo.IDTitulo == idTitulo);
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
									   Id = Convert.ToInt32(rw["Id"])
								   }).ToList();
		}

		private void RealizarAjustesGrid()
		{
			dgContrato.Columns["Id"].Width = 35;
			dgContrato.Columns["Vencimento"].DefaultCellStyle.Format = "dd/MM/yyyy";
			dgContrato.Columns["CodContrato"].HeaderCell.Value = "Contrato";
		}

		private void dgContratos_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			var indice = e.RowIndex;

			if (indice < 0) return;

			_contrato = null;

			var idContratoGrid = dgContrato.Rows[indice].Cells["Id"].Value?.ToString();
			long.TryParse(idContratoGrid, out long idContratoSelecionado);

			_contrato = ObterContratoDoAtendimentoPorId(idContratoSelecionado);

			//if (_contrato == null) return;
			//var titulos = _contrato.Titulos;

			//btnNovoTitulo.Enabled = true;

			//AtualizarGridTitulos(titulos);
		}

		private void AtualizarGridContratos(long idProspect, bool atualizar)
		{
			if (atualizar)
				dgContrato.DataSource = _contratoService.ListarExibicao(idProspect, false);
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
				CarregarStatusDeOferta(tsOferta_cmbTipoStatusOferta.ComboBox.SelectedValue);
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

		private void OfertaMigracaoPreControleClaroForm_KeyPress(object sender, KeyPressEventArgs e)
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

			_contrato = null;

			var idContratoGrid = dgContrato.Rows[e.RowIndex].Cells["ID"].Value?.ToString();

			long.TryParse(idContratoGrid, out long idContratoSelecionado);
			_contrato = ObterContratoDoAtendimentoPorId(idContratoSelecionado);

			if (_contrato == null) return;

			//btnNovoTitulo.Enabled = true;
			//AtualizarGridTitulos(_contrato.Titulos);

			if (_contrato != null)
				AtualizarGridHistoricoNegociacoesContrato(_contrato.Id);

			tcOferta.SelectedTab = tcOferta_tpAcordo;
		}

		#endregion EVENTOS        
	}
}