using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class HistoricoLeitura
    {
        public int Id { get; set; }
        public int IdNotificacao { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataLeitura { get; set; }
    }
}
