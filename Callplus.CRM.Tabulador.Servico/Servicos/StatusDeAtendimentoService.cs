using System;
using System.Collections.Generic;
using System.Data;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class StatusDeAtendimentoService
    {
        private readonly StatusDeAtendimentoDao _statusDeAtendimentoDao;

        public StatusDeAtendimentoService()
        {
            _statusDeAtendimentoDao = new StatusDeAtendimentoDao();
        }

        public IEnumerable<KeyValuePair<int, string>> ObterStatusDeAtendimento(int idTipoStatus,bool ativo)
        {
            return _statusDeAtendimentoDao.ListarStatusDeAtendimento(idTipoStatus,ativo);
        }

        public IEnumerable<StatusDeAtendimento> Listar(int? id, bool ativo)
        {
            return _statusDeAtendimentoDao.Listar(id, ativo);
        }

        public IEnumerable<KeyValuePair<int, string>> ListarTipoDeStatusDeAtendimento(bool ativo = true)
        {
            return _statusDeAtendimentoDao.ListarTipoDeStatusDeAtendimento(ativo);
        }

        public StatusDeAtendimento RetornarStatusDoAtendimento(int idStatus)
        {
            return _statusDeAtendimentoDao.ListarTipoDeStatusDeAtendimento(idStatus);
        }

        public IEnumerable<StatusDeAtendimento> Listar(int? id, int? idTipoStatus, bool? ativo, int? idCampanha)
        {
            return _statusDeAtendimentoDao.Listar(id, idTipoStatus, idCampanha, ativo);
        }

        public DataTable ListaStatusDeAtendimento(int? idcampanha, bool? ativo, string nome)
        {
            return _statusDeAtendimentoDao.ListaStatusDeAtendimento(idcampanha, ativo, nome);
        }

        public IEnumerable<TipoDeStatusDeAtendimento> ListarTipoDeStatus(bool ativo)
        {
            return _statusDeAtendimentoDao.ListarTipoDeStatusDeAtendimento(-1, ativo);
        }

        public int GravarStatusDeAtendimento(StatusDeAtendimento _statusDeAtendimento, string idsCampanhas)
        {
            return _statusDeAtendimentoDao.Gravar(_statusDeAtendimento, idsCampanhas);
        }

        public IEnumerable<StatusDeAtendimento> RetornarCampanhasSelecionadas(int idStatusDeAtendimento)
        {
            return _statusDeAtendimentoDao.RetornarCampanhasSelecionadas(idStatusDeAtendimento);
        }

        public IEnumerable<CampanhaDoStatusDeAtendimento> ListarCampanhaDoStatus(int idCampanha, bool ativo)
        {
            return _statusDeAtendimentoDao.ListarCampanhaDoStatus(idCampanha, ativo);
        }
    }
}
