using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
	public class TituloDao : DaoBase
	{
		protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

		public DataTable Listar(long id, bool ativo)
		{
			var sql = " EXEC SP_RETORNAR_STATUS_TITULO ";
			sql += $" @IDStatus = {id}";
			var args = new
			{
				Id = id
			};

			var resultado = CarregarDataTable(sql, args);

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

		public Titulo RetornarTitulo(int id)
		{
			var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

			var args = new
			{
				Id = id
			};

			var resultado = ExecutarProcedure<Titulo>(sql, args);

			return resultado.FirstOrDefault();
		}

		public void Gravar(long idProspect, Titulo titulo)
		{

			var sql = " EXEC APP_CRM_NOVO_TITULO_SALVAR ";
			sql += $" @NumeroDocumento = '{titulo.NumeroDocumento}',";
			sql += $" @DataEmissao = '{titulo.DataEmissao:MM/dd/yyyy}',";
			sql += $" @DataVencimento = '{titulo.DataVencimento:MM/dd/yyyy}',";
			sql += $" @AtribuicaoEspecial = '{titulo.AtribuicaoEspecial}',";
			sql += $" @TipoDocumento = '{titulo.TipoDocumento}',";
			sql += $" @FormaPagamento = '{titulo.FormaPagamento}',";
			sql += $" @Montante = '{titulo.Montante.ToString().Replace(",", ".")}',";
			sql += $" @IdProspect = {idProspect}";

			var args = new
			{

			};

			ExecutarSql(sql, args);
		}

		public void AtualizarStatusDoTitulo(MarcacaoStatusTitulo marcacao)
		{
			var sql = "EXEC SP_ATUALIZAR_STATUS_DO_TITULO_V3";

			sql += $" @IDTitulo = {marcacao.IDtitulo},";
			sql += $" @IDAtendimento = {marcacao.IDAtendimento},";
			sql += $" @IDStatusTitulo = {marcacao.IDStatusTitulo},";
			sql += $" @IDUsuario = {marcacao.IDUsuario},";
			sql += $" @NumeroNegociacao = '{marcacao.NumeroNegociacao}',";
			sql += $" @ValorBoleto = '{marcacao.ValorBoleto.ToString().Replace(",", ".")}',";
			sql += $" @ValorAtualizado = '{marcacao.ValorAtualizado.ToString().Replace(",", ".")}',";
			sql += $" @QuantidadeParcela = {marcacao.QuantidadeParcela},";
			sql += $" @ValorParcelas ='{marcacao.ValorParcelas.ToString().Replace(",", ".")}',";
			sql += $" @DataNegociacaoFutura = '{marcacao.DataNegociacaoFutura.ToString("MM/dd/yyyy")}',";
			sql += $" @DataVencimento = '{marcacao.DataVencimento.ToString("MM/dd/yyyy")}',";
			sql += $" @DataVencimentoAtualizado = '{marcacao.DataVencimentoAtualizado.ToString("MM/dd/yyyy")}'";

			var args = new
			{

			};

			ExecutarSql(sql, args);
		}

		public List<string> PodeAtualizarStatusDoTitulo(long idTitulo, int idStatusTitulo, long idStatusAtendimento, int idUsuario, DateTime dataVencimento, DateTime dataNegociacaoFutura, DateTime dataVencimentoAtualizada)
		{
			var sql = "EXEC [SP_PODE_ATUALIZAR_STATUS_TITULO_V3]";
			sql += $" @IDTitulo = {idTitulo},";
			sql += $" @IDStatusTitulo = {idStatusTitulo},";
			sql += $" @IDStatus = {idStatusAtendimento},";
			sql += $" @DataVencimento = '{dataVencimento.ToString("MM/dd/yyyy")}',";
			sql += $" @DataNegociacaoFutura ='{dataNegociacaoFutura.ToString("MM/dd/yyyy")}',";
			sql += $" @DataVencimentoAtualizada ='{dataVencimentoAtualizada.ToString("MM/dd/yyyy")}',";
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
	}
}
