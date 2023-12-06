using System;
using System.Collections.Generic;
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

        public IEnumerable<StatusDeOferta> ListarStatusDeOferta(long idCampanha, int? idTipoStatus, bool? ativo)
        {
            return _statusDeOfertaDao.ListarStatusDeOferta(idCampanha, idTipoStatus, ativo);
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

        public IEnumerable<StatusDeOferta> RetornarCampanhasSelecionadas(int idStatusDeAtendimento)
        {
            return _statusDeOfertaDao.RetornarCampanhasSelecionadas(idStatusDeAtendimento);
        }
    }
}
