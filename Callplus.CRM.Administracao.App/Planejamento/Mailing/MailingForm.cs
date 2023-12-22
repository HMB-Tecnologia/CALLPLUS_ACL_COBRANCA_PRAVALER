using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using MoreLinq;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.Mailing
{
	public partial class MailingForm : Form
	{
		public MailingForm(string titulo, Tabulador.Dominio.Entidades.Mailing mailing, IEnumerable<Tabulador.Dominio.Entidades.Campanha> campanhas)
		{
			_mailing = mailing;
			_campanhas = campanhas;

			_logger = LogManager.GetCurrentClassLogger();
			_mailingService = new MailingService();
			_campanhaService = new CampanhaService();
			_discadorService = new DiscadorService();

			InitializeComponent();
			lblTitulo.Text = titulo;
		}

		#region VARIAVEIS

		private readonly ILogger _logger;
		private readonly MailingService _mailingService;
		private readonly CampanhaService _campanhaService;
		private readonly DiscadorService _discadorService;
		private string _arquivoDestino = "";
		private string _caminhoServidorProcessamento = "";
		private string _arquivoOrigem = "";
		private Tabulador.Dominio.Entidades.Mailing _mailing;
		private IEnumerable<Tabulador.Dominio.Entidades.Campanha> _campanhas;
		private List<string> _campanhasDoMailing;

		public bool atualizar = false;
		public static int idLayout;
		public static string tableLayout;
		private List<string[]> _arquivoMailing;
		private List<string[]> _arquivoMarcacoesMailing;
		private string _sqlArquivoMailingLayout10 = string.Empty;
		private string _sqlArquivoMailingLayout16 = string.Empty;
		private string _sqlArquivoMailingLayout30 = string.Empty;
		private string _sqlArquivoMailingLayout60 = string.Empty;
		private string _sqlArquivoMarcacoes = string.Empty;
		private List<DadosMarcacoes> _marcacoesDistinct;
		private string _mensagemDeAviso;

		#endregion VARIAVEIS

		#region METODOS
		private void DesabilitarCampos()
		{
			if (_mailing != null)
			{
				cmbCampanha.Enabled = false;
				btnCarregarArquivoMailing.Enabled = false;
				btnCarregarArquivoMarcacoes.Enabled = false;
				cmbCampanhaArquivoMarcacoes.Enabled = false;
				pbProcessar.Visible = false;
				btnSalvar.Enabled = true;
			}
			else
				chkAtivo.Checked = true;
		}

		private bool PodeExportarEnviarMailing()
		{
			var mensagens = new List<string>();

			if (_mailing != null)
			{
				if (!_mailingService.VerificarSeMailingEstaProcessadoComSucesso(_mailing.id))
					mensagens.Add("O processamento do mailing ainda não foi finalizado!!");
			}
			else
			{
				mensagens.Add("Não há mailing selecionado!");
			}

			ExibirMensagens(mensagens);
			return !mensagens.Any();
		}

		private void CarregarDadosDoMailing()
		{
			if (_mailing != null)
			{
				cmbCampanha.SelectedValue = _mailing.idCampanha.ToString();
				txtNome.Text = _mailing.nome;
				txtObservacao.Text = _mailing.observacao;
				chkAtivo.Checked = _mailing.ativo;
				ConsultarTipoEnvioDadosDiscador();
			}
			else
			{
				cmbCampanhaArquivoMarcacoes.DefinirComoSelecione();
				ConsultarCaminhoDoServidor();
			}
		}

		private void SalvarMailing()
		{
			if (PodeSalvar())
			{
				if (_mailing == null) _mailing = new Tabulador.Dominio.Entidades.Mailing();

				if (_mailing.id == 0)
				{
					var f = new LoadingForm("Transferindo Arquivo Para Servidor Callplus");
					var start = Task.Factory.StartNew(() => { f.ShowDialog(); });
					TransferirArquivoParaServidorCallplus();
					Invoke(new MethodInvoker(() => { f.FecharFormLoad(); }));
					Task.WaitAny(start);
				}

				_mailing.idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
				_mailing.nome = txtNome.Text.Trim();
				_mailing.ativo = chkAtivo.Checked;
				_mailing.idCriador = AdministracaoMDI._usuario.Id;
				_mailing.idModificador = AdministracaoMDI._usuario.Id;
				_mailing.observacao = txtObservacao.Text.Trim();
				_mailing.nomeArquivo = _arquivoDestino;

				var idMailing = _mailingService.Gravar(_mailing);
				GravarMalingEmCache(idMailing);

				Thread.Sleep(3000);
				atualizar = true;
				this.Hide();
				this.Close();
			}
		}

		private void GravarMalingEmCache(int idMailing)
		{
			var retorno = string.Empty;
			if (_arquivoMailing != null && _arquivoMarcacoesMailing != null)
				if (idMailing > 0)
				{
					retorno += _mailingService.GravarArquivoDeMailingEMarcacoes(_sqlArquivoMailingLayout10, string.Empty, idMailing) + " - LAYOUT 10\n";
					retorno += _mailingService.GravarArquivoDeMailingEMarcacoes(_sqlArquivoMailingLayout16, string.Empty, idMailing) + " - LAYOUT 16\n";
					retorno += _mailingService.GravarArquivoDeMailingEMarcacoes(_sqlArquivoMailingLayout30, string.Empty, idMailing) + " - LAYOUT 30\n";
					retorno += _mailingService.GravarArquivoDeMailingEMarcacoes(_sqlArquivoMailingLayout60, string.Empty, idMailing) + " - LAYOUT 60\n";
					retorno += _mailingService.GravarArquivoDeMailingEMarcacoes(string.Empty, _sqlArquivoMarcacoes, idMailing) + " MARCAÇÕES";
				}
				else
					MessageBox.Show($"Ocorreu erro ao criar o Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

			txtObservacao.Text = retorno;
			txtObservacao.Update();
		}

		private void TransferirArquivoParaServidorCallplus()
		{
			if (!Directory.Exists(_caminhoServidorProcessamento))
			{
				Directory.CreateDirectory(_caminhoServidorProcessamento);
			}

			File.Copy(_arquivoOrigem, _arquivoDestino, true);
		}

		private bool PodeSalvar()
		{
			var mensagens = new List<string>();

			if (cmbCampanha.TextoEhSelecione() || string.IsNullOrEmpty(cmbCampanha.Text))
				mensagens.Add("Favor selecionar a campanha!");

			if (string.IsNullOrEmpty(txtNome.Text.Trim()))
				mensagens.Add("[Nome] deve ser informado.");

			var regex = new Regex("^[\\s-a-zA-Z0-9_-]*$");
			string nomeDoMailing = txtNome.Text;

			var formatoCorreto = regex.IsMatch(nomeDoMailing);

			if (formatoCorreto == false)
			{
				mensagens.Add("Nome do arquivo não pode conter caracteres especiais!");
			}

			if (ExisteNomeDoMailing())
				mensagens.Add("Já existe um mailing com esse nome!");

			if (_mailing == null || _mailing.id == 0)
			{
				if (string.IsNullOrEmpty(txtCaminhoDoArquivoMailing.Text))
					mensagens.Add("Selecione um [Arquivo de Mailing] para carregar!");

				if (string.IsNullOrEmpty(txtCaminhoDoArquivoMarcacoes.Text))
					mensagens.Add("Selecione o [Arquivo de Marcações] para carregar!");
			}

			ExibirMensagens(mensagens);
			return mensagens.Any() == false;
		}

		private bool ExisteNomeDoMailing()
		{
			if (_mailing == null) return false;
			return _mailingService.VerificarSeExisteNomeDoMailing(txtNome.Text.Trim());//
		}

		private void ExibirMensagens(List<string> mensagens)
		{
			if (mensagens.Any())
			{
				var msgFinal = string.Join("\n", mensagens);
				MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void LocalizarArquivo(bool localizarArquivoMailing)
		{
			var FileDialog = new OpenFileDialog
			{
				Title = "Abrir Como",
				Filter = "Arquivos Textos (*.TXT)|*.TXT"
			};

			if (DialogResult.Cancel == FileDialog.ShowDialog()) return;

			if (localizarArquivoMailing)
			{
				_arquivoOrigem = FileDialog.FileName;
				string nome = FileDialog.SafeFileName;

				if (string.IsNullOrEmpty(_arquivoOrigem))
					MessageBox.Show("[Arquivo] inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
				{
					txtCaminhoDoArquivoMailing.Text = _arquivoOrigem;
					_arquivoDestino = _caminhoServidorProcessamento + nome;

					var f = new LoadingForm("Armazenando Mailing em Cache");
					var start = Task.Factory.StartNew(() => { f.ShowDialog(); });
					ArmazenarMailingEmCache();
					Invoke(new MethodInvoker(() => { f.FecharFormLoad(); }));
					Task.WaitAny(start);
				}
			}
			else//localizarArquivoMarcações
			{
				if (string.IsNullOrEmpty(FileDialog.FileName))
					MessageBox.Show("[Arquivo] inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				else
				{
					txtCaminhoDoArquivoMarcacoes.Text = FileDialog.FileName;

					var f = new LoadingForm("Armazenando Marcações em Cache");
					var start = Task.Factory.StartNew(() => { f.ShowDialog(); });
					ArmazenarMarcacoesMailingEmCache(FileDialog.FileName);
					Invoke(new MethodInvoker(() => { f.FecharFormLoad(); }));
					Task.WaitAny(start);
				}
			}

			if (!string.IsNullOrEmpty(_mensagemDeAviso))
			{
				MessageBox.Show(_mensagemDeAviso, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				_mensagemDeAviso = string.Empty;
			}
		}

		private void ArmazenarMailingEmCache()
		{
			_arquivoMailing = File.ReadAllLines(_arquivoOrigem).Select(x => x.Split(';'))
				.Where(x => x[1].Equals("10")
				|| x[1].Equals("16")
				|| x[1].Equals("30")
				|| x[1].Equals("60")).ToList();

			if (_arquivoMailing?.Count == 0 || _arquivoMailing == null)
				_mensagemDeAviso = "Arquivo Inválido!";
			else
				btnCarregarArquivoMarcacoes.Enabled = true;
		}

		private void ArmazenarMarcacoesMailingEmCache(string FileName)
		{
			_arquivoMarcacoesMailing = File.ReadAllLines(FileName)?.Select(x => x?.Split(';')).ToList();

			if (_arquivoMarcacoesMailing?.Count == 0 || _arquivoMarcacoesMailing == null || _arquivoMarcacoesMailing?.FirstOrDefault()?.Count() > 2)
			{
				_mensagemDeAviso = $"[Arquivo] inválido!\n" +
					$"{(_arquivoMarcacoesMailing?.Count == 0 ? "Não existem linhas no arquivo de marcações.\n" : string.Empty)}" +
					$"{(_arquivoMarcacoesMailing == null ? "Não foi possível carregar o arquivo de marcações.\n" : string.Empty)}" +
					$"{(_arquivoMarcacoesMailing?.FirstOrDefault()?.Count() > 2 ? "O Arquivo de Marcações possui mais de 2 colunas.\n" : string.Empty)}";

				_arquivoMarcacoesMailing = null;
				cmbCampanhaArquivoMarcacoes.ResetarComSelecione(true);
				btnCarregar.Enabled = false;
				cmbCampanhaArquivoMarcacoes.Enabled = false;

				return;
			}

			_marcacoesDistinct = ObtemObjetoDeMarcacoes().DistinctBy(x => new { x.Cpf, x.Carteira }).ToList();
			_campanhasDoMailing = _marcacoesDistinct.DistinctBy(x => x.Carteira).Select(x => x.Carteira?.ToUpper()?.Trim()).ToList();
			CarregarCampanhasDoArquivoDeMarcacoes(_campanhasDoMailing);
		}

		private void CarregarCampanhasDoArquivoDeMarcacoes(List<string> campanhas)
		{
			var dic = new Dictionary<string, string>();
			int id = 0;
			foreach (var campanha in campanhas)
				dic.Add(Convert.ToString(id++), campanha);

			cmbCampanhaArquivoMarcacoes.PreencherComSelecione(dic.OrderBy(x => x.Value));
		}

		private void ConsultarTipoEnvioDadosDiscador()
		{
			Discador discador = new Discador();

			if (cmbCampanha.TextoEhSelecione()) return;
			int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
			discador = _discadorService.RetornarTipoEnvioDadosDiscador(idCampanha);

			cmdEnviarParaDiscador.Enabled = discador.spEnvioAutomaticoMailing != "" && _mailing != null;
			cmdExportarArquivo.Enabled = discador.spExportacaoMailing != "" && _mailing != null;
			lblDiscador.Text = "Discador: " + discador.Nome;
		}

		private void ConsultarCaminhoDoServidor()
		{
			if (cmbCampanha.TextoEhSelecione()) return;

			int idCampanha = int.Parse(cmbCampanha.SelectedValue.ToString());
			_caminhoServidorProcessamento = _campanhaService.RetornarCaminhoDoServidor(idCampanha);

			btnCarregarArquivoMailing.Enabled = !string.IsNullOrEmpty(_caminhoServidorProcessamento);
		}

		private void ExportarArquivo()
		{
			DialogResult retry = DialogResult.Retry;
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "txt files (*.txt)|*.txt";
			saveFileDialog.FilterIndex = 0;
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.Title = "Exportar Mailing";

			//saveFileDialog.FileName = "CALLPLUS-DISCADOR-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
			saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + _mailing.id + "_FULL.txt";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream myStream = null;

				try
				{
					if ((myStream = saveFileDialog.OpenFile()) != null)
					{
						while (retry == DialogResult.Retry)
						{
							try
							{
								var sw = new StreamWriter(myStream);

								var _dt = _mailingService.ExportarMailing(_mailing.id);

								for (int i = 0; i < _dt.Rows.Count; i++)
								{
									if (!string.IsNullOrEmpty(_dt.Rows[i][0].ToString().Trim()))
										sw.WriteLine(_dt.Rows[i][0].ToString());
								}

								sw.Close();
								retry = DialogResult.Cancel;
							}
							catch (Exception myException)
							{
								retry = MessageBox.Show(myException.Message, "Erro Exportação", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
							}
						}

						MessageBox.Show("Arquivo gerado com sucesso!", "Callplus Software");
					}
				}
				finally
				{
					myStream.Close();
				}
			}

		}

		private void CarregarArquivosEmCache(bool podeProcessar)
		{
			if (cmbCampanhaArquivoMarcacoes.TextoEhSelecione())
			{
				MessageBox.Show($"Selecione a Campanha do Arquivo de Marcações.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			btnCarregar.Enabled = false;
			cmbCampanhaArquivoMarcacoes.Enabled = false;
			cmbCampanhaArquivoMarcacoes.Update();

			var mailingDistinct = ObterObjetoDeMailing().DistinctBy(x => new { x.Campo03, x.Campo02 }).ToList();
			if (mailingDistinct != null && _marcacoesDistinct != null && _campanhasDoMailing != null && podeProcessar)
			{
				lblPorcentagemMailing.Visible = true;
				lblPorcentagemMarcacoesMailing.Visible = true;

				PreparaMailingParaGravar(mailingDistinct);
				PreparaMarcacoesParaGravar();

				btnSalvar.Enabled = true;
				cmbCampanhaArquivoMarcacoes.Enabled = false;
				cmbCampanha.Enabled = false;
				btnCarregarArquivoMailing.Enabled = false;
				btnCarregarArquivoMarcacoes.Enabled = false;
			}
		}

		private void PreparaMailingParaGravar(List<DadosMailing> mailingDistinct)
		{
			var sbArquivoMailing = new StringBuilder();
			sbArquivoMailing.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");

			var sbArquivoMailingLayout10 = new StringBuilder();
			var sbArquivoMailingLayout16 = new StringBuilder();
			var sbArquivoMailingLayout30 = new StringBuilder();
			var sbArquivoMailingLayout60 = new StringBuilder();
			sbArquivoMailingLayout10.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
			sbArquivoMailingLayout16.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
			sbArquivoMailingLayout30.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
			sbArquivoMailingLayout60.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");

			pbProcessar.Value = 0;
			pbProcessar.Maximum = mailingDistinct.Count;
			var linhasLayout10 = 0;
			var linhasLayout16 = 0;
			var linhasLayout30 = 0;
			var linhasLayout60 = 0;
			foreach (var mailing in mailingDistinct.ToList())
			{
				if (mailing.Campo02 == "10" && linhasLayout10 == 1000)
				{
					sbArquivoMailingLayout10.Length -= 3;//Remove último caractere
					sbArquivoMailingLayout10.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
					linhasLayout10 = 0;
				}
				else if (mailing.Campo02 == "16" && linhasLayout16 == 1000)
				{
					sbArquivoMailingLayout16.Length -= 3;//Remove último caractere
					sbArquivoMailingLayout16.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
					linhasLayout16 = 0;
				}
				else if (mailing.Campo02 == "30" && linhasLayout30 == 1000)
				{
					sbArquivoMailingLayout30.Length -= 3;//Remove último caractere
					sbArquivoMailingLayout30.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
					linhasLayout30 = 0;
				}
				else if (mailing.Campo02 == "60" && linhasLayout60 == 1000)
				{
					sbArquivoMailingLayout60.Length -= 3;//Remove último caractere
					sbArquivoMailingLayout60.AppendLine(" INSERT INTO Base_Pravaler_bulk_temp VALUES ");
					linhasLayout60 = 0;
				}

				sbArquivoMailing.AppendLine(InserirLinhaMailing(mailing));

				if (mailing.Campo02 == "10")
				{ sbArquivoMailingLayout10.AppendLine(InserirLinhaMailing(mailing)); linhasLayout10++; }
				else if (mailing.Campo02 == "16")
				{ sbArquivoMailingLayout16.AppendLine(InserirLinhaMailing(mailing)); linhasLayout16++; }
				else if (mailing.Campo02 == "30")
				{ sbArquivoMailingLayout30.AppendLine(InserirLinhaMailing(mailing)); linhasLayout30++; }
				else if (mailing.Campo02 == "60")
				{ sbArquivoMailingLayout60.AppendLine(InserirLinhaMailing(mailing)); linhasLayout60++; }

				mailingDistinct.RemoveAll(x => x?.Campo03 == mailing.Campo03 && x.Campo02 == mailing.Campo02);

				pbProcessar.Value++;
				lblPorcentagemMailing.Text = string.Format("Carregando Mailing... {0:p0}", pbProcessar.Value / (double)pbProcessar.Maximum);
				lblPorcentagemMailing.Update();
			}

			_sqlArquivoMailingLayout10 = sbArquivoMailingLayout10.ToString()?.Trim()?.TrimEnd(',');
			_sqlArquivoMailingLayout16 = sbArquivoMailingLayout16.ToString()?.Trim()?.TrimEnd(',');
			_sqlArquivoMailingLayout30 = sbArquivoMailingLayout30.ToString()?.Trim()?.TrimEnd(',');
			_sqlArquivoMailingLayout60 = sbArquivoMailingLayout60.ToString()?.Trim()?.TrimEnd(',');

			//byte[] bytes = Encoding.Default.GetBytes(_sqlArquivoMailing);
			//_sqlArquivoMailing = Encoding.UTF8.GetString(bytes);
		}

		private string InserirLinhaMailing(DadosMailing mailing)
		{
			return $"('{mailing.Campo01}','{mailing.Campo02}','{mailing.Campo03}','{mailing.Campo04}','{mailing.Campo05}','{mailing.Campo06}','{mailing.Campo07}','{mailing.Campo08}','{mailing.Campo09}','{mailing.Campo10}',"
				  + $"'{mailing.Campo11}','{mailing.Campo12}','{mailing.Campo13}','{mailing.Campo14}','{mailing.Campo15}','{mailing.Campo16}','{mailing.Campo17}','{mailing.Campo18}','{mailing.Campo19}','{mailing.Campo20}',"
				  + $"'{mailing.Campo21}','{mailing.Campo22}','{mailing.Campo23}','{mailing.Campo24}','{mailing.Campo25}','{mailing.Campo26}','{mailing.Campo27}','{mailing.Campo28}','{mailing.Campo29}','{mailing.Campo30}',"
				  + $"'{mailing.Campo31}','{mailing.Campo32}','{mailing.Campo33}','{mailing.Campo34}','{mailing.Campo35}','{mailing.Campo36}','{mailing.Campo37}','{mailing.Campo38}','{mailing.Campo39}','{mailing.Campo40}',"
				  + $"'{mailing.Campo41}','{mailing.Campo42}','{mailing.Campo43}','{mailing.Campo44}','{mailing.Campo45}','{mailing.Campo46}','{mailing.Campo47}','{mailing.Campo48}','{mailing.Campo49}','{mailing.Campo50}',"
				  + $"'{mailing.Campo51}','{mailing.Campo52}','{mailing.Campo53}','{mailing.Campo54}','{mailing.Campo55}','{mailing.Campo56}','{mailing.Campo57}','{mailing.Campo58}','{mailing.Campo59}','{mailing.Campo60}',"
				  + $"{0}),";
		}

		private void PreparaMarcacoesParaGravar()
		{
			var marcacoesDistinct = _marcacoesDistinct.Where(x => x.Carteira == cmbCampanhaArquivoMarcacoes.Text).ToList();
			if (marcacoesDistinct.Count == 0)
			{
				MessageBox.Show("Não existem linhas no arquivo de marcações para a campanha selecionada.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var sbArquivoMarcacoes = new StringBuilder();
			sbArquivoMarcacoes.AppendLine(" INSERT INTO MarcacoesMailing_bulk_temp VALUES ");

			pbProcessar.Value = 0;
			pbProcessar.Maximum = marcacoesDistinct.Count;
			var linhasMarcacoes = 0;
			foreach (var marcacao in marcacoesDistinct.ToList())
			{
				if (linhasMarcacoes == 1000)
				{
					sbArquivoMarcacoes.Length -= 3;//Remove último caractere
					if (marcacoesDistinct.Count > 0)
						sbArquivoMarcacoes.AppendLine(" INSERT INTO MarcacoesMailing_bulk_temp VALUES ");

					linhasMarcacoes = 0;
				}

				sbArquivoMarcacoes.AppendLine($"('{marcacao.Cpf}','{marcacao.Carteira}', {0}),");

				marcacoesDistinct.RemoveAll(x => x.Cpf == marcacao.Cpf);

				linhasMarcacoes++;
				pbProcessar.Value++;
				lblPorcentagemMarcacoesMailing.Text = string.Format("Carregando Marcações... {0:p0}", pbProcessar.Value / (double)pbProcessar.Maximum);
				lblPorcentagemMarcacoesMailing.Update();
			}
			_sqlArquivoMarcacoes = sbArquivoMarcacoes.ToString()?.Trim()?.TrimEnd(',');
		}

		private List<DadosMailing> ObterObjetoDeMailing()
		{
			var mailingEmCache = new List<DadosMailing>();
			if (_arquivoMailing != null)
				foreach (var linha in _arquivoMailing)
					mailingEmCache.Add(ConverterMailingParaObjeto(linha));
			return mailingEmCache;
		}

		private List<DadosMarcacoes> ObtemObjetoDeMarcacoes()
		{
			var marcacoesEmCache = new List<DadosMarcacoes>();
			if (_arquivoMarcacoesMailing != null)
				foreach (var linha in _arquivoMarcacoesMailing)
					marcacoesEmCache.Add(ConverterMarcacoesParaObjeto(linha));
			return marcacoesEmCache;
		}

		private DadosMailing ConverterMailingParaObjeto(string[] linha)
		{
			return new DadosMailing()
			{
				Campo01 = linha.ElementAtOrDefault(0)?.Replace("'", "")?.Replace(",", "."),
				Campo02 = linha.ElementAtOrDefault(1)?.Replace("'", "")?.Replace(",", "."),
				Campo03 = linha.ElementAtOrDefault(2)?.Replace("'", "")?.Replace(",", "."),
				Campo04 = linha.ElementAtOrDefault(3)?.Replace("'", "")?.Replace(",", "."),
				Campo05 = linha.ElementAtOrDefault(4)?.Replace("'", "")?.Replace(",", "."),
				Campo06 = linha.ElementAtOrDefault(5)?.Replace("'", "")?.Replace(",", "."),
				Campo07 = linha.ElementAtOrDefault(6)?.Replace("'", "")?.Replace(",", "."),
				Campo08 = linha.ElementAtOrDefault(7)?.Replace("'", "")?.Replace(",", "."),
				Campo09 = linha.ElementAtOrDefault(8)?.Replace("'", "")?.Replace(",", "."),
				Campo10 = linha.ElementAtOrDefault(9)?.Replace("'", "")?.Replace(",", "."),
				Campo11 = linha.ElementAtOrDefault(10)?.Replace("'", "")?.Replace(",", "."),
				Campo12 = linha.ElementAtOrDefault(11)?.Replace("'", "")?.Replace(",", "."),
				Campo13 = linha.ElementAtOrDefault(12)?.Replace("'", "")?.Replace(",", "."),
				Campo14 = linha.ElementAtOrDefault(13)?.Replace("'", "")?.Replace(",", "."),
				Campo15 = linha.ElementAtOrDefault(14)?.Replace("'", "")?.Replace(",", "."),
				Campo16 = linha.ElementAtOrDefault(15)?.Replace("'", "")?.Replace(",", "."),
				Campo17 = linha.ElementAtOrDefault(16)?.Replace("'", "")?.Replace(",", "."),
				Campo18 = linha.ElementAtOrDefault(17)?.Replace("'", "")?.Replace(",", "."),
				Campo19 = linha.ElementAtOrDefault(18)?.Replace("'", "")?.Replace(",", "."),
				Campo20 = linha.ElementAtOrDefault(19)?.Replace("'", "")?.Replace(",", "."),
				Campo21 = linha.ElementAtOrDefault(20)?.Replace("'", "")?.Replace(",", "."),
				Campo22 = linha.ElementAtOrDefault(21)?.Replace("'", "")?.Replace(",", "."),
				Campo23 = linha.ElementAtOrDefault(22)?.Replace("'", "")?.Replace(",", "."),
				Campo24 = linha.ElementAtOrDefault(23)?.Replace("'", "")?.Replace(",", "."),
				Campo25 = linha.ElementAtOrDefault(24)?.Replace("'", "")?.Replace(",", "."),
				Campo26 = linha.ElementAtOrDefault(25)?.Replace("'", "")?.Replace(",", "."),
				Campo27 = linha.ElementAtOrDefault(26)?.Replace("'", "")?.Replace(",", "."),
				Campo28 = linha.ElementAtOrDefault(27)?.Replace("'", "")?.Replace(",", "."),
				Campo29 = linha.ElementAtOrDefault(28)?.Replace("'", "")?.Replace(",", "."),
				Campo30 = linha.ElementAtOrDefault(29)?.Replace("'", "")?.Replace(",", "."),
				Campo31 = linha.ElementAtOrDefault(30)?.Replace("'", "")?.Replace(",", "."),
				Campo32 = linha.ElementAtOrDefault(31)?.Replace("'", "")?.Replace(",", "."),
				Campo33 = linha.ElementAtOrDefault(32)?.Replace("'", "")?.Replace(",", "."),
				Campo34 = linha.ElementAtOrDefault(33)?.Replace("'", "")?.Replace(",", "."),
				Campo35 = linha.ElementAtOrDefault(34)?.Replace("'", "")?.Replace(",", "."),
				Campo36 = linha.ElementAtOrDefault(35)?.Replace("'", "")?.Replace(",", "."),
				Campo37 = linha.ElementAtOrDefault(36)?.Replace("'", "")?.Replace(",", "."),
				Campo38 = linha.ElementAtOrDefault(37)?.Replace("'", "")?.Replace(",", "."),
				Campo39 = linha.ElementAtOrDefault(38)?.Replace("'", "")?.Replace(",", "."),
				Campo40 = linha.ElementAtOrDefault(39)?.Replace("'", "")?.Replace(",", "."),
				Campo41 = linha.ElementAtOrDefault(40)?.Replace("'", "")?.Replace(",", "."),
				Campo42 = linha.ElementAtOrDefault(41)?.Replace("'", "")?.Replace(",", "."),
				Campo43 = linha.ElementAtOrDefault(42)?.Replace("'", "")?.Replace(",", "."),
				Campo44 = linha.ElementAtOrDefault(43)?.Replace("'", "")?.Replace(",", "."),
				Campo45 = linha.ElementAtOrDefault(44)?.Replace("'", "")?.Replace(",", "."),
				Campo46 = linha.ElementAtOrDefault(45)?.Replace("'", "")?.Replace(",", "."),
				Campo47 = linha.ElementAtOrDefault(46)?.Replace("'", "")?.Replace(",", "."),
				Campo48 = linha.ElementAtOrDefault(47)?.Replace("'", "")?.Replace(",", "."),
				Campo49 = linha.ElementAtOrDefault(48)?.Replace("'", "")?.Replace(",", "."),
				Campo50 = linha.ElementAtOrDefault(49)?.Replace("'", "")?.Replace(",", "."),
				Campo51 = linha.ElementAtOrDefault(50)?.Replace("'", "")?.Replace(",", "."),
				Campo52 = linha.ElementAtOrDefault(51)?.Replace("'", "")?.Replace(",", "."),
				Campo53 = linha.ElementAtOrDefault(52)?.Replace("'", "")?.Replace(",", "."),
				Campo54 = linha.ElementAtOrDefault(53)?.Replace("'", "")?.Replace(",", "."),
				Campo55 = linha.ElementAtOrDefault(54)?.Replace("'", "")?.Replace(",", "."),
				Campo56 = linha.ElementAtOrDefault(55)?.Replace("'", "")?.Replace(",", "."),
				Campo57 = linha.ElementAtOrDefault(56)?.Replace("'", "")?.Replace(",", "."),
				Campo58 = linha.ElementAtOrDefault(57)?.Replace("'", "")?.Replace(",", "."),
				Campo59 = linha.ElementAtOrDefault(58)?.Replace("'", "")?.Replace(",", "."),
				Campo60 = linha.ElementAtOrDefault(59)?.Replace("'", "")?.Replace(",", "."),
			};
		}

		private DadosMarcacoes ConverterMarcacoesParaObjeto(string[] linha)
		{
			return new DadosMarcacoes()
			{
				Cpf = linha.ElementAtOrDefault(0)?.Replace("'", "")?.Replace(",", "."),
				Carteira = linha.ElementAtOrDefault(1)?.Replace("'", "")?.Replace(",", ".")
			};
		}

		private void EnviarDadosParaDiscador()
		{
			var path = "C://temp";
			string[] parteNome = txtNome.Text.Split('_');
			var idMailingDiscador = parteNome[0];

			var nomeDoArquivo = DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + idMailingDiscador + "_FULL.txt";
			var _file = _mailingService.ExportarMailing(_mailing.id);

			//TODO - Ajustando método para importar para área FTP.
			// _mailingService.ExportarMailingDiscador(_mailing.id);
			try
			{
				if (!Directory.Exists(path + "//exportTempFiles"))
				{
					Directory.CreateDirectory(path + "//exportTempFiles");
				}

				using (StreamWriter stream = File.AppendText(path + "//exportTempFiles//" + nomeDoArquivo))
				{
					//var sw = new StreamWriter(stream);

					var _dt = _mailingService.ExportarMailing(_mailing.id);

					for (int i = 0; i < _dt.Rows.Count; i++)
					{
						if (!string.IsNullOrEmpty(_dt.Rows[i][0].ToString().Trim()))
							stream.WriteLine(_dt.Rows[i][0].ToString());
					}
				}

				FtpWebRequest ftpRequest;
				FtpWebResponse ftpResponse;
				try
				{
					ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://172.20.20.227:21/callflex/importar/" + nomeDoArquivo));
					ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
					ftpRequest.Proxy = null;
					ftpRequest.UseBinary = true;
					ftpRequest.UsePassive = true;//coloquei agora
					ftpRequest.Credentials = new NetworkCredential("ccaedu", "9eb5fbc4095d");


					FileInfo arquivo = new FileInfo(path + "//exportTempFiles//" + nomeDoArquivo);
					byte[] fileContents = new byte[arquivo.Length];

					using (FileStream fr = arquivo.OpenRead())
					{
						fr.Read(fileContents, 0, Convert.ToInt32(arquivo.Length));
					}

					File.Delete(path + "//exportTempFiles//" + nomeDoArquivo);

					using (Stream writer = ftpRequest.GetRequestStream())
					{
						writer.Write(fileContents, 0, fileContents.Length);
					}

					//obtem o FtpWebResponse da operação de upload
					ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
					MessageBox.Show("Upload de arquivo concluído!\n" + ftpResponse.StatusDescription);
				}
				catch (Exception ex)
				{

					MessageBox.Show("Erro ao salvar arquivo no FTP: " + ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Falha ao Gerar o Arquivo, verifique permissões de pasta! " + ex);
			}
		}

		private void CarregarCampanhas()
		{
			cmbCampanha.PreencherComSelecione(_campanhas, campanha => campanha.Id, campanha => campanha.Nome);
		}

		#endregion METODOS 

		#region EVENTOS
		private void MailingForm_Load(object sender, EventArgs e)
		{
			try
			{
				CarregarCampanhas();
				CarregarDadosDoMailing();
				DesabilitarCampos();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show($"Ocorreu erro ao carregar os dados do Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmdExportarArquivo_Click(object sender, EventArgs e)
		{
			try
			{
				if (PodeExportarEnviarMailing())
					ExportarArquivo();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show($"Ocorreu erro ao exportar os dados do Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmdEnviarParaDiscador_Click(object sender, EventArgs e)
		{
			try
			{
				if (PodeExportarEnviarMailing())
				{
					EnviarDadosParaDiscador();
					MessageBox.Show("Processo finalizado.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				MessageBox.Show($"Ocorreu erro ao enviar dados do Mailing ao discador!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSalvar_Click(object sender, EventArgs e)
		{
			try
			{
				SalvarMailing();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show($"Ocorreu erro ao salvar o Mailing!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmdCarregarArquivoMailing_Click(object sender, EventArgs e)
		{
			try
			{
				LocalizarArquivo(true);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar o arquivo!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmdCarregarArquivoMarcacoes_Click(object sender, EventArgs e)
		{
			try
			{
				LocalizarArquivo(false);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar o arquivo!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCarregar_Click(object sender, EventArgs e)
		{
			try
			{
				CarregarArquivosEmCache(true);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Ocorreu erro ao Carregar os arquivos!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtCaminhoDoArquivoMarcacoes_TextChanged(object sender, EventArgs e)
		{
			cmbCampanhaArquivoMarcacoes.Enabled = !string.IsNullOrEmpty(txtCaminhoDoArquivoMarcacoes.Text) && !string.IsNullOrEmpty(txtCaminhoDoArquivoMailing.Text);
		}

		private void cmbCampanha_SelectionChangeCommitted(object sender, EventArgs e)
		{
			ConsultarCaminhoDoServidor();
			ConsultarTipoEnvioDadosDiscador();
		}

		private void cmbCampanhaArquivoMarcacoes_SelectionChangeCommitted(object sender, EventArgs e)
		{
			btnCarregar.Enabled = !string.IsNullOrEmpty(txtCaminhoDoArquivoMarcacoes.Text) && !string.IsNullOrEmpty(txtCaminhoDoArquivoMailing.Text) && !cmbCampanhaArquivoMarcacoes.TextoEhSelecione();
		}

		#endregion EVENTOS
	}
}
