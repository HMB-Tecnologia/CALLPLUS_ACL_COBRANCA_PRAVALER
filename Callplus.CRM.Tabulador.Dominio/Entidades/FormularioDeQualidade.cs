using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FormularioDeQualidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public bool ativo { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
        public string observacao { get; set; }
    }
}
