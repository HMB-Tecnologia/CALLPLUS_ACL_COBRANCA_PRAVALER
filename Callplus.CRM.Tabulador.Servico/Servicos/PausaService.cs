using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class PausaService
    {
        private readonly PausaDao _pausaDao;

        public PausaService()
        {
            _pausaDao = new PausaDao();
        }

        public IEnumerable<ArquivoDePausa> Listar(int id, bool ativo)
        {
            return _pausaDao.Listar(id, ativo);
        }

        public DataTable Listar(int id, string nome, bool ativo)
        {
            return _pausaDao.Listar(id, nome, ativo);
        }

        public bool VerificarSeMailingEstaProcessadoComSucesso(int idArquivo)
        {
            return _pausaDao.VerificarSeMailingEstaProcessadoComSucesso(idArquivo);
        }

        public bool VerificarSeExisteNomeDoArquivo(string nome)
        {
            return _pausaDao.VerificarSeExisteNomeDoArquivo(nome);
        }

        public int Gravar(ArquivoDePausa arquivoDePausa)
        {
            return _pausaDao.Gravar(arquivoDePausa);
        }
    }
}
