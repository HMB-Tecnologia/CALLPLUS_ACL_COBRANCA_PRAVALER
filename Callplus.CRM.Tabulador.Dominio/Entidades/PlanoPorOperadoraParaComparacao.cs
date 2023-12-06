using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class PlanoPorOperadoraParaComparacao
    {
        public int id { get; set; }        
        public int idTipoDePlano { get; set; }
        public int idOperadora { get; set; }
        public string plano { get; set; }
        public string pacoteDadosMensal { get; set; }
        public string ofertaRedesSociais { get; set; }
        public string voz { get; set; }
        public string torpedos { get; set; }
        public decimal valor { get; set; }
        public bool ativo { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
    }
}
