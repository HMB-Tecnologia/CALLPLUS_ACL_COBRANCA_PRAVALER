﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Callplus.CRM.Tabulador.App.WsVoiceSupportIntegration {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsVoiceSupportIntegration.WsVoiceSupportIntegrationSoap")]
    public interface WsVoiceSupportIntegrationSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportStart", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void VoiceSupportStart(string paramAgentLogin, string paramFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportStart", ReplyAction="*")]
        System.Threading.Tasks.Task VoiceSupportStartAsync(string paramAgentLogin, string paramFileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportPause", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void VoiceSupportPause(string paramAgentLogin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportPause", ReplyAction="*")]
        System.Threading.Tasks.Task VoiceSupportPauseAsync(string paramAgentLogin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportResume", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void VoiceSupportResume(string paramAgentLogin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportResume", ReplyAction="*")]
        System.Threading.Tasks.Task VoiceSupportResumeAsync(string paramAgentLogin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportStop", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void VoiceSupportStop(string paramAgentLogin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/VoiceSupportStop", ReplyAction="*")]
        System.Threading.Tasks.Task VoiceSupportStopAsync(string paramAgentLogin);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WsVoiceSupportIntegrationSoapChannel : Callplus.CRM.Tabulador.App.WsVoiceSupportIntegration.WsVoiceSupportIntegrationSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsVoiceSupportIntegrationSoapClient : System.ServiceModel.ClientBase<Callplus.CRM.Tabulador.App.WsVoiceSupportIntegration.WsVoiceSupportIntegrationSoap>, Callplus.CRM.Tabulador.App.WsVoiceSupportIntegration.WsVoiceSupportIntegrationSoap {
        
        public WsVoiceSupportIntegrationSoapClient() {
        }
        
        public WsVoiceSupportIntegrationSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsVoiceSupportIntegrationSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsVoiceSupportIntegrationSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsVoiceSupportIntegrationSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void VoiceSupportStart(string paramAgentLogin, string paramFileName) {
            base.Channel.VoiceSupportStart(paramAgentLogin, paramFileName);
        }
        
        public System.Threading.Tasks.Task VoiceSupportStartAsync(string paramAgentLogin, string paramFileName) {
            return base.Channel.VoiceSupportStartAsync(paramAgentLogin, paramFileName);
        }
        
        public void VoiceSupportPause(string paramAgentLogin) {
            base.Channel.VoiceSupportPause(paramAgentLogin);
        }
        
        public System.Threading.Tasks.Task VoiceSupportPauseAsync(string paramAgentLogin) {
            return base.Channel.VoiceSupportPauseAsync(paramAgentLogin);
        }
        
        public void VoiceSupportResume(string paramAgentLogin) {
            base.Channel.VoiceSupportResume(paramAgentLogin);
        }
        
        public System.Threading.Tasks.Task VoiceSupportResumeAsync(string paramAgentLogin) {
            return base.Channel.VoiceSupportResumeAsync(paramAgentLogin);
        }
        
        public void VoiceSupportStop(string paramAgentLogin) {
            base.Channel.VoiceSupportStop(paramAgentLogin);
        }
        
        public System.Threading.Tasks.Task VoiceSupportStopAsync(string paramAgentLogin) {
            return base.Channel.VoiceSupportStopAsync(paramAgentLogin);
        }
    }
}
