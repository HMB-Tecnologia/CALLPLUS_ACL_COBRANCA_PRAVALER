namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Banco
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int? CaracteresAgencia { get; set; }
        public int? CaracteresConta { get; set; }
    }
}
