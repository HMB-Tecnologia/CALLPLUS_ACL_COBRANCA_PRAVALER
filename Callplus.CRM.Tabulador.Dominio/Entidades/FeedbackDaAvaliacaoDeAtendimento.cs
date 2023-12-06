using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FeedbackDaAvaliacaoDeAtendimento
    {
        public long id { get; set; }
        public long idAvaliacaoDeAtendimento { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}
