using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Planejamento.VencimentoDeFatura
{
    public partial class VencimentoDeFaturaDetalhe : Form
    {
        public VencimentoDeFaturaDetalhe(string titulo, int id)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _vencimentoDeFaturaService = new VencimentoDeFaturaService();

            if (id > 0)
                _vencimentoDeFatura = GetVencimentoDeFatura(idVencimentoDeFatura: id);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        public bool atualizar { get; set; }
        private  ILogger _logger;
        private VencimentoFatura _vencimentoDeFatura;
        private VencimentoDeFaturaService _vencimentoDeFaturaService;
        private CicloDeVencimentoDeFatura _cicloDeVencimentoDeFatura;

        #endregion PROPRIEDADES

        #region VARIAVEIS

        private string Selecione = "SELECIONE...";

        #endregion VARIAVEIS

        private void CarregarCombos()
        {
            IEnumerable<CicloDeVencimentoDeFatura> retorno = _vencimentoDeFaturaService.ListarCicloDeVencimentoDeFatura(ativo: true);
            cmbCicloDeVencimento.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Id}");
        }

        private void CarregarDadosIniciais()
        {
            CarregarCombos();
            CarregarGrid();
            AjustarGrid();
            PreencherCampos();
            ConfigurarControlesCiclo(false);
            ConfigurarControlesFatura();

            if (_vencimentoDeFatura == null)
            {
                cmbDiaDeAtivacao.Text = Selecione;
                cmbOrdem.Text = Selecione;
            }
        }

        private void ConfigurarControlesFatura()
        {
            if (_vencimentoDeFatura != null) return;

            cmbDiaDeAtivacao.ResetarComSelecione(true);
            cmbOrdem.ResetarComSelecione(true);
        }

        private void AjustarGrid()
        {
            dgResultado.Columns[0].Width = 35;
            dgResultado.Columns[3].Visible = false;
            dgResultado.Columns[4].Visible = false;
            dgResultado.Columns[5].Visible = false;
            dgResultado.Columns[6].Visible = false;
            dgResultado.Columns[7].Visible = false;
            dgResultado.Columns[8].Visible = false;
        }

        private void CarregarGrid()
        {
            dgResultado.DataSource = _vencimentoDeFaturaService.ListarCicloDeVencimentoDeFatura(ativo: true);
        }

        private void ConfigurarControlesCiclo(bool ativo)
        {
            chkCicloAtivo.Checked = ativo;
            cmbFechamento.Enabled = ativo;
            cmbVencimento.Enabled = ativo;
            chkCicloAtivo.Enabled = ativo;
            tsEtapa_btnNovo.Enabled = !ativo;
            tsEtapa_btnCancelar.Enabled = ativo;
            tsEtapa_btnSalvar.Enabled = ativo;
            dgResultado.Enabled = !ativo;
            cmbVencimento.ResetarComSelecione(ativo);
            cmbFechamento.ResetarComSelecione(ativo);
        }

        private void DgResultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditarCicloDeVencimento();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível iniciar a edicao. Error: " + ex.Message, "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void EditarCicloDeVencimento()
        {
            ConfigurarControlesCiclo(true);

            _cicloDeVencimentoDeFatura = dgResultado.CurrentRow.DataBoundItem as CicloDeVencimentoDeFatura;

            if (_cicloDeVencimentoDeFatura is CicloDeVencimentoDeFatura)
            {
                cmbFechamento.Text = _cicloDeVencimentoDeFatura.Fechamento.ToString();
                cmbVencimento.Text = _cicloDeVencimentoDeFatura.Vencimento.ToString();
                chkCicloAtivo.Checked = _cicloDeVencimentoDeFatura.Ativo;
            }
        }

        private VencimentoFatura GetVencimentoDeFatura(int idVencimentoDeFatura)
        {
            return _vencimentoDeFaturaService.RetornarVencimento(idVencimentoDeFatura);
        }

        private void GravarCicloDeVencimento()
        {
            if (PodeSalvar() == false) return;

            PreencherObjetoCicloDeVenciementoDaFatura();

            _vencimentoDeFaturaService.GravarCicloDeVencimento(_cicloDeVencimentoDeFatura);

            MessageBox.Show("Ciclo de Vencimento salvo com sucesso!", "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CarregarGrid();
            ConfigurarControlesCiclo(false);
        }

        private void IniciarNovoCadastro()
        {
            ConfigurarControlesCiclo(ativo: true);
            _cicloDeVencimentoDeFatura = new CicloDeVencimentoDeFatura();
        }

        private bool PodeSalvar()
        {
            List<string> mensagens = new List<string>();

            if (cmbFechamento.TextoEhSelecione()) mensagens.Add("Selecione o [Fechamento]");
            if (cmbVencimento.TextoEhSelecione()) mensagens.Add("Selecione o [Vencimento]");

            bool podeSalvar = !mensagens.Any();

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return podeSalvar;
        }

        private void PreencherCampos()
        {
            if (_vencimentoDeFatura == null) return; // Early return

            cmbDiaDeAtivacao.Text = _vencimentoDeFatura.Dia.ToString();
            cmbOrdem.Text = _vencimentoDeFatura.Ordem.ToString();
            cmbCicloDeVencimento.Text = _vencimentoDeFatura.IdCiclo.ToString();
            chkVencimentoAtivo.Checked = _vencimentoDeFatura.Ativo;
        }

        private void PreencherObjetoCicloDeVenciementoDaFatura()
        {
            _cicloDeVencimentoDeFatura.Vencimento = Convert.ToInt32(cmbVencimento.Text);
            _cicloDeVencimentoDeFatura.Fechamento = Convert.ToInt32(cmbFechamento.Text);
            _cicloDeVencimentoDeFatura.IdCriador = AdministracaoMDI._usuario.Id;
            _cicloDeVencimentoDeFatura.IdModificador = AdministracaoMDI._usuario.Id;
            _cicloDeVencimentoDeFatura.Ativo = chkCicloAtivo.Checked;
        }

        private void TsEtapa_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigurarControlesCiclo(ativo: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível cancelar a edicao. Error: " + ex.Message, "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void TsEtapa_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarNovoCadastro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível criar novo ciclo. Error: " + ex.Message, "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void TsEtapa_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarCicloDeVencimento();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível gravar um novo Ciclo de Vencimento. Error: " + ex.Message, "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void VencimentoDeFatura_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarDadosIniciais();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível carregar dados iniciais. Erro: " + ex.Message, "Alerta do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarVencimentoDeFatura();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível gravar um novo Vencimento de Fatura. Error: " + ex.Message, "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void GravarVencimentoDeFatura()
        {
            if (PodeSalvarVencimentoDeFatura())
            {
                bool edicao = true;

                if (_vencimentoDeFatura == null)
                {
                    edicao = false;

                    _vencimentoDeFatura = new VencimentoFatura();
                    _vencimentoDeFatura.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _vencimentoDeFatura.Dia = Convert.ToInt32(cmbDiaDeAtivacao.Text);
                _vencimentoDeFatura.Ordem = Convert.ToInt32(cmbOrdem.Text);
                _vencimentoDeFatura.IdCiclo = Convert.ToInt32(cmbCicloDeVencimento.Text);
                _vencimentoDeFatura.IdModificador = AdministracaoMDI._usuario.Id;
                _vencimentoDeFatura.Ativo = chkVencimentoAtivo.Checked;

                _vencimentoDeFatura.Id = _vencimentoDeFaturaService.GravarVencimentoDeFatura(_vencimentoDeFatura);

                MessageBox.Show("Vencimento de Fatura " + ((edicao) ? "atualizada" : "incluída") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                atualizar = true;

                Close();
            }
        }

        private bool PodeSalvarVencimentoDeFatura()
        {
            List<string> mensagens = new List<string>();

            if (cmbDiaDeAtivacao.TextoEhSelecione()) mensagens.Add("Selecione o [Dia de Ativação]");
            if (cmbOrdem.TextoEhSelecione()) mensagens.Add("Selecione o [Ordem]");
            if (cmbCicloDeVencimento.TextoEhSelecione()) mensagens.Add("Selecione o [Ciclo de Vencimento]");


            bool podeSalvar = !mensagens.Any();

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return podeSalvar;
        }
    }
}