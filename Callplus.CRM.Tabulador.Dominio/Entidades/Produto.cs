using System;
using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Produto
    {
        public bool Ativo { get; set; }
        public bool? AtivoBko { get; set; }
        public DateTime DataCriacao { get; set; }
        public long Id { get; set; }
        public int IdCampanha { get; set; }
        public int? IdScriptAceite { get; set; }
        public int? IdScriptOferta { get; set; }
        public int IdTipoDeProduto { get; set; }
        public string Nome { get; set; }
        public int IdModificador { get; set; }
        public int Idcriador { get; set; }
        public string Observacao { get; set; }
        public bool Selecionado { get; set; }
        public int Ordem { get; set; }
        public decimal Valor { get; set; }

        //TipoDeProduto TipoDeProduto
        //{
        //    get
        //    {
        //        return (Tipos.TipoDeProduto)IdTipoDeProduto;
        //    }
        //}
    }
}
