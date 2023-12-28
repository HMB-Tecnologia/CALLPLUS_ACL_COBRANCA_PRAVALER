using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class StatusDeAcordoService
    {
        private readonly StatusDeAcordoDao _Dao;

        public StatusDeAcordoService()
        {
            _Dao = new StatusDeAcordoDao();
        }

        public IEnumerable<TipoDeStatusDeAcordo> ListarTipoDeStatusDeOferta(long idCampanha, bool? ativo)
        {
            return _Dao.ListarTipoDeStatusDeOferta(idCampanha, ativo);
        }

        public IEnumerable<StatusDeAcordo> ListarStatusDeAcordo(long? idCampanha, int? idTipoStatus, bool? ativo)
        {
            return _Dao.ListarStatusDeAcordo(idCampanha, idTipoStatus, ativo);
        }

        public StatusDeAcordo RetornarStatusDaOferta(int idStatus)
        {
            return _Dao.ListarTipoDeStatusDeOferta(idStatus);
        }

        public StatusDeAcordo RetornarStatusDeAcordo(int idStatusAcordo, int idCampanha)
        {
            return _Dao.RetornarStatusDeAcordo(idStatusAcordo, idCampanha);
        }

        public ConfiguracaoDoStatusDeOferta RetornarConfiguracaoDoStausDeOferta(int idStatusOferta, int idCampanha)
        {
            return _Dao.RetornarConfiguracaoDoStausDeOferta(id: null, idStatusOferta: idStatusOferta, idCampanha: idCampanha);
        }

        public int GravarStatusDeOferta(StatusDeAcordo statusDeOferta, string idsCampanhas)
        {
            return _Dao.Gravar(statusDeOferta, idsCampanhas);
        }
        public void GravarConfiguracaoDoStatusDeOferta(int idStatusDeOferta, int idStatusDeAtendimento)
        {
            _Dao.GravarConfiguracaoDoStatusDeOferta(idStatusDeOferta, idStatusDeAtendimento);
        }

        public IEnumerable<TipoDeStatusDeAcordo> ListarTipoDeStatus(bool ativo)
        {
            return _Dao.ListarTipoDeStatusDeOferta(-1, ativo);
        }

        public IEnumerable<StatusDeAcordo> RetornarCampanhasSelecionadas(int idStatusDeOferta)
        {
            return _Dao.RetornarCampanhasSelecionadas(idStatusDeOferta);
        }

        public ConfiguracaoDoStatusDeOferta RetornarStatusDeAtendimento(StatusDeAcordo statusDeOferta)
        {
            return _Dao.RetornarStatusDeAtendimento(statusDeOferta);
        }

        public DataTable ListarStatusDeOfertaExibicao(long idCampanha, bool ativo, string nome, int? idStatus)
        {
            return _Dao.ListarStatusDeOfertaExibicao(idCampanha, ativo, nome, idStatus);
        }

        public IEnumerable<StatusDeOfertaDaCampanha> ListarStatusDaOfertaDaCampanha(int id, bool ativo)
        {
            return _Dao.ListarStatusDaOfertaDaCampanha(id, ativo);
        }

        public IEnumerable<StatusDeAcordo> Listar(int? id, bool? ativo)
        {
            return _Dao.Listar(-1, ativo);
        }

        public IEnumerable<StatusDeAcordo> ListarStatusDeOfertaPorTipoCampanha(long? idCampanha, int? idTipoStatus, int? idTipoDeCampanha)
        {
            return _Dao.ListarStatusDeOfertaPorTipoCampanha(idCampanha, idTipoStatus, idTipoDeCampanha);
        }
	}
}
