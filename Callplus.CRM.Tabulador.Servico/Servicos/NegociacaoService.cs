using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
	public class NegociacaoService
	{
		private readonly NegociacaoDao _Dao;

		public NegociacaoService()
		{
			_Dao = new NegociacaoDao();
		}

		public IEnumerable<Negociacao> Listar(long id, bool baixado)
		{
			return _Dao.Listar(id, baixado);
		}

		public DataTable ListarExibicao(int idRegistro, int idCampanha, string nome, bool ativo)
		{
			return _Dao.ListarExibicao(idRegistro, idCampanha, nome, ativo);
		}

		public int Gravar(Negociacao contrato)
		{
			return _Dao.Gravar(contrato);
		}

		public Negociacao RetornarContrato(long id, bool baixado)
		{
			return _Dao.RetornarContrato(id, baixado);
		}

		public long IncluirNegociacao(Negociacao negociacao)
		{
			return _Dao.IncluirNegociacao(negociacao);
		}

		public void IncluirTituloNegociacao(TituloNegociacao tituloNegociacao)
		{
			_Dao.IncluirTituloNegociacao(tituloNegociacao);
		}

		public void IncluirParcelaNegociacao(Parcela parcela, long idNegociacao, int idUsuario)
		{
			_Dao.IncluirParcelaNegociacao(parcela, idNegociacao, idUsuario);
		}

		public DataTable RetornarHistoricoNegociacaoPorIdContrato(long idContrato)
		{
			return _Dao.RetornarHistoricoNegociacaoPorIdContrato(idContrato);
		}

		public IEnumerable<string> PodeIncluirNegociacao(long idContrato, int idUsuario)
		{
			return _Dao.PodeIncluirNegociacao(idContrato, idUsuario);
		}

		public DataTable RetornarTipoAcordo(bool ativo)
		{
			return _Dao.RetornarTipoAcordo(ativo);
		}

		public DataTable RetornarPrazoNegociacao(bool ativo)
		{
			return _Dao.RetornarPrazoNegociacao(ativo);
		}

		public bool VerificarSeExisteAcordo(long iDTitulo)
		{
			return _Dao.VerificarSeExisteAcordo(iDTitulo);
		}
	}
}
