using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class LayoutDinamicoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public List<ValorDeCampoDinamico> ListarValoresDeCamposDinamicos(long idProspect, int idCampanha)
        {
            string sql = $"APP_CRM_LAYOUT_DINAMICO_LISTAR_VALORES_CAMPOS_DINAMICOS @IDProspect = {idProspect}, @IDCampanha = {idCampanha} ";

            List<ValorDeCampoDinamico> retorno = new List<ValorDeCampoDinamico>();
            var resultado = CarregarDataTable(sql, new { });

            foreach (DataRow resultadoRow in resultado.Rows)
            {
                foreach (DataColumn col in resultadoRow.Table.Columns)
                {
                    var nome = col.ColumnName;
                    var conteudo = DaoUtil.ObterValorDaColunaEmDataTable(resultado, 0, col.ColumnName);
                    retorno.Add(new ValorDeCampoDinamico(nome,conteudo));
                }
                break;
            }
            return retorno;
        }

        public LayoutDeCampoDinamico RetornarLayoutDinamico(int idLayoutDinamico)
        {
            string sqlLayout = $"APP_CRM_LAYOUT_DINAMICO_LISTAR";
            bool? ativo = null;
            var argsLayout = new {id = idLayoutDinamico,nome = "",ativo = ativo };
            var layout = ExecutarProcedureSingleOrDefault<LayoutDeCampoDinamico>(sqlLayout, argsLayout);


            string sql = $"APP_CRM_ATENDIMENTO_LISTAR_CAMPOS_DINAMICOS_DO_LAYOUT";
            var args = new { IDLayout = idLayoutDinamico };
            var resultado = ExecutarProcedure<CampoDinamico>(sql, args);
            var grupos = resultado.GroupBy(x => x.Linha).Select(x => x.Key);

            foreach (int linhaGrp in grupos)
            {
                var linha = new LinhaDeCampoDinamico();
                linha.Campos = resultado.Where(x => x.Linha == linhaGrp).Select(x => x).ToList();
                layout.Linhas.Add(linha);
            }
            return layout;
        }

        public DataTable Listar(int id,string nome, bool? ativo)
        {
            string sql = $"APP_CRM_LAYOUT_DINAMICO_LISTAR_EXIBICAO";
            sql += $" @id = {id},";
            sql += $" @nome = '{nome}',";
            sql += $" @ativo = {ativo}";

            var args = new { };
            DataTable datatable = CarregarDataTable(sql, args);
            return datatable;
        }

        public IEnumerable<LayoutDeCampoDinamico> Listar(int id, bool ativo)
        {
            var sql = "APP_CRM_LAYOUT_DINAMICO_LISTAR";

            var args = new
            {
                Id = id,
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<LayoutDeCampoDinamico>(sql, args);

            return resultado;
        }
    }
}
