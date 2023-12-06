using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OfertaDoAtendimentoMPPortabilidadeBKO
    {
        public long? Id { get; set; }
        public int IdCampanha { get; set; }
        public long IdProspect { get; set; }
        public int? IdStatusAuditoria { get; set; }
        public int? IdStatusDaOferta { get; set; }
        public int? IdTipoDeProduto { get; set; }
        public int? IdOperador { get; set; }
        public int? IdSupervisor { get; set; }
        public int? IdBanco { get; set; }
        public string Cust_Id { get; set; }
		public string Nome { get; set; }
        public long? TelefoneCelular { get; set; }
        public long? TelefoneResidencial { get; set; }       
        public long? Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string PontoDeReferencia { get; set; }
        public string Observacao { get; set; }
        public DateTime DataRegistroOferta { get; set; }
        public DateTime DataCriacao { get; set; }
        public string TipoPessoa { get; set; }
        public string Email { get; set; }
      	public string DDDCel { get; set; }
		public string DddTel { get; set; }
		public long TelefoneDaGravacao { get; set; }
		public DateTime DataHoraAgendamento { get; set; }
		public long IdAtendimento { get; set; }
	}
}
