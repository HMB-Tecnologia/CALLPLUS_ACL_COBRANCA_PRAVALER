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
    public class MailingDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Mailing> Listar(int id, bool ativo)
        {
            var sql = "APP_CRM_MAILING_LISTAR";

            var args = new
            {
                Id = id,
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<Mailing>(sql, args);

            return resultado;
        }

        public DataTable Listar(int id, int idCampanha, string nome, bool ativo)
        {
            
            var sql = "APP_CRM_MAILING_LISTAR_EXIBICAO ";
            sql += string.Format("@id = {0}, @idCampanha = {1}, @nome = '{2}', @ativo = {3}",
            id, idCampanha, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable ExportarMailing(int idMailing)
        {
            var sql = "APP_CRM_MAILING_EXPORTAR ";
            sql += string.Format("@IdMailing = {0}", idMailing);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Mailing> Listar(int? id, int? idCampanha, bool? ativo)
        {
            var sql = "APP_CRM_MAILING_LISTAR";

            var args = new
            {
                Id = id,
                Ativo = ativo,
                IdCampanha = idCampanha
            };

            var resultado = ExecutarProcedure<Mailing>(sql, args);

            return resultado;
        }

        public string RetornoMailingsTrabalhadosDia(DateTime dataInicio, DateTime dataTermino, string codMailing, int idCampanha)
        {
            string sSql = string.Empty;
            sSql += $"EXEC SP_RETORNAR_MAILINGS_TRABALHADOS_NO_DIA ";
            sSql += $"@CodMailingClaro = '{codMailing}', ";
            sSql += $"@DataInicio = '{dataInicio.ToString("yyyy-MM-dd")}', ";
            sSql += $"@DataTermino = '{dataTermino.ToString("yyyy-MM-dd")} 23:59:59', ";
            sSql += $"@IDCampanha = {idCampanha}";

            return sSql;
        }

        public void ExportarMailingDiscador(int idMailing)
        {
            var sql = "APP_CRM_MAILING_EXPORTAR_DISCADOR_AUTOMATICO";
            var args = new { idMailing };

            ExecutarProcedure(sql, args);
        }

        public bool VerificarSeMailingEstaProcessadoComSucesso(int idMailing)
        {
            var sql = "APP_CRM_MAILING_STATUS_PROCESSAMENTO_VERIFICAR";

            var args = new
            {
                IdMailing = idMailing
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToBoolean(resultado);
        }

        public int Gravar(Mailing mailing)
        {
            var sql = "APP_CRM_MAILING_GRAVAR";

            var args = new
            {
                Id = mailing.id,
                IdCampanha = mailing.idCampanha,
                Nome = mailing.nome,
                Ativo = mailing.ativo,
                Indicacao = mailing.indicacao,
                IdModificador = mailing.idModificador,
                IdCriador = mailing.idCriador,
                NomeArquivo = mailing.nomeArquivo,
                Observacao = mailing.observacao
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }

        public bool VerificarSeExisteNomeDoMailing(string nome)
        {
            var sql = "APP_CRM_MAILING_NOME_VERIFICAR";

            var args = new
            {
                Nome = nome
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToBoolean(resultado);
        }

        public string RetornoTelefoniaPorDiaIdMailing(DateTime dataInicio, DateTime dataTermino, string nomeArquivo, int idCampanha, string codMailing, int tipoExportacaoContatosNaoTrabalhados)
        {
            string sSql = string.Empty;
            sSql += $"EXEC sRetornoTelefoniaPorDiaIdMailing ";
            sSql += $"@DataInicio = '{dataInicio.ToString("yyyy-MM-dd")}', ";
            sSql += $"@DataTermino = '{dataTermino.ToString("yyyy-MM-dd")} 23:59:59', ";
            sSql += $"@NomeArquivo = '{nomeArquivo}', ";
            sSql += $"@IdCampanha = {idCampanha}, ";
            sSql += $"@CodMailing = '{codMailing}', ";
            sSql += $"@tipoExportacaoContatosNaoTrabalhados = {tipoExportacaoContatosNaoTrabalhados}";

            return sSql;
        }

        public object RetornoOcorrenciasPorDiaIdMailing(DateTime dataInicio, DateTime dataTermino, string nomeArquivo, int idCampanha, string codMailing)
        {
            string sSql = string.Empty;
            sSql += $"EXEC sRetornoOcorrenciasPorDiaIdMailing ";
            sSql += $"@DataInicio = '{dataInicio.ToString("yyyy-MM-dd")}',";
            sSql += $"@DataTermino = '{dataTermino.ToString("yyyy-MM-dd")} 23:59:59',";
            sSql += $"@NomeArquivo = '{nomeArquivo}',";
            sSql += $"@IdCampanha  = {idCampanha}, ";
            sSql += $"@CodMailing  = '{codMailing}'";

            return sSql;
        }

        public string RetornarBaseB(string codMaillingClaro, string dataInicio, string dataTermino, bool todos, string nomeCampanha)
        {
            string sSql = "";
            var dtInicio = "";
            var dtTermino = "";

            dtInicio = Convert.ToDateTime(dataInicio).ToString("yyyy-MM-dd");
            dtTermino = Convert.ToDateTime(dataTermino).ToString("yyyy-MM-dd") + " 23:59:59";

            sSql += " EXEC SP_RETORNAR_ITENS_BASE_B";
            sSql += $" @CodMailingClaro = '{codMaillingClaro}',";
            sSql += $" @DataInicio = '{dtInicio}',";
            sSql += $" @DataTermino = '{dtTermino}',";
            sSql += $" @NomeCampanha = '{nomeCampanha}'";

            return sSql;
        }

		public string GravarArquivoDeMailingEMarcacoes(string sqlArquivoMailing, string sqlArquivoMarcacoes, int idMailing)
		{
			var sql = "APP_CRM_ARQUIVO_DE_MAILING_E_MARCACOES_PRAVALER_GRAVAR ";

			var args = new
			{
				sqlArquivoMailing,
                sqlArquivoMarcacoes,
                idMailing
			};

			var resultado = ExecuteProcedureScalar(sql, args);
            return resultado?.ToString();
		}
	}
}
