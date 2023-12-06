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
    public class GamificacaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();
        public IEnumerable<Gamificacao> Listar(int id, int idCampanha, bool ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_LISTAR";
            var args = new { Id = id, IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<Gamificacao>(sql, args);
            return resultado;
        }
        public DataTable Listar(int id, int idCampanha, string titulo, bool ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @titulo = '{2}', @ativo = {3}", id, idCampanha, titulo, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable ListarCampanhasDaGamificacao(int idGamificacao, bool? ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_LISTAR_CAMPANHAS ";

            sql += string.Format("@IdGamificacao = {0}, @Ativo = {1}", idGamificacao, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable RetornarFraseInicial(int id, int idGamificacao, int idTipo, bool ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_RETORNAR_FRASES_INICIAL ";

            sql += string.Format("@id = {0}, @idGamificacao = {1}, @idTipo= {2}, @ativo = {3}", id, idGamificacao, idTipo, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable RetornarFraseFinal(int id, int idGamificacao, int idTipo, bool ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_RETORNAR_FRASES_FINAL ";

            sql += string.Format("@id = {0}, @idGamificacao = {1}, @idTipo= {2}, @ativo = {3}", id, idGamificacao, idTipo, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable RetornarFraseObjetivas(int id, int idGamificacao, int idTipo, bool ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_RETORNAR_FRASES_OBJETIVAS ";

            sql += string.Format("@id = {0}, @idGamificacao = {1}, @idTipo = {2}, @ativo = {3}", id, idGamificacao, idTipo, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public int GravarGamificacao(Gamificacao gamificacao)
        {
            var sql = "APP_CRM_GAMIFICACAO_GRAVAR";

            var args = new
            {
                Id = gamificacao.id,
                Titulo = gamificacao.titulo,
                IdCriador = (gamificacao.id == 0 ? gamificacao.idCriador : gamificacao.idModificador),
                Observacao = gamificacao.observacao,
                Ativo = gamificacao.ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }
        public int GravarFrase(FrasesGamificacao fraseMotivacional)
        {
            var sql = "APP_CRM_GAMIFICACAO_FRASE_GRAVAR";

            var args = new
            {
                Id = fraseMotivacional.id,
                IdTipo = fraseMotivacional.idTipo,
                IdGamificacao = fraseMotivacional.idGamificacao,
                Frase = fraseMotivacional.frase,
                IdCriador = (fraseMotivacional.id == 0 ? fraseMotivacional.idCriador : fraseMotivacional.idModificador),
                Ativo = fraseMotivacional.ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }
        public void ExcluirFrase(int idFraseGamificacao)
        {
            var sql = "DELETE FraseGamificacao WHERE id = " + idFraseGamificacao;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }
    }
}
