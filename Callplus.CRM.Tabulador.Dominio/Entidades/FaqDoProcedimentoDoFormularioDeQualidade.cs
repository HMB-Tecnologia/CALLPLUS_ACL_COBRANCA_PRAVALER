using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FaqDoProcedimentoDoFormularioDeQualidade
    {
        public int id { get; set; }
        public int idProcedimento { get; set; }
        public int idTipoDeAvaliacaoDeAtendimento { get; set; }
        public string descricao { get; set; }
    }
}
