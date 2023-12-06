using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class StatusDeAuditoriaService
    {
        private readonly StatusDeAuditoriaDao _statusDeAuditoriaDao;

        public StatusDeAuditoriaService()
        {
            _statusDeAuditoriaDao = new StatusDeAuditoriaDao();
        }

        public IEnumerable<StatusDeAuditoria> Listar(int idCampanha, bool ativo, int idStatus = -1)
        {
            return _statusDeAuditoriaDao.Listar(idCampanha, ativo, idStatus: idStatus);
        }

        public IEnumerable<StatusDeAuditoria> Listar(bool ativo,  int idStatus = -1)
        {
            return _statusDeAuditoriaDao.Listar(ativo, idStatus: idStatus);
        }

        public IEnumerable<StatusDeAuditoria> OperadorListar(bool ativo, int idStatus = -1)
        {
            return _statusDeAuditoriaDao.OperadorListar(ativo, idStatus: idStatus);
        }

        public StatusDeAuditoria Retornar(int id)
        {
            return _statusDeAuditoriaDao.Retornar(id).FirstOrDefault();
        }

        public DataTable ListarExibicao(int id, string nome, bool ativo, int idCampanha)
        {
            return _statusDeAuditoriaDao.ListarExibicao(id, nome, ativo, idCampanha);
        }

        public StatusDeAuditoria RetornarStatusDeAuditoria(int idStatusAuditoria)
        {
            return _statusDeAuditoriaDao.Listar(-1, ativo: null, idStatus: idStatusAuditoria)?.FirstOrDefault(x=>x.Id == idStatusAuditoria);
        }

        public int Gravar(StatusDeAuditoria status, string idsCampanhas)
        {
            return _statusDeAuditoriaDao.Gravar(status, idsCampanhas);
        }

        public IEnumerable<StatusDeAuditoria> RetornarCampanhasSelecionadas(int idStatusAuditoria)
        {
            return _statusDeAuditoriaDao.RetornarCampanhasSelecionadas(idStatusAuditoria);
        }

        public DataTable ListarRanking(int idAuditor, string dataInicio, string dataFim)
        {
            return _statusDeAuditoriaDao.ListarRanking(idAuditor, dataInicio, dataFim);
        }

        public async Task<DataTable> ExibirRanking(string dataInicio, string dataFim, int idAuditor)
        {
            return await _statusDeAuditoriaDao.ExibirRanking(dataInicio, dataFim, idAuditor);
        }

        public IEnumerable<CampanhaDoStatusDeAuditoria> ListarStatusDeAuditoriaDaCampanha(int id, bool ativo)
        {
            return _statusDeAuditoriaDao.ListarStatusDeAuditoriaDaCampanha(id, ativo);
        }
    }
}
