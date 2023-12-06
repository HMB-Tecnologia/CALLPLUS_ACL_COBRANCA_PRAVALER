using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class DadosPagamentoDoProspect
    {
        public long Id { get; set; }
        public long IdProspect { get; set; }
        public long IdFormaDePagamento { get; set; }
        public long IdBanco { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }

        public Banco Banco { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
        
    }
}
