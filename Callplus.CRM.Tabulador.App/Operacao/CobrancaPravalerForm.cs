using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
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
		public CobrancaPravalerForm(Usuario usuario, long idOferta, Prospect prospect, ContainerDeLayoutDeCamposDinamicos camposDinamicos, Atendimento atendimento,
			bool fecharAoGravar, int? idStatusOferta = null, bool edicao = true)
		{
			_logger = LogManager.GetCurrentClassLogger();

			_atendimentoService = new AtendimentoService();
			_campanhaService = new CampanhaService();
			_checklistService = new ChecklistService();
			_cobrancaAtendimentoService = new OfertaDoAtendimentoService();
			_prospectService = new ProspectService();
			_statusDeOfertaService = new StatusDeOfertaService();
			_permissaoService = new PermissaoService();
			_contratoService = new ContratoService();
			_negociacaoService = new NegociacaoService();
			_camposDinamicos = camposDinamicos;

			_atendimento = atendimento;
			_usuario = usuario;
			_oferta = _cobrancaAtendimentoService.RetornarCobrancaAtendimentoPravaler(idOferta);
			_prospect = prospect;
			_idStatusOferta = idStatusOferta;
			_permiteEditar = edicao;
			_fecharAoGravar = fecharAoGravar;

			InitializeComponent();
		}

		#region PROPRIEDADES

		private readonly ILogger _logger;

		private readonly AtendimentoService _atendimentoService;
		private readonly CampanhaService _campanhaService;
		private readonly ChecklistService _checklistService;
		private readonly OfertaDoAtendimentoService _cobrancaAtendimentoService;
		private readonly ProspectService _prospectService;
		private readonly StatusDeOfertaService _statusDeOfertaService;
		private readonly PermissaoService _permissaoService;
		private readonly ContainerDeLayoutDeCamposDinamicos _camposDinamicos;

		private Usuario _usuario;
		private Prospect _prospect;
		private Contrato _contrato;
		private CobrancaAtendimentoPravaler _oferta;

		private int? _idStatusOferta;
		private bool _fecharAoGravar;
		private bool _permiteEditar;
		private bool _checklistAplicado;

		private IEnumerable<Contrato> _contratosDoCliente;
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

			CarregarTipoDeStatusDeOferta();
			CarregarDadosIniciais();
			CarregarControleDeEdicao();
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

		private void CarregarDadosIniciais()
		{
			txtObservacao.Text = _oferta.Observacao;
		}

		private void CarregarStatusDeOferta(object tipo)
		{
			tsOferta_btnChecklist.Enabled = false;
			int idTipo = -1;

			if (tsOferta_cmbTipoStatusOferta.Text.ToUpper() == "ACEITE")
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
				_oferta.IdStatusDaOferta = Convert.ToInt32(tsOferta_cmbStatusOferta.ComboBox.SelectedValue);

				if (!string.IsNullOrEmpty(txtObservacao.Text))
					_oferta.Observacao = txtObservacao.Text;

				_oferta.IdOperador = _usuario.Id;

				_oferta.Id = _cobrancaAtendimentoService.GravarOfertaDoAtendimentoClaroMigracao(_oferta);

				MessageBox.Show("Oferta gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

				Checklist.ChecklistForm f = new Checklist.ChecklistForm(checklistSelecionado, this, (int)_oferta.IdTipoDeProduto, _camposDinamicos, _usuario);

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
				MessageBox.Show("Nenhum checklist disponível para a ofertaDoAtendimento.\nPode prosseguir com a gravação!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void NovoTitulo()
		{
			var f = new CadastrarTituloForm(_prospect);
			f.ShowDialog();

			RecarregarDadosAtualizadosDosContratosDoProspect();
		}

		private void RecarregarDadosAtualizadosDosContratosDoProspect()
		{
			if (_prospect == null) return;

			_contratosDoCliente = _contratoService.Listar(_prospect.Id, baixado: false);

			if (_contrato != null)
			{
				_contrato = ObterContratoDoAtendimentoPorId(_contrato.IDContrato);
				AtualizarGridTitulos(_contrato.Titulos);
			}
		}

		private void AtualizarGridHistoricoNegociacoesContrato(long idContrato)
		{
			dgAcordos.DataSource = _negociacaoService.RetornarHistoricoNegociacaoPorIdContrato(idContrato);
		}

		private Contrato ObterContratoDoAtendimentoPorId(long idContrato)
		{
			return _contratosDoCliente?.FirstOrDefault(x => x?.IDContrato == idContrato);
		}

		private void AtualizarGridTitulos(List<Titulo> titulos)
		{
			decimal valorTotalTitulos = 0;
			dgDetalhesDoTitulo.Rows.Clear();
			foreach (var titulo in titulos)
			{
				var indice = dgDetalhesDoTitulo.Rows.Add();
				valorTotalTitulos += titulo.Montante;

				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colID_detalhesTitulo)].Value = titulo.IDTitulo;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colNumeroDocumento)].Value = titulo.NumeroDocumento;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colEmissao_detalhesTitulo)].Value = titulo.DataEmissao.ToString("dd/MM/yyyy");
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colVencimento_detalhesTitulo)].Value = titulo.DataVencimento.ToString("dd/MM/yyyy");
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colValor_detalhesTiulo)].Value = titulo.Montante;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colAtribuicaoRazaoEspecial)].Value = titulo.AtribuicaoEspecial;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colTipoDocumento_detalhesTitulo)].Value = titulo.TipoDocumento;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colFormaDePagamento)].Value = titulo.FormaPagamento;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colCodNegociacao_detalhesTitulo)].Value = titulo.IDNegociacao;
				dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colStatusTitulo_detalhesTiulo)].Value = titulo.Status;
			}
			txtValorTotalTitulosDetalhe.Text = $"R$ {valorTotalTitulos:N2}";
		}

		public void NovoAcordo()
		{
			if (PodeIncluirNovaNegociacao() == false) return;

			var f = new NegociacaoInclusaoForm(_usuario);
			var idStatus = 0;//_atendimento?.id?.IDStatus ?? 0;
			f.NovaNegociacao(_contrato, idStatus);

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
				var idContrato = _contrato.IDContrato;
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

		private Titulo ObterTituloDoAtendimentoPorId(long idContrato, long idTitulo)
		{
			var contrato = ObterContratoDoAtendimentoPorId(idContrato);

			var titulos = contrato?.Titulos;

			return titulos?.First(titulo => titulo.IDTitulo == idTitulo);
		}

		#endregion METODOS

		#region EVENTOS

		private void OfertaMigracaoPreControleClaroForm_Load(object sender, EventArgs e)
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
					$"Não foi possível carregar os status da ofertaDoAtendimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					$"Não foi possível gravar a ofertaDoAtendimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		#endregion EVENTOS        


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

		private void btnNovoTitulo_Click(object sender, EventArgs e)
		{
			try
			{
				NovoTitulo();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Não foi possível criar um novo Título!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dgDetalhesDoTitulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				var senderGrid = (DataGridView)sender;
				bool botaoDetalheFoiClicado = senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn
					&& e.RowIndex >= 0 && senderGrid.Columns[e.ColumnIndex].Name == nameof(colBtnDetalhe_detalhesTitulo);

				if (botaoDetalheFoiClicado)
				{
					var idTiluloSelecionado = senderGrid.Rows[e.RowIndex].Cells[nameof(colID_detalhesTitulo)].Value;
					if (idTiluloSelecionado != null && long.TryParse(idTiluloSelecionado.ToString(), out long idTtitulo))
					{
						var idContrato = _contrato?.IDContrato;
						var idStatus = 0;//_statusDoAtendimento?.IDStatus ?? 0;

						if (idContrato != null)
						{
							var titulo = ObterTituloDoAtendimentoPorId(idContrato.Value, idTtitulo);
							if (titulo == null)
							{
								MessageBox.Show("Não foi possível obter o Tílulo para exibição dos detalhes", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}

							DetalheTituloForm f = new DetalheTituloForm(_usuario, _atendimento);
							f.ExibirDetalhes(titulo, _atendimento);
							RecarregarDadosAtualizadosDosContratosDoProspect();
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu um erro inesperado ao carregar os dealhes do título!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dgContrato_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0) return;

			_contrato = null;

			var idContratoGrid = dgContrato.Rows[e.RowIndex].Cells[nameof(colID_Contratos)].Value?.ToString();
			
			long.TryParse(idContratoGrid, out long idContratoSelecionado);
			_contrato = ObterContratoDoAtendimentoPorId(idContratoSelecionado);

			if (_contrato == null) return;

			btnNovoTitulo.Enabled = true;
			AtualizarGridTitulos(_contrato.Titulos);
			
			//Colocar um condicional if aqui para atualziar a grid de contrato quando for preciso
			AtualizarGridHistoricoNegociacoesContrato(_contrato.IDContrato);
		}
	}
}