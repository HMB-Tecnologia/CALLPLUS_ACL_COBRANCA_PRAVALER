using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class StatusDeOfertaDaCampanha
    {
        public int Id { get; set; }
        public int IdCampanha { get; set; }
        public int IdStatusDeOferta { get; set; }
    }
}
