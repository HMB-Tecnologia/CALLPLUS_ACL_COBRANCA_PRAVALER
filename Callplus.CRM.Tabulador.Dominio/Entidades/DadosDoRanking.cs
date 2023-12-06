using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class DadosDoRanking
    {
        public int QtdLigacao { get; set; }
        public int QtdVenda { get; set; }
        public int MetaVenda { get; set; }
        public int Faltando { get; set; }
        public string Ranking { get; set; }     
        
    }
}
