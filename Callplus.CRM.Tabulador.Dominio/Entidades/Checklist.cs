using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Checklist
    {
        public int id { get; set; }
        public int idCampanha { get; set; }
        public string nome { get; set; }
        public string titulo { get; set; }
        public string palavraChaveMailing { get; set; }
        public string produtos { get; set; }
        public string regionais { get; set; }
        public bool ativo { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
        public string observacao { get; set; }
    }
}
