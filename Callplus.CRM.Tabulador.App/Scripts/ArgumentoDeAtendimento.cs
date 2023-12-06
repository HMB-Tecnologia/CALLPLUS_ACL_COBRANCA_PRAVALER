using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.App.Scripts
{
    public class ArgumentoDeAtendimento
    {
        public long Id;
        public int Ordem { get; set; }
        public int DuracaoSegundos { get; set; }
        public string ScriptHtml { get; set; }
        public List<RespostaArgumentoAtendimento> RespostasPossiveis { get; set; }
        //public IEnumerable<KeyValuePair<int, string>> RespostasPossiveis { get; set; }
    }
}