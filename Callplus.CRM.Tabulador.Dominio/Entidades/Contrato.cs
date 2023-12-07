using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
	public class Contrato
    {
        public long IDContrato { get; set; }
        public string Descricao { get; set; }
        public string CodCliente { get; set; }
        public string Cda { get; set; }
        public List<Titulo> Titulos { get; set; }
        public string Campo01 { get; set; }
        public string Campo02 { get; set; }
        public string Campo03 { get; set; }
        public string Campo04 { get; set; }
        public string Campo05 { get; set; }
        public string Campo06 { get; set; }
        public string Campo07 { get; set; }
        public string Campo08 { get; set; }
        public string Campo09 { get; set; }
        public string Campo10 { get; set; }
    }
}
