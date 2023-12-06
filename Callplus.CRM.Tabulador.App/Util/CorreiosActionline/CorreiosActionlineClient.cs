using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using NLog;


namespace Callplus.CRM.Tabulador.App.Util.CorreiosActionline
{
    class CorreiosActionlineClient
    {
        private ILogger _logger;

        public CorreiosActionlineClient()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public List<RetornoCepActionline> ConsultarCep(string cep, string logradouro = "", string uf = "")
        {
            var retorno = new List<RetornoCepActionline>();
            try
            {
                using (var cliente = new WebClient())
                {
                    var cepPesquisa = cep.Trim().ToString();
                    string uri = $"http://svcas2wb01:1010/api/CEP/?pid=C07A8B6EFD0F401BA6DB0DA71E78A508&pCEP={cepPesquisa}&pLOG_NO={logradouro}&pUFE_SG={uf}";

                    cliente.Encoding = Encoding.UTF8;
                    string retornoServico = cliente.DownloadString(uri);

                    _logger.Debug($"Retorno de Consulta de CEP: {retornoServico}");
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<RetornoCepActionline>));
                    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(retornoServico));
                    List<RetornoCepActionline> retornoDeCep = (List<RetornoCepActionline>)serializer.ReadObject(stream);

                    return retornoDeCep;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
                retorno.Add(new RetornoCepActionline
                {
                    _BAI_NO_ABREV = string.Empty,
                    _CEP = "-1",
                    _LOC_NO_ABREV = string.Empty,
                    _LOG_NO = string.Empty,
                    _LOG_NO_ABREV = string.Empty,
                    _UFE_SG = string.Empty
                });

                return retorno;
            }
        }
    }

    public class RetornoCepActionline
    {
        public string UF => _UFE_SG?.ToUpper();
        public string Logradouro => _LOG_NO?.ToUpper();
        public string LogradouroaAbreviado => _LOG_NO_ABREV?.ToUpper();
        public string Cep => _CEP?.ToUpper();
        public string Bairro => _BAI_NO_ABREV?.ToUpper();
        public string Localidade => _LOC_NO_ABREV?.ToUpper();


        public string _UFE_SG { get; set; }
        public string _LOC_NO_ABREV { get; set; } //
        public string _BAI_NO_ABREV { get; set; }
        public string _LOG_NO_ABREV { get; set; } //LOGRADOURO ABREVIADO
        public string _LOG_NO { get; set; } //LOGRADOURO
        public string _CEP { get; set; }
    }

}
