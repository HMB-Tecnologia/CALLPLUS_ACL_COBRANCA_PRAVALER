using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FrasesGamificacao
    {
        public int id { get; set; }
        public int idTipo { get; set; }
        public int idGamificacao { get; set; }
        public string frase { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
        public bool ativo { get; set; }

    }
}
