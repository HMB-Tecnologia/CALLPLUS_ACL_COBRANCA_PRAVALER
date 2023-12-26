using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
	public class AcordoDoAtendimentoDao : DaoBase
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

		public OfertaDoAtendimento RetornarOfertaElegivelParaAtendimento(long idAtendimento, int idCampanha, int idMailing)
		{
			var sql = "APP_CRM_COBRANCA_DO_ATENDIMENTO_RETORNAR_OFERTA_ELEGIVEL";

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

		public CobrancaAtendimentoPravaler RetornarOfertaDoAtendimentoPreVendaMigracao(long id)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_PRE_VENDA_LISTAR";

			var args = new
			{
				Id = id
			};

			var resultado = ExecutarProcedure<CobrancaAtendimentoPravaler>(sql, args).FirstOrDefault();

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

		public ResumoDoAcordoDoAtendimentoBkoDTO RetornarAcordoDoAtendimentoBKO(long id, int idTipoDeProduto)
		{
			var sql = "APP_CRM_ACORDO_DO_ATENDIMENTO_BKO_RETORNAR_RESUMO";

			var args = new
			{
				Id = id,
				IdTipoDeProduto = idTipoDeProduto
			};

			var resultado = ExecutarProcedure<ResumoDoAcordoDoAtendimentoBkoDTO>(sql, args).FirstOrDefault();

			return resultado;
		}

		public IEnumerable<ConfiguracaoVencimentoFaturaDto> RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(bool exibirTodasAsDatas)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_LISTAR_VENCIMENTO_FATURA";
			var args = new
			{
				ExibirTodasAsDatas = exibirTodasAsDatas
			};

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

		public DataTable RetornarOfertaParaAvaliacao(int idCampanha, int idSupervisor, int idOperador, string dataInicial, string dataFinal, string idStatus, long? iDOferta)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_RETORNAR_OFERTA_PARA_AVALIACAO ";

			if (iDOferta == null)
				sql += string.Format("@IdCampanha = {0}, @IdSupervisor = {1}, @IdOperador = {2}, @DataInicial = '{3}', @DataFinal = '{4}', @idStatus = '{5}', @idoferta = {6} ",
								idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus, "null");
			else
				sql += string.Format("@IdCampanha = {0}, @IdSupervisor = {1}, @IdOperador = {2}, @DataInicial = '{3}', @DataFinal = '{4}', @idStatus = '{5}', @idoferta = {6}",
								idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus, iDOferta);

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

		public IEnumerable<Usuario> ValidarUsuarioPermitidoParaAlterarProduto(string login, string senha)
		{
			var sql = "APP_CRM_ATENDIMENTO_BKO_VALIDAR_USUARIO_PARA_ALTERAR_PRODUTO";

			var args = new
			{
				Login = login,
				Senha = senha
			};

			var resultado = ExecutarProcedure<Usuario>(sql, args);

			return resultado;
		}


		#region PRAVALER
		public long GravarAcordoDoAtendimentoPravaler(CobrancaAtendimentoPravaler acordo)
		{
			var sql = "APP_CRM_ACORDO_DO_ATENDIMENTO_PRAVALER_GRAVAR";

			var args = new
			{
				acordo.Id,
				acordo.IdAtendimento,
				acordo.IdStatusDoAcordo,
				acordo.IdProduto,
				acordo.Observacao,
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

			return resultado;
		}
		#endregion PRAVALER

		#region CLARO_MIGRACAO

		public long GravarOfertaDoAtendimentoClaroMigracaoBKO(OfertaDoAtendimentoCobrancaPravalerBKO oferta)
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
				Observacao = oferta.observacao,
				receberContrato = oferta.receberContrato,
				NumeroFaturaWhatsApp = oferta.NumeroFaturaWhatsApp,
				ondeReceberContrato = oferta.ondeReceberContrato,
				Sexo = oferta.Sexo

			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
			return resultado;
		}

		public long GravarStatusDaOfertaDoAtendimentoClaroMigracao(long idOferta, long idAtendimento, long idStatus, string nome, string cpf)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_MIGRACAO_GRAVAR";

			var args = new
			{
				Id = idOferta,
				IdAtendimento = idAtendimento,
				IdStatusDaOferta = idStatus,
				Nome = nome,
				Cpf = cpf
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
			return resultado;
		}

		public CobrancaAtendimentoPravaler RetornarOfertaDoAtendimentoClaroMigracao(long id)
		{
			var sql = "APP_CRM_ACORDO_DO_ATENDIMENTO_PRAVALER_LISTAR";

			var args = new
			{
				Id = id,
				IdAtendimento = -1
			};

			var resultado = ExecutarProcedure<CobrancaAtendimentoPravaler>(sql, args).FirstOrDefault();

			return resultado;
		}

		public OfertaDoAtendimentoCobrancaPravalerBKO RetornarAcordoDoAtendimentoCobrancaPravalerBKO(long id)
		{
			var sql = "APP_CRM_ACORDO_DO_ATENDIMENTO_BKO_PRAVALER_LISTAR";

			var args = new
			{
				Id = id
			};

			var resultado = ExecutarProcedure<OfertaDoAtendimentoCobrancaPravalerBKO>(sql, args).FirstOrDefault();
			return resultado;
		}

		public long GravarHistoricoDoAtendimentoCobrancaPravalerBKO(HistoricoDoAcordoDoAtendimentoCobrancaPravalerBKO historico)
		{
			var sql = "APP_CRM_ACORDO_DO_ATENDIMENTO_BKO_PRAVALER_GRAVAR_HISTORICO";

			var args = new
			{
				Id = historico.id,
				IdAcordo = historico.idAcodoDoAtendimentoCobrancaPravalerBKO,
				IdStatusAuditoria = historico.idStatusAuditoria,
				Protocolo = historico.protocolo,
				//Autorizacao = historico.autorizacao,
				LoginWM = historico.loginWM,
				CodigoAgente = historico.codigoAgente,
				IdCriador = historico.idCriador,
				historico.Observacao
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
			return resultado;
		}

		public long GravarAlteracaoDeProdutoMigracaoBKO(long idOfertaBKO, int idProdutoInicial, int idProduto, int idUsuario)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_ALTERACAO_DE_PRODUTO_MIGRACAO_GRAVAR";

			var args = new
			{
				idOfertaBKO = idOfertaBKO,
				idProdutoInicial = idProdutoInicial,
				idProduto = idProduto,
				idUsuario = idUsuario
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
				IdStatusDaOferta = oferta.IdStatusDoAcordo,
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
		public OfertaDoAtendimentoMPPortabilidade RetornarOfertaDoAtendimentoPreVendaPortabilidade(long id)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_PRE_VENDA_LISTAR";

			var args = new
			{
				Id = id
			};

			var resultado = ExecutarProcedure<OfertaDoAtendimentoMPPortabilidade>(sql, args).FirstOrDefault();

			return resultado;
		}

		public long GravarOfertaDoAtendimentoMPPortabilidade(OfertaDoAtendimentoMPPortabilidade oferta)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_MP_PORTABILIDADE_GRAVAR ";

			var args = new
			{
				oferta.Id,
				oferta.IdAtendimento,
				oferta.IdStatusDoAcordo,
				oferta.Cust_Id,
				oferta.Nome,
				oferta.DddTel,
				oferta.TelefoneResidencial,
				oferta.DddCel,
				oferta.TelefoneCelular,
				oferta.IdBanco,
				oferta.Email,
				oferta.Cep,
				oferta.Logradouro,
				oferta.Numero,
				oferta.Complemento,
				oferta.Bairro,
				oferta.Cidade,
				oferta.Uf,
				oferta.PontoDeReferencia,
				oferta.Observacao,
				oferta.processado,
				oferta.TelefoneDaGravacao,
				oferta.TipoPessoa,
				oferta.IdStatusAuditoria
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

			return resultado;
		}

		public long GravarOfertaDoAtendimentoMPPortabilidadeBKO(OfertaDoAtendimentoMPPortabilidadeBKO oferta)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_MP_PORTABILIDADE_BKO_GRAVAR ";

			var args = new
			{
				oferta.Id,
				oferta.IdAtendimento,
				oferta.IdStatusDaOferta,
				oferta.Cust_Id,
				oferta.Nome,
				oferta.DddTel,
				oferta.TelefoneResidencial,
				oferta.DDDCel,
				oferta.TelefoneCelular,
				oferta.IdBanco,
				oferta.Email,
				oferta.Cep,
				oferta.Logradouro,
				oferta.Numero,
				oferta.Complemento,
				oferta.Bairro,
				oferta.Cidade,
				oferta.Uf,
				oferta.PontoDeReferencia,
				oferta.TipoPessoa,
				oferta.Observacao,
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

			return resultado;
		}

		public long GravarStatusDaOfertaDoAtendimentoClaroPortabilidade(long idOferta, long idAtendimento, long idStatus, string nome, string cpf, int? idBanco)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_PORTABILIDADE_GRAVAR ";

			var args = new
			{
				Id = idOferta,
				IdAtendimento = idAtendimento,
				IdStatusDaOferta = idStatus,
				Nome = nome,
				Cpf = cpf,
				IdBanco = idBanco
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);
			return resultado;
		}


		public OfertaDoAtendimentoMPPortabilidade RetornarOfertaDoAtendimentoMPPortabilidade(long id)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_CLARO_PORTABILIDADE_LISTAR";

			var args = new
			{
				Id = id,
				IdAtendimento = -1
			};

			var resultado = ExecutarProcedure<OfertaDoAtendimentoMPPortabilidade>(sql, args).FirstOrDefault();

			return resultado;
		}

		public OfertaDoAtendimentoMPPortabilidadeBKO RetornarOfertaDoAtendimentoMPPortabilidadeBKO(long id)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_CLARO_PORTABILIDADE_LISTAR";

			var args = new
			{
				id
			};

			var resultado = ExecutarProcedure<OfertaDoAtendimentoMPPortabilidadeBKO>(sql, args).FirstOrDefault();
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

		public long GravarAlteracaoDeProdutoPortabilidadeBKO(long idOfertaBKO, int idProdutoInicial, int idProduto, int idUsuario)
		{
			var sql = "APP_CRM_OFERTA_DO_ATENDIMENTO_BKO_ALTERACAO_DE_PRODUTO_PORTABILIDADE_GRAVAR";

			var args = new
			{
				idOfertaBKO = idOfertaBKO,
				idProdutoInicial = idProdutoInicial,
				idProduto = idProduto,
				idUsuario = idUsuario
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
				IdStatusDaOferta = oferta.IdStatusDoAcordo,
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

		#region WEB

		public long GravarOfertaDoAtendimentoWeb(OfertaDoAtendimentoWeb oferta)
		{
			if (oferta.DadosPessoais == null)
				oferta.DadosPessoais = new DadosPessoais();
			if (oferta.Endereco == null)
				oferta.Endereco = new Endereco();
			if (oferta.DadosOferta == null)
				oferta.DadosOferta = new DadosOferta();
			if (oferta.DadosDePagamento == null)
				oferta.DadosDePagamento = new DadosDePagamento();

			var sql = "APP_WEB_OFERTA_DO_ATENDIMENTO_GRAVAR";

			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

			var ipAddress = host
				.AddressList
				.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

			var args = new
			{
				Id = oferta.Id,
				IdCampanha = 0,
				idMailing = 0,
				idOrigemAtendimento = 0,
				idProspect = 0,
				idSupervisor = 0,
				idUsuarioPermissao = 0,
				ip = ipAddress.ToString(),
				host = System.Net.Dns.GetHostName(),

				IdStatusDoAtendimento = oferta.IdStatusDoAtendimento,

				IdProduto = oferta.DadosOferta.IdProduto,
				NumeroMigrado = oferta.DadosOferta.NumeroMigrado,
				IdProduto2 = oferta.DadosOferta.IdProduto2,
				NumeroMigrado2 = oferta.DadosOferta.NumeroMigrado2,
				FaturaDigital = oferta.DadosDePagamento.FaturaDigital,
				EmailFaturaDigital = oferta.DadosDePagamento.EmailFaturaDigital,
				DiaVencimento = oferta.DadosDePagamento.DiaVencimento,
				IdFormaDePagamento = oferta.DadosDePagamento.IdFormaDePagamento,
				IdBanco = oferta.DadosDePagamento.IdBanco,
				Agencia = oferta.DadosDePagamento.Agencia,
				Conta = oferta.DadosDePagamento.Conta,
				Nome = oferta.DadosPessoais.Nome,
				Cpf = oferta.DadosPessoais.Cpf,
				Rg = oferta.DadosPessoais.Rg,
				Nascimento = oferta.DadosPessoais.Nascimento,
				NomeDaMae = oferta.DadosPessoais.NomeDaMae,
				TelefoneCelular = oferta.DadosPessoais.TelefoneCelular,
				TelefoneResidencial = oferta.DadosPessoais.TelefoneResidencial,
				TelefoneRecado = oferta.DadosPessoais.TelefoneRecado,
				IdEstadoCivil = oferta.DadosPessoais.IdEstadoCivil,
				IdProfissao = oferta.DadosPessoais.IdProfissao,
				IdEscolaridade = oferta.DadosPessoais.IdFaixaDeRenda,
				Cep = oferta.Endereco.Cep,
				Logradouro = oferta.Endereco.Logradouro,
				Numero = oferta.Endereco.Numero,
				Complemento = oferta.Endereco.Complemento,
				Bairro = oferta.Endereco.Bairro,
				Cidade = oferta.Endereco.Cidade,
				Uf = oferta.Endereco.Estado,
				PontoDeReferencia = oferta.Endereco.PontoReferencia,
				Observacao = oferta.DadosDePagamento.Observacao,
				TitularidadeDiferente = oferta.TitularidadeDiferente,
				IdOperador = oferta.IdOperador,
				Codigo21 = oferta.codigo21,
				ReceberContrato = oferta.DadosDePagamento.receberContrato,
				NumeroFaturaWhatsApp = oferta.DadosDePagamento.NumeroFaturaWhatsApp,
				Processado = oferta.processado,
				ofertaAparelho = oferta.ofertaAparelho,
				url = oferta.DadosDePagamento.url,
				ondeReceberContrato = oferta.DadosDePagamento.ondeReceberContrato,
				Latitude = oferta.Latitude,
				Longitude = oferta.Longitude,
				ObservacaoMarcacao = oferta.ObservacaoMarcacao,
				Venda = oferta.Venda,
				etapaAtendimento = oferta.EtapaAtendimento
			};

			var resultado = ExecutarProcedureSingleOrDefault<long>(sql, args);

			return resultado;
		}

		public IEnumerable<ListarOfertaDoAtendimentoWeb> ListarOfertaDoAtendimentoWeb(int idUsuario)
		{
			var sql = "APP_WEB_OFERTA_DO_ATENDIMENTO_LISTAR";

			var args = new
			{
				idUsuario = idUsuario
			};

			var resultado = ExecutarProcedure<ListarOfertaDoAtendimentoWeb>(sql, args);

			return resultado;
		}

		public ListarOfertaDoAtendimentoWeb BuscarOfertaDoAtendimentoWeb(int id)
		{
			var sql = "APP_WEB_OFERTA_DO_ATENDIMENTO_BUSCAR";

			var args = new
			{
				id = id
			};

			var resultado = ExecutarProcedure<ListarOfertaDoAtendimentoWeb>(sql, args);

			return resultado.FirstOrDefault();
		}

		public IEnumerable<TabulacaoWeb> BuscarDadosTabulacaoWeb()
		{
			var sql = "APP_CRM_ATENDIMENTO_INICIAR_ATENDIMENTO_WEB_TESTE";

			var args = new
			{
				IdOperador = 0,
				IdSupervisor = 0,
				IdOrigemAtendimento = 0,
				Ip = string.Empty,
				Host = string.Empty,
				NumeroChamadorDiscador = string.Empty,
				IdUsuarioPermissao = 0
			};

			var resultado = ExecutarProcedure<TabulacaoWeb>(sql, args);

			return resultado;
		}

		#endregion

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