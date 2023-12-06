using System.Collections.Generic;
using System.Linq;

namespace Callplus.CRM.Tabulador.App.Scripts
{
    public class EtapaDoScriptDeAtendimento_
    {
        //public int Ordem { get; set; }
        //public string Nome { get; set; }
        //public List<ArgumentoDeAtendimento> Argumentos { get; set; }
        //private ArgumentoDeAtendimento _argumentoDeAtendimentoAtual;
        //public ArgumentoDeAtendimento ArgumentoDeAtendimentoAtual => _argumentoDeAtendimentoAtual;
        //private Stack<ArgumentoDeAtendimento> _pilhaDeArgumentos_temp;

        //public EtapaDoScriptDeAtendimento()
        //{
        //    _pilhaDeArgumentos_temp = new Stack<ArgumentoDeAtendimento>();
        //}
        //public ArgumentoDeAtendimento IrProximoArgumento()
        //{
        //    if (_argumentoDeAtendimentoAtual == null)
        //    {
        //        var argm = Argumentos?
        //             .OrderBy(x => x.Ordem)
        //             .First();

        //        _pilhaDeArgumentos_temp.Push(argm);
        //        _argumentoDeAtendimentoAtual = argm;

        //        return _argumentoDeAtendimentoAtual;
        //    }

        //    var arg = Argumentos?
        //        .OrderBy(x => x.Ordem)
        //        .FirstOrDefault(x => x.Ordem > _argumentoDeAtendimentoAtual.Ordem);
        //    _pilhaDeArgumentos_temp.Push(arg);

        //    _argumentoDeAtendimentoAtual = arg;
        //    return _argumentoDeAtendimentoAtual;
        //}

        //public ArgumentoDeAtendimento IrParaArgumentoAnterior()
        //{
        //    if (_argumentoDeAtendimentoAtual == null)
        //    {
        //        _argumentoDeAtendimentoAtual = Argumentos.First();
        //        return _argumentoDeAtendimentoAtual;
        //    }

        //    var arg = Argumentos
        //        .OrderByDescending(x => x.Ordem)
        //        .First(x => x.Ordem < _argumentoDeAtendimentoAtual.Ordem);

        //    //JUMP CAT -- SÓ PARA APRESENTAR -- NÃO FAÇA ISSO, SÉRIO
        //    if (_pilhaDeArgumentos_temp.Any())
        //        arg = _pilhaDeArgumentos_temp.Pop();

        //    if (_pilhaDeArgumentos_temp.Any())
        //        arg = _pilhaDeArgumentos_temp.Pop();

        //    _argumentoDeAtendimentoAtual = arg;
        //    return _argumentoDeAtendimentoAtual;
        //}

        //public bool PossuiProximoArgumento()
        //{
        //    if (Argumentos == null) return false;

        //    if (_argumentoDeAtendimentoAtual == null && (Argumentos.Any()))
        //    {
        //        return true;
        //    }

        //    return Argumentos.Any(x => x.Ordem > _argumentoDeAtendimentoAtual?.Ordem);
        //}

        //public bool PossuiArgumentoAnterior()
        //{
        //    if (_argumentoDeAtendimentoAtual == null || Argumentos == null) return false;

        //    return Argumentos.Any(x => x.Ordem < _argumentoDeAtendimentoAtual.Ordem);
        //}

        //public ArgumentoDeAtendimento ResponderPerguntaEIrParaProximoArgumento(RespostaArgumentoAtendimento resposta)
        //{
        //    var arg = Argumentos.First(x => x.Id == resposta.ProximoArgumento.Id);
        //    _argumentoDeAtendimentoAtual = arg;
        //    //_pilhaDeArgumentos_temp.Push(arg);
        //    return arg;
        //}
    }
}
