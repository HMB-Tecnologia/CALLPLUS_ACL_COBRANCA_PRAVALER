using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
    public partial class EnderecoForm : Form
    {
        public EnderecoForm(Usuario usuario, Prospect prospect)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _prospectService = new ProspectService();
            _enderecoService = new EnderecoService();
            _permissaoService = new PermissaoService();

            _prospect = prospect;
            _usuario = usuario;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly EnderecoService _enderecoService;
        private readonly ILogger _logger;

        private readonly PermissaoService _permissaoService;
        private readonly ProspectService _prospectService;
        private EnderecoDoProspect _enderecoDoProspectSelecionado;
        private Prospect _prospect;
        private Usuario _usuario;

        public EnderecoDoProspect EnderecoSelecionado { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtCep.Text))
                mensagens.Add("[CEP] deve ser informado!");

            if (!string.IsNullOrEmpty(txtCep.Text) && txtCep.Text.Length < 8)
                mensagens.Add("[CEP] inválido!");

            bool cepExiste = false;

            foreach (DataGridViewRow item in dgLista.Rows)
            {
                if (item.Cells["CEP"].Value.ToString() == txtCep.Text)
                {
                    if (_enderecoDoProspectSelecionado == null || item.Cells["CEP"].Value.ToString() != _enderecoDoProspectSelecionado.Cep)
                    {
                        cepExiste = true;
                        break;
                    }
                }
            }

            if (cepExiste)
                mensagens.Add("[CEP] já cadastrado!");

            if (string.IsNullOrEmpty(txtLogradouro.Text.Trim()))
                mensagens.Add("[Logradouro] deve ser informado!");

            if (string.IsNullOrEmpty(txtNumero.Text.Trim()))
                mensagens.Add("[Número] deve ser informado!");

            if (string.IsNullOrEmpty(txtBairro.Text.Trim()))
                mensagens.Add("[Bairro] deve ser informado!");

            if (string.IsNullOrEmpty(txtCidade.Text.Trim()))
                mensagens.Add("[Cidade] deve ser informada!");

            if (string.IsNullOrEmpty(cmbUf.Text) || cmbUf.TextoEhSelecione())
                mensagens.Add("[UF] deve ser informada!");

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void BuscaRapidaCep()
        {
            string cep = txtBuscaRapida.Text.Trim();

            if (!string.IsNullOrEmpty(cep))
            {
                if (cep.Length == 8)
                {
                    var endereco = _enderecoService.RetornarEndereco(cep, "", "", "", "", "").FirstOrDefault();

                    if (endereco != null)
                        endereco.Cep = endereco?.Cep?.PadLeft(8, '0');

                    ConfigurarCamposDeDetalhe(habilitarCampos: true, limparTexto: true);
                    CarregarRetornoDaBusca(endereco);
                }
            }
        }

        private void CancelarEdicao()
        {
            ResetarCamposDetalhe(habilitarCampos: false, limparTexto: true);
            BloquearCamposEdicao();

            _enderecoDoProspectSelecionado = null;

            tsEndereco_btnNovo.Enabled = true;
            tsEndereco_btnEditar.Enabled = false;
            tsEndereco_btnCancelar.Enabled = false;
            tsEndereco_btnSalvar.Enabled = false;
            tsEndereco_btnSelecionar.Enabled = false;

            dgLista.ClearSelection();
        }

        private void CarregarConfiguracaoInicial()
        {
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            dgLista.Rows.Clear();
            IEnumerable<EnderecoDoProspect> enderecos = _prospectService.ListarEnderecoDoProspect(_prospect.Id);

            foreach (EnderecoDoProspect endereco in enderecos)
            {
                int indice = dgLista.Rows.Add();

                dgLista.Rows[indice].Cells[nameof(id)].Value = endereco.Id;
                dgLista.Rows[indice].Cells[nameof(idProspect)].Value = endereco.IdProspect;
                dgLista.Rows[indice].Cells[nameof(cep)].Value = endereco.Cep;
                dgLista.Rows[indice].Cells[nameof(numero)].Value = endereco.Numero;
                dgLista.Rows[indice].Cells[nameof(bairro)].Value = endereco.Bairro;
                dgLista.Rows[indice].Cells[nameof(cidade)].Value = endereco.Cidade;
                dgLista.Rows[indice].Cells[nameof(uf)].Value = endereco.Uf;
                dgLista.Rows[indice].Cells[nameof(logradouro)].Value = endereco.Logradouro;
                dgLista.Rows[indice].Cells[nameof(pontoReferencia)].Value = endereco.PontoDeReferencia;
                dgLista.Rows[indice].Cells[nameof(complemento)].Value = endereco.Complemento;
            }

            dgLista.ClearSelection();
        }

        private void CarregarRetornoDaBusca(Endereco endereco)
        {
            if (endereco != null)
            {
                txtBuscaRapida.Resetar(habilitar: true, limparTexto: true, readOnly: false);

                txtCep.Text = endereco.Cep;

                if (string.IsNullOrEmpty(endereco.Logradouro))
                    txtLogradouro.Resetar(habilitar: true, limparTexto: false, readOnly: false);
                else
                    txtLogradouro.Text = endereco.Logradouro.ToUpper();

                if (string.IsNullOrEmpty(endereco.Bairro))
                    txtBairro.Resetar(habilitar: true, limparTexto: false, readOnly: false);
                else
                    txtBairro.Text = endereco.Bairro.ToUpper();

                txtCidade.Text = endereco.Cidade.ToUpper();
                cmbUf.Text = endereco.Estado.ToUpper();
            }
        }

        private void ExibirDetalhes(int linhaSelecionada)
        {
            if (linhaSelecionada < 0) return;

            ResetarCamposDetalhe(habilitarCampos: false, limparTexto: true);

            _enderecoDoProspectSelecionado = new EnderecoDoProspect();

            _enderecoDoProspectSelecionado.Id = (long)dgLista.Rows[linhaSelecionada].Cells[nameof(id)].Value;
            _enderecoDoProspectSelecionado.Cep = txtCep.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(cep)].Value?.ToString();
            _enderecoDoProspectSelecionado.Logradouro = txtLogradouro.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(logradouro)].Value?.ToString();
            _enderecoDoProspectSelecionado.Numero = txtNumero.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(numero)].Value?.ToString();
            _enderecoDoProspectSelecionado.Complemento = txtComplemento.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(complemento)].Value?.ToString();
            _enderecoDoProspectSelecionado.Bairro = txtBairro.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(bairro)].Value?.ToString();
            _enderecoDoProspectSelecionado.Cidade = txtCidade.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(cidade)].Value?.ToString();
            _enderecoDoProspectSelecionado.Uf = cmbUf.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(uf)].Value?.ToString();
            _enderecoDoProspectSelecionado.PontoDeReferencia = txtPontoReferencia.Text = dgLista.Rows[linhaSelecionada].Cells[nameof(pontoReferencia)].Value?.ToString();

            tsEndereco_btnNovo.Enabled = false;
            tsEndereco_btnEditar.Enabled = true;
            tsEndereco_btnCancelar.Enabled = true;
            tsEndereco_btnSalvar.Enabled = false;
            tsEndereco_btnSelecionar.Enabled = true;
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_enderecoDoProspectSelecionado == null)
                {
                    edicao = false;

                    _enderecoDoProspectSelecionado = new EnderecoDoProspect();

                    _enderecoDoProspectSelecionado.IdProspect = _prospect.Id;
                    _enderecoDoProspectSelecionado.IdCriador = _usuario.Id;
                }

                _enderecoDoProspectSelecionado.Cep = txtCep.Text.Trim();
                _enderecoDoProspectSelecionado.Logradouro = txtLogradouro.Text.Trim();
                _enderecoDoProspectSelecionado.Numero = txtNumero.Text.Trim();
                _enderecoDoProspectSelecionado.Complemento = txtComplemento.Text.Trim();
                _enderecoDoProspectSelecionado.Bairro = txtBairro.Text.Trim();
                _enderecoDoProspectSelecionado.Cidade = txtCidade.Text.Trim();
                _enderecoDoProspectSelecionado.Uf = cmbUf.Text;
                _enderecoDoProspectSelecionado.PontoDeReferencia = txtPontoReferencia.Text.Trim();

                _enderecoDoProspectSelecionado.Id = _prospectService.GravarEnderecoDoProspect(_enderecoDoProspectSelecionado);

                CarregarGrid();

                CancelarEdicao();

                MessageBox.Show("Endereço " + ((edicao) ? "atualizado" : "incluído") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void HabilitarPesquisa()
        {
            txtBuscaRapida.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            btnPesquisarEndereco.Enabled = true;
            btnBuscaRapida.Enabled = true;
        }

        private void HabitarCamposDeNovoEndereco()
        {
            txtNumero.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            txtComplemento.Resetar(habilitar: true, limparTexto: true, readOnly: false);
            txtPontoReferencia.Resetar(habilitar: true, limparTexto: true, readOnly: false);
        }

        private void IniciarEdicao()
        {
            ResetarCamposDetalhe(habilitarCampos: false, limparTexto: false);
            LiberarCamposEdicao();

            txtLoginLiberar.Resetar(habilitar: true, limparTexto: true);
            txtSenhaLiberar.Resetar(habilitar: true, limparTexto: true);
            txtLoginLiberar.ReadOnly = false;
            txtSenhaLiberar.ReadOnly = false;
            btnLiberarEdicaoManual.Enabled = true;

            tsEndereco_btnNovo.Enabled = false;
            tsEndereco_btnEditar.Enabled = false;
            tsEndereco_btnCancelar.Enabled = true;
            tsEndereco_btnSalvar.Enabled = true;
            tsEndereco_btnSelecionar.Enabled = false;
        }

        private void LiberarEdicaoManual()
        {
            var mensagensValidacao = _permissaoService.PodeLiberarEdicaoManual(txtLoginLiberar.Text, txtSenhaLiberar.Text, _usuario.Id);

            if (!mensagensValidacao.Any())
            {
                MessageBox.Show("Concluído!", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LiberarCamposEdicaoManual();
            }
            else
            {
                CallplusFormsUtil.ExibirMensagens(mensagensValidacao.ToList());
            }
        }

        private void NovoEndereco()
        {
            IniciarEdicao();
            HabilitarPesquisa();
            HabitarCamposDeNovoEndereco();
        }

        private void PesquisarEndereco()
        {
            Cep.ConsultaDeCepForm f = new Cep.ConsultaDeCepForm();

            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();

            ConfigurarCamposDeDetalhe(habilitarCampos: true, limparTexto: true);
            CarregarRetornoDaBusca(f._endereco);
        }

        private void ResetarCamposDetalhe(bool habilitarCampos, bool limparTexto)
        {
            ConfigurarCamposDeDetalhe(habilitarCampos, limparTexto);

            txtBuscaRapida.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitarCampos);
            btnBuscaRapida.Enabled = habilitarCampos;
            btnPesquisarEndereco.Enabled = habilitarCampos;

            //txtLoginLiberar.Resetar(habilitar: true, limparTexto: true, readOnly: habilitarCampos);
            // txtSenhaLiberar.Resetar(habilitar: true, limparTexto: true, readOnly: habilitarCampos);
            btnLiberarEdicaoManual.Enabled = !habilitarCampos;
        }

        private void ConfigurarCamposDeDetalhe(bool habilitarCampos, bool limparTexto)
        {
            //campos com edição bloqueada
            txtCep.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: true);
            txtLogradouro.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: true);
            txtNumero.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: true);
            txtBairro.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: true);
            txtCidade.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: true);
            txtBuscaRapida.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: true);

            if (limparTexto)
                cmbUf.ResetarComSelecione(habilitar: false);

            //Campos possiveis para edicao            
            txtComplemento.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: !habilitarCampos);
            txtNumero.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: !habilitarCampos);
            txtPontoReferencia.Resetar(habilitar: true, limparTexto: limparTexto, readOnly: !habilitarCampos);
        }

        private void Selecionar()
        {
            EnderecoSelecionado = _enderecoDoProspectSelecionado;

            this.Hide();
            this.Close();
        }
        #endregion METODOS

        #region EVENTOS

        private void BloquearCamposEdicao()
        {
            txtLoginLiberar.Resetar(habilitar: false, limparTexto: true);
            txtSenhaLiberar.Resetar(habilitar: false, limparTexto: true);
            btnLiberarEdicaoManual.Enabled = false;

            txtCep.ReadOnly = true;
            txtLogradouro.ReadOnly = true;
            txtNumero.ReadOnly = true;
            txtComplemento.ReadOnly = true;
            txtBairro.ReadOnly = true;
            txtCidade.ReadOnly = true;
            //cmbUf.ResetarComSelecione(habilitar: false);
            txtPontoReferencia.ReadOnly = true;
        }

        private void btnBuscaRapida_Click(object sender, EventArgs e)
        {
            try
            {
                //BuscaRapidaCep();
                BuscaCepClient(txtBuscaRapida.Text);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a busca rápida por CEP!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscaCepClient(string cepPesquisa)
        {
            bool verificaCepValido = false;

            Endereco enderecoCep = new Endereco();

            verificaCepValido = VerificarCepValido(cepPesquisa);

            Util.CorreiosActionline.CorreiosActionlineClient correiosActionlineClient = new Util.CorreiosActionline.CorreiosActionlineClient();

            if (verificaCepValido == true)
            {
                if (!string.IsNullOrEmpty(cepPesquisa.Trim()))
                {
                    List<Util.CorreiosActionline.RetornoCepActionline> listaDeEnderecos = correiosActionlineClient.ConsultarCep(cepPesquisa);

                    if (listaDeEnderecos.Count == 0)
                    {
                        MessageBox.Show("Nenhum endereço localizado para o CEP informado.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (listaDeEnderecos.Any(f => f.Cep == "-1"))
                    {
                        MessageBox.Show("Não foi possível recuperar os dados do CEP informado.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var enderecoLocalizado = listaDeEnderecos.First(x => x.Cep != "-1");

                    if (enderecoLocalizado == null) return;

                    enderecoCep.Cep = enderecoLocalizado.Cep;
                    enderecoCep.Bairro = enderecoLocalizado.Bairro;
                    enderecoCep.Cidade = enderecoLocalizado.Localidade;
                    enderecoCep.Logradouro = enderecoLocalizado.Logradouro ?? enderecoLocalizado.LogradouroaAbreviado;
                    enderecoCep.Estado = enderecoLocalizado.UF;
                    enderecoCep.Bairro = enderecoLocalizado.Bairro;

                    ConfigurarCamposDeDetalhe(habilitarCampos: true, limparTexto: true);
                    CarregarRetornoDaBusca(enderecoCep);
                }
            }
        }

        private bool VerificarCepValido(string cep)
        {
            if (cep.Length != 8)
            {
                MessageBox.Show("Cep inválido!", "Aviso do Sistema");
                return false;
            }
            else
                return true;
        }

        private void btnLiberarEdicaoManual_Click(object sender, EventArgs e)
        {
            try
            {
                LiberarEdicaoManual();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível liberar a edição do endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisarEndereco_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisarEndereco();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa de endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ExibirDetalhes(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível exibir os detalhes do endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnderecoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!txtSenhaLiberar.Focused)
            {
                if (Char.IsLower(e.KeyChar))
                    e.KeyChar = Char.ToUpper(e.KeyChar);

                if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
                {
                    e.Handled = true;
                }
            }
        }

        private void EnderecoForm_Load(object sender, EventArgs e)
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

        private void LiberarCamposEdicao()
        {
            txtLoginLiberar.ReadOnly = false;
            txtSenhaLiberar.ReadOnly = false;

            txtCep.ReadOnly = true;
            txtLogradouro.ReadOnly = true;
            txtNumero.ReadOnly = false;
            txtComplemento.ReadOnly = false;
            txtBairro.ReadOnly = true;
            txtCidade.ReadOnly = true;
            cmbUf.Desabilitar();
            txtPontoReferencia.ReadOnly = false;

            txtBuscaRapida.ReadOnly = false;
            btnBuscaRapida.Enabled = true;
            btnPesquisarEndereco.Enabled = true;

            txtLoginLiberar.Text = string.Empty;
            txtSenhaLiberar.Text = string.Empty;
        }

        private void LiberarCamposEdicaoManual()
        {
            txtLoginLiberar.ReadOnly = false;
            txtSenhaLiberar.ReadOnly = false;

            txtCep.ReadOnly = false;
            txtLogradouro.ReadOnly = false;
            txtNumero.ReadOnly = false;
            txtComplemento.ReadOnly = false;
            txtBairro.ReadOnly = false;
            txtCidade.ReadOnly = false;
            cmbUf.ResetarComSelecione(habilitar: true);
            txtPontoReferencia.ReadOnly = false;

            txtBuscaRapida.ReadOnly = false;
            btnBuscaRapida.Enabled = true;
            btnPesquisarEndereco.Enabled = true;

            txtLoginLiberar.Text = string.Empty;
            txtSenhaLiberar.Text = string.Empty;
        }

        private void tsEndereco_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição do endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsEndereco_btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicao();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsEndereco_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                NovoEndereco();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a inclusão de um novo endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsEndereco_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Gravar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsEndereco_btnSelecionar_Click(object sender, EventArgs e)
        {
            try
            {
                Selecionar();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível selecionar o endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        #endregion EVENTOS        
    }
}
