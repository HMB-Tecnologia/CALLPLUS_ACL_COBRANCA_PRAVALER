using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OfertaDoAtendimentoMPPortabilidade : OfertaDoAtendimento
    {
        public int? IdBanco { get; set; }
        public string Nome { get; set; }
        public long? TelefoneCelular { get; set; }
        public long? TelefoneResidencial { get; set; }
        public long? TelefoneRecado { get; set; }
        public long? Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string PontoDeReferencia { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCriacao { get; set; }     
        public bool? processado { get; set; }
        public long? TelefoneDaGravacao { get; set; }
        public string TipoPessoa { get; set; }
        public string Email { get; set; }
        public int DddTel { get; set; }
        public int DddCel { get; set; }
        public string Cust_Id { get; set; }
        public int? IdStatusAuditoria { get; set; }
    }
}
