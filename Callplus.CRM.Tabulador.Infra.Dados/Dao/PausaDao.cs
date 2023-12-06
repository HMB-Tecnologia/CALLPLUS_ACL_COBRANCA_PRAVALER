using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class PausaDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexaoUtil();

        public IEnumerable<ArquivoDePausa> Listar(int id, bool ativo)
        {
            var sql = "APP_CRM_PAUSA_LISTAR";

            var args = new
            {
                Id = id,
                Ativo = ativo
            };

            var resultado = ExecutarProcedure<ArquivoDePausa>(sql, args);

            return resultado;
        }

        public DataTable Listar(int id, string nome, bool ativo)
        {
            var sql = $"APP_CRM_PAUSA_LISTAR_EXIBICAO @id = {id}, @nome = '{nome}', @ativo = '{ativo}'";

            var args = new
            {
            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public bool VerificarSeMailingEstaProcessadoComSucesso(int idArquivo)
        {
            var sql = "APP_CRM_PAUSA_STATUS_PROCESSAMENTO_VERIFICAR";

            var args = new
            {
                IdArquivo = idArquivo
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToBoolean(resultado);
        }

        public bool VerificarSeExisteNomeDoArquivo(string nome)
        {
            var sql = "APP_CRM_PAUSA_NOME_VERIFICAR";

            var args = new
            {
                Nome = nome
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToBoolean(resultado);
        }

        public int Gravar(ArquivoDePausa arquivoDePausa)
        {
            var sql = "APP_CRM_PAUSA_GRAVAR";

            var args = new
            {
                Id = arquivoDePausa.id,
                DataInicio = arquivoDePausa.dataInicio,
                DataTermino = arquivoDePausa.dataTermino,
                Nome = arquivoDePausa.nome,
                IdCriador = arquivoDePausa.idCriador,
                IdModificador = arquivoDePausa.idModificador,
                NomeArquivo = arquivoDePausa.nomeArquivo,
                CaminhoArquivo = arquivoDePausa.caminhoArquivo,
                Ativo = arquivoDePausa.ativo,
                Observacao = arquivoDePausa.observacao
            };

            var resultado = ExecuteProcedureScalar(sql, args);

            return Convert.ToInt32(resultado);
        }
    }
}
