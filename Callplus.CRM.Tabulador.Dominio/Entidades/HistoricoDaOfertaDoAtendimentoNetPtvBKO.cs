using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class HistoricoDaOfertaDoAtendimentoNetPtvBKO
    {
        public long id { get; set; }
        public long idOfertaDoAtendimentoNetPtvBKO { get; set; }
        public int idStatusAuditoria { get; set; }
        public string protocolo { get; set; }
        public string numeroDoPedido { get; set; }
        public string numeroDoContrato { get; set; }
        public DateTime? dataInput { get; set; }
        public string loginNet { get; set; }
        public string codigoAgente { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public string observacao { get; set; }
        public DateTime? dataInstalacaoCorrigida { get; set; }
        public string periodoCorrigido { get; set; }
    }
}
