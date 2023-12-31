﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OfertaDoAtendimentoNETPTVBKO
    {
        public long? id { get; set; }
        public long idOfertaDoAtendimento_NetPtv { get; set; }
        public int idCampanha { get; set; }
        public long idProspect { get; set; }
        public int? idStatusAuditoria { get; set; }
        public int? idStatusDaOferta { get; set; }
        public int? idTipoDeProduto { get; set; }
        public int? idProduto { get; set; }
        public int? idOperador { get; set; }
        public int? idSupervisor { get; set; }
        public bool? faturaDigital { get; set; }
        public string emailFaturaDigital { get; set; }
        public int? diaVencimento { get; set; }
        public int? idFormaDePagamento { get; set; }
        public int? idBanco { get; set; }
        public string agencia { get; set; }
        public string conta { get; set; }
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
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string pontoDeReferencia { get; set; }
        public string observacao { get; set; }
        public string pontoAdicional { get; set; }
        public DateTime? dataInstalacaoPreferida { get; set; }
        public DateTime? dataInstalacaoSecundaria { get; set; }
        public string periodo { get; set; }
        public string canalAdicional { get; set; }
        public string grupo { get; set; }
        public DateTime dataRegistroOferta { get; set; }
        public DateTime dataCriacao { get; set; }
        public string PlanoTelefoneFixo { get; set; }
    }
}
