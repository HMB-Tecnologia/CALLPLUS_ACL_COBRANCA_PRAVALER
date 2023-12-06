using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ConfiguracaoDeCampoDoLayoutDinamicoService
    {
        private readonly ConfiguracaoDeCampoDoLayoutDinamicoDao _configuracaoDeCampoDoLayoutDinamicoDao;

        public ConfiguracaoDeCampoDoLayoutDinamicoService()
        {
            _configuracaoDeCampoDoLayoutDinamicoDao = new ConfiguracaoDeCampoDoLayoutDinamicoDao();
        }

        public IEnumerable<ConfiguracaoDeCampoDoLayoutDinamico> ListarConfiguracaoDeCampoDoLayoutDinamico (int idLayout)
        {
            return _configuracaoDeCampoDoLayoutDinamicoDao.ListarConfiguracaoDeCampoDoLayoutDinamico(idLayout);
        }

    }
}
