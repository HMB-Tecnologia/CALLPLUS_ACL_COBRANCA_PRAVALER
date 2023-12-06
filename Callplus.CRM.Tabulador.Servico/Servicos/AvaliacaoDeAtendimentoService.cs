using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class AvaliacaoDeAtendimentoService
    {
        private readonly AvaliacaoDeAtendimentoDao _dao;

        public AvaliacaoDeAtendimentoService()
        {
            _dao = new AvaliacaoDeAtendimentoDao();
        }

        public DataTable Listar(long id, int idCampanha, DateTime dataInicial, DateTime dataFinal, int idPerfil, int idAvaliador)
        {
            return _dao.Listar(id, idCampanha, dataInicial, dataFinal, idPerfil, idAvaliador);
        }

        public IEnumerable<AvaliacaoDeAtendimento> Listar(bool ativo)
        {
            return _dao.Listar(-1, ativo);
        }

        public DataTable Retornar(long id)
        {
            return _dao.Listar(id, -1, DateTime.Today, DateTime.Today, -1, -1);
        }

        public IEnumerable<TipoDeAvaliacaoDeAtendimento> ListarTipo(bool ativo)
        {
            return _dao.ListarTipo(ativo);
        }

        public DataTable ListarRespostaDaAvaliacao(long idAvaliacao)
        {
            return _dao.ListarRespostaDaAvaliacao(idAvaliacao);
        }

        public DataTable ListarDadosNotificacao(int idOperador)
        {
            return _dao.ListarDadosNotificacao(idOperador);
        }

        public int Gravar(AvaliacaoDeAtendimento item, string procedimentosOK, string procedimentosNOK, string procedimentosNA)
        {
            return _dao.Gravar(item, procedimentosOK, procedimentosNOK, procedimentosNA);
        }

        public AvaliacaoDeAtendimento RetornarOfertaMigracao(long idOferta)
        {
            return _dao.RetornarOfertaPortabilidade(idOferta);
        }

        public AvaliacaoDeAtendimento RetornarOfertaPortabilidade(long idOferta)
        {
            return _dao.RetornarOfertaPortabilidade(idOferta);
        }
    }
}
