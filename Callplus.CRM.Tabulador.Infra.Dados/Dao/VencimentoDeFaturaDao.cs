using Callplus.CRM.Tabulador.Infra.Dados.Util;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class VencimentoDeFaturaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<CicloDeVencimentoDeFatura> ListarCicloDeVencimentoDeFatura(int id, bool ativo)
        {
            var sql = "APP_CRM_CICLO_VENCIMENTO_FATURA_LISTAR";
            var args = new
            {
                Id = id,
                Ativo = ativo
            };
            var resultado = ExecutarProcedure<CicloDeVencimentoDeFatura>(sql, args);
            return resultado;
        }

        public IEnumerable<ConfiguracaoDeVencimentoDeFatura> ListarConfiguracaoDeVencimentoDeFatura(int id, bool ativo)
        {
            var sql = "APP_CRM_CONFIGURACAO_VENCIMENTO_FATURA_LISTAR";
            var args = new
            {
                Id = id,
                Ativo = ativo
            };
            var resultado = ExecutarProcedure<ConfiguracaoDeVencimentoDeFatura>(sql, args);
            return resultado;
        }

        public DataTable ListarExibicao(int id, int idDiaDeAtivacao, bool ativo)
        {
            var sql = "APP_CRM_VENCIMENTO_FATURA_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @DiaDeAtivacao = {1}, @ativo = {2}", id, idDiaDeAtivacao, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public int GravarCicloDeVencimento(CicloDeVencimentoDeFatura cicloDeVencimentoDeFatura)
        {
            var sql = "APP_CRM_CICLO_DE_VENCIMENTO_GRAVAR";

            var args = new
            {
                Id = cicloDeVencimentoDeFatura.Id,
                Ativo = cicloDeVencimentoDeFatura.Ativo,
                DataCriacao = cicloDeVencimentoDeFatura.DataCriacao,
                DataModificacao = cicloDeVencimentoDeFatura.DataModificacao,
                Fechamento = cicloDeVencimentoDeFatura.Fechamento,
                IdCriador = cicloDeVencimentoDeFatura.IdCriador,
                IdModificador = cicloDeVencimentoDeFatura.IdModificador,
                Vencimento = cicloDeVencimentoDeFatura.Vencimento

            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public IEnumerable<VencimentoFatura> Listar(int id, int ativo = 0)
        {
            var sql = "APP_CRM_VENCIMENTO_FATURA_LISTAR";
            var args = new
            {
                Id = id,
                Ativo = ativo
            };
            var resultado = ExecutarProcedure<VencimentoFatura>(sql, args);
            return resultado;
        }

        public int GravarVencimentoDeFatura(VencimentoFatura vencimentoFatura)
        {
            var sql = "APP_CRM_VENCIMENTO_FATURA_GRAVAR";

            var args = new
            {
                Id = vencimentoFatura.Id,
                Dia = vencimentoFatura.Dia,
                Ordem = vencimentoFatura.Ordem, 
                IdCiclo = vencimentoFatura.IdCiclo,
                IdCriador = vencimentoFatura.IdCriador,
                IdModificador = vencimentoFatura.IdModificador,
                Ativo = vencimentoFatura.Ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public DataTable ListarVencimentos(int id, int idDiaAtivacao, bool ativo)
        {
            var sql = "APP_CRM_VENCIMENTO_FATURA_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idDiaAtivacao = {1}, @ativo = {2}", id, idDiaAtivacao, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
    }
}
