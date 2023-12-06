using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using NLog;
using CallplusUtil.Forms;
using CallplusUtil.Validacoes;

namespace Callplus.CRM.Administracao.App.Planejamento.CriarFaixaDeRecarga
{
    public partial class ListaFaixaDeRecargaForm : Form
    {
        public ListaFaixaDeRecargaForm(Usuario usuarioLogado)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _faixasDeRecargaService = new FaixasDeRecargaService();
            _usuarioLogado = usuarioLogado;

            InitializeComponent();
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly Usuario _usuarioLogado;
        private readonly FaixasDeRecargaService _faixasDeRecargaService;

        #endregion PROPRIEDADES

        #region METODOS

        private void ExibirForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }
        private void CarregarConfiguracaoInicialFaixaDeRecarga()
        {
            this.ShowIcon = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
        }

        private void CarregarGrid(bool buscaRapida)
        {
            int id = -1;
            string nome = txtFaixa.Text;
            bool ativo = chkAtivos.Checked;

            if (ParametrosPesquisaValidos(buscaRapida))
            {
                if (buscaRapida)
                {
                    if (txtBuscaRapida.Text != "")
                        id = int.Parse(txtBuscaRapida.Text);
                }
                else
                {
                    nome = txtFaixa.Text.Trim() ;
                }

                dgResultado.DataSource = _faixasDeRecargaService.ListarFaixasDeRecargaExistentes(id, nome, ativo);

                lblTotalRegistros.Text = dgResultado.RowCount.ToString() + " Registro(s)";
            }
        }

        private void IniciarNovoRegistro()
        {
            FaixaDeRecargaForm f = new FaixaDeRecargaForm("NOVA FAIXA", 0);

            ExibirForm(f);

            if (f.Atualizar)
            {
                CarregarGrid(false);
            }
        }
        private void IniciarEdicaoRegistro(int linha)
        {
            if (linha >= 0)
            {
                int id = (int)dgResultado.Rows[linha].Cells["Id"].Value;

                FaixaDeRecargaForm f = new FaixaDeRecargaForm("EDITAR FAIXA", id);

                ExibirForm(f);

                if (f.Atualizar)
                {
                    CarregarGrid(false);
                }
            }
        }

        private bool ParametrosPesquisaValidos(bool buscaRapida)
        {
            var mensagens = new List<string>();

            if (buscaRapida)
            {
                if (string.IsNullOrEmpty(txtBuscaRapida.Text))
                {
                    mensagens.Add("[ID] deve ser informado!");
                }
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        #endregion METODOS

        #region EVENTOS

        private void DgListaFaixa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                IniciarEdicaoRegistro(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do registro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ListaCriarFaixaDeRecargaForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                BtnPesquisar_Click(sender, e);
                CarregarConfiguracaoInicialFaixaDeRecarga();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar as configurações iniciais!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ChkAtivos_CheckedChanged(object sender, System.EventArgs e)
        {
            BtnPesquisar_Click(sender, e);
        }
        private void BtnNovo_Click(object sender, System.EventArgs e)
        {
            try
            {
                IniciarNovoRegistro();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar o novo cadastro!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnFechar_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnBuscaRapida_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarGrid(true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível realizar a pesquisa!\n\nErro:{ex.Message}\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscaRapida_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Texto.CaractereNumerico(e.KeyChar);
        }

        #endregion EVENTOS


    }
}
