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

        public DataTable RetornarContatosTrabalhados(DateTime dataInicio, DateTime dataTermino,int? idCampanha, int? idTipoDeCampanha, int? idOperador, int? idSupervisor,
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
            sql += $" ,@IdsStatusAcordo = '{idsStatusOferta}'";
            sql += $" ,@filtrarStatusDeAtendimentoNulo = '{filtrarStatusDeAtendimentoNulo}'";
            sql += $" ,@filtrarStatusDeOfertaNulo = '{filtrarStatusDeOfertaNulo}'";
            //sql += $" ,@idTipoDeCampanha = {idTipoDeCampanha}";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarRankingDaOperacao(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            var sql = "APP_CRM_RANKING_OPERACAO_LISTAR ";
            sql += $" @idCampanha = {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@data = '{data:yyyy-MM-dd HH:mm:ss}'";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarAtendimentoPorStatus(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            var sql = "APP_CRM_DASHBOARD_OPERACAO_LISTAR_ATENDIMENTO_POR_STATUS ";
            sql += $" @idCampanha = {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@data = '{data:yyyy-MM-dd HH:mm:ss}'";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        
        public DataTable RetornarResultadoHoraHora(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            var sql = "APP_CRM_DASHBOARD_OPERACAO_LISTAR_RESULTADO_HORA_HORA ";
            sql += $" @idCampanha = {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@data = '{data:yyyy-MM-dd HH:mm:ss}'";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarAtendimentoPorTipo(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            var sql = "APP_CRM_DASHBOARD_OPERACAO_LISTAR_ATENDIMENTO_POR_TIPO ";
            sql += $" @idCampanha = {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@data = '{data:yyyy-MM-dd HH:mm:ss}'";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarAuditoriaDaVenda(int idCampanha, int idSupervisor, int idOperador, DateTime data)
        {
            var sql = "APP_CRM_DASHBOARD_OPERACAO_LISTAR_AUDITORIA_VENDA ";
            sql += $" @idCampanha = {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@data = '{data:yyyy-MM-dd HH:mm:ss}'";

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
    }
}
