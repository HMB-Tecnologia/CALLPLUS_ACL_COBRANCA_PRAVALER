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
        public int Conversao { get; set; }
        public string RankingConversao { get; set; }

        public void ValorConversao()
        {
            if (QtdLigacao == 0)
            {
                
            }
            else
            {
                double num1 = QtdVenda, num2 = QtdLigacao;

                var num = (num1 / num2) * 100;

                Conversao = (int)num;
            }
            
        }


    }
}
