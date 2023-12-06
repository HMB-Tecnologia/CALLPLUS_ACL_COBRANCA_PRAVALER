using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class ProdutoDoChecklistDto
    {
        public int Id { get; set; }
        public int IdChecklist { get; set; }
        public string Produtos { get; set; }
        public int IdCampanha { get; set; }
    }
}
