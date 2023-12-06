using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Discador
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string spExportacaoMailing { get; set; }
        public string spEnvioAutomaticoMailing { get; set; }
        public TipoDiscador TipoDiscador
        {
            get { return (TipoDiscador) Id; }
        }
    }
}
