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

        public DataTable RetornarContatosTrabalhados(DateTime dataInicio, DateTime dataTermino, int? idCampanha, int? idOperador, int? idSupervisor,
        int? idMailing, string idsStatusAtendimento, string idsStatusOferta, bool filtrarStatusDeAtendimentoNulo = false, bool filtrarStatusDeOfertaNulo = false)
        {
            return _relatorioDao.RetornarContatosTrabalhados(dataInicio, dataTermino, idCampanha, idOperador,
                idSupervisor,idMailing, idsStatusAtendimento, idsStatusOferta, filtrarStatusDeAtendimentoNulo,filtrarStatusDeOfertaNulo);
        }

    }
}
