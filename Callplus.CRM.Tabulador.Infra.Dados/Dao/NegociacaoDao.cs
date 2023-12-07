using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
	public class NegociacaoDao : DaoBase
	{
		protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

		public IEnumerable<Negociacao> Listar(long id, bool baixado)
		{
			var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

			var args = new
			{
				Id = id,
				Baixado = baixado
			};

			var resultado = ExecutarProcedure<Negociacao>(sql, args);

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

		public Negociacao RetornarContrato(long id, bool baixado)
		{
			var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

			var args = new
			{
				Id = id,
				Baixado = baixado
			};

			var resultado = ExecutarProcedure<Negociacao>(sql, args);

			return resultado.FirstOrDefault();
		}

		public int Gravar(Negociacao id)
		{
			var sql = "APP_CRM_DADOS_CEP_EXPRESS_GRAVAR";

			var args = new
			{

			};

			var resultado = ExecuteProcedureScalar(sql, args);

			return Convert.ToInt32(resultado);
		}

		public long IncluirNegociacao(Negociacao negociacao)
		{
			var sql = string.Empty;
			sql += "EXEC SP_INCLUIR_ACORDO";
			sql += $" @IdPrazo = {negociacao.IdPrazo},";
			sql += $" @IdTipoAcordo = {negociacao.IdTipoAcordo},";
			sql += $" @IdUsuario = {negociacao.IdUsuario},";
			sql += $" @NumeroNegociacao = '{negociacao.NumeroNegociacao}',";
			sql += $" @QuantidadeParcela = {negociacao.QuantidadeDeParcela},";
			sql += $" @DataVencimento = '{negociacao.DataVencimento.ToString("MM/dd/yyyy")}',";
			sql += $" @ValorDaParcelas = '{negociacao.ValorDasParcelas.ToString().Replace(",", ".")}',";
			sql += $" @ValorPrincipal = '{negociacao.ValorPrincipal.ToString().Replace(",", ".")}',";
			sql += $" @Juros = '{negociacao.Juros.ToString().Replace(",", ".")}',";
			sql += $" @Multa = '{negociacao.Multa.ToString().Replace(",", ".")}'";

			var args = new { };

			var resultado = CarregarDataTable(sql, args);
			var idNegociacao = long.Parse(DaoUtil.ObterValorDaColunaEmDataTable(resultado, 0, "IdAcordo"));

			return idNegociacao;
		}

		public void IncluirTituloNegociacao(TituloNegociacao tituloNegociacao)
		{
			var sql = "EXEC SP_INCLUIR_TITULO_ACORDO ";
			sql += $" @IDTitulo = {tituloNegociacao.IDTitulo},";
			sql += $" @IDAcordo = {tituloNegociacao.IDNegociacao}";

			var args = new
			{

			};

			var resultado = ExecuteProcedureScalar(sql, args);
		}

		public void IncluirParcelaNegociacao(Parcela parcela, long idNegociacao, int idUsuario)
		{
			var sql = "EXEC SP_INCLUIR_PARCELA_ACORDO ";
			sql += $" @IdAcordo = {idNegociacao},";
			sql += $" @IdUsuario = {idUsuario},";
			sql += $" @NumeroDaParcela = {parcela.NumeroDaParcela},";
			sql += $" @DataVencimento = '{parcela.DataVencimento.ToString("MM/dd/yyyy")}',";
			sql += $" @ValorDaParcelas = '{parcela.ValorDaParcela.ToString().Replace(",", ".")}',";
			sql += $" @ValorPrincipal = '{parcela.ValorPrincipal.ToString().Replace(",", ".")}',";
			sql += $" @Juros = '{parcela.Juros.ToString().Replace(",", ".")}',";
			sql += $" @Multa = '{parcela.Multa.ToString().Replace(",", ".")}'";

			var args = new
			{

			};

			var resultado = ExecuteProcedureScalar(sql, args);
		}

		public DataTable RetornarHistoricoNegociacaoPorIdContrato(long idContrato)
		{
			var sql = $"EXEC SP_RETORNAR_HISTORICO_ACORDO @IdContrato = {idContrato}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			return resultado;
		}

		public IEnumerable<string> PodeIncluirNegociacao(long idContrato, int idUsuario)
		{
			var sql = "EXEC SP_PODE_INCLUIR_NEGOCIACAO";
			sql += $" @IDContrato = {idContrato},";
			sql += $" @IDUsuario = {idUsuario}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			List<string> mensagens = new List<string>();
			for (int i = 0; i < resultado.Rows.Count; i++)
			{
				var mensagem = DaoUtil.ObterValorDaColunaEmDataTable(resultado, i, "Mensagem");
				mensagens.Add(mensagem);
			}

			return mensagens;
		}

		public DataTable RetornarTipoAcordo(bool ativo)
		{
			var sql = $" EXEC SP_RETORNAR_TIPO_ACORDO @Ativo = {ativo}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			return resultado;
		}

		public DataTable RetornarPrazoNegociacao(bool ativo)
		{
			var sql = $" EXEC SP_RETORNAR_PRAZO_NEGOCIACAO @Ativo = {ativo}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			return resultado;
		}

		public bool VerificarSeExisteAcordo(long iDTitulo)
		{
			var sql = $" EXEC SP_VERIFICAR_SE_EXISTE_ACORDO_TITULO @IdTitulo = {iDTitulo}";

			var args = new
			{

			};

			var resultado = CarregarDataTable(sql, args);

			return resultado.Rows.Count > 0;
		}
	}
}
