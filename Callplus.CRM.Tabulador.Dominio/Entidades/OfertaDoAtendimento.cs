﻿namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OfertaDoAtendimento
    {
        public long Id { get; set; }
        public long IdAtendimento { get; set; }        
        public int? IdStatusDoAcordo { get; set; }
        public long? IdProduto { get; set; }
        public int? IdTipoDeProduto { get; set; }
        public int? IdScriptOferta { get; set; }
        public string NomeDaOferta { get; set; }
        public bool PreVenda { get; set; }

    }
}