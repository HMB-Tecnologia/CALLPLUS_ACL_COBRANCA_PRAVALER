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
    public class TipoDiscagemDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<TipoDiscagem> ListarTipoDeDiscagem(int id, bool ativo)
        {
            var sql = "APP_CRM_DISCADOR_LISTAR_TIPO_DE_DISCAGEM";

            var args = new
            {
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<TipoDiscagem>(sql, args);

            return resultado;
        }
    }
}
