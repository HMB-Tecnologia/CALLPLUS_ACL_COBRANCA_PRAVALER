using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Dto;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ChecklistService
    {
        private readonly ChecklistDao _dao;

        public ChecklistService()
        {
            _dao = new ChecklistDao();
        }

        public DataTable Listar(int id, int idCampanha, int idProduto, string nome, bool ativo)
        {
            return _dao.Listar(id, idCampanha, idProduto, nome, ativo);
        }

        public IEnumerable<Checklist> Listar(int idCampanha, int idProduto, int ddd, bool ativo)
        {
            return _dao.Listar(-1, idCampanha, idProduto, ddd, ativo);
        }

        public IEnumerable<EtapaDoChecklist> ListarEtapas(int idChecklist, bool ativo)
        {
            return _dao.ListarEtapas(-1, idChecklist, ativo);
        }

        public IEnumerable<VariavelDoChecklist> ListarVariaveis(int idTipoDeProduto, int idCampanha)
        {
            return _dao.ListarVariaveis(idTipoDeProduto, idCampanha);
        }

        public IEnumerable<RegionalClaro> ListarRegionais()
        {
            return _dao.ListarRegionais();
        }

        public Checklist RetornarChecklist(int idChecklist)
        {
            var resultado = _dao.Listar(idChecklist, -1, -1, -1, false)?.FirstOrDefault();

            return resultado;
        }

        public EtapaDoChecklist RetornarEtapaDoChecklist(int idEtapa)
        {
            var resultado = _dao.ListarEtapas(idEtapa, -1, false)?.FirstOrDefault();

            return resultado;
        }

        public int GravarChecklist(Checklist checklist)
        {
            return _dao.GravarChecklist(checklist);
        }

        public int GravarEtapaDoChecklist(EtapaDoChecklist etapa)
        {
            return _dao.GravarEtapaDoChecklist(etapa);
        }

        public void ExcluirEtapaDoChecklist(int idEtapa)
        {
            _dao.ExcluirEtapaDoChecklist(idEtapa);
        }

        public void GravarProdutosDoChecklist(ProdutoDoChecklistDto produto)
        {
            _dao.GravarProdutosDoChecklist(produto);
        }

        public DataTable ListarProdutosDoChecklistPorCampanha(int idScript)
        {
            var resultado = _dao.ListarProdutosDoChecklistPorCampanha(idScript);
            return resultado;
        }

        public IEnumerable<Produto> ListarProdutosDoChecklist(int idScript, int idCampanha)
        {
            return _dao.ListarProdutosDoChecklist(idScript, idCampanha);
        }
    }
}