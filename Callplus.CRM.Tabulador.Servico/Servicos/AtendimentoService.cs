using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class AtendimentoService
    {
        private readonly AtendimentoDao _atendimentoDao;

        public AtendimentoService()
        {
            _atendimentoDao = new AtendimentoDao();
        }

        public long FinalizarAtendimento(ResultadoDoAtendimento resultado)
        {
           return _atendimentoDao.FinalizarAtendimento(resultado);
        }

        public Atendimento IniciarAtendimento(int idOperador, long idProspect, int idSupervisor, int? idDiscador,string numeroChamadorDiscador, 
            string ip, string host, OrigemDeAtendimento origem)
        {
            Atendimento atendimento = _atendimentoDao.IniciarAtendimento(idOperador, idProspect, idSupervisor, idDiscador, numeroChamadorDiscador, ip, host, origem);

            if (atendimento == null)
                throw new SystemException("Não foi possível iniciar um novo atendimento.");

            atendimento.CamposDoAtendimento = _atendimentoDao.RetornarValoresDosCamposDoAtendimento(atendimento.Id);
            return atendimento;
        }

        public DataTable RetornarRankingAtendimento(int idCampanha)
        {
            return _atendimentoDao.RetornarRankingAtendimento(idCampanha);
        }

        public IEnumerable<HistoricoAtendimentoDto> RetornarHistoricoDeAtendimento(long idProspect, int idUsuario)
        {
            return _atendimentoDao.RetornarHistoricoDeAtendimento(idProspect, idUsuario);
        }
        
        public IEnumerable<ConfiguracaoVencimentoFaturaDto> RetornarDatasDeVencimentoDeFaturaDisponiveis()
        {
            return _atendimentoDao.RetornarDatasDeVencimentoDeFaturaDisponiveis();
        }

        public IEnumerable<string> VerificarSePodeRealizarAgendamento(long idAtendimento, long telefone, DateTime? dataAgendamento, int idTipodeAgendamento)
        {
            return _atendimentoDao.VerificarSePodeRealizarAgendamento(idAtendimento, telefone, dataAgendamento, idTipodeAgendamento);
        }

        public IEnumerable<string> VerificarSePodeRealizarVenda(int idCampanha, long telefone, string codigoMailing)
        {
            return _atendimentoDao.VerificarSePodeRealizarVenda(idCampanha, telefone, codigoMailing);
        }

        public DadosDoRanking RetornarDadosDosAtendimentos(int idOperador, int idCampanha)
        {
            return _atendimentoDao.RetornarDadosDosAtendimentos(idOperador, idCampanha);
        }

        #region INDICACAO

        public IEnumerable<IndicacaoDoAtendimento> ListarIndicacaoDoAtendimento(long idAtendimento)
        {
            return _atendimentoDao.ListarIndicacaoDoAtendimento(-1, idAtendimento);
        }

        public IndicacaoDoAtendimento RetornarIndicacaoDoAtendimento(long idIndicacao, long idAtendimento)
        {
            return _atendimentoDao.ListarIndicacaoDoAtendimento(idIndicacao, idAtendimento).FirstOrDefault();
        }

        public long GravarIndicacaoDoAtendimento(IndicacaoDoAtendimento indicacao)
        {
            return _atendimentoDao.GravarIndicacaoDoAtendimento(indicacao);
        }

        public void ExcluirIndicacaoDoAtendimento(long idIndicacao)
        {
            _atendimentoDao.ExcluirIndicacaoDoAtendimento(idIndicacao);
        }

        public IEnumerable<string> VerificarSePodeRealizarAtendimentoManual(long idProspect, int idCampanha, int idUsuario)
        {
            return _atendimentoDao.VerificarSePodeRealizarAtendimentoManual(idProspect, idCampanha, idUsuario);
        }


        public IEnumerable<Usuario> ValidarSupervisor(int id, string login, string senha)
        {
            return _atendimentoDao.ValidarSupervisor(id, login, senha);
        }
        #endregion INDICACAO        
    }
}