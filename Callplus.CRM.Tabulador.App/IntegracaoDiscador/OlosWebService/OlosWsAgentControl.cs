using System.Timers;
using v1Tabulare_z13.IntegracaoDiscador.OlosWebService;
using Callplus.CRM.Tabulador.App.WSAgentEvent;
using Callplus.CRM.Tabulador.App.WSAgentCommand;
using NLog;
using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.App.WSAgentEvent;
using Callplus.CRM.Tabulador.App.WSAgentCommand;

namespace v1Tabulare_z13.IntegracaoDiscador
{

    public class OlosWsAgentControl
    {
        private readonly ILogger _logger;
        private readonly string WSAgentCommandBinding = "WSAgentCommandSoap12";
        private readonly string WSAgentEventBinding = "WSAgentEventSoap12";
        public delegate void ScrenPopEventHandler(ScreenPopData screenPopData);
        public event ScrenPopEventHandler ScreenPopEvent;
        public delegate void PassCodeEventHandler(int passCode);
        public event PassCodeEventHandler PassCodeEvent;
        public delegate void LoginCCMEventHandler();
        public event LoginCCMEventHandler LoginCCM;
        public delegate void LoginCampaignEventHandler(int campaignId, string campaignName);
        public event LoginCampaignEventHandler LoginCampaign;
        public delegate void LoginCCMFailEventHandler(string reason);
        public event LoginCCMFailEventHandler LoginCCMFail;
        public delegate void LoginCampaignFailEventHendler(string cause);
        public event LoginCampaignFailEventHendler LoginCampaignFail;
        public delegate void LogoutCCMEventHendler();
        public event LogoutCCMEventHendler LogoutCCM;
        public delegate void LogoutCCMFailEventHendler(string cause);
        public event LogoutCCMFailEventHendler LogoutCCMFail;
        public delegate void LogoutCampaignEventHendler(string reason);
        public event LogoutCCMFailEventHendler LogoutCampaign;
        public delegate void LogoutCampaignFailEventHandler(string cause);
        public event LogoutCampaignFailEventHandler LogoutCampaignFail;
        public delegate void PauseEventHandler(int reasonId);
        public event PauseEventHandler PauseEvent;

        public delegate void ManualCallStateEventHandler(ObjManualCallState ManualCallState);
        public event ManualCallStateEventHandler ManualCallStateEvent;

        public delegate void WrapEventHandler();
        public event WrapEventHandler WrapEvent;
        public delegate void WrapWithEndingEventHandler();
        public event WrapWithEndingEventHandler WrapWithEndingEvent;
        public delegate void WrapWithPauseEventHandler();
        public event WrapWithPauseEventHandler WrapWithPauseEvent;
        public delegate void WrapWithManualCallEventHandler();
        public event WrapWithManualCallEventHandler WrapWithManualCallEvent;
        public delegate void WrapWithPrivateCallbackEventHandler();
        public event WrapWithPrivateCallbackEventHandler WrapWithPrivateCallbackEvent;
        public delegate void ManualCallRequestFailEventHandler();
        public event ManualCallRequestFailEventHandler ManualCallRequestFailEvent;
        public delegate void HangUpEventHandler();
        public event HangUpEventHandler HangUpEvent;
        public delegate void AgentStatusEventHandler(int reasonId, ObjAgentChangeStatus status);
        public event AgentStatusEventHandler AgentStatusEvent;
        public delegate void AgentReasonRequestEventHandler(int reasonId, int agentId);

        private int _agentId;
        private EnumAgentStatusId _ultimoStatusDoCliente;
        Timer _timerOlosEvents;

        public OlosWsAgentControl()
        {
            _timerOlosEvents = new Timer();
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void StartAgentMonitoring(int agentId)
        {
            _agentId = agentId;
            _timerOlosEvents.Elapsed += new ElapsedEventHandler(OnTimerOlosEvent);
            _timerOlosEvents.Interval = 1000;
            _timerOlosEvents.Enabled = true;

        }

        private void OnTimerOlosEvent(object sender, ElapsedEventArgs e)
        {
            MonitorarEventosOlos(_agentId);
        }

        public int Login(string login, string password, bool forceLogout)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                int agentId = 0;
                agentId = agentWS.AgentAuthentication(login, password, forceLogout);
                return agentId;
            }

        }

        public void Logout(int agentId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.AgentLogout(agentId);
            }
        }

        public void Pause(int agentId, int reasonId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.AgentReasonRequest(agentId, reasonId);

            }

        }

        public void AlterarStatusParaLigacaoManual(int agentId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.ManualCallStateRequest(agentId);

            }
        }

        public void ExitPause(int agentId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.AgentIdleRequest(agentId);
            }
        }
        public List<ObjReason> ListarPausas(int agentId)
        {
            List<ObjReason> listaPausa = new List<ObjReason>();
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                ObjReason[] reasons = agentWS.ListReasons(agentId);

                foreach (var pausa in reasons)
                {
                    listaPausa.Add(pausa);
                }

            }
            return listaPausa;
        }

        public void HangupAndDispositionCall(string dispositionCode, int callId, int agentId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.DispositionCallByCode(agentId, dispositionCode, callId);
            }
        }

        public void DispositionCallBack(int agentId, int dispositionId, int CallId, string year, string month, string day, string hour, string minute, string phonenumber, bool specificagent)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.DispositionCallBack(agentId, dispositionId, CallId, year, month, day, hour, minute, phonenumber, specificagent);
            }
        }

        public void DispositionList0(int campaignId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                ObjDisposition[] dispositionList = agentWS.ListDispositions(campaignId);
                foreach (ObjDisposition disposition in dispositionList)
                {
                    switch (disposition.dispositionType)
                    {
                        case DispositionType.Success:
                        case DispositionType.DoNotCall:
                        case DispositionType.Failure:
                        case DispositionType.Callback:
                        case DispositionType.CallbackTargetContact:
                            break;
                    }
                }
            }
        }

        public void DispositionCall(int agentId, int dispId, int callId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.DispositionCall(agentId, dispId, callId);
            }
        }

        public void HangupRequest(int agentId, int callId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.HangupRequest(agentId, callId);
            }
        }

        public void SendManualCallRequest(int agentId, string DDD, string phoneNumber, int campaignId)
        {
            using (WSAgentCommandSoapClient agentWS = new WSAgentCommandSoapClient(WSAgentCommandBinding))
            {
                agentWS.SendManualCallRequest(agentId, DDD, phoneNumber, campaignId);
            }
        }


        private void MonitorarEventosOlos(int agentId)
        {
            using (WSAgentEventSoapClient eventWS = new WSAgentEventSoapClient(WSAgentEventBinding))
            {

                ObjEvent objEvent = eventWS.GetNextEvent(agentId);

                if (objEvent.AgentEventType != EnumAgentEventType.Nothing)
                    _logger.Debug($"Evento OLOS: {objEvent.AgentEventType.ToString()}");


                //_logger.Debug($"Generic String: {objEvent.GenericString ?? ""}, Generic Int: {objEvent.GenericInt.ToString() ?? ""}");


                switch (objEvent.AgentEventType)
                {
                    case EnumAgentEventType.PassCode:
                        {
                            var passCode = objEvent.GenericInt;
                            _logger.Debug($"Passcode Recebido: {passCode}");
                            DispararEventoPassCode(passCode);

                        }
                        break;
                    case EnumAgentEventType.ChangeManualCallState:
                        {
                            var statusLigacao = objEvent.EventObjectManualCallState;
                            int callId = statusLigacao.CallId;
                            DispararEventoTrocaDeStatusLigacaoManual(callId, statusLigacao);

                        }
                        break;
                    case EnumAgentEventType.ScreenPop:
                        {
                            var screenPop = objEvent.EventObjectScreenPop;
                            DispararEventoScreenPop(screenPop);
                        }
                        break;
                    case EnumAgentEventType.LoginCCM:
                        {
                            var loginCCM = objEvent.EventObjectLoginCCM;
                            DispararEventoLoginCCM();
                        }
                        break;
                    case EnumAgentEventType.LoginCampaign:
                        {
                            var loginCampaign = objEvent.EventObjectLoginCampaign;
                            int campaignId = loginCampaign.CampaignId;
                            string campaignName = loginCampaign.CampaignName;
                            DispararEventoCampaign(campaignId, campaignName);
                        }
                        break;
                    case EnumAgentEventType.LoginCCMFail:
                        {
                            string reason = objEvent.GenericString;
                            DispararEventoLoginCCMFail(reason);

                        }
                        break;
                    case EnumAgentEventType.LoginCampaignFail:
                        {
                            string cause = objEvent.GenericString;
                            DispararEventoLoginCampaignFail(cause);

                        }
                        break;
                    case EnumAgentEventType.LogoutCCM:
                        {
                            var logoutCCM = objEvent.EventObjectLogout;
                            DispararEventoLogoutCCM();
                        }
                        break;
                    case EnumAgentEventType.LogoutCCMFail:
                        {
                            string cause = objEvent.GenericString;
                            DispararEventoLogoutCCMFail(cause);
                        }
                        break;
                    case EnumAgentEventType.LogoutCampaign:
                        {
                            string reason = objEvent.GenericString;
                            DispararEventoLogoutCampaign(reason);
                        }
                        break;
                    case EnumAgentEventType.LogoutCampaignFail:
                        {
                            string cause = objEvent.GenericString;
                            DispararEventoLogoutCampaignFail(cause);
                        }
                        break;
                    case EnumAgentEventType.ChangeStatus:
                        {
                            var status = objEvent.EventObjectAgentChangeStatus;
                            DispararEventosDeTrocaDeStatus(objEvent, status);
                            DispararEventosDeTrocaDeStatusHangUp(objEvent, status);
                        }
                        break;
                    case EnumAgentEventType.ChangeStatusFail:
                        {

                            _logger.Debug($"{objEvent.AgentEventType.ToString()}: GenericString: {objEvent.GenericString ?? ""}");
                            /// DispararEventosDeTrocaDeStatus(objEvent, status);
                        }
                        break;
                    case EnumAgentEventType.ManualCallRequestFail:
                        {
                            var status = objEvent.EventObjectManualCallState;
                            DispararEventosManualCallRequestFail();
                            //_logger.Debug($"{objEvent.AgentEventType.ToString()}: GenericString: {objEvent.GenericString ?? ""}");
                            /// DispararEventosDeTrocaDeStatus(objEvent, status);
                        }
                        break;
                    case EnumAgentEventType.Nothing:
                        {
                            // _logger.Debug($"Nothing Event.");
                        }
                        break;

                }

            }
        }

        private void DispararEventoTrocaDeStatusLigacaoManual(int callId, ObjManualCallState objEvent)
        {
            _logger.Info($"Novo Status Evento Ligação Manual : Tipo: { Enum.GetName(typeof(EnumAgentStatusId), objEvent.CallState)}");
            ManualCallStateEvent?.Invoke(objEvent);            
        }

        private void DispararEventosDeTrocaDeStatusHangUp(ObjEvent objEvent, ObjAgentChangeStatus status)
        {
            if (objEvent == null) return;
            if (status == null) return;
            _logger.Info($"Novo Status Evento HangUp: Tipo: { Enum.GetName(typeof(EnumAgentStatusId), status.AgentStatusId)}");
            var reasonId = status.ReasonId;
            switch (status.AgentStatusId)
            {               
                case EnumAgentStatusId.Wrap:                  
                case EnumAgentStatusId.WrapWithEnding:
                case EnumAgentStatusId.WrapWithPause:
                case EnumAgentStatusId.WrapWithManualCall:                 
                case EnumAgentStatusId.WrapWithPrivateCallback:
                    {
                        HangUpEvent?.Invoke();
                    }
                    break;
                default:
                    break;
            }
        }

        private void DispararEventosDeTrocaDeStatus(ObjEvent objEvent, ObjAgentChangeStatus status)
        {
            if (objEvent == null) return;
            if (status == null) return;


            _logger.Info($"Novo Status: Tipo: { Enum.GetName(typeof(EnumAgentStatusId), status.AgentStatusId)}");
            _ultimoStatusDoCliente = status.AgentStatusId;
            var reasonId = status.ReasonId;
            AgentStatusEvent?.Invoke(reasonId, status);
            
            switch (status.AgentStatusId)
            {
                case EnumAgentStatusId.Pause:
                    {
                        PauseEvent?.Invoke(reasonId);
                    }
                    break;
                case EnumAgentStatusId.Wrap:
                    {
                        WrapEvent?.Invoke();                        
                    }
                    break;
                case EnumAgentStatusId.WrapWithEnding:
                    {
                        WrapWithEndingEvent?.Invoke();
                    }
                    break;
                case EnumAgentStatusId.WrapWithPause:
                    {
                        WrapWithPauseEvent?.Invoke();
                    }
                    break;
                case EnumAgentStatusId.WrapWithManualCall:
                    {
                        WrapWithManualCallEvent?.Invoke();
                    }
                    break;
                case EnumAgentStatusId.WrapWithPrivateCallback:
                    {
                        WrapWithPrivateCallbackEvent?.Invoke();
                    }
                    break;
            }
        }

        public bool PodeFinalizarContato()
        {
            if (_ultimoStatusDoCliente == null) return false;

            switch (_ultimoStatusDoCliente)
            {
                case EnumAgentStatusId.Wrap:
                case EnumAgentStatusId.WrapWithEnding:
                case EnumAgentStatusId.WrapWithManualCall:
                case EnumAgentStatusId.WrapWithPause:
                case EnumAgentStatusId.WrapWithPrivateCallback:
                    {
                        return true;
                    }  
                default:
                    {
                        return false;
                    }                             
            }

        }

        private void DispararEventosManualCallRequestFail()
        {
            ManualCallRequestFailEvent?.Invoke();
        }

        private void DispararEventoLogoutCampaignFail(string cause)
        {
            LogoutCampaignFail(cause);
        }

        private void DispararEventoLogoutCampaign(string reason)
        {
            LogoutCampaign?.Invoke(reason);
        }

        private void DispararEventoLogoutCCMFail(string cause)
        {
            LogoutCCMFail?.Invoke(cause);
        }

        private void DispararEventoLogoutCCM()
        {
            LogoutCCM?.Invoke();
        }

        private void DispararEventoLoginCampaignFail(string cause)
        {
            LoginCampaignFail?.Invoke(cause);
        }

        private void DispararEventoLoginCCMFail(string reason)
        {
            LoginCCMFail?.Invoke(reason);
        }

        private void DispararEventoCampaign(int campaignId, string campaignName)
        {
            LoginCampaign?.Invoke(campaignId, campaignName);
        }

        private void DispararEventoLoginCCM()
        {
            LoginCCM?.Invoke();

        }

        private void DispararEventoPassCode(int passCode)
        {
            PassCodeEvent?.Invoke(passCode);

        }

        private void DispararEventoScreenPop(ObjScreenPop screenPop)
        {
            ScreenPopData screenPopData = new ScreenPopData
            {
                CampaignData = screenPop.CampaignData,
                CallId = screenPop.CallId,
                CampaignCode = screenPop.CampaignCode,
                CampaignId = screenPop.CampaignId,
                CustomerId = screenPop.CustomerId,
                PhoneNumber = screenPop.PhoneNumber,
                TableName = screenPop.TableName
            };

            ScreenPopEvent?.Invoke(screenPopData);
        }        

        internal void FinishAgentMonitoring()
        {
            _timerOlosEvents.Stop();
            _timerOlosEvents.Enabled = false;

        }
    }


}
