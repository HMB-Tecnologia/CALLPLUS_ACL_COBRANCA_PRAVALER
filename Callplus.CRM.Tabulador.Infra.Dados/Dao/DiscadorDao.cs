using Callplus.CRM.Tabulador.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System.Data;
using System;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class DiscadorDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Discador> Listar(int id, bool ativo)
        {
            var sql = "APP_CRM_DISCADOR_LISTAR";

            var args = new {                
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<Discador>(sql, args);

            return resultado;
        }
        
        public Discador RetornarDiscador(int idDiscador)
        {
            string sql = $"APP_CRM_DISCADOR_LISTAR";
            var args = new {IdDiscador = idDiscador };

            var resultado = ExecutarProcedureSingleOrDefault<Discador>(sql, args);
            return resultado;
        }

        public string RetornarMensagemDiscador(int idDiscador, int codRetorno)
        {
            string sql = $"APP_CRM_DISCADOR_RETORNAR_MENSAGEM_DISCADOR";
            var args = new { IdDiscador = idDiscador,Codigo = codRetorno };

            var resultado = ExecutarProcedureSingleOrDefault<string>(sql, args);
            return resultado;
        }

        public string RetornarRamalUsuario(int idUsuario, int idDiscador)
        {
            var sql = "APP_CRM_DISCADOR_RETORNAR_RAMAL";
            var args = new { idUsuario, idDiscador };

            return ExecutarProcedureSingleOrDefault<string>(sql, args);
        }

        public Discador RetornarTipoEnvioDadosDiscador(int idCampanha)
        {
            var sql = $"APP_CRM_TIPO_ENVIO_DADOS_DISCADOR_LISTAR";
            var args = new { IdCampanha = idCampanha };

            var resultado = ExecutarProcedureSingleOrDefault<Discador>(sql, args);
            return resultado;
        }

        public string RetornarStatusDoDiscador(int idDiscador, int codigo)
        {
            string sql = $"APP_CRM_DISCADOR_RETORNAR_STATUS_DISCADOR";
            var args = new { IdDiscador = idDiscador, Codigo = codigo };

            var resultado = ExecutarProcedureSingleOrDefault<string>(sql, args);
            return resultado;
        }
    }
}
