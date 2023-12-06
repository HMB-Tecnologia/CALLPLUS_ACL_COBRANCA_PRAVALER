using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico
{
    public class LinhaDeCampoDinamico
    {
        public List<CampoDinamico> Campos { get; set; }
       

        public LinhaDeCampoDinamico()
        {
            Campos = new List<CampoDinamico>();
        }
       
    }
}
