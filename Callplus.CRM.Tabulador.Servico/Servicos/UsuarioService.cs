using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Perfil = Callplus.CRM.Tabulador.Dominio.Tipos.Perfil;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class UsuarioService
    {
        private readonly UsuarioDao _usuarioDao;

        public UsuarioService()
        {
            _usuarioDao = new UsuarioDao();
        }

        public IEnumerable<Usuario> Listar(int id, bool ativo)
        {
            return _usuarioDao.Listar(id, ativo);
        }

        public DataTable Listar(int id, int idCampanha, int idPerfil, int idSupervisor, string nome, string login, bool ativo)
        {
            return _usuarioDao.Listar(id, idCampanha, idPerfil, idSupervisor, nome, login, ativo);
        }

        public int Gravar(Usuario usuario, string campanhas, int idCampanhaPrincipal)
        {
            return _usuarioDao.Gravar(usuario, campanhas, idCampanhaPrincipal);
        }

        public string GravarEmMassa(string idsUsuarios, int idPerfil, bool ativo, bool senhaExpirada, int idResponsavel, string campanhas, int idCampanhaPrincipal, int idSupervisor)
        {
            return _usuarioDao.GravarEmMassa(idsUsuarios, idPerfil, ativo, senhaExpirada, idResponsavel, campanhas, idCampanhaPrincipal, idSupervisor);
        }

        public IEnumerable<Empresa> ListarEmpresa(bool ativo)
        {
            return _usuarioDao.ListarEmpresa(ativo);
        }

        public IEnumerable<EscalaDeTrabalho> ListarEscalaDeTrabalho(bool ativo)
        {
            return _usuarioDao.ListarEscalaDeTrabalho(ativo);
        }

        public IEnumerable<Usuario> ListarOperadores(bool ativo, int idCampanha = -1, int idSupervisor = -1)
        {
            int id = -1;
            int idPerfil = (int)Perfil.OPERADOR;
            return _usuarioDao.Listar(id, ativo, idPerfil, idCampanha, idSupervisor: idSupervisor);
        }

        public IEnumerable<Usuario> ListarAuditoresAtivos(bool ativo)
        {
            return _usuarioDao.ListarAuditoresAtivos(ativo);
        }

        public IEnumerable<Usuario> ListarSupervisores(bool ativo, int idCampanha = -1)
        {
            int id = -1;
            int idPerfil = (int)Perfil.SUPERVISOR;
            return _usuarioDao.Listar(id, ativo, idPerfil, idCampanha);
        }

        public IEnumerable<Usuario> ListarAuditores(bool ativo, int idCampanha = -1)
        {
            int id = -1;
            int idPerfil = (int)Perfil.BACKOFFICE;
            return _usuarioDao.Listar(id, ativo, idPerfil, idCampanha);
        }

        public IEnumerable<Usuario> ListarAvaliadores(bool ativo, int idPerfil, int idCampanha)
        {
            int id = -1;
            return _usuarioDao.Listar(id, ativo, idPerfil, idCampanha).Where(x => x.IdPerfil != 2 && x.IdPerfil != 4);
        }
        public IEnumerable<Usuario> ListarAgentes(bool ativo, int idPerfil, int idCampanha)
        {
            int id = -1;
            return _usuarioDao.Listar(id, ativo, idPerfil, idCampanha).Where(x => x.IdPerfil == 2);
        }

        public IEnumerable<string> VerificarSeLoginExiste(string login, int? id)
        {
            return _usuarioDao.VerificarSeLoginExiste(login, id);
        }

        public DataTable RetornarUsuariosIdSupervisor(int idSupervisor)
        {
            return _usuarioDao.RetornarUsuariosIdSupervisor(idSupervisor);
        }

    }
}
