namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FormaDePagamentoDeAparelho
    {
        public int Id { get; set; }
        public int IdAparelho { get; set; }
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public float Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
