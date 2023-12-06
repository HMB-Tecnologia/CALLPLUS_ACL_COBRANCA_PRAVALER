using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class StatusDeOfertaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

      
        public IEnumerable<StatusDeOferta> ListarStatusDeOferta(long idCampanha,int? idTipoStatus, bool? ativo)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_EXIBIR";
            var args = new {IdCampanha = idCampanha,  IdTipoTipoStatus = idTipoStatus, Ativo = ativo };
            var resultado = ExecutarProcedure<StatusDeOferta>(sql, args);
            return resultado;
        }

        public StatusDeOferta RetornarStatusDeOferta(int idStatusOferta, int idCampanha)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_LISTAR";
            var args = new { IdStatusOferta = idStatusOferta, IdCampanha = idCampanha };
            var resultado = ExecutarProcedureSingleOrDefault<StatusDeOferta>(sql, args);
            return resultado;
        }

        public IEnumerable<TipoDeStatusDeOferta> ListarTipoDeStatusDeOferta(long idCampanha, bool? ativo)
        {
            var sql = "APP_CRM_TIPO_STATUS_OFERTA_LISTAR";
            var args = new { IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<TipoDeStatusDeOferta>(sql, args);
            return resultado;
        }

        public int Gravar(StatusDeOferta statusDeOferta, string idsCampanhas)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_GRAVAR";
            var args = new
            {
                Id = statusDeOferta.Id,
                IdTipoDeStatusDeOferta = statusDeOferta.IdTipoDeStatusDeOferta,
                Nome = statusDeOferta.Nome,
                Ativo = statusDeOferta.Ativo,
                IdCriador = statusDeOferta.IdCriador,
                IdModificador = statusDeOferta.IdModificador,
                Observacao = statusDeOferta.Observacao,
                IdsCampanhas = idsCampanhas
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        public ConfiguracaoDoStatusDeOferta RetornarConfiguracaoDoStausDeOferta(int? id = null, int? idStatusOferta = null, int? idCampanha = null)
        {
            var sql = "APP_CRM_CONFIGURACAO_DO_STATUS_DE_OFERTA_LISTAR";
            var args = new { Id = id, IdStatusOferta = idStatusOferta, IdCampanha = idCampanha };
            var resultado = ExecutarProcedure<ConfiguracaoDoStatusDeOferta>(sql, args);

            var item = resultado.Any() ? resultado?.First() : null;
            return item;
        }

        public IEnumerable<StatusDeOferta> RetornarCampanhasSelecionadas(int idStatusDeAtendimento)
        {
            var sql = "APP_CRM_CAMPANHAS_DO_STATUS_DE_ATENDIMENTO_LISTAR";

            var args = new
            {
                IdStatusDeAtendimento = idStatusDeAtendimento
            };


            var resultado = ExecutarProcedure<StatusDeOferta>(sql, args);

            return resultado;
        }
    }
}
