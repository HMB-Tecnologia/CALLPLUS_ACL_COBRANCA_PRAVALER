using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
	public class ContratoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Contrato> Listar(long id, bool baixado)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

            var args = new
            {
                Id = id,
				Baixado = baixado
			};

            var resultado = ExecutarProcedure<Contrato>(sql, args);

            return resultado;
        }

        public DataTable ListarExibicao(int id, int idCampanha, string nome, bool ativo)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR_EXIBICAO ";
            sql += string.Format("@id = {0}, @idCampanha = {1}, @nome = '{2}', @ativo = {3}",
            id, idCampanha, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public Contrato RetornarContrato(long id, bool baixado)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

            var args = new
            {
                Id = id,
                Baixado = baixado
            };

            var resultado = ExecutarProcedure<Contrato>(sql, args);

            return resultado.FirstOrDefault();
        }

        public int Gravar(Contrato id)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_GRAVAR";

            var args = new
            {

            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }
	}
}
