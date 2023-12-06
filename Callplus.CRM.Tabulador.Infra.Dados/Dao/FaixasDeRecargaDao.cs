using System.Data;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class FaixasDeRecargaDao : DaoBase
    {

        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<FaixaDeRecarga> ListarFaixasDeRecarga(int? id, bool? ativo)
        {
            var sql = "APP_CRM_FAIXA_DE_RECARGA_LISTAR";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<FaixaDeRecarga>(sql, args);

            return resultado;

        }

        public IEnumerable<ProdutoPermitidoParaFaixaDeRecarga> ListarFaixasDeRecargaDoProduto(long idProduto)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_FAIXAS_DE_RECARGA_DO_PRODUTO";
            var args = new { IdProduto = idProduto };
            var resultado = ExecutarProcedure<ProdutoPermitidoParaFaixaDeRecarga>(sql, args);
            return resultado;
        }

        public IEnumerable<FaixaDeRecarga> Listar(int id)
        {
            var sql = "APP_CRM_FAIXA_DE_RECARGA_LISTAR";
            var args = new { Id = id };
            var resultado = ExecutarProcedure<FaixaDeRecarga>(sql, args);

            return resultado;
        }

        public int Salvar(ProdutoPermitidoParaFaixaDeRecarga produtoPermitidoParaFaixaDeRecarga)
        {
            var sql = "APP_CRM_PRODUTO_PERMITIDO_PARA_FAIXA_DE_RECARGA_GRAVAR";

            var args = new
            {
                id = produtoPermitidoParaFaixaDeRecarga.Id,
                idFaixaDeRecarga = produtoPermitidoParaFaixaDeRecarga.IdFaixaDeRecarga,
                idProduto = produtoPermitidoParaFaixaDeRecarga.IdProduto,
                ativo = produtoPermitidoParaFaixaDeRecarga.Ativo,

            };


            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }

        public string GravarFaixaDeRecarga(FaixaDeRecarga faixa)
        {
            var sql = "APP_CRM_FAIXA_DE_RECARGA_GRAVAR";
            var args = new
            {
                Id = faixa.Id,
                Nome = faixa.Nome,
                Ativo = faixa.Ativo,
            };

            var resultado = ExecuteProcedureScalar(sql, args);
            return Convert.ToString(resultado);
        }

        public DataTable ListarFaixasDeRecargaExistentes(int? id, string nome, bool? ativo)
        {
            var sql = "APP_CRM_FAIXA_DE_RECARGA_LISTAR_EXIBICAO";

            sql += string.Format(" @id = {0}, @nome = '{1}', @ativo = {2}", id, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
    }
}
