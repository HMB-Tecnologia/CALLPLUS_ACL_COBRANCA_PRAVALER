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
    public class AuditoriaDeOfertaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable Listar(string nome, DateTime? dataInicio, DateTime? dataTermino, int idTipoDeCampanha, string idsCampanha = "", string idsStatusAuditoria = "", long telefone = -1, long cpf = -1, int idSupervisor = -1, int idOperador = -1, string dataFiltro = "")
        {
            var sql = "APP_CRM_AUDITORIA_DE_VENDA_LISTAR_EXIBICAO_3 ";
            sql += $" @nome = '{nome}'";
            sql += $" ,@telefone = {telefone}";
            sql += $" ,@cpf = {cpf}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@idTipoDeCampanha =  {idTipoDeCampanha}";
            sql += $" ,@idsCampanha =  '{idsCampanha}'";
            sql += $" ,@idsStatusAuditoria = '{idsStatusAuditoria}'";
            sql += $" ,@dataFiltro = '{dataFiltro}'";

            if (dataInicio != null)
                sql += $" ,@dataInicio = '{dataInicio.Value.ToString("yyyy-MM-dd")} 00:00:00'";
            if (dataTermino != null)
                sql += $", @dataTermino = '{dataTermino.Value.ToString("yyyy-MM-dd")} 23:59:59'";

            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }
        
        public DataTable ExportarVenda(string nome, DateTime? dataInicio, DateTime? dataTermino, int idTipoDeCampanha, string idsCampanha = "", string idsStatusAuditoria = "", long telefone = -1, long cpf = -1, int idSupervisor = -1, int idOperador = -1, string dataFiltro = "")
        {
            var sql = "APP_CRM_AUDITORIA_DE_VENDA_EXPORTAR_2";
            sql += $" @nome = '{nome}'";
            sql += $" ,@telefone = {telefone}";
            sql += $" ,@cpf = {cpf}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@idsCampanha =  '{idsCampanha}'";
            sql += $" ,@idTipoDeCampanha =  {idTipoDeCampanha}";
            sql += $" ,@idsStatusAuditoria = '{idsStatusAuditoria}'";
            sql += $" ,@dataFiltro = '{dataFiltro}'";

            if (dataInicio != null)
                sql += $" ,@dataInicio = '{dataInicio.Value.ToString("yyyy-MM-dd")} 00:00:00'";
            if (dataTermino != null)
                sql += $", @dataTermino = '{dataTermino.Value.ToString("yyyy-MM-dd")} 23:59:59'";

            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }

        public DataTable ListarVendaAgrupadaPorStatusDeAuditoria(DateTime dataInicio, DateTime dataTermino, int idCampanha = -1, int idSupervisor = -1, int idOperador = -1, int idAuditor = -1, long telefone = -1, long cpf = -1)
        {
            var sql = "APP_CRM_AUDITORIA_VENDA_POR_STATUS_AUDITORIA_AGRUPAR_2 ";
            sql += $"  @dataInicio = '{dataInicio.ToString("yyyy-MM-dd")}'";
            sql += $" ,@dataTermino =  '{dataTermino.ToString("yyyy-MM-dd")}'";
            sql += $" ,@idCampanha =  {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@idAuditor = {idAuditor}";
            sql += $" ,@telefone = {telefone}";
            sql += $" ,@cpf = {cpf}";

            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }

        public DataTable ListarVendaPorStatusDeAuditoria(DateTime dataInicio, DateTime dataTermino, int idStatusDeAuditoria, int idCampanha = -1, int idSupervisor = -1, int idOperador = -1, int idAuditor = -1, long telefone = -1, long cpf = -1)
        {
            var sql = "APP_CRM_AUDITORIA_VENDA_POR_STATUS_AUDITORIA_LISTAR_2 ";
            sql += $"  @dataInicio = '{dataInicio.ToString("yyyy-MM-dd")}'";
            sql += $" ,@dataTermino =  '{dataTermino.ToString("yyyy-MM-dd")}'";
            sql += $" ,@idCampanha =  {idCampanha}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@idAuditor = {idAuditor}";
            sql += $" ,@telefone = {telefone}";
            sql += $" ,@cpf = {cpf}";
            sql += $" ,@idStatusDeAuditoria = {idStatusDeAuditoria}";

            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }

        public DataTable ExportarVenda(DateTime? dataInicio, DateTime? dataTermino, string idsCampanha = "", string idsStatusAuditoria = "", long telefone = -1, long cpf = -1, int idSupervisor = -1, int idOperador = -1)
        {
            var sql = "APP_CRM_AUDITORIA_DE_VENDA_EXPORTAR";
            sql += $"  @telefone = {telefone}";
            sql += $" ,@cpf = {cpf}";
            sql += $" ,@idSupervisor = {idSupervisor}";
            sql += $" ,@idOperador = {idOperador}";
            sql += $" ,@idsCampanha =  '{idsCampanha}'";
            sql += $" ,@idsStatusAuditoria = '{idsStatusAuditoria}'";

            if (dataInicio != null)
                sql += $" ,@dataInicio = '{dataInicio.Value.ToString("yyyy-MM-dd")} 00:00:00'";
            if (dataTermino != null)
                sql += $", @dataTermino = '{dataTermino.Value.ToString("yyyy-MM-dd")} 23:59:59'";

            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }
    }
}
