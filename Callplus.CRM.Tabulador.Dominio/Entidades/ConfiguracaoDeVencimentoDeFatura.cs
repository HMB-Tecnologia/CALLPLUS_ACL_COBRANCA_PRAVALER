namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ConfiguracaoDeVencimentoDeFatura
    {
        public int? Id { get; set; }
        public int? DiaAtivacao { get; set; }
        public int? Ordem { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdCriador { get; set; }
        public int? DataCriacao { get; set; }
        public int? IdModificador { get; set; }
        public int? DataModificacao { get; set; }
        public bool? Ativo { get; set; }
    }
}