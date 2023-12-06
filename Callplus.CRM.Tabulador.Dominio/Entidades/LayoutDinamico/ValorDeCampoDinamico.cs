namespace Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico
{
    public class ValorDeCampoDinamico
    {
        public ValorDeCampoDinamico(string idDoCampo, string valor)
        {
            IdCampo = idDoCampo;
            Valor = valor;
        }

        protected ValorDeCampoDinamico()
        {
            
        }
        public string IdCampo { get; }
        public string Valor { get; }
    }
}
