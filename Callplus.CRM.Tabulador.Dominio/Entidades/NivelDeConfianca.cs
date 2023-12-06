using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class NivelDeConfianca
    {
        public long id { get; set; }
        public string titulo { get; set; }
        public int idAgente { get; set; }
        public decimal nota { get; set; }
        public int idAtualizador { get; set; }
        public DateTime dataAtualizacao { get; set; }

    }
}
