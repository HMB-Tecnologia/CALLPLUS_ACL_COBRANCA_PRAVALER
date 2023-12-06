using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ModuloDoFormularioDeQualidade
    {
        public int Id { get; set; }
        public int IdFormularioDeQualidade { get; set; }
        public string Nome { get; set; }
        public int Valor { get; set; }
        public bool Ativo { get; set; }        
    }
}
