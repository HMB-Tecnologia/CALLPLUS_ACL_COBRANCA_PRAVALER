using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Infra.Dados.Util;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class OfertaDoAtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable ListarOfertaDoAtendimento(long idAtendimento)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_LISTAR_EXIBICAO ";
            sql += string.Format("@idAtendimento = {0}", idAtendimento);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public OfertaDoAtendimento RetornarOfertaElegivelParaAtendimento(long idAtendimento, int idCampanha)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_RETORNAR_OFERTA_ELEGIVEL";

            var args = new
            {
                IdAtendimento = idAtendimento,
                idCampanha = idCampanha
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimento>(sql, args).FirstOrDefault();

            return resultado;
        }

        public string ValidarLoginWM(string login)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_VALIDAR_LOGIN_WM";

            var args = new
            {
                Login = login,
            };

            var resultado = ExecutarProcedureSingleOrDefault<string>(sql, args);
            return resultado;
        }

        public string ValidarLoginDaOperadora(int idOperadora, string login)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_VALIDAR_LOGIN_OPERADORA";

            var args = new
            {
                IdOperadora = idOperadora,
                Login = login,
            };

            var resultado = ExecutarProcedureSingleOrDefault<string>(sql, args);
            return resultado;
        }

        public IEnumerable<OfertaDoAtendimento> RetornarOfertaDoAtendimento(long idAtendimento)
        {

            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_LISTAR";
            var args = new
            {
                IdAtendimento = idAtendimento
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimento>(sql, args);

            return resultado;
        }

        public IEnumerable<string> VerificarSePodeEditarOfertaBKO(int idUsuario, long idOfertaBko, int idTipoProduto)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_VERIFICAR_SE_PODE_EDITAR";

            var args = new
            {
                idUsuario = idUsuario,
                idOfertaBko = idOfertaBko,
                idTipoProduto = idTipoProduto
            };

            var resultado = ExecutarProcedure<string>(sql, args);
            return resultado;
        }

        public void RemoverHistoricoDeOfertaBkoPendente(int idUsuario, long idOfertaBko, int? idTipoDeProduto)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_REMOVER_HISTORICO_PENDENTE";

            var args = new
            {
                idUsuario = idUsuario,
                idOfertaBko = idOfertaBko,
                idTipoDeProduto = idTipoDeProduto
            };
            ExecutarProcedure(sql, args);
        }

        public DataTable ListarHistoricoDaOfertaDoAtendimentoBKO(long idOfertaBko, int idTipoDeProduto)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_LISTAR_HISTORICO_EXIBICAO";
            sql += $" @idOfertaBko = {idOfertaBko}, ";
            sql += $" @idTipoDeProduto = {idTipoDeProduto}";

            return CarregarDataTable(sql, new { });
        }

        public IEnumerable<HistoricoDaOfertaDoAtendimentoBkoDTO> ListarHistoricoDaOfertaDoAtendimentoBKO_DTO(long? idHistorico, long? idOfertaBko, int idTipoDeProduto)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_LISTAR_HISTORICO_DTO";

            var args = new
            {
                idHistorico = idHistorico,
                idOfertaBko = idOfertaBko,
                idTipoDeProduto = idTipoDeProduto
            };

            var resultado = ExecutarProcedure<HistoricoDaOfertaDoAtendimentoBkoDTO>(sql, args);
            return resultado;
        }

        public ResumoDaOfertaDoAtendimentoBkoDTO RetornarResumoDaOfertaDoAtendimentoBKO(long id, int idTipoDeProduto)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_RETORNAR_RESUMO";

            var args = new
            {
                Id = id,
                IdTipoDeProduto = idTipoDeProduto
            };

            var resultado = ExecutarProcedure<ResumoDaOfertaDoAtendimentoBkoDTO>(sql, args).FirstOrDefault();

            return resultado;
        }

        public IEnumerable<ConfiguracaoVencimentoFaturaDto> RetornarDatasDeVencimentoDeFaturaDisponiveisBKO()
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_LISTAR_VENCIMENTO_FATURA";
            var args = new { };
            return ExecutarProcedure<ConfiguracaoVencimentoFaturaDto>(sql, args);
        }

        public IEnumerable<CanalAdicional> ListarCanalAdicional(int idOperadora, int idProduto, bool ativo)
        {

            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_LISTAR_CANAL_ADICIONAL";
            var args = new
            {
                IdOperadora = idOperadora,
                IdProduto = idProduto,
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<CanalAdicional>(sql, args);

            return resultado;
        }

        public DataTable RetornarOfertaParaAvaliacao(int idCampanha, int idSupervisor, int idOperador, string dataInicial, string dataFinal, string idStatus)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_RETORNAR_OFERTA_PARA_AVALIACAO ";

            sql += string.Format("@IdCampanha = {0}, @IdSupervisor = {1}, @IdOperador = {2}, @DataInicial = '{3}', @DataFinal = '{4}', @idStatus = '{5}'",
                idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarOfertaParaAvaliacaoBKO(long idOfertaBko)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_RETORNAR_OFERTA_PARA_AVALIACAO ";

            sql += string.Format("@IdOferta = {0}", idOfertaBko);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }


        #region CLARO_MIGRACAO

        public long GravarOfertaDoAtendimentoClaroMigracao(OfertaDoAtendimentoClaroMigracao oferta)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_MIGRACAO_GRAVAR";

            var args = new
            {
                Id = oferta.Id,
                IdAtendimento = oferta.IdAtendimento,
                IdStatusDaOferta = oferta.IdStatusDaOferta,
                IdProduto = oferta.IdProduto,
                NumeroMigrado = oferta.NumeroMigrado,
                IdProduto2 = oferta.IdProduto2,
                NumeroMigrado2 = oferta.NumeroMigrado2,
                FaturaDigital = oferta.FaturaDigital,
                EmailFaturaDigital = oferta.EmailFaturaDigital,
                DiaVencimento = oferta.DiaVencimento,
                IdFormaDePagamento = oferta.IdFormaDePagamento,
                IdBanco = oferta.IdBanco,
                Agencia = oferta.Agencia,
                Conta = oferta.Conta,
                Nome = oferta.Nome,
                Cpf = oferta.Cpf,
                Rg = oferta.Rg,
                Nascimento = oferta.Nascimento,
                NomeDaMae = oferta.NomeDaMae,
                TelefoneCelular = oferta.TelefoneCelular,
                TelefoneResidencial = oferta.TelefoneResidencial,
                TelefoneRecado = oferta.TelefoneRecado,
                IdEstadoCivil = oferta.IdEstadoCivil,
                IdProfissao = oferta.IdProfissao,
                IdEscolaridade = oferta.IdFaixaDeRenda,
                Cep = oferta.Cep,
                Logradouro = oferta.Logradouro,
                Numero = oferta.Numero,
                Complemento = oferta.Complemento,
                Bairro = oferta.Bairro,
                Cidade = oferta.Cidade,
                Uf = oferta.Uf,
                PontoDeReferencia = oferta.PontoDeReferencia,
                Observacao = oferta.Observacao,
                TitularidadeDiferente = oferta.TitularidadeDiferente,
                IdOperador = oferta.IdOperador
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

            return resultado;
        }

        public long GravarOfertaDoAtendimentoClaroMigracaoBKO(OfertaDoAtendimentoClaroMigracaoBKO oferta)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_MIGRACAO_BKO_GRAVAR";

            var args = new
            {
                Id = oferta.id,
                IdStatusDaOferta = oferta.idStatusDaOferta,
                IdProduto = oferta.idProduto,
                NumeroMigrado = oferta.numeroMigrado,
                FaturaDigital = oferta.faturaDigital,
                EmailFaturaDigital = oferta.emailFaturaDigital,
                DiaVencimento = oferta.diaVencimento,
                IdFormaDePagamento = oferta.idFormaDePagamento,
                IdBanco = oferta.idBanco,
                Agencia = oferta.agencia,
                Conta = oferta.conta,
                Nome = oferta.nome,
                Cpf = oferta.cpf,
                Rg = oferta.rg,
                Nascimento = oferta.nascimento,
                NomeDaMae = oferta.nomeDaMae,
                TelefoneCelular = oferta.telefoneCelular,
                TelefoneResidencial = oferta.telefoneResidencial,
                TelefoneRecado = oferta.telefoneRecado,
                IdEstadoCivil = oferta.idEstadoCivil,
                IdProfissao = oferta.idProfissao,
                IdEscolaridade = oferta.idFaixaDeRenda,
                Cep = oferta.cep,
                Logradouro = oferta.logradouro,
                Numero = oferta.numero,
                Complemento = oferta.complemento,
                Bairro = oferta.bairro,
                Cidade = oferta.cidade,
                Uf = oferta.uf,
                PontoDeReferencia = oferta.pontoDeReferencia,
                Observacao = oferta.observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public long GravarStatusDaOfertaDoAtendimento(long idOferta, long idAtendimento, long idStatus)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_GRAVAR";

            var args = new
            {
                Id = idOferta,
                IdAtendimento = idAtendimento,
                IdStatusDaOferta = idStatus
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public OfertaDoAtendimentoClaroMigracao RetornarOfertaDoAtendimentoClaroMigracao(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_MIGRACAO_LISTAR";

            var args = new
            {
                Id = id,
                IdAtendimento = -1
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoClaroMigracao>(sql, args).FirstOrDefault();

            return resultado;
        }

        public OfertaDoAtendimentoClaroMigracaoBKO RetornarOfertaDoAtendimentoClaroMigracaoBKO(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_MIGRACAO_LISTAR";

            var args = new
            {
                Id = id
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoClaroMigracaoBKO>(sql, args).FirstOrDefault();
            return resultado;
        }

        public long GravarHistoricoDoAtendimentoClaroMigracaoBKO(HistoricoDaOfertaDoAtendimentoMigracaoBKO historico)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_MIGRACAO_GRAVAR_HISTORICO";

            var args = new
            {
                Id = historico.id,
                IdOferta = historico.idOfertaDoAtendimentoMigracaoBKO,
                IdStatusAuditoria = historico.idStatusAuditoria,
                Protocolo = historico.protocolo,
                Autorizacao = historico.autorizacao,
                LoginWM = historico.loginWM,
                CodigoAgente = historico.codigoAgente,
                IdCriador = historico.idCriador,
                Observacao = historico.Observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        #endregion CLARO_MIGRACAO

        #region CLARO_RENTABILIZACAO

        public long GravarOfertaDoAtendimentoClaroRentabilizacao(OfertaDoAtendimentoClaroRentabilizacao oferta)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_RENTABILIZACAO_GRAVAR";

            var args = new
            {
                Id = oferta.Id,
                IdAtendimento = oferta.IdAtendimento,
                IdStatusDaOferta = oferta.IdStatusDaOferta,
                IdProduto = oferta.IdProduto,
                NumeroMigrado = oferta.NumeroMigrado,
                FaturaDigital = oferta.FaturaDigital,
                EmailFaturaDigital = oferta.EmailFaturaDigital,
                DiaVencimento = oferta.DiaVencimento,
                IdFormaDePagamento = oferta.IdFormaDePagamento,
                IdBanco = oferta.IdBanco,
                Agencia = oferta.Agencia,
                Conta = oferta.Conta,
                Nome = oferta.Nome,
                Cpf = oferta.Cpf,
                Rg = oferta.Rg,
                Nascimento = oferta.Nascimento,
                NomeDaMae = oferta.NomeDaMae,
                TelefoneCelular = oferta.TelefoneCelular,
                TelefoneResidencial = oferta.TelefoneResidencial,
                TelefoneRecado = oferta.TelefoneRecado,
                IdEstadoCivil = oferta.IdEstadoCivil,
                IdProfissao = oferta.IdProfissao,
                IdEscolaridade = oferta.IdFaixaDeRenda,
                Cep = oferta.Cep,
                Logradouro = oferta.Logradouro,
                Numero = oferta.Numero,
                Complemento = oferta.Complemento,
                Bairro = oferta.Bairro,
                Cidade = oferta.Cidade,
                Uf = oferta.Uf,
                PontoDeReferencia = oferta.PontoDeReferencia,
                DesejaAparelho = oferta.DesejaAparelho,
                IdAparelho = oferta.IdAparelho,
                IdFormaDePagamentoAparelho = oferta.IdFormaDePagamentoAparelho,
                IdPassaporteOferta = oferta.IdPassaporteOferta,
                Observacao = oferta.Observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

            return resultado;
        }

        public long GravarStatusDaOfertaDoAtendimentoClaroRentabilizacao(long idOferta, long idAtendimento, long idStatus)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_RENTABILIZACAO_GRAVAR";

            var args = new
            {
                Id = idOferta,
                IdAtendimento = idAtendimento,
                IdStatusDaOferta = idStatus
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public OfertaDoAtendimentoClaroRentabilizacao RetornarOfertaDoAtendimentoClaroRentabilizacao(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_RENTABILIZACAO_LISTAR";

            var args = new
            {
                Id = id,
                IdAtendimento = -1
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoClaroRentabilizacao>(sql, args).FirstOrDefault();

            return resultado;
        }

        public OfertaDoAtendimentoClaroRentabilizacaoBKO RetornarOfertaDoAtendimentoClaroRentabilizacaoBKO(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_RENTABILIZACAO_LISTAR";

            var args = new
            {
                Id = id
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoClaroRentabilizacaoBKO>(sql, args).FirstOrDefault();
            return resultado;
        }

        public long GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO historico)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_RENTABILIZACAO_GRAVAR_HISTORICO";

            var args = new
            {
                Id = historico.id,
                IdOferta = historico.idOfertaDoAtendimentoRentabilizacaoBKO,
                IdStatusAuditoria = historico.idStatusAuditoria,
                Protocolo = historico.protocolo,
                Autorizacao = historico.autorizacao,
                LoginWM = historico.loginWM,
                CodigoAgente = historico.codigoAgente,
                IdCriador = historico.idCriador,
                Observacao = historico.Observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        #endregion CLARO_RENTABILIZACAO             

        #region CLARO_PORTABILIDADE

        public long GravarOfertaDoAtendimentoClaroPortabilidade(OfertaDoAtendimentoClaroPortabilidade oferta)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_PORTABILIDADE_GRAVAR";

            var args = new
            {
                Id = oferta.Id,
                IdAtendimento = oferta.IdAtendimento,
                IdStatusDaOferta = oferta.IdStatusDaOferta,
                IdProduto = oferta.IdProduto,
                IdProduto2 = oferta.IdProduto2,
                FaturaDigital = oferta.FaturaDigital,
                EmailFaturaDigital = oferta.EmailFaturaDigital,
                DiaVencimento = oferta.DiaVencimento,
                IdFormaDePagamento = oferta.IdFormaDePagamento,
                IdBanco = oferta.IdBanco,
                Agencia = oferta.Agencia,
                Conta = oferta.Conta,
                Nome = oferta.Nome,
                Cpf = oferta.Cpf,
                Rg = oferta.Rg,
                Nascimento = oferta.Nascimento,
                NomeDaMae = oferta.NomeDaMae,
                TelefoneCelular = oferta.TelefoneCelular,
                TelefoneResidencial = oferta.TelefoneResidencial,
                TelefoneRecado = oferta.TelefoneRecado,
                IdEstadoCivil = oferta.IdEstadoCivil,
                IdProfissao = oferta.IdProfissao,
                IdEscolaridade = oferta.IdFaixaDeRenda,
                Cep = oferta.Cep,
                Logradouro = oferta.Logradouro,
                Numero = oferta.Numero,
                Complemento = oferta.Complemento,
                Bairro = oferta.Bairro,
                Cidade = oferta.Cidade,
                Uf = oferta.Uf,
                PontoDeReferencia = oferta.PontoDeReferencia,
                Observacao = oferta.Observacao,
                IdOperadora = oferta.IdOperadora,
                IdTipoDePlano = oferta.IdTipoDePlano,
                ValorConta1 = oferta.ValorConta1,
                ValorConta2 = oferta.ValorConta2,
                ValorConta3 = oferta.ValorConta3
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

            return resultado;
        }

        public long GravarStatusDaOfertaDoAtendimentoClaroPortabilidade(long idOferta, long idAtendimento, long idStatus)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_PORTABILIDADE_GRAVAR";

            var args = new
            {
                Id = idOferta,
                IdAtendimento = idAtendimento,
                IdStatusDaOferta = idStatus
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public OfertaDoAtendimentoClaroPortabilidade RetornarOfertaDoAtendimentoClaroPortabilidade(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_PORTABILIDADE_LISTAR";

            var args = new
            {
                Id = id,
                IdAtendimento = -1
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoClaroPortabilidade>(sql, args).FirstOrDefault();

            return resultado;
        }

        public OfertaDoAtendimentoClaroPortabilidadeBKO RetornarOfertaDoAtendimentoClaroPortabilidadeBKO(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_PORTABILIDADE_LISTAR";

            var args = new
            {
                Id = id
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoClaroPortabilidadeBKO>(sql, args).FirstOrDefault();
            return resultado;
        }

        public long GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(HistoricoDaOfertaDoAtendimentoPortabilidadeBKO historico)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_PORTABILIDADE_GRAVAR_HISTORICO";

            var args = new
            {
                Id = historico.id,
                IdOferta = historico.idOfertaDoAtendimentoPortabilidadeBKO,
                IdStatusAuditoria = historico.idStatusAuditoria,
                Protocolo = historico.protocolo,
                Autorizacao = historico.autorizacao,
                Ordem = historico.ordem,
                LoginWM = historico.loginWM,
                CodigoAgente = historico.codigoAgente,
                IdCriador = historico.idCriador,
                Observacao = historico.Observacao,
                NumeroProvisorio = historico.numeroProvisorio
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        #endregion CLARO_PORTABILIDADE

        #region NET_PTV

        public long GravarOfertaDoAtendimentoNETPTV(OfertaDoAtendimentoNETPTV oferta)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_NET_PTV_GRAVAR";

            var args = new
            {
                Id = oferta.Id,
                IdAtendimento = oferta.IdAtendimento,
                IdStatusDaOferta = oferta.IdStatusDaOferta,
                IdProduto = oferta.IdProduto,
                FaturaDigital = oferta.FaturaDigital,
                EmailFaturaDigital = oferta.EmailFaturaDigital,
                DiaVencimento = oferta.DiaVencimento,
                IdFormaDePagamento = oferta.IdFormaDePagamento,
                IdBanco = oferta.IdBanco,
                Agencia = oferta.Agencia,
                Conta = oferta.Conta,
                Nome = oferta.Nome,
                Cpf = oferta.Cpf,
                Rg = oferta.Rg,
                Nascimento = oferta.Nascimento,
                NomeDaMae = oferta.NomeDaMae,
                TelefoneCelular = oferta.TelefoneCelular,
                TelefoneResidencial = oferta.TelefoneResidencial,
                TelefoneRecado = oferta.TelefoneRecado,
                IdEstadoCivil = oferta.IdEstadoCivil,
                IdProfissao = oferta.IdProfissao,
                IdEscolaridade = oferta.IdFaixaDeRenda,
                Cep = oferta.Cep,
                Logradouro = oferta.Logradouro,
                Numero = oferta.Numero,
                Complemento = oferta.Complemento,
                Bairro = oferta.Bairro,
                Cidade = oferta.Cidade,
                Uf = oferta.Uf,
                PontoDeReferencia = oferta.PontoDeReferencia,
                Observacao = oferta.Observacao,
                PontoAdicional = oferta.PontoAdicional,
                DataInstalacaoPreferida = oferta.DataInstalacaoPreferida,
                DataInstalacaoSecundaria = oferta.DataInstalacaoSecundaria,
                Periodo = oferta.Periodo,
                CanalAdicional = oferta.CanalAdicional,
                PlanoTelefoneFixo = oferta.PlanoTelefoneFixo
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

            return resultado;
        }

        public long GravarStatusDaOfertaDoAtendimentoNETPTV(long idOferta, long idAtendimento, long idStatus)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_NET_PTV_GRAVAR";

            var args = new
            {
                Id = idOferta,
                IdAtendimento = idAtendimento,
                IdStatusDaOferta = idStatus
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        public OfertaDoAtendimentoNETPTV RetornarOfertaDoAtendimentoNETPTV(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_NET_PTV_LISTAR";

            var args = new
            {
                Id = id,
                IdAtendimento = -1
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoNETPTV>(sql, args).FirstOrDefault();

            return resultado;
        }

        public OfertaDoAtendimentoNETPTVBKO RetornarOfertaDoAtendimentoNetPtvBKO(long id)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_NET_PTV_LISTAR";

            var args = new
            {
                Id = id
            };

            var resultado = ExecutarProcedure<OfertaDoAtendimentoNETPTVBKO>(sql, args).FirstOrDefault();
            return resultado;
        }

        public long GravarHistoricoDoAtendimentoNetPtvBKO(HistoricoDaOfertaDoAtendimentoNetPtvBKO historico)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_NET_PTV_GRAVAR_HISTORICO";

            var args = new
            {
                Id = historico.id,
                IdOferta = historico.idOfertaDoAtendimentoNetPtvBKO,
                IdStatusAuditoria = historico.idStatusAuditoria,
                Protocolo = historico.protocolo,
                NumeroDoContrato = historico.numeroDoContrato,
                NumeroDoPedido = historico.numeroDoPedido,
                LoginNet = historico.loginNet,
                CodigoAgente = historico.codigoAgente,
                IdCriador = historico.idCriador,
                Observacao = historico.observacao,
                DataInstalacaoCorrigida = historico.dataInstalacaoCorrigida,
                PeriodoCorrigido = historico.periodoCorrigido
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
            return resultado;
        }

        #endregion NET_PTV

        public IEnumerable<PassaporteOferta> ListarPassaporteOferta()
        {
            var sql = "APP_CRM_PASSAPORTE_OFERTA_LISTAR";

            var resultado = ExecutarProcedure<PassaporteOferta>(sql, null);
            return resultado;
        }

        public long GravarOfertaDoAtendimentoClaroRentabilizacaoBKO(OfertaDoAtendimentoClaroRentabilizacaoBKO ofertaBko)
        {
            var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_RENTABILIZACAO_BKO_GRAVAR";

            var args = new
            {
                Id = ofertaBko.id,
                IdProduto = ofertaBko.idProduto,
                NumeroMigrado = ofertaBko.numeroMigrado,
                FaturaDigital = ofertaBko.faturaDigital,
                EmailFaturaDigital = ofertaBko.emailFaturaDigital,
                DiaVencimento = ofertaBko.diaVencimento,
                IdFormaDePagamento = ofertaBko.idFormaDePagamento,
                IdBanco = ofertaBko.idBanco,
                Agencia = ofertaBko.agencia,
                Conta = ofertaBko.conta,
                Nome = ofertaBko.nome,
                Cpf = ofertaBko.cpf,
                Rg = ofertaBko.rg,
                Nascimento = ofertaBko.nascimento,
                NomeDaMae = ofertaBko.nomeDaMae,
                TelefoneCelular = ofertaBko.telefoneCelular,
                TelefoneResidencial = ofertaBko.telefoneResidencial,
                TelefoneRecado = ofertaBko.telefoneRecado,
                IdEstadoCivil = ofertaBko.idEstadoCivil,
                IdProfissao = ofertaBko.idProfissao,
                IdEscolaridade = ofertaBko.idFaixaDeRenda,
                Cep = ofertaBko.cep,
                Logradouro = ofertaBko.logradouro,
                Numero = ofertaBko.numero,
                Complemento = ofertaBko.complemento,
                Bairro = ofertaBko.bairro,
                Cidade = ofertaBko.cidade,
                Uf = ofertaBko.uf,
                PontoDeReferencia = ofertaBko.pontoDeReferencia,
                DesejaAparelho = ofertaBko.desejaAparelho,
                IdAparelho = ofertaBko.idAparelho,
                idFormaDePagamentoAparelho = ofertaBko.IdFormaDePagamentoDeAparelho,
                IdPassaporteOferta = ofertaBko.IdPassaporteOferta,
                Observacao = ofertaBko.observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

            return resultado;
        }       
    }
}