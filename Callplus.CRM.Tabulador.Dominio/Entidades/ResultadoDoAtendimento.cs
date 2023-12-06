using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ResultadoDoAtendimento
    {
        public Atendimento Atendimento { get; set; }
        public StatusDeAtendimento StatusDoAtendimento { get; set; }

        //public long? IdAtendimento
        //{
        //    get => Atendimento?.Id;
        //}
        
        //public int? IdStatusDoAtendimento
        //{
        //    get => StatusDoAtendimento?.Id;
        //}

        public bool TabulacaoAutomatica { get; set; }
        public long Telefone { get; set; }
        public long? TelefoneAgendamento { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public string Observacao { get; set; }
        public int? IdUsuarioPermissao { get; set; }
    }
}
