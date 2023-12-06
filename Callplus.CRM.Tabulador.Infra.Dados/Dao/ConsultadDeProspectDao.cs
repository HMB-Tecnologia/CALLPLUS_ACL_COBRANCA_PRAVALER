using System;
using System.Data;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ConsultadDeProspectDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable PesquisarProspects(int idUsuario, long telefone = -1, long idProspect = -1)
        {
            string query = "EXEC APP_CRM_PROSPECT_PESQUISAR";

            query += $" @idUsuario = {idUsuario}";
            query += $" ,@telefone = {telefone}";
            query += $" ,@idProspect = {idProspect}";


            var datatable = CarregarDataTable(query, new { });
            return datatable;
        }

        public DataTable PesquisarProspectsPersonalizado(int idUsuario, int idCampanha, int idCampoPesquisa, string valorPesquisa)
        {
            string query = "EXEC APP_CRM_PROSPECT_PESQUISAR_PERSONALIZADO";

            query += $" @idUsuario = {idUsuario}";
            query += $" ,@idCampanha = {idCampanha}";
            query += $" ,@idCampoPesquisa = {idCampoPesquisa}";
            query += $" ,@valorPesquisa = '{valorPesquisa}'";

            var datatable = CarregarDataTable(query, new { });
            return datatable;
        }
    }
}
