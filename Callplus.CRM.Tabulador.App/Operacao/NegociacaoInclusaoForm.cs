using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Callplus.CRM.Tabulador.App.Operacao
{
	public partial class NegociacaoInclusaoForm : Form
    {
        private Contrato _contratoDaNegociacao;
		private Usuario _usuario;
        private NegociacaoService _negociacaoService;
		public NegociacaoInclusaoForm(Usuario usuario)
        {
            _usuario = usuario;
            _negociacaoService = new NegociacaoService();

			InitializeComponent();
        }

        private void fNegociacao_Inclusao_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            ResetarCampos();
            AtualizarTotalDaNegociacao();
            MarcarTodosTitulos();
        }

        private void btnCancelarNegociacao_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbStatusTitulo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int idStatusTitulo = 0;
            idStatusTitulo = int.Parse(cmbTipoAcordo.SelectedValue.ToString());
        }

        private void btnSalvarNegociacao_Click(object sender, EventArgs e)
        {
            if (PodeSalvar() == false) return;

            //if (!VerificarSePodeIncluirNegociacao()) return;

            var resultado = MessageBox.Show("Confirmar a criação da nova negociação?\nEste processo não pode ser desfeito.", "Callplus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    IncluirNovaNegociacao();
                    MessageBox.Show("Concluído!", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (Exception erro)
                {
                    MessageBox.Show($"Ocorreu um erro inesperado ao incluir uma nova Negociação.\nErro: {erro.Message}\nStacktrace:{erro.StackTrace}");
                }
            }
        }

        private void btnAdicionarParcela_Click(object sender, EventArgs e)
        {
            try
            {
                IncluirParcelas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao incluir a(s) parcela(s)" + ex.Message, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkTodosTitulos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTodosTitulos.Checked == true)
                {
                    MarcarTodosTitulos();
                }
                else
                {
                    DesmarcarTodosTitulos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro" + ex.Message, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoAcordo_SelectionChangeCommitted(object sender, EventArgs e)
        { 
            int idStatusTitulo = 0;
            idStatusTitulo = int.Parse(cmbTipoAcordo.SelectedValue.ToString());
        }

        private void btnRemoverParcelas_Click(object sender, EventArgs e)
        {
            dgParcelas.Rows.Clear();
        }

        private void CarregarTipoAcordo()
        {
            var tipoAcordo = _negociacaoService.RetornarTipoAcordo(ativo: true);
			//cmbTipoAcordo.PreencherComSelecione(tipoAcordo, x => x.);

			DataRow dataRow = tipoAcordo.NewRow();
			dataRow["tipo"] = "SELECIONE...";
			dataRow["id"] = "-1";
			tipoAcordo.Rows.Add(dataRow);

			cmbTipoAcordo.DataSource = tipoAcordo;
			cmbTipoAcordo.ValueMember = "id";
			cmbTipoAcordo.DisplayMember = "tipo";
			cmbTipoAcordo.DropDownStyle = ComboBoxStyle.DropDown;
			cmbTipoAcordo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cmbTipoAcordo.AutoCompleteSource = AutoCompleteSource.ListItems;
			cmbTipoAcordo.SelectedValue = "-1";
			cmbTipoAcordo.DropDownStyle = ComboBoxStyle.DropDownList;
		}

        private void CarregarPrazoNegociacao()
        {
			var prazo = _negociacaoService.RetornarPrazoNegociacao(ativo: true);

			//cmbPrazo.PreencherComSelecione(cmbPrazo, x => x.id, x.);

			DataRow dataRow = prazo.NewRow();
			dataRow["nome"] = "SELECIONE...";
			dataRow["id"] = "-1";
			prazo.Rows.Add(dataRow);

			cmbPrazo.DataSource = prazo;
			cmbPrazo.ValueMember = "id";
			cmbPrazo.DisplayMember = "nome";
			cmbPrazo.DropDownStyle = ComboBoxStyle.DropDown;
			cmbPrazo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cmbPrazo.AutoCompleteSource = AutoCompleteSource.ListItems;
			cmbPrazo.SelectedValue = "-1";
			cmbPrazo.DropDownStyle = ComboBoxStyle.DropDownList;
		}

        private void ResetarCampos()
        {
            txtNumeroNegociacao.Text = string.Empty;
            txtJuros.Text = string.Empty;
            txtMulta.Text = string.Empty;
            txtValorPrincipal.Text = string.Empty;
            txtValorParcelas.Text = string.Empty;
            mskDataVencimento.Text = string.Empty;
            cmbQuantidadeParcela.Text = "1";
            //cmbPrazo.ResetarComSelecione(true);
        }

        public void NovaNegociacao(Contrato contrato)
        {
            _contratoDaNegociacao = contrato;
            if (_contratoDaNegociacao != null)
            {
                AtualizarDadosDoContrato(contrato);
                CarregarTipoAcordo();
                CarregarPrazoNegociacao();
                //AtualizarGridTitulos(_contratoDaNegociacao.Titulos);
                ShowDialog();
            }
        }

        private Negociacao CriarNegociacao()
        {
            var negociacao = new Negociacao();

            negociacao.IdTipoAcordo = int.Parse(cmbTipoAcordo.SelectedValue.ToString());
            negociacao.NumeroNegociacao = txtNumeroNegociacao.Text;

			double.TryParse(txtValorPrincipal.Text, out double valorAtualizado);
			negociacao.ValorPrincipal = valorAtualizado;

			double.TryParse(txtValorParcelas.Text, out double valorParcelas);
			negociacao.ValorDasParcelas = valorParcelas;

            double.TryParse(txtMulta.Text, out double multa);
			negociacao.Multa = multa;

			double.TryParse(txtJuros.Text, out double juros);
			negociacao.Juros = juros;

			int.TryParse(cmbPrazo.Text, out int idPrazo);
			negociacao.IdPrazo = int.Parse(cmbPrazo.SelectedValue.ToString());

			int.TryParse(cmbQuantidadeParcela.Text, out int quantidade);
			negociacao.QuantidadeDeParcela = quantidade;

			DateTime.TryParse(mskDataVencimento.Text, out DateTime dataVencimento);
			negociacao.DataVencimento = dataVencimento;

            negociacao.IdUsuario = _usuario.Id;

            return negociacao;
        }

        private Titulo RetornarTituloDoContratoEmTela(long idTitulo)
        {
            //var titulo = _contratoDaNegociacao.Titulos.First(x => x.IDTitulo == idTitulo);
            //return titulo;
            return null;
        }

        private void AtualizarDadosDoContrato(Contrato contrato)
        {
            txtIDContrato.Text = contrato.Id.ToString();
        }

        private void AtualizarGridTitulos(List<Titulo> titulos)
        {
            dgDetalhesDoTitulo.Rows.Clear();

            foreach (var titulo in titulos)
            {
                if (VerificarSeExisteAcordo(titulo.IDTitulo) == false)
                {
                    var indice = dgDetalhesDoTitulo.Rows.Add();
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colID)].Value = titulo.IDTitulo;
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colNumeroDocumento)].Value = titulo.NumeroDocumento;
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colDataEmissao)].Value = titulo.DataEmissao.ToString("dd/MM/yyyy");
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colDataVencimento)].Value = titulo.DataVencimento.ToString("dd/MM/yyyy");
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colAtribuicaoRazaoEspecial)].Value = titulo.AtribuicaoEspecial;
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colTipoDocumento)].Value = titulo.TipoDocumento;
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colFormaDePagamento)].Value = titulo.FormaPagamento;
                    dgDetalhesDoTitulo.Rows[indice].Cells[nameof(colMontante)].Value = titulo.Montante;
                }
            }
        }

        private bool VerificarSeExisteAcordo(long iDTitulo)
        {
            return _negociacaoService.VerificarSeExisteAcordo(iDTitulo);
        }

        private bool PodeSalvar()
        {
            var mensagens = new List<string>();
            if (dgParcelas.Rows.Count <= 0)
            {
                mensagens.Add("Favor incluir a(s) parcela(s) do acordo!");
            }

            if (string.IsNullOrEmpty(cmbPrazo.Text) || cmbPrazo.Text == "SELECIONE...")
            {
                mensagens.Add("Informe o prazo!");
            }

            if (string.IsNullOrEmpty(cmbTipoAcordo.Text) || cmbTipoAcordo.Text == "SELECIONE...")
            {
                mensagens.Add("Informe o tipo do Acordo!");
            }

            int qtdTitulo = 0;
            foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            {
                if (row.IsNewRow) continue;

                if ((bool)row.Cells[nameof(colSelecioneTitulo)].FormattedValue)
                {
                    qtdTitulo++;
                }
            }

            if (qtdTitulo <= 0)
            {
                mensagens.Add("Favor selecionar o(s) titulo(s) do acordo!");
            }

            DateTime data;

            if (!DateTime.TryParse(mskDataVencimento.Text, out data))
            {
                mensagens.Add("Informe a data de vencimento!");
            }

            double valorPrincipal = 0;

            if (!Double.TryParse(txtValorPrincipal.Text, out valorPrincipal) || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
            {
                mensagens.Add("Informe o valor principal!");
            }

            double valorParcela = 0;

            if (!Double.TryParse(txtValorParcelas.Text, out valorParcela) || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
            {
                mensagens.Add("Informe o valor da parcela!");
            }

            double multa = 0;

            if (!Double.TryParse(txtMulta.Text, out multa) || string.IsNullOrEmpty(txtMulta.Text.Trim()))
            {
                mensagens.Add("Informe o valor da multa!");
            }

            double juros = 0;

            if (!Double.TryParse(txtJuros.Text, out juros) || string.IsNullOrEmpty(txtJuros.Text.Trim()))
            {
                mensagens.Add("Informe o valor dos juros!");
            }

            var podeContinuar = mensagens.Any() == false;
            if (podeContinuar == false)
            {
                var msgFinal = string.Join("\n", mensagens.ToArray());
                MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return podeContinuar;

        }

        //private bool VerificarSePodeIncluirNegociacao()
        //{
        //    long idContrato = _contratoDaNegociacao.IDContrato;
        //    int idUsuario = _usuario.Id;
        //    List<string> msgs = _negociacaoCtl.PodeIncluirNegociacao(idContrato, idUsuario);

        //    var podeContinuar = msgs.Any() == false;
        //    if (podeContinuar == false)
        //    {
        //        var msgFinal = string.Join("\n", msgs.ToArray());
        //        MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //    return podeContinuar;
        //}

        private void IncluirNovaNegociacao()
        {
            var negociacao = CriarNegociacao();
            negociacao.Titulos = ObterTitulosDoAcordo();
            negociacao.Parcelas = ObterParcelasDoAcordo();

			var idNegociacao = _negociacaoService.IncluirNegociacao(negociacao);
			if (idNegociacao < 0)
				throw new SystemException("Não foi possível obter o ID da nova negociação criada.");

			foreach (var titulo in negociacao.Titulos)
				_negociacaoService.IncluirTituloNegociacao(new TituloNegociacao(titulo.IDTitulo, idNegociacao));

			foreach (var parcela in negociacao.Parcelas)
				_negociacaoService.IncluirParcelaNegociacao(parcela, idNegociacao, negociacao.IdUsuario);
		}

        private List<Parcela> ObterParcelasDoAcordo()
        {
            List<Parcela> parcelas = new List<Parcela>();
            foreach (DataGridViewRow row in dgParcelas.Rows)
            {
                if (row.IsNewRow) continue;
				Parcela parcela = new Parcela
				{
					NumeroDaParcela = int.Parse(row.Cells[nameof(colParcelaParcelas)].Value.ToString()),
					DataVencimento = DateTime.Parse(row.Cells[nameof(colVencimentoParcelas)].Value.ToString()),
					ValorDaParcela = double.Parse(row.Cells[nameof(colValordaParcelaParcelas)].Value.ToString()),
					ValorPrincipal = double.Parse(row.Cells[nameof(colValorPrincipalParcelas)].Value.ToString()),
					Multa = double.Parse(row.Cells[nameof(colMultaParcelas)].Value.ToString()),
					Juros = double.Parse(row.Cells[nameof(colJurosParcelas)].Value.ToString())
				};
				parcelas.Add(parcela);
            }

            return parcelas;
        }

        private void dgDetalhesDoTitulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == colSelecioneTitulo.Index && e.RowIndex != -1)
            {
                dgDetalhesDoTitulo.EndEdit();
                double valor = 0;
                bool todosTitulosSelecionados = true;
                foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
                {
                    if (row.IsNewRow) continue;
                    if ((bool)row.Cells[nameof(colSelecioneTitulo)].Value==true)
                        valor = valor + double.Parse(row.Cells[nameof(colMontante)].Value.ToString());
                    else
                    {
                        todosTitulosSelecionados = false;                                                
                    }
                        
                }

                chkTodosTitulos.CheckedChanged -= chkTodosTitulos_CheckedChanged;
                
                chkTodosTitulos.Checked = todosTitulosSelecionados;                

                chkTodosTitulos.CheckedChanged += chkTodosTitulos_CheckedChanged;
                


                txtTotalNegociado.Text = $"R$ {valor:N2}";
                txtValorPrincipal.Text = valor.ToString();
            }

            //if((bool)dgDetalhesDoTitulo.CurrentCell.Value == true)
            //{
            //    foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            //    {
            //        if (row.IsNewRow) continue;
            //        row.Cells[nameof(colSelecioneTitulo)].Value = false;
            //    }
            //}
        }

        private List<Titulo> ObterTitulosDoAcordo()
        {
            List<Titulo> titulos = new List<Titulo>();
            foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            {
                if (row.IsNewRow) continue;
                Titulo titulo = new Titulo();

                if ((bool)row.Cells[nameof(colSelecioneTitulo)].FormattedValue)
                {
                    titulo.IDTitulo = long.Parse(row.Cells[nameof(colID)].Value.ToString());
                    titulos.Add(titulo);
                }

            }
            return titulos;
        }

        private List<Titulo> ObterTitulosSelecionados()
        {
            //return _contratoDaNegociacao.Titulos;
            return null;
        }

        private void AtualizarTotalDaNegociacao()
        {
            decimal valorTotalNegociado = 0;
            //var titulosSelecionados = ObterTitulosSelecionados();
            //foreach (var titulo in titulosSelecionados)
            //{
            //    if (VerificarSeExisteAcordo(titulo.IDTitulo) == false)
            //        valorTotalNegociado += titulo.Montante;
            //}

            //txtTotalNegociado.Text = $"R$ {valorTotalNegociado:N2}";
            //txtValorPrincipal.Text = valorTotalNegociado.ToString();
        }

        private void txtValorBoleto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == ',') && !(e.KeyChar == Convert.ToChar(8)) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Favor informar apenas números com casas decimais separadas por virgula(\",\").", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void txtValorAtualizado_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == ',') && !(e.KeyChar == Convert.ToChar(8)) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Favor informar apenas números com casas decimais separadas por virgula(\",\").", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void txtValorParcelas_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == ',') && !(e.KeyChar == Convert.ToChar(8)) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Favor informar apenas números com casas decimais separadas por virgula(\",\").", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void ValidarSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarSomenteNumeros(e);
        }

        private void ValidarSomenteNumeros(KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                e.Handled = true;
                MessageBox.Show("Favor informar apenas números no campo.", "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void IncluirParcelas()
        {
            if (PodeInclirParcelas())
            {
                int qtdParcela = 0;
                qtdParcela = int.Parse(cmbQuantidadeParcela.Text);

                for (int i = 1; i <= qtdParcela; i++)
                {
                    DateTime data = RetornarProximaData(i);
                    int indice = dgParcelas.Rows.Add();
                    dgParcelas.Rows[indice].Cells[nameof(colParcelaParcelas)].Value = i;
                    if (i == 1)
                        dgParcelas.Rows[indice].Cells[nameof(colVencimentoParcelas)].Value = mskDataVencimento.Text;
                    else
                        dgParcelas.Rows[indice].Cells[nameof(colVencimentoParcelas)].Value = data.ToString("dd/MM/yyyy");

                    dgParcelas.Rows[indice].Cells[nameof(colValorPrincipalParcelas)].Value = txtValorPrincipal.Text;
                    dgParcelas.Rows[indice].Cells[nameof(colMultaParcelas)].Value = txtMulta.Text;
                    dgParcelas.Rows[indice].Cells[nameof(colJurosParcelas)].Value = txtJuros.Text;
                    dgParcelas.Rows[indice].Cells[nameof(colValordaParcelaParcelas)].Value = txtValorParcelas.Text;
                }
            }
        }

        private DateTime RetornarProximaData(int adicionar)
        {
            adicionar = adicionar - 1;
            if (int.Parse(cmbPrazo.SelectedValue.ToString()) == 4)
            {
                DateTime data = DateTime.Parse(mskDataVencimento.Text);
                return data.AddMonths(adicionar);
            }
            else if (int.Parse(cmbPrazo.SelectedValue.ToString()) == 3)
            {
                DateTime data = DateTime.Parse(mskDataVencimento.Text);
                return data.AddDays(adicionar * 15);
            }
            else
            {
                DateTime data = DateTime.Parse(mskDataVencimento.Text);
                return data.AddDays(adicionar * 7);
            }
        }

        private bool PodeInclirParcelas()
        {
            var mensagens = new List<string>();
            if (string.IsNullOrEmpty(cmbQuantidadeParcela.Text))
            {
                mensagens.Add("Informe a quantidade de parcelas!");
            }

            if (dgParcelas.Rows.Count > 0)
            {
                mensagens.Add("Já existem parcelas cadastradas no grid!");
            }

            if (string.IsNullOrEmpty(cmbPrazo.Text) || cmbPrazo.Text == "SELECIONE...")
            {
                mensagens.Add("Informe o prazo!");
            }

            int prazo = int.Parse(cmbPrazo?.SelectedValue.ToString());
            if (prazo != 1 && cmbQuantidadeParcela.Text == "1")
            {
                mensagens.Add("Para esse tipo de acordo a quantidade de parcelas deve ser maior que 1!");
            }

            if (prazo == 1 && cmbQuantidadeParcela.Text != "1")
            {
                mensagens.Add("Acordo à vista deve ter apenas uma parcela!");
            }

            DateTime data;

            if (!DateTime.TryParse(mskDataVencimento.Text, out data))
            {
                mensagens.Add("Informe a data de vencimento!");
            }

            double valorPrincipal = 0;

            if (!Double.TryParse(txtValorPrincipal.Text, out valorPrincipal) || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
            {
                mensagens.Add("Informe o valor principal!");
            }

            double valorParcela = 0;

            if (!Double.TryParse(txtValorParcelas.Text, out valorParcela) || string.IsNullOrEmpty(txtValorParcelas.Text.Trim()))
            {
                mensagens.Add("Informe o valor da parcela!");
            }

            double multa = 0;

            if (!Double.TryParse(txtMulta.Text, out multa) || string.IsNullOrEmpty(txtMulta.Text.Trim()))
            {
                mensagens.Add("Informe o valor da multa!");
            }

            double juros = 0;

            if (!Double.TryParse(txtJuros.Text, out juros) || string.IsNullOrEmpty(txtJuros.Text.Trim()))
            {
                mensagens.Add("Informe o valor dos juros!");
            }

            var podeContinuar = mensagens.Any() == false;
            if (podeContinuar == false)
            {
                var msgFinal = string.Join("\n", mensagens.ToArray());
                MessageBox.Show(msgFinal, "Callplus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return podeContinuar;
        }

        private void DesmarcarTodosTitulos()
        {
            foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            {
                if (row.IsNewRow) continue;
                row.Cells[nameof(colSelecioneTitulo)].Value = false;
            }
        }

        private void MarcarTodosTitulos()
        {
            foreach (DataGridViewRow row in dgDetalhesDoTitulo.Rows)
            {
                if (row.IsNewRow) continue;
                row.Cells[nameof(colSelecioneTitulo)].Value = true;
            }
        }
    }
}
