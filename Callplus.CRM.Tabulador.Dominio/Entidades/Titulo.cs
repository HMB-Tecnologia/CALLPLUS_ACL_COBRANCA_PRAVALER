using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Titulo
    {
        public long IDTitulo { get; set; }
        public string NumeroDocumento { get; set; }
        public long? IDNegociacao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string AtribuicaoEspecial { get; set; }
        public string TipoDocumento { get; set; }
        public string FormaPagamento { get; set; }
        public decimal Montante { get; set; }
        public string Status { get; set; }               
    }
}
