using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using Callplus.CRM.Tabulador.Dominio.Dto;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ChecklistDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable Listar(int id, int idCampanha, int idProduto, string nome, bool ativo)
        {
            var sql = "APP_CRM_CHECKLIST_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @idProduto = {2}, @nome = '{3}', @ativo = {4}", id, idCampanha, idProduto, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Checklist> Listar(int id, int idCampanha, int idProduto, int ddd, bool ativo)
        {
            var sql = "APP_CRM_CHECKLIST_LISTAR";
            var args = new { Id = id, IdCampanha = idCampanha, IdProduto = idProduto, Ddd = ddd, Ativo = ativo };
            var resultado = ExecutarProcedure<Checklist>(sql, args);
            return resultado;
        }

        public IEnumerable<EtapaDoChecklist> ListarEtapas(int id, int idChecklist, bool ativo)
        {
            var sql = "APP_CRM_CHECKLIST_LISTAR_ETAPA";
            var args = new { Id = id, IdChecklist = idChecklist, Ativo = ativo };
            var resultado = ExecutarProcedure<EtapaDoChecklist>(sql, args);
            return resultado;
        }

        public IEnumerable<VariavelDoChecklist> ListarVariaveis(int idTipoDeProduto, int idCampanha)
        {
            var sql = "APP_CRM_CHECKLIST_LISTAR_VARIAVEL";
            var args = new { IdTipoDeProduto = idTipoDeProduto, IdCampanha = idCampanha };
            var resultado = ExecutarProcedure<VariavelDoChecklist>(sql, args);
            return resultado;
        }

        public IEnumerable<RegionalClaro> ListarRegionais()
        {
            var sql = "APP_CRM_REGIONAL_CLARO_LISTAR";
            var args = new {  };
            var resultado = ExecutarProcedure<RegionalClaro>(sql, args);
            return resultado;
        }

        public int GravarChecklist(Checklist checklist)
        {
            var sql = "APP_CRM_CHECKLIST_GRAVAR";

            var args = new
            {
                Id = checklist.id,
                Nome = checklist.nome,
                Titulo = checklist.titulo,
                Ativo = checklist.ativo,
                PalavraChave = checklist.palavraChaveMailing,
                Regionais = checklist.regionais,
                IdUsuario = (checklist.id == 0 ? checklist.idCriador : checklist.idModificador),
                Observacao = checklist.observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public int GravarEtapaDoChecklist(EtapaDoChecklist etapa)
        {
            var sql = "APP_CRM_CHECKLIST_GRAVAR_ETAPA";

            var args = new
            {
                Id = etapa.id,
                IdChecklist = etapa.idChecklist,
                Etapa = etapa.etapa,
                Ativo = etapa.ativo,
                Descricao = etapa.descricaoRtf,
                IdUsuario = (etapa.id == 0 ? etapa.idCriador : etapa.idModificador)
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public void GravarProdutosDoChecklist(ProdutoDoChecklistDto produto)
        {
            var sql = "APP_CRM_CHECKLIST_GRAVAR_PRODUTO_VINCULADO";

            var args = new
            {
                IdChecklist = produto.IdChecklist,
                Produtos = produto.Produtos,
                IdCampanha = produto.IdCampanha
            };

            ExecutarProcedure(sql, args);
        }

        public void ExcluirEtapaDoChecklist(int idEtapa)
        {
            var sql = "DELETE EtapaDoChecklist WHERE id = " + idEtapa;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }

        public DataTable ListarProdutosDoChecklistPorCampanha(int idChecklist)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_DO_CHECKLIST_POR_CAMPANHA ";
            sql += string.Format("@idChecklist = {0}", idChecklist);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Produto> ListarProdutosDoChecklist(int idChecklist, int idCampanha)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_DO_CHECKLIST";

            var args = new
            {
                IdChecklist = idChecklist,
                IdCampanha = idCampanha
            };

            var resultado = ExecutarProcedure<Produto>(sql, args);

            return resultado;
        }
    }
}
