using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ConfiguracaoDeCampoDoLayoutDinamicoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<ConfiguracaoDeCampoDoLayoutDinamico> ListarConfiguracaoDeCampoDoLayoutDinamico(int idLayout)
        {
            var sql = "APP_CRM_CONFIGURACAO_DE_LAYOUT_DE_CAMPO_DINAMICO_LISTAR";
            var args = new { IdLayout = idLayout };
            var resultado = ExecutarProcedure<ConfiguracaoDeCampoDoLayoutDinamico>(sql, args);
            return resultado;
        }
    }
}
