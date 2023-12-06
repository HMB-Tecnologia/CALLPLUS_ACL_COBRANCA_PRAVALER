using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        public List<string> VerificarSeCepEhElegivel(string cep, bool ehCepEntrega)
        {
            var sql = "APP_CRM_VERIFICAR_CEP_ELEGIVEL_1";
            var args = new { Cep = cep, ehCepEntrega = ehCepEntrega };

            var resultado = ExecutarProcedure<string>(sql, args);

            return resultado.ToList();
        }

        public string RetornarUf(int ddd, int idcampanha)
        {
            var sql = "APP_CRM_RETORNAR_UF_POR_DDD_1";

            var args = new
            {
                DDD = ddd,
                iDCampanha = idcampanha,
            };

            var resultado = ExecutarProcedureSingleOrDefault<string>(sql, args);
            return resultado;
        }
    }
}
