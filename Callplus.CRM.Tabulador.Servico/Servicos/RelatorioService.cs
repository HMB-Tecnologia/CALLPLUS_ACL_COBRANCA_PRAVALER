using System;
using System.Data;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class RelatorioService
    {
        private readonly RelatorioDao _relatorioDao;

        public RelatorioService()
        {
            _relatorioDao = new RelatorioDao();

        }

        public DataTable RetornarContatosTrabalhados(DateTime dataInicio, DateTime dataTermino, int? idCampanha, int? idTipoDeCampanha, int? idOperador, int? idSupervisor,
        int? idMailing, string idsStatusAtendimento, string idsStatusOferta, bool filtrarStatusDeAtendimentoNulo = false, bool filtrarStatusDeOfertaNulo = false)
        {
            return _relatorioDao.RetornarContatosTrabalhados(dataInicio, dataTermino, idCampanha, idTipoDeCampanha, idOperador,
                idSupervisor,idMailing, idsStatusAtendimento, idsStatusOferta, filtrarStatusDeAtendimentoNulo,filtrarStatusDeOfertaNulo);
        }

        public DataTable RetornarRankingDaOperacao(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            return _relatorioDao.RetornarRankingDaOperacao(idCampanha, idSupervisor, idOperador, data);
        }

        public DataTable RetornarAtendimentoPorStatus(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            return _relatorioDao.RetornarAtendimentoPorStatus(idCampanha, idSupervisor, idOperador, data);
        }

        public DataTable RetornarResultadoHoraHora(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            return _relatorioDao.RetornarResultadoHoraHora(idCampanha, idSupervisor, idOperador, data);
        }

        public DataTable RetornarAtendimentoPorTipo(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            return _relatorioDao.RetornarAtendimentoPorTipo(idCampanha, idSupervisor, idOperador, data);
        }

        public DataTable RetornarAuditoriaDaVenda(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            return _relatorioDao.RetornarAuditoriaDaVenda(idCampanha, idSupervisor, idOperador, data);
        }
    }
}
