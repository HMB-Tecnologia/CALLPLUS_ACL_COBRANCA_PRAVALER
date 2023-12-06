namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class IndicacaoDoAtendimento
    {
        public long id { get; set; }
        public long idAtendimento { get; set; }
        public int idGrauDeParentesco { get; set; }
        public string GrauDeParentesco { get; set; }
        public string nome { get; set; }
        public long telefone { get; set; }
    }
}
