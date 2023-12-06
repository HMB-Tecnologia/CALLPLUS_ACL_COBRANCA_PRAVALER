using System;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class ResumoDaOfertaDoAtendimentoBkoDTO
    {
        public long idOferta { get; set; }
        public string campanha { get; set; }
        public string mailing { get; set; }
        public string operador { get; set; }
        public string supervisor { get; set; }
        public string statusOferta { get; set; }
        public string auditor { get; set; }
        public string statusAuditoria { get; set; }
        public DateTime? dataRegistroOferta { get; set; }
        public DateTime? dataAuditoria { get; set; }
        public string Observacao { get; set; }
    }
}
