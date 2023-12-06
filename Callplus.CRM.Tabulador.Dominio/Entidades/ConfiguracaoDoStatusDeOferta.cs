namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ConfiguracaoDoStatusDeOferta
    {
        public int Id { get; set; }
        public int IdStatusOferta { get; set; }
        public int? IdStatusDeAtendimentoPadrao { get; set; }  
    }
}
