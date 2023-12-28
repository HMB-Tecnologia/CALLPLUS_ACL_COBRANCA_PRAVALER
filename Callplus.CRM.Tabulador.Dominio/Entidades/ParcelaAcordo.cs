using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
	public class ParcelaAcordo
    {
        public int Id { get; set; }
        public int IdAcordo { get; set; }
        public int NumeroDaParcela { get; set; }
        public double ValorDaParcela { get; set; }
        public double ValorPrincipal { get; set; }
        public double Multa { get; set; }
        public double Juros { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdStatus { get; set; }
        public string Status { get; set; }
    }
}
