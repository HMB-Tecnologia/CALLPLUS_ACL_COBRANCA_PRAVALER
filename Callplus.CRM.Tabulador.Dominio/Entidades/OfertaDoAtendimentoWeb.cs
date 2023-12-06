using System;
using System.ComponentModel.DataAnnotations;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class TabulacaoWeb
    {
        public string Nome { get; set; }
        public long? Cpf { get; set; }
        public long? Telefone { get; set; }
        public int? IdStatusDoAtendimento { get; set; }
        public string LoginAmil { get; set; }
        public string Observacao { get; set; }
    }

    public class OfertaDoAtendimentoWeb
    {
        public long Id { get; set; }
        public long IdAtendimento { get; set; }
        public int? IdStatusDoAtendimento { get; set; }
        public int? IdTipoDeProduto { get; set; }
        public int? IdScriptOferta { get; set; }
        public string NomeDaOferta { get; set; }
        public bool PreVenda { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool TitularidadeDiferente { get; set; }
        public int IdOperador { get; set; }
        public bool? codigo21 { get; set; }
        public bool? processado { get; set; }
        public bool? ofertaAparelho { get; set; }
        public bool? Venda { get; set; }
        public EtapaAtendimento EtapaAtendimento { get; set; }
        public string LoginAmil { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ObservacaoMarcacao { get; set; }

        public DadosPessoais DadosPessoais { get; set; }
        public Endereco Endereco { get; set; }
        public DadosOferta DadosOferta { get; set; }
        public DadosDePagamento DadosDePagamento { get; set; }
    }

    public class DadosPessoais
    {
        public string Nome { get; set; }
        public long? Cpf { get; set; }
        public string Rg { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? Nascimento { get; set; }
        public string NomeDaMae { get; set; }
        public long? TelefoneCelular { get; set; }
        public long? TelefoneResidencial { get; set; }
        public long? TelefoneRecado { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Selecione o Estado Civil")]
        public int? IdEstadoCivil { get; set; }
        public int? IdProfissao { get; set; }
        public int? IdFaixaDeRenda { get; set; }
    }

    public class DadosOferta
    {
        public long? IdProduto { get; set; }
        public long? NumeroMigrado { get; set; }
        public long? IdProduto2 { get; set; }
        public long? NumeroMigrado2 { get; set; }
    }

    public class DadosDePagamento
    {
        public int? IdFormaDePagamento { get; set; }
        public int? DiaVencimento { get; set; }
        public long? NumeroFaturaWhatsApp { get; set; }
        public int? IdBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string EmailFaturaDigital { get; set; }
        public bool? receberContrato { get; set; }
        public string ondeReceberContrato { get; set; }
        public bool? FaturaDigital { get; set; }
        public string url { get; set; }
        public string Observacao { get; set; }
    }

    public enum EtapaAtendimento
    {
        DadosPessoais = 1,
        Endereco = 2,
        DadosDaOferta = 3,
        DadosDePagamento = 4,
        FinalizarAtendimento = 5
    }

    public class ListarOfertaDoAtendimentoWeb
    {
        public long Id { get; set; }
        public long IdAtendimento { get; set; }
        public int? IdStatusDoAtendimento { get; set; }
        public int? IdTipoDeProduto { get; set; }
        public int? IdScriptOferta { get; set; }
        public string NomeDaOferta { get; set; }
        public bool PreVenda { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool TitularidadeDiferente { get; set; }
        public int IdOperador { get; set; }
        public bool? codigo21 { get; set; }
        public bool? processado { get; set; }
        public bool? ofertaAparelho { get; set; }
        public bool? Venda { get; set; }
        public EtapaAtendimento EtapaAtendimento { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ObservacaoMarcacao { get; set; }

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string PontoDeReferencia { get; set; }

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

        public long? IdProduto { get; set; }
        public long? NumeroMigrado { get; set; }
        public long? IdProduto2 { get; set; }
        public long? NumeroMigrado2 { get; set; }

        public int? IdFormaDePagamento { get; set; }
        public int? DiaVencimento { get; set; }
        public long? NumeroFaturaWhatsApp { get; set; }
        public int? IdBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string EmailFaturaDigital { get; set; }
        public bool? receberContrato { get; set; }
        public string ondeReceberContrato;
        public bool? FaturaDigital { get; set; }
        public string url { get; set; }
        public string Observacao { get; set; }
    }
}