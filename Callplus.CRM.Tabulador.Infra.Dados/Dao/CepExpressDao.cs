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
    public class CepExpressDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<CepExpress> Listar(int id, int idCampanha, string nome, bool ativo)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

            var args = new
            {
                Id = id,
                idCampanha = idCampanha,
                nome = nome,
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<CepExpress>(sql, args);

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

        public CepExpress RetornarCepExpress(int id)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_LISTAR";

            var args = new
            {
                Id = id
            };

            var resultado = ExecutarProcedure<CepExpress>(sql, args);

            return resultado.FirstOrDefault();
        }

        public int Gravar(CepExpress cadastroCep)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_GRAVAR";

            var args = new
            {
                Id = cadastroCep.Id,
                IdCampanha = cadastroCep.IdCampanha,
                Nome = cadastroCep.Nome,
                Ativo = cadastroCep.Ativo,
                IdModificador = cadastroCep.IdModificador,
                IdCriador = cadastroCep.IdCriador,
                NomeArquivo = cadastroCep.NomeArquivo,
                Observacao = cadastroCep.Observacao
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }

        public bool VerificarSeExisteNomeDoCadastroCep(string nome)
        {
            var sql = "APP_CRM_DADOS_CEP_EXPRESS_NOME_VERIFICAR";

            var args = new
            {
                Nome = nome
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToBoolean(resultado);
        }
    }
}
