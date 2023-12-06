using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class PlanoPorOperadoraParaComparacaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Operadora> ListarOperadora()
        {
            var sql = "APP_CRM_ATENDIMENTO_LISTAR_OPERADORA";
            var args = new
            {

            };

            return ExecutarProcedure<Operadora>(sql, args);
        }

        public IEnumerable<TipoDePlanoPorOperadora> ListarTipoDePlanoPorOperadora()
        {
            var sql = "APP_CRM_ATENDIMENTO_LISTAR_TIPO_DE_PLANO_POR_OPERADORA";
            var args = new
            {

            };

            return ExecutarProcedure<TipoDePlanoPorOperadora>(sql, args);
        }

        public DataTable ListarFaqDeAtendimento(int id,int idCamapanha)
        {
            throw new NotImplementedException();
        }

        public DataTable Listar(int idOperadora, int idTipoDePlanoDaOperadora, int idCampanha)
        {
            var sql = $"APP_CRM_ATENDIMENTO_LISTAR_PLANO_PARA_COMPARACAO @IdOperadora = {idOperadora}, @idTipoDePlanoOperadora = {idTipoDePlanoDaOperadora}, @idCampanha = {idCampanha} ";

            List<string> retorno = new List<string>();
            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }

        public DataTable Listar(int id, int idOperadora, int idTipoDePlanoDaOperadora, int idCampanha, bool ativo)
        {
            var sql = $"APP_CRM_PLANO_PARA_COMPARACAO_LISTAR_EXIBICAO @id = {id}, @IdOperadora = {idOperadora}, @idTipoDePlanoOperadora = {idTipoDePlanoDaOperadora}, @idCampanha = {idCampanha}, @ativo = {ativo} ";

            List<string> retorno = new List<string>();
            var resultado = CarregarDataTable(sql, new { });

            return resultado;
        }

        public IEnumerable<PlanoPorOperadoraParaComparacao> Listar(int id, bool ativo)
        {
            var sql = "APP_CRM_PLANO_PARA_COMPARACAO_LISTAR";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<PlanoPorOperadoraParaComparacao>(sql, args);
            return resultado;
        }

        public int Gravar(PlanoPorOperadoraParaComparacao plano, string campanhas)
        {
            var sql = "APP_CRM_PLANO_PARA_COMPARACAO_GRAVAR";

            var args = new
            {
                Id = plano.id,                
                IdTipoDePlano = plano.idTipoDePlano,
                IdOperadora = plano.idOperadora,
                Plano = plano.plano,
                PacoteDadosMensal = plano.pacoteDadosMensal,
                OfertaRedesSociais = plano.ofertaRedesSociais,
                Voz = plano.voz,
                Torpedos = plano.torpedos,
                Valor = plano.valor,
                Ativo = plano.ativo,                
                IdUsuario = (plano.id == 0 ? plano.idCriador : plano.idModificador),
                Campanhas = campanhas
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }
    }
}
