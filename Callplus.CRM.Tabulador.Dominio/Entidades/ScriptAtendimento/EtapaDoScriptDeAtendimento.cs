using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento
{
    public class EtapaDoScriptDeAtendimento
    {
        public EtapaDoScriptDeAtendimento()
        {
            Respostas = new List<RespostaDaEtapaDoScriptDeAtendimento>();
        }

        public int Id { get; set; }
        public int IdScriptDeAtendimento { get; set; }
        public string Titulo { get; set; }
        public string DescricaoHtml { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<RespostaDaEtapaDoScriptDeAtendimento> Respostas { get; set; }

        
    }
}
