using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class LoginService
    {
        private readonly LoginDao _loginDao;

        public LoginService()
        {
            _loginDao = new LoginDao();
        }

        public List<string> VerificarSeUsuarioPodeAcessarSistema(string login, string senha, string maquinaUsuario, string enderecoIP, string modulo,string versaoSistema)
        {
            return _loginDao.VerificarSeUsuarioPodeAcessarSistema(login, senha, maquinaUsuario, enderecoIP, modulo, versaoSistema);
        }

        
        public List<string> VerificarSeSupervidorPodeAcessarSistema(string login, string senha, string maquinaUsuario, string enderecoIP, string modulo, string versaoSistema)
        {
            return _loginDao.VerificarSeSupervidorPodeAcessarSistema(login, senha, maquinaUsuario, enderecoIP, modulo, versaoSistema);
        }

        public int VerificarUsuarioPorLoginSenha(string login, string senha)
        {
            return _loginDao.VerificarUsuarioPorLoginSenha(login, senha);
        }

        public Usuario RetornarUsuario(string login)
        {
            return _loginDao.RetornarUsuario(login);
        }


        public List<string> VerificarSePodeResetarSenha(string senha, string senhaNova, string senhaNova2, string login)
        {
            return _loginDao.VerificarSePodeResetarSenha(senha, senhaNova, senhaNova2, login);
        }

        public void ResetarSenha( string login, string senhaNova)
        {
            _loginDao.ResetarSenha(login, senhaNova);
        }

        public bool VerificarSenhaExpirada(string login, string senha)
        {
            return _loginDao.VerificarSenhaExpirada(login, senha);
        }

        public async Task<DataTable> ListarSolicitacaoDeAcesso(int id, int idSupervisor, int idOperador, bool ativo)
        {
            return await _loginDao.ListarSolicitacaoDeAcesso(id, idSupervisor, idOperador, ativo);
        }

        public int GravarSolicitacaoDeAcesso(SolicitacaoDeAcessoAoSistema solicitacao)
        {
            return _loginDao.GravarSolicitacaoDeAcesso(solicitacao);
        }

        public int RealizarLogoff(int idUsuario)
        {
            return _loginDao.RealizarLogoff(idUsuario);
        }
    }
}
