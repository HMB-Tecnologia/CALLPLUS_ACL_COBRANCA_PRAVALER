using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class NotificacaoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable ListarExibicao(int id, DateTime dataInicio, DateTime dataTermino, bool ativo)
        {
            var sql = "APP_CRM_NOTIFICACAO_LISTAR_EXIBICAO";

            sql += string.Format(" @id = {0}, @dataInicio = '{1}', @dataTermino = '{2}', @ativo = {3}",
                id, dataInicio.ToString("yyyy-MM-dd"), dataTermino.ToString("yyyy-MM-dd 23:59:59"), ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Notificacao> Listar(int id)
        {
            var sql = "APP_CRM_NOTIFICACAO_LISTAR ";
            var args = new
            {
                id = id
            };

            var resultado = ExecutarProcedure<Notificacao>(sql, args);

            return resultado;
        }

        public int Gravar(Notificacao notificacao, string idsUsuarios)
        {
            var sql = "APP_CRM_NOTIFICACAO_GRAVAR";

            var args = new
            {
                Id = notificacao.Id,
                Titulo = notificacao.Titulo,
                Mensagem = notificacao.Mensagem,
                DataInicio = notificacao.DataInicio,
                DataTermino = notificacao.DataTermino,
                IdSupervisor = notificacao.IdSupervisor,
                Ativo = notificacao.Ativo,
                IdUsuario = (notificacao.Id == 0 ? notificacao.IdCriador : notificacao.IdModificador),
                idsUsuarios
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        public IEnumerable<Usuario> ListarOperadoresNotificados(int idNotificacao, int idSupervisor)
        {
            var sql = "APP_CRM_NOTIFICACAO_LISTAR_OPERADOR_NOTIFICADO";

            var args = new
            {
                IdNotificacao = idNotificacao,
                IdSupervisor = idSupervisor
            };

            var resultado = ExecutarProcedure<Usuario>(sql, args);

            return resultado;

        }

        public DataTable ListarHistoricoExibicao(int idNotificacao)
        {
            var sql = "APP_CRM_NOTIFICACAO_LISTAR_HISTORICO";

            sql += string.Format(" @idNotificacao = {0}", idNotificacao);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }
        
        public IEnumerable<Notificacao> VerificarNotificacaoDoUsuario(int idUsuario)
        {
            var sql = "APP_CRM_NOTIFICACAO_VERIFICAR_POR_USUARIO ";

            var args = new
            {
                IdUsuario = idUsuario
            };

            var resultado = ExecutarProcedure<Notificacao>(sql, args);

            return resultado;
        }

        #region HISTORICO_DE_LEITURA

        public int GravarHistoricoDeLeitura(HistoricoLeitura historico)
        {
            var sql = "APP_CRM_NOTIFICACAO_GRAVAR_HISTORICO_DE_LEITURA";
            var args = new
            {
                IdNotificacao = historico.IdNotificacao,
                IdUsuario = historico.IdUsuario
            };

            return ExecutarProcedureSingleOrDefault<int>(sql, args);
        }

        #endregion HISTORICO_DE_LEITURA
    }
}
