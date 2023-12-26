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
	public partial class AuditoriaAcordo_CobrancaPravalerForm : Form
    {
        public AuditoriaAcordo_CobrancaPravalerForm(long idOferta)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _atendimentoService = new AtendimentoService();
            _campanhaService = new CampanhaService();
            _acodoDoAtendimentoService = new AcordoDoAtendimentoService();
            _produtoService = new ProdutoService();
            _prospectService = new ProspectService();
            _statusDeAuditoriaService = new StatusDeAuditoriaService();
            _layoutDinamicoService = new LayoutDinamicoService();
            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            _usuarioLogado = AdministracaoMDI._usuario;
            _tipos = new TipoDeAvaliacaoDeAtendimento();

            _oferta = _acodoDoAtendimentoService.RetornarAcordoDoAtendimentoCobrancaPravalerBKO(idOferta);
            _resumoDoAcordo = _acodoDoAtendimentoService.RetornarAcordoDoAtendimentoBKO(idOferta, (int)_oferta.idTipoDeProduto);

            _campanha = _campanhaService.RetornarCampanha(_oferta.idCampanha);

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly AtendimentoService _atendimentoService;
        private readonly CampanhaService _campanhaService;
        private readonly ProdutoService _produtoService;
        private readonly ProspectService _prospectService;
        private readonly AcordoDoAtendimentoService _acodoDoAtendimentoService;
        private readonly StatusDeAuditoriaService _statusDeAuditoriaService;
        private readonly LayoutDinamicoService _layoutDinamicoService;
        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;
        private AvaliacaoDeAtendimento _avaliacaoDeAtendimento;
        private Tabulador.Dominio.Entidades.StatusDeAuditoria _statusDeAuditoriaAtual;
        private readonly Usuario _usuarioLogado;
        private bool _ofertaFoiAtualizada;
        private readonly TipoDeAvaliacaoDeAtendimento _tipos;
        private readonly Campanha _campanha;
        private OfertaDoAtendimentoCobrancaPravalerBKO _oferta;
        private ResumoDoAcordoDoAtendimentoBkoDTO _resumoDoAcordo;
        private HistoricoDoAcordoDoAtendimentoCobrancaPravalerBKO _historicoOfertaBKO;

        private bool _filtraPorFaixaDeRecarga;


        public bool Atualizar { get; set; }

        private int _idProdutoInicial = 0;

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            cmbStatusAuditoria.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            Atualizar = false;
            lIdade.Text = "";
            _filtraPorFaixaDeRecarga = false;

            CarregarProduto();
            CarregarStatusDeAuditoria();
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

        private void CarregarRegrasDoPerfil()
        {
            if (AdministracaoMDI._usuario.IdPerfil != 1 && AdministracaoMDI._usuario.IdPerfil != 4 && AdministracaoMDI._usuario.IdPerfil != 6)
            {
                gbDadosPessoais.Enabled = false;
                gbDadosOferta.Enabled = false;
                cmbStatusAuditoria.Enabled = false;               
                txtObservacao.Enabled = false;
                btnSalvar.Enabled = false;
            }
        }

        private void IniciarAuditoriaDeOferta(Tabulador.Dominio.Entidades.StatusDeAuditoria statusAtual, int idUsuario, long idOfertaBko)
        {
            if (statusAtual.HabilitaTrocaDeStatus)
            {
                _historicoOfertaBKO = _acodoDoAtendimentoService.IniciarAuditoriaDaOfertaClaroMigracaoBKO(idUsuario, idOfertaBko);
            }
        }

        private void ConfigurarTelaDeAcordoComStatusAuditoria(Tabulador.Dominio.Entidades.StatusDeAuditoria statusDeAuditoria)
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
            txtCodigoOferta.Text = _resumoDoAcordo.idAcordo.ToString();
            txtCampanha.Text = _resumoDoAcordo.campanha;
            txtMailing.Text = _resumoDoAcordo.mailing;
            txtOperador.Text = _resumoDoAcordo.operador;
            txtSupervisor.Text = _resumoDoAcordo.supervisor;
            txtStatusOferta.Text = _resumoDoAcordo.statusOferta;
            txtDataTabulacao.Text = _resumoDoAcordo.dataRegistroAcordo?.ToString("dd/MM/yyyy HH:mm:ss");
            cmbStatusAuditoria.SelectedValue = _resumoDoAcordo?.IdStatusAuditoria.ToString();

            txtUltimoAuditor.Text = _resumoDoAcordo.auditor;
            txtUltimoStatusAuditoria.Text = _resumoDoAcordo.statusAuditoria;
            txtUltimoDataAuditoria.Text = _resumoDoAcordo.dataAuditoria?.ToString("dd/MM/yyyy HH:mm:ss");
            txtUltimaObservacao.Text = _resumoDoAcordo.Observacao;
        }

        private void CarregarDadosDaOferta()
        {
            txtNome.Text = _oferta.nome;

            if (!string.IsNullOrEmpty(_oferta.cpf?.ToString()))
            txtCpf.Text = CallplusFormsUtil.FormatarCPF(_oferta.cpf?.ToString());

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

            txtObservacao.Text = _oferta.observacao ?? "";

            if (_oferta.idProduto != null)
            {
                cmbProduto.SelectedValue = _oferta.idProduto.ToString();

                _idProdutoInicial = (int)_oferta.idProduto;

                if (cmbProduto.SelectedValue == null)
                    cmbProduto.ResetarComSelecione(habilitar: true);
            }
        }

        private void CarregarStatusDeAuditoria()
        {
            IEnumerable<Tabulador.Dominio.Entidades.StatusDeAuditoria> retorno = _statusDeAuditoriaService.Listar(_oferta.idCampanha, ativo: true);
            cmbStatusAuditoria.PreencherComSelecione(retorno, x => x.Id, x => x.Nome);
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
                _acodoDoAtendimentoService.RemoverHistoricoDeOfertaBkoPendente(idUsuario, idOfertaBko, idTipoOferta);
            }
        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();
            int idStatusAuditoria = 0;
            Tabulador.Dominio.Entidades.StatusDeAuditoria statusDeAuditoria = new Tabulador.Dominio.Entidades.StatusDeAuditoria();

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
            if (!statusDeAuditoria.PermitidoHumano)
                mensagens.Add("[Status de Auditoria] não permitido!");

            if (_statusDeAuditoriaAtual != null && _statusDeAuditoriaAtual.HabilitaTrocaDeStatus == false)
            {
                mensagens.Add("[Status de Auditoria] atual da oferta não permite alteração de Status!");
                CallplusFormsUtil.ExibirMensagens(mensagens);
                return false;
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDaOferta()
        {   
            foreach (var item in gbDadosPessoais.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
            {
                item.ForeColor = SystemColors.WindowText;
            }

            foreach (var item in gbDadosOferta.Controls.OfType<Label>().Where(x => x.Name.Contains("lbl")))
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

            if (string.IsNullOrEmpty(cmbProduto.Text) || cmbProduto.TextoEhSelecione())
            {
                lblProduto.ForeColor = Color.Red;
                mensagens.Add("[Produto] deve ser informado!");
            }

            if (_oferta.idTipoDeProduto != 2)
            {
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
                    _historicoOfertaBKO = new HistoricoDoAcordoDoAtendimentoCobrancaPravalerBKO();
                }

                _historicoOfertaBKO.idAcodoDoAtendimentoCobrancaPravalerBKO = (long)_oferta.id;
                _historicoOfertaBKO.idStatusAuditoria = Convert.ToInt32(cmbStatusAuditoria.SelectedValue);
                _historicoOfertaBKO.idCriador = AdministracaoMDI._usuario.Id;
                _historicoOfertaBKO.Observacao = txtObservacao.Text;

                _historicoOfertaBKO.id = _acodoDoAtendimentoService.GravarHistoricoDoAtendimentoCobrancaPravalerBKO(_historicoOfertaBKO);
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
              
                if (!cmbSexo.TextoEhSelecione())
                    _oferta.Sexo = cmbSexo.Text;

                if (!string.IsNullOrEmpty(txtNome.Text))
                    _oferta.nome = txtNome.Text;

                if (!string.IsNullOrEmpty(txtCpf.Text))
                    _oferta.cpf = Convert.ToInt64(txtCpf.Text);

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
               
                if (!string.IsNullOrEmpty(txtObservacao.Text))
                    _oferta.observacao = txtObservacao.Text;

                _oferta.id = _acodoDoAtendimentoService.GravarOfertaDoAtendimentoClaroMigracaoBKO(_oferta);

                if(_idProdutoInicial != 0 && (_idProdutoInicial != _oferta.idProduto))
                    _oferta.id = _acodoDoAtendimentoService.GravarAlteracaoDeProdutoMigracaoBKO((long)_oferta.id, _idProdutoInicial, (int)_oferta.idProduto, _usuarioLogado.Id);
            }
            else
            {
                result = false;
            }

            return result;
        }

        private void CarregarHistoricoDeAuditoria()
        {
            try
            {
                long idOfertaBko = _oferta.id.Value;
                var hist = _acodoDoAtendimentoService.ListarHistoricoDaOfertaDoAtendimentoBKO(idOfertaBko, (int)_oferta.idTipoDeProduto);
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
            var historico = _acodoDoAtendimentoService.RetornarHistoricoDaOfertaDoAtendimentoBKO_DTO(idHistoricoBko, (int)_oferta.idTipoDeProduto);

            if (historico != null)
            {
                txtAuditor_historico.Text = historico.Auditor;
                txtStatusAuditoria_historico.Text = historico.StatusDeAuditoria;
                txtDataAuditoria_historico.Text = historico.DataInput?.ToString("dd/MM/yyyy HH:mm:ss");
                txtObservacoes_historico.Text = historico.Observacao;
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
                Usuario usuario = _acodoDoAtendimentoService.ValidarUsuarioPermitidoParaAlterarProduto(txtLoginProduto.Text, txtSenhaProduto.Text).FirstOrDefault();

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

        private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblProduto.ForeColor = SystemColors.WindowText;
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

        #endregion EVENTOS 
    }
}
