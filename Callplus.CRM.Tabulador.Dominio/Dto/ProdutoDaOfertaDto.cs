using System;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class ProdutoDaOfertaDto
    {
        public int idTipo { get; set; }
        public int idProduto { get; set; }        
        public string Tipo { get; set; }
        public string Produto { get; set; }
        public decimal Valor { get; set; }
    }
}
