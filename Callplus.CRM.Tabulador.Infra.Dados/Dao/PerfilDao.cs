using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class PerfilDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Perfil> Listar(bool ativo)
        {
            var sql = "APP_CRM_PERFIL_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<Perfil>(sql, args);
            return resultado;
        }
    }
}
