using System.Collections.Generic;
using System.Linq;

namespace Callplus.CRM.Tabulador.App.Scripts
{
    public class ScriptDeAceite
    {
        private EtapaScriptAceite _etapaAtual;
        public EtapaScriptAceite EtapaAtual => _etapaAtual;
        public List<EtapaScriptAceite> Etapas { get; set; }

        public ScriptDeAceite()
        {
            
        }

        public EtapaScriptAceite IrParaProximaEtapa()
        {
            if (_etapaAtual == null)
            {
                _etapaAtual = Etapas
                    .OrderBy(x => x.Ordem)
                    .First();

                return _etapaAtual;
            }

            var etapa = Etapas
                .OrderBy(x => x.Ordem)
                .First(x => x.Ordem > _etapaAtual.Ordem);

            _etapaAtual = etapa;
            return _etapaAtual;
        }

        public EtapaScriptAceite IrParaEtapaAnterior()
        {
            if (_etapaAtual == null)
            {
                _etapaAtual = Etapas.First();
                return _etapaAtual;
            }

            var etapa = Etapas
                .OrderByDescending(x => x.Ordem)
                .First(x => x.Ordem < _etapaAtual.Ordem);

            _etapaAtual = etapa;
            return _etapaAtual;
        }

        public bool PossuiProximaEtapa()
        {
            if (_etapaAtual == null && Etapas.Any())
            {
                return true;
            }

            return Etapas.Any(x => x.Ordem > _etapaAtual?.Ordem);
        }

        public bool PossuiEtapaAnterior()
        {
            if (_etapaAtual == null)
            {
                return false;
            }

            return Etapas.Any(x => x.Ordem < _etapaAtual?.Ordem);
        }

    }
}
