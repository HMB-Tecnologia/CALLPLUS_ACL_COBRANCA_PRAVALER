using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ItemDoModuloDoFormularioDeQualidade
    {
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Peso { get; set; }
        public bool Ativo { get; set; }
        public int IdModuloDoFormularioDeQualidade { get; set; }
        public string Modulo { get; set; }
    }
}
