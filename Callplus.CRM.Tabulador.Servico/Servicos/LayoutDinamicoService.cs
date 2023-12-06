using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class LayoutDinamicoService
    {
        private readonly LayoutDinamicoDao _layoutDinamicoDao;

        public LayoutDinamicoService()
        {
            _layoutDinamicoDao = new LayoutDinamicoDao();
        }

        public List<ValorDeCampoDinamico> ListarValoresDeCamposDinamicos(long idProspect, int idCampanha)
        {
            return _layoutDinamicoDao.ListarValoresDeCamposDinamicos(idProspect, idCampanha);
        }

        public LayoutDeCampoDinamico RetornarLayoutDinamico(int idLayout)
        {
            return _layoutDinamicoDao.RetornarLayoutDinamico(idLayout);
        }

        public object Listar(int id,string nome, bool? ativo)
        {
            return _layoutDinamicoDao.Listar(id,nome, ativo);
        }

        public IEnumerable<LayoutDeCampoDinamico> ListarLayout(int id, bool ativo)
        {
            return _layoutDinamicoDao.Listar(id, ativo);
        }
    }
}
