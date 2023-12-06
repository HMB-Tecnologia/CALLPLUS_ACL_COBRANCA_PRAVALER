using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class TipoDeProduto
    {   
        public long Id { get; set; }        
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public bool PermiteFaixaDeRecarga { get; set; }

    }
}
