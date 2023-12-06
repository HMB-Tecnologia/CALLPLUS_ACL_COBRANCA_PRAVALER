using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class PlanoPorOperadoraParaComparacaoService
    {
        private readonly PlanoPorOperadoraParaComparacaoDao _dao;

        public PlanoPorOperadoraParaComparacaoService()
        {
            _dao = new PlanoPorOperadoraParaComparacaoDao();
        }

        public IEnumerable<Operadora> ListarOperadora()
        {
            return _dao.ListarOperadora();
        }

        public IEnumerable<TipoDePlanoPorOperadora> ListarTipoDePlanoPorOperadora()
        {
            return _dao.ListarTipoDePlanoPorOperadora();
        }

        public DataTable Listar(int idOperadora, int idTipoDePlanoDaOperadora, int idCampanha)
        {
            return _dao.Listar(idOperadora, idTipoDePlanoDaOperadora, idCampanha);
        }

        public DataTable Listar(int id, int idOperadora, int idTipoDePlanoDaOperadora, int idCampanha, bool ativo)
        {
            return _dao.Listar(id, idOperadora, idTipoDePlanoDaOperadora, idCampanha, ativo);
        }

        public PlanoPorOperadoraParaComparacao Retornar(int id)
        {
            return _dao.Listar(id, false).FirstOrDefault();
        }

        public int Gravar(PlanoPorOperadoraParaComparacao plano, string campanhas)
        {
            return _dao.Gravar(plano, campanhas);
        }
    }
}
