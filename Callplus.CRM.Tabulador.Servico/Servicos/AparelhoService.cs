using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class AparelhoService
    {
        private AparelhoDao _aparelhoDao;

        public AparelhoService()
        {
            _aparelhoDao = new AparelhoDao();
        }

        public Aparelho RetornarAparelho(int id)
        {
            return _aparelhoDao.Listar(id).FirstOrDefault();
        }

        public IEnumerable<FormaDePagamentoDeAparelho> ListarFormaDePagamentoDeAparelho(int idProduto, int idAparelho)
        {
            return _aparelhoDao.ListarFormaDePagamentoDeAparelho(idProduto, idAparelho);
        }

        public IEnumerable<Aparelho> ListarAparelhosDoAtendimento(long idProspect)
        {
            return _aparelhoDao.ListarAparelhosDoAtendimento(idProspect);
        }

        public DataTable ListarExibicao(int id, int idCampanha, string nome, string grupo, bool ativo)
        {
            return _aparelhoDao.ListarExibicao(id, idCampanha, nome, grupo, ativo);
        }

        public IEnumerable<AparelhoDaCampanha> ListarAparelhosDaCampanha(long idAparelho)
        {
            return _aparelhoDao.ListarAparelhosDaCampanha(idAparelho);
        }

        public IEnumerable<FormaDePagamentoDeAparelho> ListarPagamentosDoAparelho(int idAparelho, int idProduto)
        {
            return _aparelhoDao.ListarPagamentosDoAparelho(idAparelho, idProduto);
        }

        public AparelhoDaCampanha RetornarCampanhaDoAparelho(int idAparelhoDaCampanha)
        {
            var resultado = CarregarCampanhaDoAparelho(idAparelhoDaCampanha)?.FirstOrDefault();
            return resultado;
        }

        public IEnumerable<AparelhoDaCampanha> CarregarCampanhaDoAparelho(int? idAparelhoDaCampanha)
        {
            return _aparelhoDao.CarregarCampanhaDoAparelho(idAparelhoDaCampanha);
        }

        public FormaDePagamentoDeAparelho RetornarFormaDePagamentoDoAparelho(int idFormaDePagamentoDoAparelho)
        {
            var resultado = CarregarFormaDePagamentoDoAparelho(idFormaDePagamentoDoAparelho)?.FirstOrDefault();
            return resultado;
        }

        public IEnumerable<FormaDePagamentoDeAparelho> CarregarFormaDePagamentoDoAparelho(int? idFormaDePagamentoDoAparelho)
        {
            return _aparelhoDao.CarregarFormaDePagamentoDoAparelho(idFormaDePagamentoDoAparelho);
        }

        public int GravarAparelho(Aparelho aparelho)
        {
            return _aparelhoDao.GravarAparelho(aparelho);
        }

        public int GravarCampanhaDoAparelho(AparelhoDaCampanha campanhaDoAparelho)
        {
            return _aparelhoDao.GravarCampanhaDoAparelho(campanhaDoAparelho);
        }

        public int GravarFormaDePagamentoDoAparelho(FormaDePagamentoDeAparelho formaDePagamentoDoAparelho)
        {
            return _aparelhoDao.GravarFormaDePagamentoDoAparelho(formaDePagamentoDoAparelho);
        }
    }
}
