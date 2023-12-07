using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
	public partial class CadastrarTituloForm : Form
    {
		public CadastrarTituloForm(Prospect prospect)
        {
            _prospect = prospect;
			_tituloService = new TituloService();
            _contratoService = new ContratoService();

			InitializeComponent();
        }

		#region PROPRIEDADES

		private Prospect _prospect;
        private Contrato _contrato;
        private Titulo _titulo;

        private TituloService _tituloService;
        private ContratoService _contratoService;

		#endregion PROPRIEDADES

		#region METODOS

		private void SalvarTitulo()
        {
            if (!PodeSalvar()) return;

            List<Titulo> listaTitulos = new List<Titulo>();
            listaTitulos = RetornarDadosDoTitulo();

            var resultado = MessageBox.Show("Confirmar o cadastro do novo titulo?\nEste processo não pode ser desfeito.", "Callplus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {

				_tituloService = new TituloService();
				_tituloService.Gravar(_prospect.Id, listaTitulos);

                this.Close();                
            }        
        }

        private bool PodeSalvar()
        {
            List<string> mensagensValidacao = new List<string>();

            var titulos = RetornarDadosDoTitulo();
            if (titulos.Any() == false)
            {
                mensagensValidacao.Add("É necessário informar pelo menos un título para o cliente.");
            }

            if (mensagensValidacao.Count > 0)
                MessageBox.Show(ObterMensagensConcatenadas(mensagensValidacao), "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (mensagensValidacao.Count == 0)
                btnExcluir.Enabled = true;

            return mensagensValidacao.Count == 0;
        }

        private List<Titulo> RetornarDadosDoTitulo()
        {
            List<Titulo> listaTitulos = new List<Titulo>();

            foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            {
                if (row.IsNewRow) continue;
				Titulo titulo = new Titulo
				{
					NumeroDocumento = row.Cells[nameof(colNumeroDocumento)].Value.ToString(),
					DataEmissao = DateTime.Parse(row.Cells[nameof(colDataEmissao)].Value.ToString()),
					DataVencimento = DateTime.Parse(row.Cells[nameof(colDataVencimento)].Value.ToString()),
					AtribuicaoEspecial = row.Cells[nameof(colAtribuicaoRazaoEspecial)].Value.ToString(),
					TipoDocumento = row.Cells[nameof(colTipoDocumento)].Value.ToString(),
					FormaPagamento = row.Cells[nameof(colFormaDePagamento)].Value.ToString(),
					Montante = decimal.Parse(row.Cells[nameof(colMontante)].Value.ToString())
				};
				listaTitulos.Add(titulo);
            }
            return listaTitulos;
        }

        private void IncluirDadosNaGrid()
        {
            if (!PodeAdicionarTitulo()) return;

            int indice = dgDetalhesDoTitulo.Rows.Add();
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colNumeroDocumento)].Value = txtNumeroDocumento.Text;
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colDataEmissao)].Value = mskDataEmissao.Text;
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colDataVencimento)].Value = mskDataVencimento.Text;
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colAtribuicaoRazaoEspecial)].Value = txtAtribuicaoEspecial.Text;
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colTipoDocumento)].Value = txtTipoDocumento.Text;
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colFormaDePagamento)].Value = txtFormaPagamento.Text;
            dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colMontante)].Value = txtMontante.Text;

            ResetarCampos();
        }

        private bool PodeAdicionarTitulo()
        {
            List<string> mensagensValidacao = new List<string>();

            if (string.IsNullOrEmpty(txtNumeroDocumento.Text.Trim()))
            {
                mensagensValidacao.Add("[Numero Do Documento] deve ser informado.");
            }

            if (string.IsNullOrEmpty(txtAtribuicaoEspecial.Text.Trim()))
            {
                mensagensValidacao.Add("[Atribuição Especial] deve ser informado.");
            }

            if (string.IsNullOrEmpty(txtTipoDocumento.Text.Trim()))
            {
                mensagensValidacao.Add("[Tipo Documento] deve ser informado.");
            }

            if (string.IsNullOrEmpty(txtFormaPagamento.Text.Trim()))
            {
                mensagensValidacao.Add("[Forma de Pagamento] deve ser informado.");
            }

            DateTime dataEmissao;

            if (!DateTime.TryParse(mskDataEmissao.Text, out dataEmissao))
            {
                mensagensValidacao.Add("[Data de Emissão] está inválida.");
            }

            DateTime dataVencimento;

            if (!DateTime.TryParse(mskDataVencimento.Text, out dataVencimento))
            {
                mensagensValidacao.Add("[Data de Vencimento] inválido.");
            }

            double montante;

            if (!double.TryParse(txtMontante.Text, out montante))
            {
                mensagensValidacao.Add("[Montante] está inválido.");
            }

            foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            {
                if (row.IsNewRow) continue;
                if (txtNumeroDocumento.Text == row.Cells[nameof(colNumeroDocumento)].Value.ToString())
                {
                    mensagensValidacao.Add("[Numero do Documento] já foi incluido em um Titulo.");
                    break;
                }
            }

            if (mensagensValidacao.Count > 0)
                MessageBox.Show(ObterMensagensConcatenadas(mensagensValidacao), "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (mensagensValidacao.Count == 0)
                btnExcluir.Enabled = true;

            return mensagensValidacao.Count == 0;
        }

        private string ObterMensagensConcatenadas(List<string> listaDeMensagens)
        {
            string mensagemFinal = "";
            listaDeMensagens.ForEach(mensagem => mensagemFinal += mensagem + "\n");
            return mensagemFinal;
        }

        private void ExcluirLinhaSelecionada()
        {
            foreach (DataGridViewRow row in dgDetalhesDoTitulo.SelectedRows)
            {
                if (row.IsNewRow) continue;
                dgDetalhesDoTitulo.Rows.RemoveAt(row.Index);
            }
        }

        private void ResetarCampos()
        {
            txtNumeroDocumento.Text = string.Empty;
            mskDataEmissao.Text = string.Empty;
            mskDataVencimento.Text = string.Empty;
            txtAtribuicaoEspecial.Text = string.Empty;
            txtTipoDocumento.Text = string.Empty;
            txtFormaPagamento.Text = string.Empty;
            txtMontante.Text = string.Empty;
        }

        #endregion METODOS

		#region EVENTOS

		private void cadastrarTitulo_Load(object sender, EventArgs e)
		{
			ResetarCampos();
		}

		private void btnIncluir_Click(object sender, EventArgs e)
		{
			try
			{
				IncluirDadosNaGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocorreu erro ao incluir um novo titulo.\n\nErro:{ex.Message}\n StackTrace: {ex.StackTrace}", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void btnExcluir_Click(object sender, EventArgs e)
		{
			ExcluirLinhaSelecionada();
		}

		private void cmdSalvar_Click(object sender, EventArgs e)
		{
			try
			{
				SalvarTitulo();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ocorreu erro ao salvar um novo titulo.\n\nErro:{ex.Message}\n StackTrace: {ex.StackTrace}", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion EVENTOS
	}
}
