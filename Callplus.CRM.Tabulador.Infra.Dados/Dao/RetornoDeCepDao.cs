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
    public class RetornoDeCepDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<CepCorreios> RetornarEndereco(string cep, string uf, string cidade, string bairro, string logradouro, int tipodepesquisa)
        {
            var sql = "APP_CRM_CEP_ACTIONLINE_LISTAR_EXIBICAO";
            var args = new
            { 
                Cep = cep,
                Uf = uf,
                Cidade = cidade,
                Bairro = bairro,
                Logradouro = logradouro,
                Tipodepesquisa = tipodepesquisa
            };

            var resultado = ExecutarProcedure<CepCorreios>(sql, args);

            return resultado;
        }

        public DataTable RetornarDataTableEndereco(string cep, string uf, string cidade, string bairro, string logradouro, int tipodepesquisa)
        {
            var sql = "APP_CRM_CEP_ACTIONLINE_LISTAR_2 ";

            sql += string.Format("@cep = '{0}', @uf = '{1}', @cidade = '{2}', @bairro = '{3}', @logradouro = '{4}', @tipodepesquisa = {5} ", 
                cep, uf, cidade, bairro, logradouro, tipodepesquisa);

            var args = new { };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<SiglaUfBrasil> ListarUfBrasil(bool ativo, string uf)
        {
            var sql = "APP_CRM_UF_LISTAR_1";
            var args = new
            {
                Ativo = ativo,
                Uf = uf
            };

            var resultado = ExecutarProcedure<SiglaUfBrasil>(sql, args);

            return resultado;
        }
    }
}
