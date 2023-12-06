using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class TipoDeFraseService
    {
        private readonly TipoDeFraseDao _tipoDeFraseDao;

        public TipoDeFraseService()
        {
            _tipoDeFraseDao = new TipoDeFraseDao();
        }
        public IEnumerable<TipoDeFrasesGamificacao> Listar()
        {
            return _tipoDeFraseDao.Listar();
        }
    }
}
