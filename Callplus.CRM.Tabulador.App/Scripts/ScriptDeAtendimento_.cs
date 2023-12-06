using System;
using System.Collections.Generic;
using System.Linq;

namespace Callplus.CRM.Tabulador.App.Scripts
{
    public class ScriptDeAtendimento_
    {
        //public long Id { get; set; }
        //public string Nome { get; set; }
        //public bool Ativo { get; set; }
        //public long IdCriador { get; set; }
        //public DateTime DataCriacao { get; set; }
        //public string Observacao { get; set; }

        //private EtapaDoScriptDeAtendimento _etapaAtual;
        //public EtapaDoScriptDeAtendimento EtapaAtual => _etapaAtual;

        //public List<EtapaDoScriptDeAtendimento> Etapas { get; set; }

        //public EtapaDoScriptDeAtendimento IrParaProximaEtapa()
        //{
        //    if (_etapaAtual == null)
        //    {
        //        _etapaAtual = Etapas
        //            .OrderBy(x => x.Ordem)
        //            .First();

        //        return _etapaAtual;
        //    }

        //    var etapa = Etapas
        //        .OrderBy(x => x.Ordem)
        //        .First(x => x.Ordem > _etapaAtual.Ordem);

        //    _etapaAtual = etapa;
        //    return _etapaAtual;
        //}

        //public EtapaDoScriptDeAtendimento IrParaEtapaAnterior()
        //{
        //    if (_etapaAtual == null)
        //    {
        //        _etapaAtual = Etapas.First();
        //        return _etapaAtual;
        //    }

        //    var etapa = Etapas
        //        .OrderByDescending(x=>x.Ordem)
        //        .First(x => x.Ordem < _etapaAtual.Ordem);

        //    _etapaAtual = etapa;
        //    return _etapaAtual;
        //}

        //public bool PossuiProximaEtapa()
        //{
        //    if (_etapaAtual == null && Etapas.Any())
        //    {
        //        return true;
        //    }

        //    return Etapas.Any(x => x.Ordem > _etapaAtual?.Ordem);
        //}

        //public bool PossuiEtapaAnterior()
        //{
        //    if (_etapaAtual == null)
        //    {
        //        return false;
        //    }

        //    return Etapas.Any(x => x.Ordem < _etapaAtual?.Ordem);
        //}
    }
}
