using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.App.Scripts;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using CallplusUtil.Extensions;

namespace Callplus.CRM.Tabulador.App.Controles
{
    public partial class ScriptAceiteControl : UserControl
    {
        public ScriptAceiteControl()
        {
            InitializeComponent();
        }

        #region Propriedades

        public event EventHandler<EtapaChangedEventArgs> ProximaEtapaClick;
        public event EventHandler<EtapaChangedEventArgs> VoltarEtapaClick;
        public event EventHandler<EtapaScriptAceite> ProximaEtapaCommitted;
        public event EventHandler<EtapaScriptAceite> VoltarEtapaCommitted;
        public event EventHandler FinalizarScriptClick;
        public event EventHandler CancelarScriptClick;
        private ScriptDeAceite _scriptDeAceite;
        private ScriptDeAtendimento _scriptDeAtendimento;

        #endregion

        #region Métodos

        public void Iniciar(ScriptDeAtendimento script)
        {
            _scriptDeAtendimento = script;
            Avancar();
        }

        private void Avancar()
        {




            var args = new EtapaChangedEventArgs { Cancelar = false };
            EtapaScriptAceite etapa = null;

            ProximaEtapaClick?.Invoke(this, args);

  

            ConfigurarEtapa(_scriptDeAceite?.EtapaAtual);
            ProximaEtapaCommitted?.Invoke(this, _scriptDeAceite?.EtapaAtual);
        }

        private void Voltar()
        {
            var args = new EtapaChangedEventArgs { Cancelar = false };
            VoltarEtapaClick?.Invoke(this, args);

           

            _scriptDeAceite?.IrParaEtapaAnterior();
            ConfigurarEtapa(_scriptDeAceite?.EtapaAtual);
            VoltarEtapaCommitted?.Invoke(this, _scriptDeAceite?.EtapaAtual);
        }

        private void FinalizarScript()
        {
            FinalizarScriptClick?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarEtapa(EtapaScriptAceite etapa)
        {
            btnVoltar.Enabled = false;
            btnAvancar.Enabled = false;

            var html = (etapa?.TextoHtml ?? "");
            webBrowser.DocumentText = html;


            var podeAvancar = _scriptDeAceite?.PossuiProximaEtapa() ?? false;
            btnAvancar.Enabled = podeAvancar;

            var podeVoltar = _scriptDeAceite?.PossuiEtapaAnterior() ?? false;
            btnVoltar.Enabled = podeVoltar;
        }

        public void Resetar()
        {
            //webBrowser.DocumentText = "";
        }

        #endregion

        #region Eventos
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Resetar();
            
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            Avancar();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Voltar();
        }

        private void btnFinalizarScript_Click(object sender, EventArgs e)
        {
            FinalizarScript();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarScriptClick?.Invoke(this, e);
        }


        #endregion

    }

    public class EtapaChangedEventArgs : EventArgs
    {
        public bool Cancelar { get; set; }
    }
}
