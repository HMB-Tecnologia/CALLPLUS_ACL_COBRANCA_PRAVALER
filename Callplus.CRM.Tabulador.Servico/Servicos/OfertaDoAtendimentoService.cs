using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Data;
using System.Linq;
using Callplus.CRM.Tabulador.Dominio.Dto;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class OfertaDoAtendimentoService
    {
        private readonly OfertaDoAtendimentoDao _ofertaDoAtendimentoDao;
        private readonly int _idStatusAuditoriaEmProcessamento = 21;

        public OfertaDoAtendimentoService()
        {
            _ofertaDoAtendimentoDao = new OfertaDoAtendimentoDao();
        }

        public DataTable ListarOfertaDoAtendimento(long idAtendimento)
        {
            return _ofertaDoAtendimentoDao.ListarOfertaDoAtendimento(idAtendimento);
        }

        public OfertaDoAtendimento RetornarOfertaElegivelParaAtendimento(long idAtendimento, int idCampanha)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaElegivelParaAtendimento(idAtendimento, idCampanha);
        }

        public long GravarStatusDaOfertaDoAtendimento(OfertaDoAtendimento oferta, StatusDeOferta status)
        {            
            //TODO: ALTERAR PARA CONSIDERAR O TIPO DA OFERTA NÃO UTILIZANDO O ID
            if (oferta.IdTipoDeProduto == 1) 
                return _ofertaDoAtendimentoDao.GravarStatusDaOfertaDoAtendimento(oferta.Id, oferta.IdAtendimento, status.Id);

            throw new InvalidOperationException("O tipo da Oferta do Atendimento não pôde ser reconhecido");
        }
        
        public string ValidarLoginWM(string login)
        {
            return _ofertaDoAtendimentoDao.ValidarLoginWM(login);
        }

        public string ValidarLoginDaOperadora(int idOperadora, string login)
        {
            return _ofertaDoAtendimentoDao.ValidarLoginDaOperadora(idOperadora, login);
        }

        public IEnumerable<OfertaDoAtendimento> RetornarOfertasDoAtendimento(long idAtendimento)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimento(idAtendimento);
        }

        public IEnumerable<string> VerificarSePodeEditarOfertaBKO(int idUsuario, long idOfertaBko, int idTipoProduto)
        {
            return _ofertaDoAtendimentoDao.VerificarSePodeEditarOfertaBKO(idUsuario,idOfertaBko,idTipoProduto);
        }

        public void RemoverHistoricoDeOfertaBkoPendente(int idUsuario, long idOfertaBko, int? idTipoProduto)
        {
             _ofertaDoAtendimentoDao.RemoverHistoricoDeOfertaBkoPendente(idUsuario, idOfertaBko, idTipoProduto);
        }

        public DataTable ListarHistoricoDaOfertaDoAtendimentoBKO(long idOfertaBko, int idTipoDeProduto)
        {
           return _ofertaDoAtendimentoDao.ListarHistoricoDaOfertaDoAtendimentoBKO(idOfertaBko, idTipoDeProduto);
        }

        public HistoricoDaOfertaDoAtendimentoBkoDTO RetornarHistoricoDaOfertaDoAtendimentoBKO_DTO(long idHistoricoBko, int idTipoDeProduto)
        {
            var item = _ofertaDoAtendimentoDao
                .ListarHistoricoDaOfertaDoAtendimentoBKO_DTO(idHistoricoBko, null, idTipoDeProduto)
                .FirstOrDefault(x => x.Id == idHistoricoBko);
            return item;

        }

        public ResumoDaOfertaDoAtendimentoBkoDTO RetornarResumoDaOfertaDoAtendimentoBKO(long id, int idTipoDeProduto)
        {
            return _ofertaDoAtendimentoDao.RetornarResumoDaOfertaDoAtendimentoBKO(id, idTipoDeProduto);
        }

        public IEnumerable<ConfiguracaoVencimentoFaturaDto> RetornarDatasDeVencimentoDeFaturaDisponiveisBKO()
        {
            return _ofertaDoAtendimentoDao.RetornarDatasDeVencimentoDeFaturaDisponiveisBKO();
        }

        public IEnumerable<CanalAdicional> ListarCanalAdicional(int idOperadora, int idProduto, bool ativo)
        {
            return _ofertaDoAtendimentoDao.ListarCanalAdicional(idOperadora, idProduto, ativo);
        }

        public DataTable RetornarOfertaParaAvaliacao(int idCampanha, int idSupervisor, int idOperador, string dataInicial, string dataFinal, string idStatus)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaParaAvaliacao(idCampanha, idSupervisor, idOperador, dataInicial, dataFinal, idStatus);
        }

        public DataTable RetornarOfertaParaAvaliacaoBKO(long idOfertaBko)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaParaAvaliacaoBKO(idOfertaBko);
        }

        #region CLARO_MIGRACAO

        public long GravarOfertaDoAtendimentoClaroMigracao(OfertaDoAtendimentoClaroMigracao oferta)
        {
            return _ofertaDoAtendimentoDao.GravarOfertaDoAtendimentoClaroMigracao(oferta);
        }

        public long GravarOfertaDoAtendimentoClaroMigracaoBKO(OfertaDoAtendimentoClaroMigracaoBKO ofertaBko)
        {
            return _ofertaDoAtendimentoDao.GravarOfertaDoAtendimentoClaroMigracaoBKO(ofertaBko);
        }

        public OfertaDoAtendimentoClaroMigracao RetornarOfertaDoAtendimentoClaroMigracao(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoClaroMigracao(id);
        }

        public OfertaDoAtendimentoClaroMigracaoBKO RetornarOfertaDoAtendimentoClaroMigracaoBKO(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoClaroMigracaoBKO(id);
        }

        public long GravarHistoricoDoAtendimentoClaroMigracaoBKO(HistoricoDaOfertaDoAtendimentoMigracaoBKO oferta)
        {
            return _ofertaDoAtendimentoDao.GravarHistoricoDoAtendimentoClaroMigracaoBKO(oferta);
        }

        public HistoricoDaOfertaDoAtendimentoMigracaoBKO IniciarAuditoriaDaOfertaClaroMigracaoBKO(int idUsuario, long idOfertaBko)
        {
            int idStatusAuditoria = _idStatusAuditoriaEmProcessamento;
            var historico = new HistoricoDaOfertaDoAtendimentoMigracaoBKO()
            {
                id = 0,
                idOfertaDoAtendimentoMigracaoBKO = idOfertaBko,
                idCriador = idUsuario,
                idStatusAuditoria = idStatusAuditoria,
                Observacao = "",
                autorizacao = "",
                loginWM = "",
                protocolo = ""
            };

            long idHistorico = GravarHistoricoDoAtendimentoClaroMigracaoBKO(historico);
            historico.id = idHistorico;
            return historico;
        }

        #endregion CLARO_MIGRACAO

        #region CLARO_RENTABILIZACAO

        public long GravarOfertaDoAtendimentoClaroRentabilizacao(OfertaDoAtendimentoClaroRentabilizacao oferta)
        {
            return _ofertaDoAtendimentoDao.GravarOfertaDoAtendimentoClaroRentabilizacao(oferta);
        }

        public OfertaDoAtendimentoClaroRentabilizacao RetornarOfertaDoAtendimentoClaroRentabilizacao(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoClaroRentabilizacao(id);
        }

        public OfertaDoAtendimentoClaroRentabilizacaoBKO RetornarOfertaDoAtendimentoClaroRentabilizacaoBKO(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoClaroRentabilizacaoBKO(id);
        }

        public long GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(HistoricoDaOfertaDoAtendimentoRentabilizacaoBKO oferta)
        {
            return _ofertaDoAtendimentoDao.GravarHistoricoDoAtendimentoClaroRentabilizacaoBKO(oferta);
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
            return _ofertaDoAtendimentoDao.ListarPassaporteOferta();
        }

        public long GravarOfertaDoAtendimentoClaroRentabilizacaoBKO(OfertaDoAtendimentoClaroRentabilizacaoBKO ofertaBko)
        {
            return _ofertaDoAtendimentoDao.GravarOfertaDoAtendimentoClaroRentabilizacaoBKO(ofertaBko);
        }

        #endregion CLARO_RENTABILIZACAO

        #region CLARO_PORTABILIDADE

        public long GravarOfertaDoAtendimentoClaroPortabilidade(OfertaDoAtendimentoClaroPortabilidade oferta)
        {
            return _ofertaDoAtendimentoDao.GravarOfertaDoAtendimentoClaroPortabilidade(oferta);
        }

        public OfertaDoAtendimentoClaroPortabilidade RetornarOfertaDoAtendimentoClaroPortabilidade(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoClaroPortabilidade(id);
        }

        public OfertaDoAtendimentoClaroPortabilidadeBKO RetornarOfertaDoAtendimentoClaroPortabilidadeBKO(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoClaroPortabilidadeBKO(id);
        }

        public long GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(HistoricoDaOfertaDoAtendimentoPortabilidadeBKO oferta)
        {
            return _ofertaDoAtendimentoDao.GravarHistoricoDoAtendimentoClaroPortabilidadeBKO(oferta);
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

        #endregion CLARO_PORTABILIDADE
        
        #region NET_PTV

        public long GravarOfertaDoAtendimentoNETPTV(OfertaDoAtendimentoNETPTV oferta)
        {
            return _ofertaDoAtendimentoDao.GravarOfertaDoAtendimentoNETPTV(oferta);
        }

        public OfertaDoAtendimentoNETPTV RetornarOfertaDoAtendimentoNETPTV(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoNETPTV(id);
        }

        public OfertaDoAtendimentoNETPTVBKO RetornarOfertaDoAtendimentoNetPtvBKO(long id)
        {
            return _ofertaDoAtendimentoDao.RetornarOfertaDoAtendimentoNetPtvBKO(id);
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
            return _ofertaDoAtendimentoDao.GravarHistoricoDoAtendimentoNetPtvBKO(oferta);
        }

        #endregion NET_PTV
    }
}
