using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class TipoDeDiscagem
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
    }
}
