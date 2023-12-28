using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
   public class StatusDeAcordo
    {
        public int Id { get; set; }
        public int IdTipoDeStatusDeOferta { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Observacao { get; set; }
        public TipoStatusDeAcordo TipoStatus => (TipoStatusDeAcordo) IdTipoDeStatusDeOferta;
        public int IdCriador { get; set; }
        public int IdModificador { get; set; }
        public bool Selecionado { get; set; }
    }
}
