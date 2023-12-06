using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class PermissaoDoUsuario
    {
        public int Id { get; set; }
        public int IdPermissao { get; set; }
        public int IdCampanha { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdCriador { get; set; }
        public Permissao Permissao { get; set; }
    }
}
