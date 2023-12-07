using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
	public class TituloService
	{
        private readonly TituloDao _Dao;

        public TituloService()
        {
            _Dao = new TituloDao();
        }

        public DataTable Listar(long idRegistro, bool ativo)
        {
            return _Dao.Listar(idRegistro, ativo);
        }

        public DataTable ListarExibicao(int idRegistro, int idCampanha, string nome, bool ativo)
        {
            return _Dao.ListarExibicao(idRegistro, idCampanha, nome, ativo);
        }

        public int Gravar(long idProspect, List<Titulo> listaTitulos)
        {
            return _Dao.Gravar(idProspect, listaTitulos);
        }

        public Titulo RetornarTitulo(int id)
        {
            return _Dao.RetornarTitulo(id);
        }

		public void AtualizarStatusDoTitulo(MarcacaoStatusTitulo marcacao)
		{
            _Dao.AtualizarStatusDoTitulo(marcacao);
		}

		public List<string> PodeAtualizarStatusDoTitulo(long idTitulo, int idStatusTitulo, long idStatusAtendimento, int idUsuario, DateTime dataVencimento, DateTime dataNegociacaoFutura, DateTime dataVencimentoAtualizada)
		{
			return _Dao.PodeAtualizarStatusDoTitulo(idTitulo, idStatusTitulo, idStatusAtendimento, idUsuario, dataVencimento, dataNegociacaoFutura, dataVencimentoAtualizada);

		}
	}
}
