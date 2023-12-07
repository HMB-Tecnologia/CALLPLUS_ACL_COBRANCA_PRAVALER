using System;
using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Negociacao
    {
        public int Id { get; set; }
        public string NumeroNegociacao { get; set; }       
        public int IdStatus { get; set; }
        public int IdTipoAcordo { get; set; }
        public double ValorPrincipal { get; set; }
        public double ValorDasParcelas { get; set; }
        public double Multa { get; set; }
        public double Juros { get; set; }
        public int IdPrazo { get; set; }
        public string Prazo { get; set; }
        public string Status { get; set; }
        public int QuantidadeDeParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int IdUsuario { get; set; }
        public List<Titulo> Titulos { get; set; }
        public string TitulosDoAcordo { get; set; }
        public List<Parcela> Parcelas { get; set; }
	}
}
