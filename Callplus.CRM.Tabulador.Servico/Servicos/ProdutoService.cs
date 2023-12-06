using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System.Data;
using System.Linq;
using System;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ProdutoService
    {
        private readonly ProdutoDao _produtoDao;

        public ProdutoService()
        {
            _produtoDao = new ProdutoDao();
        }

        public DataTable Listar(int id, int idCampanha, int idTipoDeProduto, string nome, bool ativo)
        {
            return _produtoDao.Listar(id, idCampanha, idTipoDeProduto, nome, ativo);
        }

        public IEnumerable<Produto> Listar(int id, int idCampanha, int idTipoDeProduto, bool ativo)
        {
            return _produtoDao.Listar(id, idCampanha, idTipoDeProduto, ativo);
        }

        public Produto RetornarProduto(int id)
        {
            return _produtoDao.Listar(id, -1, -1, false).FirstOrDefault();
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOferta(long idAtendimento, bool? ativo, bool? ativoBko)
        {
            return _produtoDao.ListarProdutoDaOferta(idAtendimento, ativo, ativoBko);
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOfertaPorFaixaDeRecarga(long idAtendimento)
        {
            return _produtoDao.ListarProdutoDaOfertaPorFaixaDeRecarga(idAtendimento);
        }

        public IEnumerable<ProdutoDaOfertaDto> ListarProdutoDaOfertaPorNome(long idCampanha, string nome, bool? ativo, bool? ativoBko)
        {
            return _produtoDao.ListarProdutoDaOfertaPorNome(idCampanha, nome, ativo, ativoBko);
        }

        public IEnumerable<TipoDeProduto> ListarTipoDeProduto(bool ativo)
        {
            return _produtoDao.ListarTipoDeProduto(ativo);
        }

        public IEnumerable<Produto> ListarProdutosDaCampanha(int idCampanha)
        {
            return _produtoDao.ListarProdutosDaCampanha(idCampanha);
        }
        public int Gravar(Produto produto)
        {
            return _produtoDao.Gravar(produto);
        }

    }
}
