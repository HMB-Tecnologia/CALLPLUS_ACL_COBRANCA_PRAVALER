using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Dominio.Tipos;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class AtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public long FinalizarAtendimento(ResultadoDoAtendimento resultado)
        {
            var sql = "APP_CRM_ATENDIMENTO_FINALIZAR_ATENDIMENTO";
            var args = new
            {
                IdAtendimento = resultado.Atendimento.Id,
                IdStatusAtendimento = resultado.StatusDoAtendimento.Id,
                Telefone = resultado.Telefone,
                TelefoneAgendamento = resultado.TelefoneAgendamento,
                DataAgendamento = resultado.DataAgendamento,
                Observacao = resultado.Observacao
            };

            return ExecutarProcedureSingleOrDefault<long>(sql, args);
        }

        public Atendimento IniciarAtendimento(int idOperador, long idProspect, int idSupervisor, int? idDiscador, string numeroChamadorDiscador,
            string ip, string host, OrigemDeAtendimento origem)
        {
            var sql = "APP_CRM_ATENDIMENTO_INICIAR_ATENDIMENTO";

            var args = new
            {
                IdProspect = idProspect,
                IdOperador = idOperador,
                IdSupervisor = idSupervisor,
                IdDiscador = idDiscador,
                Ip = ip,
                Host = host,
                IdOrigemAtendimento = (int)origem,
                NumeroChamadorDiscador = numeroChamadorDiscador
            };

            return ExecutarProcedureSingleOrDefault<Atendimento>(sql, args);
        }

        public DataTable RetornarRankingAtendimento(int idCampanha)
        {
            var sql = $"APP_CRM_RANKING_VENDA_LISTAR @IdCampanha = {idCampanha} ";

            List<string> retorno = new List<string>();
            var resultado = CarregarDataTable(sql, new { });

            return resultado;

        }

        public IEnumerable<HistoricoAtendimentoDto> RetornarHistoricoDeAtendimento(long idProspect, int idUsuario)
        {
            var sql = "APP_CRM_ATENDIMENTO_HISTORICO_ATENDIMENTO_LISTAR";
            var args = new
            {
                IdProspect = idProspect,
                IdUsuario = idUsuario
            };

            return ExecutarProcedure<HistoricoAtendimentoDto>(sql, args);
        }

        public DadosDoRanking RetornarDadosDosAtendimentos(int idOperador, int idCampanha)
        {
            var sql = "APP_CRM_ATENDIMENTO_DADOS_DO_RANKING_DE_ATENDIMENTO_DO_OPERADOR_LISTAR";
            var args = new
            {
                IdOperador = idOperador,
                IdCampanha = idCampanha
            };

            return ExecutarProcedureSingleOrDefault<DadosDoRanking>(sql, args);

        }

        public IEnumerable<ValorDeCampoDinamico> RetornarValoresDosCamposDoAtendimento(long idAtendimento)
        {
            var sql = $"APP_CRM_ATENDIMENTO_LISTAR_VALORES_CAMPOS_DO_ATENDIMENTO @IdAtendimento = {idAtendimento}";
            var args = new { IdAtendimento = idAtendimento };
            List<ValorDeCampoDinamico> retorno = new List<ValorDeCampoDinamico>();
            var resultado = CarregarDataTable(sql, new { });

            foreach (DataRow resultadoRow in resultado.Rows)
            {
                foreach (DataColumn col in resultadoRow.Table.Columns)
                {
                    var nome = col.ColumnName;
                    var conteudo = DaoUtil.ObterValorDaColunaEmDataTable(resultado, 0, col.ColumnName);
                    retorno.Add(new ValorDeCampoDinamico(nome, conteudo));
                }
                break;
            }
            return retorno;
        }

        public IEnumerable<string> VerificarSePodeRealizarAtendimentoManual(long idProspect, int idCampanha, int idUsuario)
        {
            var sql = "APP_CRM_ATENDIMENTO_VERIFICAR_SE_PODE_REALIZAR_ATENDIMENTO_MANUAL";
            var args = new
            {
                idProspect = idProspect,
                idCampanha = idCampanha,
                idUsuario = idUsuario

            };
            return ExecutarProcedure<string>(sql, args);
        }

        public IEnumerable<string> VerificarSePodeRealizarAgendamento(long idAtendimento, long telefone, DateTime? dataAgendamento, int idTipodeAgendamento)
        {
            var sql = "APP_CRM_ATENDIMENTO_VERIFICAR_SE_PODE_REALIZAR_AGENDAMENTO2";
            var args = new
            {
                IdAtendimento = idAtendimento,
                TelefoneAgendamento = telefone,
                DataAgendamento = dataAgendamento,
                IdTipodeAgendamento = idTipodeAgendamento
            };

            return ExecutarProcedure<string>(sql, args);
        }


        public IEnumerable<Usuario> ValidarSupervisor(int idSupervisor, string login, string senha)
        {
            var sql = "APP_CRM_ATENDIMENTO_VALIDAR_VENDA_CASADA";

            var args = new
            {
                Login = login,
                Senha = senha,
                IdSupervisor = idSupervisor
            };

            var resultado = ExecutarProcedure<Usuario>(sql, args);

            return resultado;
        }

        public IEnumerable<ConfiguracaoVencimentoFaturaDto> RetornarDatasDeVencimentoDeFaturaDisponiveis()
        {
            var sql = "APP_CRM_ATENDIMENTO_LISTAR_CONFIGURACAO_DE_VENCIMENTO_FATURA";
            var args = new { };
            return ExecutarProcedure<ConfiguracaoVencimentoFaturaDto>(sql, args);
        }

        public IEnumerable<string> VerificarSePodeRealizarVenda(int idCampanha, long telefone, string codigoMailing)
        {
            var sql = "APP_CRM_ATENDIMENTO_VERIFICAR_SE_PODE_REALIZAR_VENDA";
            var args = new
            {
                IdCampanha = idCampanha,
                Telefone = telefone,
                CodigoMailing = codigoMailing
            };

            return ExecutarProcedure<string>(sql, args);
        }

        #region INDICACAO

        public IEnumerable<IndicacaoDoAtendimento> ListarIndicacaoDoAtendimento(long id, long idAtendimento)
        {
            var sql = "APP_CRM_INDICACAO_DO_ATENDIMENTO_LISTAR";
            var args = new
            {
                Id = id,
                IdAtendimento = idAtendimento
            };

            return ExecutarProcedure<IndicacaoDoAtendimento>(sql, args);
        }

        public long GravarIndicacaoDoAtendimento(IndicacaoDoAtendimento indicacao)
        {
            var sql = "APP_CRM_INDICACAO_DO_ATENDIMENTO_GRAVAR";

            var args = new
            {
                Id = indicacao.id,
                IdAtendimento = indicacao.idAtendimento,
                Nome = indicacao.nome,
                Telefone = indicacao.telefone
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public void ExcluirIndicacaoDoAtendimento(long idIndicacao)
        {
            var sql = "DELETE IndicacaoDoAtendimento WHERE id = " + idIndicacao;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }

        #endregion INDICACAO
    }
}
