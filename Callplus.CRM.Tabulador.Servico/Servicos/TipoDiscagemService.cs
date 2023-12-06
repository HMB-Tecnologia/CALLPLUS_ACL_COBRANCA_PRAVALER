using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class TipoDiscagemService
    {
        private readonly TipoDiscagemDao _Dao;

        public TipoDiscagemService()
        {
            _Dao = new TipoDiscagemDao();
        }
        public IEnumerable<TipoDiscagem> ListarTipoDeDiscagem(int id, bool ativo)
        {
            return _Dao.ListarTipoDeDiscagem(id, ativo);
        }
    }
}
