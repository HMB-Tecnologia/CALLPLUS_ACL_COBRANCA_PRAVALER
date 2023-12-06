using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ProcedimentoDoItemDoFormularioDeQualidade
    {
        public int Id { get; set; }        
        public int IdItemDoModuloDoFormularioDeQualidade { get; set; }
        public int Numero { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }       
    }
}
