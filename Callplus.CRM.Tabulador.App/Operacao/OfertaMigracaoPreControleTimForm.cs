using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
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
using Callplus.CRM.Tabulador.App.Controles.CamposDinamicos;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.operador;
using Callplus.CRM.Tabulador.App.Util.CorreiosActionline;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class OfertaMigracaoPreControleTimForm : Form
    {
        public OfertaMigracaoPreControleTimForm(Usuario usuario, long idOferta, Prospect prospect, ContainerDeLayoutDeCamposDinamicos camposDinamicos, Discador discadorConectado,
            bool bloqueioStatus, bool fecharAoGravar, int? idStatusOferta = null, int? idCampanha = null, bool edicao = true, bool exibirTodasAsDatasVencimento = false)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _atendimentoService = new AtendimentoService();
            _campanhaService = new CampanhaService();
            _checklistService = new ChecklistService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _produtoService = new ProdutoService();
            _prospectService = new ProspectService();
            _usuarioService = new UsuarioService();
            _statusDeOfertaService = new StatusDeOfertaService();
            _permissaoService = new PermissaoService();
            _correioService = new RetornoDeCepService();

            _camposDinamicos = camposDinamicos;
            _exibirTodasAsDatasVencimento = exibirTodasAsDatasVencimento;
            _discadorConectado = discadorConectado;
            _usuario = usuario;
            _prospect = prospect;
            _idStatusOferta = idStatusOferta;
            _bloqueioStatus = bloqueioStatus;
            _permiteEditar = edicao;
            _fecharAoGravar = fecharAoGravar;

            if (idCampanha != null)
                _campanhaAtual = _campanhaService.RetornarCampanha((int)idCampanha);

            _oferta = _ofertaDoAtendimentoService.RetornarCobrancaAtendimentoPravaler(idOferta);

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
        private readonly UsuarioService _usuarioService;
        private readonly StatusDeOfertaService _statusDeOfertaService;
        private readonly PermissaoService _permissaoService;
        private readonly RetornoDeCepService _correioService;
        private readonly ContainerDeLayoutDeCamposDinamicos _camposDinamicos;
        private Dictionary<string, bool> mapDeReproducoesGravacoes;

        private Usuario _usuario;
        private Prospect _prospect;
        private CobrancaAtendimentoPravaler _oferta;
        private CobrancaAtendimentoPravaler _preVenda;

        public delegate void PararTempoHandler(int? idUsuarioAprovacao);
        public event PararTempoHandler PararTempoEvent;

        private int? _idStatusOferta;
        private bool _bloqueioStatus;
        private bool _fecharAoGravar;
        private bool _permiteEditar;
        private bool _checklistAplicado;
        private bool _filtraPorFaixaDeRecarga;
        private bool _exibirTodasAsDatasVencimento;

        private string _nomeProduto;
        private int ativo;
        private int ativoBko;
        bool vendaSegundoProduto = false;
        private Campanha _campanhaAtual;
        private bool _venda;
        private readonly Discador _discadorConectado;
        private int _numeroCaracteresAgenciaBancaria = 0;
        private int _numeroCaracteresContaBancaria = 0;

        public bool Atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            Atualizar = false;
            tsOferta_cmbStatusOferta.Enabled = false;
            lIdade.Text = "";
            //cmbOfertaAparelho.ResetarComSelecione(habilitar: true);
            //cmbFaturaDigital.ResetarComSelecione(habilitar: true);
            cmbReceberContrato.ResetarComSelecione(habilitar: true);
            cmbSexo.ResetarComSelecione(habilitar: true);

            _filtraPorFaixaDeRecarga = true;
            _nomeProduto = "";

            //TODO - Comentado para produto2 - Rei Almeida
            //if (_campanhaAtual.idTipoDaCampanha == 7)
            //{
                _filtraPorFaixaDeRecarga = false;
            //if (!string.IsNullOrEmpty(_prospect.Campo006))
            //{
            //    _nomeProduto = !string.IsNullOrEmpty(_prospect.Campo007) ? _prospect.Campo006 + "," +_prospect.Campo007 : _prospect.Campo006;
            //}
                

                //gbLiberarMplay.Enabled = false;
                //gbLiberarVendaCasada.Enabled = false;                
            //}

            tsOferta_btnChecklist.Enabled = false;

            CarregarProduto();
            CarregarBanco();
            CarregarDiaDeVencimentoDaFatura();
            CarregarFaixaDeRenda();
            BloquearProduto();
            CarregarEstadoCivil();
            CarregarProfissao();
            CarregarFormaDePagamento();
            CarregarTipoDeStatusDeOferta();
            CarregarDadosIniciais();

            if (_idStatusOferta != null && _idStatusOferta > 0)
            {
                ConfigurarStatusDaOferta(_idStatusOferta.Value);
            }
            
            CarregarControleDeEdicao();
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

                foreach (var item in gbLiberarMplay.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbLiberarMplay.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }

                foreach (var item in gbLiberarMplay.Controls.OfType<Button>().Where(x => x.Name.Contains("btn")))
                {
                    item.Enabled = false;
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
                _preVenda = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoPreVendaMigracao(idAtendimento);

            if (_preVenda != null)
            {
                txtNome.Text = _preVenda.Nome;
            }
            else
            {
                if (string.IsNullOrEmpty(_oferta.Nome))
                {
                    txtNome.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(_prospect.Campo002).ToUpper();
                }
                else
                    txtNome.Text = _oferta.Nome;
            }

            if (_preVenda != null)
            {
                txtCpf.Text = _preVenda.Cpf.ToString();
            }
            else
            {
                if (_oferta.Cpf == null)
                {
                    if(!string.IsNullOrEmpty(_prospect.Campo001))
                    txtCpf.Text = CallplusFormsUtil.FormatarCPF(_prospect.Campo001);
                }
                else
                    txtCpf.Text = CallplusFormsUtil.FormatarCPF(_oferta.Cpf.ToString());
            }

            //TODO - corrigir
            if (Texto.CpfPossuiFormatoValido(txtCpf.Text))
            {
                if (_idStatusOferta == 1121)//VENDA - TROCA TITULARIDADE = habilita a EDIÇÃO DO CAMPO CPF
                    txtCpf.Resetar(habilitar: true, limparTexto: false, readOnly: false);
                else
                    txtCpf.Resetar(habilitar: true, limparTexto: false, readOnly: false);
            }
            else
            {
                lblCpf.ForeColor = Color.Red;
                txtCpf.Resetar(habilitar: true, limparTexto: false, readOnly: false);
                //MessageBox.Show("[CPF] carregado inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (_preVenda != null)
            {
                txtDataNascimento.Text = _preVenda.Nascimento.ToString();
            }
            else
            {
                if (_oferta.Nascimento == null)
                {
                    txtDataNascimento.Text = (_prospect.Campo047);
                }
                else
                    txtDataNascimento.Text = _oferta.Nascimento.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.Rg != null)
                    txtRg.Text = _preVenda.Rg;
            }
            else
            {
                if (_oferta.Rg != null)
                    txtRg.Text = _oferta.Rg;
            }

            if (_preVenda != null)
            {
                if (_preVenda.NomeDaMae != null)
                    txtNomeDaMae.Text = _preVenda.NomeDaMae;
            }
            else
            {
                if (_oferta.NomeDaMae != null)
                    txtNomeDaMae.Text = _oferta.NomeDaMae;
            }

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
                    txtTelCelular.Text = _prospect.Telefone01.ToString();
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

            if (_preVenda != null)
            {
                if (_preVenda.TelefoneRecado != null)
                    txtTelRecado.Text = _preVenda.TelefoneRecado.ToString();
            }
            else
            {
                if (_oferta.TelefoneRecado != null)
                    txtTelRecado.Text = _oferta.TelefoneRecado.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.IdEstadoCivil != null)
                    cmbEstadoCivil.SelectedValue = _preVenda.IdEstadoCivil.ToString();
            }
            else
            {
                if (_oferta.IdEstadoCivil != null)
                    cmbEstadoCivil.SelectedValue = _oferta.IdEstadoCivil.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.IdProfissao != null)
                    cmbProfissao.SelectedValue = _preVenda.IdProfissao.ToString();
            }
            else
            {
                if (_oferta.IdProfissao != null)
                    cmbProfissao.SelectedValue = _oferta.IdProfissao.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.IdFaixaDeRenda != null)
                    cmbFaixaRenda.SelectedValue = _preVenda.IdFaixaDeRenda.ToString();
            }
            else
            {
                if (_oferta.IdFaixaDeRenda != null)
                    cmbFaixaRenda.SelectedValue = _oferta.IdFaixaDeRenda.ToString();
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
                    txtObservacao.Text = _oferta.Observacao.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.IdProduto != null)
                {
                    cmbProduto.SelectedValue = _preVenda.IdProduto.ToString();

                    if (cmbProduto.SelectedValue != null)
                        cmbProduto.Enabled = true;
                    else
                        cmbProduto.ResetarComSelecione(habilitar: true);
                }
            }
            else
            {
                if (_oferta.IdProduto != null)
                {
                    cmbProduto.SelectedValue = _oferta.IdProduto.ToString();

                    if (cmbProduto.SelectedValue != null)
                        cmbProduto.Enabled = true;
                    else
                        cmbProduto.ResetarComSelecione(habilitar: true);
                }
            }

            //if (_preVenda != null)
            //{
            //    if (_preVenda.ofertaAparelho == true)
            //        cmbOfertaAparelho.Text = "SIM";
            //    else if (_preVenda.ofertaAparelho == false)
            //        cmbOfertaAparelho.Text = "NÃO";
            //}
            //else
            //{
            //    if (_oferta.ofertaAparelho != null)
            //    {
            //        if (_oferta.ofertaAparelho == true)
            //            cmbOfertaAparelho.Text = "SIM";
            //        else if (_oferta.ofertaAparelho == false)
            //            cmbOfertaAparelho.Text = "NÃO";
            //    }
            //    else
            //        cmbOfertaAparelho.ResetarComSelecione(habilitar: true);

            //}

            //if (_preVenda != null)
            //{
            //    if (_preVenda.url != null)
            //        txtUrl.Text = _preVenda.url.ToString();
            //}
            //else
            //{
            //    if (_oferta.url != null)
            //        txtUrl.Text = _oferta.url.ToString();
            //}

            if (_preVenda != null)
            {

                if (_preVenda.IdProduto2 != null)
                {
                    cmbProduto2.SelectedValue = _preVenda.IdProduto2.ToString();

                    if (cmbProduto2.SelectedValue != null && cmbProduto2.SelectedValue.ToString() != "-1")
                    {
                        cmbProduto2.Enabled = true;
                        btnPlayerProduto2.Enabled = true;
                        txtNumeroMigrado2.Enabled = true;
                    }
                    else
                        cmbProduto2.ResetarComSelecione(habilitar: true);
                }
            }
            else
            {
                if (_oferta.IdProduto2 != null)
                {
                    cmbProduto2.SelectedValue = _oferta.IdProduto2.ToString();

                    if (cmbProduto2.SelectedValue != null)
                        cmbProduto2.Enabled = true;
                    else
                        cmbProduto2.ResetarComSelecione(habilitar: true);
                }
            }

            if (_preVenda != null)
            {
                if (_preVenda.NumeroMigrado != null)
                    txtNumeroMigrado.Text = _preVenda.NumeroMigrado.ToString();
            }
            else
            {
                if (_oferta.NumeroMigrado != null)
                    txtNumeroMigrado.Text = _oferta.NumeroMigrado.ToString();
                else
                    txtNumeroMigrado.Text = _prospect.Telefone01.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.NumeroMigrado2 != null)
                    txtNumeroMigrado2.Text = _preVenda.NumeroMigrado2.ToString();
            }
            else
            {
                if (_oferta.NumeroMigrado2 != null)
                    txtNumeroMigrado2.Text = _oferta.NumeroMigrado2.ToString();
                else
                    txtNumeroMigrado2.Text = "";
            }

            //if (_preVenda != null)
            //{
            //    if (_preVenda.FaturaDigital == true)
            //        cmbFaturaDigital.Text = "SIM";
            //    else if (_preVenda.FaturaDigital == false)
            //        cmbFaturaDigital.Text = "NÃO";
            //}
            //else
            //{
            //    if (_oferta.FaturaDigital == true)
            //        cmbFaturaDigital.Text = "SIM";
            //    else if (_oferta.FaturaDigital == false)
            //        cmbFaturaDigital.Text = "NÃO";
            //}


            if (_preVenda != null)
            {
                if (_preVenda.DiaVencimento != null)
                    cmbDiaDeVencimentoDaFatura.Text = _preVenda.DiaVencimento.ToString();
            }
            else
            {
                if (_oferta.DiaVencimento != null)
                    cmbDiaDeVencimentoDaFatura.Text = _oferta.DiaVencimento.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.IdFormaDePagamento != null)
                    cmbFormaPagamento.SelectedValue = _preVenda.IdFormaDePagamento.ToString();
            }
            else
            {
                if (_oferta.IdFormaDePagamento != null)
                    cmbFormaPagamento.SelectedValue = _oferta.IdFormaDePagamento.ToString();
            }

            if (_preVenda != null)
            {
                if (_preVenda.EmailFaturaDigital != null)
                    txtEmailFaturaDigital.Text = _preVenda.EmailFaturaDigital;
            }
            else
            {
                if (_oferta.EmailFaturaDigital != null)
                    txtEmailFaturaDigital.Text = _oferta.EmailFaturaDigital;
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

            if (_preVenda != null)
            {
                if (_preVenda.Agencia != null)
                    txtAgencia.Text = _preVenda.Agencia;
            }
            else
            {
                if (_oferta.Agencia != null)
                    txtAgencia.Text = _oferta.Agencia;
            }

            if (_preVenda != null)
            {
                if (_preVenda.Conta != null)
                    txtConta.Text = _preVenda.Conta;
            }
            else
            {
                if (_oferta.Conta != null)
                    txtConta.Text = _oferta.Conta;
            }

            //if (_preVenda != null)
            //{
            //    if (_preVenda.codigo21 == true)
            //        cmbCodigo21.Text = "SIM";
            //    else if (_preVenda.codigo21 == false)
            //        cmbCodigo21.Text = "NÃO";
            //}
            //else
            //{
            //    if (_oferta.codigo21 == true)
            //        cmbCodigo21.Text = "SIM";
            //    else if (_oferta.codigo21 == false)
            //        cmbCodigo21.Text = "NÃO";
            //}

            if (_preVenda != null)
            {
                if (_preVenda.receberContrato == true)
                    cmbReceberContrato.Text = "SIM";
                else if (_preVenda.receberContrato == false)
                    cmbReceberContrato.Text = "NÃO";
            }
            else
            {
                if (_oferta.receberContrato == true)
                    cmbReceberContrato.Text = "SIM";
                else if (_oferta.receberContrato == false)
                    cmbReceberContrato.Text = "NÃO";
            }

            if (_preVenda != null)
            {
                if (_preVenda.NumeroFaturaWhatsApp != null)
                    txtNumeroFaturaWhatsApp.Text = _preVenda.NumeroFaturaWhatsApp.ToString();
            }
            else
            {
                if (_oferta.NumeroFaturaWhatsApp != null)
                    txtNumeroFaturaWhatsApp.Text = _oferta.NumeroFaturaWhatsApp.ToString();
            }
        }

        private void CarregarDadosEndereco()
        {
            if (_preVenda != null)
            {
                if (_preVenda != null)
                {
                    if (_preVenda.Cep != null)
                        txtCep.Text = _preVenda.Cep.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.Numero != null)
                        txtNumero.Text = _preVenda.Numero.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.PontoDeReferencia != null)
                        txtPontoReferencia.Text = _preVenda.PontoDeReferencia.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.Complemento != null)
                        txtComplemento.Text = _preVenda.Complemento.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.Uf != null)
                        txtUf.Text = _preVenda.Uf.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.Cidade != null)
                        txtCidade.Text = _preVenda.Cidade.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.Logradouro != null)
                        txtLogradouro.Text = _preVenda.Logradouro.ToString();
                }

                if (_preVenda != null)
                {
                    if (_preVenda.Bairro != null)
                        txtBairro.Text = _preVenda.Bairro.ToString();
                }
            }
            else
            {
                if (_oferta.Cep == null)
                {
                    var cep = _prospect.Campo017;
                    long cepNumerico;

                    if (string.IsNullOrEmpty(cep) == false && (cep.Length == 7 || cep.Length == 8) && long.TryParse(cep, out cepNumerico))
                    {
                        if (cep.Length == 7)
                        {
                            cep = $"0{cep}";
                        }

                        CarregarEnderecoTabDadosDaVenda(cep);
                    }
                }
                else
                {
                    if (_oferta.Cep != null)
                        txtCep.Text = _oferta.Cep.ToString();

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

                if (endereco != null)// && endereco.Cep != "-1")
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

        private void CarregarDiaDeVencimentoDaFatura()
        {
            if (_exibirTodasAsDatasVencimento)
            {
                IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _ofertaDoAtendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(true);
                cmbDiaDeVencimentoDaFatura.PreencherComSelecione(configuracaoDatas, x => x.Fechamento, x => x.Vencimento);
            }
            else
            {
                IEnumerable<ConfiguracaoVencimentoFaturaDto> configuracaoDatas = _atendimentoService.RetornarDatasDeVencimentoDeFaturaDisponiveis(_campanhaAtual.Id);
                cmbDiaDeVencimentoDaFatura.PreencherComSelecione(configuracaoDatas, x => x.Fechamento, x => x.Vencimento);
            }
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

        private void CarregarProduto()
        {
            int idTipoDeProduto = -1;
            bool ativo = true;
            bool? ativoBko = null;

            if (_idStatusOferta == 21)
            {
                idTipoDeProduto = 2;
                cmbProduto.ResetarComSelecione(habilitar: true);
            }
            else
            {
                idTipoDeProduto = 1;
                cmbProduto.ResetarComSelecione(habilitar: false);
            }

            IEnumerable<ProdutoDaOfertaDto> produtos = null;

            //if (_filtraPorFaixaDeRecarga && _campanhaAtual.Id != 16)
            //{
            //    produtos = _produtoService.ListarProdutoDaOfertaPorFaixaDeRecarga(_oferta.IdAtendimento).Where(x => x.idTipo == idTipoDeProduto).Distinct();
            //}            
            //else
            //{
                produtos = _produtoService.ListarProdutoDaOfertaPorIdProspect(_prospect.IdCampanha, _prospect.Id, ativo, ativoBko).Where(x => x.idTipo == idTipoDeProduto).Distinct();
            //}
            
            cmbProduto.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
            //cmbProduto.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
        }

        private void BloquearProduto()
        {
            cmbProduto2.Enabled = false;
            btnPlayerProduto2.Enabled = false;
            txtNumeroMigrado2.Enabled = false;
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

        private bool AtendeRegrasDeGravacao(bool considerarCheckList)
        {
            var mensagens = new List<string>();

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

            //if (_prospect.IdCampanha != 8)
            //    quantidadeVendaParaNaoTitularExcedida = _permissaoService.QuantidadeVendaParaNaoTitularExcedida(_usuario.Id, _prospect.IdCampanha);

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
                    if (CallplusFormsUtil.FormatarCPF(_prospect.Campo001) != CallplusFormsUtil.FormatarCPF(txtCpf.Text))
                    {
                        lblCpf.ForeColor = Color.Red;
                        mensagens.Add("[CPF] deve ser o mesmo informado no Mailing!");
                    }
                }

                if (quantidadeVendaParaNaoTitularExcedida)
                {
                    if (CallplusFormsUtil.FormatarCPF(_prospect.Campo001) != CallplusFormsUtil.FormatarCPF(txtCpf.Text))
                    {
                        lblCpf.ForeColor = Color.Red;
                        mensagens.Add("[CPF] deve ser o mesmo informado no Mailing! Pois a quantidade de Mudanças de Titularidade foi excedida!");
                    }
                }

                //if (string.IsNullOrEmpty(cmbOfertaAparelho.Text) || cmbOfertaAparelho.TextoEhSelecione())
                //{
                //    lblDesejaAparelho.ForeColor = Color.Red;
                //    mensagens.Add("[Deseja Receber Oferta de Aparelho?] deve ser informado!");
                //}
                //else if(cmbOfertaAparelho.Text == "SIM")
                //{
                //    if (string.IsNullOrEmpty(txtUrl.Text))
                //    {
                //        lblUrl.ForeColor = Color.Red;
                //        mensagens.Add("[Url] deve ser preenchido!");
                //    }
                //}

                if (string.IsNullOrEmpty(cmbProduto.Text) || cmbProduto.TextoEhSelecione())
                {
                    lblProduto.ForeColor = Color.Red;
                    mensagens.Add("[Produto] deve ser informado!");
                }

                if (vendaSegundoProduto)
                {
                    if (!string.IsNullOrEmpty(txtNumeroMigrado2.Text) && Convert.ToInt32(cmbProduto2.SelectedValue) == -1)
                    {
                        lblProduto2.ForeColor = Color.Red;
                        mensagens.Add("[Produto 2] deve ser informado!");
                    }

                    if (string.IsNullOrEmpty(txtNumeroMigrado2.Text) && Convert.ToInt32(cmbProduto2.SelectedValue) != -1)
                    {
                        lblNumeroMigrado2.ForeColor = Color.Red;
                        mensagens.Add("[Telefone 2] deve ser informado!");
                    }

                    if (!string.IsNullOrEmpty(txtNumeroMigrado2.Text))
                    {
                        lblNumeroMigrado2.ForeColor = Color.Red;
                        if (Texto.TelefonePossuiFormatoValido(txtNumeroMigrado2.Text) == false)
                            mensagens.Add("[Telefone 2] informado não é válido");
                    }
                }

                //if (Convert.ToInt32(VerificaRGMaiorQueZero(txtRg.Text)) < 1)
                //{
                //    txtRg.ForeColor = Color.Red;
                //    MessageBox.Show("[RG] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtRg.Focus();
                //}
                if (tsOferta_cmbStatusOferta.Text != "VENDA - SOMENTE PROMOÇÃO")
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
                    else if (!string.IsNullOrEmpty(txtTelRecado.Text) && txtTelResidencial.Text == txtTelRecado.Text)
                    {
                        //Random rand = new Random();
                        //txtTelRecado.Text = txtTelCelular.Text.Substring(0, 3) + rand.Next(10000000, 99999999).ToString();

                        lblTelRecado.ForeColor = Color.Red;
                        mensagens.Add("[Telefone Recado] não pode ser igual ao Telefone Residencial!");
                    }
                    else if (!string.IsNullOrEmpty(txtTelRecado.Text) && txtTelCelular.Text == txtTelRecado.Text)
                    {
                        //Random rand = new Random();
                        //txtTelRecado.Text = txtTelCelular.Text.Substring(0, 3) + rand.Next(10000000, 99999999).ToString();

                        lblTelRecado.ForeColor = Color.Red;
                        mensagens.Add("[Telefone Recado] não pode ser igual ao Telefone Celular!");
                    }

                    if (string.IsNullOrEmpty(txtCep.Text))
                    {
                        lblCep.ForeColor = Color.Red;
                        mensagens.Add("[CEP] deve ser informado!");
                    }

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

                if (considerarCheckList)
                {
                    int idCampanha = _prospect.IdCampanha;
                    int idProduto = Convert.ToInt32(cmbProduto.SelectedValue);
                    string ddd = _prospect.Telefone01.ToString().Substring(0, 2);

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
                if (AtendeRegrasDeGravacao(true))
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

        private void DadosAhGravar(bool venda)
        {
            _oferta.IdStatusDaOferta = venda ? Convert.ToInt32(tsOferta_cmbStatusOferta.ComboBox.SelectedValue) : 38;

            if (!cmbProduto.TextoEhSelecione())
                _oferta.IdProduto = Convert.ToInt64(cmbProduto.SelectedValue);

            if (!string.IsNullOrEmpty(txtNumeroMigrado.Text))
                _oferta.NumeroMigrado = Convert.ToInt64(txtNumeroMigrado.Text);

            if (!cmbProduto2.TextoEhSelecione())
                _oferta.IdProduto2 = Convert.ToInt64(cmbProduto2.SelectedValue);

            if (!string.IsNullOrEmpty(txtNumeroMigrado2.Text))
                _oferta.NumeroMigrado2 = Convert.ToInt64(txtNumeroMigrado2.Text);


            //if (!cmbFaturaDigital.TextoEhSelecione())
            //    _oferta.FaturaDigital = (cmbFaturaDigital.Text == "SIM") ? true : false;

            if (!string.IsNullOrEmpty(txtEmailFaturaDigital.Text))
                _oferta.EmailFaturaDigital = txtEmailFaturaDigital.Text;

            if (!cmbDiaDeVencimentoDaFatura.TextoEhSelecione())
                _oferta.DiaVencimento = Convert.ToInt32(cmbDiaDeVencimentoDaFatura.Text);

            if (!cmbFormaPagamento.TextoEhSelecione())
                _oferta.IdFormaDePagamento = Convert.ToInt32(cmbFormaPagamento.SelectedValue);

            if (!cmbBanco.TextoEhSelecione())
                _oferta.IdBanco = Convert.ToInt32(cmbBanco.SelectedValue);

            if (!string.IsNullOrEmpty(txtAgencia.Text))
                _oferta.Agencia = txtAgencia.Text;

            if (!string.IsNullOrEmpty(txtConta.Text))
                _oferta.Conta = txtConta.Text;

            if (!string.IsNullOrEmpty(txtNome.Text))
                _oferta.Nome = txtNome.Text;

            if (!string.IsNullOrEmpty(txtCpf.Text))
                _oferta.Cpf = Convert.ToInt64(txtCpf.Text);

            if (!string.IsNullOrEmpty(txtRg.Text))
                _oferta.Rg = txtRg.Text;

            DateTime dtNascimento;
            if (DateTime.TryParse(txtDataNascimento.Text, out dtNascimento))
                _oferta.Nascimento = dtNascimento;

            if (!string.IsNullOrEmpty(txtNomeDaMae.Text))
                _oferta.NomeDaMae = txtNomeDaMae.Text;

            if (!string.IsNullOrEmpty(txtTelCelular.Text))
                _oferta.TelefoneCelular = Convert.ToInt64(txtTelCelular.Text);

            if (!string.IsNullOrEmpty(txtTelResidencial.Text))
                _oferta.TelefoneResidencial = Convert.ToInt64(txtTelResidencial.Text);

            if (!string.IsNullOrEmpty(txtTelRecado.Text))
                _oferta.TelefoneRecado = Convert.ToInt64(txtTelRecado.Text);

            if (!cmbEstadoCivil.TextoEhSelecione())
                _oferta.IdEstadoCivil = Convert.ToInt32(cmbEstadoCivil.SelectedValue);

            if (!cmbProfissao.TextoEhSelecione())
                _oferta.IdProfissao = Convert.ToInt32(cmbProfissao.SelectedValue);

            if (!cmbFaixaRenda.TextoEhSelecione())
                _oferta.IdFaixaDeRenda = Convert.ToInt32(cmbFaixaRenda.SelectedValue);

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

            if (!cmbSexo.TextoEhSelecione())
                _oferta.Sexo = cmbSexo.SelectedItem.ToString();

            _oferta.IdOperador = _usuario.Id;

            if (!string.IsNullOrEmpty(txtCpf.Text))
            {
                string cpf = string.IsNullOrEmpty(_prospect.Campo001) ? "0" : _prospect.Campo001;

                    if (CallplusFormsUtil.FormatarCPF(cpf) != CallplusFormsUtil.FormatarCPF(txtCpf.Text))
                        _oferta.TitularidadeDiferente = true;
                    else
                        _oferta.TitularidadeDiferente = false;
                
               
            }
         

            if (!string.IsNullOrEmpty(txtNumeroFaturaWhatsApp.Text))
                _oferta.NumeroFaturaWhatsApp = Convert.ToInt64(txtNumeroFaturaWhatsApp.Text);

            //if (!cmbCodigo21.TextoEhSelecione())
            //    _oferta.codigo21 = (cmbCodigo21.Text == "SIM") ? true : false;

            if (!cmbReceberContrato.TextoEhSelecione())
                _oferta.receberContrato = (cmbReceberContrato.Text == "SIM") ? true : false;

            if (!cmbOndeReceberContrato.TextoEhSelecione())
                _oferta.ondeReceberContrato = cmbOndeReceberContrato.Text;

            //if (!cmbOfertaAparelho.TextoEhSelecione())
            //    _oferta.ofertaAparelho = (cmbOfertaAparelho.Text == "SIM") ? true : false;

            //if (!string.IsNullOrEmpty(txtUrl.Text))
            //    _oferta.url = txtUrl.Text;

            if (venda)
                _oferta.processado = true;
            else
                _oferta.processado = false;

            _oferta.Id = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoClaroMigracao(_oferta);

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
            if (Texto.TelefonePossuiFormatoValido(txtTelRecado.Text))
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

        public void validacoesParaFaturaDigital()
        {
            bool resultado;

            resultado = cmbFormaPagamento.Text.Contains("WHATSAPP") ? true : false;

        }

        private bool FormaDePagamentoEhDebito()
        {
            if (cmbFormaPagamento.Text == "DÉBITO EM CONTA" || cmbFormaPagamento.Text.Contains("DCC") || cmbFormaPagamento.Text.Contains("DEBITO"))
                return true;
            else
                return false;
        }

        private void CarregarChecklist()
        {
            if (!AtendeRegrasDeGravacao(false)) return;

            _checklistAplicado = true;

            int idCampanha = _prospect.IdCampanha;
            int idProduto = Convert.ToInt32(cmbProduto.SelectedValue);
            string ddd = _prospect.Telefone01.ToString().Substring(0, 2);

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

                Checklist.ChecklistForm f = new Checklist.ChecklistForm(checklistSelecionado, this, (int)_oferta.IdTipoDeProduto, _camposDinamicos,_usuario);

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
                MessageBox.Show("Nenhum checklist disponível para a ofertaDoAtendimento.\nPode prosseguir com a gravação!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PreencherCampoDoEndereco(EnderecoDoProspect endereco)
        {
            foreach (Control item in gbEnderecoResidencial.Controls.OfType<TextBox>())
            {
                item.ResetText();
            }

            if (endereco != null)
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

        private bool AlterarValidacaoDeReproducaoDeGravacao(string chaveProduto, bool validado)
        {
            if (mapDeReproducoesGravacoes != null && mapDeReproducoesGravacoes.ContainsKey(chaveProduto))
            {
                mapDeReproducoesGravacoes[chaveProduto] = validado;
                return true;
            }
            return false;
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

                if (b != null)
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
                //txtAgencia.Resetar(habilitar: true, limparTexto: true, readOnly: false);
                //txtConta.Resetar(habilitar: true, limparTexto: true, readOnly: false);

                //txtAgencia.BackColor = SystemColors.InactiveBorder;
                //txtConta.BackColor = SystemColors.InactiveBorder;
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

            txtEmailFaturaDigital.Resetar(habilitar: false, limparTexto: true, readOnly: true);

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

        private void LiberarVendaCasada()
        {
            if (string.IsNullOrEmpty(txtLoginVendaCasada.Text))
            {
                MessageBox.Show("Para liberar Venda Casada. Informe um Login de Supervisor!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (string.IsNullOrEmpty(txtSenhaVendaCasada.Text))
            {
                MessageBox.Show("Para liberar Venda Casada. Informe uma Senha de Supervisor!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!string.IsNullOrEmpty(txtLoginVendaCasada.Text) && !string.IsNullOrEmpty(txtSenhaVendaCasada.Text))
            {
                Usuario supervisor = _atendimentoService.ValidarSupervisor(_usuario.IdSupervisor, txtLoginVendaCasada.Text, txtSenhaVendaCasada.Text).FirstOrDefault();

                if (supervisor != null)
                {
                    txtLoginVendaCasada.Text = string.Empty;
                    txtSenhaVendaCasada.Text = string.Empty;
                    cmbProduto2.Enabled = true;
                    btnPlayerProduto2.Enabled = true;
                    txtNumeroMigrado2.Enabled = true;
                    vendaSegundoProduto = true;
                }
                else
                {
                    MessageBox.Show("Você não tem permissão para executar essa ação!", "CallPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

        private void btnLiberarVendaCasada_Click(object sender, EventArgs e)
        {
            try
            {
                LiberarVendaCasada();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível liberar venda casada!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void OfertaMigracaoPreControleClaroForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!txtSenhaMplay.Focused &&
                !txtSenhaVendaCasada.Focused)
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

        private void cmbFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFormaPagamento.ForeColor = SystemColors.WindowText;
            lblBanco.ForeColor = SystemColors.WindowText;
            lblAgencia.ForeColor = SystemColors.WindowText;
            lblConta.ForeColor = SystemColors.WindowText;
            lblEmailFaturaDigital.ForeColor = SystemColors.WindowText;

            ConfiguracoesDeFormaDePagamento();
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
            //    txtEmailFaturaDigital.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            //    txtEmailFaturaDigital.BackColor = Color.WhiteSmoke;
            //}
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
            lblAgencia.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }


        private void txtAgencia_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgencia.Text) && txtAgencia.Text.Length < 4)
            {
                lblAgencia.ForeColor = Color.Red;
                MessageBox.Show("[Agência] inválida! O campo deve possuir 4 dígitos.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAgencia.Focus();
            }
        }

        private void txtConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblConta.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);

            if (_numeroCaracteresContaBancaria > 0)
            {
                int numeroDeCaracteresDoBanco = _numeroCaracteresContaBancaria;

                if (txtConta.Text.Replace("-", "").Length >= numeroDeCaracteresDoBanco && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
                //else //if(Texto.CaractereNumerico(e.KeyChar))
                //{
                //    int position = txtConta.SelectionStart;
                //    txtConta.Text = txtConta.Text.Replace("-", "");

                //    txtConta.SelectionStart = position;
                //    txtConta.SelectionLength = 0;
                //}
            }
        }

        private void cmbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConfiguracoesDeSelecaoDeBanco();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar o Banco!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtRg_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool permitirLetra = true;

            if (!permitirLetra)
            {
                if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                    MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                lblRg.ForeColor = SystemColors.WindowText;
                //e.Handled = char.IsWhiteSpace(e.KeyChar);
            }
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

        private void btnPlayerProduto1Indicacao_Click(object sender, EventArgs e)
        {
            if (cmbProduto.Text == "SELECIONE...")
                return;

            var loginAgente = _usuario.Login;
            var chaveNomeProduto = "ProdutoIndicacao1";
            var idCampanha = _prospect.IdCampanha;

            int idProduto = 0;

            if (int.TryParse(cmbProduto.SelectedValue.ToString(), out idProduto))
            {
                if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI || _discadorConectado.TipoDiscador == TipoDiscador.Olos)
                {
                    var arquivo = new CampanhaService().RetornarNomeDeArquivoDeAudioPorProduto(idProduto, idCampanha);

                    if (!string.IsNullOrEmpty(arquivo))
                    {
                        var player = new fPlayerAudioOlos(loginAgente, arquivo);
                        player.Reproduzir();

                        bool validado = player.EstadoDeReproducao == EstadoReproducao.Reproduzindo;
                        AlterarValidacaoDeReproducaoDeGravacao(chaveNomeProduto, validado);
                    }
                    else
                    {
                        MessageBox.Show("Não existe gravação cadastrada para o produto selecionado.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possível determinar o produto selecionado para reprodução.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPlayerDataVencimentoIndicacao_Click(object sender, EventArgs e)
        {
            if (cmbDiaDeVencimentoDaFatura.Text == "SELECIONE...")
                return;

            var loginAgente = _usuario.Login;
            int diaVencimentoFatura = 0;
            string chaveNomeAudio = "DiaVencimentoFaturaIndicacao";
            var idCampanha = _prospect.IdCampanha;

            if (int.TryParse(cmbDiaDeVencimentoDaFatura.Text, out diaVencimentoFatura))
            {
                if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI || _discadorConectado.TipoDiscador == TipoDiscador.Olos)
                {
                    var arquivo = new CampanhaService().RetornarNomeDeArquivoDeAudioPorDataVencimento(diaVencimentoFatura, idCampanha);

                    if (!string.IsNullOrEmpty(arquivo))
                    {
                        var player = new fPlayerAudioOlos(loginAgente, arquivo);
                        player.Reproduzir();

                        bool validado = player.EstadoDeReproducao == EstadoReproducao.Reproduzindo;
                        AlterarValidacaoDeReproducaoDeGravacao(chaveNomeAudio, validado);
                    }
                    else
                    {
                        MessageBox.Show("Não existe gravação cadastrada para o produto selecionado.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possível determinar o produto selecionado para reprodução.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPlayerProduto2_Click(object sender, EventArgs e)
        {
            if (cmbProduto2.Text == "SELECIONE...")
                return;

            var loginAgente = _usuario.Login;
            var chaveNomeProduto = "ProdutoIndicacao2";
            var idCampanha = _prospect.IdCampanha;

            int idProduto = 0;

            if (int.TryParse(cmbProduto2.SelectedValue.ToString(), out idProduto))
            {
                if (_discadorConectado.TipoDiscador == TipoDiscador.OlosAPI || _discadorConectado.TipoDiscador == TipoDiscador.Olos)
                {
                    var arquivo = new CampanhaService().RetornarNomeDeArquivoDeAudioPorProduto(idProduto, idCampanha);

                    if (!string.IsNullOrEmpty(arquivo))
                    {
                        var player = new fPlayerAudioOlos(loginAgente, arquivo);
                        player.Reproduzir();

                        bool validado = player.EstadoDeReproducao == EstadoReproducao.Reproduzindo;
                        AlterarValidacaoDeReproducaoDeGravacao(chaveNomeProduto, validado);
                    }
                    else
                    {
                        MessageBox.Show("Não existe gravação cadastrada para o produto selecionado.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possível determinar o produto selecionado para reprodução.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumeroMigrado2_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNumeroMigrado2.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void txtNumeroMigrado2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroMigrado2.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroMigrado2.Text))
                {
                    lblNumeroMigrado2.ForeColor = Color.Red;
                    txtNumeroMigrado2.Focus();
                    MessageBox.Show("[Número 2] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroMigrado2.ForeColor = SystemColors.WindowText;
            }
        }

        private void cmbProduto2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblProduto2.ForeColor = SystemColors.WindowText;
        }

        private void txtNumeroMigrado_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCpf.ForeColor = SystemColors.WindowText;
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
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

        private void OfertaMigracaoPreControleClaroForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_venda)
                Gravar(false);
        }

        private void txtNumeroMigrado_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroMigrado.Text))
            {
                if (!Texto.TelefoneCelularPossuiFormatoValido(txtNumeroMigrado.Text))
                {
                    lblNumeroMigrado.ForeColor = Color.Red;
                    txtNumeroMigrado.Focus();
                    MessageBox.Show("[Número] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                lblNumeroMigrado.ForeColor = SystemColors.WindowText;
            }
        }

        #endregion EVENTOS        

        private void txtRg_Leave(object sender, EventArgs e)
        {
            //TODO - chamado 14610 - retirar validação do RG na migração. Rei Almeida
            //Comentei o código desse IF abaixo
          /*  if (Convert.ToInt32(VerificaRGMaiorQueZero(txtRg.Text)) == 0)
            {
                MessageBox.Show("[RG] inválido!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRg.Focus();
                return;
            }*/

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
                    // Alterado de if (cont < 4) para  if (cont < 8) - Rei Almeida
                    if (cont < 8)
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

        private void cmbDesejaAparelho_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDesejaAparelho.ForeColor = SystemColors.WindowText;

            if (cmbOfertaAparelho.Text == "SIM")
                txtUrl.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            else
                txtUrl.Resetar(habilitar: false, limparTexto: true, readOnly: false);
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUrl.ForeColor = SystemColors.WindowText;
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

        private void txtConta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                return;

            if (_numeroCaracteresContaBancaria > 0)
            {
                int numeroDeCaracteresDoBanco = _numeroCaracteresContaBancaria;
                bool ultimaPosicao = false;

                int position = txtConta.SelectionStart;

                if (position == txtConta.Text.Length - 1 || position == txtConta.Text.Length)
                    ultimaPosicao = true;

                if (txtConta.Text.Replace("-", "").Length == numeroDeCaracteresDoBanco)
                {
                    txtConta.Text = txtConta.Text.Replace("-", "");

                    txtConta.Text =
                        txtConta.Text.Substring(0, numeroDeCaracteresDoBanco - 1) + "-" +
                        txtConta.Text.Substring(numeroDeCaracteresDoBanco - 1, 1);

                    if (ultimaPosicao)
                        txtConta.SelectionStart = position + 1;
                    else
                        txtConta.SelectionStart = position;

                    txtConta.SelectionLength = 0;
                }
                else
                {
                    txtConta.Text = txtConta.Text.Replace("-", "");
                    txtConta.SelectionStart = position;

                    txtConta.SelectionLength = 0;
                }
            }
        }

        private void txtConta_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConta.Text) && txtConta.Text.Replace("-", "").Length < _numeroCaracteresContaBancaria)
            {
                lblConta.ForeColor = Color.Red;
                MessageBox.Show("[Conta] inválida! O campo deve possuir " + _numeroCaracteresContaBancaria + " dígitos.", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConta.Focus();
            }
        }

    }
}