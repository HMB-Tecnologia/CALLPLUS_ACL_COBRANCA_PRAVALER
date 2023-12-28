using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Callplus.CRM.Tabulador.App.Operacao
{
	public partial class NegociacaoInclusaoForm : Form
	{
		public NegociacaoInclusaoForm(Usuario usuario, Prospect prospect, int? idStatusAcordo, List<int> idsContratos = null)
		{
			_negociacaoService = new NegociacaoService();
			_statusDeAcordoService = new StatusDeAcordoService();
			_contratoService = new ContratoService();

			_usuario = usuario;
			_idStatusAcordo = idStatusAcordo;
			_prospect = prospect;
			_idsContratos = idsContratos;

			InitializeComponent();
		}

		#region PROPRIEDADES

		private Contrato _contratoDaNegociacao;
		private List<Contrato> _listaContratosDaNegociacao;
		private Usuario _usuario;
		private NegociacaoService _negociacaoService;
		private Prospect _prospect;
		private int? _idStatusAcordo;
		private List<int> _idsContratos;
		private readonly StatusDeAcordoService _statusDeAcordoService;
		private ContratoService _contratoService;
		public bool Atualizar = false;
		private double _valorPrincipal = 0.0;
		private double _valorParcela = 0.0;

		#endregion PROPRIEDADES

		#region MÉTODOS

		private void ResetarCampos()
		{
			txtValorPrincipal.Text = string.Empty;
			txtValorParcelas.Text = string.Empty;
			dtpDataVencimento.Text = string.Empty;
			cmbParcela.ResetarComSelecione(true);
			cmbPrazo.ResetarComSelecione(true);
		}

		public void NovaNegociacao(Contrato contrato)
		{
			_contratoDaNegociacao = contrato;
			if (_contratoDaNegociacao != null)
			{
				CarregarPrazoNegociacao();
				CarregarParcelas();

				ShowDialog();
			}
		}

		private void CarregarPrazoNegociacao()
		{
			var prazo = _negociacaoService.RetornarPrazoNegociacao(true);
			cmbPrazo.PreencherComSelecione(prazo, x => x.Id, x => x.Nome);
		}

		private void CarregarParcelas()
		{
			var parcelas = _negociacaoService.RetornarParcelaNegociacao(true);
			cmbParcela.PreencherComSelecione(parcelas, x => x.Id, x => x.Nome);
		}

		private Negociacao CriarNegociacao()
		{
			var negociacao = new Negociacao
			{
				IdTipoAcordo = int.Parse(tsAcordo_cmbStatusAcordo.ComboBox.SelectedValue.ToString()),
				ValorPrincipal = _valorPrincipal,
				ValorDasParcelas = _valorParcela,
				IdUsuario = _usuario.Id,
				IdPrazo = int.Parse(cmbPrazo.SelectedValue.ToString()),
			};

			int.TryParse(cmbParcela.Text, out int quantidade);
			negociacao.QuantidadeDeParcela = quantidade;

			DateTime.TryParse(dtpDataVencimento.Value.ToString("dd/MM/yyyy"), out DateTime dataVencimento);
			negociacao.DataVencimento = dataVencimento;

			return negociacao;
		}

		//private bool VerificarSeExisteAcordo(long iDTitulo)
		//{
		//	return _negociacaoService.VerificarSeExisteAcordo(iDTitulo);
		//}

		private bool PodeSalvar()
		{
			var mensagens = new List<string>();
			if (dgParcelas.Rows.Count <= 0)
				mensagens.Add("Favor incluir a(s) parcela(s) do acordo!");

			if (!DateTime.TryParse(dtpDataVencimento.Text, out DateTime data))
				mensagens.Add("Informe a data de vencimento!");

			if (string.IsNullOrEmpty(cmbPrazo.Text) || cmbPrazo.TextoEhSelecione())
				mensagens.Add("Informe o prazo!");

			if (_valorPrincipal < 0 || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
				mensagens.Add("Informe o valor principal!");

			if (_valorParcela < 0 || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
				mensagens.Add("Informe o valor da parcela!");

			var podeContinuar = mensagens.Any() == false;
			if (podeContinuar == false)
			{
				var msgFinal = string.Join("\n", mensagens.ToArray());
				MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			return podeContinuar;
		}

		//private bool VerificarSePodeIncluirNegociacao()
		//{
		//    long idContrato = _contratoDaNegociacao.Id;
		//    int idUsuario = _usuario.Id;
		//    List<string> msgs = _negociacaoCtl.PodeIncluirNegociacao(idContrato, idUsuario);

		//    var podeContinuar = msgs.Any() == false;
		//    if (podeContinuar == false)
		//    {
		//        var msgFinal = string.Join("\n", msgs.ToArray());
		//        MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		//    }
		//    return podeContinuar;
		//}

		private void IncluirNovaNegociacao()
		{
			long idNegociacao = 0;

			var negociacao = CriarNegociacao();
			negociacao.Parcelas = ObterParcelasDoAcordo();

			if (_idsContratos.Count == 0)
			{
				idNegociacao = _negociacaoService.IncluirNegociacao(negociacao, _prospect.Id, _contratoDaNegociacao.Id, _prospect.Campo001);
				if (idNegociacao < 0)
					throw new SystemException("Não foi possível obter o ID da nova negociação criada.");

				foreach (var parcela in negociacao.Parcelas)
					_negociacaoService.IncluirParcelaNegociacao(parcela, idNegociacao, negociacao.IdUsuario);
			}
			else
			{
				foreach (var idContrato in _idsContratos)
				{
					idNegociacao = _negociacaoService.IncluirNegociacao(negociacao, _prospect.Id, idContrato, _prospect.Campo001);
					if (idNegociacao < 0)
						throw new SystemException("Não foi possível obter o ID da nova negociação criada.");
				}

				foreach (var parcela in negociacao.Parcelas)
					_negociacaoService.IncluirParcelaNegociacao(parcela, idNegociacao, negociacao.IdUsuario);
			}

		}

		private List<ParcelaAcordo> ObterParcelasDoAcordo()
		{
			List<ParcelaAcordo> parcelas = new List<ParcelaAcordo>();
			foreach (DataGridViewRow row in dgParcelas.Rows)
			{
				if (row.IsNewRow) continue;

				ParcelaAcordo parcela = new ParcelaAcordo
				{
					NumeroDaParcela = int.Parse(row.Cells[nameof(colParcelaParcelas)].Value.ToString()),
					DataVencimento = DateTime.Parse(row.Cells[nameof(colVencimentoParcelas)].Value.ToString()),
					ValorPrincipal = _valorPrincipal,
					ValorDaParcela = _valorParcela,
				};
				parcelas.Add(parcela);
			}

			return parcelas;
		}

		private void AtualizarTotalDaNegociacao()
		{
			decimal valorTotalNegociado = 0;
			//var titulosSelecionados = ObterTitulosSelecionados();
			//foreach (var titulo in titulosSelecionados)
			//{
			//    if (VerificarSeExisteAcordo(titulo.IDTitulo) == false)
			//        valorTotalNegociado += titulo.Montante;
			//}

			//txtTotalNegociado.Text = $"R$ {valorTotalNegociado:N2}";
			//txtValorPrincipal.Text = valorTotalNegociado.ToString();
		}

		private void IncluirParcelas()
		{
			if (PodeIncluirParcelas())
			{
				int qtdParcela = int.Parse(cmbParcela.Text);

				for (int i = 1; i <= qtdParcela; i++)
				{
					DateTime data = RetornarProximaData(i);
					int indice = dgParcelas.Rows.Add();
					dgParcelas.Rows[indice].Cells[nameof(colParcelaParcelas)].Value = i;
					if (i == 1)
						dgParcelas.Rows[indice].Cells[nameof(colVencimentoParcelas)].Value = dtpDataVencimento.Value.ToString("dd/MM/yyyy");
					else
						dgParcelas.Rows[indice].Cells[nameof(colVencimentoParcelas)].Value = data.ToString("dd/MM/yyyy");

					dgParcelas.Rows[indice].Cells[nameof(colValorPrincipalParcelas)].Value = txtValorPrincipal.Text;
					dgParcelas.Rows[indice].Cells[nameof(colValordaParcelaParcelas)].Value = txtValorParcelas.Text;
				}
			}
		}

		private DateTime RetornarProximaData(int adicionar)
		{
			adicionar--;
			if (int.Parse(cmbPrazo.SelectedValue.ToString()) == 4)
			{
				DateTime data = DateTime.Parse(dtpDataVencimento.Text);
				return data.AddMonths(adicionar);
			}
			else if (int.Parse(cmbPrazo.SelectedValue.ToString()) == 3)
			{
				DateTime data = DateTime.Parse(dtpDataVencimento.Text);
				return data.AddDays(adicionar * 15);
			}
			else
			{
				DateTime data = DateTime.Parse(dtpDataVencimento.Text);
				return data.AddDays(adicionar * 7);
			}
		}

		private bool PodeIncluirParcelas()
		{
			var mensagens = new List<string>();

			if (_valorPrincipal < 0 || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
				mensagens.Add("Informe o valor principal!");

			if (string.IsNullOrEmpty(cmbParcela.Text) || cmbParcela.TextoEhSelecione())
				mensagens.Add("Informe a quantidade de parcelas!");

			if (_valorPrincipal < 0 || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
				mensagens.Add("Informe o valor da parcela!");

			if (string.IsNullOrEmpty(cmbPrazo.Text) || cmbPrazo.Text == "SELECIONE...")
				mensagens.Add("Informe o prazo!");

			if (!DateTime.TryParse(dtpDataVencimento.Text, out DateTime data))
				mensagens.Add("Informe a data de vencimento!");

			var prazo = int.Parse(cmbPrazo.SelectedValue.ToString());
			if (prazo != 1 && cmbParcela.Text == "1")
				mensagens.Add("Para esse tipo de acordo a quantidade de parcelas deve ser maior que 1!");

			if (dgParcelas.Rows.Count > 0)
				mensagens.Add("Já existem parcelas cadastradas!");

			var podeContinuar = mensagens.Any() == false;
			if (podeContinuar == false)
			{
				var msgFinal = string.Join("\n", mensagens.ToArray());
				MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return podeContinuar;
		}

		private void CarregarStatusDeAcordo()
		{
			var statusDeAcordo = _statusDeAcordoService.ListarStatusDeAcordo(_prospect.IdCampanha, (int)TipoStatusDeAcordo.Aceite, true);
			tsAcordo_cmbStatusAcordo.ComboBox.PreencherComSelecione(statusDeAcordo, x => x.Id, x => x.Nome);

			var statusAcordo = _statusDeAcordoService.RetornarStatusDeAcordo((int)_idStatusAcordo, _prospect.IdCampanha);
			tsAcordo_cmbStatusAcordo.ComboBox.SelectedValue = statusAcordo.Id.ToString();
			tsAcordo_cmbStatusAcordo.Enabled = false;
		}

		private void CarregarCamposDeAcordoComStatus()
		{
			txtNomeCliente.Text = _prospect.Campo002?.ToUpper();
			txtValorPrincipal.Text = $"R$ {_contratoDaNegociacao.Valor:N2}";

			if (tsAcordo_cmbStatusAcordo.ComboBox.SelectedItem.ToString() == "ACORDO TOTAL")
			{
				cmbParcela.Enabled = false;
				cmbParcela.SelectedValue = "1";
				cmbPrazo.Enabled = false;
				cmbPrazo.SelectedValue = "1";

				var listaContratos = _contratoService.Listar(_prospect.Id, baixado: false);
				var contratosSelecionados = listaContratos.Where(x => _idsContratos.Count > 0 ? _idsContratos.Any(y => y == x.Id) : _contratoDaNegociacao.Id == x.Id).ToList();

				foreach (var item in contratosSelecionados)
					if (double.TryParse(item.Valor, out double valorPrincipal))
						_valorPrincipal += double.Parse(item.Valor, CultureInfo.InvariantCulture);

				_valorParcela = _valorPrincipal;

				txtValorPrincipal.Text = $"R$ {_valorPrincipal:N2}";
				txtValorParcelas.Text = $"R$ {_valorPrincipal:N2}";

				lblIdContrato.Text = "Ids Contratos:";
				txtIDContrato.Text = string.Join(", ", listaContratos.Select(p => p.Id));
			}
			else if (tsAcordo_cmbStatusAcordo.ComboBox.SelectedItem.ToString() == "ACORDO PARCIAL")
			{
				var listaContratos = _contratoService.Listar(_prospect.Id, baixado: false);
				var contratosSelecionados = listaContratos.Where(x => _idsContratos.Count > 0 ? _idsContratos.Any(y => y == x.Id) : _contratoDaNegociacao.Id == x.Id).ToList();

				foreach (var item in contratosSelecionados)
					if (double.TryParse(item.Valor, out double valorPrincipal))
						_valorPrincipal += double.Parse(item.Valor, CultureInfo.InvariantCulture);

				txtValorPrincipal.Text = $"R$ {_valorPrincipal:N2}";
				txtValorParcelas.Text = $"R$ {_valorPrincipal:N2}";

				lblIdContrato.Text = "Ids Contratos:";
				txtIDContrato.Text = string.Join(", ", contratosSelecionados.Select(p => p.Id));
			}
		}

		private void LimparGridParcela()
		{
			dgParcelas.Rows.Clear();
		}

		private void CarregarConfiguracaoInicial()
		{
			this.DialogResult = DialogResult.None;

			ResetarCampos();
			AtualizarTotalDaNegociacao();
			CarregarStatusDeAcordo();
			LimparGridParcela();
			CarregarCamposDeAcordoComStatus();
		}

		#endregion MÉTODOS

		#region EVENTOS

		private void negociacaoInclusao_Load(object sender, EventArgs e)
		{
			try
			{
				CarregarConfiguracaoInicial();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ocorreu um erro ao carregar as configurações iniciais." + ex.Message, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSalvarNegociacao_Click(object sender, EventArgs e)
		{
			if (!PodeSalvar()) return;

			var resultado = MessageBox.Show("Confirmar a criação da nova negociação?\nEste processo não pode ser desfeito.", "Callplus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (resultado == DialogResult.Yes)
			{
				try
				{
					IncluirNovaNegociacao();
					MessageBox.Show("Concluído!", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);

					Atualizar = true;
					Hide();
					Close();
				}
				catch (Exception erro)
				{
					MessageBox.Show($"Ocorreu um erro inesperado ao incluir uma nova Negociação.\nErro: {erro.Message}\nStacktrace:{erro.StackTrace}");
				}
			}
		}

		private void btnAdicionarParcela_Click(object sender, EventArgs e)
		{
			try
			{
				IncluirParcelas();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ocorreu o seguinte erro ao incluir a(s) parcela(s)" + ex.Message, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnRemoverParcelas_Click(object sender, EventArgs e)
		{
			LimparGridParcela();
		}

		private void cmbParcela_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_valorPrincipal <= 0 || cmbParcela.TextoEhSelecione()) return;

			var v = _valorPrincipal / Convert.ToInt32(cmbParcela.SelectedValue);
			_valorParcela = v;

			txtValorParcelas.Text = $"R$ {v:N2}";
		}

		#endregion EVENTOS
	}
}
