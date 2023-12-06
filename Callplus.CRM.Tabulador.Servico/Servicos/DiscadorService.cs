using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{

    public class DiscadorService
    {
        private readonly DiscadorDao _discadorDao;

        public DiscadorService()
        {
            _discadorDao = new DiscadorDao();
        }

        public IEnumerable<Discador> Listar(int id, bool ativo)
        {
            return _discadorDao.Listar(id, ativo);
        }

        public Discador RetornarDiscador(int idDiscador)
        {
            return _discadorDao.RetornarDiscador(idDiscador);
        }

        public string RetornarMensagemDiscador(int idDiscador, int codRetorno)
        {
            return _discadorDao.RetornarMensagemDiscador(idDiscador, codRetorno);
        }

        public string RetornarStatusDoDiscador(int idDiscador, int codigo)
        {
            return _discadorDao.RetornarStatusDoDiscador(idDiscador, codigo);
        }

        public Discador RetornarTipoEnvioDadosDiscador(int idCampanha)
        {
            return _discadorDao.RetornarTipoEnvioDadosDiscador(idCampanha);
        }

        public string RetornarRamalUsuario(int idUsuario, int idDiscador)
        {
            return _discadorDao.RetornarRamalUsuario(idUsuario, idDiscador);
        }
    }
}
