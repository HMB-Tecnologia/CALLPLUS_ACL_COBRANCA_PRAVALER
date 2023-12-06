using Callplus.CRM.Tabulador.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class NotificacaoService
    {

        private readonly NotificacaoDao _notificacaoDao;

        public NotificacaoService()
        {
            _notificacaoDao = new NotificacaoDao();
        }

        public int GravarNotificacao(Notificacao notificacao, string idsUsuarios)
        {
            return _notificacaoDao.Gravar(notificacao, idsUsuarios);
        }

        public DataTable ListarExibicao(int id, DateTime dataInicio, DateTime dataTermino, bool ativo)
        {
            return _notificacaoDao.ListarExibicao(id, dataInicio, dataTermino, ativo);
        }

        public Notificacao Retornar(int id)
        {
            return _notificacaoDao.Listar(id).FirstOrDefault();
        }

        public IEnumerable<Notificacao> VerificarNotificacaoDoUsuario(int idUsuario)
        {
            return _notificacaoDao.VerificarNotificacaoDoUsuario(idUsuario);
        }

        public int GravarHistoricoDeLeitura(HistoricoLeitura historico)
        {
            return _notificacaoDao.GravarHistoricoDeLeitura(historico);
        }

        public DataTable Listar(int id)
        {
            return _notificacaoDao.ListarHistoricoExibicao(id);
        }

        public IEnumerable<Usuario> ListarOperadoresNotificados(int idNotificacao, int idSupervisor)
        {
            return _notificacaoDao.ListarOperadoresNotificados(idNotificacao, idSupervisor);
        }
    }
}
