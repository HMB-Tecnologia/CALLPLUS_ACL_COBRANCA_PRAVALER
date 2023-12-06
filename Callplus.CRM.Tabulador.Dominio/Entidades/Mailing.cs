using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Mailing
    {
        public int id { get; set; }
        public int idCampanha { get; set; }
        public string nome { get; set; }        
        public bool ativo { get; set; }
        public bool indicacao { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
        public string observacao { get; set; }
        public int idStatusProcessamento { get; set; }
        public string nomeArquivo { get; set; }
    }
}