using System;
using System.Data;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ConsultaDeProspectService
    {
        private readonly ConsultadDeProspectDao _consultaDeProspectsDao;

        public ConsultaDeProspectService()
        {
            _consultaDeProspectsDao = new ConsultadDeProspectDao();
        }

        public DataTable PesquisarProspects(int idUsuario, long telefone, long idProspect)
        {
            return _consultaDeProspectsDao.PesquisarProspects(idUsuario, telefone, idProspect);

        }

        public DataTable PesquisarProspectsPersonalizado(int idUsuario, int idCampanha, int idCampoPesquisa, string valor)
        {
            return _consultaDeProspectsDao.PesquisarProspectsPersonalizado(idUsuario, idCampanha, idCampoPesquisa, valor);

        }

        public long PesquisarProspectPorTelefone(long telefone, int idUsuario)
        {
            return _consultaDeProspectsDao.PesquisarProspectPorTelefone(telefone);
        }
    }
}
