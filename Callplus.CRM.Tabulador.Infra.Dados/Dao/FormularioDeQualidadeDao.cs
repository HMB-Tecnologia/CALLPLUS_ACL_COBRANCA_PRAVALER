using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class FormularioDeQualidadeDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable Listar(int id, int idCampanha, string nome, bool ativo)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_LISTAR_EXIBICAO ";

            sql += string.Format("@id = {0}, @idCampanha = {1}, @nome = '{2}', @ativo = {3}", id, idCampanha, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<FormularioDeQualidade> Listar(int id, int idCampanha, bool ativo)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_LISTAR";
            var args = new { Id = id, IdCampanha = idCampanha, Ativo = ativo };
            var resultado = ExecutarProcedure<FormularioDeQualidade>(sql, args);
            return resultado;
        }

        public DataTable ListarFaqDoProcedimento(long idAvaliacao, int idItem)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_RETORNAR_FAQ ";

            sql += string.Format("@idAvaliacao = {0}, @idItem = {1}", idAvaliacao, idItem);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarEstrutura(int idFormulario, int idCampanha)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_RETORNAR_ESTRUTURA ";

            sql += string.Format("@idCampanha = {0}, @idFormulario = {1}", idCampanha, idFormulario);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarProcedimento(int idFormulario, int idCampanha)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_RETORNAR_PROCEDIMENTO ";

            sql += string.Format("@idCampanha = {0}, @idFormulario = {1}", idCampanha, idFormulario);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarModulo(int idModulo, int idFormularioDeQualidade, bool ativo)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_RETORNAR_MODULO ";

            sql += string.Format("@idModulo = {0}, @idFormularioDeQualidade = {1}, @Ativo = {2}", idModulo, idFormularioDeQualidade, ativo);

            var args = new
            {
                
            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarItem(int idItem, int idModuloDoFormularioDeQualidade, int idFormularioDeQualidade, bool ativo)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_RETORNAR_ITEM ";

            sql += string.Format("@idItem = {0}, @idModuloDoFormularioDeQualidade = {1}, @idFormularioDeQualidade= {2}, @Ativo = {3}", idItem, idModuloDoFormularioDeQualidade, idFormularioDeQualidade, ativo);

            var args = new
            {
                
            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public DataTable RetornarFAQ(int idFAQ, int idProcedimento, bool ativo)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_RETORNAR_FAQ_LISTAR ";

            sql += string.Format("@idFAQ = {0}, @idProcedimento = {1}, @Ativo = {2}", idFAQ, idProcedimento, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public int Gravar(FormularioDeQualidade formulario)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_GRAVAR";

            var args = new
            {
                Id = formulario.id,
                Nome = formulario.nome,
                Ativo = formulario.ativo,
                IdCriador = (formulario.id == 0 ? formulario.idCriador : formulario.idModificador),                
                Observacoes = formulario.observacao
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public DataTable ListarCampanhasDoFormulario(int idFormulario, bool? ativo)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_LISTAR_CAMPANHAS ";

            sql += string.Format("@IdFormulario = {0}, @Ativo = {1}", idFormulario, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public int GravarModuloDoFormularioDeQualidade(ModuloDoFormularioDeQualidade moduloDoFormularioDeQualidade)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_MODULO_GRAVAR";

            var args = new
            {
                Id = moduloDoFormularioDeQualidade.Id,
                IdFormularioDeQualidade = moduloDoFormularioDeQualidade.IdFormularioDeQualidade,
                Nome = moduloDoFormularioDeQualidade.Nome,
                Valor = moduloDoFormularioDeQualidade.Valor,
                Ativo = moduloDoFormularioDeQualidade.Ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public void ExcluirModuloDoFormularioDeQualidade(int idModulo)
        {
            var sql = "DELETE ModuloDoFormularioDeQualidade WHERE id = " + idModulo;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }

        public int GravarItemDoModuloDoFormularioDeQualidade(ItemDoModuloDoFormularioDeQualidade itemDoModuloDoFormularioDeQualidade)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_ITEM_GRAVAR";

            var args = new
            {
                Id = itemDoModuloDoFormularioDeQualidade.Id,
                IdModuloDoFormularioDeQualidade = itemDoModuloDoFormularioDeQualidade.IdModuloDoFormularioDeQualidade,
                Nome = itemDoModuloDoFormularioDeQualidade.Nome,
                Descricao = itemDoModuloDoFormularioDeQualidade.Descricao,
                Peso = itemDoModuloDoFormularioDeQualidade.Peso,
                Ativo = itemDoModuloDoFormularioDeQualidade.Ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public void ExcluirItemDoModuloDoFormularioDeQualidade(int idItem)
        {
            var sql = "DELETE ItemDoModuloDoFormularioDeQualidade WHERE id = " + idItem;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }

        public int GravarProcedimentoDoItemDoFormularioDeQualidade(ProcedimentoDoItemDoFormularioDeQualidade procedimentoDoItemDoFormularioDeQualidade)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_PROCEDIMENTO_GRAVAR";

            var args = new
            {
                Id = procedimentoDoItemDoFormularioDeQualidade.Id,
                IdItemDoModuloDoFormularioDeQualidade = procedimentoDoItemDoFormularioDeQualidade.IdItemDoModuloDoFormularioDeQualidade,
                Numero = procedimentoDoItemDoFormularioDeQualidade.Numero,
                Descricao = procedimentoDoItemDoFormularioDeQualidade.Descricao,                
                Ativo = procedimentoDoItemDoFormularioDeQualidade.Ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public void ExcluirProcedimentoDoItemDoFormularioDeQualidade(int idProcedimento)
        {
            var sql = "DELETE ProcedimentoDoItemDoFormularioDeQualidade WHERE id = " + idProcedimento;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }

        public int GravarFaqDoProcedimentoDoFormularioDeQualidade(FaqDoProcedimentoDoFormularioDeQualidade faqDoProcedimentoDoFormularioDeQualidade)
        {
            var sql = "APP_CRM_FORMULARIO_QUALIDADE_FAQ_GRAVAR";

            var args = new
            {
                Id = faqDoProcedimentoDoFormularioDeQualidade.id,
                IdProcedimento = faqDoProcedimentoDoFormularioDeQualidade.idProcedimento,
                IdTipoDeAvaliacaoDeAtendimento = faqDoProcedimentoDoFormularioDeQualidade.idTipoDeAvaliacaoDeAtendimento,
                Descricao = faqDoProcedimentoDoFormularioDeQualidade.descricao 
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public void ExcluirFaqDoProcedimentoDoFormularioDeQualidade(int idFAQ)
        {
            var sql = "DELETE FaqDoProcedimentoDoFormularioDeQualidade WHERE id = " + idFAQ;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }
    }
}
