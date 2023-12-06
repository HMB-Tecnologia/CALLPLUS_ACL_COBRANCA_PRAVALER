using System;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class HistoricoAtendimentoDto
    {
        public long IdProspect { get; set; }
        public long IdAtendimento { get; set; }
        public long? Telefone { get; set; }
        public string NomeOperador { get; set; }
        public string ResultadoInteracao { get; set; }
        public DateTime? DataAtendimento { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public long? TelefoneAgendamento { get; set; }
        public string Observacao { get; set; }
        public string CampanhaAtivo { get; set; }
        public string CampanhaNome { get; set; }
        public int IndicacoesDoAtendimento { get; set; }
    }
}
