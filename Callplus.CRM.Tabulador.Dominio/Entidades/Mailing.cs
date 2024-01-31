using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Mailing
    {
        public int Id { get; set; }
        public int IdCampanha { get; set; }
		public string Nome { get; set; }
		public bool Ativo { get; set; }
        public bool Indicacao { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Observacao { get; set; }
        public int IdStatusProcessamento { get; set; }
        public string NomeArquivo { get; set; }
    }
}