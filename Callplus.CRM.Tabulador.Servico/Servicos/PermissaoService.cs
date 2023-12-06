using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class PermissaoService
    {
        private static PermissaoDao _permissaoDao;

        public PermissaoService()
        {
            _permissaoDao = new PermissaoDao();
        }

        public IEnumerable<string> PodeLiberarEdicaoManual(string loginSupervisor, string senhaSupervisor, int idUsuario)
        {
            return _permissaoDao.PodeLiberarEdicaoManual(loginSupervisor, senhaSupervisor, idUsuario);
        }

        public IEnumerable<PermissaoDoUsuario> PermissoesDoUsuarioListar(int? id, int? idUsuario, int? idCampanha)
        {
            return _permissaoDao.PermissoesDoUsuarioListar(id, idUsuario, idCampanha);
        }

        public IEnumerable<PermissaoDeAcessoMenuDTO> PermissalDeAcessoMenuListar(int idPerfil, int idUsuario)
        {
            return _permissaoDao.PermissalDeAcessoMenuListar(idPerfil,idUsuario);
        }

        public bool QuantidadeVendaParaNaoTitularExcedida(int idUsuario, int idCampanha)
        {
            return _permissaoDao.QuantidadeVendaParaNaoTitularExcedida(idUsuario, idCampanha);
        }

        public IEnumerable<string> VerificarPermissaoPorLoginESenha(int idUsuario, string login, string senha, bool verificarPerfilSupervisor, bool permitePerfilAdministrador, bool _contatoManual = false)
        {
            return _permissaoDao.VerificarPermissaoPorLoginESenha(idUsuario, login, senha, verificarPerfilSupervisor, permitePerfilAdministrador, _contatoManual);
        }
    }
}
