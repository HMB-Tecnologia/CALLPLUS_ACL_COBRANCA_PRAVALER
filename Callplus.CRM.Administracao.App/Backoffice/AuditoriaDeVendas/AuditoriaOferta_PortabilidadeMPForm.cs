using Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Backoffice.AuditoriaDeVendas
{
	public partial class AuditoriaOferta_PortabilidadeMPForm : Form
	{
		public AuditoriaOferta_PortabilidadeMPForm(long idOferta)
		{
			_logger = LogManager.GetCurrentClassLogger();

			_atendimentoService = new AtendimentoService();
			_prospectService = new ProspectService();
			_campanhaService = new CampanhaService();
			_ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
			_produtoService = new ProdutoService();
			_ofertaService = new ProspectService();
			_statusDeAuditoriaService = new StatusDeAuditoriaService();
			_layoutDinamicoService = new LayoutDinamicoService();
			_planoService = new PlanoPorOperadoraParaComparacaoService();
			_tipos = new TipoDeAvaliacaoDeAtendimento();
			_avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
			_usuarioLogado = AdministracaoMDI._usuario;
			_enderecoService = new EnderecoService();

			_oferta = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoMPPortabilidadeBKO(idOferta);
			_resumoDaOferta = _ofertaDoAtendimentoService.RetornarResumoDaOfertaDoAtendimentoBKO(idOferta, (int)_oferta.IdTipoDeProduto);

			if (_oferta != null)
			{
				_campanhaAtual = _campanhaService.RetornarCampanha(_oferta.IdCampanha);
				_campanhaEhExpress = _campanhaAtual.HabilitaCepExpress;
				_prospect = _prospectService.RetornarProspect(_oferta.IdProspect);
			}

			InitializeComponent();
		}

		#region PROPRIEDADES

		private readonly ILogger _logger;

		private readonly AtendimentoService _atendimentoService;
		private readonly CampanhaService _campanhaService;
		private readonly ProdutoService _produtoService;
		private readonly ProspectService _prospectService;
		private readonly ProspectService _ofertaService;
		private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
		private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
		private readonly LayoutDinamicoService _layoutDinamicoService;
		private readonly PlanoPorOperadoraParaComparacaoService _planoService;
		private readonly TipoDeAvaliacaoDeAtendimento _tipos;
		private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
		private Tabulador.Dominio.Entidades.StatusDeAuditoria _statusDeAuditoriaAtual;
		private readonly EnderecoService _enderecoService;
		private readonly Usuario _usuarioLogado;
		private bool _ofertaFoiAtualizada;
		private Campanha _campanhaAtual;
		private readonly bool _campanhaEhExpress;
		private bool _cepPossuiElegibilidade;
		private Prospect _prospect;

		private OfertaDoAtendimentoMPPortabilidadeBKO _oferta;
		private ResumoDaOfertaDoAtendimentoBkoDTO _resumoDaOferta;
		private HistoricoDaOfertaDoAtendimentoPortabilidadeBKO _historicoOfertaBKO;
		private AvaliacaoDeAtendimento _avaliacaoDeAtendimento;

		public bool Atualizar { get; set; }
		private int _idProdutoInicial = 0;

		#endregion PROPRIEDADES

		#region METODOS

		private void CarregarConfiguracaoInicial()
		{
			tcAuditoria.TabPages.Remove(tabPage1);
			tcAuditoria.TabPages.Remove(tcAuditoria_tpDadosDoProduto);
			tcAuditoria.TabPages.Remove(tcEndereco_tpEnderecoEntrega);

			cmbStatusAuditoria.DropDown += CallplusUtil.Forms.CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

			Atualizar = false;
			lIdade.Text = "";
			lblLoginValidado.Text = "";
			cmbReceberContrato.ResetarComSelecione(habilitar: true);
			cmbPlugin.ResetarComSelecione(habilitar: true);
			cmbPossuiEmail.ResetarComSelecione(habilitar: true);

			rbFisica.Checked = true;
			rbFisica.Enabled = false;
			rbJuridica.Enabled = false;

			tcDadosPessoais.TabPages.Remove(tabPessoaJuridica);

			CarregarBanco();
			CarregarStatusDeAuditoria();
			CarregarHistoricoDeAuditoria();
			CarregarDadosDaAvaliacaoDeQualidade();

			if (_oferta != null)
			{
				CarregarResumoDaOferta();
				CarregarDadosDaOferta();
				CarregarLayoutDinamicoBko();
				CarregarStatusDeAuditoriaAtual(_oferta.IdStatusAuditoria);
				ConfigurarTelaDeAcordoComStatusAuditoria(_statusDeAuditoriaAtual);
				IniciarAuditoriaDeOferta(_statusDeAuditoriaAtual, _usuarioLogado.Id, _oferta.Id.Value);
			}
			CarregarRegrasDoPerfil();
		}

		private void AlterarLayout(Campanha campanhaAtual)
		{
			if (_campanhaAtual.Id == 13)
			{
				lblTelCelular.Text = "Núm. Gravação da Venda*";
				lblTelCelular.Width = 132;
				lblTelCelular.Height = 20;
				lblTelCelular.Location = new Point(382, 60);
				txtTelCelular.Location = new Point(383, 76);

				txtTelCelular.Width = 132;

				txtCpf.Location = new Point(385, 35);
				lblCpf.Location = new Point(385, 16);
				txtCpf.Width = 132;

				lblTelResidencial.Text = "Número Chamador";
				lblTelCelular.Width = 95;

				txtNome.Width = 368;
				txtNome.Height = 20;

				txtNomeDaMae.Width = 368;
			}
		}

		private void CarregarRegrasCampanhaExpress(bool ehExpress)
		{
			lklCopiarEndereco.Visible = ehExpress;
			//lbElegibilidade.Visible = ehExpress;
			//lblEntregaExpress.Visible = ehExpress;
			//cmbEntregaExpress.Visible = ehExpress;

			if (!ehExpress)
			{
				// tcEndereco.TabPages.Remove(tcEndereco_tpEnderecoEntrega);

				cmbEstadoCivil.Size = new Size(250, 21);

				lblProfissao.Location = new Point(264, 102);
				cmbProfissao.Size = new Size(250, 21);
				cmbProfissao.Location = new Point(266, 118);

				lblFaixaRenda.Location = new Point(520, 102);
				cmbFaixaRenda.Location = new Point(523, 118);
			}

		}

		private void IniciarEdicaoAvaliacaoDeAtendimento()
		{
			AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("DETALHES DA AVALIAÇÃO", _avaliacaoDeAtendimento.id, null, _avaliacaoDeAtendimento.Nome, (_avaliacaoDeAtendimento.idFeedback != null) ? true : false, (long)_oferta.Id);
			CarregarDadosDaAvaliacaoDeQualidade();
			f.Iniciar();
		}

		private void IniciarNovaAvaliacaoDeAtendimento()
		{
			int _tipos = (_oferta.Id > 0) ? 1 : 2;

			AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("NOVA AVALIAÇÃO", 0, _oferta.IdCampanha, _oferta.IdOperador.Value, _oferta.IdSupervisor.Value, _oferta.DataRegistroOferta.ToString("yyyy-MM-dd"), DateTime.Now.ToString(("yyyy-MM-dd") + " 23:59:59"), _oferta.IdStatusDaOferta.Value.ToString(), _tipos, _oferta.Id.Value);
			f.Iniciar();

			CarregarDadosDaAvaliacaoDeQualidade();
		}

		private void CarregarDadosDaAvaliacaoDeQualidade()
		{
			_avaliacaoDeAtendimento = _avaliacaoDeAtendimentoService.RetornarOfertaPortabilidade((long)_oferta.Id);

			if (_avaliacaoDeAtendimento != null)
			{
				btnAvaliacao.Text = "Ver Avaliação";

				txtAvaliador.Text = _avaliacaoDeAtendimento.Nome;
				txtPontuacao.Text = _avaliacaoDeAtendimento.pontuacao.ToString();
				txtDataDeAvaliacao.Text = _avaliacaoDeAtendimento.dataCriacao.ToString("dd/MM/yyyy hh:mm");
			}
			else
			{
				btnAvaliacao.Text = "Realizar Avaliação";
			}
		}

		private void IniciarEdicaoRegistro()
		{
			if (_avaliacaoDeAtendimento != null)
			{
				IniciarEdicaoAvaliacaoDeAtendimento();
			}
			else
			{
				IniciarNovaAvaliacaoDeAtendimento();
			}
		}

		private void CarregarRegrasDoPerfil()
		{
			if (AdministracaoMDI._usuario.IdPerfil != 1 && AdministracaoMDI._usuario.IdPerfil != 4 && AdministracaoMDI._usuario.IdPerfil != 6)
			{
				gbDadosPessoais.Enabled = false;
				gbEnderecoResidencial.Enabled = false;
				gbDadosOferta.Enabled = false;
				gbDadosPagamento.Enabled = false;

				cmbStatusAuditoria.Enabled = false;
				txtProtocolo.Enabled = false;
				txtAutorizacao.Enabled = false;
				txtOrdem.Enabled = false;
				txtLoginWM.Enabled = false;
				txtCodigo.Enabled = false;
				txtObservacao.Enabled = false;
				btnValidar.Enabled = false;
				btnSalvar.Enabled = false;
			}
		}

		private void IniciarAuditoriaDeOferta(Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria statusAtual, int idUsuario, long idOfertaBko)
		{
			if (statusAtual.HabilitaTrocaDeStatus)
			{
				_historicoOfertaBKO = _ofertaDoAtendimentoService.IniciarAuditoriaDaOfertaClaroPortabilidadeBKO(idUsuario, idOfertaBko);
			}
		}

		private void ConfigurarTelaDeAcordoComStatusAuditoria(Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria statusDeAuditoria)
		{
			if (statusDeAuditoria != null)
			{
				cmbStatusAuditoria.ResetarComSelecione(habilitar: statusDeAuditoria.HabilitaTrocaDeStatus);
				btnSalvar.Enabled = statusDeAuditoria.HabilitaTrocaDeStatus;
			}
		}

		private void CarregarStatusDeAuditoriaAtual(int? idStatusAuditoria)
		{
			if (idStatusAuditoria != null)
			{
				var status = _statusDeAuditoriaService
					.Listar(-1, ativo: true, idStatus: idStatusAuditoria.Value)
					.FirstOrDefault(x => x.Id == idStatusAuditoria);

				_statusDeAuditoriaAtual = status;
			}
		}

		private void CarregarLayoutDinamicoBko()
		{
			Campanha campanhaDaOferta = _campanhaService.RetornarCampanha(_oferta.IdCampanha);

			if (campanhaDaOferta != null && campanhaDaOferta.IdLayoutCampoDinamicoBko != null)
			{
				int idLayout = campanhaDaOferta.IdLayoutCampoDinamicoBko.Value;
				LayoutDeCampoDinamico layout = _layoutDinamicoService.RetornarLayoutDinamico(idLayout);
				containerDeLayoutDeCampoDinamico.CarregarLayout(layout);

				var valores = _layoutDinamicoService.ListarValoresDeCamposDinamicos(_oferta.IdProspect, _oferta.IdCampanha);
				containerDeLayoutDeCampoDinamico.PreencherCampos(valores);
			}
		}

		private void CarregarResumoDaOferta()
		{
			txtCodigoOferta.Text = _resumoDaOferta.idOferta.ToString();
			txtCampanha.Text = _resumoDaOferta.campanha;
			txtMailing.Text = _resumoDaOferta.mailing;
			txtOperador.Text = _resumoDaOferta.operador;
			txtSupervisor.Text = _resumoDaOferta.supervisor;
			txtStatusOferta.Text = _resumoDaOferta.statusOferta;
			txtDataTabulacao.Text = _resumoDaOferta.dataRegistroOferta?.ToString("dd/MM/yyyy HH:mm:ss");

			txtUltimoAuditor.Text = _resumoDaOferta.auditor;
			txtUltimoStatusAuditoria.Text = _resumoDaOferta.statusAuditoria;
			txtUltimoDataAuditoria.Text = _resumoDaOferta.dataAuditoria?.ToString("dd/MM/yyyy HH:mm:ss");
			txtUltimaObservacao.Text = _resumoDaOferta.Observacao;
		}

		private void CarregarDadosDaOferta()
		{
			txtCustId.Text = _oferta.Cust_Id;
			txtNome.Text = _oferta.Nome;

			txtDddTelResidencial.Text = _oferta.DddTel;
			if (_oferta.TelefoneResidencial != null)
				txtTelResidencial.Text = _oferta.TelefoneResidencial.ToString();

			txtDddCel.Text = _oferta.DDDCel;
			txtDddTelResidencial.Text = _oferta.DddTel;

			if (_oferta.TelefoneCelular != null)
				txtTelCelular.Text = _oferta.TelefoneCelular.ToString();

			if (_oferta.IdBanco != null)
				cmbBanco.SelectedValue = _oferta.IdBanco.ToString();

			if (!string.IsNullOrEmpty(_oferta.Email))
			{
				txtEmail.Text = _oferta.Email;
				cmbPossuiEmail.Text = "SIM";
				txtEmail.Enabled = true;
			}
			else
			{
				cmbPossuiEmail.Text = "NÃO";
				txtEmail.Enabled = false;
			}

			txtObservacaoOperador.Text = _oferta.Observacao ?? "";

			if (_oferta.Cep != null)
				txtCep.Text = _oferta.Cep.ToString().PadLeft(8, '0');

			txtLogradouro.Text = _oferta.Logradouro ?? "";
			txtNumero.Text = _oferta.Numero ?? "";
			txtComplemento.Text = _oferta.Complemento ?? "";
			txtBairro.Text = _oferta.Bairro ?? "";
			txtCidade.Text = _oferta.Cidade ?? "";
			txtUf.Text = _oferta.Uf ?? "";
			txtPontoReferencia.Text = _oferta.PontoDeReferencia ?? "";

			if (_oferta.TelefoneDaGravacao != null)
				txtTelefoneDaGravacao.Text = _oferta.TelefoneDaGravacao.ToString();

			if (_oferta.DataHoraAgendamento != null)
			{
				DateTime? dt = _oferta.DataHoraAgendamento;

				mkbDtAgendamento.Text = dt?.ToString("dd/MM/yyyy");
				mkbHoraAgendamento.Text = dt?.ToString("HH:mm");
			}
		}

		private void VerificarCepElegivel(string cep)
		{
			var mensagensValidacao = new List<string>();

			if (!string.IsNullOrEmpty(cep))
			{
				mensagensValidacao = _enderecoService.VerificarSeCepEhElegivel(cep, true);

				if (mensagensValidacao.Count > 0)
				{
					lblMensagem.Text = mensagensValidacao[0].ToString().ToUpper();
				}

				bool visivel = mensagensValidacao.Count() > 0 ? true : false;

				_cepPossuiElegibilidade = mensagensValidacao[0].Contains("NÃO") ? false : true;

				lblMensagem.ForeColor = !_cepPossuiElegibilidade ? Color.Red : Color.Green;
				lblMensagem.Visible = visivel;
			}

			RegraCepElegivel();
		}

		private void CarregarStatusDeAuditoria()
		{
			IEnumerable<Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria> retorno = _statusDeAuditoriaService.Listar(_campanhaAtual.Id, ativo: true);

			cmbStatusAuditoria.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
		}

		private void CarregarBanco()
		{
			IEnumerable<Banco> retorno = _campanhaService.ListarBancoDaCampanha(_oferta.IdCampanha, ativo: true);
			cmbBanco.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Codigo} - {x.Nome}");
		}

		private void CarregarDiaDeVencimentoDaFatura()
		{
			IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _ofertaDoAtendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(false);
			cmbDiaDeVencimentoDaFatura.PreencherComSelecione(configuracaoDatas, x => x.Fechamento, x => x.Vencimento);
		}

		private void CarregarFaixaDeRenda()
		{
			IEnumerable<FaixaDeRenda> retorno = _ofertaService.ListarFaixaDeRenda(ativo: true);
			cmbFaixaRenda.PreencherComSelecione(retorno, x => x.id, x => x.nome);
		}

		private void CarregarEstadoCivil()
		{
			IEnumerable<EstadoCivil> retorno = _ofertaService.ListarEstadoCivil(ativo: true);
			cmbEstadoCivil.PreencherComSelecione(retorno, x => x.id, x => x.nome);
		}

		private void CarregarFormaDePagamento()
		{
			IEnumerable<FormaDePagamento> retorno = _campanhaService.ListarFormasDePagamentoDaCampanha(_oferta.IdCampanha, ativo: true);
			cmbFormaPagamento.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
		}

		private void CarregarProfissao()
		{
			IEnumerable<Profissao> retorno = _ofertaService.ListarProfissao(ativo: true);
			cmbProfissao.PreencherComSelecione(retorno, x => x.id, x => x.nome);
		}

		private void CarregarOperadora()
		{
			IEnumerable<Operadora> retorno = _planoService.ListarOperadora();
			cmbOperadora.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
		}

		private void CarregarTipoDePlano()
		{
			IEnumerable<TipoDePlanoPorOperadora> retorno = _planoService.ListarTipoDePlanoPorOperadora();
			cmbTipoDePlano.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
		}

		private void CarregarProduto()
		{
			IEnumerable<Produto> produtos = _produtoService.Listar(-1, _oferta.IdCampanha, (int)_oferta.IdTipoDeProduto, true).Distinct();
			cmbProduto.PreencherComSelecione(produtos, x => x.Id, x => x.Nome);
			cmbProduto2.PreencherComSelecione(produtos, x => x.Id, x => x.Nome);
			cmbProduto3.PreencherComSelecione(produtos, x => x.Id, x => x.Nome);
			cmbProduto4.PreencherComSelecione(produtos, x => x.Id, x => x.Nome);
		}

		public void Iniciar()
		{
			this.StartPosition = FormStartPosition.CenterScreen;
			this.ShowDialog();
		}

		private void FinalizarAuditoriaDeOferta()
		{
			int idUsuario = _usuarioLogado.Id;
			long idOfertaBko = _oferta.Id.Value;
			int? idTipoOferta = _oferta.IdTipoDeProduto;

			if (_ofertaFoiAtualizada == false)
			{
				_ofertaDoAtendimentoService.RemoverHistoricoDeOfertaBkoPendente(idUsuario, idOfertaBko, idTipoOferta);
			}
		}

		private bool AtendeRegrasDeGravacao()
		{
			var mensagens = new List<string>();
			int idStatusAuditoria = 0;
			Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria statusDeAuditoria = new Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria();

			if (cmbStatusAuditoria.TextoEhSelecione())
			{
				mensagens.Add("[Status de Auditoria] deve ser informado!");
				CallplusFormsUtil.ExibirMensagens(mensagens);
				return false;
			}

			idStatusAuditoria = int.Parse(cmbStatusAuditoria.SelectedValue.ToString());
			statusDeAuditoria = _statusDeAuditoriaService.RetornarStatusDeAuditoria(idStatusAuditoria);

			if (statusDeAuditoria == null)
			{
				mensagens.Add("[Status de Auditoria] não pôde ser determinado!");
				CallplusFormsUtil.ExibirMensagens(mensagens);
				return false;
			}

			if (statusDeAuditoria.PermitidoHumano == false)
			{
				mensagens.Add("[Status de Auditoria] não permitido!");
				CallplusFormsUtil.ExibirMensagens(mensagens);
				return false;
			}

			if (_statusDeAuditoriaAtual != null && _statusDeAuditoriaAtual.HabilitaTrocaDeStatus == false)
			{
				mensagens.Add("[Status de Auditoria] atual da oferta não permite alteração de Status!");
				CallplusFormsUtil.ExibirMensagens(mensagens);
				return false;
			}

			//if (statusDeAuditoria.AprovaOferta)
			//{
			//    if (string.IsNullOrEmpty(txtProtocolo.Text))
			//    {
			//        mensagens.Add("[Pedido] deve ser informado!");
			//    }

			//    if (string.IsNullOrEmpty(txtIdInteracao.Text))
			//    {
			//        mensagens.Add("[Id Interação] deve ser informada!");
			//    }

			//    if (string.IsNullOrEmpty(txtLoginWM.Text))
			//    {
			//        mensagens.Add("[Login Siebel] deve ser informado!");
			//    }
			//}

			CallplusFormsUtil.ExibirMensagens(mensagens);
			return mensagens.Any() == false;
		}

		private bool AtendeRegrasDeGravacaoDaOferta()
		{
			bool permiteVendaParaNaoTitular = _oferta.IdCampanha == 8;

			foreach (var item in gbDadosPessoais.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
			{
				item.ForeColor = SystemColors.WindowText;
			}

			foreach (var item in gbEnderecoResidencial.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
			{
				item.ForeColor = SystemColors.WindowText;
			}

			foreach (var item in gbDadosOferta.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
			{
				item.ForeColor = SystemColors.WindowText;
			}

			foreach (var item in gbDadosPagamento.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
			{
				item.ForeColor = SystemColors.WindowText;
			}

			var mensagens = new List<string>();

			//Dados do Cliente
			if (string.IsNullOrEmpty(txtCustId.Text))
			{
				lblCustId.ForeColor = Color.Red;
				mensagens.Add("[Cust_Id] deve ser informado!");
			}

			if (string.IsNullOrEmpty(txtNome.Text))
			{
				lblNome.ForeColor = Color.Red;
				mensagens.Add("[Nome] deve ser informado!");
			}

			string[] nome = txtNome.Text.Trim().Split(' ');
			if (nome.Length <= 1)
			{
				lblNome.ForeColor = Color.Red;
				mensagens.Add("[Nome] inválido!");
			}

			if (string.IsNullOrEmpty(txtTelCelular.Text))
			{
				lblTelCelular.ForeColor = Color.Red;
				mensagens.Add("[Telefone Celular] deve ser informado!");
			}

			if (!string.IsNullOrEmpty(txtTelCelular.Text) && !Texto.TelefoneCelularPossuiFormatoValido(txtTelCelular.Text))
			{
				lblTelCelular.ForeColor = Color.Red;
				mensagens.Add("[Telefone Celular] inválido!");
			}

			if (cmbPossuiEmail.Text.Equals("SIM"))
			{
				if (string.IsNullOrEmpty(txtEmail.Text))
				{
					mensagens.Add("[E-mail] inválido!");
				}

				if (!string.IsNullOrEmpty(txtEmail.Text) && !Texto.EmailPosuiFormatoValido(txtEmail.Text))
				{
					mensagens.Add("[E-mail] inválido!");
				}
			}

			CallplusFormsUtil.ExibirMensagens(mensagens);

			return mensagens.Any() == false;
		}

		private void RegraCepElegivel()
		{
			if (_campanhaEhExpress)
			{
				if (!_cepPossuiElegibilidade && !cmbEntregaExpress.TextoEhSelecione() && cmbEntregaExpress.Text == "SIM")
				{
					MessageBox.Show("CEP não é elegível! Entrega Express não disponível.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
					cmbEntregaExpress.Text = "NÃO";
				}

				if (_cepPossuiElegibilidade && !cmbEntregaExpress.TextoEhSelecione() && cmbEntregaExpress.Text == "NÃO")
				{
					var resposta = MessageBox.Show("CEP é elegível, mas Entrega Express está marcado como NÃO.\n" +
						"Deseja escolher Entrega Express?", "Alerta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if (resposta == DialogResult.Yes)
						cmbEntregaExpress.Text = "SIM";
				}
			}
		}

		private void Gravar()
		{
			if (GravarDadosDaOferta())
			{
				GravarHistoricoDaOferta();
			}
		}

		private void GravarHistoricoDaOferta()
		{
			if (AtendeRegrasDeGravacao())
			{
				if (_historicoOfertaBKO == null)
				{
					_historicoOfertaBKO = new HistoricoDaOfertaDoAtendimentoPortabilidadeBKO();
				}

				_historicoOfertaBKO.idOfertaDoAtendimentoPortabilidadeBKO = (long)_oferta.Id;
				_historicoOfertaBKO.idStatusAuditoria = Convert.ToInt32(cmbStatusAuditoria.SelectedValue);
				_historicoOfertaBKO.protocolo = txtProtocolo.Text;
				_historicoOfertaBKO.autorizacao = txtIdInteracao.Text;
				//_historicoOfertaBKO.ordem = txtOrdem.Text;

				if (!string.IsNullOrEmpty(txtNumeroProvisorio.Text))
				{
					_historicoOfertaBKO.numeroProvisorio = Convert.ToInt64(txtNumeroProvisorio.Text);
				}

				_historicoOfertaBKO.loginWM = txtLoginWM.Text;
				//_historicoOfertaBKO.codigoAgente = txtCodigo.Text;
				_historicoOfertaBKO.idCriador = AdministracaoMDI._usuario.Id;
				_historicoOfertaBKO.Observacao = txtObservacao.Text;

				_historicoOfertaBKO.id = _ofertaDoAtendimentoService.GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(_historicoOfertaBKO);

				cmbStatusAuditoria.ResetarComSelecione(true);
				txtObservacao.Text = "";

				MessageBox.Show("Auditoria gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

				_ofertaFoiAtualizada = true;
				Atualizar = true;
			}
		}

		private bool GravarDadosDaOferta()
		{
			bool result = true;

			if (AtendeRegrasDeGravacaoDaOferta())
			{
				if (rbFisica.Checked)
					_oferta.TipoPessoa = "FÍSICA";

				if (rbJuridica.Checked)
					_oferta.TipoPessoa = "JURÍDICA";

				if (!string.IsNullOrEmpty(txtNome.Text))
					_oferta.Nome = txtNome.Text;

				if (!string.IsNullOrEmpty(txtDddTelResidencial.Text))
					_oferta.DddTel = txtDddTelResidencial.Text;

				if (!string.IsNullOrEmpty(txtTelResidencial.Text))
					_oferta.TelefoneResidencial = Convert.ToInt64(txtTelResidencial.Text);

				if (!string.IsNullOrEmpty(txtDddCel.Text))
					_oferta.DDDCel = txtDddCel.Text;

				if (!string.IsNullOrEmpty(txtTelCelular.Text))
					_oferta.TelefoneCelular = Convert.ToInt64(txtTelCelular.Text);

				if (!cmbBanco.TextoEhSelecione())
					_oferta.IdBanco = Convert.ToInt32(cmbBanco.SelectedValue);

				_oferta.Email = !string.IsNullOrEmpty(txtEmail.Text) && cmbPossuiEmail.Text == "SIM" ?  txtEmail.Text : string.Empty;

				if (!string.IsNullOrEmpty(txtCep.Text))
					_oferta.Cep = Convert.ToInt64(txtCep.Text);

				if (!string.IsNullOrEmpty(txtLogradouro.Text))
					_oferta.Logradouro = txtLogradouro.Text;

				if (!string.IsNullOrEmpty(txtNumero.Text))
					_oferta.Numero = txtNumero.Text;

				if (!string.IsNullOrEmpty(txtComplemento.Text))
					_oferta.Complemento = txtComplemento.Text;

				if (!string.IsNullOrEmpty(txtBairro.Text))
					_oferta.Bairro = txtBairro.Text;

				if (!string.IsNullOrEmpty(txtCidade.Text))
					_oferta.Cidade = txtCidade.Text;

				if (!string.IsNullOrEmpty(txtUf.Text))
					_oferta.Uf = txtUf.Text;

				if (!string.IsNullOrEmpty(txtPontoReferencia.Text))
					_oferta.PontoDeReferencia = txtPontoReferencia.Text;

				if (!string.IsNullOrEmpty(txtObservacao.Text))
					_oferta.Observacao = txtObservacao.Text;

				if (!string.IsNullOrEmpty(txtTelefoneDaGravacao.Text))
					_oferta.TelefoneDaGravacao = Convert.ToInt64(txtTelefoneDaGravacao.Text);

				if (DateTime.TryParse(mkbDtAgendamento.Text + " " + mkbHoraAgendamento.Text, out DateTime dtHoraAgenda))
					_oferta.DataHoraAgendamento = dtHoraAgenda;

				_oferta.Id = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoMPPortabilidadeBKO(_oferta);
			}
			else
			{
				result = false;
			}

			return result;
		}

		private void ConsultarLoginWM()
		{
			string codigoAgente = _ofertaDoAtendimentoService.ValidarLoginWM(txtLoginWM.Text);

			if (!string.IsNullOrEmpty(codigoAgente))
			{
				txtCodigo.Text = codigoAgente;
				lblLoginValidado.Text = txtLoginWM.Text;

				txtLoginWM.Text = "";
			}
			else
			{
				MessageBox.Show("[Login WM] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void CarregarHistoricoDeAuditoria()
		{
			try
			{
				long idOfertaBko = _oferta.Id.Value;
				var hist = _ofertaDoAtendimentoService.ListarHistoricoDaOfertaDoAtendimentoBKO(idOfertaBko, (int)_oferta.IdTipoDeProduto);
				dgHistorico.DataSource = hist;

				txtAuditor_historico.Text = "";
				txtStatusAuditoria_historico.Text = "";
				txtDataAuditoria_historico.Text = "";
				txtLoginWM_historico.Text = "";
				txtCodigoAgente_historico.Text = "";
				txtProtocolo_historico.Text = "";
				txtAutorizacao_historico.Text = "";
				txtNumeroProvisorio_historico.Text = "";
				txtOrdem_historico.Text = "";
			}
			catch (Exception e)
			{
				_logger.Error(e);
				MessageBox.Show($"Ocorreu um erro ao carregar o histórico da auditoria!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LiberarAlteracaoDeProduto()
		{
			if (string.IsNullOrEmpty(txtLoginProduto.Text))
			{
				MessageBox.Show("Para liberar Alteração de Produto, informe um Login permitido!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else if (string.IsNullOrEmpty(txtSenhaProduto.Text))
			{
				MessageBox.Show("Para liberar Alteração de Produto, informe uma Senha de login permitido!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else if (!string.IsNullOrEmpty(txtLoginProduto.Text) && !string.IsNullOrEmpty(txtSenhaProduto.Text))
			{
				Usuario usuario = _ofertaDoAtendimentoService.ValidarUsuarioPermitidoParaAlterarProduto(txtLoginProduto.Text, txtSenhaProduto.Text).FirstOrDefault();

				if (usuario != null)
				{
					txtLoginProduto.Text = string.Empty;
					txtSenhaProduto.Text = string.Empty;
					cmbProduto.Enabled = true;
					cmbProduto2.Enabled = true;
					cmbProduto3.Enabled = true;
					cmbProduto4.Enabled = true;
				}
				else
				{
					MessageBox.Show("Você não tem permissão para executar essa ação!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void SelecionarHistorico(int linhaSelecionada)
		{
			long idHistoricoBko = (long)dgHistorico.Rows[linhaSelecionada].Cells["id"].Value;
			var historico = _ofertaDoAtendimentoService.RetornarHistoricoDaOfertaDoAtendimentoBKO_DTO(idHistoricoBko, (int)_oferta.IdTipoDeProduto);

			if (historico != null)
			{
				txtAuditor_historico.Text = historico.Auditor;
				txtStatusAuditoria_historico.Text = historico.StatusDeAuditoria;
				txtDataAuditoria_historico.Text = historico.DataInput?.ToString("dd/MM/yyyy HH:mm:ss");
				txtLoginWM_historico.Text = historico.LoginWM;
				txtCodigoAgente_historico.Text = historico.CodigoAgente;
				txtProtocolo_historico.Text = historico.Protocolo;
				txtAutorizacao_historico.Text = historico.Autorizacao;
				txtNumeroProvisorio_historico.Text = historico.numeroProvisorio.ToString();
				txtOrdem_historico.Text = historico.ordem;
				txtObservacoes_historico.Text = historico.Observacao;
			}
		}

		private void ConfiguracoessDeFormaDePagamento()
		{
			if (FormaDePagamentoEhDebito())
			{
				cmbBanco.ResetarComSelecione(habilitar: true);
				txtAgencia.Resetar(habilitar: true, limparTexto: true, readOnly: false);
				txtConta.Resetar(habilitar: true, limparTexto: true, readOnly: false);

				txtAgencia.BackColor = SystemColors.InactiveBorder;
				txtConta.BackColor = SystemColors.InactiveBorder;
			}
			else
			{
				cmbBanco.ResetarComSelecione(habilitar: false);
				txtAgencia.Resetar(habilitar: false, limparTexto: false, readOnly: true);
				txtConta.Resetar(habilitar: false, limparTexto: false, readOnly: true);
			}

			if (cmbFormaPagamento.Text.Contains("WHATSAPP"))
				txtNumeroFaturaWhatsApp.Resetar(habilitar: true, limparTexto: true, readOnly: false);
			else
				txtNumeroFaturaWhatsApp.Resetar(habilitar: false, limparTexto: true, readOnly: false);

			txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: false);
		}

		private void SelecionarEndereco()
		{
			lblCep.ForeColor = SystemColors.WindowText;

			string telefone = string.Empty;
			if (Texto.TelefonePossuiFormatoValido(txtTelCelular.Text))
			{
				telefone = txtTelCelular.Text;
			}
			else
				telefone = txtTelCelular.Text;

			Tabulador.App.Operacao.EnderecoForm f = new Tabulador.App.Operacao.EnderecoForm(_usuarioLogado, _prospect, _campanhaAtual, telefone);

			f.StartPosition = FormStartPosition.CenterScreen;
			f.ShowDialog();

			PreencherCampoDoEndereco(f.EnderecoSelecionado);
		}

		private void PreencherCampoDoEndereco(EnderecoDoProspect endereco)
		{
			foreach (Control item in gbEnderecoResidencial.Controls.OfType<TextBox>())
			{
				item.ResetText();
			}

			bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
			bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;

			if (endereco != null)
			{
				if (residencial)
				{
					txtCep.Text = endereco.Cep;
					txtLogradouro.Text = endereco.Logradouro;
					txtNumero.Text = endereco.Numero;
					txtComplemento.Text = endereco.Complemento;
					txtBairro.Text = endereco.Bairro;
					txtCidade.Text = endereco.Cidade;
					txtUf.Text = endereco.Uf;
					txtPontoReferencia.Text = endereco.PontoDeReferencia;
				}

				if (entrega)
				{
					txtCepEntrega.Text = endereco.Cep;
					txtLogradouroEntrega.Text = endereco.Logradouro;
					txtNumeroEntrega.Text = endereco.Numero;
					txtComplementoEntrega.Text = endereco.Complemento;
					txtBairroEntrega.Text = endereco.Bairro;
					txtCidadeEntrega.Text = endereco.Cidade;
					txtUfEntrega.Text = endereco.Uf;
					txtPontoReferenciaEntrega.Text = endereco.PontoDeReferencia;

					//VerificarCepElegivel(endereco.Cep, true);
				}
			}
		}

		private void ExibirValidacaoTravaUfPorDDD(bool? exibir = null)
		{
			var mensagens = new List<string>();
			ValidarTravaUF(mensagens);

			if (Convert.ToBoolean(exibir))
				CallplusFormsUtil.ExibirMensagens(mensagens);
		}

		private void ValidarTravaUF(List<string> mensagens)
		{
			string uf = RetornarUfDeAcordoComDdd();
			string ufDoCLiente = txtUf.Text;

			bool mesmoUF = string.IsNullOrEmpty(ufDoCLiente) ? false : uf.Contains(ufDoCLiente);

			if (!mesmoUF) //if (uf != ufDoCLien
			{
				lblUf.ForeColor = Color.Red;

				string ddd = string.Empty;
				if (!string.IsNullOrEmpty(txtTelCelular.Text))
					ddd = (txtTelCelular.Text).Substring(0, 2);

				if (!string.IsNullOrEmpty(ufDoCLiente))
				{
					mensagens.Add("[DDD] divergente do [UF] do Cliente!");

					lblMensagem.Visible = true;
					lblMensagem.Text = "O UF do DDD Cliente (" + uf + "-" + ddd + ") é divergente do UF endereço (" + ufDoCLiente + ").";
					lblMensagem.ForeColor = Color.Red;
				}
				else
				{
					ResetarCampos();
				}
			}
			else
			{
				ResetarCampos();
			}
		}

		private void ResetarCampos()
		{
			lblUf.ForeColor = SystemColors.WindowText;
			lblMensagem.Text = string.Empty;
			lblMensagem.Visible = false;
		}

		private string RetornarUfDeAcordoComDdd()
		{
			string telefone = txtTelCelular.Text;
			string DDD = string.Empty;
			if (!string.IsNullOrEmpty(telefone))
				DDD = (telefone).Substring(0, 2);

			int ddd = 0;
			int.TryParse(DDD, out ddd);
			string uf = _enderecoService.RetornarUf(ddd, _campanhaAtual.Id);

			return uf;
		}

		private bool FormaDePagamentoEhDebito()
		{
			if (cmbFormaPagamento.Text == "DÉBITO EM CONTA" || cmbFormaPagamento.Text.Contains("DCC") || cmbFormaPagamento.Text.Contains("DEBITO EM CONTA"))
				return true;
			else
				return false;
		}

		private string VerificaRGMaiorQueZero(string numRG)
		{
			int soma = 0;
			for (int i = 0; i < numRG.Length; i++)
			{
				if ((numRG).Substring(i, 1) == "1" ||
				   (numRG).Substring(i, 1) == "2" ||
				   (numRG).Substring(i, 1) == "3" ||
				   (numRG).Substring(i, 1) == "4" ||
				   (numRG).Substring(i, 1) == "5" ||
				   (numRG).Substring(i, 1) == "6" ||
				   (numRG).Substring(i, 1) == "7" ||
				   (numRG).Substring(i, 1) == "8" ||
				   (numRG).Substring(i, 1) == "9" ||
				   (numRG).Substring(i, 1) == "0")
				{
					soma += Convert.ToInt32((txtRg.Text).Substring(i, 1));
				}
			}
			numRG = soma.ToString();
			return numRG;
		}

		private void VerificarCepElegivel(string cep, bool ehCepEntrega)
		{
			var mensagensValidacao = new List<string>();

			if (!string.IsNullOrEmpty(cep))
			{
				mensagensValidacao = _enderecoService.VerificarSeCepEhElegivel(cep, ehCepEntrega);

				if (mensagensValidacao.Count > 0)
				{
					lblCepBluechip.Text = mensagensValidacao[0].ToString().ToUpper();
				}

				bool visivel = mensagensValidacao.Count() > 0 ? true : false;

				if (ehCepEntrega)
				{
					_cepPossuiElegibilidade = mensagensValidacao[0].Contains("NÃO") ? false : true;
					lblCepBluechip.ForeColor = !_cepPossuiElegibilidade ? Color.Red : Color.Green;
				}
				else
					lblCepBluechip.ForeColor = !visivel ? Color.Red : Color.Blue;

				lblCepBluechip.Visible = visivel;
			}
		}

		#endregion METODOS

		#region EVENTOS

		private void AuditoriaOferta_PortabilidadeClaroForm_Load(object sender, EventArgs e)
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
					$"Não foi possível gravar a Auditoria!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void AuditoriaOferta_PortabilidadeClaroForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				FinalizarAuditoriaDeOferta();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				MessageBox.Show($"Ocorreu um erro ao finalizar a auditoria!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dgHistorico_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex < 0) return;

				SelecionarHistorico(e.RowIndex);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
				MessageBox.Show($"Ocorreu um erro ao selecioanar os dados do Histórico.", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void AuditoriaOferta_PortabilidadeClaroForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!txtSenhaProduto.Focused)
			{
				if (Char.IsLower(e.KeyChar))
					e.KeyChar = Char.ToUpper(e.KeyChar);

				if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
				{
					e.Handled = true;
				}
			}
		}

		private void cmbFaturaDigital_SelectedIndexChanged(object sender, EventArgs e)
		{
			//lblFaturaDigital.ForeColor = SystemColors.WindowText;
			//lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;

			//if (cmbFaturaDigital.Text == "SIM")
			//{
			//    txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: false);
			//    txtEmailFaturaDigital.BackColor = SystemColors.InactiveBorder;
			//}
			//else
			//{
			//    txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: true);
			//    txtEmailFaturaDigital.BackColor = Color.WhiteSmoke;
			//}
		}

		private void cmbDiaDeVencimentoDaFatura_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblDiaVencimento.ForeColor = SystemColors.WindowText;

			txtPrimeiraFatura.Resetar(habilitar: true, limparTexto: true, readOnly: true);
			txtCicloFechamento.Resetar(habilitar: true, limparTexto: true, readOnly: true);

			if (cmbDiaDeVencimentoDaFatura.TextoEhSelecione() || string.IsNullOrEmpty(cmbDiaDeVencimentoDaFatura.Text)) return;

			txtCicloFechamento.Text = cmbDiaDeVencimentoDaFatura.SelectedValue.ToString();

			DateTime mesFatura = DateTime.Today;

			try
			{
				int diaVencimento = int.Parse(cmbDiaDeVencimentoDaFatura.Text.Replace(" - HOJE", ""));
				int ciclo = int.Parse(cmbDiaDeVencimentoDaFatura.SelectedValue.ToString());

				if (ciclo < mesFatura.Day && diaVencimento <= mesFatura.Day && ciclo >= diaVencimento)
				{
					mesFatura = mesFatura.AddMonths(2);
				}
				else if (ciclo >= diaVencimento)
				{
					mesFatura = mesFatura.AddMonths(1);
				}
				else
				{
					if (mesFatura.Day >= ciclo)
						mesFatura = mesFatura.AddMonths(1);
				}

				txtPrimeiraFatura.Text = mesFatura.Month.ToString();
			}
			catch (Exception ex)
			{

			}
		}

		private void cmbFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblFormaPagamento.ForeColor = SystemColors.WindowText;
			lblBanco.ForeColor = SystemColors.WindowText;
			lblAgencia.ForeColor = SystemColors.WindowText;
			lblConta.ForeColor = SystemColors.WindowText;
			lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;

			//ConfiguracoessDeFormaDePagamento();

			//if (cmbFormaPagamento.Text == "DÉBITO EM CONTA" || cmbFormaPagamento.Text.Contains("DCC"))
			//{
			//    cmbBanco.ResetarComSelecione(habilitar: true);
			//    txtAgencia.Resetar(habilitar: true, limparTexto: true, readOnly: false);
			//    txtConta.Resetar(habilitar: true, limparTexto: true, readOnly: false);

			//    txtAgencia.BackColor = SystemColors.InactiveBorder;
			//    txtConta.BackColor = SystemColors.InactiveBorder;
			//}
			//else
			//{
			//    cmbBanco.ResetarComSelecione(habilitar: false);
			//    txtAgencia.Resetar(habilitar: false, limparTexto: false, readOnly: true);
			//    txtConta.Resetar(habilitar: false, limparTexto: false, readOnly: true);

			//    txtAgencia.BackColor = Color.WhiteSmoke;
			//    txtConta.BackColor = Color.WhiteSmoke;
			//    //txtAgencia.BackColor = Color.LightGray;
			//    //txtConta.BackColor = Color.LightGray;
			//}
			//if (cmbFormaPagamento.Text.Contains("WHATSAPP"))
			//{
			//    txtNumeroFaturaWhatsApp.Resetar(habilitar: true, limparTexto: true, readOnly: false);
			//}
			//else
			//{
			//    txtNumeroFaturaWhatsApp.Resetar(habilitar: false, limparTexto: true, readOnly: false);
			//}
		}

		private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblProduto.ForeColor = SystemColors.WindowText;
		}

		private void txtEmailFaturaDigital_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;
		}

		private void txtEmailFaturaDigital_Leave(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEmailFaturaDigital.Text))
			{
				if (!Texto.EmailPosuiFormatoValido(txtEmailFaturaDigital.Text))
				{
					lblEmailFaturaDigital.ForeColor = Color.Red;
					txtEmailFaturaDigital.Focus();
					MessageBox.Show("[E-mail para Fatura] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;
			}
		}

		private void txtAgencia_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
			{
				e.Handled = true;
				MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//lblConta.ForeColor = SystemColors.WindowText;
				//e.Handled = char.IsWhiteSpace(e.KeyChar);
			}

		}

		private void txtConta_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
			{
				e.Handled = true;
				MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//lblConta.ForeColor = SystemColors.WindowText;
				//e.Handled = char.IsWhiteSpace(e.KeyChar);
			}
		}

		private void txtNome_Leave(object sender, EventArgs e)
		{
			txtNome.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(txtNome.Text).ToUpper();

			if (!string.IsNullOrEmpty(txtNome.Text))
			{
				string[] nome = txtNome.Text.Trim().Split(' ');
				if (nome.Length <= 1)
				{
					lblNome.ForeColor = Color.Red;
					txtNome.Focus();
					MessageBox.Show("[Nome] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				lblNome.ForeColor = SystemColors.WindowText;
			}
		}

		private void cmbTipoDePlano_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtValorConta1.Enabled = false;
			txtValorConta2.Enabled = false;
			txtValorConta3.Enabled = false;

			txtValorConta1.Text = "";
			txtValorConta2.Text = "";
			txtValorConta3.Text = "";

			lblTipoDePlano.ForeColor = SystemColors.WindowText;
			lblValorConta1.ForeColor = SystemColors.WindowText;
			lblValorConta2.ForeColor = SystemColors.WindowText;
			lblValorConta3.ForeColor = SystemColors.WindowText;

			if (cmbTipoDePlano.Text == "POS")
			{
				txtValorConta1.Enabled = true;
				txtValorConta2.Enabled = true;
				txtValorConta3.Enabled = true;
			}
		}

		private void btnAvaliacao_Click(object sender, EventArgs e)
		{
			try
			{
				IniciarEdicaoRegistro();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Não foi possivel carregar a avaliação do atendimento: " + ex.Message);
			}
		}

		private void txtNumeroPortado_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblNumeroPortado.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtNumProvisorio_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblNumProvisorio.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtNumeroPortado_Leave(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtNumeroPortado.Text))
			{
				if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroPortado.Text))
				{
					lblNumeroPortado.ForeColor = Color.Red;
					txtNumeroPortado.Focus();
					MessageBox.Show("[Número Portado] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				lblNumeroPortado.ForeColor = SystemColors.WindowText;
			}
		}

		private void txtNumeroWhatsApp_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblNumeroFaturaWhatsApp.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtNumeroWhatsApp_Leave(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtNumeroFaturaWhatsApp.Text))
			{
				if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroFaturaWhatsApp.Text))
				{
					lblNumeroFaturaWhatsApp.ForeColor = Color.Red;
					txtNumeroFaturaWhatsApp.Focus();
					MessageBox.Show("[Número Fatura WhatsApp] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				lblNumeroPortado.ForeColor = SystemColors.WindowText;
			}
		}

		private void btnSelecionarEndereco_Click(object sender, EventArgs e)
		{
			SelecionarEndereco();
		}

		private void txtCepEntrega_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblCepEntrega.ForeColor = SystemColors.WindowText;
		}

		private void txtNumeroEntrega_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblNumeroEntrega.ForeColor = SystemColors.WindowText;
		}

		private void cmbReceberContrato_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbReceberContrato.Text == "SIM")
			{
				cmbOndeReceberContrato.ResetarComSelecione(habilitar: true);
			}
			else
			{
				cmbOndeReceberContrato.ResetarComSelecione(habilitar: false);
			}
		}

		private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
			{
				e.Handled = true;
				MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void rbFisica_CheckedChanged(object sender, EventArgs e)
		{
			tcDadosPessoais.TabPages.Clear();


			if (rbFisica.Checked)
			{
				tcDadosPessoais.TabPages.Add(tabPessoaFisica);
			}
			else
			{
				tcDadosPessoais.TabPages.Add(tabPessoaJuridica);
			}
		}

		private void cmbEhProvisorio_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (cmbEhProvisorio.TextoEhSelecione() || cmbEhProvisorio.Text == "NÃO")
			{
				txtNumProvisorio.Enabled = false;
				txtNumProvisorio.ReadOnly = true;
				txtNumProvisorio.Clear();
			}
			else
			{
				txtNumProvisorio.Enabled = true;
				txtNumProvisorio.ReadOnly = false;
			}
		}

		private void txtIccCid_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblIcCid.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtPedidoDeEntrega_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblPedidoDeEntrega.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void lblAtualizarHistorico_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				CarregarHistoricoDeAuditoria();
			}
			catch (Exception ex)
			{
				_logger.Error(ex);

				MessageBox.Show(
					$"Não foi possível atualizar o Histórico!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void lklCopiarEndereco_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
		{
			txtCepEntrega.Text = txtCep.Text;
			txtBairroEntrega.Text = txtBairro.Text;
			txtLogradouroEntrega.Text = txtLogradouro.Text;
			txtUfEntrega.Text = txtUf.Text;
			txtNumeroEntrega.Text = txtNumero.Text;
			txtComplementoEntrega.Text = txtComplemento.Text;
			txtCidadeEntrega.Text = txtCidade.Text;
			txtPontoReferenciaEntrega.Text = txtPontoReferencia.Text;

			tcEndereco.SelectedTab = tcEndereco_tpEnderecoEntrega;

			//VerificarCepElegivel(txtCepEntrega.Text, true);
		}

		private void cmbPossuiEmail_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (cmbPossuiEmail.Text == "NÃO" || cmbPossuiEmail.TextoEhSelecione())
			{
				txtEmail.Enabled = false;
				txtEmail.Clear();
			}
			else
				txtEmail.Enabled = true;
		}

		private void txtDddTelResidencial_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblTelCelular.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtTelResidencial_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblTelCelular.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtDddCel_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblTelCelular.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtTelCelular_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblTelCelular.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblNome.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereSomenteLetra(e.KeyChar);
		}
		#endregion EVENTOS
	}
}
