using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class FaixasDeRecargaService
    {
        private readonly FaixasDeRecargaDao _faixasDeRecargaDao;

        public FaixasDeRecargaService()
        {
            _faixasDeRecargaDao = new FaixasDeRecargaDao();
        }

        public IEnumerable<FaixaDeRecarga> ListarFaixasDeRecarga(int? id, bool? ativo)
        {
            return _faixasDeRecargaDao.ListarFaixasDeRecarga(id, ativo);
        }

        public FaixaDeRecarga Retornar(int id)
        {
            return _faixasDeRecargaDao.Listar(id).FirstOrDefault();
        }

        public IEnumerable<ProdutoPermitidoParaFaixaDeRecarga> ListarFaixasDeRecargaDoProduto(long idProduto)
        {
            return _faixasDeRecargaDao.ListarFaixasDeRecargaDoProduto(idProduto);
        }



        public int Salvar(ProdutoPermitidoParaFaixaDeRecarga produtoPermitidoParaFaixaDeRecarga)
        {
            return _faixasDeRecargaDao.Salvar(produtoPermitidoParaFaixaDeRecarga);
        }

        public DataTable ListarFaixasDeRecargaExistentes(int? id, string nome, bool? ativo)
        {
            return _faixasDeRecargaDao.ListarFaixasDeRecargaExistentes(id, nome, ativo);
        }

        public string GravarFaixaDeRecarga(FaixaDeRecarga faixa)
        {
            return _faixasDeRecargaDao.GravarFaixaDeRecarga(faixa);
        }
    }
}
