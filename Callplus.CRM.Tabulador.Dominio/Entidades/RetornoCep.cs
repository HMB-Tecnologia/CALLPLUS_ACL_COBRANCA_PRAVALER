using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class RetornoCep
    {
        public int Id { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string LogradouroAbreviado { get; set; }
        public string tipo { get; set; }
    }
}
