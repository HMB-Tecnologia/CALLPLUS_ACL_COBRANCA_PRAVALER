using System;


namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class VencimentoFatura
    {
        public int Id { get; set; }
        public int Dia { get; set; }
        public int Ordem { get; set; }
        public int Vencimento { get; set; }
        public int Fechamento { get; set;}
        public bool Ativo { get; set; }
        public int? IdCiclo { get; set; }
        public int IdModificador { get; set; }
        public int IdCriador { get; set; }
        public string Criador { get; set; }
        public string Modificador { get; set; }
    }
}
