using System;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class PermissoesDoUsuarioDTO
    {
        public int IdPermissaoUsuario { get; set; }
        public int IdCampanha { get; set; }
        public DateTime DataCriacaoPermissaoUsuario { get; set; }
        public int IdPermissao { get; set; }
        public string NomePermissao { get; set; }
        public DateTime DataCriacaoPermissao { get; set; }
        public int IdUsuario { get; set; }
        public int IdCriadorPermissaoUsuario { get; set; }
    }
}
