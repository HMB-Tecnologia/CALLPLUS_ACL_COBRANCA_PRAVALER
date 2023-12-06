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
    public class TipoDeFraseDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        //public DataTable Listar()
        //{
        //    var sql = "APP_CRM_GAMIFICACAO_TIPO_DE_FRASES_LISTAR ";

        //    //sql += string.Format();

        //    var args = new
        //    {

        //    };

        //    var resultado = CarregarDataTable(sql, args);

        //    return resultado;
        //}
        public IEnumerable<TipoDeFrasesGamificacao> Listar()
        {
            var sql = "APP_CRM_GAMIFICACAO_TIPO_DE_FRASES_LISTAR";
            var args = new {};
            var resultado = ExecutarProcedure<TipoDeFrasesGamificacao>(sql, args);
            return resultado;
        }
    }
}
