using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class StatusDeAuditoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool AprovaOferta  { get; set; }
        public bool HabilitaTrocaDeStatus { get; set; }
        public bool PermitidoHumano { get; set; }
        public bool Selecionado { get; set; }
    }
}
