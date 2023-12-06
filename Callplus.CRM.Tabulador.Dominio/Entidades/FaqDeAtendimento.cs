using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FaqDeAtendimento
    {
        public long Id { get; set; }
        public int IdCampanha { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public bool Ativo { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}