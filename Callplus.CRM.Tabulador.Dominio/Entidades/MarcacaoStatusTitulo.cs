using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class MarcacaoStatusTitulo
    {
        public long IDAtendimento;
        public long IDtitulo { get; set; }
        public long IDStatusTitulo { get; set; }
        public long IDHistorico { get; set; }
        public long IDUsuario { get; set; }
        public string NumeroNegociacao { get; set; }
        public string NumeroDocumento { get; set; }
        public string StatusNegociacao { get; set; }
        public string StatusTitulo { get; set; }
        public bool Baixado { get; set; }
        public decimal ValorBoleto { get; set; }
        public decimal ValorAtualizado { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal ValorParcelas { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataNegociacaoFutura { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataVencimentoAtualizado { get; set; }
        public string Observacao { get; set; }
    }
}
