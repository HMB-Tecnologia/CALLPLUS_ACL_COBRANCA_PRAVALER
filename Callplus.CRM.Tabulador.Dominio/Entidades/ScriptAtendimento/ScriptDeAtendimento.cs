using System;
using System.Collections.Generic;
using System.Linq;

namespace Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento
{
    public class ScriptDeAtendimento
    {
        public ScriptDeAtendimento()
        {
            Etapas = new List<EtapaDoScriptDeAtendimento>();
        }

        public int Id { get; set; }
        public int? IdPrimeiraEtapa { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public long IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public long IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Observacao { get; set; }
        public IEnumerable<EtapaDoScriptDeAtendimento> Etapas { get; set; }
        public EtapaDoScriptDeAtendimento PrimeiraEtapa
        {
            get { return Etapas?.FirstOrDefault(x => x.Id == IdPrimeiraEtapa); }
        }
        public EtapaDoScriptDeAtendimento EtapaAtual { get; set; }
        public string Ddd { get; set; }
    }
}
