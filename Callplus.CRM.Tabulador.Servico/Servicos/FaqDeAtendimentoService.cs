using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class FaqDeAtendimentoService
    {
        private readonly FaqDeAtendimentoDao _faqDeAtendimentoDao;

        public FaqDeAtendimentoService()
        {
            _faqDeAtendimentoDao = new FaqDeAtendimentoDao();
        }

        public List<FaqDeAtendimento> Listar(int id,int idCampanha,bool? ativo)
        {
            return _faqDeAtendimentoDao.Listar(id,idCampanha,ativo);
        }

        public DataTable ListarFaqDeAtendimentoExibicao(int id, int idCampanha)
        {
            return _faqDeAtendimentoDao.ListarFaqDeAtendimentoExibicao(id, idCampanha);
        }

        public int GravarFaqDeAtendimento(FaqDeAtendimento faqDeAtendimento)
        {
            return _faqDeAtendimentoDao.GravarFaqDeAtendimento(faqDeAtendimento);
        }

    }
}
