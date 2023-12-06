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

        public IEnumerable<FormaDePagamento> ListarFormasDePagamento(int id, bool? ativo)
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

        public Campanha RetornarInformacoesDaCampanha(int idCampanha)
        {
            var sql = "APP_CRM_RETORNAR_INFORMACOES_DA_CAMPANHA";
            var args = new { IdCampanha = idCampanha };

            Campanha campanha = ExecutarProcedureSingleOrDefault<Campanha>(sql, args);

            return campanha;
        }

        public IEnumerable<string> RetornarMensagemDeRevendaHabilitada()
        {
            var sql = "APP_CRM_RETORNAR_MENSAGEM_DE_REVENDA_HABILITADA";

            var args = new { };

            var resultado = ExecutarProcedure<string>(sql, args);
            return resultado;
        }

        public int Gravar(Campanha campanha, bool? espelho, int? idCampanha)
        {

            var sql = "APP_CRM_CAMPANHA_GRAVAR";

            var args = new

            {
                id								 = campanha.Id,
                idCriador						 = campanha.IdCriador,
                idModificador					 = campanha.IdModificador,
                nomeCampanha					 = campanha.Nome,
                idDiscador						 = campanha.IdDiscador,
                IdTipoDaCampanha				 = campanha.idTipoDaCampanha,
                idTipoDeDiscagem				 = campanha.IdTipoDeDiscagem,
                afterCall						 = campanha.AfterCall,
                metaVenda						 = campanha.MetaVenda,
                idScriptApresentacao			 = campanha.IdScriptApresentacao,
                idScriptFinalizacao				 = campanha.IdScriptFinalizacao,
                idLayoutCampoDinamico			 = campanha.IdLayoutCampoDinamico,
                idLayoutCampoDinamicoBko		 = campanha.IdLayoutCampoDinamicoBko,
                enderecoDeImportacaoDoMailing	 = campanha.EnderecoDeImportacaoDoMailing,
                idMailingCadastroManual			 = campanha.IdMailingCadastroManual,
                idStatusTabulacaoAutomaticaVenda = campanha.IdStatusTabulacaoAutomaticaVenda,
                idStatusTabulacaoAutomatica		 = campanha.IdStatusTabulacaoAutomatica,
                habilitaCadastroManual			 = campanha.HabilitaCadastroManual,
                habilitaDiscagemManual			 = campanha.HabilitaDiscagemManual,
                habilitarContatoManual			 = campanha.HabilitarContatoManual,
                habilitaHistorico				 = campanha.HabilitaHistorico,
                habilitaIndicacao				 = campanha.HabilitaIndicacao,
                habilitaComparadorDePlanos		 = campanha.HabilitaComparadorDePlanos,
                habilitaPesquisa				 = campanha.HabilitaPesquisa,
                habilitaCepExpress               = campanha.HabilitaCepExpress,
                habilitaRevenda                  = campanha.HabilitaRevenda,
                observacao                       = campanha.Observacao,
                ativo							 = campanha.Ativo,
                idsBancos						 = campanha.idBancosDaCampanha,
                idsFormasPagamento				 = campanha.idFormasDePagamento,
                idsStatusAtendimento		     = campanha.idStatusDeAtendimento,
                idsStatusOferta					 = campanha.idStatusDeOferta,
                idsStatusAuditoria				 = campanha.idStatusDeAuditoria,
                Espelho							 = espelho,
                idCampanhaEspelho				 = idCampanha,
                aparelhos						 = campanha.Aparelhos,
                variaveisDoScript				 = campanha.VariaveisDoScript,
                checkListVenda					 = campanha.CheckListVenda,
                planosComparacao				 = campanha.PlanosComparacao,
                formularioQualidade				 = campanha.FormularioQualidade,
                faqAtendimento                   = campanha.FaqAtendimento,
                IdTipoDeAuditoria                = campanha.IdTipoDeAuditoria
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
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

        public IEnumerable<Campanha> ListarTipoDaCampanha(int id, bool? ativo)
        {
            var sql = "APP_CRM_TIPO_DE_CAMPANHA_LISTAR";
            var args = new { Ativo = ativo };
            var resultado = ExecutarProcedure<Campanha>(sql, args);
            return resultado;
        }

        public int VincularUsuarioAhCampanha(int idUsuario, int idCampanha)
        {
            var sql = "APP_CRM_CAMPANHA_VINCULAR_USUARIO";
            var args = new
            {
                IdUsuario = idUsuario,
                IdCampanha = idCampanha
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

    }
}
