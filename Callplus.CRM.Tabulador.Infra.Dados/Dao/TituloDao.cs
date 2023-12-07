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

        public IEnumerable<Titulo> Listar(long id, bool ativo)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

            var args = new
            {
                Id = id,   
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<Titulo>(sql, args);

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

        public int Gravar(long idProspect, List<Titulo> listaTitulos)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_GRAVAR";

            var args = new
            {

            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
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

			ExecutarProcedure<Titulo>(sql, args);
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
	}
}
