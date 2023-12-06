using Callplus.CRM.Tabulador.App.WsVoiceSupportIntegration;
using System;

namespace v1Tabulare_z13.ws
{
    public static class OlosWsVoiceSupport
    {
        private static readonly string BindingName = "WsVoiceSupportIntegrationSoap12";
        public static RetornoServico VoiceSupportPause(string loginAgente)
        {

            using (var cliente = new WsVoiceSupportIntegrationSoapClient(BindingName))
            {
                try
                {
                    cliente.VoiceSupportPause(loginAgente);
                    return new RetornoServico(sucesso: true);
                }
                catch (Exception erro)
                {
                    return new RetornoServico(sucesso: false, mensagem: erro.Message);
                }
            }

        }

        public static RetornoServico VoiceSupportResume(string loginAgente)
        {

            using (var cliente = new WsVoiceSupportIntegrationSoapClient(BindingName))
            {
                try
                {
                    cliente.VoiceSupportResume(loginAgente);
                    return new RetornoServico(sucesso: true);
                }
                catch (Exception erro)
                {
                    return new RetornoServico(sucesso: false, mensagem: erro.Message);
                }
            }
        }

        public static RetornoServico VoiceSupportStart(string loginAgente, string nomeArquivo)
        {
            using (var cliente = new WsVoiceSupportIntegrationSoapClient(BindingName))
            {
                try
                {
                    cliente.VoiceSupportStart(loginAgente, nomeArquivo);
                    return new RetornoServico(sucesso: true);
                }
                catch (Exception erro)
                {
                    return new RetornoServico(sucesso: false, mensagem: erro.Message);
                }
            }
        }

        public static RetornoServico VoiceSupportStop(string loginAgente)
        {
            using (var cliente = new WsVoiceSupportIntegrationSoapClient(BindingName))
            {
                try
                {
                    cliente.VoiceSupportStop(loginAgente);
                    return new RetornoServico(sucesso: true);
                }
                catch (Exception erro)
                {
                    return new RetornoServico(sucesso: false, mensagem: erro.Message);
                }
            }
        }


    }
}
