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

		public long IncluirNegociacao(Negociacao negociacao, long idProspect, long idContrato, string cpf)
		{
			return _Dao.IncluirNegociacao(negociacao, idProspect, idContrato, cpf);
		}

		public void IncluirTituloNegociacao(TituloNegociacao tituloNegociacao)
		{
			_Dao.IncluirTituloNegociacao(tituloNegociacao);
		}

		public void IncluirParcelaNegociacao(ParcelaAcordo parcela, long idNegociacao, int idUsuario)
		{
			_Dao.IncluirParcelaNegociacao(parcela, idNegociacao, idUsuario);
		}

		public DataTable RetornarHistoricoNegociacaoPorIdProspect(long idProspect)
		{
			return _Dao.RetornarHistoricoNegociacaoPorIdProspect(idProspect);
		}

		public IEnumerable<string> PodeIncluirNegociacao(long idContrato, int idUsuario)
		{
			return _Dao.PodeIncluirNegociacao(idContrato, idUsuario);
		}

		public DataTable RetornarTipoAcordo(bool ativo)
		{
			return _Dao.RetornarTipoAcordo(ativo);
		}

		public IEnumerable<Prazo> RetornarPrazoNegociacao(bool? ativo)
		{
			return _Dao.RetornarPrazoNegociacao(ativo);
		}

		public bool VerificarSeExisteAcordo(long iDTitulo)
		{
			return _Dao.VerificarSeExisteAcordo(iDTitulo);
		}

		public IEnumerable<Parcela> RetornarParcelaNegociacao(bool ativo)
		{
			return _Dao.RetornarParcelaNegociacao(ativo);
		}
	}
}
