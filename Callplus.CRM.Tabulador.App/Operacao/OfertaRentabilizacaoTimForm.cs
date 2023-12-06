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

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class OfertaRentabilizacaoTimForm : Form
    {
        public OfertaRentabilizacaoTimForm(Usuario usuario, long idOferta, Prospect prospect, bool bloqueioStatus, bool fecharAoGravar, int? idStatusOferta = null, int? idCampanha = null, bool edicao = true)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _atendimentoService = new AtendimentoService();
            _campanhaService = new CampanhaService();
            _checklistService = new ChecklistService();
            _ofertaDoAtendimentoService = new OfertaDoAtendimentoService();
            _produtoService = new ProdutoService();
            _prospectService = new ProspectService();
            _statusDeOfertaService = new StatusDeOfertaService();
            _aparelhoService = new AparelhoService();

            _usuario = usuario;
            _oferta = _ofertaDoAtendimentoService.RetornarOfertaDoAtendimentoClaroRentabilizacao(idOferta);
            _prospect = prospect;
            _idStatusOferta = idStatusOferta;
            _bloqueioStatus = bloqueioStatus;
            _fecharAoGravar = fecharAoGravar;
            _permiteEditar = edicao;

            if (idCampanha != null)
                _campanhaAtual = _campanhaService.RetornarCampanha((int)idCampanha);

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly AtendimentoService _atendimentoService;
        private readonly AparelhoService _aparelhoService;
        private readonly CampanhaService _campanhaService;
        private readonly ChecklistService _checklistService;
        private readonly OfertaDoAtendimentoService _ofertaDoAtendimentoService;
        private readonly ProdutoService _produtoService;
        private readonly ProspectService _prospectService;
        private readonly StatusDeOfertaService _statusDeOfertaService;

        private Usuario _usuario;
        private Prospect _prospect;
        private OfertaDoAtendimentoClaroRentabilizacao _oferta;

        private int? _idStatusOferta;
        private bool _bloqueioStatus;
        private bool _fecharAoGravar;
        private bool _permiteEditar;
        private bool _editarDadosPessoais;
        private bool _editarEndereco;
        private bool _editarDadosProduto;
        private bool _editarDadosAparelho;
        private bool _editarDadosPagamento;
        private bool _checklistAplicado;
        private bool _editarDadosPassaporte;
        private Campanha _campanhaAtual;

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
            _editarDadosPassaporte = VerificarSeHabilitaOfertaPassaporte();

            if(_prospect.IdCampanha == 4 || _prospect.IdCampanha == 23 || _prospect.IdCampanha == 24)
            {
                _editarDadosPessoais = true;
                _editarEndereco = true;
                _editarDadosProduto = true;
                _editarDadosAparelho = true;
                _editarDadosPagamento = true;
            }

            gbDadosPessoais.Enabled = _editarDadosPessoais;
            gbEnderecoResidencial.Enabled = _editarEndereco;
            gbDadosOferta.Enabled = _editarDadosProduto;
            gbDadosAparelho.Enabled = _editarDadosAparelho;
            gbDadosPagamento.Enabled = _editarDadosPagamento;

            cmbPassaporteOferta.Enabled = _editarDadosPassaporte;

            Atualizar = false;
            tsOferta_cmbStatusOferta.Enabled = false;
            lIdade.Text = "";
            cmbFaturaDigital.ResetarComSelecione(habilitar: true);

            tsOferta_btnChecklist.Enabled = false;

            CarregarProduto();
            CarregarBanco();
            CarregarDiaDeVencimentoDaFatura();
            CarregarFaixaDeRenda();
            CarregarEstadoCivil();
            CarregarProfissao();
            CarregarFormaDePagamento();
            CarregarTipoDeStatusDeOferta();
            CarregarEConfigurarCombosVazios();
            CarregarDadosIniciais();

            if (_editarDadosPassaporte)
                CarregarPassaporteOferta();

            CarregarEConfigurarCamposDeAparelho(desejaAparelho: false, prospectId: _prospect.Id);

            if (_idStatusOferta != null && _idStatusOferta > 0)
            {
                ConfigurarStatusDaOferta(_idStatusOferta.Value);
            }

            CarregarControleDeEdicao();

            if (!Texto.CpfPossuiFormatoValido(txtCpf.Text))
            {
                txtCpf.ReadOnly = false;
                txtCpf.Enabled = true;
            }
        }

        private bool VerificarSeHabilitaOfertaPassaporte()
        {
            bool habilita = false;
            long idCampanha = _prospect.IdCampanha;

            habilita = (idCampanha == 2 || idCampanha == 4 || idCampanha == 9 || idCampanha == 6 || idCampanha == 23 || idCampanha == 24);

            return habilita;
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

        private void CarregarControleDeEdicao()
        {
            if (!_permiteEditar)
            {
                tsOferta.Enabled = false;
                txtObservacao.Enabled = false;
                btnSelecionarEndereco.Enabled = false;

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

                foreach (var item in gbDadosPagamento.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = true;
                }

                foreach (var item in gbDadosPagamento.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = false;
                }
            }
            else
            {
                gbDadosPessoais.Enabled = true;

                foreach (var item in gbDadosPessoais.Controls.OfType<TextBox>().Where(x => x.Name.Contains("txt")))
                {
                    item.ReadOnly = false;
                }

                foreach (var item in gbDadosPessoais.Controls.OfType<ComboBox>().Where(x => x.Name.Contains("cmb")))
                {
                    item.Enabled = true;
                }

                txtCpf.ReadOnly = false;
                txtCpf.Enabled = true;
                txtDataNascimento.Enabled = true;
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
            if (string.IsNullOrEmpty(_oferta.Nome))
            {
                txtNome.Text = CallplusFormsUtil.FormatarNomeRegraDaClaro(_prospect.Campo008 ?? "").ToUpper();
            }
            else
                txtNome.Text = _oferta.Nome;

            if (_oferta.Cpf == null)
            {
                txtCpf.Text = CallplusFormsUtil.FormatarCPF(_prospect.Campo009 ?? "");
            }
            else
                txtCpf.Text = CallplusFormsUtil.FormatarCPF(_oferta.Cpf?.ToString());

            if (DeveExibirCPF() == false)
            {
                txtCpf.Text = string.Empty;
            }

            txtRg.Text = _oferta.Rg;

            if (_oferta.Nascimento != null)
                txtDataNascimento.Text = _oferta.Nascimento.ToString();

            txtNomeDaMae.Text = _oferta.NomeDaMae ?? "";

            if (_oferta.TelefoneCelular != null)
                txtTelCelular.Text = _oferta.TelefoneCelular.ToString();

            if (_oferta.TelefoneResidencial != null)
                txtTelResidencial.Text = _oferta.TelefoneResidencial.ToString();

            if (_oferta.TelefoneRecado != null)
                txtTelRecado.Text = _oferta.TelefoneRecado.ToString();

            if (_oferta.IdEstadoCivil != null)
                cmbEstadoCivil.SelectedValue = _oferta.IdEstadoCivil.ToString();

            if (_oferta.IdProfissao != null)
                cmbProfissao.SelectedValue = _oferta.IdProfissao.ToString();

            if (_oferta.IdFaixaDeRenda != null)
                cmbFaixaRenda.SelectedValue = _oferta.IdFaixaDeRenda.ToString();

            if (_oferta.Cep != null)
                txtCep.Text = _oferta.Cep.ToString();

            txtLogradouro.Text = _oferta.Logradouro ?? "";
            txtNumero.Text = _oferta.Numero ?? "";
            txtComplemento.Text = _oferta.Complemento ?? "";
            txtBairro.Text = _oferta.Bairro ?? "";
            txtCidade.Text = _oferta.Cidade ?? "";
            txtUf.Text = _oferta.Uf ?? "";
            txtPontoReferencia.Text = _oferta.PontoDeReferencia ?? "";
            txtObservacao.Text = _oferta.Observacao ?? "";

            if (_oferta.IdProduto != null)
            {
                cmbProduto.SelectedValue = _oferta.IdProduto.ToString();

                if (cmbProduto.SelectedValue != null)
                    cmbProduto.Enabled = false;
                else
                    cmbProduto.ResetarComSelecione(habilitar: true);
            }

            txtNumeroMigrado.Text = _prospect.Telefone01.ToString();
            txtPossuiDepentende.Text = _prospect.Campo045 ?? "";
            txtTelefoneDoDepentende.Text = _prospect.Telefone02?.ToString() ?? "";

            txtValorPlanoSugerido.Text = _prospect.Campo021 ?? "";
            txtValorPlanoDesconto.Text = _prospect.Campo026 ?? "";
            txtPercentualDesconto.Text = _prospect.Campo022 ?? "";
            txtValorDaMulta.Text = _prospect.Campo039 ?? "";


            if (_oferta.FaturaDigital == true)
                cmbFaturaDigital.Text = "SIM";
            else if (_oferta.FaturaDigital == false)
                cmbFaturaDigital.Text = "NÃO";

            txtEmailFaturaDigital.Text = _oferta.EmailFaturaDigital ?? "";

            if (_oferta.DiaVencimento != null)
                cmbDiaDeVencimentoDaFatura.Text = _oferta.DiaVencimento?.ToString();

            if (_oferta.IdFormaDePagamento != null)
                cmbFormaPagamento.SelectedValue = _oferta.IdFormaDePagamento.ToString();

            if (_oferta.IdBanco != null)
                cmbBanco.SelectedValue = _oferta.IdBanco.ToString();

            txtAgencia.Text = _oferta.Agencia ?? "";
            txtConta.Text = _oferta.Conta ?? "";
        }

        private bool DeveExibirCPF()
        {
            bool deveExibirCpf = true;

            if((_prospect.IdCampanha == 4 || _prospect.IdCampanha == 23 || _prospect.IdCampanha == 24) && _permiteEditar)
                deveExibirCpf = false;

            return deveExibirCpf;
        }

        private void CarregarBanco()
        {
            IEnumerable<Banco> retorno = _campanhaService.ListarBancoDaCampanha(_prospect.IdCampanha, ativo: true);
            cmbBanco.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Codigo} - {x.Nome}");
        }

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

        private void CarregarFormaDePagamentoDeAparelho(int idProduto, int idAparelho)
        {
            IEnumerable<FormaDePagamentoDeAparelho> retorno = _aparelhoService.ListarFormaDePagamentoDeAparelho(idProduto, idAparelho);
            cmbFormaPagamentoAparelho.PreencherComSelecione(retorno, x => x.Id, x => x.Descricao);
        }

        private void CarregarAparelhosDoAtendimento(long idProspect)
        {
            IEnumerable<Aparelho> retorno = _aparelhoService.ListarAparelhosDoAtendimento(idProspect);
            cmbAparelho.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarPassaporteOferta()
        {
            IEnumerable<PassaporteOferta> retorno = _ofertaDoAtendimentoService.ListarPassaporteOferta();
            cmbPassaporteOferta.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
        }

        private void CarregarProfissao()
        {
            IEnumerable<Profissao> retorno = _prospectService.ListarProfissao(ativo: true);
            cmbProfissao.PreencherComSelecione(retorno, x => x.id, x => x.nome);
        }

        private void CarregarProduto()
        {
            bool ativo = true;
            bool? ativoBko = null;
            IEnumerable<ProdutoDaOfertaDto> produtos = _produtoService.ListarProdutoDaOferta(_oferta.IdAtendimento, ativo, ativoBko).Where(x => x.idTipo == _oferta.IdTipoDeProduto).Distinct();
            cmbProduto.PreencherComSelecione(produtos, x => x.idProduto, x => x.Produto);
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

            var mensagens = new List<string>();

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

                //if (CallplusFormsUtil.FormatarCPF(_prospect.Campo009) != CallplusFormsUtil.FormatarCPF(txtCpf.Text))
                //{
                //    lblCpf.ForeColor = Color.Red;
                //    mensagens.Add("[CPF] deve ser o mesmo informado no Mailing!");
                //}

                if (string.IsNullOrEmpty(cmbProduto.Text) || cmbProduto.TextoEhSelecione())
                {
                    lblProduto.ForeColor = Color.Red;
                    mensagens.Add("[Produto] deve ser informado!");
                }

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

                    if (!cmbDesejaAparelho.TextoEhSelecione())
                    {
                        if (cmbDesejaAparelho.Text == "SIM" && cmbAparelho.TextoEhSelecione())
                        {
                            lblAparelho.ForeColor = Color.Red;
                            mensagens.Add("[Aparelho] deve ser informado!");
                        }

                        if (cmbDesejaAparelho.Text == "SIM" && cmbFormaPagamentoAparelho.TextoEhSelecione())
                        {
                            lblParcela.ForeColor = Color.Red;
                            mensagens.Add("[Parcelas] deve ser informado!");
                        }
                    }
                }

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

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao(true))
            {
                _oferta.IdStatusDaOferta = Convert.ToInt32(tsOferta_cmbStatusOferta.ComboBox.SelectedValue);

                if (!cmbProduto.TextoEhSelecione())
                    _oferta.IdProduto = Convert.ToInt64(cmbProduto.SelectedValue);

                if (!string.IsNullOrEmpty(txtNumeroMigrado.Text))
                    _oferta.NumeroMigrado = Convert.ToInt64(txtNumeroMigrado.Text);

                if (!cmbFaturaDigital.TextoEhSelecione())
                    _oferta.FaturaDigital = (cmbFaturaDigital.Text == "SIM") ? true : false;

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

                if (!string.IsNullOrEmpty(cmbDesejaAparelho.Text) && cmbDesejaAparelho.Text == "NÃO")
                    _oferta.DesejaAparelho = false;

                if (!string.IsNullOrEmpty(cmbDesejaAparelho.Text) && cmbDesejaAparelho.Text == "SIM")
                {
                    int idAparelho = 0;
                    int idFormaPagamentoAparelho = 0;

                    _oferta.DesejaAparelho = true;

                    if (int.TryParse(cmbAparelho.SelectedValue.ToString(), out idAparelho))
                        _oferta.IdAparelho = idAparelho;

                    if (int.TryParse(cmbFormaPagamentoAparelho.SelectedValue.ToString(), out idFormaPagamentoAparelho))
                        _oferta.IdFormaDePagamentoAparelho = idFormaPagamentoAparelho;

                }

                //Dados Adicionais

                if (!string.IsNullOrEmpty(cmbPassaporteOferta.Text) && cmbPassaporteOferta.TextoEhSelecione() == false)
                {
                    int idPassaportOferta = int.Parse(cmbPassaporteOferta.SelectedValue.ToString());
                    _oferta.IdPassaporteOferta = idPassaportOferta;
                }

                _oferta.Observacao = txtObservacao.Text;

                _oferta.Id = _ofertaDoAtendimentoService.GravarOfertaDoAtendimentoClaroRentabilizacao(_oferta);

                MessageBox.Show("Oferta gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Atualizar = true;

                if (_fecharAoGravar)
                {
                    this.Close();
                }
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

                Checklist.ChecklistForm f = new Checklist.ChecklistForm(checklistSelecionado, this, (int)_oferta.IdTipoDeProduto, null,_usuario);

                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();

                _checklistAplicado = f._checklistRealizado;

                if (_checklistAplicado)
                {
                    tsOferta_cmbTipoStatusOferta.Enabled = false;
                    tsOferta_cmbStatusOferta.Enabled = false;
                    tsOferta_btnChecklist.Enabled = false;

                    gbDadosOferta.Enabled = false;
                    gbAdicionaisOferta.Enabled = false;
                    gbDadosPagamento.Enabled = false;
                    gbDadosPessoais.Enabled = false;
                    gbEnderecoResidencial.Enabled = false;
                    gbDadosAparelho.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Nenhum checklist disponível para a Oferta.\nPode prosseguir com a gravação!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CarregarEConfigurarCamposDeAparelho(bool desejaAparelho, long prospectId)
        {
            cmbAparelho.ResetarComSelecione(habilitar: desejaAparelho);
            cmbFormaPagamentoAparelho.ResetarComSelecione(habilitar: desejaAparelho);

            if (desejaAparelho)
                CarregarAparelhosDoAtendimento(idProspect: prospectId);
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

        #endregion METODOS

        #region EVENTOS

        private void OfertaRentabilizacaoClaroForm_Load(object sender, EventArgs e)
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

        private void cmbAparelho_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
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
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a ofertaDoAtendimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OfertaRentabilizacaoClaroForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

            if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
            {
                e.Handled = true;
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

        private void txtAgencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblAgencia.ForeColor = SystemColors.WindowText;
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void txtConta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            lblAgencia.ForeColor = SystemColors.WindowText;
        }

        private void cmbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDiaVencimento.ForeColor = SystemColors.WindowText;
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

        private void cmbPassaporteAmerica_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPassaporte.ForeColor = SystemColors.WindowText;
        }


        private void cmbDesejaAparelho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                bool desejaAparelho = cmbDesejaAparelho.Text == "SIM";
                lblDesejaAparelho.ForeColor = SystemColors.WindowText;

                CarregarEConfigurarCamposDeAparelho(desejaAparelho, _prospect.Id);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show($"Não foi possível Carregar os aparelhos. \n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            lblRg.ForeColor = SystemColors.WindowText;
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
