using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ProdutoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable Listar(int id, int idCampanha, int idTipoDeProduto, string nome, bool ativo)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @idTipoDeProduto = {2}, @nome = '{3}', @ativo = {4}", id, idCampanha, idTipoDeProduto, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Produto> Listar(int id, int idCampanha, int idTipoDeProduto, bool ativo)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR";
            var args = new { Id = id, IdCampanha = idCampanha, IdTipoDeProduto = idTipoDeProduto, Ativo = ativo };

            var resultado = ExecutarProcedure<Produto>(sql, args);
            return resultado;
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOferta(long idAtendimento, bool? ativo, bool? ativoBko)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_OFERTA";
            var args = new
            {
                IdAtendimento = idAtendimento,
                Ativo = ativo,
                AtivoBko = ativoBko
            };

            var resultado = ExecutarProcedure<ProdutoDaOfertaDto>(sql, args);
            return resultado;
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOfertaPorFaixaDeRecarga(long idAtendimento)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_OFERTA_POR_FAIXA_RECARGA";
            var args = new { IdAtendimento = idAtendimento };

            var resultado = ExecutarProcedure<ProdutoDaOfertaDto>(sql, args);
            return resultado;
        }

        public IEnumerable<Produto> ListarProdutoDaOfertaPorFaixaDeRecargaBKO(long idProspect)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_OFERTA_POR_FAIXA_RECARGA_BKO";
            var args = new { IdProspect = idProspect };

            var resultado = ExecutarProcedure<Produto>(sql, args);
            return resultado;
        }
        
        public IEnumerable<Produto> ListarProdutosDaCampanha(int idCampanha)
        {
            var sql = "APP_CRM_PRODUTOS_DA_CAMPANHA_LISTAR";
            var args = new { IdCampanha = idCampanha };

            var resultado = ExecutarProcedure<Produto>(sql, args);
            return resultado;
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOfertaPorNome(long idCampanha, string nome, bool? ativo, bool? ativoBko)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_OFERTA_POR_NOME";
            var args =
            new
            {
                IdCampanha = idCampanha,
                Nome = nome,
                Ativo = ativo,
                AtivoBko = ativoBko

            };

            var resultado = ExecutarProcedure<ProdutoDaOfertaDto>(sql, args);
            return resultado;
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOfertaPorIdProspect(long idCampanha, long idProspect, bool? ativo, bool? ativoBko)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_OFERTA_POR_IDPROSPECT";
            var args =
            new
            {

                IdCampanha = idCampanha,
                IdProspect = idProspect,
                Ativo = ativo,
                AtivoBko = ativoBko

            };

            var resultado = ExecutarProcedure<ProdutoDaOfertaDto>(sql, args);
            return resultado;
        }

        public IEnumerable<TipoDeProduto> ListarTipoDeProduto(bool ativo)
        {
            var sql = "APP_CRM_TIPO_DE_PRODUTO_LISTAR";
            var args = new { Ativo = ativo };

            var resultado = ExecutarProcedure<TipoDeProduto>(sql, args);

            return resultado;
        }

        public IEnumerable<FormaDePagamentoDeAparelho> ListarFormaDePagamentoDeAparelho(int idProduto, int idAparelho)
        {
            var sql = "APP_CRM_TIPO_DE_PRODUTO_LISTAR";
            var args = new { idProduto = idProduto, idAparelho = idAparelho };

            var resultado = ExecutarProcedure<FormaDePagamentoDeAparelho>(sql, args);

            return resultado;
        }

        public int Gravar(Produto produto)
        {
            var sql = "APP_CRM_PRODUTO_GRAVAR";

            var args = new
            {
                id = produto.Id,
                ativo = produto.Ativo,
                ativoBko = produto.AtivoBko,
                idCampanha = produto.IdCampanha,
                idtipoDeProduto = produto.IdTipoDeProduto,
                idScriptOferta = produto.IdScriptOferta,
                nome = produto.Nome,
                observacao = produto.Observacao,
                ordem = produto.Ordem,
                valor = produto.Valor,
                idModificador = produto.IdModificador,
                idCriador = produto.Idcriador
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }
    }
}
