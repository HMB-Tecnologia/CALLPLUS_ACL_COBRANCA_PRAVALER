using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class RespostaDaPesquisaDoAtendimento
    {
        public long Id { get; set; }
        public long IdAtendimento { get; set; }
        public int IdPerguntaDaPesquisa { get; set; }
        public int IdOpcaoRespondida { get; set; }
    }
}