using System;
using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Permissao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdCriador { get; set; }
        public int IdCampanha { get; set; }
        public TipoPermissao TipoPermissao => (TipoPermissao) Id;
    }
}
