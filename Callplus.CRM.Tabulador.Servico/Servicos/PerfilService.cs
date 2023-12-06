using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class PerfilService
    {
        private readonly PerfilDao _perfilDao;

        public PerfilService()
        {
            _perfilDao = new PerfilDao();
        }

        public IEnumerable<Perfil> Listar(bool ativo)
        {
            return _perfilDao.Listar(ativo);
        }

       
    }
}
