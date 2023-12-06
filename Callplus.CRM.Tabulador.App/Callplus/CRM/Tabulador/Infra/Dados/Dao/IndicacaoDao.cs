using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class IndicacaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public void GravarIndicacaoDoAtendimento(Indicacao indicacao)
        {
            var sql = "APP_CRM_INDICACAO_GRAVAR";

            var args = new
            {
                Id = indicacao.id,
                IdProspect = indicacao.idProspect,
                IdAtendimento = indicacao.idAtendimento,
                Descricao = indicacao.descricao,
                QuantidadeDeIndicacoes = indicacao.quantidadeDeIndicacoes
            };

            ExecutarProcedure(sql, args);
        }
    }
}