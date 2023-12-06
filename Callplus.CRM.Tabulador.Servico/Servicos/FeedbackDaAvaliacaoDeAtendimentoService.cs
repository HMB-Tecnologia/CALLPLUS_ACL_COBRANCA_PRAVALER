using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class FeedbackDaAvaliacaoDeAtendimentoService
    {
        private readonly FeedbackDaAvaliacaoDeAtendimentoDao _dao;

        public FeedbackDaAvaliacaoDeAtendimentoService()
        {
            _dao = new FeedbackDaAvaliacaoDeAtendimentoDao();
        }

        public DataTable Listar(long id, int idCampanha, DateTime dataInicial, DateTime dataFinal, int idStatus, int idAuditor)
        {
            return _dao.Listar(id, idCampanha, dataInicial, dataFinal, idStatus, idAuditor);
        }

        public IEnumerable<FeedbackDaAvaliacaoDeAtendimento> Listar(bool ativo)
        {
            return _dao.Listar(-1, ativo);
        }

        public long Gravar(FeedbackDaAvaliacaoDeAtendimento item)
        {
            return _dao.Gravar(item);
        }

        public void Excluir(long idAvaliacao)
        {
            _dao.Excluir(idAvaliacao);
        }
    }
}
