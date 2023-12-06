namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class AparelhoDaCampanha
    {
        public int Id { get; set; }

        public int IdCampanha { get; set; }

        public string Campanha { get; set; }

        public int IdAparelho { get; set; }

        public bool Ativo { get; set; }

    }
}
