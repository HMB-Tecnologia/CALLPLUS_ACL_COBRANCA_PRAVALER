using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class AuditoriaDeOfertaService
    {
        private readonly AuditoriaDeOfertaDao _auditoriaDeOfertaDao;

        public AuditoriaDeOfertaService()
        {
            _auditoriaDeOfertaDao = new AuditoriaDeOfertaDao();
        }

        public DataTable Listar(string nomeProspect, DateTime? dataInicio, DateTime? dataTermino, int idTipoDeCampanha, string idsCampanha = "", string idsStatusAuditoria = "", long telefone = -1, string cpf = "", int idSupervisor = -1, int idOperador = -1, string dataFiltro = "")
        {
            return _auditoriaDeOfertaDao.Listar(nomeProspect, dataInicio, dataTermino, idTipoDeCampanha, idsCampanha, idsStatusAuditoria, telefone, cpf, idSupervisor, idOperador, dataFiltro);
        }

        public DataTable ExportarVenda(string nomeProspect, DateTime? dataInicio, DateTime? dataTermino, int idTipoDeCampanha, string idsCampanha,  string idsAuditoria, long telefone, long cpf, int idSupervisor, int idOperador, string dataFiltro)
        {
            return _auditoriaDeOfertaDao.ExportarVenda(nomeProspect, dataInicio, dataTermino,  idTipoDeCampanha, idsCampanha, idsAuditoria, telefone, cpf, idSupervisor, idOperador, dataFiltro);
        }

        public DataTable ListarVendaAgrupadaPorStatusDeAuditoria(DateTime dataInicio, DateTime dataTermino, int idCampanha = -1, int idSupervisor = -1, int idOperador = -1, int idAuditor = -1, long telefone = -1, long cpf = -1)
        {
            return _auditoriaDeOfertaDao.ListarVendaAgrupadaPorStatusDeAuditoria(dataInicio, dataTermino, idCampanha, idSupervisor, idOperador, idAuditor, telefone, cpf);
        }

        public DataTable ListarVendaPorStatusDeAuditoria(DateTime dataInicio, DateTime dataTermino, int idStatusDeAuditoria, int idCampanha = -1, int idSupervisor = -1, int idOperador = -1, int idAuditor = -1, long telefone = -1, long cpf = -1)
        {
            return _auditoriaDeOfertaDao.ListarVendaPorStatusDeAuditoria(dataInicio, dataTermino, idStatusDeAuditoria, idCampanha, idSupervisor, idOperador, idAuditor, telefone, cpf);
        }

        public DataTable ExportarVenda(DateTime? dataInicio, DateTime? dataTermino, string idsCampanha, string idsAuditoria, long telefone, long cpf, int idSupervisor, int idOperador)
        {
            return _auditoriaDeOfertaDao.ExportarVenda(dataInicio, dataTermino, idsCampanha, idsAuditoria, telefone, cpf, idSupervisor, idOperador);
        }
    }
}
