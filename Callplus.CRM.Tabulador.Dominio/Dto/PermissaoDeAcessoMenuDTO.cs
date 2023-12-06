namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class PermissaoDeAcessoMenuDTO
    {

        public int IdPerfilUsuario { get; set; }
        public string NomeDoControle { get; set; }
        public bool Visivel { get; set; }
        public bool Habilitado { get; set; }

    }
}
