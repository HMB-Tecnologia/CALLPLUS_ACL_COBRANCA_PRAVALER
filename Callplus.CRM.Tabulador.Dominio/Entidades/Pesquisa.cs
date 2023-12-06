using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Pesquisa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public string Observacao { get; set; }
    }
}
