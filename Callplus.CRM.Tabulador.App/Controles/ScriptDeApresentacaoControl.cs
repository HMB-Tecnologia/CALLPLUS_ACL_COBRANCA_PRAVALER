using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.App.Scripts;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using CallplusUtil.Extensions;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Servico.Servicos;

namespace Callplus.CRM.Tabulador.App.Controles
{
    public partial class ScriptDeApresentacaoControl : UserControl
    {
        public ScriptDeApresentacaoControl()
        {
            _pilhaDeEtapas = new Stack<EtapaDoScriptDeAtendimento>();
            _statusDeOfertaService = new StatusDeOfertaService();
            _scriptDeAtendimentoService = new ScriptDeAtendimentoService();
            InitializeComponent();

            try
            {
                //((Control)webBrowserEtapa).Enabled = false;
                webBrowserEtapa.DocumentCompleted += (sender, args) =>
                {
                    if (_carregarHtmPendente)
                    {
                        _carregarHtmPendente = false;
                        webBrowserEtapa.DocumentText = _htmlPendente;
                        _htmlPendente = "";
                    }
                };
            }
            catch
            { }
        }

        #region PROPRIEDADES
        private bool _carregarHtmPendente = false;
        private string _htmlPendente = "";
        private ScriptDeAtendimento _scriptDeAtendimento;
        private Campanha _campanha;
        private StatusDeOfertaService _statusDeOfertaService;
        private ScriptDeAtendimentoService _scriptDeAtendimentoService;
        private readonly Stack<EtapaDoScriptDeAtendimento> _pilhaDeEtapas;
        private RespostaDaEtapaDoScriptDeAtendimento _respostaSelecionada;

        public event EventHandler<EtapaChangedEventArgs> ProximaEtapaClick;
        public event EventHandler<EtapaChangedEventArgs> VoltarEtapaClick;
        public event EventHandler ApresentarOfertaClick;

        private Form _form;
        private IEnumerable<VariavelDoScriptDeAtendimento> _variavelDoScriptDeAtendimento;

        #endregion PROPRIEDADES

        #region MÉTODOS

        public void CarregarCombosIniciais()
        {
            var itens = new List<KeyValuePair<int, string>>();
            IEnumerable<KeyValuePair<int, string>> listaFake = new List<KeyValuePair<int, string>>();
            itens.Add(new KeyValuePair<int, string>((int)TipoResultadoFimScriptAtendimento.Recusa, TipoResultadoFimScriptAtendimento.Recusa.ToString()));
            itens.Add(new KeyValuePair<int, string>((int)TipoResultadoFimScriptAtendimento.Telefonia, TipoResultadoFimScriptAtendimento.Telefonia.ToString()));

            tsComboRespostas.ComboBox.PreencherComSelecione(listaFake);

        }

        public void Iniciar(ScriptDeAtendimento script, Campanha campanha, Form form, bool apresentarOferta)
        {
            _scriptDeAtendimento = script;
            _campanha = campanha;
            _form = form;

            _variavelDoScriptDeAtendimento = _scriptDeAtendimentoService.ListarVariaveis(campanha.Id);

            if (!apresentarOferta)
                btnApresentarOferta.Enabled = false;

            CarregarEtapaInicial();
        }

        public void Resetar()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Resetar));
            }
            else
            {
                webBrowserEtapa.AllowNavigation = true;
                _respostaSelecionada = null;
                tsComboRespostas.ComboBox.ResetarComSelecione(habilitar: false);
                lblResposta.Visible = false;
                toolSeparatorResposta.Visible = false;
                tsComboRespostas.Visible = false;
                // webBrowser1.DocumentText = "";
            }


        }

        private void Avancar()
        {
            if (_scriptDeAtendimento.EtapaAtual == null)
                return;

            bool etapaAtualpossuiRespostas = _scriptDeAtendimento.EtapaAtual.Respostas?.Any() ?? false;
            if (etapaAtualpossuiRespostas)
            {
                var selecionouReposta = _respostaSelecionada != null;//tsComboRespostas.ComboBox.TextoEhSelecione() == false;
                if (selecionouReposta)
                {
                    EtapaDoScriptDeAtendimento proximaEtapa = _respostaSelecionada?.ProximaEtapa;
                    if (proximaEtapa != null)
                    {
                        _scriptDeAtendimento.EtapaAtual = proximaEtapa;
                        ConfigurarEtapa(proximaEtapa);

                        bool existeProximaEtapa = _scriptDeAtendimento.EtapaAtual.Respostas.Where(x => x.ProximaEtapa != null).Any();

                        if(!existeProximaEtapa)
                        {
                            ConfigurarFimDoScript();
                        }
                    }
                    else
                    {
                        ConfigurarFimDoScript();
                    }
                }
            }
        }

        private void CarregarEtapaInicial()
        {
            if (_scriptDeAtendimento.PrimeiraEtapa == null)
            {
                MessageBox.Show("O Script não possui uma etapa inicial configurada.", "Aviso do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _pilhaDeEtapas.Clear();

            _scriptDeAtendimento.EtapaAtual = _scriptDeAtendimento.PrimeiraEtapa;
            ConfigurarEtapa(_scriptDeAtendimento.PrimeiraEtapa);

            // _pilhaDeEtapas.Push(_scriptDeAtendimento.PrimeiraEtapa);
        }

        private void ConfigurarEtapa(EtapaDoScriptDeAtendimento etapa)
        {
            tsComboRespostas.ComboBox.SelectionChangeCommitted -= ComboRespostas_OnSelectionChangeCommitted;
            btnAvancar.Enabled = false;
            btnVoltar.Enabled = false;

            var listaFake = new List<KeyValuePair<int, string>>();
            tsComboRespostas.ComboBox.PreencherComSelecione(listaFake);
            tsComboRespostas.ComboBox.ResetarComSelecione(habilitar: true);

            try
            {
                string html = AtribuirVariaveisDaEtapa(etapa.DescricaoHtml) ?? "";

                if (webBrowserEtapa.ReadyState != WebBrowserReadyState.Complete)
                {
                    _carregarHtmPendente = true;
                    _htmlPendente = html;
                }
                webBrowserEtapa.DocumentText = html;
            }
            catch
            { }
            
            if (etapa == null) return;

            _pilhaDeEtapas.Push(etapa);

            bool podeVoltar = _pilhaDeEtapas.Count > 1;

            btnVoltar.Enabled = podeVoltar;

            if (etapa.Respostas != null && etapa.Respostas.Any())
            {
                tsComboRespostas.ComboBox.PreencherComSelecione(etapa.Respostas, resposta => resposta.Id, x => x.Descricao);
                tsComboRespostas.ComboBox.ResetarComSelecione(habilitar: true);
                if (etapa.Respostas.Count() == 1)
                {
                    var resposta = etapa.Respostas.First();
                    tsComboRespostas.ComboBox.Text = resposta.Descricao;
                    SelecionarResposta(resposta.Id);
                }
                else
                {
                    SelecionarResposta(0);
                }

            }

            tsComboRespostas.ComboBox.SelectionChangeCommitted -= ComboRespostas_OnSelectionChangeCommitted;
        }

        private void ConfigurarFimDoScript()
        {
            btnAvancar.Enabled = false;
            btnVoltar.Enabled = true;
        }

        private void SelecionarResposta(int idResposta)
        {
            if (idResposta <= 0)
            {
                btnAvancar.Enabled = false;
                lblResposta.Visible = true;
                toolSeparatorResposta.Visible = true;
                tsComboRespostas.Visible = true;
            };
            RespostaDaEtapaDoScriptDeAtendimento resposta = _scriptDeAtendimento?.EtapaAtual?.Respostas?.FirstOrDefault(x => x.Id == idResposta);
            _respostaSelecionada = resposta;

            if (resposta == null) return;


            var podeAvancar = true;
            btnAvancar.Enabled = podeAvancar;

            if (_pilhaDeEtapas.Count > 1)
            {
                btnVoltar.Enabled = true;
            }

            if (resposta.RespostaAutomatica)
            {
                btnAvancar.Enabled = true;
                lblResposta.Visible = false;
                toolSeparatorResposta.Visible = false;
                tsComboRespostas.Visible = false;
            }
            else
            {
                lblResposta.Visible = true;
                toolSeparatorResposta.Visible = true;
                tsComboRespostas.Visible = true;
            }
        }

        private void Voltar()
        {
            if (_pilhaDeEtapas.Count > 0)
            {
                var etapaAtual = _pilhaDeEtapas.Pop();
                var etapaAnterior = _pilhaDeEtapas.Pop();

                _scriptDeAtendimento.EtapaAtual = etapaAnterior;
                ConfigurarEtapa(etapaAnterior);
            }
        }

        private string RetornarTextoDoControle(string nomeControle)
        {
            string result = null;

            Control[] lista = _form.Controls.Find(nomeControle, true);

            if (lista.Count() > 0)
            {
                if (lista[0].ToString().ToUpper().Contains("CUSTOM"))
                {
                    result = ((App.CamposDinamicos.CustomTextBox)(lista[0])).TextBoxText;
                }
                else
                {
                    result = lista[0].Text;
                }
            }

            return result;
        }

        private string AtribuirVariaveisDaEtapa(string html)
        {
            foreach (var item in _variavelDoScriptDeAtendimento)
            {
                string result = RetornarTextoDoControle(item.controleDaTela);

                if (result != null)
                    html = html.Replace(item.nome, result);
            }

            return html;
        }

        #endregion

        #region Eventos
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Resetar();

            tsComboRespostas.ComboBox.SelectionChangeCommitted += ComboRespostas_OnSelectionChangeCommitted;
            CarregarCombosIniciais();
        }

        protected virtual void OnProximaEtapaClick(EtapaChangedEventArgs e)
        {
            ProximaEtapaClick?.Invoke(this, e);
        }

        protected virtual void OnVoltarEtapaClick(EtapaChangedEventArgs e)
        {
            VoltarEtapaClick?.Invoke(this, e);
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            Avancar();
        }

        private void ComboRespostas_OnSelectionChangeCommitted(object sender, EventArgs eventArgs)
        {
            int idResposta = 0;
            int.TryParse(tsComboRespostas.ComboBox.SelectedValue.ToString(), out idResposta);
            if (idResposta == 0) idResposta = -1;
            SelecionarResposta(idResposta);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Voltar();
        }
        #endregion

        private void btnApresentarOferta_Click(object sender, EventArgs e)
        {
            ApresentarOfertaClick?.Invoke(this, EventArgs.Empty);
        }

        private void webBrowserEtapa_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
