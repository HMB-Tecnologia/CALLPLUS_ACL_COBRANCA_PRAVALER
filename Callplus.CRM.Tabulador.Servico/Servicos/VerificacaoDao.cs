using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class VerificacaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public List<string> VerificarSePodeCriarDiretorio(string diretorio)
        {
            string sql = $"APP_CRM_ATENDIMENTO_VERIFICAR_SE_PODE_CRIAR_DIRETORIO ";

            var args = new { Diretorio = diretorio };

            var resultado = ExecutarProcedure<string>(sql, args);

            return resultado.ToList();
        }

        public List<string> VerificarSePodeCriarNomeCampanha(string nomeCampanha)
        {
            string sql = $"APP_CRM_ATENDIMENTO_VERIFICAR_SE_PODE_CRIAR_NOME_CAMPANHA ";

            var args = new { campanha = nomeCampanha };

            var resultado = ExecutarProcedure<string>(sql, args);

            return resultado.ToList();
        }
    }
}
