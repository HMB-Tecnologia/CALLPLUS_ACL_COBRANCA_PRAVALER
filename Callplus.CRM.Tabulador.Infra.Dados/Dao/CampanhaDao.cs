using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class CampanhaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Campanha> ListarCampanhasDoUsuario(int idUsuario)
        {
            var sql = "APP_CRM_CAMPANHA_LISTAR_CAMPANHAS_DO_USUARIO";
            var args = new { IdUsuario = idUsuario };
            var resultado = ExecutarProcedure<Campanha>(sql, args);
            return resultado;
        }

        public IEnumerable<Banco> ListarBanco(int id, bool ativo)
        {
            var sql = "APP_CRM_BANCO_LISTAR";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<Banco>(sql, args);
            return resultado;
        }

        public IEnumerable<Banco> ListarBancoDaCampanha(int? idCampanha, bool? ativo)
        {
            var sql = "APP_CRM_BANCO_BANCOS_DA_CAMPANHA_LISTAR";
            var args = new { IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<Banco>(sql, args);
            return resultado;
        }

        public IEnumerable<FormaDePagamento> ListarFormasDePagamento(int id, bool ativo)
        {
            var sql = "APP_CRM_FORMA_DE_PAGAMENTO_LISTAR";
            var args = new { Id = id, Ativo = ativo };
            var resultado = ExecutarProcedure<FormaDePagamento>(sql, args);
            return resultado;
        }

        public IEnumerable<Campanha> ListarCampanhasDoAparelho(int idAparelho)
        {
            var sql = "APP_CRM_CAMPANHAS_DO_APARELHO_LISTAR";
            var args = new { IdAparelho = idAparelho };
            var resultado = ExecutarProcedure<Campanha>(sql, args);
            return resultado;
        }

        public IEnumerable<FormaDePagamento> ListarFormasDePagamentoDaCampanha(int? idCampanha, bool? ativo)
        {
            var sql = "APP_CRM_FORMA_DE_PAGAMENTO_DA_CAMPANHA_LISTAR";
            var args = new { IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<FormaDePagamento>(sql, args);
            return resultado;
        }

        public Campanha RetornarCampanha(int idCampanha)
        {
            var sql = "APP_CRM_CAMPANHA_RETORNAR_CAMPANHA";
            var args = new { IdCampanha = idCampanha };

            Campanha campanha = ExecutarProcedureSingleOrDefault<Campanha>(sql, args);

            return campanha;
        }

        public DataTable Listar(int id, int idDiscador, string nome, bool ativo)
        {
            var sql = "APP_CRM_CAMPANHA_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idDiscador = {1}, @nome = '{2}', @ativo = {3}", id, idDiscador, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public string RetornarNomeDeArquivoDeAudioPorDataVencimento(int diaVencimentoFatura, int idCampanha)
        {
            var sql = $"EXEC APP_CRM_RETORNAR_ARQUIVO_REPRODUCAO_POR_VENCIMENTO_FATURA @diaVencimento = {diaVencimentoFatura}, @idCampanha = {idCampanha}";

            string nomeArquivo = "";

            var args = new
            {

            };

            var datatable = CarregarDataTable(sql, args);

            if (datatable.Rows.Count > 0)
            {
                nomeArquivo = datatable.Rows[0]["NomeArquivo"].ToString();
            }

            return nomeArquivo;
        }

        public int AtualizarDadosDeCadastroManual(int idCampanha, int idMailing)
        {
            var sql = "APP_CRM_CAMPANHA_GRAVAR_MAILING_CADASTRO_MANUAL";
            var args = new
            {
                IdCampanha = idCampanha,
                IdMailing = idMailing
            };
            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        public int Gravar(Campanha campanha, string idsStatusDeAtendimento)
        {

            var sql = "APP_CRM_CAMPANHA_GRAVAR";
            var args = new

            {
                Id = campanha.Id,
                IdDiscador = campanha.IdDiscador,
                IdTipoDeDiscagem = campanha.IdTipoDeDiscagem,
                IdScriptApresentacao = campanha.IdScriptApresentacao,
                IdScriptFinalizacao = campanha.IdScriptFinalizacao,
                NomeCampanha = campanha.Nome,
                AfterCall = campanha.AfterCall,
                Ativo = campanha.Ativo,
                IdCriador = campanha.IdCriador,
                IdModificador = campanha.IdModificador,
                Observacao = campanha.Observacao,
                IdLayoutCampoDinamico = campanha.IdLayoutCampoDinamico,
                HabilitaDiscagemManual = campanha.HabilitaDiscagemManual,
                HabilitaHistorico = campanha.HabilitaHistorico,
                HabilitaCadastroManual = campanha.HabilitaCadastroManual,
                IdStatusTabulacaoAutomatica = campanha.IdStatusTabulacaoAutomatica,
                EnderecoDeImportacaoDoMailing = campanha.EnderecoDeImportacaoDoMailing,
                MetaVenda = campanha.MetaVenda,
                IdStatusTabulacaoAutomaticaVenda = campanha.IdStatusTabulacaoAutomaticaVenda,
                IdLayoutCampoDinamicoBko = campanha.IdLayoutCampoDinamicoBko,
                HabilitaIndicacao = campanha.HabilitaIndicacao,
                HabilitaComparadorDePlanos = campanha.HabilitaComparadorDePlanos,
                HabilitaPesquisa = campanha.HabilitaPesquisa,
                HabilitarContatoManual = campanha.HabilitarContatoManual,
                IdMailingCadastroManual = campanha.IdMailingCadastroManual,
                IdsStatusAtendimento = idsStatusDeAtendimento
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);

        }

        public string RetornarNomeDeArquivoDeAudioPorProduto(int idProduto, int idCampanha)
        {
            var sql = $"EXEC APP_CRM_RETORNAR_NOME_ARQUIVO_DE_AUDIO @idProduto = {idProduto}, @idCampanha = {idCampanha}";

            var nomeArquivo = string.Empty;

            var args = new
            {

            };
            var datatable = CarregarDataTable(sql, args);

            if (datatable.Rows.Count > 0)
            {
                nomeArquivo = datatable.Rows[0]["NomeArquivo"].ToString();
            }

            return nomeArquivo;
        }

        public IEnumerable<Campanha> Listar(int id, bool? ativo)
        {
            var sql = "APP_CRM_CAMPANHA_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<Campanha>(sql, args);
            return resultado;
        }

        public string RetornarCaminhoDoServidor(int idCampanha)
        {
            var sql = "APP_CRM_CAMINHO_DO_SERVIDOR_LISTAR";
            var args = new { IdCampanha = idCampanha };
            var resultado = ExecutarProcedureSingleOrDefault<string>(sql, args);
            return resultado;
        }

        public IEnumerable<Campanha> ListarCampanhasDoPlanoParaComparacao(int idPlano)
        {
            var sql = "APP_CRM_CAMPANHA_LISTAR_CAMPANHAS_DO_PLANO_COMPARACAO";
            var args = new { IdPlano = idPlano };
            var resultado = ExecutarProcedure<Campanha>(sql, args);
            return resultado;
        }
    }
}
