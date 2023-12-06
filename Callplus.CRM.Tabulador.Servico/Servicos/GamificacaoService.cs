using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class GamificacaoService
    {
        private readonly GamificacaoDao _dao;
        public GamificacaoService()
        {
            _dao = new GamificacaoDao();
        }
        public DataTable Listar(int id, int idCampanha, string titulo, bool ativo)
        {
            return _dao.Listar(id, idCampanha, titulo, ativo);
        }
        public Gamificacao Retornar(int idGamificacao)
        {
            return _dao.Listar(idGamificacao, -1, true).FirstOrDefault();
        }
        public DataTable ListarCampanhasDaGamificacao(int idGamificacao, bool? ativo)
        {
            return _dao.ListarCampanhasDaGamificacao(idGamificacao, ativo);
        }
        public DataTable RetornarFraseInicial(int id, int idGamificacao, int idTipo, bool ativo)
        {
            return _dao.RetornarFraseInicial(id, idGamificacao, idTipo, ativo);
        }
        public DataTable RetornarFraseFinal(int id, int idGamificacao, int idTipo, bool ativo)
        {
            return _dao.RetornarFraseFinal(id, idGamificacao, idTipo, ativo);
        }
        public DataTable RetornarFraseObjetivas(int id, int idGamificacao, int idTipo, bool ativo)
        {
            return _dao.RetornarFraseObjetivas(id, idGamificacao, idTipo, ativo);
        }
        public int GravarGamificacao(Gamificacao gamificacao)
        {
            return _dao.GravarGamificacao(gamificacao);

        }
        public int GravarFrase(FrasesGamificacao fraseMotivacional)
        {
            return _dao.GravarFrase(fraseMotivacional);

        }
        public void ExcluirFrase(int idFraseGamificacao)
        {
            _dao.ExcluirFrase(idFraseGamificacao);
        }
    }
}
