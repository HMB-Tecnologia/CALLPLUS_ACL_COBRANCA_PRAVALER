using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Environment;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
	public partial class OfertaPortabilidadeMPForm : Form
    {
        public OfertaPortabilidadeMPForm(Usuario usuario, long idOferta, Prospect prospect, bool bloqueioStatus, bool fecharAoGravar,
            ContainerDeLayoutDeCamposDinamicos camposDinamicos, Discador discadorConectado, string loginHuawei, int? idStatusOferta = null, int? idCampanha = null, bool edicao = true)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _atendimentoService = new AtendimentoService();
            _campanhaService = new CampanhaService();
            _checklistService = new ChecklistService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _produtoService = new ProdutoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeOfertaService();
            _planoService = new PlanoPorOperadoraParaComparacaoService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _correioService = new RetornoDeCepService();
            _enderecoService = new EnderecoService();

            _discadorConectado = discadorConectado;
            _usuario = usuario;
            _prospect = prospect;
            _idStatusOferta = idStatusOferta;
            _bloqueioStatus = bloqueioStatus;
            _fecharAoGravar = fecharAoGravar;
            _permiteEditar = edicao;
            _camposDinamicos = camposDinamicos;
            _loginService = new LoginService();

            if (idCampanha != null)
            {
                _campanhaAtual = _campanhaService.RetornarCampanha((int)idCampanha);
                _campanhaEhExpress = _campanhaAtual.HabilitaCepExpress;
            }

            _loginHuawei = loginHuawei;

            _oferta = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoMPPortabilidade(idOferta);

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly AtendimentoService _atendimentoService;
        private readonly CampanhaService _campanhaService;
        private readonly ChecklistService _checklistService;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly ProdutoService _produtoService;
        private readonly ProspectService _prospectService;
        private readonly StatusDeOfertaService _statusDeOfertaService;
        private readonly PermissaoService _permissaoService;
        private readonly PlanoPorOperadoraParaComparacaoService _planoService;
        private Dictionary<string, bool> mapDeReproducoesGravacoes;
        private readonly RetornoDeCepService _correioService;
        private readonly EnderecoService _enderecoService;
        private readonly bool _campanhaEhExpress;
        private Usuario _usuario;
        private Prospect _prospect;
        private OfertaDoAtendimentoMPPortabilidade _oferta;
        private ContainerDeLayoutDeCamposDinamicos _camposDinamicos;
        public delegate void PararTempoHandler(int? idUsuarioAprovacao);
        public event PararTempoHandler PararTempoEvent;
        private OfertaDoAtendimentoMPPortabilidade _preVenda;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;

        private int? _idStatusOferta;
        private bool _bloqueioStatus;
        private bool _fecharAoGravar;
        private bool _permiteEditar;
        private bool _checklistAplicado;

        private string _nomeProduto;
        private bool _filtraPorFaixaDeRecarga;
        private Campanha _campanhaAtual;
        private readonly Discador _discadorConectado;
        bool vendaSegundoProduto = false;
        private bool _venda;
        private bool _cepPossuiElegibilidade;
        private string _loginHuawei;
        private int _numeroCaracteresAgenciaBancaria = 0;
        private int _numeroCaracteresContaBancaria = 0;
        private readonly LoginService _loginService;
        private string _maquinaUsuario = "";
        private string _enderecoIP = "";
        private string _modulo = "OPE";
        private string _release = "01.000";

        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            tcOferta.TabPages.Remove(tpInativo);
            tcOferta.TabPages.Remove(tabPage1);
            tcOferta.TabPages.Remove(tcOferta_tpDadosDoProduto);
            tcEndereco.TabPages.Remove(tcEndereco_tp_EnderecoInstalacao);
            tcEndereco.TabPages.Remove(tcEndereco_tpEnderecoEntrega);
            tcDadosPessoais.TabPages.Remove(tpPessoaJuridica);

            Atualizar = false;
            tsOferta_cmbStatusOferta.Enabled = false;
            lIdade.Text = string.Empty;
            _nomeProduto = string.Empty;
            _filtraPorFaixaDeRecarga = false;

            cmbPortabilidade.ResetarComSelecione(habilitar: true);
            cmbPortabilidade2.ResetarComSelecione(habilitar: false);
            cmbPortabilidade3.ResetarComSelecione(habilitar: false);
            cmbPortabilidade4.ResetarComSelecione(habilitar: false);
            cmbPortabilidade5.ResetarComSelecione(habilitar: false);


            cmbNumeroProvisorio.ResetarComSelecione(habilitar: true);
            cmbSexo.ResetarComSelecione(habilitar: true);
            cmbPossuiEmail.ResetarComSelecione(habilitar: true);
            CarregarProduto();

            if (_campanhaAtual.TipoAuditoria == TipoDeAuditoria.ONLINE)
            {
                cmbStatusAuditoria.Enabled = true;
                cmbStatusAuditoria.Visible = true;

                lblStatusAuditoria.Visible = true;
                
                CarregarStatusDeAuditoria();
            }
            else
            {
                cmbStatusAuditoria.Visible = false;
                cmbStatusAuditoria.Enabled = false;
                lblStatusAuditoria.Visible = false;

            }
            
            CarregarBanco();
            //CarregarDiaDeVencimentoDaFatura();
            //CarregarOperadora();
            CarregarFormaDePagamento();
            CarregarTipoDeStatusDeOferta();
            CarregarDadosIniciais();
                        
            if (_idStatusOferta != null && _idStatusOferta > 0)
            {
                ConfigurarStatusDaOferta(_idStatusOferta.Value);
            }

            CarregarControleDeEdicao();

            //ExibirValidacaoTravaUfPorDDD(txtUf, lblUf, false);
        }

        private void CarregarStatusDeAuditoria()
        {
            IEnumerable<StatusDeAuditoria> statusDeAuditoria = _statusDeAuditoriaService.OperadorListar(true);

            cmbStatusAuditoria.PreencherComSelecione(statusDeAuditoria, x => x.Id, x => x.Nome);
        }

        private void LimparEndereco()
        {
            bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
            bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;

            if (residencial)
            {
                txtCep.Clear();
                txtLogradouro.Clear();
                txtNumero.Clear();
                txtComplemento.Clear();
                txtBairro.Clear();
                txtCidade.Clear();
                txtUf.Clear();
                txtPontoReferencia.Clear();

                //btnSelecionarEndereco.Focus();
            }

            if (entrega)
            {
                txtCepEntrega.Clear();
                txtLogradouroEntrega.Clear();
                txtNumeroEntrega.Clear();
                txtComplementoEntrega.Clear();
                txtBairroEntrega.Clear();
                txtCidadeEntrega.Clear();
                txtUfEntrega.Clear();
                txtPontoReferenciaEntrega.Clear();

            }
          
        }

        private void Autorizar()
        {
            bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
            bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;

            if (residencial)
            {
                if (PodeAcessar())
                {
                    pnlAutorizar.Visible = false;
                    txtSupervisor.Clear();
                    txtSenha.Clear();
                }
                else
                {
                    txtSupervisor.Clear();
                    txtSenha.Clear();
                }
            }

            if (entrega)
            {
                if (PodeAcessar())
                {
                    pnlAutorizarEntrega.Visible = false;
                    txtSupervisorEnt.Clear();
                    txtSenhaEnt.Clear();
                }
                else
                {
                    txtSupervisorEnt.Clear();
                    txtSenhaEnt.Clear();
                }
            }
            

        }

        private bool PodeAcessar()
        {
            var mensagens = new List<string>();

            string loginSupervisor = "";
            string senhaSupervisor = "";

            bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
            bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;

            if (residencial)
            {
                loginSupervisor = txtSupervisor.Text.Trim();
                senhaSupervisor = txtSenha.Text.Trim();

            }

            if (entrega)
            {
                loginSupervisor = txtSupervisorEnt.Text.Trim();
                senhaSupervisor = txtSenhaEnt.Text.Trim();
            }
            
            _maquinaUsuario = ConfiguracaoDeAmbiente.HostName;
            _enderecoIP = ConfiguracaoDeAmbiente.RetornarEnderecoIP();

            if (loginSupervisor.Trim() == "" || senhaSupervisor.Trim() == "")
            {
                mensagens.Add("Informe o login e senha!");
            }
            else
            {
                mensagens = _loginService.VerificarSeSupervidorPodeAcessarSistema(loginSupervisor, senhaSupervisor, _maquinaUsuario, _enderecoIP, _modulo, _release);
               
            }

            if (mensagens.Any())
            {
                ExibirMensagens(mensagens);
            }
            else
            {
                if (SenhaExpirada(loginSupervisor, senhaSupervisor))
                {
                    MessageBox.Show("Sua senha expirou!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //var resetarForm = new ResetarSenhaForm();
                    //resetarForm.SolicitarAlteracaoDeSenha(senhaSupervisor);

                    if (residencial)
                    {
                        txtSupervisor.Text = string.Empty;
                        txtSenha.Text = string.Empty;
                    }

                    if (entrega)
                    {
                        txtSenhaEnt.Text = string.Empty;
                        txtSupervisorEnt.Text = string.Empty;
                    }
                   
                    return false;
                }
            }
            return !mensagens.Any();
        }

        private void ExibirMensagens(List<string> mensagens)
        {
            if (mensagens.Any())
            {
                var msgFinal = string.Join("\n", mensagens);
                MessageBox.Show(msgFinal, "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool SenhaExpirada(string login, string senha)
        {
            return _loginService.VerificarSenhaExpirada(login, senha);
        }

        private void CarregarControleDeEdicao()
        {
            if (!_permiteEditar)
            {
                tsOferta.Enabled = false;
                txtObservacao.Enabled = false;
                txtDataNascimento.ReadOnly = true;
                btnSelecionarEndereco.Enabled = false;
                btnPlayerDataVencimentoIndicacao.Enabled = false;
   
                foreach (var item in gbDadosPessoais.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbDadosPessoais.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbEnderecoResidencial.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbEnderecoResidencial.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbDadosOferta.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbDadosOferta.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                    
                }

                foreach (var item in gbDadosOferta.Controls.OfType<Button>().Where(x => x.Name.Contains("btn")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbDadosPagamento.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbDadosPagamento.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbLiberarVendaCasada.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbLiberarVendaCasada.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbLiberarVendaCasada.Controls.OfType<Button>().Where(x => x.Name.Contains("btn")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbDadosPlanoAtual.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbDadosPlanoAtual.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }
            }
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

        private void CarregarDadosIniciais()
        {
            long idAtendimento = _atendimentoService.VerificarSeExisteVendaPendente(_campanhaAtual.Id, _prospect.Id);

            if (idAtendimento > 0 && _usuario.IdPerfil == 2)
                _preVenda = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoPreVendaPortabilidade(idAtendimento);

            if (_preVenda != null)
            {
                txtNome.Text = _preVenda.Nome;
            }

            if (!string.IsNullOrEmpty(_prospect.Campo002))
                txtCustId.Text = _prospect.Campo002;

            if (!string.IsNullOrEmpty(_prospect.Campo004))
                txtDddTelResidencial.Text = _prospect.Campo004;

            if (!string.IsNullOrEmpty(_prospect.Campo005))
                txtTelResidencial.Text = _prospect.Campo005;

            if (!string.IsNullOrEmpty(_prospect.Campo006))
                txtDddCel.Text = _prospect.Campo006;

            if (!string.IsNullOrEmpty(_prospect.Telefone02.ToString()))
                txtTelCelular.Text = _prospect.Telefone02.ToString();

            if (_preVenda != null)
            {
                if (_preVenda.TelefoneCelular != null)
                    txtTelCelular.Text = _preVenda.TelefoneCelular.ToString();
            }
            else
            {
                if (_oferta.TelefoneCelular != null)
                    txtTelCelular.Text = _oferta.TelefoneCelular.ToString();
                else
                {
                    if (Texto.TelefoneCelularPossuiFormatoValido(_prospect.Telefone01.ToString()))
                        txtTelCelular.Text = _prospect.Telefone01.ToString();
                    else if (Texto.TelefoneFixoPossuiFormatoValido(_prospect.Telefone01.ToString()))
                        txtTelResidencial.Text = _prospect.Telefone01.ToString();
                }
            }

            if (_preVenda != null)
            {
                if (_preVenda.TelefoneResidencial != null)
                    txtTelResidencial.Text = _preVenda.TelefoneResidencial.ToString();
            }
            else
            {
                if (_oferta.TelefoneResidencial != null)
                    txtTelResidencial.Text = _oferta.TelefoneResidencial.ToString();
            }

            CarregarDadosEndereco();

            if (_preVenda != null)
            {
                if (_preVenda.Observacao != null)
                    txtObservacao.Text = _preVenda.Observacao;
            }
            else
            {
                if (_oferta.Observacao != null)
                    txtObservacao.Text = _oferta.Observacao;
            }

            if (_preVenda != null)
            {
                if (_preVenda.IdBanco != null)
                    cmbBanco.SelectedValue = _preVenda.IdBanco.ToString();
            }
            else
            {
                if (_oferta.IdBanco != null)
                    cmbBanco.SelectedValue = _oferta.IdBanco.ToString();
            }
        }

        private void CarregarDadosEndereco()
        {
            if (_preVenda != null)
            {
                if (_preVenda.Cep != null)
                {
                    string cep = _preVenda.Cep.ToString();

                    long cepNumerico = 0;

                    if (string.IsNullOrEmpty(cep) == false && (cep.Length == 7 || cep.Length == 8) && long.TryParse(cep, out cepNumerico))
                    {
                        if (cep.Length == 7)
                        {
                            cep = $"0{cep}";
                        }
                    }

                    txtCep.Text = cep;
                }

                if (_preVenda.Numero != null)
                    txtNumero.Text = _preVenda.Numero.ToString();

                if (_preVenda.PontoDeReferencia != null)
                    txtPontoReferencia.Text = _preVenda.PontoDeReferencia.ToString();

                if (_preVenda.Complemento != null)
                    txtComplemento.Text = _preVenda.Complemento.ToString();

                if (_preVenda.Uf != null)
                    txtUf.Text = _preVenda.Uf.ToString();

                if (_preVenda.Cidade != null)
                    txtCidade.Text = _preVenda.Cidade.ToString();

                if (_preVenda.Logradouro != null)
                    txtLogradouro.Text = _preVenda.Logradouro.ToString();

                if (_preVenda.Bairro != null)
                    txtBairro.Text = _preVenda.Bairro.ToString();            
            }
            else
            {
                if (_oferta.Cep == null)
                {
                    //var cep = _prospect.Campo014;
                    //long cepNumerico;

                    //if (string.IsNullOrEmpty(cep) == false && (cep.Length == 7 || cep.Length == 8) && long.TryParse(cep, out cepNumerico))
                    //{
                    //    if (cep.Length == 7)
                    //    {
                    //        cep = $"0{cep}";
                    //    }

                    //    CarregarEnderecoTabDadosDaVenda(cep);

                    //    //if (_oferta.cepEntrega == null && cep != null)
                    //    //{
                    //    //    if (_campanhaEhExpress)
                    //    //        VerificarCepElegivel(cep, false);
                    //    //}
                    //}
                }
                else
                {
                    if (_oferta.Cep != null)
                    {
                        txtCep.Text = _oferta.Cep.ToString();

                        //if (_campanhaEhExpress)
                        //    VerificarCepElegivel(txtCep.Text, false);
                    }

                    if (_oferta.Numero != null)
                        txtNumero.Text = _oferta.Numero.ToString();

                    if (_oferta.PontoDeReferencia != null)
                        txtPontoReferencia.Text = _oferta.PontoDeReferencia.ToString();

                    if (_oferta.Complemento != null)
                        txtComplemento.Text = _oferta.Complemento.ToString();

                    if (_oferta.Uf != null)
                        txtUf.Text = _oferta.Uf.ToString();

                    if (_oferta.Cidade != null)
                        txtCidade.Text = _oferta.Cidade.ToString();

                    if (_oferta.Logradouro != null)
                        txtLogradouro.Text = _oferta.Logradouro.ToString();

                    if (_oferta.Bairro != null)
                        txtBairro.Text = _oferta.Bairro.ToString();
                }
            }
        }

        private bool CarregarEnderecoTabDadosDaVenda(string cep)
        {
            try
            {
                int tipodepesquisa = 1;
                IEnumerable<CepCorreios> enderecos = _correioService.RetornarEndereco(cep, null, null, null, null, tipodepesquisa);

                var endereco = enderecos.FirstOrDefault();

                if (endereco != null)
                {
                    txtCep.Text = endereco.Cep;
                    txtLogradouro.Text = endereco.Logradouro;
                    txtBairro.Text = endereco.Bairro;
                    txtCidade.Text = endereco.Cidade;
                    txtUf.Text = endereco.UF;

                    return true;
                }
                else
                {
                    MessageBox.Show(
                    $"Não foi possível carregar o CEP do cliente.\nUtilize a pesquisa ou cadatro manual.\n", "Mensagem do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Ocorreu um erro ao tentar carregar o CEP do cliente.\nUtilize a pesquisa ou cadatro manual.\n Erro::{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void CarregarBanco()
        {
            IEnumerable<Banco> retorno = _campanhaService.ListarBancoDaCampanha(_prospect.IdCampanha, ativo: true);
            cmbBanco.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Codigo} - {x.Nome}");
        }

        //TODO ajustado para levar o Parâmetro idCampanha.
        private void CarregarDiaDeVencimentoDaFatura()
        {
            IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _atendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveis(_campanhaAtual.Id);
            cmbDiaDeVencimentoDaFatura.PreencherComSelecione(configuracaoDatas, x => x.Fechamento, x => x.Vencimento);
        }

        private void CarregarFaixaDeRenda()
        {
            IEnumerable<FaixaDeRenda> retorno = _prospectService.ListarFaixaDeRenda(ativo: true);
            cmbFaixaRenda.PreencherComSelecione(retorno, x => x.id, x => x.nome);
        }

        private void CarregarEstadoCivil()
        {
            IEnumerable<EstadoCivil> retorno = _prospectService.ListarEstadoCivil(ativo: true);
            cmbEstadoCivil.PreencherComSelecione(retorno, x => x.id, x => x.nome);
        }

        private void CarregarFormaDePagamento()
        {
            IEnumerable<FormaDePagamento> retorno = _campanhaService.ListarFormasDePagamentoDaCampanha(_prospect.IdCampanha, ativo: true);
            cmbFormaPagamento.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarProfissao()
        {
            IEnumerable<Profissao> retorno = _prospectService.ListarProfissao(ativo: true);
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
            int idTipoDeProduto = 4;

            bool ativo = true;
            bool? ativoBko = null;

            IEnumerable<ProdutoDaOfertaDto> produtos = null;

            //produtos = _produtoService.ListarProdutoDaOferta(_oferta.IdAtendimento, ativo, ativoBko).Where(x => x.idTipo == idTipoDeProduto).Distinct();
            produtos = _produtoService.ListarProdutoDaOferta(_oferta.IdAtendimento, ativo, ativoBko).Distinct();

            cmbProduto.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
            //cmbProduto2.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
            //cmbProduto3.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
            //cmbProduto4.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
            //cmbProduto5.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
        }

        private void CarregarStatusDeOferta(object tipo)
        {
            tsOferta_btnChecklist.Enabled = false;
            int idTipo = -1;

            if (tsOferta_cmbTipoStatusOferta.Text.ToUpper() == "ACEITE")
                tsOferta_btnChecklist.Enabled = true;

            int.TryParse(tipo.ToString(), out idTipo);

            IEnumerable<StatusDeOferta> statusDeOferta = _statusDeOfertaService.ListarStatusDeOferta(_prospect.IdCampanha, idTipo, true);

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

        private string RetornarUfDeAcordoComDdd()
        {
            string telefone = txtTelCelular.Text;
            string telefoneFixo = txtTelResidencial.Text;

            string DDD = string.Empty;

            if (telefone.Length > 2)
                DDD = (telefone).Substring(0, 2);
            else if (telefoneFixo.Length > 2)
                DDD = (telefoneFixo).Substring(0, 2);
			
            string uf = string.Empty;

			if (int.TryParse(DDD, out int ddd))
				uf = _enderecoService.RetornarUf(ddd, _campanhaAtual.Id);

			return uf;
        }

        private bool AtendeRegrasDeGravacao(bool considerarCheckList)
        {
            var mensagens = new List<string>();

            //VendasParaNaoTitular();

            ControlesDeEdicao();

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
                if (rbFisica.Checked)
                {
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
                    if (nome.Length <= 1 && !string.IsNullOrEmpty(txtNome.Text))
                    {
                        lblNome.ForeColor = Color.Red;
                        mensagens.Add("[Nome] inválido!");
                    }             

                    if (string.IsNullOrEmpty(txtTelCelular.Text))
                    {
                        lblTelCelular.ForeColor = Color.Red;
                        mensagens.Add("[Telefone Celular] deve ser informado!");
                    }

                    if (string.IsNullOrEmpty(txtTelCelular.Text))
                    {
                        lblTelCelular.ForeColor = Color.Red;
                        mensagens.Add("[Telefone Celular] inválido!");
                    }                     

                    if (cmbPossuiEmail.Text.Equals("SIM"))
					{
						if (string.IsNullOrEmpty(txtEmail.Text))
						{
                            lblEmailCliente.ForeColor = Color.Red;
                            mensagens.Add("[E-mail] inválido!");
                        }

					    if (!string.IsNullOrEmpty(txtEmail.Text) && !Texto.EmailPosuiFormatoValido(txtEmail.Text))
					    {
						    lblEmailCliente.ForeColor = Color.Red;
						    mensagens.Add("[E-mail] inválido!");
					    }
					}					
				}
                               
                if (_campanhaAtual.TipoAuditoria == TipoDeAuditoria.ONLINE)
                {
                    if (cmbStatusAuditoria.TextoEhSelecione())
                    {
                        lblStatusAuditoria.ForeColor = Color.Red;
                        mensagens.Add("[Status De Auditoria] deve ser informado!");
                    }
                    else
                    {
                        lblStatusAuditoria.ForeColor = SystemColors.WindowText;
                    }
                }         

                if (considerarCheckList)
                {
                    int idCampanha = _prospect.IdCampanha;
                    int idProduto = Convert.ToInt32(cmbProduto.SelectedValue);
                    string ddd = "";

                    if (txtNumeroPortado.Text.Length > 2)
                        ddd = txtNumeroPortado.Text.Substring(0, 2);
                    else if (_prospect.Telefone01.ToString().Length > 2)
                        ddd = _prospect.Telefone01.ToString().Substring(0, 2);

                    IEnumerable<Dominio.Entidades.Checklist> retorno = _checklistService.Listar(idCampanha, idProduto, int.Parse(ddd), true);

                    if (retorno.Count() > 0)
                    {
                        if (!_checklistAplicado)
                        {
                            mensagens.Add("[Checklist] deve ser aplicado para gravar o aceite!");
                        }
                    }
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void Gravar(bool venda)
        {
            if (venda)
            {
                if (AtendeRegrasDeGravacao(false))
                {
                    _venda = venda;

                    DadosAhGravar(venda);
                }
            }
            else
            {
                DadosAhGravar(venda);
            }
        }

        private void ValidarTravaUF(List<string> mensagens, TextBox txtUF, Label lblUF)
        {
            string uf = RetornarUfDeAcordoComDdd();
            string ufDoCLiente = txtUF.Text;

            bool mesmoUF = string.IsNullOrEmpty(ufDoCLiente) ? false : uf.Contains(ufDoCLiente);

            if (!mesmoUF) //if (uf != ufDoCLiente)
            {
                lblUF.ForeColor = Color.Red;

                string ddd = string.Empty;

                if (txtTelCelular.Text.Length > 2)
                    ddd = (txtTelCelular.Text).Substring(0, 2);
                //TODO Retirar trava de DDD
                //if (!string.IsNullOrEmpty(ufDoCLiente))
                //{
                //    mensagens.Add("[DDD] divergente do [UF] do Cliente!");

                //    lblMensagem.Visible = true;
                //    lblMensagem.Text = "O UF do DDD Cliente (" + uf + "-" + ddd + ") é divergente do UF endereço (" + ufDoCLiente + ").";
                //    lblMensagem.ForeColor = Color.Red;
                //}
                //else
                //    ResetarCampos(lblUF);
            }
            else
                ResetarCampos(lblUF);
        }

        private void ResetarCampos(Label lblUF)
        {
            lblUF.ForeColor = SystemColors.WindowText;
            lblMensagem.Text = string.Empty;
            lblMensagem.Visible = false;
        }

        private bool FormaDePagamentoEhDebito()
        {
            if (cmbFormaPagamento.Text == "DÉBITO EM CONTA" || cmbFormaPagamento.Text.Contains("DCC") || cmbFormaPagamento.Text.Contains("DEBITO EM CONTA"))
                return true;
            else
                return false;
        }

        private void VendasParaNaoTitular()
        {
            bool permiteVendaParaNaoTitular = true; //_prospect.IdCampanha == 8;

            bool quantidadeVendaParaNaoTitularExcedida = false;

            if (!permiteVendaParaNaoTitular)
            {
                IEnumerable<PermissaoDoUsuario> permissoesDoUsuario = _permissaoService.PermissoesDoUsuarioListar(null, _usuario.Id, _prospect.IdCampanha);

                foreach (PermissaoDoUsuario permissaoUsuario in permissoesDoUsuario)
                {
                    if (permissaoUsuario == null || permissaoUsuario.Permissao == null) continue;
                    TipoPermissao tipoPermissao = permissaoUsuario.Permissao.TipoPermissao;

                    if (tipoPermissao == TipoPermissao.VendaParaNaoTitular)
                        permiteVendaParaNaoTitular = true;
                }
            }
        }

        private void ControlesDeEdicao()
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

            foreach (var item in gbDadosPlanoAtual.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
            {
                item.ForeColor = SystemColors.WindowText;
            }
        }

        private void DadosAhGravar(bool venda)
        {
            _oferta.IdStatusDaOferta = venda ? Convert.ToInt32(tsOferta_cmbStatusOferta.ComboBox.SelectedValue) : 38;

            if (rbFisica.Checked)
                _oferta.TipoPessoa = "FÍSICA"; 

            //PESSOA FÍSICA
            if (!string.IsNullOrEmpty(txtCustId.Text))
				_oferta.Cust_Id = txtCustId.Text;

            if (!string.IsNullOrEmpty(txtNome.Text))
                _oferta.Nome = txtNome.Text;

            if (!string.IsNullOrEmpty(txtDddTelResidencial.Text))
                _oferta.DddTel = int.Parse(txtDddTelResidencial.Text);
            
            if (!string.IsNullOrEmpty(txtTelResidencial.Text))
                _oferta.TelefoneResidencial = Convert.ToInt64(txtTelResidencial.Text);

            if (!string.IsNullOrEmpty(txtDddCel.Text))
                _oferta.DddCel = int.Parse(txtDddCel.Text);           

            if (!string.IsNullOrEmpty(txtTelCelular.Text))
                _oferta.TelefoneCelular = Convert.ToInt64(txtTelCelular.Text);

            if (!cmbBanco.TextoEhSelecione())
                _oferta.IdBanco = Convert.ToInt32(cmbBanco.SelectedValue);

            if (!string.IsNullOrEmpty(txtEmail.Text))
                _oferta.Email = txtEmail.Text;

            //ENDEREÇO RESIDENCIAL
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

            if (venda)
                _oferta.processado = true;
            else
                _oferta.processado = false;

            _oferta.Id = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoMPPortabilidade(_oferta);

            if (venda)
                MessageBox.Show("Oferta gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Atualizar = true;

            if (_fecharAoGravar)
            {
                this.Close();
            }
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
                telefone = _prospect.Telefone01.ToString();

            EnderecoForm f = new EnderecoForm(_usuario, _prospect, _campanhaAtual, telefone);

            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();

            PreencherCampoDoEndereco(f.EnderecoSelecionado);
        }

        private void CarregarChecklist()
        {
            bool auditoriaEmPe = false;
            
            _checklistAplicado = true;

            if (_campanhaAtual.TipoAuditoria == TipoDeAuditoria.ONLINE)
            {
                auditoriaEmPe = true;
            }

            if (!AtendeRegrasDeGravacao(auditoriaEmPe)) return;         

            int idCampanha = _prospect.IdCampanha;
            int idProduto = Convert.ToInt32(cmbProduto.SelectedValue);
            string ddd = "";

            if (txtNumeroPortado.Text.Length > 2)
                ddd = txtNumeroPortado.Text.Substring(0, 2);
            else if(_prospect.Telefone01.ToString().Length > 2)
                ddd = _prospect.Telefone01.ToString().Substring(0, 2);

            IEnumerable<Dominio.Entidades.Checklist> retorno = _checklistService.Listar(idCampanha, idProduto, int.Parse(ddd), true);

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

                    gbDadosOferta.Enabled = false;
                    gbDadosPagamento.Enabled = false;
                    gbDadosPessoais.Enabled = false;
                    gbEnderecoResidencial.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Nenhum checklist disponível para a Oferta.\nPode prosseguir com a gravação!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PreencherCampoDoEndereco(EnderecoDoProspect endereco)
        {
            foreach (Control item in gbEnderecoResidencial.Controls.OfType<TextBox>())
            {
                item.ResetText();
            }

            bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
            bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;
            bool instalacao = tcEndereco.SelectedTab == tcEndereco_tp_EnderecoInstalacao ? true : false;

            if (endereco != null)
            {
                //TODO - mudando a regra de pegar o endereço - Rei Almeida
                /*if (residencial) { txtCep.Text = endereco.Cep; } else { txtCepEntrega.Text = endereco.Cep; };
                if (residencial) { txtLogradouro.Text = endereco.Logradouro; } else { txtLogradouroEntrega.Text = endereco.Logradouro; };
                if (residencial) { txtNumero.Text = endereco.Numero; } else { txtNumeroEntrega.Text = endereco.Numero; };
                if (residencial) { txtComplemento.Text = endereco.Complemento; } else { txtComplementoEntrega.Text = endereco.Complemento; };
                if (residencial) { txtBairro.Text = endereco.Bairro; } else { txtBairroEntrega.Text = endereco.Bairro; };
                if (residencial) { txtCidade.Text = endereco.Cidade; } else { txtCidadeEntrega.Text = endereco.Cidade; };
                if (residencial) { txtUf.Text = endereco.Uf; } else { txtUfEntrega.Text = endereco.Uf; };
                if (residencial) { txtPontoReferencia.Text = endereco.PontoDeReferencia; } else { txtPontoReferenciaEntrega.Text = endereco.PontoDeReferencia; };
                */

                //TODO - Mecanismo de carregar endereço - Rei Almeida

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

                    VerificarCepElegivel(endereco.Cep, true);
                }

                if (instalacao)
                {
                    txtCepInstalacao.Text = endereco.Cep;
                    txtLogradouroInstalacao.Text = endereco.Logradouro;
                    txtNumeroInstalacao.Text = endereco.Numero;
                    txtComplementoInstalacao.Text = endereco.Complemento;
                    txtBairroInstalacao.Text = endereco.Bairro;
                    txtCidadeInstalacao.Text = endereco.Cidade;
                    txtUF_Instalacao.Text = endereco.Uf;
                    txtPontoReferenciaInstal.Text = endereco.PontoDeReferencia;
                }

                //if (_campanhaEhExpress)
                //{
                //    //if(residencial && (string.IsNullOrEmpty(txtCepEntrega.Text)))
                //    //    VerificarCepElegivel(txtCep.Text, false);
                //    //else
                //    //    VerificarCepElegivel(txtCepEntrega.Text, true);
                //}
            }

            //if (_campanhaAtual.idTipoDaCampanha == 2 || _campanhaAtual.idTipoDaCampanha == 3)
            //{

            //    if (residencial)
            //        ExibirValidacaoTravaUfPorDDD(txtUf, lblUf, true);
            //    else if (_campanhaEhExpress)
            //        ExibirValidacaoTravaUfPorDDD(txtUfEntrega, lblUfEntrega, true);
            //}
        }

        private void ExibirValidacaoTravaUfPorDDD(TextBox txtUF, Label lblUF, bool? exibir = null)
        {
            var mensagens = new List<string>();
            ValidarTravaUF(mensagens, txtUF, lblUF);

            if (Convert.ToBoolean(exibir))
                CallplusFormsUtil.ExibirMensagens(mensagens);
        }

        private bool AlterarValidacaoDeReproducaoDeGravacao(string chaveProduto, bool validado)
        {
            if (mapDeReproducoesGravacoes != null && mapDeReproducoesGravacoes.ContainsKey(chaveProduto))
            {
                mapDeReproducoesGravacoes[chaveProduto] = validado;
                return true;
            }
            return false;
        }

        private void LiberarVendaCasada()
        {
            if (txtLoginVendaCasada.Text == "")
            {
                MessageBox.Show("Para liberar Venda Casada. Informe um Login de Supervisor!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtSenhaVendaCasada.Text == "")
            {
                MessageBox.Show("Para liberar Venda Casada. Informe uma Senha de Supervisor!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtLoginVendaCasada.Text != "" && txtSenhaVendaCasada.Text != "")
            {
                Usuario supervisor = _atendimentoService.ValidarSupervisor(_usuario.IdSupervisor, txtLoginVendaCasada.Text, txtSenhaVendaCasada.Text).FirstOrDefault();

                if (supervisor != null)
                {
                    txtLoginVendaCasada.Text = "";
                    txtSenhaVendaCasada.Text = "";
                    cmbProduto2.Enabled = true;
                    btnPlayerProduto2.Enabled = true;
                    vendaSegundoProduto = true;
                }
                else
                {
                    MessageBox.Show("Você não tem permissão para executar essa ação!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConfiguracoesDeSelecaoDeBanco()
        {
            lblBanco.ForeColor = SystemColors.WindowText;

            _numeroCaracteresAgenciaBancaria = 0;
            _numeroCaracteresContaBancaria = 0;

            if (!cmbBanco.TextoEhSelecione())
            {
                txtAgencia.Resetar(habilitar: true, limparTexto: true, readOnly: false);
                txtConta.Resetar(habilitar: true, limparTexto: true, readOnly: false);

                txtAgencia.BackColor = SystemColors.InactiveBorder;
                txtConta.BackColor = SystemColors.InactiveBorder;

                Banco b = _campanhaService.ListarBanco(Convert.ToInt32(cmbBanco.SelectedValue.ToString()), true).FirstOrDefault();

                if(b != null)
                {
                    if (b.CaracteresConta != null)
                        _numeroCaracteresContaBancaria = (int)b.CaracteresConta;
                }
            }
            else
            {   
                txtAgencia.Resetar(habilitar: false, limparTexto: true, readOnly: true);
                txtConta.Resetar(habilitar: false, limparTexto: true, readOnly: true);                
            }
        }

        private void ConfiguracoesDeFormaDePagamento()
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
                txtAgencia.Resetar(habilitar: false, limparTexto: true, readOnly: true);
                txtConta.Resetar(habilitar: false, limparTexto: true, readOnly: true);
            }

            //if (cmbFormaPagamento.Text.Trim() != "BOLETO" && !cmbFormaPagamento.TextoEhSelecione())
            //    cmbFaturaDigital.Text = "SIM";

            //else if (cmbFormaPagamento.Text.Trim() == "BOLETO")
            //    cmbFaturaDigital.Text = "NÃO";

            //else
            //    cmbFaturaDigital.ResetarComSelecione(habilitar: true);

            txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: false);

            if (_preVenda != null)
            {
                if (cmbFormaPagamento.Text.Contains("WHATSAPP"))
                    txtNumeroFaturaWhatsApp.Resetar(habilitar: true, limparTexto: false, readOnly: false);
            }
            else
            {
                if (cmbFormaPagamento.Text.Contains("WHATSAPP"))
                {
                    txtNumeroFaturaWhatsApp.Resetar(habilitar: true, limparTexto: true, readOnly: false);
                }
                else
                    txtNumeroFaturaWhatsApp.Resetar(habilitar: false, limparTexto: true, readOnly: false);
            }
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

        private void VerificarCepComRestricao()
        {
            bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
            bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;
            string cepTexto = "";
            if (residencial)
            {
                cepTexto = txtCep.Text;
            }

            if (entrega)
            {
                cepTexto = txtCepEntrega.Text;
            }

            int finalCep = 0;
            if (!string.IsNullOrEmpty(cepTexto))
            {
                finalCep = int.Parse(cepTexto.Replace("-", "").Substring(5, 3));



                if (finalCep == 970)
                {
                    var result = MessageBox.Show("Solicitar autorização do Supervisor?", "CEP " + cepTexto + " com Restrição.", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (DialogResult.Yes == result)
                    {
                        if (residencial)
                        {
                            pnlAutorizar.Visible = true;
                        }
                        if (entrega)
                        {
                            pnlAutorizarEntrega.Visible = true;
                        }

                    }
                    else
                    {
                        if (residencial)
                        {
                            pnlAutorizar.Visible = false;
                        }
                        if (entrega)
                        {
                            pnlAutorizarEntrega.Visible = false;
                        }
                        LimparEndereco();


                    }
                }
            }
        }

        private void PararTempo()
        {
            SolicitarPermissaoForm solicitarPemissaoForm = new SolicitarPermissaoForm(_usuario);
            var retorno = solicitarPemissaoForm.SolicitarPermissaoDeUsuario(true, true);

            if (retorno?.PermissaoConfirmada ?? false)
            {
                PararTempoEvent?.Invoke(retorno.IdUsuarioPermissao);
            }
        }
        private void AjustarCombos(ComboBox combo, bool ativo = false)
        {

            combo.ResetarComSelecione(habilitar: ativo);

        }

        #endregion METODOS

        #region EVENTOS

        private void OfertaPortabilidadeClaroForm_Load(object sender, EventArgs e)
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

        private void btnSelecionarEndereco_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionarEndereco();
                //VerificarCepComRestricao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível selecionar o endereço!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsOferta_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a ofertaDoAtendimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OfertaPortabilidadeClaroForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!txtSenhaVendaCasada.Focused && !txtSupervisor.Focused && !txtSenha.Focused && !txtSupervisorEnt.Focused && !txtSenhaEnt.Focused)
            {
                if (Char.IsLower(e.KeyChar))
                    e.KeyChar = Char.ToUpper(e.KeyChar);

                if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNome.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereSomenteLetra(e.KeyChar);
        }

        private void txtRazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblRazaoSocial.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereSomenteLetra(e.KeyChar);
        }

        private void txtNomeGestorAdm_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNomeGestorAdm.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereSomenteLetra(e.KeyChar);
        }

        private void txtNomeRepresentante_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNomeRepresentante.ForeColor = SystemColors.WindowText;
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

        private void txtRazaoSocial_Leave(object sender, EventArgs e)
        {
            txtRazaoSocial.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(txtRazaoSocial.Text).ToUpper();

            if (!string.IsNullOrEmpty(txtRazaoSocial.Text))
            {
                string[] nome = txtRazaoSocial.Text.Trim().Split(' ');
                if (nome.Length <= 1)
                {
                    lblRazaoSocial.ForeColor = Color.Red;
                    txtRazaoSocial.Focus();
                    MessageBox.Show("[Razão Social] inválida!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblRazaoSocial.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNomeGestorAdm_Leave(object sender, EventArgs e)
        {
            txtNomeGestorAdm.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(txtNomeGestorAdm.Text).ToUpper();

            if (!string.IsNullOrEmpty(txtNomeGestorAdm.Text))
            {
                string[] nome = txtNomeGestorAdm.Text.Trim().Split(' ');
                if (nome.Length <= 1)
                {
                    lblNomeGestorAdm.ForeColor = Color.Red;
                    txtNomeGestorAdm.Focus();
                    MessageBox.Show("[Nome Gestor Adm] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNomeGestorAdm.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNomeRepresentante_Leave(object sender, EventArgs e)
        {
            txtNomeRepresentante.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(txtNomeRepresentante.Text).ToUpper();

            if (!string.IsNullOrEmpty(txtNomeRepresentante.Text))
            {
                string[] nome = txtNomeRepresentante.Text.Trim().Split(' ');
                if (nome.Length <= 1)
                {
                    lblNomeRepresentante.ForeColor = Color.Red;
                    txtNomeRepresentante.Focus();
                    MessageBox.Show("[Nome Gestor Adm] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNomeRepresentante.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtCpfGestorAdm_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCpfGestorAdm.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtCpfRepresentante_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCpfRepresentante.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCnpj.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtCpfGestorAdm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCpfGestorAdm.Text))
            {
                txtCpfGestorAdm.Text = CallplusFormsUtil.FormatarCPF(txtCpfGestorAdm.Text);

                if (!Texto.CpfPossuiFormatoValido(txtCpfGestorAdm.Text) && !string.IsNullOrEmpty(txtCpfGestorAdm.Text))
                {
                    lblCpfGestorAdm.ForeColor = Color.Red;
                    txtCpfGestorAdm.Focus();
                    MessageBox.Show("[CPF Gestor Adm] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblCpfGestorAdm.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtCpfRepresentante_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCpfRepresentante.Text))
            {
                txtCpfRepresentante.Text = CallplusFormsUtil.FormatarCPF(txtCpfRepresentante.Text);

                if (!Texto.CpfPossuiFormatoValido(txtCpfRepresentante.Text) && !string.IsNullOrEmpty(txtCpfRepresentante.Text))
                {
                    lblCpfRepresentante.ForeColor = Color.Red;
                    txtCpfRepresentante.Focus();
                    MessageBox.Show("[CPF Representante] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblCpfRepresentante.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtCNPJ_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCnpj.Text))
            {
                //txtCnpj.Text = CallplusFormsUtil.FormatarCNP(txtCnpj.Text);

                if (!Texto.CnpjPosuiFormatoValido(txtCnpj.Text) && !string.IsNullOrEmpty(txtCnpj.Text))
                {
                    lblCnpj.ForeColor = Color.Red;
                    txtCnpj.Focus();
                    MessageBox.Show("[CNPJ] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblCnpj.ForeColor = SystemColors.WindowText;
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
                //if (!Texto.TelefoneCelularPossuiFormatoValido(txtTelCelular.Text))
                //{
                //    lblTelCelular.ForeColor = Color.Red;
                //    txtTelCelular.Focus();
                //    MessageBox.Show("[Telefone Celular] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
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
                //if (!Texto.TelefonePossuiFormatoValido(txtTelResidencial.Text))
                //{
                //    lblTelResidencial.ForeColor = Color.Red;
                //    txtTelResidencial.Focus();
                //    MessageBox.Show("[Telefone da Gravação] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            else
            {
                lblTelResidencial.ForeColor = SystemColors.WindowText;
            }
        }

        private void cmbFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFormaPagamento.ForeColor = SystemColors.WindowText;
            lblBanco.ForeColor = SystemColors.WindowText;
            lblAgencia.ForeColor = SystemColors.WindowText;
            lblConta.ForeColor = SystemColors.WindowText;
            lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;

            ConfiguracoesDeFormaDePagamento();
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

        private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblProduto.ForeColor = SystemColors.WindowText;
        }

        private void tsOferta_cmbStatusOferta_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarProduto();
        }

        private void tsOferta_bntPararTempo_Click(object sender, EventArgs e)
        {
            try
            {
                PararTempo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar o checklist!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumeroPortado_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNumeroPortado.ForeColor = SystemColors.WindowText;
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
                lblNumeroPortado.ForeColor = SystemColors.WindowText;
            }
        }

        private void OfertaPortabilidadeClaroForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_venda)
                Gravar(false);
        }

        private void llklCopiarEndereco_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

            VerificarCepElegivel(txtCepEntrega.Text, true);
            //VerificarCepComRestricao();
        }

        private void lklCopiarEnderecoInstalacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCepInstalacao.Text = txtCep.Text;
            txtBairroInstalacao.Text = txtBairro.Text;
            txtLogradouroInstalacao.Text = txtLogradouro.Text;
            txtUF_Instalacao.Text = txtUf.Text;
            txtNumeroInstalacao.Text = txtNumero.Text;
            txtComplementoInstalacao.Text = txtComplemento.Text;
            txtCidadeInstalacao.Text = txtCidade.Text;
            txtPontoReferenciaInstal.Text = txtPontoReferencia.Text;

            tcEndereco.SelectedTab = tcEndereco_tp_EnderecoInstalacao;
        }

        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCep.ForeColor = SystemColors.WindowText;
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            lblNumero.ForeColor = SystemColors.WindowText;
        }

        private void txtUf_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUf.ForeColor = SystemColors.WindowText;
        }

        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCidade.ForeColor = SystemColors.WindowText;
        }

        private void txtCepEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCepEntrega.ForeColor = SystemColors.WindowText;
        }

        private void txtNumeroEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNumeroEntrega.ForeColor = SystemColors.WindowText;
        }
        
        private void txtUfEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUfEntrega.ForeColor = SystemColors.WindowText;
        }      
        
        private void txtEmail_Leave(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(txtEmail.Text) && !Texto.EmailPosuiFormatoValido(txtEmail.Text))
            {
                lblEmailCliente.ForeColor = Color.Red;
                MessageBox.Show("[E-Mail] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else
            {
                lblEmailCliente.ForeColor = SystemColors.WindowText;
            }
        }

        private void rbFisica_CheckedChanged(object sender, EventArgs e)
        {
            tcDadosPessoais.TabPages.Clear();

            if (rbFisica.Checked)
            {
                tcDadosPessoais.TabPages.Add(tpPessoaFisica);                
            }
            else
            {
                tcDadosPessoais.TabPages.Add(tpPessoaJuridica);                
            }
        }

        private void cmbPossuiEmail_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbPossuiEmail.Text == "SIM")
            {
                txtEmail.Enabled = true;
                lblEmailCliente.Enabled = true;
            }
            else
            {
                txtEmail.Enabled = false;
                lblEmailCliente.Enabled = false;
                txtEmail.Clear();
            }
        }       

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Autorizar();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);

                MessageBox.Show($"Ocorreu um erro Inesperado!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bool residencial = tcEndereco.SelectedTab == tcEndereco_tpEnderecoResidencial ? true : false;
            bool entrega = tcEndereco.SelectedTab == tcEndereco_tpEnderecoEntrega ? true : false;
            if (residencial)
            {
                pnlAutorizar.Visible = false;
                txtSenha.Clear();
                txtSupervisor.Clear();
                
            }
            if (entrega)
            {
                pnlAutorizarEntrega.Visible = false;
                txtSenhaEnt.Clear();
                txtSupervisorEnt.Clear();
                
            }

            LimparEndereco();

        }

        private void cmbProduto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbProduto.TextoEhSelecione())
            {
                AjustarCombos(cmbProduto2, ativo: true);
                AjustarCombos(cmbProduto3, ativo: false);
                AjustarCombos(cmbProduto4, ativo: false);
                AjustarCombos(cmbProduto5, ativo: false);

                AjustarCombos(cmbPortabilidade2, ativo: true);
                AjustarCombos(cmbPortabilidade3, ativo: false);
                AjustarCombos(cmbPortabilidade4, ativo: false);
                AjustarCombos(cmbPortabilidade5, ativo: false);

            }
            else
            {
                AjustarCombos(cmbProduto2, ativo: false);
                AjustarCombos(cmbProduto3, ativo: false);
                AjustarCombos(cmbProduto4, ativo: false);
                AjustarCombos(cmbProduto5, ativo: false);

                AjustarCombos(cmbPortabilidade2, ativo: false);
                AjustarCombos(cmbPortabilidade3, ativo: false);
                AjustarCombos(cmbPortabilidade4, ativo: false);
                AjustarCombos(cmbPortabilidade5, ativo: false);
            }

            

        }

        private void cmbProduto2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbProduto2.TextoEhSelecione())
            {
                AjustarCombos(cmbProduto3, ativo: true);
                AjustarCombos(cmbProduto4, ativo: false);
                AjustarCombos(cmbProduto5, ativo: false);

                AjustarCombos(cmbPortabilidade3, ativo: true);
                AjustarCombos(cmbPortabilidade4, ativo: false);
                AjustarCombos(cmbPortabilidade5, ativo: false);

            }
            else
            {
                AjustarCombos(cmbProduto3, ativo: false);
                AjustarCombos(cmbProduto4, ativo: false);
                AjustarCombos(cmbProduto5, ativo: false);

                AjustarCombos(cmbPortabilidade3, ativo: false);
                AjustarCombos(cmbPortabilidade4, ativo: false);
                AjustarCombos(cmbPortabilidade5, ativo: false);
            }
        }

        private void cmbProduto3_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (!cmbProduto3.TextoEhSelecione())
            {
                AjustarCombos(cmbProduto4, ativo: true);
                AjustarCombos(cmbProduto5, ativo: false);

                AjustarCombos(cmbPortabilidade4, ativo: true);
                AjustarCombos(cmbPortabilidade5, ativo: false);
            }
            else
            {
                AjustarCombos(cmbProduto4, ativo: false);
                AjustarCombos(cmbProduto5, ativo: false);

                AjustarCombos(cmbPortabilidade4, ativo: false);
                AjustarCombos(cmbPortabilidade5, ativo: false);
            }

        }

        private void cmbProduto4_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (!cmbProduto4.TextoEhSelecione())
            {
                AjustarCombos(cmbProduto5, ativo: true);
                AjustarCombos(cmbPortabilidade5, ativo: true);
            }
            else
            {
                AjustarCombos(cmbProduto5, ativo: false);
                AjustarCombos(cmbPortabilidade5, ativo: false);

            }
        }

        private void cmbPortabilidade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbPortabilidade.TextoEhSelecione() && cmbPortabilidade.Text != "NÃO")
            {
                // TODO: Flávio - Chamado 15972 Preenchendo o campo com telefone do contato
                if (tsOferta_cmbStatusOferta.SelectedIndex == 1)
                {
                    txtNumeroPortado.Enabled = false;
                    txtNumeroPortado.Text = string.IsNullOrEmpty(txtTelCelular.Text) ? txtTelResidencial.Text : txtTelCelular.Text;
                }
                else
                    txtNumeroPortado.Enabled = true;
            }
            else
            {
                txtNumeroPortado.Enabled = false;
                txtNumeroPortado.Clear();
            }
        }

        private void cmbPortabilidade2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbPortabilidade2.TextoEhSelecione() && cmbPortabilidade2.Text != "NÃO")
            {
                txtNumeroPortado2.Enabled = true;
            }
            else
            {
                txtNumeroPortado2.Enabled = false;
                txtNumeroPortado2.Clear();
            }
        }

        private void cmbPortabilidade3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbPortabilidade3.TextoEhSelecione() && cmbPortabilidade3.Text != "NÃO")
            {
                txtNumeroPortado3.Enabled = true;
            }
            else
            {
                txtNumeroPortado3.Enabled = false;
                txtNumeroPortado3.Clear();
            }
        }

        private void cmbPortabilidade4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbPortabilidade4.TextoEhSelecione() && cmbPortabilidade4.Text != "NÃO")
            {
                txtNumeroPortado4.Enabled = true;
            }
            else
            {
                txtNumeroPortado4.Enabled = false;
                txtNumeroPortado4.Clear();
            }
        }

        private void cmbPortabilidade5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!cmbPortabilidade5.TextoEhSelecione() && cmbPortabilidade5.Text != "NÃO")
            {
                txtNumeroPortado5.Enabled = true;
            }
            else
            {
                txtNumeroPortado5.Enabled = false;
                txtNumeroPortado5.Clear();
            }
        }

        private void txtNumeroPortado2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroPortado2.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroPortado2.Text))
                {
                    lblNumeroPortado2.ForeColor = Color.Red;
                    //txtNumeroPortado.Focus();
                    MessageBox.Show("[Número Portado 2] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroPortado2.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNumeroPortado3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroPortado3.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroPortado3.Text))
                {
                    lblNumeroPortado3.ForeColor = Color.Red;
                    //txtNumeroPortado3.Focus();
                    MessageBox.Show("[Número Portado 3] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroPortado3.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNumeroPortado4_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroPortado4.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroPortado4.Text))
                {
                    lblNumeroPortado4.ForeColor = Color.Red;
                   // txtNumeroPortado4.Focus();
                    MessageBox.Show("[Número Portado 4] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroPortado4.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtNumeroPortado5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroPortado5.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroPortado5.Text))
                {
                    lblNumeroPortado5.ForeColor = Color.Red;
                    //txtNumeroPortado5.Focus();
                    MessageBox.Show("[Número Portado 5] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroPortado5.ForeColor = SystemColors.WindowText;
            }
        }

		private void txtDddTel_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblDddTel.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtDddCel_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblDddCel.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}

		private void txtCustId_KeyPress(object sender, KeyPressEventArgs e)
		{
			lblCustId.ForeColor = SystemColors.WindowText;
			e.Handled = Texto.CaractereNumerico(e.KeyChar);
		}
        #endregion EVENTOS
    }
}
