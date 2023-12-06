using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class HistoricoDePausa
    {
        public long Id { get; set; }
        public int IdUsuario { get; set; }
        public int idConfiguracaoDaEscalaDePausa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? dataTermino { get; set; }
    }
}