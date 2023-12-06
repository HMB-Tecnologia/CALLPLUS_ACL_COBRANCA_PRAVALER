using Callplus.CRM.Tabulador.App.Util.CorreiosActionline;
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

namespace Callplus.CRM.Tabulador.App.Cep
{
    public partial class ConsultaDeCepForm : Form
    {
        public ConsultaDeCepForm(Campanha campanha, string telefone)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _enderecoService = new EnderecoService();
            _retornoCep = new RetornoCep();
            _correioService = new RetornoDeCepService();

            _campanha = campanha;
            _telefone = telefone;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly EnderecoService _enderecoService;
        private readonly Campanha _campanha;
        RetornoCep _retornoCep;
        private readonly string _telefone;
        private readonly RetornoDeCepService _correioService;
        private IEnumerable<CepCorreios> _retornarLogradouros;
        private IEnumerable<CepCorreios> _retornarEnderecos;
        private IEnumerable<CepCorreios> _retornarCidades;
        private IEnumerable<CepCorreios> _retornarBairros;
        private IEnumerable<CepCorreios> _retornarCeps;
        private static string Selecione = "SELECIONE...";
        private string _uf;
        private string[] _cep;
        private string[] _logradouro;
        private string[] _bairro;
        private string[] _cidade;
        private string[] _selecione = new string[] { Selecione };

        public AutoCompleteMode AutoCompleteMode { get; set; }

        public Endereco _endereco { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private string CorrigirFormatoCep(string cep)
        {
            while (cep.Length < 8) cep = "0" + cep;
            return cep;
        }

        private void CarregarConfiguracaoInicial()
        {
            ResetarDropDown();

            AjustesComboBox();

            if (_campanha.idTipoDaCampanha == 2)
            {
                string uf = RetornarUfDeAcordoComDdd();

                RetornoCep(uf);
            }
            else
            {
                IEnumerable<SiglaUfBrasil> retorno = _correioService.ListarUfBrasil(true, null);
                cmbUF.PreencherComSelecione(retorno, x => x.Id, x => x.nome);
            }
        }

        private void RetornoCep(string uf)
        {
            int tipodepesquisa = 5;
            _retornarCeps = _correioService.RetornarEndereco(null, uf, null, null, null, tipodepesquisa);
            CarregarEnderecoEmMemoria(_retornarCeps);

            //int tipodepesquisa = 2;
            //_retornarEnderecos = _correioService.RetornarEndereco(null, uf, null, null, null, tipodepesquisa);
            //if (_retornarEnderecos != null)
            //    CarregarEnderecoEmMemoria(_retornarEnderecos);
        }

        private void AjustesComboBox()
        {
            cmbCidade.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;
            cmbBairro.DropDown += CallplusFormsUtil.AjustarTamanhoDoDropDownNoComboBox;

            cmbBairro.SelectedIndex = cmbBairro.Items.IndexOf(Selecione);
            cmbCidade.SelectedIndex = cmbCidade.Items.IndexOf(Selecione);

            cmbUF.MouseWheel += ComboBox_MouseWheel;
        }

        private void CarregarEnderecoEmMemoria(IEnumerable<CepCorreios> enderecos)
        {
            string cep = null;
            string bairro = null;
            string cidade = null;
            string logradouro = null;

            foreach (var item in enderecos)
            {
                cep = item.Cep;
                bairro = item.Bairro;
                cidade = item.Cidade;
                logradouro = item.Logradouro;
            }

            if (!string.IsNullOrEmpty(cep))
            {
                _cep = enderecos.Select(x => x.Cep).Distinct().Where(x => !x.Equals(string.Empty)).OrderBy(x => x).ToArray();

                var cepAuto = new AutoCompleteStringCollection();
                cepAuto.AddRange(_cep);
                txtCep.AutoCompleteCustomSource = cepAuto;
                txtCep.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCep.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

            if (!string.IsNullOrEmpty(logradouro))
            {
                _logradouro = enderecos.Select(x => x.Logradouro).Distinct().Where(x => !x.Equals(string.Empty)).OrderBy(x => x).ToArray();

                var logradouroAuto = new AutoCompleteStringCollection();
                logradouroAuto.AddRange(_logradouro);
                txtLogradouro.AutoCompleteCustomSource = logradouroAuto;
                txtLogradouro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtLogradouro.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }

            if (!string.IsNullOrEmpty(bairro))
            {
                _bairro = enderecos.Select(x => x.Bairro).Distinct().Where(x => !x.Equals(string.Empty)).OrderBy(x => x).ToArray();
                _bairro = _bairro.Union(_selecione).ToArray();

                var bairroAuto = new AutoCompleteStringCollection();
                cmbBairro.DataSource = _bairro;
                bairroAuto.AddRange(_bairro);
                cmbBairro.AutoCompleteCustomSource = bairroAuto;
                cmbBairro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBairro.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbBairro.SelectedIndex = cmbBairro.Items.IndexOf(Selecione);
            }

            if (!string.IsNullOrEmpty(cidade))
            {
                _cidade = enderecos.Select(x => x.Cidade).Distinct().Where(x => !x.Equals(string.Empty)).OrderBy(x => x).ToArray();
                _cidade = _cidade.Union(_selecione).ToArray();

                var cidadeAuto = new AutoCompleteStringCollection();
                cmbCidade.DataSource = _cidade;
                cidadeAuto.AddRange(_cidade);
                cmbCidade.AutoCompleteCustomSource = cidadeAuto;
                cmbCidade.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbCidade.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbCidade.SelectedIndex = cmbCidade.Items.IndexOf(Selecione);
            }
        }

        public string RetornarUfDeAcordoComDdd()
        {
            string DDD = string.Empty;
            if (!string.IsNullOrEmpty(_telefone))
                DDD = (_telefone).Substring(0, 2);

            int ddd = 0;
            int.TryParse(DDD, out ddd);
            string uf = _enderecoService.RetornarUf(ddd, _campanha.Id);

            IEnumerable<SiglaUfBrasil> retorno = _correioService.ListarUfBrasil(true, uf);
            cmbUF.PreencherComSelecione(retorno, x => x.Id, x => x.nome);
            //cmbUF.Text = uf;

            return uf;
        }

        private void PesquisarCep(int tipodePesquisa)
        {
            string cep = string.IsNullOrEmpty(txtCep.Text) ? null : txtCep.Text;
            string uf = cmbUF.TextoEhSelecione() ? null : cmbUF.Text;
            string cidade = cmbCidade.TextoEhSelecione() ? null : cmbCidade.Text;
            string bairro = cmbBairro.TextoEhSelecione() ? null : cmbBairro.Text;
            string logradouro = string.IsNullOrEmpty(txtLogradouro.Text) ? null : txtLogradouro.Text;

            BuscarEndereco(cep, uf, cidade, bairro, logradouro, tipodePesquisa);
        }

        private bool AtendeRegrasPesquisa(int tipoPesquisa)
        {
            var mensagens = new List<string>();

            if (tipoPesquisa == 1)
            {
                if (string.IsNullOrEmpty(txtCep.Text.Trim()) || txtCep.Text.Length != 8)
                    mensagens.Add("[CEP] inválido!");

                if (cmbUF.TextoEhSelecione() || string.IsNullOrEmpty(cmbUF.Text))
                    mensagens.Add("[UF] deve ser informado!");
            }
            else
            {
                if (cmbUF.TextoEhSelecione() || string.IsNullOrEmpty(cmbUF.Text))
                {
                    mensagens.Add("[UF] deve ser informado!");
                }

                if (cmbCidade.TextoEhSelecione() || string.IsNullOrEmpty(cmbCidade.Text))
                {
                    mensagens.Add("[Cidade] deve ser informado!");
                }

                //if (string.IsNullOrEmpty(txtLogradouro.Text.Trim()) || txtLogradouro.Text == Selecione)
                //{
                //    mensagens.Add("[Logradouro] deve ser informado.");
                //}
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return mensagens.Any() == false;
        }

        private void BuscarEndereco(string cep, string uf, string cidade, string bairro, string logradouro, int tipodepesquisa)
        {
            IEnumerable<CepCorreios> enderecos = _correioService.RetornarEndereco(cep, uf, cidade, bairro, logradouro, tipodepesquisa);

            if (enderecos != null)
            {
                dgEndereco.DataSource = enderecos;

                if (enderecos.Count() == 0)
                    MessageBox.Show(
                        $"Nenhum endereço localizado para {(tipodepesquisa == 1 ? "o dado informado" : "os dados informados")} !", "Mensagem do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RealizarAjusteGrid();
            }
            else
            {
                MessageBox.Show(
                $"Não foi possível carregar o CEP do cliente.\nUtilize a pesquisa ou cadatro manual.\n", "Mensagem do sistema!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RealizarAjusteGrid()
        {
            dgEndereco.Columns["UF"].Width = 35;
            dgEndereco.Columns["CEP"].Width = 75;
            dgEndereco.Columns["Id"].Visible = false;
            dgEndereco.Columns["CidadeAbreviado"].Visible = false;
            dgEndereco.Columns["BairroAbreviado"].Visible = false;
            dgEndereco.Columns["LogradouroAbreviado"].Visible = false;
            dgEndereco.Columns["ComplementoLogradouro"].Visible = false;
            dgEndereco.Columns["Nome"].Visible = false;
            dgEndereco.Columns["NomeAbreviado"].Visible = false;
        }

        public void ConfiguraTipoDePesquisa()
        {
            ResetarDropDown();

            dgEndereco.DataSource = null;

            bool filtroDePesquisaEhPorCep = rbPesquisaCep.Checked;
            bool filtroDePesquisaEhPorEndereco = rbPesquisaEndereco.Checked;

            if (filtroDePesquisaEhPorCep)
            {
                cmbUF.Enabled = filtroDePesquisaEhPorCep;
                cmbCidade.Enabled = filtroDePesquisaEhPorEndereco;
                cmbBairro.Enabled = filtroDePesquisaEhPorEndereco;
                txtLogradouro.Enabled = filtroDePesquisaEhPorEndereco;
                txtCep.Enabled = filtroDePesquisaEhPorCep;
                lklLimpar.Enabled = true;
                lblCep.Text = "CEP *";
                lblCidade.Text = "Cidade ";
            }

            if (filtroDePesquisaEhPorEndereco)
            {
                txtCep.Enabled = !filtroDePesquisaEhPorEndereco;
                cmbUF.Enabled = filtroDePesquisaEhPorEndereco;
                cmbBairro.Enabled = filtroDePesquisaEhPorEndereco;
                txtLogradouro.Enabled = filtroDePesquisaEhPorEndereco;
                cmbCidade.Enabled = filtroDePesquisaEhPorEndereco;
                lklLimpar.Enabled = true;
                lblCep.Text = "CEP ";
                lblCidade.Text = "Cidade *";
            }

            if (filtroDePesquisaEhPorCep)
                ResetarCamposEndereco();
            else
                ResetarCamposCep();

        }

        private void ResetarCamposEndereco()
        {
            txtCep.Text = string.Empty;
            cmbUF.Text = Selecione;
            cmbCidade.Text = Selecione;
            cmbBairro.Text = Selecione;
            txtLogradouro.Text = string.Empty;
        }

        private void ResetarCamposCep()
        {
            txtCep.Text = string.Empty;
            cmbUF.Text = Selecione;
            cmbCidade.Text = Selecione;
            cmbBairro.Text = Selecione;
            txtLogradouro.Text = string.Empty;
        }

        private void CarregarCidades()
        {
            if (string.IsNullOrEmpty(cmbUF.Text.Trim()) || cmbUF.TextoEhSelecione()) return;

            string[] city = new string[] { };
            string uf = cmbUF.Text;
            //int tipodepesquisa;

            if (_retornarEnderecos != null)
            {
                string[] cidade = _retornarEnderecos.Select(x => x.Cidade).Distinct().OrderBy(x => x).ToArray();

                city = cidade;
                city = city.Union(_selecione).ToArray();
                cmbCidade.DataSource = city;
                cmbCidade.Text = Selecione;
                txtLogradouro.Text = string.Empty;
            }
            else
            {
                if (_campanha.idTipoDaCampanha == 2)
                {
                    //tipodepesquisa = 2;

                    if (_retornarCidades == null)
                    {
                        if (rbPesquisaEndereco.Checked)
                        {
                            _retornarCidades = _correioService.RetornarEndereco(null, uf, null, null, null, 3);
                            CarregarEnderecoEmMemoria(_retornarCidades);
                        }

                        //_retornarEnderecos = _correioService.RetornarEndereco(null, uf, null, null, null, tipodepesquisa);
                        //_uf = _retornarEnderecos.Select(x => x.UF).ToArray().First();

                        //CarregarEnderecoEmMemoria(_retornarEnderecos);
                    }

                    txtLogradouro.Text = string.Empty;
                    dgEndereco.DataSource = null;
                }
                else if (_campanha.idTipoDaCampanha == 1)
                {
                    if (rbPesquisaCep.Checked)
                    {
                        if (_retornarCeps == null)
                        {
                            _retornarCeps = _correioService.RetornarEndereco(null, uf, null, null, null, 5);
                            CarregarEnderecoEmMemoria(_retornarCeps);
                        }
                        else if (cmbUF.Text != _uf)
                        {
                            _retornarCeps = _correioService.RetornarEndereco(null, uf, null, null, null, 5);
                            CarregarEnderecoEmMemoria(_retornarCeps);
                        }

                        _uf = cmbUF.Text;
                    }
                    else
                    {
                        _retornarCidades = _correioService.RetornarEndereco(null, uf, null, null, null, 3);
                        CarregarEnderecoEmMemoria(_retornarCidades);
                    }
                }
            }
        }

        private void CarregarBairros()
        {
            _selecione = new string[] { Selecione };
            string cidade = cmbCidade.SelectedValue != null ? cmbCidade.SelectedValue.ToString() : cmbCidade.Text;

            if (string.IsNullOrEmpty(cidade) || cidade == Selecione) return;

            if (_retornarEnderecos == null)
            {
                int tipodepesquisa = 4;
                _retornarBairros = _correioService.RetornarEndereco(null, cmbUF.Text, cidade, null, null, tipodepesquisa);
                string[] bairro = _retornarBairros.Select(x => x.Bairro).Where(x => !x.Equals(string.Empty)).Distinct().OrderBy(x => x).ToArray();
                bairro = bairro.Union(_selecione).ToArray();
                cmbBairro.DataSource = bairro;
            }
            else
            {
                string[] bairro = _retornarEnderecos.Where(x => x.Cidade.Equals(cidade) && !x.Equals(string.Empty))
                    .Select(x => x.Bairro).Where(x => !x.Equals(string.Empty)).Distinct().OrderBy(x => x).ToArray();

                bairro = bairro.Union(_selecione).ToArray();
                cmbBairro.DataSource = bairro;
            }

            cmbBairro.Text = Selecione;
            txtLogradouro.Text = string.Empty;
        }

        private void CarregarLogradouros()
        {
            txtLogradouro.Text = string.Empty;
            string[] logradouro;
            string bairro = cmbBairro.SelectedValue != null ? cmbBairro.SelectedValue.ToString() : cmbBairro.Text;
            string cidade = cmbCidade.SelectedValue != null ? cmbCidade.SelectedValue.ToString() : cmbCidade.Text;

            if (string.IsNullOrEmpty(bairro) || bairro == Selecione) return;

            if (_retornarLogradouros == null)
            {
                _retornarLogradouros = _correioService.RetornarEndereco(null, cmbUF.Text, cidade, bairro, null, 6);
            }

            if (_retornarEnderecos != null)
            {
                logradouro = _retornarEnderecos.Where(x => x.Bairro.Equals(bairro) && x.Cidade.Equals(cidade))
                    .Select(x => x.Logradouro).Where(x => !x.Equals(string.Empty)).Distinct().ToArray();
            }
            else
            {
                logradouro = _retornarLogradouros.Where(x => x.Bairro.Equals(bairro))
                    .Select(x => x.Logradouro).Where(x => !x.Equals(string.Empty)).Distinct().ToArray();
            }

            if (logradouro.Length > 0)
            {
                var logradouroAuto = new AutoCompleteStringCollection();
                logradouroAuto.AddRange(logradouro);
                txtLogradouro.AutoCompleteCustomSource = logradouroAuto;
                txtLogradouro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtLogradouro.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void ResetarCampos()
        {
            txtCep.Text = string.Empty;
            cmbCidade.Text = Selecione;
            cmbBairro.Text = Selecione;
            txtLogradouro.Text = string.Empty;
            dgEndereco.DataSource = null;
        }

        private void ResetarDropDown()
        {
            cmbCidade.DropDownStyle = ComboBoxStyle.DropDown;
            cmbBairro.DropDownStyle = ComboBoxStyle.DropDown;
        }

        #endregion METODOS

        #region EVENTOS

        private void ConsultaDeCepForm_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarConfiguracaoInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar corretamente a pesquisa de CEP!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (_endereco != null)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Nenhum CEP disponível para selecionar!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                bool pesquisaCep = rbPesquisaCep.Checked;
                bool pesquisaEnd = rbPesquisaEndereco.Checked;
                int tipodePesquisa = pesquisaCep ? 1 : 2;

                if (AtendeRegrasPesquisa(tipodePesquisa))
                {
                    PesquisarCep(tipodePesquisa);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível consultar o endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbUF_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate { CarregarCidades(); });

                ResetarCampos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as cidades!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCidade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate { CarregarBairros(); });          
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os bairros!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBairro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate { CarregarLogradouros(); });               
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os logradouros!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgEndereco_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int linha = e.RowIndex;

                    if (_endereco == null)
                        _endereco = new Endereco();

                    string cep = dgEndereco.Rows[linha].Cells["Cep"].Value.ToString();
                    cep = CorrigirFormatoCep(cep);

                    string Bairro = dgEndereco.Rows[linha].Cells["Bairro"].Value.ToString();
                    string Cidade = dgEndereco.Rows[linha].Cells["Cidade"].Value.ToString();
                    string Logradouro = dgEndereco.Rows[linha].Cells["Logradouro"].Value.ToString();
                    string Uf = dgEndereco.Rows[linha].Cells["Uf"].Value.ToString();

                    _endereco.Cep = cep;
                    _endereco.Estado = Uf;
                    _endereco.Cidade = Cidade;
                    _endereco.Logradouro = Logradouro;
                    _endereco.Bairro = Bairro;

                    btnSelecionar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os bairros!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        private void rbPesquisaCep_CheckedChanged(object sender, EventArgs e)
        {
            ConfiguraTipoDePesquisa();
        }

        private void rbPesquisaEndereco_CheckedChanged(object sender, EventArgs e)
        {
            ConfiguraTipoDePesquisa();
        }

        private void ConsultaDeCepForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

            if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
            {
                e.Handled = true;
            }
        }

        private void cmbCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

            if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
            {
                e.Handled = true;
            }
        }

        private void txtLogradouro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

            if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
            {
                e.Handled = true;
            }
        }

        private void cmbBairro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLower(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

            if (Control.ModifierKeys.ToString().ToUpper().Contains("ALT"))
            {
                e.Handled = true;
            }
        }

        private void cmbUF_Leave(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate { CarregarCidades(); });          
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as cidades!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCidade_Leave(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate { CarregarBairros(); }) ;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os bairros!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBairro_Leave(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate { CarregarLogradouros(); });        
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os logradouros!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lklLimpar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_campanha.idTipoDaCampanha == 2)
            {
                var cepAuto = new AutoCompleteStringCollection();
                var logradouroAuto = new AutoCompleteStringCollection();
                var bairroAuto = new AutoCompleteStringCollection();
                var cidadeAuto = new AutoCompleteStringCollection();

                ResetarDropDown();

                if (_cep != null)
                {
                    cepAuto.AddRange(_cep);
                    txtCep.AutoCompleteCustomSource = cepAuto;
                    txtCep.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtCep.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

                if (_logradouro != null)
                {
                    logradouroAuto.AddRange(_logradouro);
                    txtLogradouro.AutoCompleteCustomSource = logradouroAuto;
                    txtLogradouro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtLogradouro.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

                if (_cidade != null)
                {
                    cmbCidade.DataSource = _cidade;
                    cidadeAuto.AddRange(_cidade);
                    cmbCidade.AutoCompleteCustomSource = cidadeAuto;
                    cmbCidade.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbCidade.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                else
                    cmbCidade.DataSource = null;

                if (_bairro != null)
                {
                    cmbBairro.DataSource = _bairro;
                    bairroAuto.AddRange(_bairro);
                    cmbBairro.AutoCompleteCustomSource = bairroAuto;
                    cmbBairro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbBairro.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                else
                    cmbBairro.DataSource = null;
            }

            if (rbPesquisaCep.Checked)
            {
                txtCep.Text = string.Empty;
                cmbUF.Text = Selecione;
                cmbCidade.Text = Selecione;
                cmbBairro.Text = Selecione;
                dgEndereco.DataSource = null;
            }
            else
            {
                cmbUF.Text = Selecione;
                cmbCidade.Text = Selecione;
                cmbBairro.Text = Selecione;
                txtLogradouro.Text = string.Empty;
                dgEndereco.DataSource = null;
            }

            ResetarDropDown();
        }

        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        #endregion EVENTOS 
    }
}
