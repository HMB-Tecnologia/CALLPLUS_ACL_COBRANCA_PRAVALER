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

        public IEnumerable<Contrato> Listar(long idProspect, bool baixado)
        {
			var sql = "APP_CRM_CONTRATOS_DO_CLIENTE_RETORNAR";
			
			var args = new
			{
				IdProspect = idProspect,
				Baixado = baixado
			};

			var resultado = ExecutarProcedure<Contrato>(sql, args);

			return resultado;
		}

		public DataTable ListarExibicao(long id, bool baixado)
        {
			var sql = "EXEC APP_CRM_CONTRATOS_DO_CLIENTE_LISTAR_EXIBICAO";
			sql += $" @IDProspect = {id}";
			sql += $" ,@baixado = {baixado}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);
			return resultado;
		}

		public Contrato RetornarContrato(long id, bool baixado, long idContrato)
        {
            var sql = "APP_CRM_CONTRATOS_DO_CLIENTE_RETORNAR";

            var args = new
            {
                Id = id,
                Baixado = baixado,
                IdContrato = idContrato
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

		public void BaixarContratosDoAcordo(string idsContratos, long id)
		{
			var sql = "APP_CRM_CONTRATO_ATUALIZAR";

			var args = new
			{
				idsContratos,
				idProspect = id,
			};

			var resultado = ExecuteProcedureScalar(sql, args);
		}
	}
}
