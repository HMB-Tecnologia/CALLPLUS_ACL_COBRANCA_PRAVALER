using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class TelefoneDoProspect
    {
        public long Id { get; set; }
        public long Numero { get; set; }
        public long IdProspect { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
