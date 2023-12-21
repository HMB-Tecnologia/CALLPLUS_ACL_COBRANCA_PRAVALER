using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
	public class BancoDeDadosDao : DaoBase
	{
		protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

		public string RetornarNomeDoBanco()
		{
			var sql = "APP_CRM_BANCO_DE_DADOS_LISTAR";
			var args = new {  };
			var resultado = ExecuteProcedureScalar(sql, args);
			return resultado.ToString();
		}

	}
}
