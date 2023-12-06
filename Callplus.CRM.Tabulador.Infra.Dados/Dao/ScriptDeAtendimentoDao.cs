using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using Callplus.CRM.Tabulador.Dominio.Entidades;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class ScriptDeAtendimentoDao : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public DataTable ListarScriptDeAtendimento(int id, int idCampanha, int idProduto, string nome, bool ativo)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_LISTAR_EXIBICAO ";
            sql += string.Format("@id = {0}, @idCampanha = {1}, @idProduto = {2}, @nome = '{3}', @ativo = {4}",
            id, idCampanha, idProduto, nome, ativo);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<ScriptDeAtendimento> Listar(int? id, bool ativo)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_LISTAR";

            var args = new
            {
                Id = id
            };

            var resultado = ExecutarProcedure<ScriptDeAtendimento>(sql, args);

            return resultado;
        }

        public ScriptDeAtendimento RetornarScriptDeAtendimento(int idScriptAtendimento)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_LISTAR_DTO";

            var args = new
            {
                Id = idScriptAtendimento
            };

            var resultado = ExecutarProcedure<ScriptDeAtendimentoDto>(sql, args);

            //MONTA O SCRIPT
            var script = resultado
               .Select(x => new ScriptDeAtendimento()
               {
                   Id = x.IdScript,
                   Nome = x.NomeScript,
                   IdPrimeiraEtapa = x.IdPrimeiraEtapaScript,
                   Observacao = x.ObservacaoScript,
                   Ativo = x.AtivoScript,
                   Ddd = x.Ddd
               }
                ).FirstOrDefault();


            var etapas =
                resultado
                .Where(x => x.IdEtapa != null)
              .Select(x => new EtapaDoScriptDeAtendimento()
              {
                  Id = x.IdEtapa.Value,
                  DescricaoHtml = x.DescricaoHtmlEtapa,
                  Titulo = x.TituloEtapa,
                  IdScriptDeAtendimento = x.IdScriptEtapa ?? 0
              }
                    )
                    .GroupBy(x => x.Id)
                    .Select(grp => grp.FirstOrDefault())
                    .ToList();

            var respostas =
                resultado
                .Where(x => x.IdResposta != null)
                    .Select(x => new RespostaDaEtapaDoScriptDeAtendimento()
                    {
                        Id = x.IdResposta.Value,
                        Descricao = x.DescricaoResposta,
                        IdEtapaDoScriptDeAtendimento = x.IdEtapaResposta ?? 0,
                        Ativo = x.AtivoResposta ?? false,
                        RespostaAutomatica =  x.RespostaAutomatica,
                        IdProximaEtapaDoScriptDeAtendimento = x.IdProximaEtapaResposta
                    }
                    )
                    .GroupBy(x => x.Id)
                    .Select(grp => grp.FirstOrDefault())
                    .ToList();
            
            foreach (EtapaDoScriptDeAtendimento etapa in etapas)
            {
                etapa.Respostas = respostas.Where(x => x.IdEtapaDoScriptDeAtendimento == etapa.Id).ToList();
               
            }

            foreach (var resposta in respostas)
            {
                resposta.ProximaEtapa =
                    etapas.FirstOrDefault(x => x.Id == resposta.IdProximaEtapaDoScriptDeAtendimento);
            }

            script.Etapas = etapas
                .Where(x => x.IdScriptDeAtendimento == script.Id)
                .ToList();            

            return script;
        }
        
        public IEnumerable<EtapaDoScriptDeAtendimento> ListarEtapasDoScriptDeAtendimento(int? idEtapa, int? idScriptAtendimento)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_LISTAR_ETAPA";

            var args = new
            {
                Id = idEtapa ?? -1,
                IdScriptDeAtendimento = idScriptAtendimento ?? -1
            };

            var resultado = ExecutarProcedure<EtapaDoScriptDeAtendimento>(sql, args);

            //foreach (EtapaDoScriptDeAtendimento etapa in resultado)
            //{
            //    etapa.Respostas = ListarRespostasDaEtapaDoScriptDeAtendimento(idResposta: null, idEtapaDoScriptDeAtendimento: etapa.Id);
            //}

            return resultado;
        }

        public IEnumerable<RespostaDaEtapaDoScriptDeAtendimento> ListarRespostasDaEtapaDoScriptDeAtendimento(int? idResposta, int? idEtapaDoScriptDeAtendimento)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_LISTAR_RESPOSTA_DA_ETAPA";

            var args = new
            {
                Id = idResposta ?? -1,
                IdEtapaDoScriptDeAtendimento = idEtapaDoScriptDeAtendimento ?? -1
            };

            var resultado = ExecutarProcedure<RespostaDaEtapaDoScriptDeAtendimento>(sql, args);

            //foreach (RespostaDaEtapaDoScriptDeAtendimento resposta in resultado)
            //{
            //    if (resposta.IdProximaEtapaDoScriptDeAtendimento != null)
            //        resposta.ProximaEtapa = RetornarEtapaDoScriptDeAtendimento(resposta.IdProximaEtapaDoScriptDeAtendimento.Value);
            //}

            return resultado;
        }

        public IEnumerable<VariavelDoScriptDeAtendimento> ListarVariaveis(int idCampanha)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_LISTAR_VARIAVEL";
            var args = new { IdCampanha = idCampanha };
            var resultado = ExecutarProcedure<VariavelDoScriptDeAtendimento>(sql, args);
            return resultado;
        }

        public int GravarScriptDeAtendimento(ScriptDeAtendimento script)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_GRAVAR";

            var args = new
            {
                Id = script.Id,
                IdPrimeiraEtapa = script.IdPrimeiraEtapa,
                Nome = script.Nome,
                Ativo = script.Ativo,
                IdUsuario = (script.Id == 0 ? script.IdCriador : script.IdModificador),
                Observacao = script.Observacao,
                Ddd = script.Ddd
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public int GravarEtapaDoScriptDeAtendimento(EtapaDoScriptDeAtendimento etapa)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_GRAVAR_ETAPA";

            var args = new
            {
                Id = etapa.Id,
                IdScriptDeAtendimento = etapa.IdScriptDeAtendimento,
                Titulo = etapa.Titulo,
                DescricaoHtml = etapa.DescricaoHtml,
                Ativo = etapa.Ativo
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public int GravarRespostaDaEtapaDoScriptDeAtendimento(RespostaDaEtapaDoScriptDeAtendimento resposta)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_GRAVAR_RESPOSTA_DA_ETAPA";

            var args = new
            {
                Id = resposta.Id,
                IdEtapaDoScriptDeAtendimento = resposta.IdEtapaDoScriptDeAtendimento,
                IdProximaEtapaDoScriptDeAtendimento = resposta.IdProximaEtapaDoScriptDeAtendimento,
                Descricao = resposta.Descricao,
                Ativo = resposta.Ativo,
                RespostaAutomatica = resposta.RespostaAutomatica
            };

            var resultado = ExecutarProcedureSingleOrDefault<int>(sql, args);
            return resultado;
        }

        public void GravarProdutosDoScriptDeAtendimento(ProdutoDoScriptDeAtendimentoDto produto)
        {
            var sql = "APP_CRM_SCRIPT_DE_ATENDIMENTO_GRAVAR_VINCULO";

            var args = new
            {
                IdScript = produto.IdScriptDeAtendimento,
                Produtos = produto.Produtos,
                IdCampanha = produto.IdCampanha,
                Apresentacao = produto.Apresentacao,
                Finalizacao = produto.Finalizacao
            };

            ExecutarProcedure(sql, args);
        }

        public void ExcluirEtapaDoScriptDeAtendimento(int idEtapa)
        {
            var sql = "DELETE EtapaDoScriptDeAtendimento WHERE id = " + idEtapa;

            var args = new
            {
                
            };

            ExecutarSql(sql, args);
        }

        public void ExcluirRespostaDaEtapaDoScriptDeAtendimento(int idResposta)
        {
            var sql = "DELETE RespostaDaEtapaDoScriptDeAtendimento WHERE id = " + idResposta;

            var args = new
            {

            };

            ExecutarSql(sql, args);
        }

        public DataTable ListarProdutosDoScriptPorCampanha(int idScript)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_DO_SCRIPT_DE_ATENDIMENTO_POR_CAMPANHA ";
            sql += string.Format("@idScript = {0}", idScript);

            var args = new
            {

            };

            var resultado = CarregarDataTable(sql, args);

            return resultado;
        }

        public IEnumerable<Produto> ListarProdutosDoScript(int idScript, int idCampanha)
        {
            var sql = "APP_CRM_PRODUTO_LISTAR_PRODUTOS_DO_SCRIPT_DE_ATENDIMENTO";

            var args = new
            {
                IdScript = idScript,
                IdCampanha = idCampanha
            };

            var resultado = ExecutarProcedure<Produto>(sql, args);

            return resultado;
        }
    }
}