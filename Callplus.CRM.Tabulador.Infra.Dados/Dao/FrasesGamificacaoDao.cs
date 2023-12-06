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
    public class FrasesGamificacaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();
        public IEnumerable<FrasesGamificacao> Listar( int idGamificacao, bool ativo)
        {
            var sql = "APP_CRM_FRASES_GAMIFICACAO_LISTAR";
            var args = new { IdGamificacao = idGamificacao, Ativo = ativo };
            var resultado = ExecutarProcedure<FrasesGamificacao>(sql, args);

            return resultado;
        }
        public DataTable RetornarFrases(int idGamificacao, int idTipo, bool ativo)
        {
            var sql = "APP_CRM_GAMIFICACAO_RETORNAR_FRASES ";

            sql += string.Format("@idGamificacao = {0}, @idTipo = {1}, @ativo = {2}", idGamificacao, idTipo, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public IEnumerable<TipoDeFrasesGamificacao> ListarTipo()
        {
            var sql = "APP_CRM_GAMIFICACAO_TIPO_DE_FRASES_LISTAR";
            var args = new { };
            var resultado = ExecutarProcedure<TipoDeFrasesGamificacao>(sql, args);
            return resultado;
        }
    }
}
