﻿using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OfertaDoAtendimentoClaroRentabilizacaoBKO
    {
        public long? id { get; set; }
        public long idOfertaDoAtendimento_Rentabilizacao { get; set; }
        public int idCampanha { get; set; }
        public long idProspect { get; set; }
        public int? idStatusAuditoria { get; set; }
        public int? idStatusDaOferta { get; set; }
        public int? idTipoDeProduto { get; set; }
        public int? idProduto { get; set; }
        public int? idOperador { get; set; }
        public int? idSupervisor { get; set; }
        public long? numeroMigrado { get; set; }
        public bool? passaporteAmerica { get; set; }
        public bool? passaporteEuropa { get; set; }
        public bool? desejaAparelho { get; set; }
        public int? idAparelho { get; set; }
        public int? parcelas { get; set; }
        public bool? faturaDigital { get; set; }
        public string emailFaturaDigital { get; set; }
        public int? diaVencimento { get; set; }
        public int? idFormaDePagamento { get; set; }
        public int? idBanco { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
        public string desconto { get; set; }
        public string nome { get; set; }
        public long? cpf { get; set; }
        public string rg { get; set; }
        public DateTime? nascimento { get; set; }
        public string nomeDaMae { get; set; }
        public long? telefoneCelular { get; set; }
        public long? telefoneResidencial { get; set; }
        public long? telefoneRecado { get; set; }
        public int? idEstadoCivil { get; set; }
        public int? idProfissao { get; set; }
        public int? idFaixaDeRenda { get; set; }
        public long? cep { get; set; }
        public int? IdFormaDePagamentoDeAparelho { get; set; }
        public int? IdPassaporteOferta { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string pontoDeReferencia { get; set; }
        public string observacao { get; set; }
        public DateTime dataRegistroOferta { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}
