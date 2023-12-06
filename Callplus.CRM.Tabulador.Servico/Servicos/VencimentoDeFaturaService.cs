using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class VencimentoDeFaturaService
    {
        private readonly VencimentoDeFaturaDao _vencimentoFaturaDao;

        public VencimentoDeFaturaService()
        {
            _vencimentoFaturaDao = new VencimentoDeFaturaDao();
        }

        public IEnumerable<CicloDeVencimentoDeFatura> ListarCicloDeVencimentoDeFatura(bool ativo)
        {
            return _vencimentoFaturaDao.ListarCicloDeVencimentoDeFatura(-1, ativo);
        }

        public IEnumerable<ConfiguracaoDeVencimentoDeFatura> ListarConfiguracaoDeVencimentoDeFatura(bool ativo)
        {
            return _vencimentoFaturaDao.ListarConfiguracaoDeVencimentoDeFatura(-1, ativo);
        }

        public DataTable ListarExibicao(int id, int diaDeAtivacao, bool ativo)
        {
            return _vencimentoFaturaDao.ListarExibicao(id, diaDeAtivacao, ativo);
        }

        public int GravarVencimentoDeFatura(VencimentoFatura vencimentoFatura)
        {
            return _vencimentoFaturaDao.GravarVencimentoDeFatura(vencimentoFatura);
        }

        public VencimentoFatura RetornarVencimento(int id)
        {
            return _vencimentoFaturaDao.Listar(id).FirstOrDefault();
        }

        public int GravarCicloDeVencimento(CicloDeVencimentoDeFatura cicloDeVencimentoDeFatura)
        {
            return _vencimentoFaturaDao.GravarCicloDeVencimento(cicloDeVencimentoDeFatura);
        }
    }
}
