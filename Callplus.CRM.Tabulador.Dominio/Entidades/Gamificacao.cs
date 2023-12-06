using System;
using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Gamificacao
    {
        public int id { get; set; }
        public string titulo { get; set; }
        //public string fraseInicial { get; set; }
        //public string fraseFinal { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
        public bool ativo { get; set; }
        public string observacao { get; set; }

        public List<FrasesGamificacao> ItemList { get; set; }

    }
}
