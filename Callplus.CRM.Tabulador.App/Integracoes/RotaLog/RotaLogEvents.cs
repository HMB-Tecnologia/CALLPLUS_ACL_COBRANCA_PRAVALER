using Callplus.CRM.Tabulador.App.WSRotalogService;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System.Data;

namespace Callplus.CRM.Tabulador.App.Integracoes.RotaLog
{
    public class RotaLogEvents
    {
        public RotaLogEvents()
        {            
        }

        public void SolicitarColeta(long idOferta)
        {
            string result = "";


        }

        public string ReservaChips()
        {
            string result = "";

            using (WebServiceRota agentWS = new WebServiceRota())
            {
                DataSet agentId;
                agentId = agentWS.ReservaChips("USU00871", "102030", "11", "30431083", 1);
            }

            return result;
        }

        public string IncluiColeta()
        {
            string result = "";

            using (WebServiceRota agentWS = new WebServiceRota())
            {
                DataSet agentId;
                //agentId = agentWS.IncluiColeta("USU00871", "102030", );
            }

            return result;
        }
    }
}
