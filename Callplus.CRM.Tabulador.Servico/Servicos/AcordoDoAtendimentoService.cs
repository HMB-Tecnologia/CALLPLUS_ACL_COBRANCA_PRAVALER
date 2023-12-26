using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Dto;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class AcordoDoAtendimentoService
    {
        private readonly AcordoDoAtendimentoDao _dao;
        private readonly int _idStatusAuditoriaEmProcessamento = 21;

        public AcordoDoAtendimentoService()
        {
            _dao = new AcordoDoAtendimentoDao();
        }

        public DataTable ListarOfertaDoAtendimento(long idAtendimento)
        {
            return _dao.ListarOfertaDoAtendimento(idAtendimento);
        }

        public OfertaDoAtendimento RetornarOfertaElegivelParaAtendimento(long idAtendimento, int idCampanha, int idMailing)
        {
            return _dao.RetornarOfertaElegivelParaAtendimento(idAtendimento, idCampanha, idMailing);
        }

        public long GravarStatusDaOfertaDoAtendimento(OfertaDoAtendimento oferta, StatusDeOferta status, string nome, string cpf, int? idBanco)
        {            
            //TODO: ALTERAR PARA CONSIDERAR O TIPO DA OFERTA NÃO UTILIZANDO O ID
            //if (oferta.IdTipoDeProduto == 0) 
                return _dao.GravarStatusDaOfertaDoAtendimentoClaroMigracao(oferta.Id, oferta.IdAtendimento, status.Id, nome, cpf);

            //if (oferta.IdTipoDeProduto == 0)
            //    return _ofertaDoAtendimentoDao.GravarStatusDaOfertaDoAtendimentoClaroRentabilizacao(oferta.Id, oferta.IdAtendimento, status.Id);

            //if (oferta.IdTipoDeProduto == 0 || oferta.IdTipoDeProduto == 0 || oferta.IdTipoDeProduto == 0 || oferta.IdTipoDeProduto == 4 || oferta.IdTipoDeProduto == 5)
            //    return _ofertaDoAtendimentoDao.GravarStatusDaOfertaDoAtendimentoClaroPortabilidade(oferta.Id, oferta.IdAtendimento, status.Id, nome, cpf, idBanco);

            //if (oferta.IdTipoDeProduto == 0)
            //    return _ofertaDoAtendimentoDao.GravarStatusDaOfertaDoAtendimentoNETPTV(oferta.Id, oferta.IdAtendimento, status.Id);

            throw new InvalidOperationException("O tipo da Oferta do Atendimento não pôde ser reconhecido");
        }
        
        public string ValidarLoginWM(string login)
        {
            return _dao.ValidarLoginWM(login);
        }

        public string ValidarLoginDaOperadora(int idOperadora, string login)
        {
            return _dao.ValidarLoginDaOperadora(idOperadora, login);
        }

        public CobrancaAtendimentoPravaler RetornarOfertaDoAtendimentoPreVendaMigracao(long idOferta)
        {
            return _dao.RetornarOfertaDoAtendimentoPreVendaMigracao(idOferta);
        }

        public IEnumerable<OfertaDoAtendimento> RetornarOfertasDoAtendimento(long idAtendimento)
        {
            return _dao.RetornarOfertaDoAtendimento(idAtendimento);
        }

        public IEnumerable<string> VerificarSePodeEditarOfertaBKO(int idUsuario, long idOfertaBko, int idTipoProduto)
        {
            return _dao.VerificarSePodeEditarOfertaBKO(idUsuario,idOfertaBko,idTipoProduto);
        }

        public void RemoverHistoricoDeOfertaBkoPendente(int idUsuario, long idOfertaBko, int? idTipoProduto)
        {
             _dao.RemoverHistoricoDeOfertaBkoPendente(idUsuario, idOfertaBko, idTipoProduto);
        }

        public DataTable ListarHistoricoDaOfertaDoAtendimentoBKO(long idOfertaBko, int idTipoDeProduto)
        {
           return _dao.ListarHistoricoDaOfertaDoAtendimentoBKO(idOfertaBko, idTipoDeProduto);
        }

        public HistoricoDaOfertaDoAtendimentoBkoDTO RetornarHistoricoDaOfertaDoAtendimentoBKO_DTO(long idHistoricoBko, int idTipoDeProduto)
        {
            var item = _dao
                .ListarHistoricoDaOfertaDoAtendimentoBKO_DTO(idHistoricoBko, null, idTipoDeProduto)
                .FirstOrDefault(x => x.Id == idHistoricoBko);
            return item;

        }

        public ResumoDoAcordoDoAtendimentoBkoDTO RetornarAcordoDoAtendimentoBKO(long id, int idTipoDeProduto)
        {
            return _dao.RetornarAcordoDoAtendimentoBKO(id, idTipoDeProduto);
        }

        public IEnumerable<ConfiguracaoVencimentoFaturaDto> RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(bool exibirTodasAsDatas)
        {
            return _dao.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO(exibirTodasAsDatas);
        }

        public IEnumerable<CanalAdicional> ListarCanalAdicional(int idOperadora, int idProduto, bool ativo)
        {
            return _dao.ListarCanalAdicional(idOperadora, idProduto, ativo);
        }

        public DataTable RetornarOfertaParaAvaliacao(int idCampanha, int idSupervisor, int idOperador, string dataInicial, string dataFinal, string idStatus, long? iDOferta)
        {
            return _dao.RetornarOfertaParaAvaliacao(idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus, iDOferta);
        }

        public DataTable RetornarOfertaParaAvaliacaoBKO(long idOfertaBko)
        {
            return _dao.RetornarOfertaParaAvaliacaoBKO(idOfertaBko);
        }

        public long GravarAcordoDoAtendimentoPravaler(CobrancaAtendimentoPravaler acordo)
        {
            return _dao.GravarAcordoDoAtendimentoPravaler(acordo);
        }

        public long GravarOfertaDoAtendimentoClaroMigracaoBKO(OfertaDoAtendimentoCobrancaPravalerBKO ofertaBko)
        {
            return _dao.GravarOfertaDoAtendimentoClaroMigracaoBKO(ofertaBko);
        }

        #region CLARO_MIGRACAO

        public CobrancaAtendimentoPravaler RetornarCobrancaAtendimentoPravaler(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoClaroMigracao(id);
        }

        public OfertaDoAtendimentoCobrancaPravalerBKO RetornarAcordoDoAtendimentoCobrancaPravalerBKO(long id)
        {
            return _dao.RetornarAcordoDoAtendimentoCobrancaPravalerBKO(id);
        }

        public long GravarHistoricoDoAtendimentoCobrancaPravalerBKO(HistoricoDoAcordoDoAtendimentoCobrancaPravalerBKO oferta)
        {
            return _dao.GravarHistoricoDoAtendimentoCobrancaPravalerBKO(oferta);
        }

        public HistoricoDoAcordoDoAtendimentoCobrancaPravalerBKO IniciarAuditoriaDaOfertaClaroMigracaoBKO(int idUsuario, long idOfertaBko)
        {
            int idStatusAuditoria = _idStatusAuditoriaEmProcessamento;
            var historico = new HistoricoDoAcordoDoAtendimentoCobrancaPravalerBKO()
            {
                id = 0,
                idAcodoDoAtendimentoCobrancaPravalerBKO = idOfertaBko,
                idCriador = idUsuario,
                idStatusAuditoria = idStatusAuditoria,
                Observacao = "",
                autorizacao = "",
                loginWM = "",
                protocolo = ""
            };

            long idHistorico = GravarHistoricoDoAtendimentoCobrancaPravalerBKO(historico);
            historico.id = idHistorico;
            return historico;
        }

        public IEnumerable<Usuario> ValidarUsuarioPermitidoParaAlterarProduto(string login, string senha)
        {
            return _dao.ValidarUsuarioPermitidoParaAlterarProduto(login, senha);
        }

        public long GravarAlteracaoDeProdutoMigracaoBKO(long idOfertaBKO, int idProdutoInicial, int idProduto, int idUsuario)
        {
            return _dao.GravarAlteracaoDeProdutoMigracaoBKO(idOfertaBKO, idProdutoInicial, idProduto, idUsuario);
        }


        #endregion CLARO_MIGRACAO

        #region CLARO_RENTABILIZACAO

        public long GravarOfertaDoAtendimentoClaroRentabilizacao(OfertaDoAtendimentoClaroRentabilizacao oferta)
        {
            return _dao.GravarOfertaDoAtendimentoClaroRentabilizacao(oferta);
        }

        public OfertaDoAtendimentoClaroRentabilizacao RetornarOfertaDoAtendimentoClaroRentabilizacao(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoClaroRentabilizacao(id);
        }

        public OfertaDoAtendimentoClaroRentabilizacaoBKO RetornarOfertaDoAtendimentoClaroRentabilizacaoBKO(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoClaroRentabilizacaoBKO(id);
        }

        public long GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO oferta)
        {
            return _dao.GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(oferta);
        }

        public HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO IniciarAuditoriaDaOfertaClaroRentabilizacaoBKO(int idUsuario, long idOfertaBko)
        {
            int idStatusAuditoria = _idStatusAuditoriaEmProcessamento;
            var historico = new HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO()
            {
                id = 0,
                idOfertaDoAtendimentoRentabilizacaoBKO = idOfertaBko,
                idCriador = idUsuario,
                idStatusAuditoria = idStatusAuditoria,
                Observacao = "",
                autorizacao = "",
                loginWM = "",
                protocolo = ""
            };

            long idHistorico = GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(historico);
            historico.id = idHistorico;
            return historico;
        }

        public IEnumerable<PassaporteOferta> ListarPassaporteOferta()
        {
            return _dao.ListarPassaporteOferta();
        }

        public long GravarOfertaDoAtendimentoClaroRentabilizacaoBKO(OfertaDoAtendimentoClaroRentabilizacaoBKO ofertaBko)
        {
            return _dao.GravarOfertaDoAtendimentoClaroRentabilizacaoBKO(ofertaBko);
        }

        #endregion CLARO_RENTABILIZACAO

        #region CLARO_PORTABILIDADE

        public long GravarOfertaDoAtendimentoMPPortabilidade(OfertaDoAtendimentoMPPortabilidade oferta)
        {
            return _dao.GravarOfertaDoAtendimentoMPPortabilidade(oferta);
        }

        public OfertaDoAtendimentoMPPortabilidade RetornarOfertaDoAtendimentoPreVendaPortabilidade(long idAtendimento)
        {
            return _dao.RetornarOfertaDoAtendimentoPreVendaPortabilidade(idAtendimento);
        }

        public long GravarOfertaDoAtendimentoMPPortabilidadeBKO(OfertaDoAtendimentoMPPortabilidadeBKO ofertaBko)
        {
            return _dao.GravarOfertaDoAtendimentoMPPortabilidadeBKO(ofertaBko);
        }

        public OfertaDoAtendimentoMPPortabilidade RetornarOfertaDoAtendimentoMPPortabilidade(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoMPPortabilidade(id);
        }

        public OfertaDoAtendimentoMPPortabilidadeBKO RetornarOfertaDoAtendimentoMPPortabilidadeBKO(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoMPPortabilidadeBKO(id);
        }

        public long GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(HistoricoDaOfertaDoAtendimentoPortabilidadeBKO oferta)
        {
            return _dao.GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(oferta);
        }

        public HistoricoDaOfertaDoAtendimentoPortabilidadeBKO IniciarAuditoriaDaOfertaClaroPortabilidadeBKO(int idUsuario, long idOfertaBko)
        {
            int idStatusAuditoria = _idStatusAuditoriaEmProcessamento;
            var historico = new HistoricoDaOfertaDoAtendimentoPortabilidadeBKO()
            {
                id = 0,
                idOfertaDoAtendimentoPortabilidadeBKO = idOfertaBko,
                idCriador = idUsuario,
                idStatusAuditoria = idStatusAuditoria,
                Observacao = "",
                autorizacao = "",
                loginWM = "",
                protocolo = ""
            };

            long idHistorico = GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(historico);
            historico.id = idHistorico;
            return historico;
        }

        public long GravarAlteracaoDeProdutoPortabilidadeBKO(long idOfertaBKO, int idProdutoInicial, int idProduto, int idUsuario)
        {
            return _dao.GravarAlteracaoDeProdutoPortabilidadeBKO(idOfertaBKO, idProdutoInicial, idProduto, idUsuario);
        }

        #endregion CLARO_PORTABILIDADE

        #region NET_PTV

        public long GravarOfertaDoAtendimentoNETPTV(OfertaDoAtendimentoNETPTV oferta)
        {
            return _dao.GravarOfertaDoAtendimentoNETPTV(oferta);
        }

        public OfertaDoAtendimentoNETPTV RetornarOfertaDoAtendimentoNETPTV(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoNETPTV(id);
        }

        public OfertaDoAtendimentoNETPTVBKO RetornarOfertaDoAtendimentoNetPtvBKO(long id)
        {
            return _dao.RetornarOfertaDoAtendimentoNetPtvBKO(id);
        }

        public HistoricoDaOfertaDoAtendimentoNetPtvBKO IniciarAuditoriaDaOfertaNetPtvBKO(int idUsuario, long idOfertaBko)
        {
            int idStatusAuditoria = _idStatusAuditoriaEmProcessamento;
            var historico = new HistoricoDaOfertaDoAtendimentoNetPtvBKO()
            {
                id = 0,
                idOfertaDoAtendimentoNetPtvBKO = idOfertaBko,
                idStatusAuditoria = idStatusAuditoria,
                protocolo = "",
                numeroDoPedido = "",
                numeroDoContrato = "",
                loginNet = "",
                observacao = "",
                dataInstalacaoCorrigida = null,
                periodoCorrigido = "",
                idCriador = idUsuario,
            };

            long idHistorico = GravarHistoricoDoAtendimentoNetPtvBKO(historico);
            historico.id = idHistorico;
            return historico;
        }

        public long GravarHistoricoDoAtendimentoNetPtvBKO(HistoricoDaOfertaDoAtendimentoNetPtvBKO oferta)
        {
            return _dao.GravarHistoricoDoAtendimentoNetPtvBKO(oferta);
        }

        #endregion NET_PTV
    }
}
