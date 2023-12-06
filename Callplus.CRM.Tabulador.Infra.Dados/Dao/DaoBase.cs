using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public abstract class DaoBase
    {
        protected abstract IDbConnection Connection { get; }

        protected IList<T> Select<T>(string query, object arguments)
        {
            IList<T> entities;

            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                entities = connection.Query<T>(query, arguments).ToList();
            }

            return entities;
        }

        protected IList<T> SelectIlist<T>(string query, object arguments)
        {
            IList<T> entities;

            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                entities = connection.Query<T>(query, arguments).ToList();
            }

            return entities;
        }

        protected IEnumerable<T> ExecutarProcedure<T>(string procedure, object arguments)
        {
            IEnumerable<T> entities;

            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                entities = connection.Query<T>(procedure, arguments, commandType: CommandType.StoredProcedure);
            }

            return entities;
        }

        protected T ExecutarProcedureSingleOrDefault<T>(string procedure, object arguments)
        {
            T entity;

            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                entity = connection.Query<T>(procedure, arguments, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }

            return entity;
        }

        protected T SelectSingleOrDefault<T>(string query, object arguments)
        {
            T entity;

            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                entity =
                    connection.Query<T>(query, arguments).SingleOrDefault();
            }

            return entity;
        }

        protected void ExecutarProcedure(string procedure, object arguments)
        {
            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                connection.Execute(procedure, arguments, commandType: CommandType.StoredProcedure);
            }
        }

        protected void ExecutarSql(string query, object arguments)
        {
            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                connection.Execute(query, arguments);
            }
        }

        protected void Update(string query, object arguments)
        {
            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                connection.Execute(query, arguments, commandType: CommandType.StoredProcedure);
            }
        }

        protected object ExecuteProcedureScalar(string query, object arguments)
        {
            object id;
            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                id = connection.ExecuteScalar<object>(query, arguments, commandType: CommandType.StoredProcedure);
            }
            return id;
        }

        protected object ExecuteScalar(string query, object arguments)
        {
            object id;
            using (var connection = Connection)
            {
                AbrirConexaoSeEstiverFechada(connection);
                id = connection.ExecuteScalar<object>(query, arguments);
            }
            return id;
        }

        protected DataTable CarregarDataTable(string sql, object arguments)
        {
            DataTable dt = new DataTable();            
            IDataReader reader = Connection.ExecuteReader(sql, arguments, commandTimeout: 300);
            dt.Load(reader);
            return dt;
        }

        protected async Task<DataTable> CarregarDataTableAsync(string sql, object arguments)
        {
            DataTable dt = new DataTable();
            IDataReader reader = await Connection.ExecuteReaderAsync(sql, arguments, commandTimeout: 300);
            dt.Load(reader);
            return dt;
        }

        void AbrirConexaoSeEstiverFechada(IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
    }
}
