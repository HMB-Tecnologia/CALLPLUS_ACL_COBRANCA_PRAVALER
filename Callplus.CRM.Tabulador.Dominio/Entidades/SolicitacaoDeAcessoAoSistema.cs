using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class SolicitacaoDeAcessoAoSistema
    {
        public int id { get; set; }
        public int idOperador { get; set; }
        public DateTime dataCadastro { get; set; }
        public bool liberado { get; set; }
        public int idUsuarioLiberacao { get; set; }
        public DateTime dataLiberacao { get; set; }
        public string observacao { get; set; }
        public string operador { get; set; }
        public string supervisor { get; set; }
    }
}
