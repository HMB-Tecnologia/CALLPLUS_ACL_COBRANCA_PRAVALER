using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;


namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class GrauDeParentescoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<GrauDeParentesco> ListarParentescos(bool ativo)
        {
            var sql = "APP_CRM_GRAU_DE_PARENTESCO_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<GrauDeParentesco>(sql, args);
            return resultado;
        }
    }
}
