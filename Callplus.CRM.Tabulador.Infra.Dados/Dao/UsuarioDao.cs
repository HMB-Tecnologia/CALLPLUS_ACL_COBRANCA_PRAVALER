using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{ 
    public class UsuarioDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public IEnumerable<Usuario> Listar(int id, bool ativo, int idPerfil = -1, int idCampanha = -1, int idSupervisor = -1)
        {
            //var sql = "APP_CRM_USUARIO_LISTAR";
            var sql = "APP_CRM_USUARIO_LISTAR_2";


            var args = new
            {
                Id = id,
                Ativo = ativo,
                IdPerfil = idPerfil,
                IdCampanha = idCampanha,
                IdSupervisor = idSupervisor
            };

            var resultado = ExecutarProcedure<Usuario>(sql, args);

            return resultado;
        }

        public DataTable Listar(int id, int idCampanha, int idPerfil, int idSupervisor, string nome, string login, bool ativo)
        {
            //var sql = "APP_CRM_USUARIO_LISTAR_EXIBICAO ";
            var sql = "APP_CRM_USUARIO_LISTAR_EXIBICAO_2 ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @idPerfil = {2}, @idSupervisor = {3}, @nome = '{4}', @login = '{5}', @ativo = {6}",
            id, idCampanha, idPerfil, idSupervisor, nome, login, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public int Gravar(Usuario usuario, string campanhas, int idCampanhaPrincipal)
        {
            

            var sql = "APP_CRM_USUARIO_GRAVAR_2";
            
            var args = new
            {
                Id = usuario.Id,
                IdPerfil = usuario.IdPerfil,
                IdEmpresa = usuario.IdEmpresa,
                IdSupervisor = usuario.IdSupervisor,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Login = usuario.Login,
                Senha = usuario.Senha,
                PermiteExportacao = usuario.PermiteExportacao,
                Ativo = usuario.Ativo,
                SenhaExpirada = usuario.SenhaExpirada,
                GerarNota = usuario.GerarNota,
                IdResponsavel = (usuario.Id == 0 ? usuario.IdCriador : usuario.IdModificador),
                Observacao = usuario.Observacao,
                Campanha = campanhas.Replace("-",""),
                IdCampanhaPrincipal = idCampanhaPrincipal,
                IdEscalaDeTrabalho = usuario.IdEscalaDeTrabalho,
                ReceberAvaliacaoQualidade = usuario.ReceberAvaliacaoDeQualidade,
                Cpf = usuario.CPF,
                DataNascimento = usuario.DataNascimento,
                alterarProdutoBKO = usuario.alterarProdutoBKO
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        public string GravarEmMassa(string idsUsuarios, int idPerfil, bool ativo, bool senhaExpirada, int idResponsavel, string campanhas, int idCampanhaPrincipal, int idSupervisor)
        {           
            var sql = "APP_CRM_USUARIO_GRAVAR_EM_MASSA";

            var args = new
            {
                IdsUsuarios = idsUsuarios,
                IdPerfil = idPerfil,                
                Ativo = ativo,
                SenhaExpirada = senhaExpirada,
                IdResponsavel = idResponsavel,                
                Campanha = campanhas,
                IdCampanhaPrincipal = idCampanhaPrincipal,
                IdSupervisor = idSupervisor
            };

            return ExecutarProcedureSingleOrDefault<string>(sql, args);
        }

        public IEnumerable<Usuario> ListarAuditoresAtivos(bool ativo)
        {
            var sql = "APP_CRM_USUARIO_AUDITOR_ATIVO";

            var args = new
            {
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<Usuario>(sql, args);

            return resultado;
        }

        public DataTable RetornarUsuariosIdSupervisor(int idSupervisor)
        {
            var sql = "APP_CRM_USUARIO_NOTIFICACOES_LISTAR";

            var args = new
            {
                IdSupervisor = idSupervisor
            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<string> VerificarSeLoginExiste(string login, int? id)
        {
            var sql = "APP_CRM_USUARIO_VERIFICAR_SE_PODE_GRAVAR";

            var args = new
            {
                @Login = login,
                @Id = id
            };

            var resultado = ExecutarProcedure<string>(sql, args);

            return resultado;
        }

        public IEnumerable<Empresa> ListarEmpresa(bool ativo)
        {
            var sql = "APP_CRM_EMPRESA_LISTAR";

            var args = new
            {
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<Empresa>(sql, args);

            return resultado;
        }

        public IEnumerable<EscalaDeTrabalho> ListarEscalaDeTrabalho(bool ativo)
        {
            var sql = "APP_CRM_ESCALA_TRABALHO_LISTAR";

            var args = new
            {
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<EscalaDeTrabalho>(sql, args);

            return resultado;
        }
    }
}
