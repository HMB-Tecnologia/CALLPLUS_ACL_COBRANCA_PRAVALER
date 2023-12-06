using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using System;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class PermissaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        #region ENDEREÇO
        public IEnumerable<string> PodeLiberarEdicaoManual(string loginSupervisor, string senhaSupervisor, int idUsuario)
        {
            var sql = "APP_CRM_PERMISSAO_LIBERAR_EDICAO_MANUAL";
            var args = new { LoginSupervisor = loginSupervisor, SenhaSupervisor = senhaSupervisor, IdUsuario = idUsuario };

            var resultado = ExecutarProcedure<string>(sql, args);
            return resultado;
        }
        #endregion ENDEREÇO

        public IEnumerable<PermissaoDoUsuario> PermissoesDoUsuarioListar(int? id, int? idUsuario, int? idCampanha)
        {
            List<PermissaoDoUsuario> permissoes = new List<PermissaoDoUsuario>();

            var sql = "APP_CRM_PERMISSOES_DO_USUARIO_LISTAR";
            var args = new { Id = id, IdUsuario = idUsuario, IdCampanha = idCampanha };

            var resultado = ExecutarProcedure<PermissoesDoUsuarioDTO>(sql, args);

            foreach (PermissoesDoUsuarioDTO permissoesDto in resultado)
            {
                var permissaoUsuario = new PermissaoDoUsuario();
                permissaoUsuario.Id = permissoesDto.IdPermissaoUsuario;
                permissaoUsuario.DataCriacao = permissoesDto.DataCriacaoPermissaoUsuario;
                permissaoUsuario.IdCampanha = permissoesDto.IdCampanha;
                permissaoUsuario.IdUsuario = permissoesDto.IdUsuario;
                permissaoUsuario.IdCriador = permissoesDto.IdCriadorPermissaoUsuario;
                permissaoUsuario.IdPermissao = permissoesDto.IdPermissao;

                var permissao = new Permissao();
                permissao.Id = permissoesDto.IdPermissao;
                permissao.Nome = permissoesDto.NomePermissao;
                permissao.DataCriacao = permissoesDto.DataCriacaoPermissao;

                permissaoUsuario.Permissao = permissao;
                permissoes.Add(permissaoUsuario);
            }


            return permissoes;
        }

        public IEnumerable<string> VerificarPermissaoPorLoginESenha(int idUsuario, string login, string senha,
            bool verificarPerfilSupervisor, bool permitePerfilAdministrador)
        {
            var sql = "APP_CRM_PERMISSAO_VERIFICAR_POR_LOGIN_E_SENHA";
            var args = new
            {
                idUsuario = idUsuario,
                login = login,
                senha = senha,
                verificarPerfilSupervisor = verificarPerfilSupervisor,
                permitePerfilAdministrador = permitePerfilAdministrador
            };

            var resultado = ExecutarProcedure<string>(sql, args);
            return resultado;

        }

        public IEnumerable<PermissaoDeAcessoMenuDTO> PermissalDeAcessoMenuListar(int idPerfil, int idUsuario)
        {
            var sql = "APP_CRM_PERMISSAO_DE_MENU_LISTAR";
            var args = new { IdPerfilUsuario = idPerfil, IdUsuario = idUsuario};

            var resultado = ExecutarProcedure<PermissaoDeAcessoMenuDTO>(sql, args);
            return resultado;
        }

        public bool QuantidadeVendaParaNaoTitularExcedida(int idUsuario, int idCampanha)
        {
            var sql = "APP_CRM_PERMISSAO_VERIFICAR_QUANTIDADE_MUDANCA_TITULARIDADE_EXCEDIDA";
            var args = new { IdUsuario = idUsuario, IdCampanha = idCampanha };

            var resultado = (bool)ExecuteProcedureScalar(sql, args);
            return resultado;
        }
    }
}