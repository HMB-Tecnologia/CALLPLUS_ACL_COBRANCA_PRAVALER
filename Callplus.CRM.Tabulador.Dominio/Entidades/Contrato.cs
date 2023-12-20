namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
	public class Contrato
    {
        public long Id { get; set; }
        //public string Descricao { get; set; }
        //public string CodCliente { get; set; }
        //public string Cda { get; set; }
        //public List<Titulo> Titulos { get; set; }

		public string Cpf { get; set; }
		public string CodContrato { get; set; }
		public string Valor { get; set; }
		public string Vencimento { get; set; }
		public string Campo01 { get; set; }
        public string Campo02 { get; set; }
        public string Campo03 { get; set; }
        public string Campo04 { get; set; }
        public string Campo05 { get; set; }
        public string Campo06 { get; set; }
    }
}
