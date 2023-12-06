using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class AparelhoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<FormaDePagamentoDeAparelho> ListarFormaDePagamentoDeAparelho(int idProduto, int idAparelho)
        {
            var sql = "APP_CRM_FORMA_DE_PAGAMENTO_DE_APARELHO_LISTAR";
            var args = new { idProduto = idProduto, idAparelho = idAparelho };

            var resultado = ExecutarProcedure<FormaDePagamentoDeAparelho>(sql, args);

            return resultado;
        }

        public IEnumerable<Aparelho> Listar(int idAparelho)
        {
            var sql = "APP_CRM_APARELHO_LISTAR";
            var args = new { idAparelho = idAparelho};

            var resultado = ExecutarProcedure<Aparelho>(sql, args);

            return resultado;
        }

        public DataTable ListarExibicao(int id, int idCampanha, string nome, string grupo, bool ativo)
        {
            var sql = "APP_CRM_APARELHO_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @nome = '{2}', @grupo = '{3}', @ativo = {4}", id, idCampanha, nome, grupo, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Aparelho> ListarAparelhosDoAtendimento(long idProspect)
        {
            var sql = "APP_CRM_APARELHO_LISTAR_APARELHOS_DO_ATENDIMENTO";
            var args = new { idProspect = idProspect};

            var resultado = ExecutarProcedure<Aparelho>(sql, args);

            return resultado;
        }

        public IEnumerable<FormaDePagamentoDeAparelho> ListarPagamentosDoAparelho(int idAparelho, int idProduto)
        {
            var sql = "APP_CRM_APARELHO_PAGAMENTO_LISTAR_EXIBICAO";
            var args = new { IdAparelho = idAparelho, idProduto = idProduto };

            var resultado = ExecutarProcedure<FormaDePagamentoDeAparelho>(sql, args);

            return resultado;
        }

        public IEnumerable<AparelhoDaCampanha> CarregarCampanhaDoAparelho(int? id)
        {
            {
                var sql = "APP_CRM_CAMPANHA_DO_APARELHO_LISTAR";

                var args = new
                {
                    Id = id ?? -1,
                };

                var resultado = ExecutarProcedure<AparelhoDaCampanha>(sql, args);


                return resultado;
            }
        }

        public int GravarAparelho(Aparelho aparelho)
        {
            {
                var sql = "APP_CRM_APARELHO_GRAVAR";

                var args = new
                {
                    Id = aparelho.Id,
                    Nome = aparelho.Nome,
                    Grupo = aparelho.Grupo,
                    Ativo = aparelho.Ativo,
                };

                var resultado = ExecuteProcedureScalar(sql, args);

                return Convert.ToInt32(resultado);
            }
        }

        public int GravarFormaDePagamentoDoAparelho(FormaDePagamentoDeAparelho formaDePagamentoDoAparelho)
        {
            {
                var sql = "APP_CRM_APARELHO_FORMA_DE_PAGAMENTO_GRAVAR";

                var args = new
                {
                    Id = formaDePagamentoDoAparelho.Id,
                    IdAparelho = formaDePagamentoDoAparelho.IdAparelho,
                    IdProduto = formaDePagamentoDoAparelho.IdProduto,
                    Descricao = formaDePagamentoDoAparelho.Descricao,
                    Valor = formaDePagamentoDoAparelho.Valor,
                    Ativo = formaDePagamentoDoAparelho.Ativo,

                };

                var resultado = ExecuteProcedureScalar(sql, args);

                return Convert.ToInt32(resultado);
            }
        }

        public int GravarCampanhaDoAparelho(AparelhoDaCampanha campanhaDoAparelho)
        {
            var sql = "APP_CRM_APARELHO_DA_CAMPANHA_GRAVAR";

            var args = new
            {
                Id = campanhaDoAparelho.Id,
                IdCampanha = campanhaDoAparelho.IdCampanha,
                IdAparelho = campanhaDoAparelho.IdAparelho,
                Ativo = campanhaDoAparelho.Ativo,
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }

        public IEnumerable<FormaDePagamentoDeAparelho> CarregarFormaDePagamentoDoAparelho(int? id)
        {
            {
                var sql = "APP_CRM_FORMA_DE_PAGAMENTO_DO_APARELHO_LISTAR";

                var args = new
                {
                    Id = id ?? -1,
                };

                var resultado = ExecutarProcedure<FormaDePagamentoDeAparelho>(sql, args);


                return resultado;
            }
        }

        public IEnumerable<AparelhoDaCampanha> ListarAparelhosDaCampanha(long idAparelho)
        {
            var sql = "APP_CRM_CAMPANHAS_DO_APARELHO_LISTAR_EXIBICAO";
            var args = new { IdAparelho = idAparelho};

            var resultado = ExecutarProcedure<AparelhoDaCampanha>(sql, args);

            return resultado;
        }

      


    }

}
