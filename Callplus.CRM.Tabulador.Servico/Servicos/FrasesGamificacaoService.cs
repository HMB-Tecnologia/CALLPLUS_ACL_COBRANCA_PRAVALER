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
    public class FrasesGamificacaoService
    {
        private readonly FrasesGamificacaoDao _dao;
        public FrasesGamificacaoService()
        {
            _dao = new FrasesGamificacaoDao();
        }
        public DataTable RetornarFrases(int idGamificacao, int idTipo, bool ativo)
        {
            return _dao.RetornarFrases(idGamificacao, idTipo, ativo);
        }
        public FrasesGamificacao Retornar(int idGamificacao)
        {
            return _dao.Listar( idGamificacao, true).FirstOrDefault();
        }
        public IEnumerable<TipoDeFrasesGamificacao> ListarTipo()
        {
            return _dao.ListarTipo();
        }
    }
}
