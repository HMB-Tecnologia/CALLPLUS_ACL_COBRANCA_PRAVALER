using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class AvaliacaoDeAtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable Listar(long id, int idCampanha, DateTime dataInicial, DateTime dataFinal, int idPerfil, int idAvaliador)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @dataInicial = '{2}', @dataFinal = '{3}', @idPerfil = '{4}', @idAvaliador = '{5}'", 
                id, idCampanha, dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd 23:59:59"), idPerfil, idAvaliador);

            var args = new
            {
                    
            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<AvaliacaoDeAtendimento> Listar(long id, bool ativo)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_LISTAR";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<AvaliacaoDeAtendimento>(sql, args);
            return resultado;
        }

        public IEnumerable<TipoDeAvaliacaoDeAtendimento> ListarTipo(bool ativo)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_LISTAR_TIPO";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<TipoDeAvaliacaoDeAtendimento>(sql, args);
            return resultado;
        }

        public DataTable ListarRespostaDaAvaliacao(long idAvaliacao)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_LISTAR_RESPOSTA ";

            sql += string.Format("@idAvaliacao = {0}", idAvaliacao);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable ListarDadosNotificacao(int idOperador)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_LISTAR_DADOS_NOTIFICACAO ";

            sql += string.Format("@idOperador = {0}", idOperador);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public AvaliacaoDeAtendimento RetornarOfertaMigracao(long idOferta)
        {
            var sql = "APP_CRM_AVALIACAO_DE_ATENDIMENTO_RETORNAR_OFERTA_MIGRACAO";
            var args = new { IdOferta = idOferta };
            var resultado = ExecutarProcedureSingleOrDefault<AvaliacaoDeAtendimento>(sql, args);
            return resultado;
        }

        public AvaliacaoDeAtendimento RetornarOfertaPortabilidade(long idOferta)
        {
            var sql = "APP_CRM_AVALIACAO_DE_ATENDIMENTO_RETORNAR_OFERTA_PORTABILIDADE";
            var args = new { IdOferta = idOferta };
            var resultado = ExecutarProcedureSingleOrDefault<AvaliacaoDeAtendimento>(sql, args);
            return resultado;
        }

        public int Gravar(AvaliacaoDeAtendimento item, string procedimentosOK, string procedimentosNOK, string procedimentosNA)
        {
            var sql = "APP_CRM_AVALIACAO_ATENDIMENTO_GRAVAR";

            var args = new
            {
                Id = item.id,
                IdFormulario = item.idFormularioDeQualidade,
                IdAtendimento = item.idAtendimento,
                IdAvaliador = item.idCriador,
                IdTipo = item.idTipoDeAvaliacaoDeAtendimento,
                Pontuacao = item.pontuacao,
                ProcedimentoOK = procedimentosOK,
                ProcedimentoNOK = procedimentosNOK,
                ProcedimentoNA = procedimentosNA,
                Observacao = item.observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }
    }
}
