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
		private TituloDao _tituloDao = new TituloDao();

        public List<Contrato> Listar(long id, bool baixado)
        {
			var sql = "EXEC APP_CRM_CONTRATOS_DO_CLIENTE_RETORNAR";
			sql += $" @IDProspect = {id}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			var contratos = new List<Contrato>();
			for (int i = 0; i < resultado.Rows.Count; i++)
			{
				var contrato = new Contrato();
				contrato.Id = long.Parse(DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Id"));
				contrato.Cpf = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Cpf");
				contrato.CodContrato = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "CodContrato");
				contrato.Valor = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Valor");
				contrato.Vencimento = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Vencimento");
				contrato.Campo01 = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Campo01");
				contrato.Campo02 = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Campo02");
				contrato.Campo03 = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Campo03");
				contrato.Campo04 = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Campo04");
				contrato.Campo05 = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Campo05");
				contrato.Campo06 = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Campo06");

				//var titulos = _tituloDao.RetornarTitulosDoContrato(contrato.Id, baixado);
				//contrato.Titulos = titulos;//RetornarTitulosDoContrato(contrato.IDContrato, baixado);

				contratos.Add(contrato);
			}
			return contratos;
		}

		public List<Titulo> RetornarTitulosDoContrato(long idTitulo, bool baixado)
		{
			var titulos = new List<Titulo>();

			var sql = "";
			sql += "EXEC APP_CRM_TITULOS_DO_CONTRATO_LISTAR";
			sql += $" @IDContrato = {idTitulo},";
			sql += $" @baixado = {(baixado ? 1 : 0)}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			for (int i = 0; i < resultado.Rows.Count; i++)
			{
				var titulo = new Titulo
				{
					IDTitulo = long.Parse(DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "IDTitulo")),
					NumeroDocumento = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "NumeroDocumento"),
					DataEmissao = DateTime.Parse(DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "DataLancamento")),
					DataVencimento = DateTime.Parse(DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "DataPagamento")),
					Montante = decimal.Parse(DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Montante")),
					AtribuicaoEspecial = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "AtribuicaoEspecial"),
					TipoDocumento = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "TipoDocumento"),
					FormaPagamento = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "FormaPagamento"),
					Status = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "Status")
				};

				var idNeg = DaoUtil.ObterValorDaColunaEmDataTable(resultado, index: i, nomeDaColuna: "IDNegociacao");
				if (string.IsNullOrEmpty(idNeg) == false)
					titulo.IDNegociacao = long.Parse(idNeg);

				titulos.Add(titulo);
			}
			return titulos;
		}

		public DataTable ListarExibicao(long id, bool baixado)
        {
			var sql = "EXEC APP_CRM_CONTRATOS_DO_CLIENTE_LISTAR_EXIBICAO";
			sql += $" @IDProspect = {id}";

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
