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
    public class FaqDeAtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public List<FaqDeAtendimento> Listar(int id,int idCampanha,bool? ativo)
        {
            string sql = $"APP_CRM_FAQDEATENDIMENTO_LISTAR";
            var args = new {id= id, idCampanha = idCampanha,ativo = ativo };

            var resultado = ExecutarProcedure<FaqDeAtendimento>(sql, args);
            return resultado.ToList();
        }

        public DataTable ListarFaqDeAtendimentoExibicao(int idCampanha, int id)
        {
            var sql = "APP_CRM_FAQDEATENDIMENTO_LISTAR_EXIBICAO ";
            sql += string.Format("@id = {0}, @idCampanha = {1}",
            id, idCampanha);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public FaqDeAtendimento RetornarFaqAtendimento(int idFaqAtendimento)
        {
            var sql = "APP_CRM_FAQ_DE_ATENDIMENTO_LISTAR_POR_ID";

            var args = new
            {
                Id = idFaqAtendimento
            };

            var resultado = ExecutarProcedureSingleOrDefault<FaqDeAtendimento>(sql, args);          

            return resultado;
        }

        public int GravarFaqDeAtendimento(FaqDeAtendimento faqDeAtendimento)
        {
            var sql = "APP_CRM_FAQDEATENDIMENTO_GRAVAR";
           
            var args = new
            {
                Id = faqDeAtendimento.Id,
                IdCampanha = faqDeAtendimento.IdCampanha,
                Pergunta = faqDeAtendimento.Pergunta,
                Resposta = faqDeAtendimento.Resposta,
                Ativo = faqDeAtendimento.Ativo,              
                IdCriador = faqDeAtendimento.IdCriador,
                IdModificador = faqDeAtendimento.IdModificador

            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;


        }
    }
}
