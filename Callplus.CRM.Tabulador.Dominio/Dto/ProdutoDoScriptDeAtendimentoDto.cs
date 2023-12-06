using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class ProdutoDoScriptDeAtendimentoDto
    {
        public int Id { get; set; }
        public int IdScriptDeAtendimento { get; set; }
        public string Produtos { get; set; }
        public int IdCampanha { get; set; }
        public bool Apresentacao { get; set; }
        public bool Finalizacao { get; set; }
    }
}
