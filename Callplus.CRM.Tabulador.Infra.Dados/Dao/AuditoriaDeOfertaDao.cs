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

        public DataTable Listar(DateTime? dataInicio, DateTime? dataTermino, string idsCampanha = "", string idsStatusAuditoria = "", long telefone = -1, long cpf = -1, int idSupervisor = -1, int idOperador = -1)
        {
            var sql = "APP_CRM_AUDITORIA_DE_VENDA_LISTAR_EXIBICAO ";
            sql += $" @telefone = {telefone}";
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
