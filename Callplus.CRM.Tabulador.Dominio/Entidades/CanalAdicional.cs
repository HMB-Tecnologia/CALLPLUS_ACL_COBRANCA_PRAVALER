namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class CanalAdicional
    {
        public int Id { get; set; }
        public int IdOperadora { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
    }
}
