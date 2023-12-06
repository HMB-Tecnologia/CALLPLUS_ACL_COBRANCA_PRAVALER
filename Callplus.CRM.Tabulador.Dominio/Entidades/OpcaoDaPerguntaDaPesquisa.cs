using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class OpcaoDaPerguntaDaPesquisa
    {
        public int Id { get; set; }
        public int IdPerguntaDaPesquisa { get; set; }
        public string Opcao { get; set; }
        public bool Ativo { get; set; }
    }
}
