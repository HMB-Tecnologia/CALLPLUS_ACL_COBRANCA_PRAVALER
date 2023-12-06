using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class StatusDeEntregaBluechip
    {
        public long Id { get; set; }
        public string cnpjCpfDes { get; set; }
        public string numeroPedido { get; set; }
        public string numeroRemessa { get; set; }
        public string dataOcorrencia { get; set; }
        public string nomeMotorista { get; set; }
        public string placaVeiculo { get; set; }
        public string codOcorrencia { get; set; }
        public string descOcorrencia { get; set; }
        public string iccid { get; set; }

    }
}
