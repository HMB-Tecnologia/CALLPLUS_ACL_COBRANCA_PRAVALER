using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class CampanhaDoStatusDeAtendimento
    {
        public int Id { get; set; }
        public int IdCampanha { get; set; }
        public int IdStatusDeAtendimento { get; set; }
    }
}
