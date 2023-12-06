using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class RelatorioDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable RetornarContatosTrabalhados(DateTime dataInicio, DateTime dataTermino,int? idCampanha,int? idOperador, int? idSupervisor,
            int? idMailing, string idsStatusAtendimento,string idsStatusOferta, bool filtrarStatusDeAtendimentoNulo = false, bool filtrarStatusDeOfertaNulo = false)
        {
            var sql = "APP_CRM_REL_CONTATOS_TRABALHADOS ";
            sql += $"  @dataInicio = '{dataInicio:yyyy-MM-dd HH:mm:ss}'";
            sql += $" ,@dataTermino = '{dataTermino:yyyy-MM-dd HH:mm:ss}'";
            sql += $" ,@idCampanha = {idCampanha}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idMailing = '{idMailing}'";
            sql += $" ,@idsStatusAtendimento = '{idsStatusAtendimento}'";
            sql += $" ,@idsStatusOferta = '{idsStatusOferta}'";
            sql += $" ,@filtrarStatusDeAtendimentoNulo = '{filtrarStatusDeAtendimentoNulo}'";
            sql += $" ,@filtrarStatusDeOfertaNulo = '{filtrarStatusDeOfertaNulo}'";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }


    }
}
