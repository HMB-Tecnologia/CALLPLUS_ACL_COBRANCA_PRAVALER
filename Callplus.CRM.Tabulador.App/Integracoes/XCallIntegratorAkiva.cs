using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NLog;
using xCallIntegrator;
using xCallIntegrator.BLL;
using xCallAgent = xCallIntegrator.xCallAgent;
using xCallData = xCallIntegrator.xCallData;
using xDisposition = xCallIntegrator.xDisposition;

namespace Callplus.CRM.Tabulador.App.Integracoes
{
    public class XCallIntegratorAkiva
    {
        private XCallIntegratorAkiva _instancia;
        public readonly xCallAgent _agente;
        public event EventHandler SucessoAoRegistrarAgente;
        public event EventHandler FimDaChamada;
        public event EventHandler<DadosDaChamada> OnDisposition;
        public event EventHandler<int> MudancaNoStatusDoAgente;
        public event EventHandler<DadosDafalha> FalhaAoResponder;
        public event EventHandler<DadosDaChamada> NovaChamadaRecebida;
        public event EventHandler ErroAoRegistrarAgente;
        public event EventHandler<DadosDaChamada> SucessoAoConectar;
        public event EventHandler FalhaAoConectar;
        private ILogger _logger;

        private string _loginAgente;
        private string _senhaAgente;


        public XCallIntegratorAkiva(string loginAgente, string senha)
        {
            _loginAgente = loginAgente;
            _senhaAgente = senha;
            _logger = LogManager.GetCurrentClassLogger();
            string arquivoConfigAkiva = ConfigurationManager.AppSettings["ArquivoConfigAkiva"];
            _agente = _agente = new xCallAgent(_sConfigFile: arquivoConfigAkiva);
            ConfigurarEventos();
        }

        #region Metodos
        public bool VerificarSessaoAgente()
        {
            return _agente.VerifyAgentSession();
        }
        public int Registrar()
        {
            int retorno = _agente.Register(_loginAgente, _senhaAgente, "");
            return retorno;
        }

        public int Login()
        {
            int retorno = _agente.Login();
            return retorno;
        }

        public int LogOut()
        {
            int retorno = _agente.Logout();
            return retorno;
        }

        public int RetirarOperadorDaPausa()
        {
            int retorno = _agente.ExitPause();
            return retorno;
        }

        public int RetornarIdPausaDoAgente()
        {
            int retorno = _agente.GetPauseId();
            return retorno;
        }

        public string RetornarDescricaoDaPausaDoAgente()
        {
            string resultado = _agente.GetPauseDescription();
            return resultado;
        }

        public int PausarOperador(int id)
        {
            int retorno = _agente.SetPause(id);
            return retorno;
        }

        public int DiscarManual(string telefone)
        {
            int retorno = _agente.Dial(telefone);
            return retorno;
        }

        public int EncerrarChamada()
        {
            int retono = _agente.HungUp();
            return retono;
        }

        public List<PausaAkiva> RetornarPausasOperador()
        {
            var lista = new List<PausaAkiva>();
            var tipoPausas = _agente.GetPauseList();
            foreach (var item in tipoPausas)
            {
                PausaAkiva pausaAkiva = new PausaAkiva();
                pausaAkiva.Id = item.Id;
                pausaAkiva.Nome = item.Type;
                pausaAkiva.Tempo = item.Time;
                pausaAkiva.Restritiva = item.Restriction;
                pausaAkiva.Quantidade = item.Quantity;
                pausaAkiva.Acao = item.Act;
                pausaAkiva.Exibir = item.Show;
                lista.Add(pausaAkiva);
            }

            //var id = _agente.GetPauseId();

            return lista;
        }

        public void ConfigurarDiscagemManual(bool entrarEmPausa, bool habilitarDiscagemManual)
        {
            _agente.ManualDialing(entrarEmPausa, habilitarDiscagemManual);
        }

        public int InserirTabulacaoNoDiscador(int idDisposition, string dataAgendamento, string observacao, bool solicitouGravacao, string idExterno, int idAuditor, int rediscagem, int mailingPhoneRedial, int queueClassReasonId)
        {
            int retorno = _agente.SetDisposition(idDisposition, dataAgendamento, observacao, solicitouGravacao, idExterno, idAuditor, rediscagem, mailingPhoneRedial, queueClassReasonId);
            return retorno;
        }

        private void ConfigurarEventos()
        {
            _agente.OnSucessToRegister += Agente_OnSucessToRegister;
            _agente.OnDisposition += Agente_OnDisposition;
            _agente.OnFailToConnect += Agente_OnFailToConnect;
            _agente.OnSucessToConnect += Agente_OnSucessToConnect;
            _agente.OnSucessToRegister += Agente_OnSucessToRegister;
            _agente.OnFailToRegister += Agente_OnFailToRegister;
            _agente.OnHungUp += Agente_OnHungUp;
            _agente.OnIncomingCall += Agente_OnIncomingCall;
            _agente.OnStatusChange += Agente_OnStatusChange;
        }

        private bool VerificarSeExisteAgendamentoPrivado()
        {
            return _agente.VerifyPrivateSchedule();
        }

        private List<AgendamentoAkiva> RetornarListaDeAgendamentosPrivados()
        {
            var listaAkiva = _agente.GetPrivateScheduleList();
            List<AgendamentoAkiva> agendamentos = new List<AgendamentoAkiva>();

            agendamentos = listaAkiva.Select(x => new AgendamentoAkiva()
            {
                IdProspect = x.ID_Client,
                Telefone = x.PhoneNumber,
                NomeCliente = x.Name_Client,
                Hora = x.Hora,
                IdAgendamento = x.ID_Schedule,
                DataCadastro = x.Hora_Cadastro,
                IdUsuario = x.ID_User
            })?.ToList();

            return agendamentos;
        }


        #endregion Metodos

        #region Eventos
        private void Agente_OnSucessToRegister()
        {
            SucessoAoRegistrarAgente?.Invoke(this, null);
        }

        private void Agente_OnSucessToConnect(xCallData callData)
        {
            var dadosDaChamada = new DadosDaChamada();

            dadosDaChamada.IdProspect = _agente.xMailing.ClientId;
            dadosDaChamada.Telefone = callData.PhoneNumber;
            dadosDaChamada.IdMailing = callData.MailingId;
            dadosDaChamada.IdTicket = callData.TicketId;
            dadosDaChamada.IdFila = callData.QueueId;
            dadosDaChamada.NomeDaFila = callData.QueueName;


            //_logger.Debug($"UniqueId: {callData.UniqueId}, CallId: {callData.CallId}, ClientId: {_agente.xMailing.ClientId}, CallType: {callData.CallType}, QueueId: {callData.QueueId}, PhoneNumber: {callData.PhoneNumber}, TicketId: {callData.TicketId}, QueueId: {callData.QueueId}");

            SucessoAoConectar?.Invoke(null, dadosDaChamada);
        }

        private void Agente_OnFailureResponse(int nStatusCode, string sReasonPhrase)
        {
            var dadosDaFalha = new DadosDafalha();

            dadosDaFalha.StatusCode = nStatusCode;
            dadosDaFalha.ReasonPhrase = sReasonPhrase;
            FalhaAoResponder?.Invoke(null, dadosDaFalha);

        }

        private void Agente_OnStatusChange(int statusId)
        {

            MudancaNoStatusDoAgente?.Invoke(null, statusId);
        }

        private void Agente_OnIncomingCall(xCallData callData)
        {
            var dadosDaChamada = new DadosDaChamada();

            dadosDaChamada.IdProspect = _agente.xMailing.ClientId;
            dadosDaChamada.Telefone = callData.PhoneNumber;
            dadosDaChamada.IdMailing = callData.MailingId;
            dadosDaChamada.IdTicket = callData.TicketId;
            dadosDaChamada.IdFila = callData.QueueId;
            dadosDaChamada.NomeDaFila = callData.QueueName;

            NovaChamadaRecebida?.Invoke(null, dadosDaChamada);
        }

        private void Agente_OnHungUp()
        {
            FimDaChamada?.Invoke(null, null);
        }

        private void Agente_OnFailToRegister()
        {
            ErroAoRegistrarAgente?.Invoke(null, null);
        }

        private void Agente_OnFailToConnect()
        {
            FalhaAoConectar?.Invoke(null, null);
        }

        private void Agente_OnDisposition(xCallData callData)
        {
            var dadosDaChamada = new DadosDaChamada();
            dadosDaChamada.IdProspect = _agente.xMailing.ClientId;
            dadosDaChamada.Telefone = callData.PhoneNumber;
            dadosDaChamada.IdMailing = callData.MailingId;
            dadosDaChamada.IdTicket = callData.TicketId;
            dadosDaChamada.IdFila = callData.QueueId;
            dadosDaChamada.NomeDaFila = callData.QueueName;
            OnDisposition?.Invoke(null, dadosDaChamada);
        }

        #endregion Eventos

        public List<StatusAkiva> RetornarListaDeTabulacoes()
        {
            var lista = _agente.GetDispositionList("0");
            var listarRetorno = new List<StatusAkiva>();

            foreach (xDisposition disposition in lista)
            {
                listarRetorno.Add(new StatusAkiva()
                {
                    Id = disposition.Id,
                    Descricao = disposition.Description,
                    IdRetorno = disposition.ReturnID,
                    Automatico = disposition.Automatic,
                    Sucesso = disposition.Sucess

                });
            }

            return listarRetorno;
        }

        public StatusAgenteAkiva RetornarStatusAgente()
        {
            return (StatusAgenteAkiva)_agente.GetAgentStatus();
        }
    }

    #region Classes
    public class DadosDaChamada : EventArgs
    {
        public string IdProspect { get; set; }
        public string Telefone { get; set; }
        public string IdMailing { get; set; }
        public string IdTicket { get; set; }
        public string IdFila { get; set; }
        public string NomeDaFila { get; set; }
    }

    public class AgendamentoAkiva
    {
        public string IdProspect { get; set; }
        public string Telefone { get; set; }
        public string NomeCliente { get; set; }
        public string Hora { get; set; }
        public int IdUsuario { get; set; }
        public int IdAgendamento { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class PausaAkiva
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Restritiva { get; set; }
        public int Quantidade { get; set; }
        public string Tempo { get; set; }
        public bool Acao { get; set; }
        public bool Exibir { get; set; }
    }

    public class StatusAkiva
    {
        public int Id { get; set; }
        public bool Automatico { get; set; }
        public string Descricao { get; set; }
        public string IdRetorno { get; set; }
        public bool Sucesso { get; set; }

    }

    public class DadosDafalha : EventArgs
    {
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
    }

    public enum StatusAgenteAkiva
    {
        PausaLogin = 1,
        Disponivel = 2,
        Discando = 3,
        Ativo = 4,
        Atendimento = 5,
        PausaPosAtendimento = 6,
        PausaDefinidaPeloAgente = 7,
        LogOff = 8,
        EstourouTempoEmPausa = 9,
        PausaParaRediscagemDeMailing = 10,
        PausaParaDiscageManual = 11,
        PausaParaAtendimentoDeAgendamentoPrivado = 12,
        PausaDeFalhaDoSistema = 13,
        AtendimentoDeCHAT = 14,
        TrocaAutomaticaDeCampanha = 15,
        AtendimentoEletronico = 16,
        PausaPosAtendimentoAguardandoTabulacaoExterna = 17,
        PausaDiscagemPreviewAutomatica = 18
    }
    #endregion Classes

}
