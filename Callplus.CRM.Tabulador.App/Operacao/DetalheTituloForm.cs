using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
	public partial class DetalheTituloForm : Form
	{
		public DetalheTituloForm(Usuario usuario, Atendimento atendimento)
		{
			_atendimento = atendimento;
			_usuario = usuario;
			_tituloService = new TituloService();


			InitializeComponent();
		}

		#region PROPRIEDADES

		private long _idStatusAtendimento;
		private Titulo _titulo;
		private Usuario _usuario;
		private Atendimento _atendimento;
		private TituloService _tituloService;

		#endregion PROPRIEDADES

		private void fDetalheTitulo_Load(object sender, EventArgs e)
		{
			ResetarCampos();
		}

		public void ExibirDetalhes(Titulo titulo, Atendimento atendimento)
		{
			if (titulo == null)
				throw new ArgumentException(nameof(titulo));


			if (atendimento == null)
				throw new ArgumentException(nameof(atendimento));

			_titulo = titulo;
			_idStatusAtendimento = atendimento.Id;

			CarregarDadosDoTítulo(titulo);
			CarregarStatusTitulo(_idStatusAtendimento);
			ShowDialog();
		}

		private void CarregarStatusTitulo(long idStatus)
		{
			var titulo = _tituloService.Listar(idStatus, true);
			DataRow dataRow = titulo.NewRow();
			dataRow[""] = "SELECIONE...";
			dataRow[""] = "-1";
			titulo.Rows.Add(dataRow);

			cmbStatusTitulo.DataSource = titulo;
			cmbStatusTitulo.ValueMember = "IDStatus";
			cmbStatusTitulo.DisplayMember = "Status";
			cmbStatusTitulo.SelectedValue = "-1";
			//cmbStatusTitulo.PreencherComSelecione(titulo, x => x.IDTitulo, x => x.Status);
		}

		private void CarregarCamposIniciais()
		{
			CarregarStatusTitulo(_idStatusAtendimento);
		}

		private void ResetarCampos()
		{
			cmbStatusTitulo.ResetarComSelecione(true);
			dtDataVencimentoAtualizada.CustomFormat = "SELECIONE...";
			dtDataVencimentoAtualizada.Format = DateTimePickerFormat.Custom;
			dtDataVencimentoAtualizada.Enabled = false;
		}

		private void CarregarDadosDoTítulo(Titulo titulo)
		{
			txtNumeroDocumento.Text = titulo.NumeroDocumento;
			txtValorTitulo.Text = $"R$ {titulo.Montante:N2}";
			txtDataEmissaoTitulo.Text = titulo.DataEmissao.ToString("dd/MM/yyyy");
			txtDataVencimentTitulo.Text = titulo.DataVencimento.ToString("dd/MM/yyyy");
			txtTipoDocumento.Text = titulo.TipoDocumento;
			txtAtribuicaoEspecial.Text = titulo.AtribuicaoEspecial;
		}

		private void btnSalvar_Click(object sender, EventArgs e)
		{
			try
			{
				if (PodeSalvar() == false) return;

				//Verificar as mensagens de validacao no banco de dados
				if (VerificarSePodeAtualizarTitulo() == false) return;

				AtualizarStatusDoTitulo();
				MessageBox.Show("Concluído!", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Close();

			}
			catch (Exception erro)
			{
				MessageBox.Show($"Ocorreu um erro inesperado ao tentar salvar o Status do Título:\nErro:{erro.Message}\nStacktrace:{erro.StackTrace}", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void AtualizarStatusDoTitulo()
		{
			var marcacao = new MarcacaoStatusTitulo();

			marcacao.IDtitulo = _titulo.IDTitulo;
			marcacao.IDUsuario = _usuario.Id;
			marcacao.IDStatusTitulo = int.Parse(cmbStatusTitulo.SelectedValue.ToString());
			marcacao.IDAtendimento = _atendimento.Id;
			marcacao.NumeroNegociacao = txtNumeroNegociacao.Text;

			decimal.TryParse(txtValorBoleto.Text, out decimal valorBoleto);
			marcacao.ValorBoleto = valorBoleto;

			decimal.TryParse(txtValorAtualizado.Text, out decimal valorAtualizado);
			marcacao.ValorAtualizado = valorAtualizado;

			int.TryParse(txtQtdParcela.Text, out int quantidade);
			marcacao.QuantidadeParcela = quantidade;

			decimal.TryParse(txtValorParcelas.Text, out decimal valorParcelas);
			marcacao.ValorParcelas = valorParcelas;

			marcacao.DataNegociacaoFutura = dtDataFuturaNegociar.Value;
			marcacao.DataVencimento = dtDataVencimento.Value;
			marcacao.DataVencimentoAtualizado = dtDataVencimentoAtualizada.Value;

			_tituloService.AtualizarStatusDoTitulo(marcacao);
		}

		private bool PodeSalvar()
		{
			var mensagens = new List<string>();
			if (string.IsNullOrEmpty(cmbStatusTitulo.Text) || cmbStatusTitulo.TextoEhSelecione())
			{
				mensagens.Add("Selecione o Status do título");
			}

			int idStatusTitulo = 0;
			int.TryParse(cmbStatusTitulo.SelectedValue.ToString(), out idStatusTitulo);

			if (idStatusTitulo == 15 || idStatusTitulo == 17)
			{
				if (string.IsNullOrEmpty(txtNumeroNegociacao.Text) || string.IsNullOrEmpty(txtValorParcelas.Text) || string.IsNullOrEmpty(txtQtdParcela.Text) || string.IsNullOrEmpty(txtValorAtualizado.Text) || string.IsNullOrEmpty(txtValorBoleto.Text))
					mensagens.Add("Deve ser informdo o Número da negociação, Valor do Boleto, Valor Atualizado!");

				if (!double.TryParse(txtValorParcelas.Text, out double valor))
					mensagens.Add("Deve ser informdo o Valor da Parcela!");

				if (!double.TryParse(txtValorAtualizado.Text, out valor))
					mensagens.Add("Deve ser informdo o Valor Atualizado!");

				if (!double.TryParse(txtValorBoleto.Text, out valor))
					mensagens.Add("Deve ser informdo o Valor do Boleto!");

			}

			if (idStatusTitulo == 13 || idStatusTitulo == 14)
			{
				if (string.IsNullOrEmpty(txtNumeroNegociacao.Text) || string.IsNullOrEmpty(txtValorBoleto.Text) || string.IsNullOrEmpty(txtValorAtualizado.Text))
					mensagens.Add("Deve ser informdo o Número da negociação, Valor do Boleto, Valor Atualizado!");

				if (!double.TryParse(txtValorAtualizado.Text, out double valor))
					mensagens.Add("Deve ser informdo o Valor Atualizado!");

				if (!double.TryParse(txtValorBoleto.Text, out valor))
					mensagens.Add("Deve ser informdo o Valor do Boleto!");
			}

			if (idStatusTitulo == 18)
			{
				if (string.IsNullOrEmpty(txtNumeroNegociacao.Text) || string.IsNullOrEmpty(txtValorBoleto.Text) || string.IsNullOrEmpty(txtValorAtualizado.Text))
				{
					mensagens.Add("Deve ser informdo o Número da negociação, Valor do Boleto, Valor Atualizado!");
				}

				double valor = 0;
				if (!double.TryParse(txtValorAtualizado.Text, out valor))
				{
					mensagens.Add("Deve ser informdo o Valor Atualizado!");
				}

				if (!double.TryParse(txtValorBoleto.Text, out valor))
				{
					mensagens.Add("Deve ser informdo o Valor do Boleto!");
				}
			}

			var podeContinuar = mensagens.Any() == false;
			if (podeContinuar == false)
			{
				var msgFinal = string.Join("\n", mensagens.ToArray());
				MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return podeContinuar;
		}

		private bool VerificarSePodeAtualizarTitulo()
		{
			long idTitulo = _titulo.IDTitulo;
			int idUsuario = _usuario.Id;
			int idStatusTitulo;
			int.TryParse(cmbStatusTitulo.SelectedValue.ToString(), out idStatusTitulo);
			DateTime dataVencimento;
			DateTime dataNegociacaoFutura;
			DateTime dataVencimentoAtualizada;

			dataVencimento = dtDataVencimento.Value;
			dataNegociacaoFutura = dtDataFuturaNegociar.Value;
			dataVencimentoAtualizada = dtDataVencimentoAtualizada.Value;

			var msgs = _tituloService.PodeAtualizarStatusDoTitulo(idTitulo, idStatusTitulo, _idStatusAtendimento, idUsuario, dataVencimento, dataNegociacaoFutura, dataVencimentoAtualizada);
			var podeContinuar = msgs.Any() == false;
			if (podeContinuar == false)
			{
				var msgFinal = string.Join("\n", msgs.ToArray());
				MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return podeContinuar;
		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmbStatusTitulo_SelectionChangeCommitted(object sender, EventArgs e)
		{
			int idStatusTitulo = int.Parse(cmbStatusTitulo.SelectedValue.ToString());
			LiberarCampos(idStatusTitulo);
		}

		private void LiberarCampos(int idStatusTitulo)
		{
			txtNumeroNegociacao.Text = string.Empty;
			txtValorBoleto.Text = string.Empty;
			txtValorAtualizado.Text = string.Empty;
			txtValorParcelas.Text = string.Empty;
			txtQtdParcela.Text = string.Empty;
			dtDataVencimentoAtualizada.Enabled = false;

			//if (idStatusTitulo == 15 || idStatusTitulo == 17)
			//{
			//	txtNumeroNegociacao.Enabled = true;
			//	txtValorParcelas.Enabled = true;
			//	txtValorAtualizado.Enabled = true;
			//	txtQtdParcela.Enabled = true;
			//	dtDataVencimento.Enabled = true;
			//	dtDataFuturaNegociar.Enabled = false;
			//	txtValorBoleto.Enabled = true;
			//	dtDataVencimentoAtualizada.Enabled = true;

			//}

			//if (idStatusTitulo == 13 || idStatusTitulo == 14)
			//{
			//	txtNumeroNegociacao.Enabled = true;
			//	txtValorBoleto.Enabled = true;
			//	txtValorAtualizado.Enabled = true;
			//	txtValorParcelas.Enabled = false;
			//	txtQtdParcela.Enabled = false;
			//	dtDataVencimento.Enabled = true;
			//	dtDataFuturaNegociar.Enabled = false;
			//	dtDataVencimentoAtualizada.Enabled = true;
			//}

			//if (idStatusTitulo == 16)
			//{
			//	txtNumeroNegociacao.Enabled = false;
			//	txtValorBoleto.Enabled = false;
			//	txtValorAtualizado.Enabled = false;
			//	txtValorParcelas.Enabled = false;
			//	txtQtdParcela.Enabled = false;
			//	dtDataVencimento.Enabled = false;
			//	dtDataFuturaNegociar.Enabled = true;

			//}

			//if (idStatusTitulo == 18)
			//{
			//	txtNumeroNegociacao.Enabled = true;
			//	txtValorBoleto.Enabled = true;
			//	txtValorAtualizado.Enabled = true;
			//	dtDataVencimento.Enabled = true;
			//	dtDataFuturaNegociar.Enabled = false;
			//	txtValorParcelas.Enabled = false;
			//	txtQtdParcela.Enabled = false;
			//	dtDataVencimentoAtualizada.Enabled = true;
			//}
		}

		private void dtDataVencimentoAtualizada_ValueChanged(object sender, EventArgs e)
		{
			dtDataVencimentoAtualizada.Format = DateTimePickerFormat.Long;
		}
	}
}
