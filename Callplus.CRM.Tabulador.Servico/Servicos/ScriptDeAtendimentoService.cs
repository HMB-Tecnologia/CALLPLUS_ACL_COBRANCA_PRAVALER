using System.Collections.Generic;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Dto;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ScriptDeAtendimentoService
    {
        private readonly ScriptDeAtendimentoDao _scrScriptDeAtendimentoDao;

        public ScriptDeAtendimentoService()
        {
            _scrScriptDeAtendimentoDao = new ScriptDeAtendimentoDao();
        }
        
        public ScriptDeAtendimento RetornarScriptDeAtendimento(int idScriptAtendimento)
        {
            return _scrScriptDeAtendimentoDao.RetornarScriptDeAtendimento(idScriptAtendimento);
        }

        public DataTable ListarScriptDeAtendimento(int id, int idCampanha, int idProduto, string nome, bool ativo)
        {
            var resultado = _scrScriptDeAtendimentoDao.ListarScriptDeAtendimento(id, idCampanha, idProduto, nome, ativo);
            return resultado;
        }

        public IEnumerable<ScriptDeAtendimento> Listar(int? id, bool ativo)
        {
            return _scrScriptDeAtendimentoDao.Listar(id, ativo);
        }

        public EtapaDoScriptDeAtendimento RetornarEtapaDoScriptDeAtendimento(int idEtapa)
        {
            var resultado = ListarEtapasDoScriptDeAtendimento(idEtapa, null)?.FirstOrDefault();
            return resultado;
        }

        public IEnumerable<EtapaDoScriptDeAtendimento> ListarEtapasDoScriptDeAtendimento(int? idEtapa, int? idScriptAtendimento)
        {
            return _scrScriptDeAtendimentoDao.ListarEtapasDoScriptDeAtendimento(idEtapa, idScriptAtendimento);
        }

        public RespostaDaEtapaDoScriptDeAtendimento RetornarRespostaDaEtapaDoScriptDeAtendimento(int idResposta)
        {
            var resultado = ListarRespostasDaEtapaDoScriptDeAtendimento(idResposta, null)?.FirstOrDefault();
            return resultado;
        }

        public IEnumerable<RespostaDaEtapaDoScriptDeAtendimento> ListarRespostasDaEtapaDoScriptDeAtendimento(int? idResposta, int? idEtapaDoScriptDeAtendimento)
        {
            return _scrScriptDeAtendimentoDao.ListarRespostasDaEtapaDoScriptDeAtendimento(idResposta, idEtapaDoScriptDeAtendimento);
        }        

        public IEnumerable<VariavelDoScriptDeAtendimento> ListarVariaveis(int idCampanha)
        {
            return _scrScriptDeAtendimentoDao.ListarVariaveis(idCampanha);
        }

        public int GravarScriptDeAtendimento(ScriptDeAtendimento script)
        {
            return _scrScriptDeAtendimentoDao.GravarScriptDeAtendimento(script);
        }

        public int GravarEtapaDoScriptDeAtendimento(EtapaDoScriptDeAtendimento etapa)
        {
            return _scrScriptDeAtendimentoDao.GravarEtapaDoScriptDeAtendimento(etapa);
        }

        public int GravarRespostaDaEtapaDoScriptDeAtendimento(RespostaDaEtapaDoScriptDeAtendimento resposta)
        {
            return _scrScriptDeAtendimentoDao.GravarRespostaDaEtapaDoScriptDeAtendimento(resposta);
        }

        public void GravarProdutosDoScriptDeAtendimento(ProdutoDoScriptDeAtendimentoDto produto)
        {
            _scrScriptDeAtendimentoDao.GravarProdutosDoScriptDeAtendimento(produto);
        }

        public void ExcluirEtapaDoScriptDeAtendimento(int idEtapa)
        {
            _scrScriptDeAtendimentoDao.ExcluirEtapaDoScriptDeAtendimento(idEtapa);
        }

        public void ExcluirRespostaDaEtapaDoScriptDeAtendimento(int idResposta)
        {
            _scrScriptDeAtendimentoDao.ExcluirRespostaDaEtapaDoScriptDeAtendimento(idResposta);
        }

        public DataTable ListarProdutosDoScriptPorCampanha(int idScript)
        {
            var resultado = _scrScriptDeAtendimentoDao.ListarProdutosDoScriptPorCampanha(idScript);
            return resultado;
        }

        public IEnumerable<Produto> ListarProdutosDoScript(int idScript, int idCampanha)
        {
            return _scrScriptDeAtendimentoDao.ListarProdutosDoScript(idScript, idCampanha);
        }
    }
}