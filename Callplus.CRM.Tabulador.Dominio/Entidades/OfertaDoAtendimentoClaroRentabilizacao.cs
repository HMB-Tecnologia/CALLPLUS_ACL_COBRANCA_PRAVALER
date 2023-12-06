using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OfertaDoAtendimentoClaroRentabilizacao : OfertaDoAtendimento
    {
        public long? NumeroMigrado { get; set; }
        public int? DiaVencimento { get; set; }
        public int? IdFormaDePagamento { get; set; }
        public int? IdBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Nome { get; set; }
        public long? Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime? Nascimento { get; set; }
        public string NomeDaMae { get; set; }
        public long? TelefoneCelular { get; set; }
        public long? TelefoneResidencial { get; set; }
        public long? TelefoneRecado { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdProfissao { get; set; }
        public int? IdFaixaDeRenda { get; set; }
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
        public bool? FaturaDigital { get; set; }
        public string EmailFaturaDigital { get; set; }
        public int? IdAparelho { get; set; }
        public bool? DesejaAparelho { get; set; }
        public int? IdFormaDePagamentoAparelho { get; set; }
        public int? IdPassaporteOferta { get; set; }
    }
}
