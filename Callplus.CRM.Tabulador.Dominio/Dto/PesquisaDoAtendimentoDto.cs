using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class PesquisaDoAtendimentoDto
    {
        public long Id { get; set; }
        public int IdPergunta { get; set; }
        public string Pergunta { get; set; }
        public int IdResposta { get; set; }
        public string Resposta { get; set; }
    }
}
