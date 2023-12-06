using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class TipoDeFrasesGamificacao
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public string tipoDecimal { get; set; }
        public bool ativo { get; set; }

    }
}
