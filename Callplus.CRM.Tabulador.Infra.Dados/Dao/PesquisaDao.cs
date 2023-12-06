using Callplus.CRM.Tabulador.Dominio.Dto;
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
    public class PesquisaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<PesquisaDoAtendimentoDto> CarregarPesquisaDoAtendimento(long idAtendimento)
        {
            var sql = "APP_CRM_PESQUISA_CARREGAR_PESQUISA_DO_ATENDIMENTO";

            var args = new { IdAtendimento = idAtendimento };

            return ExecutarProcedure<PesquisaDoAtendimentoDto>(sql, args);
        }

        public IEnumerable<OpcaoDaPerguntaDaPesquisa> ListarOpcaoDaPerguntaDaPesquisa(int idPergunta)
        {
            var sql = "APP_CRM_PESQUISA_LISTAR_OPCAO_DA_PERGUNTA_DA_PESQUISA";

            var args = new { IdPergunta = idPergunta };

            return ExecutarProcedure<OpcaoDaPerguntaDaPesquisa>(sql, args);
        }

        public long GravarRespostaDoAtendimento(RespostaDaPesquisaDoAtendimento resposta)
        {
            var sql = "APP_CRM_PESQUISA_GRAVAR_RESPOSTA_DO_ATENDIMENTO";

            var args = new
            {
                Id = resposta.Id,
                IdAtendimento = resposta.IdAtendimento,
                IdPerguntaDaPesquisa = resposta.IdPerguntaDaPesquisa,
                IdOpcaoRespondida = resposta.IdOpcaoRespondida
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }
    }
}
