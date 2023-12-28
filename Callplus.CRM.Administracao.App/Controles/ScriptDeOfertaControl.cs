using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using CallplusUtil.Extensions;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Servico.Servicos;

namespace Callplus.CRM.Administracao.App.Controles
{
    public partial class ScriptDeOfertaControl : UserControl
    {

        public ScriptDeOfertaControl()
        {
            _pilhaDeEtapas = new Stack<EtapaDoScriptDeAtendimento>();
            _statusDeOfertaService = new StatusDeAcordoService();
            InitializeComponent();

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

        #region PROPRIEDADES

        private bool _carregarHtmPendente = false;
        private string _htmlPendente = "";
        private ScriptDeAtendimento _scriptDeAtendimento;
        private readonly StatusDeAcordoService _statusDeOfertaService;
        private readonly Stack<EtapaDoScriptDeAtendimento> _pilhaDeEtapas;
        private RespostaDaEtapaDoScriptDeAtendimento _respostaSelecionada;

        public delegate void FinalizarAtendimento(StatusDeAcordo tipoResultadoFimScriptAtendimento);
        public event EventHandler<EtapaChangedEventArgs> ProximaEtapaClick;
        public event EventHandler<EtapaChangedEventArgs> VoltarEtapaClick;

        #endregion PROPRIEDADES

        #region MÉTODOS

        public void CarregarCombosIniciais()
        {
            var itens = new List<KeyValuePair<int, string>>();
            IEnumerable<KeyValuePair<int, string>> listaFake = new List<KeyValuePair<int, string>>();
            itens.Add(new KeyValuePair<int, string>((int)TipoStatusDeAcordo.Aceite, TipoStatusDeAcordo.Aceite.ToString()));
            itens.Add(new KeyValuePair<int, string>((int)TipoStatusDeAcordo.Recusa, TipoStatusDeAcordo.Recusa.ToString()));
            itens.Add(new KeyValuePair<int, string>((int)TipoStatusDeAcordo.Telefonia, TipoStatusDeAcordo.Telefonia.ToString()));

            tsComboRespostas.ComboBox.PreencherComSelecione(listaFake);
            //   tsComboTipo.ComboBox.PreencherComSelecione(listaFake);
            tsComboRespostas.ComboBox.PreencherComSelecione(listaFake);

        }

        public void Iniciar(ScriptDeAtendimento script)
        {
            lblResposta.Visible = true;
            toolSeparatorResposta.Visible = true;
            tsComboRespostas.Visible = true;

            btnVoltar.Enabled = false;
            btnAvancar.Enabled = false;

            _scriptDeAtendimento = script;
            CarregarEtapaInicial();
        }

        public void Resetar()
        {
            _respostaSelecionada = null;
            _scriptDeAtendimento = null;
            //webBrowserEtapa.AllowNavigation = true; 

            webBrowserEtapa.DocumentText = "";
            webBrowserEtapa.Stop();

            CarregarCombosIniciais();
            tsComboRespostas.ComboBox.ResetarComSelecione(habilitar: false);
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

                        bool existeProximaEtapa = _scriptDeAtendimento.
                            EtapaAtual.
                            Respostas.
                            Where(x => x.ProximaEtapa != null).Any();

                        if (!existeProximaEtapa)
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

            bool existeProximaEtapa = _scriptDeAtendimento.EtapaAtual.Respostas.Where(x => x.ProximaEtapa != null).Any();

            if (!existeProximaEtapa)
            {
                ConfigurarFimDoScript();
            }
        }

        private void ConfigurarEtapa(EtapaDoScriptDeAtendimento etapa)
        {
            tsComboRespostas.ComboBox.SelectionChangeCommitted -= ComboRespostas_OnSelectionChangeCommitted;
            btnAvancar.Enabled = false;
            btnVoltar.Enabled = false;

            var listaFake = new List<KeyValuePair<int, string>>();
            tsComboRespostas.ComboBox.PreencherComSelecione(listaFake);
            tsComboRespostas.ComboBox.ResetarComSelecione(habilitar: true);

            if (webBrowserEtapa.ReadyState != WebBrowserReadyState.Complete)
            {
                _carregarHtmPendente = true;
                _htmlPendente = etapa.DescricaoHtml;
            }
            webBrowserEtapa.DocumentText = etapa.DescricaoHtml ?? "";
            bool podeVoltar = _pilhaDeEtapas.Count > 1;
            btnVoltar.Enabled = podeVoltar;

            if (etapa == null) return;

            _pilhaDeEtapas.Push(etapa);
            podeVoltar = _pilhaDeEtapas.Count > 1;
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

            tsComboRespostas.ComboBox.SelectionChangeCommitted += ComboRespostas_OnSelectionChangeCommitted;
        }

        private void ConfigurarFimDoScript()
        {
            btnAvancar.Enabled = false;
            lblResposta.Visible = false;
            toolSeparatorResposta.Visible = false;
            tsComboRespostas.Visible = false;
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

                //btnAvancar.Enabled = true;

                _scriptDeAtendimento.EtapaAtual = etapaAnterior;
                ConfigurarEtapa(etapaAnterior);
            }
        }

        #endregion MÉTODOS

        #region EVENTOS

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

        #endregion EVENTOS
    }

    public class EtapaChangedEventArgs : EventArgs
    {
        public bool Cancelar { get; set; }
    }
}
