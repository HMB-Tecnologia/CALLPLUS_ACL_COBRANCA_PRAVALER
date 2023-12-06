using Callplus.CRM.Tabulador.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System.Data;


namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class LoginDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public List<string> VerificarSeUsuarioPodeAcessarSistema(string login, string senha, string maquinaUsuario, string enderecoIP, string modulo,string versaoSistema)
        {
            string sql = $"APP_CRM_LOGIN_VERIFICAR_LOGIN_SENHA ";
          
            var args = new { Login = login , Senha = senha, MaquinaUsuario = maquinaUsuario, EnderecoIP = enderecoIP, Modulo = modulo,VersaoSistema = versaoSistema};            

            var resultado = ExecutarProcedure<string>(sql, args);

            return resultado.ToList();
        }

        public int VerificarUsuarioPorLoginSenha(string login, string senha)
        {
            string sql = $"APP_CRM_LOGIN_RETORNAR_USUARIO_LOGIN_SENHA";

            var args = new
            {
                Login = login,
                Senha = senha
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public Usuario RetornarUsuario(string login)
        {
            string sql = $"APP_CRM_LOGIN_RETORNAR_USUARIO";

            var usuario = new Usuario();
            var args = new { Login = login };

            return ExecutarProcedureSingleOrDefault<Usuario>(sql, args);
        }

        public List<string> VerificarSePodeResetarSenha(string senha, string senhaNova, string senhaNova2, string login)
        {
            string sql = $"APP_CRM_RESETARSENHA_VALIDAR_SENHA";
            var args = new { Senha = senha, SenhaNova = senhaNova, SenhaNova2 = senhaNova2, Login = login };

            var resultado = ExecutarProcedure<string>(sql, args);

           
            return resultado.ToList();
        }

        public void ListarFaqDeAtendimento(int id, int idcampanha, string pergunta, string resposta, bool ativo)
        {
            throw new NotImplementedException();
        }

        public bool VerificarSenhaExpirada(string login, string senha)
        {
   
            string sql = $"APP_CRM_LOGIN_VERIFICAR_SENHA_EXPIRADA";

            var args = new { Login = login, Senha = senha};

            var expirada = ExecutarProcedureSingleOrDefault<bool>(sql, args);

            return expirada;
        }

        public void ResetarSenha(string login, string senhaNova)
        {
            string sql = $"APP_CRM_RESETARSENHA_ATUALIZAR_SENHA";

            var args = new { Login  = login, SenhaNova = senhaNova};

            ExecutarProcedure(sql, args);
        }

        public DataTable ListarSolicitacaoDeAcesso(int id, int idSupervisor, int idOperador, bool ativo)
        {
            var sql = "APP_CRM_SOLICITACAO_ACESSO_LISTAR_EXIBICAO ";
            sql += string.Format("@id = {0}, @idSupervisor = {1}, @idOperador = {2}, @liberado = {3}",
            id, idSupervisor, idOperador, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public int GravarSolicitacaoDeAcesso(SolicitacaoDeAcessoAoSistema solicitacao)
        {
            var sql = "APP_CRM_SOLICITACAO_ACESSO_GRAVAR";

            var args = new
            {
                Id = solicitacao.id,
                IdUsuarioLiberacao = solicitacao.idUsuarioLiberacao,
                Observacao = solicitacao.observacao
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }
    }
}
