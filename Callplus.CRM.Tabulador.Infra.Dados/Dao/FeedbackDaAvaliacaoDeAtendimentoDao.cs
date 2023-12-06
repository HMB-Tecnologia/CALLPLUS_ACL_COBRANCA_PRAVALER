using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class FeedbackDaAvaliacaoDeAtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable Listar(long id, int idCampanha, DateTime dataInicial, DateTime dataFinal, int idStatus, int idAuditor)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_LISTAR_FEEDBACK_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @dataInicial = '{2}', @dataFinal = '{3}', @idStatus = '{4}', @idAuditor = '{5}'",
                id, idCampanha, dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd"), idStatus, idAuditor);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<FeedbackDaAvaliacaoDeAtendimento> Listar(int id, bool ativo)
        {
            var sql = "APP_CRM_FEEDBACK_AVALIACAO_ATENDIMENTO_LISTAR";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<FeedbackDaAvaliacaoDeAtendimento>(sql, args);
            return resultado;
        }

        public long Gravar(FeedbackDaAvaliacaoDeAtendimento item)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_GRAVAR_FEEDBACK";

            var args = new
            {
                IdAvaliacaoDeAtendimento = item.idAvaliacaoDeAtendimento,
                IdCriador = item.idCriador
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public void Excluir(long idAvaliacao)
        {
            var sql = "DELETE FeedbackDaAvaliacaoDeAtendimento WHERE idAvaliacaoDeAtendimento = " + idAvaliacao;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }
    }
}
