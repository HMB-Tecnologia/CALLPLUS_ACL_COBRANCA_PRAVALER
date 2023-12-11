using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class StatusDeAtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<KeyValuePair<int, string>> ListarTipoDeStatusDeAtendimento(bool ativo)
        {
            var sql = "APP_CRM_TIPO_STATUS_ATENDIMENTO_LISTAR";
            var args = new { Ativo = ativo };
            var lista = new List<KeyValuePair<int, string>>();
            var resultado = ExecutarProcedure<TipoDeStatusDeAtendimento>(sql, args);

            foreach (TipoDeStatusDeAtendimento tipo in resultado)
            {
                lista.Add(new KeyValuePair<int, string>(tipo.Id, tipo.Nome));
            }

            return lista;
        }

        public IEnumerable<KeyValuePair<int, string>> ListarStatusDeAtendimento(int idTipoStatus, bool? ativo)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_LISTAR";
            var args = new { IdTipoTipoStatus = idTipoStatus, Ativo = ativo };
            var lista = new List<KeyValuePair<int, string>>();
            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);

            foreach (StatusDeAtendimento tipo in resultado)
            {
                lista.Add(new KeyValuePair<int, string>(tipo.Id, tipo.Nome));
            }

            return lista;
        }

        public IEnumerable<StatusDeAtendimento> Listar(int? id, int? idTipoStatus, int? idCampanha, bool? ativo)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_LISTAR";
            var args = new
            {
                IdStatus = id,
                IdTipoTipoStatus = idTipoStatus,
                Ativo = ativo,
                IdCampanha = idCampanha
            };

            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);

            return resultado;
        }

        public IEnumerable<StatusDeAtendimento> ListarPorTipoCampanha(int? id, int? idTipoStatus, int? idCampanha, int? idTipoDeCampanha)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_POR_TIPO_CAMPANHA_LISTAR";
            var args = new
            {
                IdStatus = id,
                IdTipoTipoStatus = idTipoStatus,
                IdCampanha = idCampanha,
                IdTipoDeCampanha = idTipoDeCampanha
            };

            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);

            return resultado;
        }

        public int Gravar(StatusDeAtendimento statusDeAtendimento, string idsCampanhas)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_GRAVAR";
            var args = new
            {
                Id = statusDeAtendimento.Id,
                IdTipoDeStatusDeAtendimento = statusDeAtendimento.IdTipoDeStatusDeAtendimento,
                Nome = statusDeAtendimento.Nome,
                Ativo = statusDeAtendimento.Ativo,
                IdCriador = statusDeAtendimento.IdCriador,
                IdModificador = statusDeAtendimento.IdModificador,
                Observacao = statusDeAtendimento.Observacao,
                IdsCampanhas = idsCampanhas
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        public IEnumerable<StatusDeAtendimento> ListarTabulacaoAutomatica(bool? tabulacaoAutomatica)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_LISTAR";
            var args = new
            {
                TabulacaoAutomatica = tabulacaoAutomatica
            };

            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);

            return resultado;
        }

        public IEnumerable<CampanhaDoStatusDeAtendimento> ListarCampanhaDoStatus(int idCampanha, bool ativo)
        {
            var sql = "APP_CRM_BANCO_CAMPANHA_DO_STATUS_DE_ATENDIMENTO_LISTAR";
            var args = new { IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<CampanhaDoStatusDeAtendimento>(sql, args);
            return resultado;
        }

        public IEnumerable<TipoDeStatusDeAtendimento> ListarTipoDeStatusDeAtendimento(int id, bool ativo)
        {
            var sql = "APP_CRM_TIPO_STATUS_DE_ATENDIMENTO_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<TipoDeStatusDeAtendimento>(sql, args);
            return resultado;
        }

        public IEnumerable<StatusDeAtendimento> RetornarCampanhasSelecionadas(int idStatusDeAtendimento)
        {
            var sql = "APP_CRM_CAMPANHAS_DO_STATUS_DE_ATENDIMENTO_LISTAR";

            var args = new
            {
                IdStatusDeAtendimento = idStatusDeAtendimento
            };


            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);

            return resultado;
        }

        public DataTable ListaStatusDeAtendimento(int? idcampanha, bool? ativo, string nome, int? idStatus)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_LISTAR_EXIBICAO";

            sql += string.Format(" @idcampanha = {0}, @ativo= {1}, @nome = '{2}', @idStatus = {3}", idcampanha, ativo, nome, idStatus);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);
            return resultado;
        }

        public StatusDeAtendimento ListarTipoDeStatusDeAtendimento(int idStatus)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_LISTAR";
            var args = new { IdStatus = idStatus };
            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);
            return resultado.FirstOrDefault();
        }

        public IEnumerable<StatusDeAtendimento> Listar(int? id, bool? ativo)
        {
            var sql = "APP_CRM_STATUS_DE_ATENDIMENTO_LISTAR";

            var args = new
            {
                IdStatus = id,
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<StatusDeAtendimento>(sql, args);

            return resultado;
        }

        public IEnumerable<Canal> ListarCanal()
        {
            var sql = "APP_CRM_CANAL_LISTAR";
            var args = new
            {

            };

            var resultado = ExecutarProcedure<Canal>(sql, args);

            return resultado;
        }

        public IEnumerable<TipoContato> ListarTipoContato()
        {
            var sql = "APP_CRM_TIPO_CONTATO_LISTAR";
            var args = new
            {

            };

            var resultado = ExecutarProcedure<TipoContato>(sql, args);

            return resultado;
        }
    }
}
