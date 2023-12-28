using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class StatusDeAcordoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

      
        public IEnumerable<StatusDeAcordo> ListarStatusDeAcordo(long? idCampanha,int? idTipoStatus, bool? ativo)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_LISTAR";
            var args = new {IdCampanha = idCampanha,  IdTipoTipoStatus = idTipoStatus, Ativo = ativo };
            var resultado = ExecutarProcedure<StatusDeAcordo>(sql, args);
            return resultado;
        }

        public StatusDeAcordo RetornarStatusDeAcordo(int idStatusAcordo, int idCampanha)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_LISTAR";
            var args = new { IdStatusOferta = idStatusAcordo, IdCampanha = idCampanha };
            var resultado = ExecutarProcedureSingleOrDefault<StatusDeAcordo>(sql, args);
            return resultado;
        }

        public IEnumerable<TipoDeStatusDeAcordo> ListarTipoDeStatusDeOferta(long idCampanha, bool? ativo)
        {
            var sql = "APP_CRM_TIPO_STATUS_OFERTA_LISTAR";
            var args = new { IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<TipoDeStatusDeAcordo>(sql, args);
            return resultado;
        }

        public IEnumerable<StatusDeAcordo> RetornarCampanhasSelecionadas(int idStatusDeOferta)
        {
            var sql = "APP_CRM_CAMPANHAS_DO_STATUS_DE_OFERTA_LISTAR";

            var args = new
            {
                IdStatusDeOferta = idStatusDeOferta
            };


            var resultado = ExecutarProcedure<StatusDeAcordo>(sql, args);

            return resultado;
        }

        public ConfiguracaoDoStatusDeOferta RetornarStatusDeAtendimento(StatusDeAcordo statusDeOferta)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_VINCULO_RETORNAR";

            var args = new
            {
                idStatusDeOferta = statusDeOferta.Id
            };


            var resultado = ExecutarProcedureSingleOrDefault<ConfiguracaoDoStatusDeOferta>(sql, args);

            return resultado;
        }

        public StatusDeAcordo ListarTipoDeStatusDeOferta(int idStatus)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_TIPO_LISTAR";
            var args = new { IdStatus = idStatus };
            var resultado = ExecutarProcedure<StatusDeAcordo>(sql, args);
            return resultado.FirstOrDefault();
        }
        public int Gravar(StatusDeAcordo statusDeOferta, string idsCampanhas)
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

        public IEnumerable<StatusDeAcordo> Listar(int? id, bool? ativo)
        {
            var sql = "APP_CRM_RETORNAR_STATUS_DE_OFERTA";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<StatusDeAcordo>(sql, args);
            return resultado;
        }

        public IEnumerable<StatusDeOfertaDaCampanha> ListarStatusDaOfertaDaCampanha(int id, bool ativo)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_DA_CAMPANHA_LISTAR";
            var args = new { IdCampanha = id, Ativo = ativo };
            var resultado = ExecutarProcedure<StatusDeOfertaDaCampanha>(sql, args);
            return resultado;
        }

        public void GravarConfiguracaoDoStatusDeOferta(int idStatusDeOferta, int idStatusDeAtendimento)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_CONFIGURACAO_GRAVAR";

            var args = new
            {
                idStatusDeOferta = idStatusDeOferta,
                idStatusDeAtendimento = idStatusDeAtendimento
            };

            ExecutarProcedure(sql, args);
        }

        public ConfiguracaoDoStatusDeOferta RetornarConfiguracaoDoStausDeOferta(int? id = null, int? idStatusOferta = null, int? idCampanha = null)
        {
            var sql = "APP_CRM_CONFIGURACAO_DO_STATUS_DE_OFERTA_LISTAR";
            var args = new { Id = id, IdStatusOferta = idStatusOferta, IdCampanha = idCampanha };
            var resultado = ExecutarProcedure<ConfiguracaoDoStatusDeOferta>(sql, args);

            var item = resultado.Any() ? resultado?.First() : null;
            return item;
        }

        public DataTable ListarStatusDeOfertaExibicao(long? idCampanha, bool ativo, string nome, int? idStatus)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_LISTAR_EXIBICAO";
            sql += string.Format(" @idcampanha = {0}, @ativo= {1}, @nome = '{2}', @idStatus = {3}", idCampanha, ativo, nome, idStatus);

            var args = new
            {

            };
            var resultado = CarregarDataTable(sql, args);
            return resultado;
        }

        public IEnumerable<StatusDeAcordo> ListarStatusDeOfertaPorTipoCampanha(long? idCampanha, int? idTipoStatus, int? idTipoDeCampanha)
        {
            var sql = "APP_CRM_STATUS_DE_OFERTA_POR_TIPO_CAMPANHA_LISTAR";
            var args = new { IdCampanha = idCampanha, IdTipoTipoStatus = idTipoStatus, IdTipoDeCampanha = idTipoDeCampanha };
            var resultado = ExecutarProcedure<StatusDeAcordo>(sql, args);
            return resultado;
        }
    }
}
