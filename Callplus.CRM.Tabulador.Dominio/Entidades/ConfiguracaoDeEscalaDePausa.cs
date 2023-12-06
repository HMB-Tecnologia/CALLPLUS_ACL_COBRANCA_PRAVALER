using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ConfiguracaoDaEscalaDePausa
    {
        public int? Id { get; set; }
        public int? IdEscala { get; set; }
        public string Pausa { get; set; }
        public DateTime? Horario { get; set; }
        public int? TempoEmSegundos { get; set; }
        public int? Limite { get; set; }
        public bool? Ativo { get; set; }
    }
}