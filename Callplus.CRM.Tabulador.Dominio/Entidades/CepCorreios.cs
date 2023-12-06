using Callplus.CRM.Tabulador.Dominio.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class CepCorreios
    {
        public string Id { get; set; }
        public string UF { get; set; } 
        public string Cidade { get; set; } 
        public string CidadeAbreviado { get; set; } 
        public string Bairro { get; set; } 
        public string BairroAbreviado { get; set; } 
        public string Logradouro { get; set; } 
        public string LogradouroAbreviado { get; set; } 
        public string Cep { get; set; }
        public string ComplementoLogradouro { get; set; } 
        public string Nome { get; set; }
        public string NomeAbreviado { get; set; }
    }
}
