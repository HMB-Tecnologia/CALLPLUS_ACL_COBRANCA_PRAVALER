using Callplus.CRM.Tabulador.Dominio.Tipos;
using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
   public class StatusDeOferta
    {
        public int Id { get; set; }
        public int IdTipoDeStatusDeOferta { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Observacao { get; set; }
        public int IdCriador { get; set; }
        public int IdModificador { get; set; }
        public bool Selecionado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }

        public TipoStatusDeOferta TipoStatus => (TipoStatusDeOferta) IdTipoDeStatusDeOferta;
    }
}
