namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FaixaDeRecarga
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public bool Selecionado { get; set; }
        public int idCriador { get; set; }
        public int idModificador { get; set; }
    }
}
