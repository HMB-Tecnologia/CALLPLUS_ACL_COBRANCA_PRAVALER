using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class GrauDeParentescoService
    {
        private readonly GrauDeParentescoDao _grauDeParentescoDao;

        public GrauDeParentescoService()
        {
            _grauDeParentescoDao = new GrauDeParentescoDao();
        }

        public IEnumerable<GrauDeParentesco> ListarParentescos(bool ativo)
        {
            return _grauDeParentescoDao.ListarParentescos(ativo);
        }
    }
}
