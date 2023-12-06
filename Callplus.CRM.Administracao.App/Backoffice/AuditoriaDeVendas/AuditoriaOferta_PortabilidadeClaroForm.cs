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
    public partial class AuditoriaOferta_PortabilidadeClaroForm : Form
    {
        public AuditoriaOferta_PortabilidadeClaroForm(long idOferta)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _atendimentoService = new AtendimentoService();
            _campanhaService = new CampanhaService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _produtoService = new ProdutoService();
            _ofertaService = new ProspectService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _planoService = new PlanoPorOperadoraParaComparacaoService();
            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();

            _usuarioLogado = AdministracaoMDI._usuario;

            _oferta = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoClaroPortabilidadeBKO(idOferta);
            _resumoDaOferta = _ofertaDoAtendimentoService.RetornarResumoDaOfertaDoAtendimentoBKO(idOferta, (int)_oferta.idTipoDeProduto);

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly AtendimentoService _atendimentoService;
        private readonly CampanhaService _campanhaService;
        private readonly ProdutoService _produtoService;
        private readonly ProspectService _ofertaService;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private readonly PlanoPorOperadoraParaComparacaoService _planoService;
        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
        private Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria _statusDeAuditoriaAtual;
        private readonly Usuario _usuarioLogado;
        private bool _ofertaFoiAtualizada;
        private bool _editaProtocoloNET;

        private OfertaDoAtendimentoClaroPortabilidadeBKO _oferta;
        private ResumoDaOfertaDoAtendimentoBkoDTO _resumoDaOferta;
        private HistoricoDaOfertaDoAtendimentoPortabilidadeBKO _historicoOfertaBKO;
        private AvaliacaoDeAtendimento _avaliacaoDeAtendimento;

        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            cmbStatusAuditoria.DropDown += CallplusUtil.Forms.CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            Atualizar = false;
            lIdade.Text = "";
            lblLoginValidado.Text = "";
            cmbFaturaDigital.ResetarComSelecione(habilitar: true);

            _editaProtocoloNET = false;

            if (_oferta.idCampanha == 10)
                _editaProtocoloNET = true;

            CarregarProduto();
            CarregarBanco();
            CarregarDiaDeVencimentoDaFatura();
            CarregarFaixaDeRenda();
            CarregarEstadoCivil();
            CarregarProfissao();
            CarregarOperadora();
            CarregarTipoDePlano();
            CarregarFormaDePagamento();
            CarregarStatusDeAuditoria();
            CarregarHistoricoDeAuditoria();
            CarregarDadosDaAvaliacaoDeQualidade();

            if (_oferta != null)
            {
                CarregarResumoDaOferta();
                CarregarDadosDaOferta();
                CarregarLayoutDinamicoBko();
                CarregarStatusDeAuditoriaAtual(_oferta.idStatusAuditoria);
                ConfigurarTelaDeAcordoComStatusAuditoria(_statusDeAuditoriaAtual);
                IniciarAuditoriaDeOferta(_statusDeAuditoriaAtual, _usuarioLogado.Id, _oferta.id.Value);
            }

            CarregarRegrasDoPerfil();
        }
        
        private void IniciarEdicaoAvaliacaoDeAtendimento()
        {
            AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("DETALHES DA AVALIAÇÃO", _avaliacaoDeAtendimento.id, null, _avaliacaoDeAtendimento.Nome, (_avaliacaoDeAtendimento.idFeedback != null) ? true : false, (long)_oferta.id);

            f.Iniciar();

            CarregarDadosDaAvaliacaoDeQualidade();
        }

        private void IniciarNovaAvaliacaoDeAtendimento()
        {
            AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("NOVA AVALIAÇÃO", 0, null, "", false, (long)_oferta.id, _oferta.idCampanha);

            f.Iniciar();

            CarregarDadosDaAvaliacaoDeQualidade();
        }

        private void CarregarDadosDaAvaliacaoDeQualidade()
        {
            _avaliacaoDeAtendimento = _avaliacaoDeAtendimentoService.RetornarOfertaPortabilidade((long)_oferta.id);

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
                    .Listar(ativo: true, idStatus: idStatusAuditoria.Value)
                    .FirstOrDefault(x => x.Id == idStatusAuditoria);

                _statusDeAuditoriaAtual = status;
            }
        }

        private void CarregarLayoutDinamicoBko()
        {
            Campanha campanhaDaOferta = _campanhaService.RetornarCampanha(_oferta.idCampanha);

            if (campanhaDaOferta != null && campanhaDaOferta.IdLayoutCampoDinamicoBko != null)
            {
                int idLayout = campanhaDaOferta.IdLayoutCampoDinamicoBko.Value;
                LayoutDeCampoDinamico layout = _layoutDinamicoService.RetornarLayoutDinamico(idLayout);
                containerDeLayoutDeCampoDinamico.CarregarLayout(layout);

                var valores = _layoutDinamicoService.ListarValoresDeCamposDinamicos(_oferta.idProspect, _oferta.idCampanha);
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
            txtNome.Text = _oferta.nome;

            txtCpf.Text = CallplusFormsUtil.FormatarCPF(_oferta.cpf?.ToString());

            txtRg.Text = _oferta.rg;

            if (_oferta.nascimento != null)
                txtDataNascimento.Text = _oferta.nascimento.ToString();

            txtNomeDaMae.Text = _oferta.nomeDaMae ?? "";

            if (_oferta.telefoneCelular != null)
                txtTelCelular.Text = _oferta.telefoneCelular.ToString();

            if (_oferta.telefoneResidencial != null)
                txtTelResidencial.Text = _oferta.telefoneResidencial.ToString();

            if (_oferta.telefoneRecado != null)
                txtTelRecado.Text = _oferta.telefoneRecado.ToString();

            if (_oferta.idEstadoCivil != null)
                cmbEstadoCivil.SelectedValue = _oferta.idEstadoCivil.ToString();

            if (_oferta.idProfissao != null)
                cmbProfissao.SelectedValue = _oferta.idProfissao.ToString();

            if (_oferta.idFaixaDeRenda != null)
                cmbFaixaRenda.SelectedValue = _oferta.idFaixaDeRenda.ToString();

            if (_oferta.cep != null)
                txtCep.Text = _oferta.cep.ToString().PadLeft(8, '0');

            txtLogradouro.Text = _oferta.logradouro ?? "";
            txtNumero.Text = _oferta.numero ?? "";
            txtComplemento.Text = _oferta.complemento ?? "";
            txtBairro.Text = _oferta.bairro ?? "";
            txtCidade.Text = _oferta.cidade ?? "";
            txtUf.Text = _oferta.uf ?? "";
            txtPontoReferencia.Text = _oferta.pontoDeReferencia ?? "";
            txtObservacao.Text = _oferta.observacao ?? "";

            if (_oferta.idProduto != null)
            {
                cmbProduto.SelectedValue = _oferta.idProduto.ToString();

                if (cmbProduto.SelectedValue == null)
                    cmbProduto.ResetarComSelecione(habilitar: true);
            }

            txtNumeroMigrado.Text = _oferta.numeroPortabilizado.ToString();

            if (_oferta.faturaDigital == true)
                cmbFaturaDigital.Text = "SIM";
            else if (_oferta.faturaDigital == false)
                cmbFaturaDigital.Text = "NÃO";

            txtEmailFaturaDigital.Text = _oferta.emailFaturaDigital ?? "";

            if (_oferta.diaVencimento != null)
                cmbDiaDeVencimentoDaFatura.Text = _oferta.diaVencimento?.ToString();

            if (_oferta.idFormaDePagamento != null)
                cmbFormaPagamento.SelectedValue = _oferta.idFormaDePagamento.ToString();

            if (_oferta.idBanco != null)
                cmbBanco.SelectedValue = _oferta.idBanco.ToString();

            txtAgencia.Text = _oferta.agencia ?? "";
            txtConta.Text = _oferta.conta ?? "";

            if (_oferta.idOperadora != null)
                cmbOperadora.SelectedValue = _oferta.idOperadora?.ToString();

            if (_oferta.idTipoDeProduto != null)
                cmbTipoDePlano.SelectedValue = _oferta.idTipoDePlano?.ToString();

            txtValorConta1.Text = _oferta.valorConta1.ToString();
            txtValorConta2.Text = _oferta.valorConta2.ToString();
            txtValorConta3.Text = _oferta.valorConta3.ToString();
        }

        private void CarregarStatusDeAuditoria()
        {
            IEnumerable<Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria> retorno = _statusDeAuditoriaService.Listar(ativo: true);
            cmbStatusAuditoria.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarBanco()
        {
            IEnumerable<Banco> retorno = _campanhaService.ListarBancoDaCampanha(_oferta.idCampanha, ativo: true);
            cmbBanco.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Codigo} - {x.Nome}");
        }

        private void CarregarDiaDeVencimentoDaFatura()
        {
            IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _ofertaDoAtendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO();
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
            IEnumerable<FormaDePagamento> retorno = _campanhaService.ListarFormasDePagamentoDaCampanha(_oferta.idCampanha, ativo: true);
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
            IEnumerable<Produto> produtos = _produtoService.Listar(-1, _oferta.idCampanha, (int)_oferta.idTipoDeProduto, true).Distinct();
            cmbProduto.PreencherComSelecione(produtos, x => x.Id, x => x.Nome);
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void FinalizarAuditoriaDeOferta()
        {
            int idUsuario = _usuarioLogado.Id;
            long idOfertaBko = _oferta.id.Value;
            int? idTipoOferta = _oferta.idTipoDeProduto;

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
                mensagens.Add("[Status de Auditoria] não permitido!");

            if (_statusDeAuditoriaAtual != null && _statusDeAuditoriaAtual.HabilitaTrocaDeStatus == false)
            {
                mensagens.Add("[Status de Auditoria] atual da oferta não permite alteração de Status!");
                CallplusFormsUtil.ExibirMensagens(mensagens);
                return false;
            }

            if (statusDeAuditoria.AprovaOferta)
            {
                if (string.IsNullOrEmpty(txtProtocolo.Text))
                {
                    mensagens.Add("[Protocolo] deve ser informado!");
                }

                if (string.IsNullOrEmpty(txtAutorizacao.Text))
                {
                    mensagens.Add("[Autorização] deve ser informada!");
                }

                if (string.IsNullOrEmpty(txtOrdem.Text))
                {
                    mensagens.Add("[Ordem] deve ser informada!");
                }

                if (string.IsNullOrEmpty(txtNumeroProvisorio.Text))
                {
                    mensagens.Add("[Número Provisório] deve ser informado!");
                }

                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    mensagens.Add("[Código Agente] deve ser informado!");
                }

                if (_editaProtocoloNET && string.IsNullOrEmpty(txtProtocoloNet.Text))
                {
                    mensagens.Add("[Protocolo NET] deve ser informado!");
                }

                if (_editaProtocoloNET && string.IsNullOrEmpty(txtNumeroPedido.Text))
                {
                    mensagens.Add("[Número do Pedido] deve ser informado!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDaOferta()
        {
            bool permiteVendaParaNaoTitular = _oferta.idCampanha == 8;

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

            if (string.IsNullOrEmpty(txtCpf.Text))
            {
                lblCpf.ForeColor = Color.Red;
                mensagens.Add("[CPF] deve ser informado!");
            }

            if (!string.IsNullOrEmpty(txtCpf.Text) && !Texto.CpfPossuiFormatoValido(txtCpf.Text))
            {
                lblCpf.ForeColor = Color.Red;
                mensagens.Add("[CPF] inválido!");
            }

            if (permiteVendaParaNaoTitular == false)
            {
                if (CallplusFormsUtil.FormatarCPF(_oferta.cpf.ToString()) != CallplusFormsUtil.FormatarCPF(txtCpf.Text))
                {
                    lblCpf.ForeColor = Color.Red;
                    mensagens.Add("[CPF] deve ser o mesmo informado no Mailing!");
                }
            }

            if (string.IsNullOrEmpty(cmbProduto.Text) || cmbProduto.TextoEhSelecione())
            {
                lblProduto.ForeColor = Color.Red;
                mensagens.Add("[Produto] deve ser informado!");
            }

            if (_oferta.idTipoDeProduto != 2)
            {
                if (string.IsNullOrEmpty(txtRg.Text))
                {
                    lblRg.ForeColor = Color.Red;
                    mensagens.Add("[RG] deve ser informado!");
                }

                if (string.IsNullOrEmpty(txtDataNascimento.Text))
                {
                    lblDataNascimento.ForeColor = Color.Red;
                    mensagens.Add("[Data de Nascimento] deve ser informada!");
                }

                DateTime dataNascimento;
                if (DateTime.TryParse(txtDataNascimento.Text, out dataNascimento))
                {
                    if (CallplusFormsUtil.RetornarIdade(dataNascimento) < 18)
                    {
                        lblDataNascimento.ForeColor = Color.Red;
                        mensagens.Add("[Idade] deve ser maior que 18 anos!");
                    }

                    if (CallplusFormsUtil.RetornarIdade(dataNascimento) > 120)
                    {
                        lblDataNascimento.ForeColor = Color.Red;
                        mensagens.Add("[Idade] deve ser menor que 120 anos!");
                    }
                }
                else
                {
                    lblDataNascimento.ForeColor = Color.Red;
                    mensagens.Add("[Data de Nascimento] inválida!");
                }

                if (string.IsNullOrEmpty(txtNomeDaMae.Text))
                {
                    lblNomeMae.ForeColor = Color.Red;
                    mensagens.Add("[Nome da Mãe] deve ser informado!");
                }

                string[] nomeMae = txtNomeDaMae.Text.Trim().Split(' ');
                if (nomeMae.Length <= 1)
                {
                    lblNomeMae.ForeColor = Color.Red;
                    mensagens.Add("[Nome da Mãe] inválido!");
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

                if (!string.IsNullOrEmpty(txtTelResidencial.Text) && !Texto.TelefoneFixoPossuiFormatoValido(txtTelResidencial.Text))
                {
                    lblTelResidencial.ForeColor = Color.Red;
                    mensagens.Add("[Telefone Residencial] inválido!");
                }

                if (!string.IsNullOrEmpty(txtTelRecado.Text) && !Texto.TelefonePossuiFormatoValido(txtTelRecado.Text))
                {
                    lblTelRecado.ForeColor = Color.Red;
                    mensagens.Add("[Telefone Recado] inválido!");
                }

                if (string.IsNullOrEmpty(txtCep.Text))
                {
                    lblCep.ForeColor = Color.Red;
                    mensagens.Add("[CEP] deve ser informado!");
                }

                if (string.IsNullOrEmpty(cmbFaturaDigital.Text) || cmbFaturaDigital.TextoEhSelecione())
                {
                    lblFaturaDigital.ForeColor = Color.Red;
                    mensagens.Add("[Deseja Fatura Digital] deve ser informada!");
                }

                if (cmbFaturaDigital.Text == "SIM" && string.IsNullOrEmpty(txtEmailFaturaDigital.Text))
                {
                    lblEmailFaturaDigital.ForeColor = Color.Red;
                    mensagens.Add("[E-mail Fatura] deve ser informado!");
                }

                if (cmbFaturaDigital.Text == "SIM" && !string.IsNullOrEmpty(txtEmailFaturaDigital.Text) && !Texto.EmailPosuiFormatoValido(txtEmailFaturaDigital.Text))
                {
                    lblEmailFaturaDigital.ForeColor = Color.Red;
                    mensagens.Add("[E-mail Fatura] inválido!");
                }

                if (string.IsNullOrEmpty(cmbDiaDeVencimentoDaFatura.Text) || cmbDiaDeVencimentoDaFatura.TextoEhSelecione())
                {
                    lblDiaVencimento.ForeColor = Color.Red;
                    mensagens.Add("[Dia de Vencimento] deve ser informado!");
                }

                if (string.IsNullOrEmpty(cmbFormaPagamento.Text) || cmbFormaPagamento.TextoEhSelecione())
                {
                    lblFormaPagamento.ForeColor = Color.Red;
                    mensagens.Add("[Forma de Pagamento] deve ser informado!");
                }

                if (cmbFormaPagamento.Text == "DÉBITO EM CONTA")
                {
                    if (string.IsNullOrEmpty(cmbBanco.Text) || cmbBanco.TextoEhSelecione())
                    {
                        lblBanco.ForeColor = Color.Red;
                        mensagens.Add("[Banco] deve ser informado!");
                    }

                    if (string.IsNullOrEmpty(txtAgencia.Text))
                    {
                        lblAgencia.ForeColor = Color.Red;
                        mensagens.Add("[Agência] deve ser informada!");
                    }

                    if (string.IsNullOrEmpty(txtConta.Text))
                    {
                        lblConta.ForeColor = Color.Red;
                        mensagens.Add("[Conta] deve ser informada!");
                    }
                }

                if (string.IsNullOrEmpty(cmbOperadora.Text) || cmbOperadora.TextoEhSelecione())
                {
                    lblOperadora.ForeColor = Color.Red;
                    mensagens.Add("[Operadora] deve ser informada!");
                }

                if (string.IsNullOrEmpty(cmbTipoDePlano.Text) || cmbTipoDePlano.TextoEhSelecione())
                {
                    lblTipoDePlano.ForeColor = Color.Red;
                    mensagens.Add("[Tipo de Plano] deve ser informado!");
                }

                if (cmbTipoDePlano.Text == "POS")
                {
                    decimal valor;

                    if (string.IsNullOrEmpty(txtValorConta1.Text))
                    {
                        lblValorConta1.ForeColor = Color.Red;
                        mensagens.Add("[Valor Conta 1] deve ser informado!");
                    }

                    if (!string.IsNullOrEmpty(txtValorConta1.Text) && !decimal.TryParse(txtValorConta1.Text, out valor))
                    {
                        lblValorConta1.ForeColor = Color.Red;
                        mensagens.Add("[Valor Conta 1] inválido!");
                    }

                    if (string.IsNullOrEmpty(txtValorConta2.Text))
                    {
                        lblValorConta2.ForeColor = Color.Red;
                        mensagens.Add("[Valor Conta 2] deve ser informado!");
                    }

                    if (!string.IsNullOrEmpty(txtValorConta2.Text) && !decimal.TryParse(txtValorConta2.Text, out valor))
                    {
                        lblValorConta2.ForeColor = Color.Red;
                        mensagens.Add("[Valor Conta 2] inválido!");
                    }

                    if (string.IsNullOrEmpty(txtValorConta3.Text))
                    {
                        lblValorConta3.ForeColor = Color.Red;
                        mensagens.Add("[Valor Conta 3] deve ser informado!");
                    }

                    if (!string.IsNullOrEmpty(txtValorConta3.Text) && !decimal.TryParse(txtValorConta3.Text, out valor))
                    {
                        lblValorConta3.ForeColor = Color.Red;
                        mensagens.Add("[Valor Conta 3] inválido!");
                    }
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
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

                _historicoOfertaBKO.idOfertaDoAtendimentoPortabilidadeBKO = (long)_oferta.id;
                _historicoOfertaBKO.idStatusAuditoria = Convert.ToInt32(cmbStatusAuditoria.SelectedValue);
                _historicoOfertaBKO.protocolo = txtProtocolo.Text;
                _historicoOfertaBKO.autorizacao = txtAutorizacao.Text;
                _historicoOfertaBKO.ordem = txtOrdem.Text;

                if (!string.IsNullOrEmpty(txtNumeroProvisorio.Text))
                {
                    _historicoOfertaBKO.numeroProvisorio = Convert.ToInt64(txtNumeroProvisorio.Text);
                }

                _historicoOfertaBKO.loginWM = lblLoginValidado.Text;
                _historicoOfertaBKO.codigoAgente = txtCodigo.Text;
                _historicoOfertaBKO.idCriador = AdministracaoMDI._usuario.Id;
                _historicoOfertaBKO.Observacao = txtObservacao.Text;

                _historicoOfertaBKO.id = _ofertaDoAtendimentoService.GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(_historicoOfertaBKO);
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
                if (!cmbProduto.TextoEhSelecione())
                    _oferta.idProduto = Convert.ToInt32(cmbProduto.SelectedValue);

                if (!string.IsNullOrEmpty(txtNumeroMigrado.Text))
                    _oferta.numeroPortabilizado = Convert.ToInt64(txtNumeroMigrado.Text);

                if (!cmbFaturaDigital.TextoEhSelecione())
                    _oferta.faturaDigital = (cmbFaturaDigital.Text == "SIM") ? true : false;

                if (!string.IsNullOrEmpty(txtEmailFaturaDigital.Text))
                    _oferta.emailFaturaDigital = txtEmailFaturaDigital.Text;

                if (!cmbDiaDeVencimentoDaFatura.TextoEhSelecione())
                    _oferta.diaVencimento = Convert.ToInt32(cmbDiaDeVencimentoDaFatura.Text);

                if (!cmbFormaPagamento.TextoEhSelecione())
                    _oferta.idFormaDePagamento = Convert.ToInt32(cmbFormaPagamento.SelectedValue);

                if (!cmbBanco.TextoEhSelecione())
                    _oferta.idBanco = Convert.ToInt32(cmbBanco.SelectedValue);

                if (!string.IsNullOrEmpty(txtAgencia.Text))
                    _oferta.agencia = txtAgencia.Text;

                if (!string.IsNullOrEmpty(txtConta.Text))
                    _oferta.conta = txtConta.Text;

                if (!string.IsNullOrEmpty(txtNome.Text))
                    _oferta.nome = txtNome.Text;

                if (!string.IsNullOrEmpty(txtCpf.Text))
                    _oferta.cpf = Convert.ToInt64(txtCpf.Text);

                if (!string.IsNullOrEmpty(txtRg.Text))
                    _oferta.rg = txtRg.Text;

                DateTime dtNascimento;
                if (DateTime.TryParse(txtDataNascimento.Text, out dtNascimento))
                    _oferta.nascimento = dtNascimento;

                if (!string.IsNullOrEmpty(txtNomeDaMae.Text))
                    _oferta.nomeDaMae = txtNomeDaMae.Text;

                if (!string.IsNullOrEmpty(txtTelCelular.Text))
                    _oferta.telefoneCelular = Convert.ToInt64(txtTelCelular.Text);

                if (!string.IsNullOrEmpty(txtTelResidencial.Text))
                    _oferta.telefoneResidencial = Convert.ToInt64(txtTelResidencial.Text);

                if (!string.IsNullOrEmpty(txtTelRecado.Text))
                    _oferta.telefoneRecado = Convert.ToInt64(txtTelRecado.Text);

                if (!cmbEstadoCivil.TextoEhSelecione())
                    _oferta.idEstadoCivil = Convert.ToInt32(cmbEstadoCivil.SelectedValue);

                if (!cmbProfissao.TextoEhSelecione())
                    _oferta.idProfissao = Convert.ToInt32(cmbProfissao.SelectedValue);

                if (!cmbFaixaRenda.TextoEhSelecione())
                    _oferta.idFaixaDeRenda = Convert.ToInt32(cmbFaixaRenda.SelectedValue);

                if (!string.IsNullOrEmpty(txtCep.Text))
                    _oferta.cep = Convert.ToInt64(txtCep.Text);

                if (!string.IsNullOrEmpty(txtLogradouro.Text))
                    _oferta.logradouro = txtLogradouro.Text;

                if (!string.IsNullOrEmpty(txtNumero.Text))
                    _oferta.numero = txtNumero.Text;

                if (!string.IsNullOrEmpty(txtComplemento.Text))
                    _oferta.complemento = txtComplemento.Text;

                if (!string.IsNullOrEmpty(txtBairro.Text))
                    _oferta.bairro = txtBairro.Text;

                if (!string.IsNullOrEmpty(txtCidade.Text))
                    _oferta.cidade = txtCidade.Text;

                if (!string.IsNullOrEmpty(txtUf.Text))
                    _oferta.uf = txtUf.Text;

                if (!string.IsNullOrEmpty(txtPontoReferencia.Text))
                    _oferta.pontoDeReferencia = txtPontoReferencia.Text;

                if (!string.IsNullOrEmpty(txtObservacao.Text))
                    _oferta.observacao = txtObservacao.Text;

                if (!cmbOperadora.TextoEhSelecione())
                    _oferta.idOperadora = Convert.ToInt32(cmbOperadora.SelectedValue);

                if (!cmbTipoDePlano.TextoEhSelecione())
                    _oferta.idTipoDePlano = Convert.ToInt32(cmbTipoDePlano.SelectedValue);

                if (!string.IsNullOrEmpty(txtValorConta1.Text))
                    _oferta.valorConta1 = Convert.ToDecimal(txtValorConta1.Text);

                if (!string.IsNullOrEmpty(txtValorConta2.Text))
                    _oferta.valorConta2 = Convert.ToDecimal(txtValorConta2.Text);

                if (!string.IsNullOrEmpty(txtValorConta3.Text))
                    _oferta.valorConta3 = Convert.ToDecimal(txtValorConta3.Text);

                //_oferta.id = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoClaroMigracao(_oferta);
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
                long idOfertaBko = _oferta.id.Value;
                var hist = _ofertaDoAtendimentoService.ListarHistoricoDaOfertaDoAtendimentoBKO(idOfertaBko, (int)_oferta.idTipoDeProduto);
                dgHistorico.DataSource = hist;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                MessageBox.Show($"Ocorreu um erro ao carregar o histórico da auditoria!", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelecionarHistorico(int linhaSelecionada)
        {
            long idHistoricoBko = (long)dgHistorico.Rows[linhaSelecionada].Cells["id"].Value;
            var historico = _ofertaDoAtendimentoService.RetornarHistoricoDaOfertaDoAtendimentoBKO_DTO(idHistoricoBko, (int)_oferta.idTipoDeProduto);

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

        private void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultarLoginWM();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível consultar o Login!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

            if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
            {
                e.Handled = true;
            }
        }

        private void cmbFaturaDigital_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFaturaDigital.ForeColor = SystemColors.WindowText;
            lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;

            if (cmbFaturaDigital.Text == "SIM")
            {
                txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: false);
                txtEmailFaturaDigital.BackColor = SystemColors.InactiveBorder;
            }
            else
            {
                txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: true);
                txtEmailFaturaDigital.BackColor = Color.WhiteSmoke;
            }
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
                int diaVencimento = int.Parse(cmbDiaDeVencimentoDaFatura.Text);
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

            if (cmbFormaPagamento.Text == "DÉBITO EM CONTA")
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
                txtAgencia.Resetar(habilitar: true, limparTexto: true, readOnly: true);
                txtConta.Resetar(habilitar: true, limparTexto: true, readOnly: true);

                txtAgencia.BackColor = Color.WhiteSmoke;
                txtConta.BackColor = Color.WhiteSmoke;
            }
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
                    MessageBox.Show("[E-mail] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;
            }
        }

        private void cmbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBanco.ForeColor = SystemColors.WindowText;
        }

        private void txtAgencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblAgencia.ForeColor = SystemColors.WindowText;
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void txtConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblConta.ForeColor = SystemColors.WindowText;
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNome.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereSomenteLetra(e.KeyChar);
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

        private void txtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCpf.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtCpf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCpf.Text))
            {
                txtCpf.Text = CallplusFormsUtil.FormatarCPF(txtCpf.Text);

                if (!Texto.CpfPossuiFormatoValido(txtCpf.Text) && !string.IsNullOrEmpty(txtCpf.Text))
                {
                    lblCpf.ForeColor = Color.Red;
                    txtCpf.Focus();
                    MessageBox.Show("[CPF] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblCpf.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtRg_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblRg.ForeColor = SystemColors.WindowText;
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void txtDataNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            lIdade.Text = "";
            lblDataNascimento.ForeColor = SystemColors.WindowText;
        }

        private void txtDataNascimento_Leave(object sender, EventArgs e)
        {
            lIdade.Text = "";

            if (!string.IsNullOrEmpty(txtDataNascimento.Text))
            {
                if (Texto.DataEhValida(txtDataNascimento.Text))
                {
                    DateTime dataNascimento = DateTime.Parse(txtDataNascimento.Text);

                    int idade = CallplusFormsUtil.RetornarIdade(dataNascimento);

                    lIdade.Text = idade + " anos";

                    if (idade < 18)
                    {
                        lblDataNascimento.ForeColor = Color.Red;
                        txtDataNascimento.Focus();
                        MessageBox.Show("[Idade] deve ser maior que 18 anos!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (CallplusFormsUtil.RetornarIdade(dataNascimento) > 120)
                    {
                        lblDataNascimento.ForeColor = Color.Red;
                        MessageBox.Show("[Idade] deve ser menor que 120 anos!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    lblDataNascimento.ForeColor = Color.Red;
                    txtDataNascimento.Focus();
                    MessageBox.Show("[Data de Nascimento] inválida!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblDataNascimento.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNomeDaMae_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNomeMae.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereSomenteLetra(e.KeyChar);
        }

        private void txtNomeDaMae_Leave(object sender, EventArgs e)
        {
            txtNomeDaMae.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(txtNomeDaMae.Text).ToUpper();

            if (!string.IsNullOrEmpty(txtNomeDaMae.Text))
            {
                string[] nomeMae = txtNomeDaMae.Text.Trim().Split(' ');
                if (nomeMae.Length <= 1)
                {
                    lblNomeMae.ForeColor = Color.Red;
                    txtNomeDaMae.Focus();
                    MessageBox.Show("[Nome da Mãe] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNome.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtTelCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblTelCelular.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtTelCelular_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTelCelular.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtTelCelular.Text))
                {
                    lblTelCelular.ForeColor = Color.Red;
                    txtTelCelular.Focus();
                    MessageBox.Show("[Telefone Celular] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblTelCelular.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtTelResidencial_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblTelResidencial.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtTelResidencial_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTelResidencial.Text))
            {
                if (!Texto.TelefoneFixoPossuiFormatoValido(txtTelResidencial.Text))
                {
                    lblTelResidencial.ForeColor = Color.Red;
                    txtTelResidencial.Focus();
                    MessageBox.Show("[Telefone Residencial] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblTelResidencial.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtTelRecado_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblTelRecado.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtTelRecado_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTelRecado.Text))
            {
                if (!Texto.TelefonePossuiFormatoValido(txtTelRecado.Text))
                {
                    lblTelRecado.ForeColor = Color.Red;
                    txtTelRecado.Focus();
                    MessageBox.Show("[Telefone Recado] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblTelRecado.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCep.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtNumeroProvisorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNumeroProvisorio.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
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

        #endregion EVENTOS        
    }
}
