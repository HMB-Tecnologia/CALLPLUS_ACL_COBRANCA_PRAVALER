using Callplus.CRM.Administracao.App.Qualidade.AvaliacaoDeAtendimento;
using Callplus.CRM.Administracao.App.Qualidade.NovaAvaliacaoDeAtendimentoForm;
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
    public partial class AuditoriaOferta_MigracaoClaroForm : Form
    {
        public AuditoriaOferta_MigracaoClaroForm(long idOferta)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _atendimentoService = new AtendimentoService();
            _campanhaService = new CampanhaService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _produtoService = new ProdutoService();
            _ofertaService = new ProspectService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _usuarioLogado = AdministracaoMDI._usuario;
            _tipos = new TipoDeAvaliacaoDeAtendimento();

            _oferta = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoClaroMigracaoBKO(idOferta);
            _resumoDaOferta = _ofertaDoAtendimentoService.RetornarResumoDaOfertaDoAtendimentoBKO(idOferta, (int)_oferta.idTipoDeProduto);

            _campanha = _campanhaService.RetornarCampanha(_oferta.idCampanha);

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
        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
        private AvaliacaoDeAtendimento _avaliacaoDeAtendimento;
        private Callplus.CRM.Tabulador.Dominio.Entidades.StatusDeAuditoria _statusDeAuditoriaAtual;
        private readonly Usuario _usuarioLogado;
        //private readonly int _idFormulario;
        //private readonly string _avaliador;
        //private readonly int _pontuacao;
        //private readonly string _dataDaAvaliacao;
        //private readonly long _idAvaliacao;
        private bool _ofertaFoiAtualizada;
        private readonly TipoDeAvaliacaoDeAtendimento _tipos;
        private readonly Campanha _campanha;


        private OfertaDoAtendimentoClaroMigracaoBKO _oferta;
        private ResumoDaOfertaDoAtendimentoBkoDTO _resumoDaOferta;
        private HistoricoDaOfertaDoAtendimentoMigracaoBKO _historicoOfertaBKO;

        private bool _filtraPorFaixaDeRecarga;


        public bool Atualizar { get; set; }

        private int _idProdutoInicial = 0;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            cmbStatusAuditoria.DropDown += CallplusUtil.Forms.CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            Atualizar = false;
            lIdade.Text = "";
            lblLoginValidado.Text = "";
            //cmbFaturaDigital.ResetarComSelecione(habilitar: true);
            //cmbCodigo21.ResetarComSelecione(habilitar: true);
            cmbReceberContrato.ResetarComSelecione(habilitar: true);
            cmbOndeReceberContrato.ResetarComSelecione(habilitar: true);
            cmbSexo.ResetarComSelecione(habilitar: true);
            //TODO - O código comentado abaixo não se aplica a essa operação Vivo / Filtro de recarga sempre false - Rei Almeida
            _filtraPorFaixaDeRecarga = false;
            
            //if (_campanha.idTipoDaCampanha == 7)
            //{
            //    _filtraPorFaixaDeRecarga = false;
            //    gbLiberarMplay.Enabled = false;
            //}

            CarregarProduto();
            CarregarBanco();
            CarregarDiaDeVencimentoDaFatura();
            CarregarFaixaDeRenda();
            CarregarEstadoCivil();
            CarregarProfissao();
            CarregarFormaDePagamento();
            CarregarStatusDeAuditoria();
            CarregarDadosDaAvaliacaoDeQualidade();
            CarregarHistoricoDeAuditoria();

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

        private void CarregarDadosDaAvaliacaoDeQualidade()
        {
            _avaliacaoDeAtendimento = _avaliacaoDeAtendimentoService.RetornarOfertaMigracao((long)_oferta.id);

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
                _historicoOfertaBKO = _ofertaDoAtendimentoService.IniciarAuditoriaDaOfertaClaroMigracaoBKO(idUsuario, idOfertaBko);
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
                var status = _statusDeAuditoriaService.Listar(-1, ativo: true, idStatus: idStatusAuditoria.Value).FirstOrDefault(x => x.Id == idStatusAuditoria);

                _statusDeAuditoriaAtual = status;
            }
        }

        private void CarregarLayoutDinamicoBko()
        {
            Campanha campanhaDaOferta = _campanha;

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

            if (!string.IsNullOrEmpty(_oferta.cpf?.ToString()))
            txtCpf.Text = CallplusFormsUtil.FormatarCPF(_oferta.cpf?.ToString());

            txtRg.Text = _oferta.rg;

            if (!string.IsNullOrEmpty(_oferta.Sexo))
                cmbSexo.Text = _oferta.Sexo.ToString();

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

                _idProdutoInicial = (int)_oferta.idProduto;

                if (cmbProduto.SelectedValue == null)
                    cmbProduto.ResetarComSelecione(habilitar: true);
            }

            txtNumeroMigrado.Text = _oferta.numeroMigrado.ToString();


            //if (_oferta.OfertaAparelho == true)
            //    cmbOfertaAparelho.Text = "SIM";
            //else if (_oferta.OfertaAparelho == false)
            //    cmbOfertaAparelho.Text = "NÃO";

            //txtUrl.Text = _oferta.Url ?? "";

            //if (_oferta.faturaDigital == true)
            //    cmbFaturaDigital.Text = "SIM";
            //else if (_oferta.faturaDigital == false)
            //    cmbFaturaDigital.Text = "NÃO";


            if (_oferta.diaVencimento != null)
            {
                cmbDiaDeVencimentoDaFatura.Text = _oferta.diaVencimento?.ToString();

                if (cmbDiaDeVencimentoDaFatura.TextoEhSelecione())
                {
                    cmbDiaDeVencimentoDaFatura.Text = _oferta.diaVencimento?.ToString() + " - HOJE";
                    //lblDiaVencimento.Text = lblDiaVencimento.Text + " (DIA:" + _oferta.diaVencimento?.ToString() + ")";
                }
            }

            if (_oferta.idFormaDePagamento != null)
                cmbFormaPagamento.SelectedValue = _oferta.idFormaDePagamento.ToString();

            if (_oferta.idBanco != null)
                cmbBanco.SelectedValue = _oferta.idBanco.ToString();

            txtAgencia.Text = _oferta.agencia ?? "";
            txtConta.Text = _oferta.conta ?? "";

            //if (_oferta.codigo21 == true)
            //    cmbCodigo21.Text = "SIM";
            //else if (_oferta.codigo21 == false)
            //    cmbCodigo21.Text = "NÃO";
            
            txtEmailFaturaDigital.Text = _oferta.emailFaturaDigital ?? "";

            if (_oferta.receberContrato == true)
                cmbReceberContrato.Text = "SIM";
            else if (_oferta.receberContrato == false)
                cmbReceberContrato.Text = "NÃO";

            if (string.IsNullOrEmpty(_oferta.ondeReceberContrato))
                cmbOndeReceberContrato.ResetarComSelecione(habilitar: true);
            else if (_oferta.ondeReceberContrato == "E-MAIL")
                cmbOndeReceberContrato.Text = "E-MAIL";
            else if (_oferta.ondeReceberContrato == "CORREIO")
                cmbOndeReceberContrato.Text = "CORREIO";

            if (_oferta.NumeroFaturaWhatsApp != null)
                txtNumeroFaturaWhatsApp.Text = _oferta.NumeroFaturaWhatsApp.ToString();
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
            IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _ofertaDoAtendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(false); ;
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

        private void CarregarProduto()
        {
            IEnumerable<Produto> produtos;

            if (_filtraPorFaixaDeRecarga && _oferta.idCampanha != 16)
            {
                produtos = _produtoService.ListarProdutoDaOfertaPorFaixaDeRecargaBKO(_oferta.idProspect).Distinct();
            }
            else
            {
                produtos = _produtoService.Listar(-1, _oferta.idCampanha, (int)_oferta.idTipoDeProduto, true).Distinct();
                //produtos = _produtoService.ListarProdutoDaOfertaPorIdProspect(-1, _oferta.idProspect, );
            }

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

            //if (statusDeAuditoria.AprovaOferta)
            //{
            //    if (string.IsNullOrEmpty(txtProtocolo.Text))
            //    {
            //        mensagens.Add("[Protocolo] deve ser informado!");
            //    }

            //    if (string.IsNullOrEmpty(txtAutorizacao.Text))
            //    {
            //        mensagens.Add("[Autorização] deve ser informada!");
            //    }

            //    if (string.IsNullOrEmpty(txtCodigo.Text))
            //    {
            //        mensagens.Add("[Código Agente] deve ser informado!");
            //    }
            //}

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDaOferta()
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

            foreach (var item in gbDadosPagamento.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
            {
                item.ForeColor = SystemColors.WindowText;
            }

            var mensagens = new List<string>();

            if (_campanha.idTipoDaCampanha == 7)
            {
                return true;
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

            //if (permiteVendaParaNaoTitular == false)
            //{
            //    if (CallplusFormsUtil.FormatarCPF(_oferta.cpf.ToString()) != CallplusFormsUtil.FormatarCPF(txtCpf.Text))
            //    {
            //        lblCpf.ForeColor = Color.Red;
            //        mensagens.Add("[CPF] deve ser o mesmo informado no Mailing!");
            //    }
            //}

            if (string.IsNullOrEmpty(cmbProduto.Text) || cmbProduto.TextoEhSelecione())
            {
                lblProduto.ForeColor = Color.Red;
                mensagens.Add("[Produto] deve ser informado!");
            }

            //if(Convert.ToInt32(VerificaRGMaiorQueZero(txtRg.Text)) < 1)
            //{
            //    txtRg.ForeColor = Color.Red;
            //    mensagens.Add("[RG] inválido!!");
            //}

            if (_oferta.idTipoDeProduto != 2)
            {
                //if (string.IsNullOrEmpty(txtRg.Text))
                //{
                //    lblRg.ForeColor = Color.Red;
                //    mensagens.Add("[RG] deve ser informado!");
                //}

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
                if (!string.IsNullOrEmpty(txtNomeDaMae.Text) && nomeMae.Length <= 1)
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

                //if (string.IsNullOrEmpty(cmbOfertaAparelho.Text) || cmbOfertaAparelho.TextoEhSelecione())
                //{
                //    lblDesejaAparelho.ForeColor = Color.Red;
                //    mensagens.Add("[Deseja Receber Oferta de Aparelho?] deve ser informado!");
                //}
                //else if (cmbOfertaAparelho.Text == "SIM")
                //{
                //    if (string.IsNullOrEmpty(txtUrl.Text))
                //    {
                //        lblUrl.ForeColor = Color.Red;
                //        mensagens.Add("[Url] deve ser preenchido!");
                //    }
                //}

                //if (string.IsNullOrEmpty(cmbFaturaDigital.Text) || cmbFaturaDigital.TextoEhSelecione())
                //{
                //    lblFaturaDigital.ForeColor = Color.Red;
                //    mensagens.Add("[Deseja Fatura Digital] deve ser informada!");
                //}

                if (!string.IsNullOrEmpty(txtEmailFaturaDigital.Text) && !Texto.EmailPosuiFormatoValido(txtEmailFaturaDigital.Text))
                {
                    lblEmailFaturaDigital.ForeColor = Color.Red;
                    mensagens.Add("[E-mail para Fatura] inválido!");
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
                else if (cmbFormaPagamento.Text.Contains("WHATSAPP") && (string.IsNullOrEmpty(txtNumeroFaturaWhatsApp.Text)))
                {
                    lblNumeroFaturaWhatsApp.ForeColor = Color.Red;
                    mensagens.Add("[Número Fatura WhatsApp] deve ser informado!");
                }

                if (FormaDePagamentoEhDebito())
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

                //if (string.IsNullOrEmpty(cmbCodigo21.Text) || cmbCodigo21.TextoEhSelecione())
                //{
                //    lblCodigo21.ForeColor = Color.Red;
                //    mensagens.Add("[Código 21] deve ser informado!");
                //}

                //if (string.IsNullOrEmpty(cmbReceberContrato.Text) || cmbReceberContrato.TextoEhSelecione())
                //{
                //    lblReceberContrato.ForeColor = Color.Red;
                //    mensagens.Add("[Deseja Receber Contrato] deve ser informado!");
                //}

                //if (cmbOndeReceberContrato.TextoEhSelecione() && cmbReceberContrato.Text == "SIM")
                //{
                //    lblOndeReceberContrato.ForeColor = Color.Red;
                //    mensagens.Add("[Onde Receber Contrato] deve ser informado!");
                //}

                //if (string.IsNullOrEmpty(txtEmailFaturaDigital.Text))
                //{
                //    if ((cmbReceberContrato.Text == "SIM" && cmbOndeReceberContrato.Text == "E-MAIL") || cmbFormaPagamento.Text.Contains("E-MAIL"))
                //    {
                //        lblEmailFaturaDigital.ForeColor = Color.Red;
                //        mensagens.Add("[E-mail para Fatura] deve ser informado!");
                //    }
                //}

                //if (cmbReceberContrato.Text == "SIM" && cmbOndeReceberContrato.Text == "CORREIO")
                //{
                //    if (string.IsNullOrEmpty(txtCep.Text))
                //    {
                //        lblCep.ForeColor = Color.Red;
                //        mensagens.Add("[Cep Residencial] deve ser informado!");
                //    }

                //    if (string.IsNullOrEmpty(txtNumero.Text))
                //    {
                //        label15.ForeColor = Color.Red;
                //        mensagens.Add("[Número Residencial] deve ser informado!");
                //    }

                //    if (string.IsNullOrEmpty(txtCidade.Text))
                //    {
                //        label22.ForeColor = Color.Red;
                //        mensagens.Add("[Cidade Residencial] deve ser informado!");
                //    }
                //}
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
                    _historicoOfertaBKO = new HistoricoDaOfertaDoAtendimentoMigracaoBKO();
                }

                _historicoOfertaBKO.idOfertaDoAtendimentoMigracaoBKO = (long)_oferta.id;
                _historicoOfertaBKO.idStatusAuditoria = Convert.ToInt32(cmbStatusAuditoria.SelectedValue);
                _historicoOfertaBKO.protocolo = txtProtocolo.Text;
                _historicoOfertaBKO.autorizacao = txtAutorizacao.Text;
                _historicoOfertaBKO.loginWM = lblLoginValidado.Text;
                _historicoOfertaBKO.codigoAgente = txtCodigo.Text;
                _historicoOfertaBKO.idCriador = AdministracaoMDI._usuario.Id;
                _historicoOfertaBKO.Observacao = txtObservacao.Text;

                _historicoOfertaBKO.id = _ofertaDoAtendimentoService.GravarHistoricoDoAtendimentoClaroMigracaoBKO(_historicoOfertaBKO);
                MessageBox.Show("Auditoria gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _ofertaFoiAtualizada = true;
                Atualizar = true;
                this.Close();
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
                    _oferta.numeroMigrado = Convert.ToInt64(txtNumeroMigrado.Text);

                if (!cmbSexo.TextoEhSelecione())
                    _oferta.Sexo = cmbSexo.Text;

                //if (!cmbFaturaDigital.TextoEhSelecione())
                //    _oferta.faturaDigital = (cmbFaturaDigital.Text == "SIM") ? true : false;

                //if (!cmbOfertaAparelho.TextoEhSelecione())
                //    _oferta.OfertaAparelho = (cmbOfertaAparelho.Text == "SIM") ? true : false;

                // _oferta.Url = txtUrl.Text;

                if (!string.IsNullOrEmpty(txtEmailFaturaDigital.Text))
                    _oferta.emailFaturaDigital = txtEmailFaturaDigital.Text;
                else
                    _oferta.emailFaturaDigital = null;

                if (!cmbDiaDeVencimentoDaFatura.TextoEhSelecione())
                    _oferta.diaVencimento = Convert.ToInt32(cmbDiaDeVencimentoDaFatura.Text.Replace(" - HOJE", ""));

                if (!cmbFormaPagamento.TextoEhSelecione())
                    _oferta.idFormaDePagamento = Convert.ToInt32(cmbFormaPagamento.SelectedValue);

                if (!cmbBanco.TextoEhSelecione())
                    _oferta.idBanco = Convert.ToInt32(cmbBanco.SelectedValue);
                else
                    _oferta.idBanco = null;

                if (!string.IsNullOrEmpty(txtAgencia.Text))
                    _oferta.agencia = txtAgencia.Text;
                else
                    _oferta.agencia = null;

                if (!string.IsNullOrEmpty(txtConta.Text))
                    _oferta.conta = txtConta.Text;
                else
                    _oferta.conta = null;

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
                else
                    _oferta.telefoneResidencial = null;

                if (!string.IsNullOrEmpty(txtTelRecado.Text))
                    _oferta.telefoneRecado = Convert.ToInt64(txtTelRecado.Text);
                else
                    _oferta.telefoneRecado = null;

                if (!cmbEstadoCivil.TextoEhSelecione())
                    _oferta.idEstadoCivil = Convert.ToInt32(cmbEstadoCivil.SelectedValue);
                else
                    _oferta.idEstadoCivil = null;

                if (!cmbProfissao.TextoEhSelecione())
                    _oferta.idProfissao = Convert.ToInt32(cmbProfissao.SelectedValue);
                else
                    _oferta.idProfissao = null;

                if (!cmbFaixaRenda.TextoEhSelecione())
                    _oferta.idFaixaDeRenda = Convert.ToInt32(cmbFaixaRenda.SelectedValue);
                else
                    _oferta.idFaixaDeRenda = null;

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
                else
                    _oferta.pontoDeReferencia = null;

                if (!string.IsNullOrEmpty(txtObservacao.Text))
                    _oferta.observacao = txtObservacao.Text;

                //if (!cmbCodigo21.TextoEhSelecione())
                //    _oferta.codigo21 = (cmbCodigo21.Text == "SIM") ? true : false;

                if (!cmbReceberContrato.TextoEhSelecione())
                    _oferta.receberContrato = (cmbReceberContrato.Text == "SIM") ? true : false;

                if (!cmbOndeReceberContrato.TextoEhSelecione())
                    _oferta.ondeReceberContrato = cmbOndeReceberContrato.Text;

                if (!string.IsNullOrEmpty(txtNumeroFaturaWhatsApp.Text))
                    _oferta.NumeroFaturaWhatsApp = Convert.ToInt64(txtNumeroFaturaWhatsApp.Text);

                _oferta.id = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoClaroMigracaoBKO(_oferta);

                if(_idProdutoInicial != 0 && (_idProdutoInicial != _oferta.idProduto))
                    _oferta.id = _ofertaDoAtendimentoService.GravarAlteracaoDeProdutoMigracaoBKO((long)_oferta.id, _idProdutoInicial, (int)_oferta.idProduto, _usuarioLogado.Id);
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

        private void IniciarEdicaoAvaliacaoDeAtendimento()
        {
            AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("DETALHES DA AVALIAÇÃO", _avaliacaoDeAtendimento.id, null, _avaliacaoDeAtendimento.Nome, (_avaliacaoDeAtendimento.idFeedback != null) ? true : false, (long)_oferta.id);
            f.Iniciar();

            CarregarDadosDaAvaliacaoDeQualidade();
        }

        private void IniciarNovaAvaliacaoDeAtendimento()
        {
            int _tipos = (_oferta.id > 0) ? 1 : 2;

            AvaliacaoDeAtendimentoForm f = new AvaliacaoDeAtendimentoForm("NOVA AVALIAÇÃO", 0, _oferta.idCampanha, _oferta.idOperador.Value, _oferta.idSupervisor.Value, _oferta.dataRegistroOferta.ToString("yyyy-MM-dd"), DateTime.Now.ToString(("yyyy-MM-dd") + " 23:59:59"), _oferta.idStatusDaOferta.Value.ToString(), _tipos, _oferta.id.Value);
            f.Iniciar();

            CarregarDadosDaAvaliacaoDeQualidade();
        }

        private void ConfiguracoesDoCampoFormaPagamento()
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

        private bool FormaDePagamentoEhDebito()
        {
            if (cmbFormaPagamento.Text == "DÉBITO EM CONTA" || cmbFormaPagamento.Text.Contains("DCC") || cmbFormaPagamento.Text.Contains("DEBITO EM CONTA"))
                return true;
            else
                return false;
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
                }
                else
                {
                    MessageBox.Show("Você não tem permissão para executar essa ação!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void AuditoriaOferta_MigracaoClaroForm_Load(object sender, EventArgs e)
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

        private void AuditoriaOferta_MigracaoClaroForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void AuditoriaOferta_MigracaoClaroForm_KeyPress(object sender, KeyPressEventArgs e)
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

            ConfiguracoesDoCampoFormaPagamento();
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

        private void cmbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBanco.ForeColor = SystemColors.WindowText;
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
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //lblRg.ForeColor = SystemColors.WindowText;
            //e.Handled = char.IsWhiteSpace(e.KeyChar);
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

        private void btnAvaliação_Click(object sender, EventArgs e)
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

        private void txtNumeroFaturaWhatsApp_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNumeroFaturaWhatsApp.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtNumeroFaturaWhatsApp_Leave(object sender, EventArgs e)
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
                lblNumeroFaturaWhatsApp.ForeColor = SystemColors.WindowText;
            }
        }

        #endregion EVENTOS 

        private void txtRg_Leave(object sender, EventArgs e)
        {                      
            if (!string.IsNullOrEmpty(txtRg.Text) && Convert.ToInt32(VerificaRGMaiorQueZero(txtRg.Text)) == 0)
            {
                MessageBox.Show("[RG] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRg.Focus();
                return;
            }

            char[] rg = txtRg.Text.ToCharArray();
            string rgCorreto = string.Empty;
            int cont = 0;
            char last = '_';
            for (int i = 0; i < rg.Length; i++)
            {
                if (char.IsNumber(rg[i]) || char.IsLetter(rg[i]))
                {
                    if (rg[i] == last)
                        cont++;
                    else
                        cont = 0;

                    if (cont < 4)
                        rgCorreto += rg[i];

                    last = rg[i];
                }
            }
            txtRg.Text = rgCorreto.Replace('Á', 'A').Replace('É', 'E').Replace('Í', 'I').Replace('Ó', 'O').Replace('Ú', 'U')
                .Replace('À', 'A').Replace('È', 'E').Replace('Ì', 'I').Replace('Ò', 'O').Replace('Ù', 'U')
                .Replace('Â', 'A').Replace('Ê', 'E').Replace('Î', 'I').Replace('Ô', 'O').Replace('Û', 'U')
                .Replace('Ã', 'A').Replace('Õ', 'O');
            
        }
        private string VerificaRGMaiorQueZero(string numRG)
        {
            int soma = 0;
            for (int i = 0; i < numRG.Length; i++)
            {               
                if((numRG).Substring(i, 1) == "1" ||
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
            return numRG ;
        }

        private void txtNumeroMigrado_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroMigrado.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroMigrado.Text))
                {
                    lblNumeroMigrado.ForeColor = Color.Red;
                    txtNumeroMigrado.Focus();
                    MessageBox.Show("[Número Migrado] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroMigrado.ForeColor = SystemColors.WindowText;
            }
        }

        private void cmbDesejaAparelho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOfertaAparelho.Text == "SIM")
                txtUrl.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            else
                txtUrl.Resetar(habilitar: false, limparTexto: true, readOnly: false);
        }

        private void btnLiberarProduto_Click(object sender, EventArgs e)
        {
            try
            {
                LiberarAlteracaoDeProduto();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível liberar alteração de produto!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
