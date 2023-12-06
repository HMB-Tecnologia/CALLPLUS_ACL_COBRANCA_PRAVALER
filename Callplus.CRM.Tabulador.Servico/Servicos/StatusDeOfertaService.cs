using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class StatusDeOfertaService
    {
        private readonly StatusDeOfertaDao _statusDeOfertaDao;

        public StatusDeOfertaService()
        {
            _statusDeOfertaDao = new StatusDeOfertaDao();
        }

        public IEnumerable<TipoDeStatusDeOferta> ListarTipoDeStatusDeOferta(long idCampanha, bool? ativo)
        {
            return _statusDeOfertaDao.ListarTipoDeStatusDeOferta(idCampanha, ativo);
        }

        public IEnumerable<StatusDeOferta> ListarStatusDeOferta(long? idCampanha, int? idTipoStatus, bool? ativo)
        {
            return _statusDeOfertaDao.ListarStatusDeOferta(idCampanha, idTipoStatus, ativo);
        }

        public StatusDeOferta RetornarStatusDaOferta(int idStatus)
        {
            return _statusDeOfertaDao.ListarTipoDeStatusDeOferta(idStatus);
        }

        public StatusDeOferta RetornarStatusDeOferta(int idStatusOferta, int idCampanha)
        {
            return _statusDeOfertaDao.RetornarStatusDeOferta(idStatusOferta, idCampanha);
        }

        public ConfiguracaoDoStatusDeOferta RetornarConfiguracaoDoStausDeOferta(int idStatusOferta, int idCampanha)
        {
            return _statusDeOfertaDao.RetornarConfiguracaoDoStausDeOferta(id: null, idStatusOferta: idStatusOferta, idCampanha: idCampanha);
        }

        public int GravarStatusDeOferta(StatusDeOferta statusDeOferta, string idsCampanhas)
        {
            return _statusDeOfertaDao.Gravar(statusDeOferta, idsCampanhas);
        }
        public void GravarConfiguracaoDoStatusDeOferta(int idStatusDeOferta, int idStatusDeAtendimento)
        {
            _statusDeOfertaDao.GravarConfiguracaoDoStatusDeOferta(idStatusDeOferta, idStatusDeAtendimento);
        }

        public IEnumerable<TipoDeStatusDeOferta> ListarTipoDeStatus(bool ativo)
        {
            return _statusDeOfertaDao.ListarTipoDeStatusDeOferta(-1, ativo);
        }

        public IEnumerable<StatusDeOferta> RetornarCampanhasSelecionadas(int idStatusDeOferta)
        {
            return _statusDeOfertaDao.RetornarCampanhasSelecionadas(idStatusDeOferta);
        }

        public ConfiguracaoDoStatusDeOferta RetornarStatusDeAtendimento(StatusDeOferta statusDeOferta)
        {
            return _statusDeOfertaDao.RetornarStatusDeAtendimento(statusDeOferta);
        }

        public DataTable ListarStatusDeOfertaExibicao(long idCampanha, bool ativo, string nome, int? idStatus)
        {
            return _statusDeOfertaDao.ListarStatusDeOfertaExibicao(idCampanha, ativo, nome, idStatus);
        }

        public IEnumerable<StatusDeOfertaDaCampanha> ListarStatusDaOfertaDaCampanha(int id, bool ativo)
        {
            return _statusDeOfertaDao.ListarStatusDaOfertaDaCampanha(id, ativo);
        }

        public IEnumerable<StatusDeOferta> Listar(int? id, bool? ativo)
        {
            return _statusDeOfertaDao.Listar(-1, ativo);
        }

        public IEnumerable<StatusDeOferta> ListarStatusDeOfertaPorTipoCampanha(long? idCampanha, int? idTipoStatus, int? idTipoDeCampanha)
        {
            return _statusDeOfertaDao.ListarStatusDeOfertaPorTipoCampanha(idCampanha, idTipoStatus, idTipoDeCampanha);
        }
    }
}
