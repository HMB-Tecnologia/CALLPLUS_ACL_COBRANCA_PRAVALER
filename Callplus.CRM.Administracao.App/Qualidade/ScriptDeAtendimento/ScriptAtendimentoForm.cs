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

namespace Callplus.CRM.Administracao.App.Qualidade.ScriptDeAtendimento
{
    public partial class ScriptAtendimentoForm : Form
    {
        public ScriptAtendimentoForm(string titulo, int idScriptDeAtendimento)
        {
            _logger = LogManager.GetCurrentClassLogger();

            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();
            _checklistService = new ChecklistService();

            if (idScriptDeAtendimento > 0)
                _scriptDeAtendimento = _scriptDeAtendimentoService.RetornarScriptDeAtendimento(idScriptDeAtendimento);

            InitializeComponent(); 

            lblTitulo.Text = titulo;
        }

        #region PROPRIEDADES

        private readonly ILogger _logger;

        private readonly ScriptDeAtendimentoService _scriptDeAtendimentoService;
        private readonly ChecklistService _checklistService;

        private Tabulador.Dominio.Entidades.ScriptAtendimento.ScriptDeAtendimento _scriptDeAtendimento;
        private EtapaDoScriptDeAtendimento _etapaScriptDeAtendimento;
        private RespostaDaEtapaDoScriptDeAtendimento _respostaScriptDeAtendimento;
        private Tabulador.Dominio.Dto.ProdutoDoScriptDeAtendimentoDto _produtoDoScriptDeAtendimentoDto;

        private int _idCampanha;

        public bool atualizar { get; set; }

        #endregion PROPRIEDADES

        #region METODOS

        private void CarregarConfiguracaoInicial()
        {
            CarregarVariaveisNoHtml();

            gvProdutoDoScript.Columns["produtos"].Width = 50;
            gvProdutoDoScript.Columns["apresentacao"].Width = 50;
            gvProdutoDoScript.Columns["finalizacao"].Width = 90;

            tcEtapa.SelectTab("tpResposta");
            tcEtapa.SelectTab("tpDadosEtapa");

            if (_scriptDeAtendimento != null)
            {
                CarregarEtapas(_scriptDeAtendimento.Id);
                pnlEtapa.Enabled = true;
                btnSimular.Enabled = true;
                CarregarDadosDoScript();
                CarregarProdutosPorCampanha(_scriptDeAtendimento.Id);
            }
            else
            {
                tcScript.TabPages.Remove(tcScript_tpProduto);

                CarregarEtapas(-5);
                pnlEtapa.Enabled = false;
                btnSimular.Enabled = false;
                CarregarProdutosPorCampanha(0);
            }

            CarregarDDD();
        }

        private void CarregarVariaveisNoHtml()
        {
            List<KeyValuePair<string, string>> _listVariable = new List<KeyValuePair<string, string>>();

            IEnumerable<VariavelDoScriptDeAtendimento> listVariaveis = _scriptDeAtendimentoService.ListarVariaveis(-1);

            foreach (var item in listVariaveis)
            {
                if (!_listVariable.Where(x => x.Key == item.nome).Any())
                    _listVariable.Add(new KeyValuePair<string, string>(item.nome, item.nome));
            }

            hecDescricao.Variable(_listVariable);
        }

        private void CarregarProdutosPorCampanha(int idScript)
        {
            DataTable retorno = _scriptDeAtendimentoService.ListarProdutosDoScriptPorCampanha(idScript);
            
            gvProdutoDoScript.AutoGenerateColumns = false;
            gvProdutoDoScript.DataSource = retorno;
        }

        private void CarregarDDD()
        {
            IEnumerable<RegionalClaro> retorno = _checklistService.ListarRegionais();

            clbDDD.Items.Clear();

            foreach (var item in retorno)
            {
                if (_scriptDeAtendimento != null && _scriptDeAtendimento.Ddd.Contains(item.ddd.ToString()))
                    clbDDD.Items.Add(item.ddd, true);
                else
                    clbDDD.Items.Add(item.ddd, false);
            }
        }

        private void CarregarEtapas(int idScript)
        {
            IEnumerable<EtapaDoScriptDeAtendimento> retorno = _scriptDeAtendimentoService.ListarEtapasDoScriptDeAtendimento(-1, idScript);
            cmbEtapa.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Id} - {x.Titulo}");
            cmbEtapaDestino.PreencherComSelecione(retorno, x => x.Id, x => $"{x.Id} - {x.Titulo}");

            dgEtapa.AutoGenerateColumns = false;
            dgEtapa.DataSource = retorno;

            foreach (DataGridViewRow row in dgEtapa.Rows)
                row.Cells["AtivoFormatado"].Value = (bool)row.Cells["Ativo"].Value ? "Sim" : "Não";
        }

        private void CarregarRespostasDaEtapa(int idEtapa)
        {
            IEnumerable<RespostaDaEtapaDoScriptDeAtendimento> retorno = _scriptDeAtendimentoService.ListarRespostasDaEtapaDoScriptDeAtendimento(-1, idEtapa);
            
            dgResposta.AutoGenerateColumns = false;
            dgResposta.DataSource = retorno;

            foreach (DataGridViewRow resposta in dgResposta.Rows)
                foreach (DataGridViewRow etapa in dgEtapa.Rows)
                {
                    if(resposta.Cells["idEtapaDestino"].Value.ToString() == etapa.Cells["IdEtapa"].Value.ToString())
                    {
                        resposta.Cells["etapaDestino"].Value = etapa.Cells["IdEtapa"].Value.ToString() + " - " + etapa.Cells["Titulo"].Value.ToString();
                    }
                }

            ResetarControlesResposta(false);
        }

        private void CarregarDadosDoScript()
        {
            txtNome.Text = _scriptDeAtendimento.Nome;

            if (_scriptDeAtendimento.IdPrimeiraEtapa > 0)
                cmbEtapa.SelectedValue = _scriptDeAtendimento.IdPrimeiraEtapa.ToString();

            if (cmbEtapa.SelectedValue == null)
                cmbEtapa.SelectedValue = "-1";

            chkAtivo.Checked = _scriptDeAtendimento.Ativo;

            txtObservacao.Text = _scriptDeAtendimento.Observacao;
        }

        private void CarregarDadosDaEtapa(int linha)
        {
            if (linha >= 0)
            {
                ResetarControlesEtapa(true);

                int id = (int)dgEtapa.Rows[linha].Cells["IdEtapa"].Value;

                _etapaScriptDeAtendimento = _scriptDeAtendimentoService.RetornarEtapaDoScriptDeAtendimento(id);
                
                txtTituloEtapa.Text = _etapaScriptDeAtendimento.Titulo;

                hecDescricao.InnerHtml = _etapaScriptDeAtendimento.DescricaoHtml;
                hecDescricao.ReadOnly = false;

                chkAtivoEtapa.Checked = _etapaScriptDeAtendimento.Ativo;

                CarregarRespostasDaEtapa(id);
            }
        }

        private void CarregarDadosDaResposta(int linha)
        {
            if (linha >= 0)
            {
                ResetarControlesResposta(true);

                int id = (int)dgResposta.Rows[linha].Cells["IdResposta"].Value;
                bool respostaAutomatica = (bool)dgResposta.Rows[linha].Cells["respostaAutomatica"].Value;

                _respostaScriptDeAtendimento = _scriptDeAtendimentoService.RetornarRespostaDaEtapaDoScriptDeAtendimento(id);
                
                if(respostaAutomatica)
                {
                    txtTituloResposta.Enabled = false;
                }

                chkRespostaAutomatica.Checked = respostaAutomatica;

                txtTituloResposta.Text = _respostaScriptDeAtendimento.Descricao;
                cmbEtapaDestino.SelectedValue = _respostaScriptDeAtendimento.IdProximaEtapaDoScriptDeAtendimento.ToString();
                chkAtivoResposta.Checked = _respostaScriptDeAtendimento.Ativo;
            }
        }

        private void CarregarDetalhesDaCampanha(int linha)
        {
            if (linha >= 0)
            {
                ResetarControlesCampanha(true);

                int idCampanha = (int)gvProdutoDoScript.Rows[linha].Cells["idCampanha"].Value;
                string campanha = gvProdutoDoScript.Rows[linha].Cells["campanha"].Value.ToString();
                string apresentacao = gvProdutoDoScript.Rows[linha].Cells["apresentacao"].Value.ToString();
                string finalizacao = gvProdutoDoScript.Rows[linha].Cells["finalizacao"].Value.ToString();

                _idCampanha = idCampanha;

                txtCampanha.Text = campanha;

                if (apresentacao == "SIM")
                    chkApresentacao.Checked = true;

                if (finalizacao == "SIM")
                    chkFinalizacao.Checked = true;

                IEnumerable<Produto> retorno = _scriptDeAtendimentoService.ListarProdutosDoScript(_scriptDeAtendimento.Id, idCampanha);

                clbProduto.Items.Clear();

                foreach (var item in retorno)
                {
                    if (item.Selecionado)
                        clbProduto.Items.Add(item.Id + " - " + item.Nome, true);
                    else
                        clbProduto.Items.Add(item.Id + " - " + item.Nome, false);
                }
            }
        }

        private void CancelarEdicaoDaEtapa()
        {
            dgEtapa.ClearSelection();

            _etapaScriptDeAtendimento = null;

            ResetarControlesEtapa(false);

            dgResposta.DataSource = null;            

            CancelarEdicaoDaResposta();

            tsResposta_btnNovo.Enabled = false;
        }

        private void CancelarEdicaoDaResposta()
        {
            dgResposta.ClearSelection();

            _respostaScriptDeAtendimento = null;

            ResetarControlesResposta(false);
        }

        private void CancelarEdicaoDaCampanha()
        {
            _idCampanha = 0;

            gvProdutoDoScript.ClearSelection();
                        
            ResetarControlesCampanha(false);
        }

        private bool AtendeRegrasDeGravacao()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
            {
                mensagens.Add("[Nome do Script] deve ser informado!");
            }

            if (cmbEtapa.TextoEhSelecione() && cmbEtapa.Items.Count > 1)
            {
                mensagens.Add("[Etapa Inicial] deve ser informada!");
            }

            if (clbDDD.CheckedItems.Count == 0)
            {
                mensagens.Add("[DDD] deve(m) ser informado(s)!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDaEtapa()
        {
            var mensagens = new List<string>();

            if (string.IsNullOrEmpty(txtTituloEtapa.Text.Trim()))
            {
                mensagens.Add("[Título da Etapa] deve ser informado!");
            }

            if (string.IsNullOrEmpty(hecDescricao.InnerText))
            {
                mensagens.Add("[Descrição da Etapa] deve ser informada!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private bool AtendeRegrasDeGravacaoDaResposta()
        {
            var mensagens = new List<string>();
                        
            foreach (DataGridViewRow item in dgResposta.Rows)
            {
                if ((bool)item.Cells["respostaAutomatica"].Value &&
                    (_respostaScriptDeAtendimento == null || item.Cells["idResposta"].Value.ToString() != _respostaScriptDeAtendimento.Id.ToString()))
                {
                    mensagens.Add("[Resposta] não pode ser gravada, pois a etapa está configurada para resposta automática!");
                }
            }

            if(chkRespostaAutomatica.Checked && 
                (dgResposta.Rows.Count > 1 && _respostaScriptDeAtendimento != null || dgResposta.Rows.Count > 0 && _respostaScriptDeAtendimento == null))
            {
                mensagens.Add("[Resposta Automática] não pode ser gravada, pois já existem respostas gravadas!");
            }
            
            if (string.IsNullOrEmpty(txtTituloResposta.Text.Trim()))
            {
                mensagens.Add("[Título da Resposta] deve ser informado!");
            }

            //if (cmbEtapaDestino.TextoEhSelecione())
            //{
            //    mensagens.Add("[Etapa Destino] deve ser informada!");
            //}

            if (!cmbEtapaDestino.TextoEhSelecione() && cmbEtapaDestino.SelectedValue.ToString() == _etapaScriptDeAtendimento.Id.ToString())
            {
                mensagens.Add("[Etapa Destino] não pode ser a etapa corrente!");
            }

            CallplusFormsUtil.ExibirMensagens(mensagens);

            return mensagens.Any() == false;
        }

        private void Gravar()
        {
            if (AtendeRegrasDeGravacao())
            {
                bool edicao = true;

                if (_scriptDeAtendimento == null)
                {
                    edicao = false;
                    _scriptDeAtendimento = new Tabulador.Dominio.Entidades.ScriptAtendimento.ScriptDeAtendimento();

                    _scriptDeAtendimento.IdCriador = AdministracaoMDI._usuario.Id;
                }

                _scriptDeAtendimento.Nome = txtNome.Text;
                _scriptDeAtendimento.Ativo = chkAtivo.Checked;

                if (!cmbEtapa.TextoEhSelecione())
                    _scriptDeAtendimento.IdPrimeiraEtapa = Convert.ToInt32(cmbEtapa.SelectedValue);

                _scriptDeAtendimento.Observacao = txtObservacao.Text;

                _scriptDeAtendimento.IdModificador = AdministracaoMDI._usuario.Id;

                string ddd = "";

                foreach (var item in clbDDD.CheckedItems)
                {
                    ddd += item.ToString() + ",";
                }

                _scriptDeAtendimento.Ddd = ddd;

                _scriptDeAtendimento.Id = _scriptDeAtendimentoService.GravarScriptDeAtendimento(_scriptDeAtendimento);

                MessageBox.Show("Script [" + _scriptDeAtendimento.Nome + "] " + ((edicao)? "atualizado" : "incluído") + " com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!pnlEtapa.Enabled)
                {
                    pnlEtapa.Enabled = true;
                    btnSimular.Enabled = true;
                }

                if(!tcScript.TabPages.Contains(tcScript_tpProduto))
                    tcScript.TabPages.Add(tcScript_tpProduto);

                atualizar = true;
            }
        }

        private void GravarEtapa()
        {
            if (AtendeRegrasDeGravacaoDaEtapa())
            {
                if (_etapaScriptDeAtendimento == null)
                {
                    _etapaScriptDeAtendimento = new Tabulador.Dominio.Entidades.ScriptAtendimento.EtapaDoScriptDeAtendimento();
                }

                _etapaScriptDeAtendimento.IdScriptDeAtendimento = _scriptDeAtendimento.Id;

                _etapaScriptDeAtendimento.Titulo = txtTituloEtapa.Text;

                hecDescricao.ReadOnly = true;
                _etapaScriptDeAtendimento.DescricaoHtml = hecDescricao.DocumentHtml;

                _etapaScriptDeAtendimento.Ativo = chkAtivoEtapa.Checked;

                _etapaScriptDeAtendimento.Id = _scriptDeAtendimentoService.GravarEtapaDoScriptDeAtendimento(_etapaScriptDeAtendimento);

                MessageBox.Show("Etapa gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                object idPrimeiraEtapa = cmbEtapa.SelectedValue;
                
                CarregarEtapas(_scriptDeAtendimento.Id);

                cmbEtapa.SelectedValue = idPrimeiraEtapa;

                if (cmbEtapa.SelectedValue == null)
                    cmbEtapa.SelectedValue = "-1";

                CancelarEdicaoDaEtapa();
            }
        }

        private void GravarResposta()
        {
            if (AtendeRegrasDeGravacaoDaResposta())
            {
                if (_respostaScriptDeAtendimento == null)
                {
                    _respostaScriptDeAtendimento = new Tabulador.Dominio.Entidades.ScriptAtendimento.RespostaDaEtapaDoScriptDeAtendimento();
                }

                _respostaScriptDeAtendimento.IdEtapaDoScriptDeAtendimento = _etapaScriptDeAtendimento.Id;
                _respostaScriptDeAtendimento.Descricao = txtTituloResposta.Text;
                _respostaScriptDeAtendimento.IdProximaEtapaDoScriptDeAtendimento = Convert.ToInt32(cmbEtapaDestino.SelectedValue);
                _respostaScriptDeAtendimento.Ativo = chkAtivoResposta.Checked;
                _respostaScriptDeAtendimento.RespostaAutomatica = chkRespostaAutomatica.Checked;

                _respostaScriptDeAtendimento.Id = _scriptDeAtendimentoService.GravarRespostaDaEtapaDoScriptDeAtendimento(_respostaScriptDeAtendimento);

                MessageBox.Show("Resposta gravada com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarRespostasDaEtapa(_respostaScriptDeAtendimento.IdEtapaDoScriptDeAtendimento);

                CancelarEdicaoDaResposta();
            }
        }

        private void GravarProdutos()
        {
            _produtoDoScriptDeAtendimentoDto = new Tabulador.Dominio.Dto.ProdutoDoScriptDeAtendimentoDto();

            _produtoDoScriptDeAtendimentoDto.IdScriptDeAtendimento = _scriptDeAtendimento.Id;
            _produtoDoScriptDeAtendimentoDto.IdCampanha = _idCampanha;

            string produtos = "";
            string[] splitProduto;

            foreach (var item in clbProduto.CheckedItems)
            {
                splitProduto = item.ToString().Split('-');

                if (splitProduto.Count() > 0)
                    produtos += splitProduto[0].Trim() + ",";
            }

            _produtoDoScriptDeAtendimentoDto.Produtos = produtos;
            _produtoDoScriptDeAtendimentoDto.Apresentacao = chkApresentacao.Checked;
            _produtoDoScriptDeAtendimentoDto.Finalizacao = chkFinalizacao.Checked;

            _scriptDeAtendimentoService.GravarProdutosDoScriptDeAtendimento(_produtoDoScriptDeAtendimentoDto);

            MessageBox.Show("Registro gravado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CarregarProdutosPorCampanha(_scriptDeAtendimento.Id);

            CancelarEdicaoDaCampanha();
        }

        private void ResetarControlesEtapa(bool habilitar)
        {
            tsEtapa_btnNovo.Enabled = !habilitar;
            tsEtapa_btnExcluir.Enabled = habilitar;
            tsEtapa_btnCancelar.Enabled = habilitar;
            tsEtapa_btnSalvar.Enabled = habilitar;

            txtTituloEtapa.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            hecDescricao.InnerHtml = "";
            hecDescricao.ReadOnly = !habilitar;
            chkAtivoEtapa.Checked = false;
            chkAtivoEtapa.Enabled = habilitar;            
        }

        private void ResetarControlesResposta(bool habilitar)
        {
            tsResposta_btnNovo.Enabled = !habilitar;
            tsResposta_btnExcluir.Enabled = habilitar;
            tsResposta_btnCancelar.Enabled = habilitar;
            tsResposta_btnSalvar.Enabled = habilitar;

            txtTituloResposta.Resetar(habilitar: true, limparTexto: true, readOnly: !habilitar);
            chkRespostaAutomatica.Checked = false;
            chkRespostaAutomatica.Enabled = habilitar;
            cmbEtapaDestino.ResetarComSelecione(habilitar);
            chkAtivoResposta.Checked = false;
            chkAtivoResposta.Enabled = habilitar;
        }

        private void ResetarControlesCampanha(bool habilitar)
        {
            tsProdutoDoScript_btnCancelar.Enabled = habilitar;
            tsProdutoDoScript_btnSalvar.Enabled = habilitar;

            txtCampanha.Resetar(habilitar: true, limparTexto: true, readOnly: true);

            chkApresentacao.Checked = false;
            chkApresentacao.Enabled = habilitar;

            chkFinalizacao.Checked = false;
            chkFinalizacao.Enabled = habilitar;
            
            clbProduto.Enabled = habilitar;
            clbProduto.Items.Clear();
        }

        public void Iniciar()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowDialog();
        }

        private void IniciarEdicaoEtapa()
        {   
            ResetarControlesEtapa(true);
        }

        private void IniciarEdicaoResposta()
        {
            ResetarControlesResposta(true);
        }

        private void ExcluirEtapa()
        {
            _scriptDeAtendimentoService.ExcluirEtapaDoScriptDeAtendimento(_etapaScriptDeAtendimento.Id);

            object idPrimeiraEtapa = cmbEtapa.SelectedValue;

            CarregarEtapas(_etapaScriptDeAtendimento.IdScriptDeAtendimento);

            cmbEtapa.SelectedValue = idPrimeiraEtapa;

            if (cmbEtapa.SelectedValue == null)
                cmbEtapa.SelectedValue = "-1";          

            CancelarEdicaoDaEtapa();

            MessageBox.Show("Etapa excluída com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExcluirResposta()
        {
            _scriptDeAtendimentoService.ExcluirRespostaDaEtapaDoScriptDeAtendimento(_respostaScriptDeAtendimento.Id);

            CarregarRespostasDaEtapa(_respostaScriptDeAtendimento.IdEtapaDoScriptDeAtendimento);

            CancelarEdicaoDaResposta();

            MessageBox.Show("Resposta excluída com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void IniciarSimulador()
        {
            SimuladorScriptAtendimentoForm f = new SimuladorScriptAtendimentoForm();

            f.Iniciar(_scriptDeAtendimento.Id);
        }

        #endregion METODOS

        #region EVENTOS

        private void dgEtapa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDaEtapa(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados da Etapa!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgResposta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDadosDaResposta(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os dados da Resposta!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScriptAtendimentoForm_Load(object sender, EventArgs e)
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

        private void tsEtapa_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDaEtapa();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Etapa!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsEtapa_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoEtapa();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da Etapa!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void tsEtapa_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarEtapa();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a Etapa!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsResposta_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDaResposta();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Resposta!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void tsResposta_btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarEdicaoResposta();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível iniciar a edição da Resposta!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsResposta_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarResposta();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar a Etapa!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    $"Não foi possível gravar o Script!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void chkUsarRespostaAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRespostaAutomatica.Checked)
            {
                txtTituloResposta.Text = "RESPOSTA AUTOMATICA";
                txtTituloResposta.Enabled = false;
            }
            else
            {
                txtTituloResposta.Text = "";
                txtTituloResposta.Enabled = true;
            }
        }

        private void tsResposta_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirResposta();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"A Resposta não pode ser excluída por ter dependências na base de dados!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir a Resposta!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsEtapa_btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirEtapa();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show($"A Resposta não pode ser excluída por ter dependências na base de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show($"Não foi possível excluir a Resposta!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível excluir a Resposta!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            try
            {
                IniciarSimulador();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar o simulador!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvProdutoDoScript_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CarregarDetalhesDaCampanha(e.RowIndex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível carregar os detalhes da Campanha!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsProdutoDoScript_btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelarEdicaoDaCampanha();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível cancelar a edição da Campanha!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsProdutoDoScript_btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                GravarProdutos();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                MessageBox.Show(
                    $"Não foi possível gravar os vínculos da Campanha!\n\nErro:{ex.Message}\n\n\nStacktrace:{ex.StackTrace}", "Erro do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkMarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbDDD.SetarTodosRegistros(true);
        }

        private void lnkDesmarcarTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbDDD.SetarTodosRegistros(false);
        }

        private void lnkMarcarTodosProdutos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbProduto.SetarTodosRegistros(true);
        }

        private void lnkDesmarcarTodosProdutos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clbProduto.SetarTodosRegistros(false);
        }

        #endregion EVENTOS
    }
}
