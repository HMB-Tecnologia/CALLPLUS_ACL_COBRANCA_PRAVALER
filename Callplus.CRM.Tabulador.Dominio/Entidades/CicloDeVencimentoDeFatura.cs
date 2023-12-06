namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class CicloDeVencimentoDeFatura
    {
        public int? Id { get; set; }
        public int? Fechamento { get; set; }
        public int? Vencimento { get; set; }
        public int? IdCriador { get; set; }
        public string Criador { get; set; }
        public string DataCriacao { get; set; }
        public int? IdModificador { get; set; }
        public string Modificador { get; set; }
        public string DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}