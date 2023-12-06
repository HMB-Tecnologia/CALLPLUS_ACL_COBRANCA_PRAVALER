using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class ProspectService
    {
        private readonly ProspectDao _prospectDao;

        public ProspectService()
        {
            _prospectDao = new ProspectDao();
        }

        public Prospect RetornarProspect(long idProspect)
        {
            return _prospectDao.ObterProspect(idProspect);
        }

        public IEnumerable<TelefoneDoProspect> ListarTelefoneDoProspect(long idProspect, bool? ativo)
        {
            return _prospectDao.ListarTelefonesDoProspect(idProspect, ativo);
        }

        public IEnumerable<EnderecoDoProspect> ListarEnderecoDoProspect(long idProspect)
        {
            return _prospectDao.ListarEnderecosDoProspect(idProspect);
        }

        public long GravarEnderecoDoProspect(EnderecoDoProspect endereco)
        {
            return _prospectDao.GravarEnderecoDoProspect(endereco);
        }

        public IEnumerable<Profissao> ListarProfissao(bool ativo)
        {
            return _prospectDao.ListarProfissao(ativo);
        }

        public IEnumerable<FaixaDeRenda> ListarFaixaDeRenda(bool ativo)
        {
            return _prospectDao.ListarFaixaDeRenda(ativo);
        }

        public IEnumerable<EstadoCivil> ListarEstadoCivil(bool ativo)
        {
            return _prospectDao.ListarEstadoCivil(ativo);
        }

        public IEnumerable<string> VerificarSePodeGravar(int idUsuario, int idCampanha)
        {
            return _prospectDao.VerificarSePodeGravar(idUsuario, idCampanha);
        }

        public long GravarProspect(Prospect prospect)
        {
            return _prospectDao.GravarProspect(prospect);
        }
    }
}