using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Cep
{
    public partial class ConsultaDeCepForm : Form
    {   
        public ConsultaDeCepForm()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _enderecoService = new EnderecoService();

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly EnderecoService _enderecoService;

        public Endereco _endereco { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private bool PodeConsultarEndereco()
        {
            var mensagens = new List<string>();

            if (rbPesquisaCep.Checked == false && rbPesquisaEndereco.Checked == false)
                mensagens.Add("[Tipo de pesquisa] deve ser informado!");

            if (rbPesquisaCep.Checked == true)
            {
                if (string.IsNullOrEmpty(txtCep.Text.Trim()))
                    mensagens.Add("[CEP] deve ser informado!");

                if (!string.IsNullOrEmpty(txtCep.Text.Trim()) && txtCep.Text.Length != 8)
                    mensagens.Add("[CEP] inválido!");
            }
            else            
            {
                if (string.IsNullOrEmpty(cmbUF.Text.Trim()))
                    mensagens.Add("[UF] deve ser informada!");

                if (cmbCidade.TextoEhSelecione() || string.IsNullOrEmpty(cmbCidade.Text))
                    mensagens.Add("[Cidade] deve ser informada!");
                
                if (cmbBairro.TextoEhSelecione())
                    mensagens.Add("[Bairro] deve ser informado!");

                if (cmbBairro.Text != ":NÃO DEFINIDO:" && string.IsNullOrEmpty(txtLogradouro.Text.Trim()) && cmbBairro.Text != ":TODOS:")
                    mensagens.Add("[Logradouro] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);
            return !mensagens.Any() ;
        }

        private void ConsultarEndereco()
        {
            if (PodeConsultarEndereco() == false) return;

            string cep = "";
            string logradouro = "";
            string cidade = "";
            string uf = "";
            string bairro = "";
            string tipo = "";

            if (rbPesquisaCep.Checked == true)
                cep = txtCep.Text;

            if (rbPesquisaEndereco.Checked == true)
            {
                if (!cmbBairro.TextoEhSelecione())
                    bairro = cmbBairro.Text;

                logradouro = txtLogradouro.Text;
                uf = cmbUF.Text;

                if (cmbCidade.SelectedValue.ToString().Length == 1)
                {
                    cidade = cmbCidade.Text;
                    tipo = "CIDADE";
                }
                else
                {
                    cidade = cmbCidade.SelectedValue.ToString();
                    tipo = "CHAVE";
                }
            }

            IEnumerable<Endereco> enderecos = _enderecoService.RetornarEndereco(cep, logradouro, cidade, uf, bairro, tipo);

            dgEndereco.DataSource = null;
            dgEndereco.AutoGenerateColumns = false;
            dgEndereco.DataSource = enderecos;

            dgEndereco.ClearSelection();
        }

        private void CarregarCidades()
        {
            if (string.IsNullOrEmpty(cmbUF.Text.Trim()) || cmbUF.TextoEhSelecione()) return;

            cmbCidade.Enabled = true;
            string uf = cmbUF.Text;

            IEnumerable<KeyValuePair<string, string>> cidades = _enderecoService.RetornarCidade(uf);
            cmbCidade.PreencherComSelecione(cidades);
        }

        private void CarregarBairros()
        {
            if (string.IsNullOrEmpty(cmbUF.Text.Trim()) || cmbUF.TextoEhSelecione() || cmbCidade.SelectedValue == null || cmbCidade.TextoEhSelecione()) return;

            cmbBairro.Enabled = true;
            string uf = cmbUF.Text;
            string cidade = cmbCidade.SelectedValue.ToString();
            IEnumerable<KeyValuePair<string, string>> bairros = _enderecoService.RetornarBairro(uf, cidade);
            cmbBairro.PreencherComSelecione(bairros);
        }

        private string CorrigirFormatoCep(string cep)
        {
            while (cep.Length < 8)
                cep = "0" + cep;
            return cep;
        }

        #endregion METODOS

        #region EVENTOS

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
                ConsultarEndereco();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível consultarr o endereço!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbUF_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CarregarCidades();
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
                CarregarBairros();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os bairros!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgEndereco_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (_endereco == null)
                        _endereco = new Endereco();

                    string cep = dgEndereco.Rows[e.RowIndex].Cells[nameof(Cep)].Value.ToString();

                    cep = CorrigirFormatoCep(cep);

                    _endereco.Cep = cep;
                    _endereco.Estado = dgEndereco.Rows[e.RowIndex].Cells[nameof(Uf)].Value.ToString();
                    _endereco.Cidade = dgEndereco.Rows[e.RowIndex].Cells[nameof(Cidade)].Value.ToString();
                    _endereco.Logradouro = dgEndereco.Rows[e.RowIndex].Cells[nameof(Logradouro)].Value.ToString();
                    _endereco.Bairro = dgEndereco.Rows[e.RowIndex].Cells[nameof(Bairro)].Value.ToString();

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

        #endregion EVENTOS        
    }
}
