using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Servico.Servicos;
using CallplusUtil.Extensions;
using CallplusUtil.Forms;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Callplus.CRM.Administracao.App.Qualidade.FormularioDeQualidade
{
    public partial class FormularioDeQualidadeForm : Form
    {
        public FormularioDeQualidadeForm(string titulo, int idFormularioDeQualidade)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _formularioDeQualidadeService = new FormularioDeQualidadeService();
            _avaliacaoDeAtendimentoService = new AvaliacaoDeAtendimentoService();
            
            if (idFormularioDeQualidade > 0)
                _formularioDeQualidade = _formularioDeQualidadeService.Retornar(idFormularioDeQualidade);

            InitializeComponent();

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly FormularioDeQualidadeService _formularioDeQualidadeService;
        private readonly AvaliacaoDeAtendimentoService _avaliacaoDeAtendimentoService;

        private Tabulador.Dominio.Entidades.FormularioDeQualidade _formularioDeQualidade;
        private Tabulador.Dominio.Entidades.ModuloDoFormularioDeQualidade _moduloDoFormularioDeQualidade;
        private Tabulador.Dominio.Entidades.ItemDoModuloDoFormularioDeQualidade _itemDoModuloDoFormularioDeQualidade;
        private Tabulador.Dominio.Entidades.ProcedimentoDoItemDoFormularioDeQualidade _procedimentoDoItemDoFormularioDeQualidade;
        private Tabulador.Dominio.Entidades.FaqDoProcedimentoDoFormularioDeQualidade _faqDoProcedimentoDoFormularioDeQualidade;

        //private readonly CampanhaService _campanhaService;

        //private int _idCampanha;

        public bool atualizar { get; set; }

        DataTable dataTableCampanhasFormularioQualidade;
                
        #endregion PROPRIEDADES

        #region METODOS

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void CarregarConfiguracaoInicial()
        {   
            tcDados.SelectTab("tbpCampanha");

            if (_formularioDeQualidade != null)
            {
                CarregarDadosDoFormulario();
                CarregarCampanhas(_formularioDeQualidade.id);
                CarregarModulos(_formularioDeQualidade.id);
                CarregarItens(-1);
                CarregarProcedimentos(-1);
                CarregarFaqs(-1);
            }
            else
            {                                
                CarregarCampanhas(0);
            }            
        }

        private void CarregarDadosDoFormulario()
        {
            if (_formularioDeQualidade != null)
            {   
                txtNome.Text = _formularioDeQualidade.nome;
                chkAtivo.Checked = _formularioDeQualidade.ativo;
                txtObservacao.Text = _formularioDeQualidade.observacao;   
            }
        }

        private void CarregarCampanhas(int idFormulario)
        {
            bool? ativo = true;
            
            dataTableCampanhasFormularioQualidade = _formularioDeQualidadeService.ListarCampanhasDoFormulario(idFormulario, ativo);

            if (dataTableCampanhasFormularioQualidade.Rows.Count > 0)
            {
                clbCampanha.Items.Clear();

                foreach (DataRow item in dataTableCampanhasFormularioQualidade.Rows)
                {
                    clbCampanha.Items.Add(item["nome"].ToString(), item["selecionado"].ToString() == "1");
                }
            }
        }

        private void CarregarModulos(int idFormulario)
        {
            DataTable dt = _formularioDeQualidadeService.RetornarModulo(-1, _formularioDeQualidade.id, false);
            DataTable dtNew = new DataTable();
                        
            if (dt.Rows.Count > 0)
            {
                dtNew = dt.Copy();
                dt.Rows.RemoveAt(0);

                dgModulo.DataSource = dt;
                                
                cmbModulo.DefinirComoSelecione();
                cmbModulo.DataSource = dtNew.DefaultView;
                cmbModulo.ValueMember = "Id";
                cmbModulo.DisplayMember = "Nome";
            }
        }

        private void CarregarDadosDoModulo(int linha)
        {
            if (linha >= 0)
            {
                if (_moduloDoFormularioDeQualidade == null)
                {
                    _moduloDoFormularioDeQualidade = new ModuloDoFormularioDeQualidade();
                }

                ResetarControlesDoModulo(true);

                _moduloDoFormularioDeQualidade.Id = (int)dgModulo.Rows[linha].Cells["Id"].Value;
                _moduloDoFormularioDeQualidade.IdFormularioDeQualidade = _formularioDeQualidade.id;
                _moduloDoFormularioDeQualidade.Nome = (string)dgModulo.Rows[linha].Cells["Nome"].Value;
                _moduloDoFormularioDeQualidade.Ativo = (string)dgModulo.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;
                _moduloDoFormularioDeQualidade.Valor = (int)dgModulo.Rows[linha].Cells["Valor"].Value;
                                
                txtNomeModulo.Text = _moduloDoFormularioDeQualidade.Nome;
                txtValorModulo.Text = _moduloDoFormularioDeQualidade.Valor.ToString();
                chkAtivoModulo.Checked = _moduloDoFormularioDeQualidade.Ativo;                
            }
        }

        private void CarregarItens(int idItem)
        {
            DataTable dt = _formularioDeQualidadeService.RetornarItem(idItem, -1, _formularioDeQualidade.id, true);
            DataTable dtNew = new DataTable();

            if (dt.Rows.Count > 0)
            {
                dtNew = dt.Copy();
                dt.Rows.RemoveAt(0);

                dgItem.DataSource = dt;

                cmbItem.DefinirComoSelecione();
                cmbItem.DataSource = dtNew.DefaultView;
                cmbItem.ValueMember = "Id";
                cmbItem.DisplayMember = "Nome";
                                
                RealizarAjustesGridItem();
            }
        }

        private void CarregarDadosDoItem(int linha)
        {
            if (linha >= 0)
            {
                if (_itemDoModuloDoFormularioDeQualidade == null)
                {
                    _itemDoModuloDoFormularioDeQualidade = new ItemDoModuloDoFormularioDeQualidade();
                }

                ResetarControlesDoItem(true);

                _itemDoModuloDoFormularioDeQualidade.Id = (int)dgItem.Rows[linha].Cells["Id"].Value;
                _itemDoModuloDoFormularioDeQualidade.IdModuloDoFormularioDeQualidade = (int)dgItem.Rows[linha].Cells["IdModuloDoFormularioDeQualidade"].Value;
                _itemDoModuloDoFormularioDeQualidade.Nome = (string)dgItem.Rows[linha].Cells["Nome"].Value;
                _itemDoModuloDoFormularioDeQualidade.Peso = (int)dgItem.Rows[linha].Cells["Peso"].Value;
                _itemDoModuloDoFormularioDeQualidade.Descricao = (string)dgItem.Rows[linha].Cells["Descrição"].Value;
                _itemDoModuloDoFormularioDeQualidade.Ativo = (string)dgItem.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;
                
                cmbModulo.SelectedValue = _itemDoModuloDoFormularioDeQualidade.IdModuloDoFormularioDeQualidade.ToString();
                txtNomeItem.Text = _itemDoModuloDoFormularioDeQualidade.Nome;
                txtPesoItem.Text = _itemDoModuloDoFormularioDeQualidade.Peso.ToString();
                txtDescricaoItem.Text = _itemDoModuloDoFormularioDeQualidade.Descricao;
                chkAtivoItem.Checked = _itemDoModuloDoFormularioDeQualidade.Ativo;                
            }
        }

        private void RealizarAjustesGridItem()
        {
            for (int i = dgItem.Columns["Nome"].Index + 1; i < dgItem.Columns.Count; i++)
            {
                dgItem.Columns[i].Visible = false;
            }
        }

        private void RealizarAjustesGridProcedimento()
        {
            for (int i = dgProcedimento.Columns["Ativo"].Index + 1; i < dgProcedimento.Columns.Count; i++)
            {
                dgProcedimento.Columns[i].Visible = false;
            }
        }

        private void RealizarAjustesGridFAQ()
        {
            for (int i = dgFaq.Columns["Descrição"].Index + 1; i < dgFaq.Columns.Count; i++)
            {
                dgFaq.Columns[i].Visible = false;
            }
        }

        private void CarregarProcedimentos(int idItem)
        {
            DataTable dt = _formularioDeQualidadeService.RetornarProcedimento(_formularioDeQualidade.id, -1);
            DataTable dtNew = new DataTable();

            if (dt.Rows.Count > 0)
            {                
                dtNew = dt.Copy();
                dt.Rows.RemoveAt(0);

                dgProcedimento.DataSource = dt;

                cmbProcedimento.DefinirComoSelecione();
                cmbProcedimento.DataSource = dtNew.DefaultView;
                cmbProcedimento.ValueMember = "IdProcedimento";
                cmbProcedimento.DisplayMember = "Descrição";

                RealizarAjustesGridProcedimento();
            }
        }

        private void CarregarDadosDoProcedimento(int linha)
        {
            if (linha >= 0)
            {
                if (_procedimentoDoItemDoFormularioDeQualidade == null)
                {
                    _procedimentoDoItemDoFormularioDeQualidade = new ProcedimentoDoItemDoFormularioDeQualidade();
                }

                ResetarControlesDoProcedimento(true);

                _procedimentoDoItemDoFormularioDeQualidade.Id = (int)dgProcedimento.Rows[linha].Cells["IdProcedimento"].Value;
                _procedimentoDoItemDoFormularioDeQualidade.IdItemDoModuloDoFormularioDeQualidade = (int)dgProcedimento.Rows[linha].Cells["IdItem"].Value;
                _procedimentoDoItemDoFormularioDeQualidade.Numero = (int)dgProcedimento.Rows[linha].Cells["Número"].Value;
                _procedimentoDoItemDoFormularioDeQualidade.Descricao = (string)dgProcedimento.Rows[linha].Cells["Descrição"].Value;
                _procedimentoDoItemDoFormularioDeQualidade.Ativo = (string)dgProcedimento.Rows[linha].Cells["Ativo"].Value.ToString().ToUpper() == "SIM" ? true : false;
                
                cmbItem.SelectedValue = _procedimentoDoItemDoFormularioDeQualidade.IdItemDoModuloDoFormularioDeQualidade.ToString();
                txtNumeroProcedimento.Text = _procedimentoDoItemDoFormularioDeQualidade.Numero.ToString();                
                chkAtivoProcedimento.Checked = _procedimentoDoItemDoFormularioDeQualidade.Ativo;
                txtDescricaoProcedimento.Text = _procedimentoDoItemDoFormularioDeQualidade.Descricao;
            }
        }

        private void CarregarFaqs(int idProcedimento)
        {
            DataTable dt = _formularioDeQualidadeService.RetornarFAQ(-1, idProcedimento, true);
            
            if (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(0);

                dgFaq.DataSource = dt;
                                
                RealizarAjustesGridFAQ();
            }

            cmbTipoFaq.PreencherComSelecione(_avaliacaoDeAtendimentoService.ListarTipo(true), x => x.Id, x => x.Nome);
        }

        private void CarregarDadosDoFAQ(int linha)
        {
            if (linha >= 0)
            {
                if (_faqDoProcedimentoDoFormularioDeQualidade == null)
                {
                    _faqDoProcedimentoDoFormularioDeQualidade = new FaqDoProcedimentoDoFormularioDeQualidade();
                }

                ResetarControlesDoFAQ(true);

                _faqDoProcedimentoDoFormularioDeQualidade.id = (int)dgFaq.Rows[linha].Cells["Id"].Value;
                _faqDoProcedimentoDoFormularioDeQualidade.idProcedimento = (int)dgFaq.Rows[linha].Cells["IdProcedimento"].Value;
                _faqDoProcedimentoDoFormularioDeQualidade.idTipoDeAvaliacaoDeAtendimento = (int)dgFaq.Rows[linha].Cells["IdTipoDeAvaliacaoDeAtendimento"].Value;
                _faqDoProcedimentoDoFormularioDeQualidade.descricao = (string)dgFaq.Rows[linha].Cells["Descrição"].Value;

                cmbProcedimento.SelectedValue = _faqDoProcedimentoDoFormularioDeQualidade.idProcedimento.ToString();
                cmbTipoFaq.SelectedValue = _faqDoProcedimentoDoFormularioDeQualidade.idTipoDeAvaliacaoDeAtendimento.ToString();
                txtDescricaoFaq.Text = _faqDoProcedimentoDoFormularioDeQualidade.descricao;                
            }
        }

        private void IniciarEdicaoDoModulo()
        {
            ResetarControlesDoModulo(true);
        }

        private void IniciarEdicaoDoItem()
        {
            ResetarControlesDoItem(true);
        }

        private void IniciarEdicaoDoProcedimento()
        {
            ResetarControlesDoProcedimento(true);
        }

        private void IniciarEdicaoDoFAQ()
        {
            ResetarControlesDoFAQ(true);
        }

        private void ExcluirModulo()
        {
            _formularioDeQualidadeService.ExcluirModuloDoFormularioDeQualidade(_moduloDoFormularioDeQualidade.Id);

            CarregarModulos(_formularioDeQualidade.id);
            
            CancelarEdicaoDoModulo();

            MessageBox.Show("Módulo excluído com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExcluirItem()
        {
            _formularioDeQualidadeService.ExcluirItemDoModuloDoFormularioDeQualidade(_itemDoModuloDoFormularioDeQualidade.Id);

            CarregarItens(-1);

            CancelarEdicaoDoItem();

            MessageBox.Show("Item excluído com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExcluirProcedimento()
        {
            _formularioDeQualidadeService.ExcluirProcedimentoDoItemDoFormularioDeQualidade(_procedimentoDoItemDoFormularioDeQualidade.Id);

            CarregarProcedimentos(-1);

            CancelarEdicaoDoProcedimento();

            MessageBox.Show("Procedimento excluído com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExcluirFAQ()
        {
            _formularioDeQualidadeService.ExcluirFaqDoProcedimentoDoFormularioDeQualidade(_faqDoProcedimentoDoFormularioDeQualidade.id);

            CarregarFaqs(-1);

            CancelarEdicaoDoFAQ();

            MessageBox.Show("FAQ excluído com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancelarEdicaoDoModulo()
        {
            dgModulo.ClearSelection();

            ResetarControlesDoModulo(false);

            //dgModulo.DataSource = null;

            _moduloDoFormularioDeQualidade = null;
        }

        private void CancelarEdicaoDoItem()
        {
            dgItem.ClearSelection();

            ResetarControlesDoItem(false);

            _itemDoModuloDoFormularioDeQualidade = null;
        }

        private void CancelarEdicaoDoProcedimento()
        {
            dgProcedimento.ClearSelection();

            ResetarControlesDoProcedimento(false);

            _procedimentoDoItemDoFormularioDeQualidade = null;
        }

        private void CancelarEdicaoDoFAQ()
        {
            dgFaq.ClearSelection();

            ResetarControlesDoFAQ(false);

            _faqDoProcedimentoDoFormularioDeQualidade = null;
        }

        private bool AtendeRegrasDeGravacaoDoModulo()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtNomeModulo.Text.Trim()))
            {
                mensagens.Add("[Nome do Modulo] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtValorModulo.Text.Trim()))
            {
                mensagens.Add("[Valor do Modulo] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDoItem()
        {
            var mensagens = new List<string>();

            if (cmbModulo.TextoEhSelecione())
            {
                mensagens.Add("[Modulo] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtNomeItem.Text.Trim()))
            {
                mensagens.Add("[Nome do Item] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtDescricaoItem.Text.Trim()))
            {
                mensagens.Add("[Descrição do Item] deve ser informada!");
            }

            if (string.IsNullOrEmpty(txtPesoItem.Text.Trim()))
            {
                mensagens.Add("[Peso do Item] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDoProcedimento()
        {
            var mensagens = new List<string>();

            if (cmbItem.TextoEhSelecione())
            {
                mensagens.Add("[Item do Procedimento] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtNumeroProcedimento.Text.Trim()))
            {
                mensagens.Add("[Número do Procedimento] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtDescricaoProcedimento.Text.Trim()))
            {
                mensagens.Add("[Descrição do Procedimento] deve ser informada!");
            }
                        
            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDoFAQ()
        {
            var mensagens = new List<string>();

            if (cmbProcedimento.TextoEhSelecione())
            {
                mensagens.Add("[Procedimento do FAQ] deve ser informado!");
            }

            if (cmbTipoFaq.TextoEhSelecione())
            {
                mensagens.Add("[Tipo do FAQ] deve ser informado!");
            }

            if (string.IsNullOrEmpty(txtDescricaoFaq.Text.Trim()))
            {
                mensagens.Add("[Descrição do FAQ] deve ser informada!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void GravarModulo()
        {
            if (AtendeRegrasDeGravacaoDoModulo())
            {
                if (_moduloDoFormularioDeQualidade == null)
                {
                    _moduloDoFormularioDeQualidade = new ModuloDoFormularioDeQualidade();
                }

                _moduloDoFormularioDeQualidade.IdFormularioDeQualidade = _formularioDeQualidade.id;
                _moduloDoFormularioDeQualidade.Nome = txtNomeModulo.Text;
                _moduloDoFormularioDeQualidade.Valor = Convert.ToInt32(txtValorModulo.Text);
                _moduloDoFormularioDeQualidade.Ativo = chkAtivoModulo.Checked;

                _moduloDoFormularioDeQualidade.Id = _formularioDeQualidadeService.GravarModuloDoFormularioDeQualidade(_moduloDoFormularioDeQualidade);
                
                MessageBox.Show("Módulo gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarModulos(_formularioDeQualidade.id);

                CancelarEdicaoDoModulo();
            }
        }

        private void GravarItem()
        {
            if (AtendeRegrasDeGravacaoDoItem())
            {
                if (_itemDoModuloDoFormularioDeQualidade == null)
                {
                    _itemDoModuloDoFormularioDeQualidade = new ItemDoModuloDoFormularioDeQualidade();
                }

                _itemDoModuloDoFormularioDeQualidade.IdModuloDoFormularioDeQualidade = Convert.ToInt32(cmbModulo.SelectedValue);
                _itemDoModuloDoFormularioDeQualidade.Nome = txtNomeItem.Text;
                _itemDoModuloDoFormularioDeQualidade.Descricao = txtDescricaoItem.Text;
                _itemDoModuloDoFormularioDeQualidade.Peso = Convert.ToInt32(txtPesoItem.Text);
                _itemDoModuloDoFormularioDeQualidade.Ativo = chkAtivoModulo.Checked;

                _itemDoModuloDoFormularioDeQualidade.Id = _formularioDeQualidadeService.GravarItemDoModuloDoFormularioDeQualidade(_itemDoModuloDoFormularioDeQualidade);

                MessageBox.Show("Item gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarItens(-1);

                CancelarEdicaoDoItem();
            }
        }

        private void GravarProcedimento()
        {
            if (AtendeRegrasDeGravacaoDoProcedimento())
            {
                if (_procedimentoDoItemDoFormularioDeQualidade == null)
                {
                    _procedimentoDoItemDoFormularioDeQualidade = new ProcedimentoDoItemDoFormularioDeQualidade();
                }

                _procedimentoDoItemDoFormularioDeQualidade.IdItemDoModuloDoFormularioDeQualidade = Convert.ToInt32(cmbItem.SelectedValue);
                _procedimentoDoItemDoFormularioDeQualidade.Numero = Convert.ToInt32(txtNumeroProcedimento.Text);
                _procedimentoDoItemDoFormularioDeQualidade.Descricao = txtDescricaoProcedimento.Text;
                _procedimentoDoItemDoFormularioDeQualidade.Ativo = chkAtivoProcedimento.Checked;

                _procedimentoDoItemDoFormularioDeQualidade.Id = _formularioDeQualidadeService.GravarProcedimentoDoItemDoFormularioDeQualidade(_procedimentoDoItemDoFormularioDeQualidade);

                MessageBox.Show("Procedimento gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarProcedimentos(-1);

                CancelarEdicaoDoProcedimento();
            }
        }

        private void GravarFAQ()
        {
            if (AtendeRegrasDeGravacaoDoFAQ())
            {
                if (_faqDoProcedimentoDoFormularioDeQualidade == null)
                {
                    _faqDoProcedimentoDoFormularioDeQualidade = new FaqDoProcedimentoDoFormularioDeQualidade();
                }

                _faqDoProcedimentoDoFormularioDeQualidade.idProcedimento = Convert.ToInt32(cmbProcedimento.SelectedValue);
                _faqDoProcedimentoDoFormularioDeQualidade.idTipoDeAvaliacaoDeAtendimento = Convert.ToInt32(cmbTipoFaq.SelectedValue);
                _faqDoProcedimentoDoFormularioDeQualidade.descricao = txtDescricaoFaq.Text;
                
                _faqDoProcedimentoDoFormularioDeQualidade.id = _formularioDeQualidadeService.GravarFaqDoProcedimentoDoFormularioDeQualidade(_faqDoProcedimentoDoFormularioDeQualidade);

                MessageBox.Show("FAQ gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarFaqs(-1);

                CancelarEdicaoDoFAQ();
            }
        }

        private void ResetarControlesDoModulo(bool habilitar)
        {
            tsModulo_btnNovo.Enabled = !habilitar;
            tsModulo_btnExcluir.Enabled = habilitar;
            tsModulo_btnCancelar.Enabled = habilitar;
            tsModulo_btnSalvar.Enabled = habilitar;

            txtNomeModulo.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            txtValorModulo.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkAtivoModulo.Checked = false;
            chkAtivoModulo.Enabled = habilitar;
        }

        private void ResetarControlesDoItem(bool habilitar)
        {
            tsItem_btnNovo.Enabled = !habilitar;
            tsItem_btnExcluir.Enabled = habilitar;
            tsItem_btnCancelar.Enabled = habilitar;
            tsItem_btnSalvar.Enabled = habilitar;

            cmbModulo.ResetarComSelecione(true);
            txtNomeItem.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            txtPesoItem.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkAtivoItem.Checked = false;
            chkAtivoItem.Enabled = habilitar;
            txtDescricaoItem.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);            
        }

        private void ResetarControlesDoProcedimento(bool habilitar)
        {
            tsProcedimento_btnNovo.Enabled = !habilitar;
            tsProcedimento_btnExcluir.Enabled = habilitar;
            tsProcedimento_btnCancelar.Enabled = habilitar;
            tsProcedimento_btnSalvar.Enabled = habilitar;

            cmbItem.ResetarComSelecione(true);
            txtNumeroProcedimento.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            txtDescricaoProcedimento.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkAtivoProcedimento.Checked = false;
            chkAtivoProcedimento.Enabled = habilitar;            
        }

        private void ResetarControlesDoFAQ(bool habilitar)
        {
            tsFaq_btnNovo.Enabled = !habilitar;
            tsFaq_btnExcluir.Enabled = habilitar;
            tsFaq_btnCancelar.Enabled = habilitar;
            tsFaq_btnSalvar.Enabled = habilitar;

            cmbProcedimento.ResetarComSelecione(true);
            cmbTipoFaq.ResetarComSelecione(true);
            txtDescricaoFaq.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);            
            chkAtivoFaq.Checked = false;
            chkAtivoFaq.Enabled = habilitar;           
        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
            {
                mensagens.Add("[Nome do Modulo] deve ser informado!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_formularioDeQualidade == null)
                {
                    edicao = false;
                    _formularioDeQualidade = new Tabulador.Dominio.Entidades.FormularioDeQualidade();

                    _formularioDeQualidade.idCriador = AdministracaoMDI._usuario.Id;
                }

                _formularioDeQualidade.nome = txtNome.Text;
                _formularioDeQualidade.ativo = chkAtivo.Checked;
                _formularioDeQualidade.observacao = txtObservacao.Text;

                _formularioDeQualidade.idModificador = AdministracaoMDI._usuario.Id;

                _formularioDeQualidade.id = _formularioDeQualidadeService.Gravar(_formularioDeQualidade);

                MessageBox.Show("Script [" + _formularioDeQualidade.nome + "] " + ((edicao) ? "atualizado" : "incluído") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                atualizar = true;
            }
        }

        private void ApenasValorNumerico(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txt.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void FormularioDeQualidadeForm_Load(object sender, EventArgs e)
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
        
        private void dgModulo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDoModulo(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados do Módulo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDoItem(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados do Item!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgProcedimento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDoProcedimento(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados do Procedimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgFaq_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDoFAQ(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados do FAQ!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsModulo_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDoModulo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do Módulo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsModulo_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirModulo();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"O Módulo não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir o Módulo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir o Módulo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsModulo_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDoModulo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição do Módulo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void tsModulo_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarModulo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o Módulo!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsItem_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDoItem();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do Item!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsItem_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirItem();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"O Item não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir o Item!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir o Item!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsItem_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDoItem();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição do Item!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsItem_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarItem();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o Item!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsProcedimento_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDoProcedimento();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do Procedimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsProcedimento_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirProcedimento();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"O Procedimento não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir o Procedimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir o Procedimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsProcedimento_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDoProcedimento();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição do Procedimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsProcedimento_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarProcedimento();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o Procedimento!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFaq_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoDoFAQ();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição do FAQ!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFaq_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirFAQ();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"O FAQ não pode ser excluído por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir o FAQ!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir o FAQ!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFaq_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDoFAQ();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição do FAQ!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsFaq_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarFAQ();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar o FAQ!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    $"Não foi possível gravar o Formulário!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void lnkMarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanha.SetarTodosRegistros(true);
        }

        private void lnkDesmarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbCampanha.SetarTodosRegistros(false);
        }

        #endregion EVENTOS        

        private void tsCampanha_btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}
