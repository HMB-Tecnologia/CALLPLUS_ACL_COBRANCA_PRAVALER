using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class StatusDeAuditoriaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<StatusDeAuditoria> Listar(int idCampanha, bool? ativo = null, int idStatus = -1)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_LISTAR";
            var args = new { Id = idStatus, Ativo = ativo , IdCampanha = idCampanha};
            var resultado = ExecutarProcedure<StatusDeAuditoria>(sql, args);
            return resultado;
        }

        //SOBRECARGA DE MÉTODO LISTAR
        public IEnumerable<StatusDeAuditoria> Listar(bool? ativo = null,  int idStatus = -1)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_LISTAR";
            var args = new { Id = idStatus, Ativo = ativo };
            var resultado = ExecutarProcedure<StatusDeAuditoria>(sql, args);
            return resultado;
        }

        public IEnumerable<StatusDeAuditoria> OperadorListar(bool? ativo = null, int idStatus = -1)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_OPERADOR_LISTAR";
            var args = new { Id = idStatus, Ativo = ativo };
            var resultado = ExecutarProcedure<StatusDeAuditoria>(sql, args);
            return resultado;
        }

        public DataTable ListarExibicao(int id, string nome, bool ativo, int idCampanha)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_LISTAR_EXIBICAO";

            sql += string.Format(" @id = {0}, @nome = '{1}', @ativo = {2}, @idcampanha = {3}", id, nome, ativo, idCampanha);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<StatusDeAuditoria> Retornar(int id)
        {
            var sql = "APP_CRM_RETORNAR_STATUS_DE_AUDITORIA_DA_CAMPANHA ";

            var args = new
            {
                id = id
            };

            var resultado = ExecutarProcedure<StatusDeAuditoria>(sql, args);

            return resultado;
        }

        public int Gravar(StatusDeAuditoria status, string idsCampanhas)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_GRAVAR";

            var args = new
            {
                Id = status.Id,
                Nome = status.Nome,
                AprovaOferta = status.AprovaOferta,
                HabilitaTrocaStatus = status.HabilitaTrocaDeStatus,
                PermitidoHumano = status.PermitidoHumano,
                IdModificador = status.IdModificador,
                IdCriador = status.IdCriador,
                Ativo = status.Ativo,
                IdsCampanhas = idsCampanhas,
                AuditoriaOperador = status.AuditoriaOperador
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        public IEnumerable<CampanhaDoStatusDeAuditoria> ListarStatusDeAuditoriaDaCampanha(int id, bool ativo)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_DA_CAMPANHA_LISTAR ";

            var args = new
            {
                IdCampanha = id,
                Ativo =ativo
            };

            var resultado = ExecutarProcedure<CampanhaDoStatusDeAuditoria>(sql, args);
            return resultado;
        }

        public async Task<DataTable> ExibirRanking(string dataInicio, string dataFim, int idAuditor)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_EXIBIR_RANKING";

            sql += $" @IdAuditor = {idAuditor} ";

            if (dataInicio != null)
                sql += $" ,@dataInicio = '{dataInicio} 00:00:00'";

            if (dataFim != null)
                sql += $", @dataFim = '{dataFim} 23:59:59'";

            var args = new
            { };


            var resultado = await CarregarDataTableAsync(sql, args);

            return resultado;
        }

        public DataTable ListarRanking(int idAuditor, string dataInicio, string dataFim)
        {
            var sql = "APP_CRM_STATUS_DE_AUDITORIA_LISTAR_RANKING";

            sql += $" @IdAuditor = {idAuditor} ";

            if (dataInicio != null)
                sql += $" ,@dataInicio = '{dataInicio} 00:00:00'";

            if (dataFim != null)
                sql += $", @dataFim = '{dataFim} 23:59:59'";

            var args = new
            { };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<StatusDeAuditoria> RetornarCampanhasSelecionadas(int idStatusAuditoria)
        {
            var sql = "APP_CRM_CAMPANHAS_DO_STATUS_DE_AUDITORIA_LISTAR";

            var args = new
            {
                IdStatusAuditoria = idStatusAuditoria
            };


            var resultado = ExecutarProcedure<StatusDeAuditoria>(sql, args);

            return resultado;
        }
    }
}
