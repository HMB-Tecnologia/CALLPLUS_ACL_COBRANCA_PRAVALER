using Callplus.CRM.Tabulador.Infra.Dados.Util;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class CicloDeVencimentoDeFaturaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<CicloDeVencimentoDeFatura> ListarCicloDeVencimentoDeFatura(int id, bool ativo)
        {
            var sql = "APP_CRM_CICLO_VENCIMENTO_FATURA_LISTAR";
            var args = new
            {
                Id = id,
                Ativo = ativo
            };
            var resultado = ExecutarProcedure<CicloDeVencimentoDeFatura>(sql, args);
            return resultado;
        }
    }
}
