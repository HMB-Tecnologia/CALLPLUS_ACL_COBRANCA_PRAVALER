using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO
    {
        public long id { get; set; }
        public long idOfertaDoAtendimentoRentabilizacaoBKO { get; set; }
        public int idStatusAuditoria { get; set; }
        public string protocolo { get; set; }
        public string autorizacao { get; set; }
        public DateTime? dataInput { get; set; }
        public string loginWM { get; set; }
        public string codigoAgente { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }        
        public string Observacao { get; set; }
    }
}
