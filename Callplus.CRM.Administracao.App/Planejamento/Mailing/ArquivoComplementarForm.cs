using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Mailing
{
	public partial class ArquivoComplementarForm : Form
	{
		public ArquivoComplementarForm(string titulo)
		{
			_logger = LogManager.GetCurrentClassLogger();
			_campanhaService = new CampanhaService();
			_mailingService = new MailingService();

			InitializeComponent();
			lblTitulo.Text = titulo;
		}

		#region PROPRIEDADES

		private readonly ILogger _logger;
		private readonly CampanhaService _campanhaService;
		private readonly MailingService _mailingService;
		private List<string[]> _arquivoD;
		private List<string[]> _arquivoCaixa;

		#endregion PROPRIEDADES

		#region ENUM INTERNO

		enum TipoArquivo
		{
			ArquivoCaixa = 1,
			ArquivoD = 2
		}

		#endregion ENUM INTERNO

		#region METODOS

		private void CarregarConfiguracoesIniciais()
		{
			CarregarMailing();
		}

		private void CarregarMailing()
		{
			var mailing = _mailingService.Listar(-1, true);
			cmbMailing.PreencherComSelecione(mailing, x => x.Id, x => x.Nome);
		}

		private void ResetarCampos()
		{
			if (cmbMailing.TextoEhSelecione())
			{
				txtCaminhoDoArquivoCaixa.Text = string.Empty;
				txtCaminhoDoArquivoD.Text = string.Empty;

				_arquivoD = null;
				_arquivoCaixa = null;
			}

			btnCarregar.Enabled = !cmbMailing.TextoEhSelecione() && (!string.IsNullOrEmpty(txtCaminhoDoArquivoCaixa.Text) || !string.IsNullOrEmpty(txtCaminhoDoArquivoD.Text));
			btnCarregarArquivoCaixa.Enabled = !cmbMailing.TextoEhSelecione();
			btnCarregarArquivoD.Enabled = !cmbMailing.TextoEhSelecione();
		}

		private void CarregarCampanha()
		{
			txtCampanha.Text = string.Empty;
			var campanha = _campanhaService.RetornarCampanha(Convert.ToInt32(cmbMailing.SelectedValue));

			if (campanha == null) return;

			txtCampanha.Text = string.Concat("Id: ", campanha?.Id.ToString(), " - ", "Nome: ", campanha?.Nome).ToUpper();
		}

		private void LocalizarArquivo(TipoArquivo tipoArquivo)
		{
			var FileDialog = new OpenFileDialog
			{
				Title = "Abrir Como",
				Filter = "Arquivos Textos (*.TXT)|*.TXT|Arquivos CSV (*.CSV)|*.CSV"
			};

			if (DialogResult.Cancel == FileDialog.ShowDialog()) return;

			var arquivoOrigem = FileDialog.FileName;
			var nome = FileDialog.SafeFileName;

			if (string.IsNullOrEmpty(arquivoOrigem))
			{
				MessageBox.Show("[Arquivo] inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (TipoArquivo.ArquivoD == tipoArquivo)
			{
				_arquivoD = null;
				_arquivoD = File.ReadAllLines(arquivoOrigem).Select(x => x.Split(';'))
					.Where(x => x[1].Equals("19")).ToList();

				if (_arquivoD?.Count() == 0 || _arquivoD == null)
				{
					MessageBox.Show($"[Arquivo] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				txtCaminhoDoArquivoD.Text = arquivoOrigem;
			}
			else
			{
				_arquivoCaixa = null;
				_arquivoCaixa = File.ReadAllLines(arquivoOrigem).Select(x => x.Split(';')).ToList();

				if (_arquivoCaixa?.Count() == 0 || _arquivoCaixa == null)
				{
					MessageBox.Show($"[Arquivo] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (_arquivoCaixa.FirstOrDefault()[0].ToUpper().Contains("CPF"))
					_arquivoCaixa.RemoveAt(0);

				if (!Texto.CpfPossuiFormatoValido(_arquivoCaixa.FirstOrDefault()[0]))
				{
					MessageBox.Show($"[Arquivo] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				txtCaminhoDoArquivoCaixa.Text = arquivoOrigem;
			}

			btnCarregar.Enabled = true;
		}

		private bool PodeGravar()
		{
			var mensagens = new List<string>();

			if (cmbMailing.TextoEhSelecione() || string.IsNullOrEmpty(cmbMailing.Text))
				mensagens.Add("Favor selecionar o mailing!");

			if (string.IsNullOrEmpty(txtCaminhoDoArquivoCaixa.Text) || string.IsNullOrEmpty(txtCaminhoDoArquivoD.Text))
				mensagens.Add("Selecione um arquivo para carregar!");

			CallplusFormsUtil.ExibirMensagens(mensagens);

			return !mensagens.Any();
		}

		private void Gravar()
		{
			if (PodeGravar())
			{

				Hide();
				Close();
			}
		}

		private void CarregarArquivoEmCache()
		{
			//Enviar para o banco os dados.
			//_mailingService.GravarArquivoCaixaEArquivoD();

			var listaArquivoD = new List<ArquivoD>();
			var listaArquivoCaixa = new List<ArquivoCaixa>();

			foreach (var linha in _arquivoD)
				listaArquivoD.Add(ConverterArquivoDParaObjeto(linha));

			var arquivoDVencimentosElegiveis = listaArquivoD.Where(x => (Convert.ToDateTime(x.Campo05) - DateTime.Today).TotalDays < 0).ToList();

			foreach (var linha in _arquivoCaixa)
				listaArquivoCaixa.Add(ConverterArquivoCaixaParaObjeto(linha));

			btnCarregar.Enabled = false;
			btnSalvar.Enabled = true;
		}

		private ArquivoD ConverterArquivoDParaObjeto(string[] linha)
		{
			if ((Convert.ToDateTime(linha[4].ToString()) - DateTime.Today).TotalDays < 0)
				return new ArquivoD()
				{
					Campo01 = linha.ElementAtOrDefault(0)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo02 = linha.ElementAtOrDefault(1)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo03 = linha.ElementAtOrDefault(2)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo04 = linha.ElementAtOrDefault(3)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo05 = linha.ElementAtOrDefault(4)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo06 = linha.ElementAtOrDefault(5)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo07 = linha.ElementAtOrDefault(6)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo08 = linha.ElementAtOrDefault(7)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo09 = linha.ElementAtOrDefault(8)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo10 = linha.ElementAtOrDefault(9)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo11 = linha.ElementAtOrDefault(10)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo12 = linha.ElementAtOrDefault(11)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo13 = linha.ElementAtOrDefault(12)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo14 = linha.ElementAtOrDefault(13)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo15 = linha.ElementAtOrDefault(14)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo16 = linha.ElementAtOrDefault(15)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo17 = linha.ElementAtOrDefault(16)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo18 = linha.ElementAtOrDefault(17)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo19 = linha.ElementAtOrDefault(18)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo20 = linha.ElementAtOrDefault(19)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo21 = linha.ElementAtOrDefault(20)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo22 = linha.ElementAtOrDefault(21)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo23 = linha.ElementAtOrDefault(22)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo24 = linha.ElementAtOrDefault(23)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo25 = linha.ElementAtOrDefault(24)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo26 = linha.ElementAtOrDefault(25)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo27 = linha.ElementAtOrDefault(26)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
					Campo28 = linha.ElementAtOrDefault(27)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				};
			return new ArquivoD();
		}

		private ArquivoCaixa ConverterArquivoCaixaParaObjeto(string[] linha)
		{
			return new ArquivoCaixa()
			{
				Cpf = linha.ElementAtOrDefault(0)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Fundo = linha.ElementAtOrDefault(1)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Datapg = linha.ElementAtOrDefault(2)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Qtd_boletos_pagos = linha.ElementAtOrDefault(3)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Maior_atraso = linha.ElementAtOrDefault(4)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Tipo_baixa = linha.ElementAtOrDefault(5)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Valor_boletos = linha.ElementAtOrDefault(6)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Valor_pago = linha.ElementAtOrDefault(7)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Valor_atualizado = linha.ElementAtOrDefault(8)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Desconto_cedido = linha.ElementAtOrDefault(9)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Juros_recebido = linha.ElementAtOrDefault(10)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Desconto_principal = linha.ElementAtOrDefault(11)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Faixa_atraso = linha.ElementAtOrDefault(12)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Tipo_canal = linha.ElementAtOrDefault(13)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Tipo_fundo = linha.ElementAtOrDefault(14)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Carteira = linha.ElementAtOrDefault(15)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Fase = linha.ElementAtOrDefault(16)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Data_acordo = linha.ElementAtOrDefault(17)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Data_vencimento = linha.ElementAtOrDefault(18)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Contra = linha.ElementAtOrDefault(19)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Login_promessa = linha.ElementAtOrDefault(20)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Patrono_do_caixa_fim = linha.ElementAtOrDefault(21)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Pagamento_antecipado_em_dia = linha.ElementAtOrDefault(22)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
				Tipo_produto = linha.ElementAtOrDefault(23)?.Replace("'", "")?.Replace(",", ".")?.Replace("\"", "").Trim(),
			};
		}

		#endregion METODOS

		#region EVENTOS

		private void ArquivoComplementarForm_Load(object sender, EventArgs e)
		{
			try
			{
				CarregarConfiguracoesIniciais();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar o formulário!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmbMailing_SelectionChangeCommitted(object sender, EventArgs e)
		{
			CarregarCampanha();
			ResetarCampos();
		}

		private void btnCarregarArquivoD_Click(object sender, EventArgs e)
		{
			try
			{
				LocalizarArquivo(TipoArquivo.ArquivoD);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar o [Arquivo D]!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCarregarArquivoCaixa_Click(object sender, EventArgs e)
		{
			try
			{
				LocalizarArquivo(TipoArquivo.ArquivoCaixa);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar o [Arquivo Caixa]!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCarregar_Click(object sender, EventArgs e)
		{
			try
			{
				CarregarArquivoEmCache();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar o formulário!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSalvar_Click(object sender, EventArgs e)
		{
			try
			{
				Gravar();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Salvar!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion EVENTOS
	}
}
