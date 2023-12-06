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

        public DataTable Listar(DateTime? dataInicio, DateTime? dataTermino, string idsCampanha = "", string idsStatusAuditoria = "", long telefone = -1, long cpf = -1, int idSupervisor = -1, int idOperador = -1)
        {
            return _auditoriaDeOfertaDao.Listar(dataInicio, dataTermino, idsCampanha, idsStatusAuditoria, telefone, cpf, idSupervisor, idOperador);
        }
    }
}
