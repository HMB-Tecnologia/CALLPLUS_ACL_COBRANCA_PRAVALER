using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ProdutoPermitidoParaFaixaDeRecarga
    {

        public int Id { get; set; }
        public int IdFaixaDeRecarga { get; set; }
        public long IdProduto { get; set; }
        public string FaixaDeRecarga { get; set; }
        public bool Ativo { get; set; } 

    }
}
