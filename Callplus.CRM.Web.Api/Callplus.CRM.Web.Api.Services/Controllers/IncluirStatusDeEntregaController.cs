using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Callplus.CRM.Web.Api.Services.Models;
using System.Configuration;

namespace Callplus.CRM.Web.Api.Services.Controllers
{
    public class IncluirStatusDeEntregaController : ApiController
    {
        public Entrega Post([FromBody] object data)
        {
            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;

            EntregaModel m = new EntregaModel();

            var list = JsonConvert.DeserializeObject<List<GravarEntrega>>(((Newtonsoft.Json.Linq.JContainer)data)["entregas"].ToString());

            if (headers.Contains("token"))
            {
                string tokenRecebido = headers.GetValues("token").First();

                string token = ConfigurationManager.AppSettings["token"];

                if (tokenRecebido != token)
                {
                    return m.RetornarErroToken(list);
                }
            }
            else
            {
                return m.RetornarErroToken(list);
            }

            return m.GravarStatus(list);
        }
    }
}