using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class NivelDeConfiancaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();


        public int ArquivoGravar(string titulo, int idAtualizador)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_GRAVAR_ARQUIVO ";
            sql += string.Format("@titulo = '{0}', @idAtualizador = {1}",
            titulo, idAtualizador);

            var args = new
            {
                Titulo = titulo,
                IdAtualizador = idAtualizador
            };

            var resultado = ExecuteScalar(sql, args);
            return Convert.ToInt32(resultado);
        }
        public int Gravar(int idArquivo, int idAgente, decimal nota, int idAtualizador)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_GRAVAR ";
            sql += string.Format("@idArquivo = {0}, @idAgente = '{1}', @nota = '{2}', @idAtualizador = {3}",
            idArquivo, idAgente,nota, idAtualizador);

            var args = new
            {
                IdArquivo = idArquivo,
                IdAgente = idAgente,
                Nota = nota,
                IdAtualizador = idAtualizador
            };

            var resultado = ExecuteScalar(sql, args);
            return Convert.ToInt32(resultado);
        }
        public int Editar(int id, decimal nota, int idAgente, int idAtualizador)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_EDITAR ";
            sql += string.Format("@id = {0}, @nota = '{1}', @idAgente = {2}, @idAtualizador = {3}",
            id, nota, idAgente, idAtualizador);

            var args = new
            {
                Id = id,
                Nota = nota,
                Agente = idAgente,
                IdAtualizador = idAtualizador
            };

            var resultado = ExecuteScalar(sql, args);
            return Convert.ToInt32(resultado);
        }
        public DataTable Listar(long id, int idCampanha, DateTime dataInicial, DateTime dataFinal, int idAgente, int idAvaliador, decimal notaMinima, decimal notaMaxima)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @dataInicial = '{2}', @dataFinal = '{3}', @idAgente = '{4}', @idAvaliador = '{5}', @notaMinima = '{6}', @notaMaxima = '{7}'",
                id, idCampanha, dataInicial.ToString("yyyy-MM-dd"), dataFinal.ToString("yyyy-MM-dd 23:59:59"), idAgente, idAvaliador, notaMinima, notaMaxima);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable ListarAgente(int idCampanha)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_LISTAR_AGENTE ";
            sql += string.Format("@idCampanha = {0}",
            idCampanha);
            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable ListarHistorico(int idAgente)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_LISTAR_HISTORICO_EXIBICAO ";

            sql += string.Format("@idAgente = '{0}'",
                 idAgente);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        public DataTable ListarHistoricoProducao(int idAgente)
        {
            var sql = "APP_CRM_NIVEL_DE_CONFIANCA_LISTAR_HISTORICO_PRODUCAO_EXIBICAO ";

            sql += string.Format("@idAgente = '{0}'",
                 idAgente);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
    }
}
