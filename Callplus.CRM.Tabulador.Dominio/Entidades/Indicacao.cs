namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Indicacao
    {
        public long id { get; set; }
        public long idProspect { get; set; }
        public long idAtendimento { get; set; }
        public string descricao { get; set; }
        public int quantidadeDeIndicacoes { get; set; }
    }
}
