using System;
using System.Collections.Generic;

namespace Callplus.CRM.Web.Api.Services.Models
{
    public class Entrega
    {
        public List<RetornoEntrega> entregas { get; set; }
    }

        public class GravarEntrega
    {
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

    public class RetornoEntrega
    {
        public string cnpjCpfDes { get; set; }
        public string numeroPedido { get; set; }
        public string numeroRemessa { get; set; }
        public string codMensagem { get; set; }
        public string mensagem { get; set; }
    }
}