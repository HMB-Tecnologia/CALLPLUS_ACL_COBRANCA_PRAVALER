using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Backoffice.Gamificacao
{
    public partial class GamificacaoForm : Form
    {
        public GamificacaoForm(string titulo, int idGamificacao)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _gamificacaoService = new GamificacaoService();
            _frasesGamificacaoService = new FrasesGamificacaoService();
            _tipoDeFraseService = new TipoDeFraseService();
            //_gamificacaoService = new AvaliacaoDeAtendimentoService();

            if (idGamificacao > 0)
            {
                _gamificacao = _gamificacaoService.Retornar(idGamificacao);
                _frasesGamificacao = _frasesGamificacaoService.Retornar(_gamificacao.id);
            }


            InitializeComponent();

            lblTitulo.Text = titulo;

        }
        #region PROPRIEDADES

        private readonly ILogger _logger;
        private readonly GamificacaoService _gamificacaoService;
        private readonly FrasesGamificacaoService _frasesGamificacaoService;
        private readonly TipoDeFraseService _tipoDeFraseService;
        //private readonly TipoDeFraseService _tipoDeFraseService;




        //private readonly FormularioDeQualidadeService _formularioDeQualidadeService;
        //private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;

        private Tabulador.Dominio.Entidades.Gamificacao _gamificacao;
        private Tabulador.Dominio.Entidades.FrasesGamificacao _frasesGamificacao;


        private readonly CampanhaService _campanhaService;

        //private int _idCampanha;

        public bool atualizar { get; set; }

        DataTable dataTableCampanhasGamificacao = null;

        #endregion PROPRIEDADES
        #region METODOS
        private void CarregarConfiguracaoInicial()
        {
            tcDados.SelectTab("tbpCampanha");
            CarregarTipoDeFrases();

            if (_gamificacao != null)
            {
                txtTituloGamificacao.Enabled = false;
                tcDados.Enabled = true;

                CarregarDadosDaGamificacao();
                CarregarCampanhas(_gamificacao.id);
                CarregarFrasesIniciais(_gamificacao.id);
                CarregarFrasesFinais(_gamificacao.id);
                CarregarFrasesMotivacionais(_gamificacao.id);
            }
            else
            {
                CarregarCampanhas(-1);

                tcDados.Enabled = false;

                txtTituloGamificacao.Enabled = true;
                chkAtivoGamificacao.Checked = true;

            }
        }
        private void CarregarTipoDeFrases()
        {
            cmbTipoFrases.PreencherComSelecione(_frasesGamificacaoService.ListarTipo(), x => x.id, x => x.tipo);
        }


        private void CarregarDadosDaGamificacao()
        {
            if (_gamificacao != null)
            {
                txtTituloGamificacao.Text = _gamificacao.titulo;
                chkAtivoGamificacao.Checked = _gamificacao.ativo;
                txtObservacao.Text = _gamificacao.observacao;
            }
        }
        private void CarregarCampanhas(int idGamificacao)
        {
            bool? ativo = true;

            dataTableCampanhasGamificacao = _gamificacaoService.ListarCampanhasDaGamificacao(idGamificacao, ativo);

            if (dataTableCampanhasGamificacao.Rows.Count > 0)
            {
                clbCampanha.Items.Clear();

                foreach (DataRow item in dataTableCampanhasGamificacao.Rows)
                {
                    clbCampanha.Items.Add(item["nome"].ToString(), item["selecionado"].ToString() == "1");
                }
            }
        }
        private void CarregarFrasesIniciais(int idGamificacao)
        {
            DataTable dt = _frasesGamificacaoService.RetornarFrases(_gamificacao.id, 1, true);

            DataTable dtNew = new DataTable();

            if (dt.Rows.Count > 0)
            {
                dtNew = dt.Copy();

                dgFraseIniciais.DataSource = dt;

                RealizarAjustesGridFraseIniciais();
            }
            else
            {
                dgFraseIniciais.DataSource = dt;

                RealizarAjustesGridFraseIniciais();
            }
        }
        private void CarregarFrasesFinais(int idGamificacao)
        {
            DataTable dt = _frasesGamificacaoService.RetornarFrases(_gamificacao.id, 2, true);

            DataTable dtNew = new DataTable();

            if (dt.Rows.Count > 0)
            {
                dtNew = dt.Copy();

                dgFraseFinal.DataSource = dt;

                RealizarAjustesGridFraseFinais();
            }
            else
            {
                dgFraseFinal.DataSource = dt;

                RealizarAjustesGridFraseFinais();
            }
        }
        private void CarregarFrasesMotivacionais(int idGamificacao)
        {
            DataTable dt = _frasesGamificacaoService.RetornarFrases(_gamificacao.id, -1, true);

            DataTable dtNew = new DataTable();

            if (dt.Rows.Count > 0)
            {
                dtNew = dt.Copy();

                dgFraseMotivacional.DataSource = dt;

                RealizarAjustesGridFraseMotivacionais();

            }
            else
            {
                dgFraseMotivacional.DataSource = dt;

                RealizarAjustesGridFraseMotivacionais();
            }
        }
        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }
        #endregion

        private void GamificacaoForm_Load(object sender, EventArgs e)
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
        private void IniciarEdicaoDasFrasesIniciais()
        {
            _frasesGamificacao = null;

            ResetarControlesDasFrasesIniciais(true);
        }
        private void IniciarEdicaoDasFrasesFinais()
        {
            ResetarControlesDasFrasesFinais(true);
        }
        private void ResetarControlesDasFrasesIniciais(bool habilitar)
        {
            tsFraseInicial_btnNovo.Enabled = !habilitar;
            tsFraseInicial_btnExcluir.Enabled = habilitar;
            tsFraseInicial_btnCancelar.Enabled = habilitar;
            tsFraseInicial_btnSalvar.Enabled = habilitar;

            txtFrasePrimeiraVenda.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkFraseInicialAtiva.Checked = habilitar;
            chkFraseInicialAtiva.Enabled = habilitar;
        }
        private void ResetarControlesDasFrasesFinais(bool habilitar)
        {
            tsFraseFinal_btnNovo.Enabled = !habilitar;
            tsFraseFinal_btnExcluir.Enabled = habilitar;
            tsFraseFinal_btnCancelar.Enabled = habilitar;
            tsFraseFinal_btnSalvar.Enabled = habilitar;

            txtFraseConcluida.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkFraseConcluidaAtivo.Checked = habilitar;
            chkFraseConcluidaAtivo.Enabled = habilitar;
        }
        private void ResetarControlesDasFrasesMotivacionais(bool habilitar)
        {
            tsFraseMotivacional_btnNovo.Enabled = !habilitar;
            tsFraseMotivacional_btnExcluir.Enabled = habilitar;
            tsFraseMotivacional_btnCancelar.Enabled = habilitar;
            tsFraseMotivacional_btnSalvar.Enabled = habilitar;

            txtFrasesMotivacionais.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);

            chkAtivoFrasesMotivacionais.Checked = habilitar;
            chkAtivoFrasesMotivacionais.Enabled = habilitar;

            cmbTipoFrases.ResetarComSelecione(true);
            cmbTipoFrases.Enabled = habilitar;
        }
        private void txtObservacao_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsFraseMotivacional_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarFraseMotivacional();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a Frase Motivacional!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RealizarAjustesGridFraseIniciais()
        {
            dgFraseIniciais.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgFraseIniciais.Columns["Tipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgFraseIniciais.Columns["Ativo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgFraseIniciais.Columns["Gamificacao"].HeaderText = "Gamificação";

            dgFraseIniciais.Columns["idTipo"].Visible = false;
            dgFraseIniciais.Columns["TipoDecimal"].Visible = false;
            dgFraseIniciais.Columns["IdGamificacao"].Visible = false;
        }
        private void RealizarAjustesGridFraseFinais()
        {
            dgFraseFinal.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgFraseFinal.Columns["Tipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgFraseFinal.Columns["Ativo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgFraseFinal.Columns["Gamificacao"].HeaderText = "Gamificação";

            dgFraseFinal.Columns["idTipo"].Visible = false;
            dgFraseFinal.Columns["TipoDecimal"].Visible = false;
            dgFraseFinal.Columns["IdGamificacao"].Visible = false;
        }
        private void RealizarAjustesGridFraseMotivacionais()
        {
            dgFraseMotivacional.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgFraseMotivacional.Columns["Tipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgFraseMotivacional.Columns["Ativo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgFraseMotivacional.Columns["Gamificacao"].HeaderText = "Gamificação";

            dgFraseMotivacional.Columns["idTipo"].Visible = false;
            dgFraseMotivacional.Columns["TipoDecimal"].Visible = false;
            dgFraseMotivacional.Columns["IdGamificacao"].Visible = false;
        }

        private bool AtendeRegrasDeGravacaoDaFraseMotivacional()
        {
            var mensagens = new List<string>();

            if (cmbTipoFrases.TextoEhSelecione())
            {
                mensagens.Add("[Tipo] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtFrasesMotivacionais.Text.Trim()))
            {
                mensagens.Add("[Frase Motivacional] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        private bool AtendeRegrasDeGravacaoDaFraseInicial()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtFrasePrimeiraVenda.Text.Trim()))
            {
                mensagens.Add("[Frase Primeira Venda] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        private bool AtendeRegrasDeGravacaoDaFraseFinal()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtFraseConcluida.Text.Trim()))
            {
                mensagens.Add("[Frase Meta Concluida] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void tsFraseMotivacional_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirFraseMotivacional();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"A Frase Motivacional não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir a Frase Motivacional!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir a Frase Motivacional!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool AtendeRegrasDeExcluirFraseInicial()
        {
            var mensagens = new List<string>();

            //if (dgFraseIniciais.Rows.Count <= 1)
            //{
            //    mensagens.Clear();
            //    mensagens.Add("[Frase Inicial] Não será possivel excluir a última Frase.");
            //}

            if (_frasesGamificacao == null)
            {
                mensagens.Add("Selecione uma Frase.");
            }
            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        private bool AtendeRegrasDeExcluirFraseFinal()
        {
            var mensagens = new List<string>();

            //if (dgFraseFinal.Rows.Count <= 1)
            //{
            //    mensagens.Clear();
            //    mensagens.Add("[Frase Final] Não será possivel excluir a última Frase.");
            //}
            if (_frasesGamificacao == null)
            {
                mensagens.Add("Selecione uma Frase.");
            }
            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        private bool AtendeRegrasDeExcluirFraseMotivacional()
        {
            var mensagens = new List<string>();

            if (_frasesGamificacao == null)
            {
                mensagens.Add("Selecione uma Frase.");
            }
            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }
        private void ExcluirFraseInicial()
        {
            if (AtendeRegrasDeExcluirFraseInicial())
            {
                _gamificacaoService.ExcluirFrase(_frasesGamificacao.id);

                CarregarFrasesIniciais(-1);
                CancelarEdicaoDasFrasesIniciais();

                MessageBox.Show("Frase Inicial excluída com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ExcluirFraseFinal()
        {
            if (AtendeRegrasDeExcluirFraseFinal())
            {
                _gamificacaoService.ExcluirFrase(_frasesGamificacao.id);

                CarregarFrasesFinais(-1);
                CancelarEdicaoDasFrasesFinais();

                MessageBox.Show("Frase Final excluída com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ExcluirFraseMotivacional()
        {
            if (AtendeRegrasDeExcluirFraseMotivacional())
            {

                _gamificacaoService.ExcluirFrase(_frasesGamificacao.id);

                CarregarFrasesMotivacionais(-1);
                CancelarEdicaoDasFrasesMotivacionais();

                MessageBox.Show("Frase Motivacional excluída com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void tsFraseMotivacional_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDaFraseMotivacional();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da Frase Motivacional!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IniciarEdicaoDaFraseMotivacional()
        {
            ResetarControlesDasFrasesMotivacionais(true);
        }
        private void GravarFraseInicial()
        {
            if (AtendeRegrasDeGravacaoDaFraseInicial())
            {
                if (_frasesGamificacao == null)
                {
                    _frasesGamificacao = new FrasesGamificacao();

                    _frasesGamificacao.idTipo = 1;
                    _frasesGamificacao.idGamificacao = _gamificacao.id;
                    _frasesGamificacao.frase = txtFrasePrimeiraVenda.Text;
                    _frasesGamificacao.ativo = chkFraseInicialAtiva.Checked;
                    _frasesGamificacao.idCriador = AdministracaoMDI._usuario.Id;

                    _frasesGamificacao.id = _gamificacaoService.GravarFrase(_frasesGamificacao);

                    MessageBox.Show("Frase Inicial gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    _frasesGamificacao.idTipo = 1;
                    _frasesGamificacao.idGamificacao = _gamificacao.id;
                    _frasesGamificacao.frase = txtFrasePrimeiraVenda.Text;
                    _frasesGamificacao.ativo = chkFraseInicialAtiva.Checked;
                    _frasesGamificacao.idCriador = AdministracaoMDI._usuario.Id;

                    _gamificacaoService.GravarFrase(_frasesGamificacao);

                    MessageBox.Show("Frase Inicial atualizada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                CarregarFrasesIniciais(-1);

                CancelarEdicaoDasFrasesIniciais();
            }
        }
        private void GravarFraseFinal()
        {
            if (AtendeRegrasDeGravacaoDaFraseFinal())
            {
                if (_frasesGamificacao == null)
                {
                    _frasesGamificacao = new FrasesGamificacao();

                    _frasesGamificacao.idTipo = 2;
                    _frasesGamificacao.idGamificacao = _gamificacao.id;
                    _frasesGamificacao.frase = txtFraseConcluida.Text;
                    _frasesGamificacao.ativo = chkFraseConcluidaAtivo.Checked;
                    _frasesGamificacao.idCriador = AdministracaoMDI._usuario.Id;

                    _frasesGamificacao.id = _gamificacaoService.GravarFrase(_frasesGamificacao);

                    MessageBox.Show("Frase Final gravadas com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    _frasesGamificacao.idTipo = 2;
                    _frasesGamificacao.idGamificacao = _gamificacao.id;
                    _frasesGamificacao.frase = txtFraseConcluida.Text;
                    _frasesGamificacao.ativo = chkFraseConcluidaAtivo.Checked;
                    _frasesGamificacao.idCriador = AdministracaoMDI._usuario.Id;

                    _gamificacaoService.GravarFrase(_frasesGamificacao);
                    MessageBox.Show("Frase Final atualizada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CarregarFrasesFinais(_gamificacao.id);

                CancelarEdicaoDasFrasesFinais();
            }
        }
        private void GravarFraseMotivacional()
        {
            if (AtendeRegrasDeGravacaoDaFraseMotivacional())
            {
                if (_frasesGamificacao == null)
                {
                    _frasesGamificacao = new FrasesGamificacao();

                    _frasesGamificacao.idTipo = Convert.ToInt32(cmbTipoFrases.SelectedValue);
                    _frasesGamificacao.idGamificacao = _gamificacao.id;
                    _frasesGamificacao.frase = txtFrasesMotivacionais.Text;
                    _frasesGamificacao.ativo = chkAtivoFrasesMotivacionais.Checked;
                    _frasesGamificacao.idCriador = AdministracaoMDI._usuario.Id;

                    _frasesGamificacao.id = _gamificacaoService.GravarFrase(_frasesGamificacao);

                    MessageBox.Show("Frase Motivacional gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    _frasesGamificacao.idTipo = Convert.ToInt32(cmbTipoFrases.SelectedValue);
                    _frasesGamificacao.idGamificacao = _gamificacao.id;
                    _frasesGamificacao.frase = txtFrasesMotivacionais.Text;
                    _frasesGamificacao.ativo = chkAtivoFrasesMotivacionais.Checked;
                    _frasesGamificacao.idCriador = AdministracaoMDI._usuario.Id;

                    _gamificacaoService.GravarFrase(_frasesGamificacao);

                    MessageBox.Show("Frase Motivacional atualizada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CarregarFrasesMotivacionais(-1);

                CancelarEdicaoDasFrasesMotivacionais();
            }
        }
        private void CancelarEdicaoDasFrasesIniciais()
        {
            dgFraseIniciais.ClearSelection();

            ResetarControlesDasFrasesIniciais(false);

            _frasesGamificacao = null;
        }
        private void CancelarEdicaoDasFrasesFinais()
        {
            dgFraseFinal.ClearSelection();

            ResetarControlesDasFrasesFinais(false);

            _frasesGamificacao = null;
        }
        private void CancelarEdicaoDasFrasesMotivacionais()
        {
            dgFraseFinal.ClearSelection();

            ResetarControlesDasFrasesMotivacionais(false);

            _frasesGamificacao = null;
        }
        private void dgFraseIniciais_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosFraseInicial(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados da Frase Inicial!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarDadosFraseInicial(int linha)
        {
            if (linha >= 0)
            {
                if (_frasesGamificacao == null)
                {
                    _frasesGamificacao = new FrasesGamificacao();
                }

                ResetarControlesDaFraseInicial(true);

                _frasesGamificacao.id = (int)dgFraseIniciais.Rows[linha].Cells["Id"].Value;
                _frasesGamificacao.idTipo = (int)dgFraseIniciais.Rows[linha].Cells["IdTipo"].Value;
                _frasesGamificacao.idGamificacao = (int)dgFraseIniciais.Rows[linha].Cells["IdGamificacao"].Value;
                _frasesGamificacao.frase = (string)dgFraseIniciais.Rows[linha].Cells["Frase"].Value;
                _frasesGamificacao.ativo = (string)dgFraseIniciais.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;

                txtFrasePrimeiraVenda.Text = _frasesGamificacao.frase;
                chkFraseInicialAtiva.Checked = _frasesGamificacao.ativo;
            }
        }
        private void CarregarDadosFraseFinal(int linha)
        {
            if (linha >= 0)
            {
                if (_frasesGamificacao == null)
                {
                    _frasesGamificacao = new FrasesGamificacao();
                }

                ResetarControlesDaFraseFinal(true);

                _frasesGamificacao.id = (int)dgFraseFinal.Rows[linha].Cells["Id"].Value;
                _frasesGamificacao.idTipo = (int)dgFraseFinal.Rows[linha].Cells["IdTipo"].Value;
                _frasesGamificacao.idGamificacao = (int)dgFraseFinal.Rows[linha].Cells["IdGamificacao"].Value;
                _frasesGamificacao.frase = (string)dgFraseFinal.Rows[linha].Cells["Frase"].Value;
                _frasesGamificacao.ativo = (string)dgFraseFinal.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;

                txtFraseConcluida.Text = _frasesGamificacao.frase;
                chkFraseConcluidaAtivo.Checked = _frasesGamificacao.ativo;
            }
        }
        private void CarregarDadosFraseMotivacional(int linha)
        {
            if (linha >= 0)
            {
                if (_frasesGamificacao == null)
                {
                    _frasesGamificacao = new FrasesGamificacao();
                }

                ResetarControlesDaFraseMotivacional(true);

                _frasesGamificacao.id = (int)dgFraseMotivacional.Rows[linha].Cells["Id"].Value;
                _frasesGamificacao.idTipo = (int)dgFraseMotivacional.Rows[linha].Cells["IdTipo"].Value;
                _frasesGamificacao.idGamificacao = (int)dgFraseMotivacional.Rows[linha].Cells["IdGamificacao"].Value;
                _frasesGamificacao.frase = (string)dgFraseMotivacional.Rows[linha].Cells["Frase"].Value;
                _frasesGamificacao.ativo = (string)dgFraseMotivacional.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;
                _frasesGamificacao.idTipo = (int)dgFraseMotivacional.Rows[linha].Cells["idTipo"].Value;

                txtFrasesMotivacionais.Text = _frasesGamificacao.frase;
                cmbTipoFrases.SelectedValue = _frasesGamificacao.idTipo.ToString();


                chkAtivoFrasesMotivacionais.Checked = _frasesGamificacao.ativo;
            }
        }
        private void ResetarControlesDaFraseInicial(bool habilitar)
        {
            tsFraseInicial_btnNovo.Enabled = !habilitar;
            tsFraseInicial_btnExcluir.Enabled = habilitar;
            tsFraseInicial_btnCancelar.Enabled = habilitar;
            tsFraseInicial_btnSalvar.Enabled = habilitar;

            //cmbItem.ResetarComSelecione(true);
            //txtNumeroProcedimento.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            txtFrasePrimeiraVenda.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkFraseInicialAtiva.Checked = false;
            chkFraseInicialAtiva.Enabled = habilitar;
        }
        private void ResetarControlesDaFraseFinal(bool habilitar)
        {
            tsFraseFinal_btnNovo.Enabled = !habilitar;
            tsFraseFinal_btnExcluir.Enabled = habilitar;
            tsFraseFinal_btnCancelar.Enabled = habilitar;
            tsFraseFinal_btnSalvar.Enabled = habilitar;

            //cmbItem.ResetarComSelecione(true);
            //txtNumeroProcedimento.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            txtFraseConcluida.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkFraseConcluidaAtivo.Checked = false;
            chkFraseConcluidaAtivo.Enabled = habilitar;
        }
        private void ResetarControlesDaFraseMotivacional(bool habilitar)
        {
            tsFraseMotivacional_btnNovo.Enabled = !habilitar;
            tsFraseMotivacional_btnExcluir.Enabled = habilitar;
            tsFraseMotivacional_btnCancelar.Enabled = habilitar;
            tsFraseMotivacional_btnSalvar.Enabled = habilitar;

            //txtNumeroProcedimento.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            cmbTipoFrases.ResetarComSelecione(true);
            txtFrasesMotivacionais.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkAtivoFrasesMotivacionais.Checked = false;
            chkAtivoFrasesMotivacionais.Enabled = habilitar;
        }

        private void dgFraseFinal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosFraseFinal(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados da Frase Final!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgFraseMotivacional_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosFraseMotivacional(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados da Frases Motivacional!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFraseInicial_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDasFrasesIniciais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da Frase Inicial!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFraseInicial_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDasFrasesIniciais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Frase Inicial!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFraseInicial_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarFraseInicial();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar as Frases Inicial!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFraseFinal_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDasFrasesFinais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da Frase Final!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tsFraseFinal_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDasFrasesFinais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Frase Final!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFraseFinal_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarFraseFinal();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a Frase Final!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsFraseMotivacional_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDasFrasesMotivacionais();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Frase Motivacional!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFraseInicial_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirFraseInicial();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"A Frase Inicial não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir a Frase Inicial!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir a Frase Inicial!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void tsFraseFinal_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirFraseFinal();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"A Frase Final não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir a Frase Final!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir a Frase Final!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    $"Não foi possível gravar a Gamificação!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_gamificacao == null)
                {
                    edicao = false;
                    _gamificacao = new Tabulador.Dominio.Entidades.Gamificacao();

                    _gamificacao.idCriador = AdministracaoMDI._usuario.Id;
                }

                _gamificacao.titulo = txtTituloGamificacao.Text;
                _gamificacao.ativo = chkAtivoGamificacao.Checked;
                _gamificacao.observacao = txtObservacao.Text;

                _gamificacao.idModificador = AdministracaoMDI._usuario.Id;

                _gamificacao.id = _gamificacaoService.GravarGamificacao(_gamificacao);

                MessageBox.Show("Gamificação [" + _gamificacao.titulo + "] " + ((edicao) ? "atualizado" : "incluído") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                atualizar = true;
            }

            if (atualizar)
            {
                CarregarConfiguracaoInicial();
            }
        }
        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtTituloGamificacao.Text.Trim()))
            {
                mensagens.Add("[Título da Gamificação] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void tsCampanha_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GamificicarCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível atribuir a Gamificação para as Campanhas!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Corrigir
        private void GamificicarCampanha()
        {
            string CampanhaGamificacao = "";
            bool CampanhaSelecionada = false;

            if (dataTableCampanhasGamificacao.Rows.Count > 0)
            {
                clbCampanha.Items.Clear();
                foreach (DataRow item in dataTableCampanhasGamificacao.Rows)
                {

                    CampanhaGamificacao = item["nome"].ToString();
                    CampanhaSelecionada = item["selecionado"].ToString() == "1";

                }
            }

            MessageBox.Show("nomeCampanha", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}