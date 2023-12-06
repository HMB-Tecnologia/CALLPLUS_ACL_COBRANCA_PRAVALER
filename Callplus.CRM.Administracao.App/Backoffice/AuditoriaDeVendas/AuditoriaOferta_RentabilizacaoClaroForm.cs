using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CallplusUtil.Validacoes;

namespace Callplus.CRM.Administracao.App.Backoffice.AuditoriaDeVendas
{
    public partial class AuditoriaOferta_RentabilizacaoClaroForm : Form
    {
        public AuditoriaOferta_RentabilizacaoClaroForm(long idOferta)
        {
            _logger = LogManager.GetCurrentClassLogger();
            
            _campanhaService = new CampanhaService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _produtoService = new ProdutoService();
            _ofertaService = new ProspectService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _aparelhoService = new AparelhoService();

            _usuarioLogado = AdministracaoMDI._usuario;


            _oferta = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoClaroRentabilizacaoBKO(idOferta);
            _resumoDaOferta = _ofertaDoAtendimentoService.RetornarResumoDaOfertaDoAtendimentoBKO(idOferta, (int)_oferta.idTipoDeProduto);


            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        
        private readonly AparelhoService _aparelhoService;
        private readonly CampanhaService _campanhaService;
        private readonly ProdutoService _produtoService;
        private readonly ProspectService _ofertaService;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria _statusDeAuditoriaAtual;
        private readonly Usuario _usuarioLogado;
        private bool _ofertaFoiAtualizada;


        private OfertaDoAtendimentoClaroRentabilizacaoBKO _oferta;
        private ResumoDaOfertaDoAtendimentoBkoDTO _resumoDaOferta;
        private HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO _historicoOfertaBKO;

        private bool _editarDadosPessoais;
        private bool _editarEndereco;
        private bool _editarDadosProduto;
        private bool _editarDadosAparelho;
        private bool _editarDadosPagamento;
        private bool _editarDadosPassaporte;

        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            _editarDadosPessoais = false;
            _editarEndereco = false;
            _editarDadosProduto = false;
            _editarDadosAparelho = true;
            _editarDadosPagamento = false;
            _editarDadosPassaporte = VerificarSeHabilitaOfertaPassaporte(_oferta.idCampanha);

            if (_oferta.idCampanha == 4)
            {
                _editarDadosPessoais = true;
                _editarEndereco = true;
                _editarDadosProduto = true;
                _editarDadosAparelho = true;
                _editarDadosPagamento = true;
            }

            //gbDadosPessoais.Enabled = _editarDadosPessoais;
            //gbEnderecoResidencial.Enabled = _editarEndereco;
            //gbDadosOferta.Enabled = _editarDadosProduto;
            gbDadosAparelho.Enabled = _editarDadosAparelho;
            gbDadosPagamento.Enabled = _editarDadosPagamento;

            cmbPassaporteOferta.Enabled = _editarDadosPassaporte;
            cmbStatusAuditoria.DropDown += CallplusUtil.Forms.CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            Atualizar = false;
            lIdade.Text = "";
            lblLoginValidado.Text = "";
            cmbFaturaDigital.ResetarComSelecione(habilitar: true);

            CarregarEConfigurarCombosVazios();
            CarregarProduto();
            CarregarBanco();
            CarregarDiaDeVencimentoDaFatura();
            CarregarPassaporteOferta();
            CarregarFaixaDeRenda();
            CarregarEstadoCivil();
            CarregarProfissao();
            CarregarFormaDePagamento();
            CarregarStatusDeAuditoria();
            CarregarHistoricoDeAuditoria();


            if (_oferta != null)
            {
                CarregarAparelhosDoAtendimento(_oferta.idProspect);

                CarregarResumoDaOferta();
                CarregarDadosDaOferta();
                CarregarLayoutDinamicoBko();
                CarregarStatusDeAuditoriaAtual(_oferta.idStatusAuditoria);
                ConfigurarTelaDeAcordoComStatusAuditoria(_statusDeAuditoriaAtual);
                IniciarAuditoriaDeOferta(_statusDeAuditoriaAtual, _usuarioLogado.Id, _oferta.id.Value);
            }

            CarregarRegrasDoPerfil();
        }
                
        private void CarregarEConfigurarCombosVazios()
        {
            List<KeyValuePair<int, string>> listaFake = new List<KeyValuePair<int, string>>();

            cmbDesejaAparelho.ResetarComSelecione(habilitar: true);
            cmbAparelho.PreencherComSelecione(listaFake);
            cmbFormaPagamentoAparelho.PreencherComSelecione(listaFake);
            cmbPassaporteOferta.PreencherComSelecione(listaFake);
            cmbPassaporteOferta.ResetarComSelecione(habilitar: _editarDadosPassaporte);
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
                _historicoOfertaBKO = _ofertaDoAtendimentoService.IniciarAuditoriaDaOfertaClaroRentabilizacaoBKO(idUsuario, idOfertaBko);
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
                txtCep.Text = _oferta.cep.ToString();

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

                if (cmbProduto.SelectedValue != null)
                    cmbProduto.Enabled = false;
                else
                    cmbProduto.ResetarComSelecione(habilitar: true);
            }

            txtNumeroMigrado.Text = _oferta.numeroMigrado.ToString();

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

            CarregarAparelhosDoAtendimento(_oferta.idProspect);

            if (_oferta.desejaAparelho != null)
            {
                bool desejaAparelho = _oferta.desejaAparelho ?? false;

                cmbDesejaAparelho.Text = desejaAparelho ? "SIM" : "NÃO";

                if (_oferta.idAparelho != null)
                {
                    int idAparelho = _oferta.idAparelho ?? 0;
                    int idProduto = _oferta.idProduto ?? 0;

                    cmbAparelho.SelectedValue = idAparelho.ToString();
                    CarregarFormaDePagamentoDeAparelho(idProduto, idAparelho);

                    if (_oferta.IdFormaDePagamentoDeAparelho != null)
                        cmbFormaPagamentoAparelho.SelectedValue = _oferta.IdFormaDePagamentoDeAparelho.ToString();
                }

                ConfigurarCamposDeAparelhoAoCarregar(desejaAparelho);
            }

            if (_oferta.IdPassaporteOferta != null)
                cmbPassaporteOferta.SelectedValue = _oferta.IdPassaporteOferta.ToString();

        }

        private void CarregarStatusDeAuditoria()
        {
            IEnumerable<Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria> retorno = _statusDeAuditoriaService.Listar(_oferta.idCampanha, ativo: true);
            cmbStatusAuditoria.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarBanco()
        {
            IEnumerable<Banco> retorno = _campanhaService.ListarBancoDaCampanha(_oferta.idCampanha, ativo: true);
            cmbBanco.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Codigo} - {x.Nome}");
        }

        private void CarregarDiaDeVencimentoDaFatura()
        {
            IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _ofertaDoAtendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(false);
            cmbDiaDeVencimentoDaFatura.PreencherComSelecione(configuracaoDatas, x => x.Fechamento, x => x.Vencimento);
        }

        private void CarregarPassaporteOferta()
        {
            IEnumerable<PassaporteOferta> retorno = _ofertaDoAtendimentoService.ListarPassaporteOferta();
            cmbPassaporteOferta.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
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

        private void CarregarFormaDePagamentoDeAparelho(int idProduto, int idAparelho)
        {
            IEnumerable<FormaDePagamentoDeAparelho> retorno = _aparelhoService.ListarFormaDePagamentoDeAparelho(idProduto, idAparelho);
            cmbFormaPagamentoAparelho.PreencherComSelecione(retorno, x => x.Id, x => x.Descricao);
        }

        private void CarregarProfissao()
        {
            IEnumerable<Profissao> retorno = _ofertaService.ListarProfissao(ativo: true);
            cmbProfissao.PreencherComSelecione(retorno, x => x.id, x => x.nome);
        }

        private void CarregarProduto()
        {
            IEnumerable<Produto> produtos = _produtoService.Listar(-1, _oferta.idCampanha, (int)_oferta.idTipoDeProduto, true).Distinct();
            cmbProduto.PreencherComSelecione(produtos, x => x.Id, x => x.Nome);
        }

        private void CarregarAparelhosDoAtendimento(long idProspect)
        {
            IEnumerable<Aparelho> retorno = _aparelhoService.ListarAparelhosDoAtendimento(idProspect);
            cmbAparelho.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void ConfigurarCamposDeAparelhoAoCarregar(bool desejaAparelho)
        {
            if (desejaAparelho)
            {
                cmbAparelho.Habilitar();
                cmbFormaPagamentoAparelho.Habilitar();
            }
            else
            {
                cmbAparelho.Desabilitar();
                cmbFormaPagamentoAparelho.Desabilitar();
            }
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

                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    mensagens.Add("[Código Agente] deve ser informado!");
                }
            }

            LimparNotificacoesDaTela();

            if (_editarDadosPessoais)
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
            }

            if (_editarEndereco)
            {
                if (string.IsNullOrEmpty(txtCep.Text))
                {
                    lblCep.ForeColor = Color.Red;
                    mensagens.Add("[CEP] deve ser informado!");
                }
            }

            if (_editarDadosPagamento)
            {
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
            }

            if (_editarDadosPassaporte)
            {
                if (string.IsNullOrEmpty(cmbPassaporteOferta.Text) || cmbPassaporteOferta.TextoEhSelecione())
                {
                    lblPassaporte.ForeColor = Color.Red;
                    mensagens.Add("[Passaporte] deve ser informado!");
                }
            }

            if (_editarDadosAparelho)
            {
                if (string.IsNullOrEmpty(cmbDesejaAparelho.Text) || cmbDesejaAparelho.TextoEhSelecione())
                {
                    lblDesejaAparelho.ForeColor = Color.Red;
                    mensagens.Add("[Deseja Aparelho] deve ser informado!");
                }

                if (cmbDesejaAparelho.TextoEhSelecione() == false)
                {
                    bool desejaAparelho = cmbDesejaAparelho.Text == "SIM";
                    if (desejaAparelho && cmbAparelho.TextoEhSelecione())
                    {
                        lblAparelho.ForeColor = Color.Red;
                        mensagens.Add("[Aparelho] deve ser informado!");
                    }

                    if (desejaAparelho && cmbFormaPagamentoAparelho.TextoEhSelecione())
                    {
                        lblParcela.ForeColor = Color.Red;
                        mensagens.Add("[Parcelas] deve ser informado!");
                    }
                }
            }



            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool VerificarSeHabilitaOfertaPassaporte(long idCampanha)
        {
            bool habilita = false;

            habilita = (idCampanha == 2 || idCampanha == 4 || idCampanha == 9 || idCampanha == 6);

            return habilita;
        }

        private void LimparNotificacoesDaTela()
        {
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

            foreach (var item in gbDadosAparelho.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
            {
                item.ForeColor = SystemColors.WindowText;
            }

            foreach (var item in gbDadosPagamento.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
            {
                item.ForeColor = SystemColors.WindowText;
            }

        }
        
        private bool GravarDadosDaOferta()
        {
            bool result = false;
            if (AtendeRegrasDeGravacao())
            {
             
                if (!cmbProduto.TextoEhSelecione())
                    _oferta.idProduto = int.Parse(cmbProduto.SelectedValue.ToString());

                if (!string.IsNullOrEmpty(txtNumeroMigrado.Text))
                    _oferta.numeroMigrado = Convert.ToInt64(txtNumeroMigrado.Text);

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

                if (!string.IsNullOrEmpty(cmbDesejaAparelho.Text) && cmbDesejaAparelho.Text == "NÃO")
                    _oferta.desejaAparelho = false;

                if (!string.IsNullOrEmpty(cmbDesejaAparelho.Text) && cmbDesejaAparelho.Text == "SIM")
                {
                    int idAparelho = 0;
                    int idFormaPagamentoAparelho = 0;

                    _oferta.desejaAparelho = true;

                    if (int.TryParse(cmbAparelho.SelectedValue.ToString(), out idAparelho))
                        _oferta.idAparelho = idAparelho;

                    if (int.TryParse(cmbFormaPagamentoAparelho.SelectedValue.ToString(), out idFormaPagamentoAparelho))
                        _oferta.IdFormaDePagamentoDeAparelho = idFormaPagamentoAparelho;

                }

                //Dados Adicionais
                if (!string.IsNullOrEmpty(cmbPassaporteOferta.Text) && cmbPassaporteOferta.TextoEhSelecione() == false)
                {
                    int idPassaportOferta = int.Parse(cmbPassaporteOferta.SelectedValue.ToString());
                    _oferta.IdPassaporteOferta = idPassaportOferta;
                }

                long idRetorno = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoClaroRentabilizacaoBKO(_oferta);
                if (idRetorno > 0) result = true;
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
                txtObservacoes_historico.Text = historico.Observacao;
            }

        }

        private void ResetarCamposDeAparelho(bool desejaAparelho)
        {
            cmbAparelho.ResetarComSelecione(habilitar: desejaAparelho);
            cmbFormaPagamentoAparelho.ResetarComSelecione(habilitar: desejaAparelho);
        }

        #endregion METODOS

        #region EVENTOS

        private void AuditoriaOferta_RentabilizacaoClaroForm_Load(object sender, EventArgs e)
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
                if (GravarDadosDaOferta())
                {
                    GravarHistoricoDaOferta();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show($"Não foi possível gravar a Auditoria!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GravarHistoricoDaOferta()
        {
            if (_historicoOfertaBKO == null)
            {
                _historicoOfertaBKO = new HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO();
            }

            _historicoOfertaBKO.idOfertaDoAtendimentoRentabilizacaoBKO = (long)_oferta.id;
            _historicoOfertaBKO.idStatusAuditoria = Convert.ToInt32(cmbStatusAuditoria.SelectedValue);
            _historicoOfertaBKO.protocolo = txtProtocolo.Text;
            _historicoOfertaBKO.autorizacao = txtAutorizacao.Text;
            _historicoOfertaBKO.loginWM = lblLoginValidado.Text;
            _historicoOfertaBKO.codigoAgente = txtCodigo.Text;
            _historicoOfertaBKO.idCriador = AdministracaoMDI._usuario.Id;
            _historicoOfertaBKO.Observacao = txtObservacao.Text;

            _historicoOfertaBKO.id = _ofertaDoAtendimentoService.GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(_historicoOfertaBKO);
            MessageBox.Show("Auditoria gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            _ofertaFoiAtualizada = true;
            Atualizar = true;
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

        private void AuditoriaOferta_RentabilizacaoClaroForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void cmbFormaPagamento_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void cmbDiaDeVencimentoDaFatura_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void cmbBanco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblDiaVencimento.ForeColor = SystemColors.WindowText;
        }

        private void cmbFaturaDigital_SelectedValueChanged(object sender, EventArgs e)
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

        private void cmbDesejaAparelho_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool desejaAparelho = cmbDesejaAparelho.Text == "SIM";
            ResetarCamposDeAparelho(desejaAparelho);
        }

        private void cmbAparelho_SelectionChangeCommitted(object sender, EventArgs e)
        {try
            {
                int idAparelho = int.Parse(cmbAparelho.SelectedValue.ToString());
                int idProduto = int.Parse(cmbProduto.SelectedValue.ToString());
                CarregarFormaDePagamentoDeAparelho(idProduto, idAparelho);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível carregar as formas de pagamento para o aparelho selecionado|\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        #endregion EVENTOS        

        private void txtRg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
