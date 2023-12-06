using System.Data;
using System.Data.SqlClient;

namespace Callplus.CRM.Tabulador.Infra.Dados.Util
{
    public static class DaoUtil
    {
        public static DataTable ExecuteDataTable(string sql, SqlTransaction transacao)
        {
            var dt = new DataTable();
            using (var cmd = new SqlCommand(sql, transacao.Connection, transacao))
            {
                IDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            //var dt = new DataTable();
            //using (var connection = new SqlConnection(PontoBr.Banco.SqlServer.sConexao))
            //{
            //    using (var cmd = new SqlCommand(sql, connection))
            //    {
            //        connection.Open();
            //        IDataReader reader = cmd.ExecuteReader();
            //        dt.Load(reader);
            //        connection.Close();
            //        return dt;
            //    }
            //}

            return null;
        }

        public static T ExecuteScalar<T>(string sql, SqlTransaction transcao)
        {
            using (var cmd = new SqlCommand(sql, transcao.Connection, transcao))
            {
                return (T)cmd.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(string sql, SqlTransaction transcao)
        {
            using (var cmd = new SqlCommand(sql, transcao.Connection, transcao))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public static string ObterValorDaColunaEmDataTable(DataTable dataTable, int index, string nomeDaColuna)
        {
            if (dataTable == null) return null;
            var columns = dataTable.Columns;
            if (columns.Contains(nomeDaColuna) && dataTable.Rows.Count > index)
            {
                return dataTable.Rows[index][nomeDaColuna].ToString();
            }

            return null;
        }
    }
}
