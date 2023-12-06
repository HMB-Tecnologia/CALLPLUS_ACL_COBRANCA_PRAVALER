using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class EnderecoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Endereco> RetornarEnderecos(string cep, string logradouro, string cidade, string uf, string bairro, string tipo)
        {
            var sql = "APP_CRM_ENDERECO_LISTAR";
            var args = new { Cep = cep, Logradouro = logradouro, Cidade = cidade, Uf  = uf, Bairro = bairro, Tipo = tipo};
            return ExecutarProcedure<Endereco>(sql, args);
        }

        public IEnumerable<KeyValuePair<string, string>> RetornarCidade(string uf)
        {
            var sql = "APP_CRM_ENDERECO_LISTAR_CIDADES_POR_UF";
            var args = new { Uf = uf };

            var resultado = ExecutarProcedure<KeyValuePair<string, string>>(sql, args);
            return resultado;           
        }

        public IEnumerable<KeyValuePair<string, string>> RetornarBairro(string uf, string cidade)
        {
            var sql = "APP_CRM_ENDERECO_LISTAR_BAIRROS_POR_UF_CIDADE";
            var args = new { Uf = uf, Cidade = cidade };

            var resultado = ExecutarProcedure<KeyValuePair<string, string>>(sql, args);
            return resultado;
        }
    }
}
