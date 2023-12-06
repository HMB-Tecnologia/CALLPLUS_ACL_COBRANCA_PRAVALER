using System;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class HistoricoDaOfertaDoAtendimentoBkoDTO
    {
        public long Id { get; set; }
        public long IdOferta { get; set; }
        public string Auditor { get; set; }
        public int? IdStatusAuditoria { get; set; }
        public string Protocolo { get; set; }
        public string Autorizacao { get; set; }
        public string numeroDoContrato { get; set; }
        public string numeroDoPedido { get; set; }
        public string ordem { get; set; }
        public long numeroProvisorio { get; set; }
        public DateTime? DataInput { get; set; }
        public string LoginWM { get; set; }
        public string CodigoAgente { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }        
        public string Observacao { get; set; }
        public string StatusDeAuditoria { get; set; }
        public DateTime? dataInstalacaoCorrigida { get; set; }
        public string periodoCorrigido { get; set; }
    }
}
