using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Callplus.CRM.Tabulador.Infra.Dados.Util
{
    public static class ConnectionFactory
    {
        private static readonly string _stringConexao = ConfigurationManager.AppSettings["StringConexao"].ToString();

        public static IDbConnection ObterConexao()
        {
            var connectionString = _stringConexao;
            return new SqlConnection(connectionString);
        }

        public static IDbConnection ObterConexaoUtil()
        {
            var connectionString = ConfigurationManager.AppSettings["StringConexaoUtil"].ToString();
            return new SqlConnection(connectionString);
        }
    }
}
