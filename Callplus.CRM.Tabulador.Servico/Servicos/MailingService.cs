using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class MailingService
    {
        private readonly MailingDao _mailingDao;

        public MailingService()
        {
            _mailingDao = new MailingDao();
        }

        public IEnumerable<Mailing> Listar(int id, bool ativo)
        {
            return _mailingDao.Listar(id, ativo);
        }

        public DataTable Listar(int id, int idCampanha, string nome, bool ativo)
        {
            return _mailingDao.Listar(id, idCampanha, nome, ativo);
        }

        public int Gravar(Mailing mailing)
        {
            return _mailingDao.Gravar(mailing);
        }

        public bool VerificarSeExisteNomeDoMailing(string nome)
        {
            return _mailingDao.VerificarSeExisteNomeDoMailing(nome);
        }

        public DataTable ExportarMailing(int idMailing)
        {
            return _mailingDao.ExportarMailing(idMailing);
        }

        public bool VerificarSeMailingEstaProcessadoComSucesso(int idMailing)
        {
            return _mailingDao.VerificarSeMailingEstaProcessadoComSucesso(idMailing);
        }

        public void ExportarMailingDiscador(int idMailing)
        {
            _mailingDao.ExportarMailingDiscador(idMailing);
        }

        public IEnumerable<Mailing> Listar(int? id, int? idCampanha, bool? ativo)
        {
            return _mailingDao.Listar(id, idCampanha, ativo);
        }
    }
}
