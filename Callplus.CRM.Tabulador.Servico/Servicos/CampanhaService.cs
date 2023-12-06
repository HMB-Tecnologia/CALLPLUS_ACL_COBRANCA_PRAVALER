using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class CampanhaService
    {
        private readonly CampanhaDao _campanhaDao;

        public CampanhaService()
        {
            _campanhaDao = new CampanhaDao();
        }

        public IEnumerable<Campanha> ListarCampanhasDoUsuario(int idUsuario)
        {
            return _campanhaDao.ListarCampanhasDoUsuario(idUsuario);
        }

        public Campanha RetornarCampanha(int idCampanha)
        {
            return _campanhaDao.RetornarCampanha(idCampanha);
        }

        public Campanha RetornarCampanhaPrincipalDoUsuario(int idUsuario)
        {
            IEnumerable<Campanha> campanhasDoUsuario = ListarCampanhasDoUsuario(idUsuario);
            Campanha campanhaPrincipal = campanhasDoUsuario.FirstOrDefault(x => x.Principal == true);
            return campanhaPrincipal;
        }

        public IEnumerable<Banco> ListarBanco(int id, bool ativo)
        {
            return _campanhaDao.ListarBanco(id, ativo);
        }

        public IEnumerable<Banco> ListarBancoDaCampanha(int? idCampanha, bool? ativo)
        {
            return _campanhaDao.ListarBancoDaCampanha(idCampanha, ativo);
        }

        public IEnumerable<FormaDePagamento> ListarFormasDePagamento(int id, bool? ativo)
        {
            return _campanhaDao.ListarFormasDePagamento(id, ativo);
        }

        public IEnumerable<FormaDePagamento> ListarFormasDePagamentoDaCampanha(int? idCampanha, bool? ativo)
        {
            return _campanhaDao.ListarFormasDePagamentoDaCampanha(idCampanha, ativo);
        }

        public string RetornarCaminhoDoServidor(int idCampanha)
        {
            return _campanhaDao.RetornarCaminhoDoServidor(idCampanha);
        }

        public IEnumerable<Campanha> ListarCampanhasDoPlanoParaComparacao(int idPlano)
        {
            return _campanhaDao.ListarCampanhasDoPlanoParaComparacao(idPlano);
        }

        public IEnumerable<Campanha> ListarCampanhasDoAparelho(int idAparelho)
        {
            return _campanhaDao.ListarCampanhasDoAparelho(idAparelho);
        }

        public DataTable Listar(int id, int idDiscador, string nome, bool ativo)
        {
            return _campanhaDao.Listar(id, idDiscador, nome, ativo);
        }


        //TODO - Buscar somente campanhas ativas - Rei Almeida
        public IEnumerable<Campanha> Listar(bool? ativo)
        {
            return _campanhaDao.Listar(-1, ativo);
        }

        public string RetornarNomeDeArquivoDeAudioPorProduto(int idProduto, int idCampanha)
        {
            CampanhaDao BProspect = new CampanhaDao();
            return BProspect.RetornarNomeDeArquivoDeAudioPorProduto(idProduto, idCampanha);
        }

        public string RetornarNomeDeArquivoDeAudioPorDataVencimento(int diaVencimentoFatura, int idCampanha)
        {
            CampanhaDao BProspect = new CampanhaDao();
            return BProspect.RetornarNomeDeArquivoDeAudioPorDataVencimento(diaVencimentoFatura, idCampanha);
        }

        public int AtualizarDadosDeCadastroManual(int idCampanha, int idMailing)
        {
            return _campanhaDao.AtualizarDadosDeCadastroManual(idCampanha, idMailing);
        }

        public int Gravar(Campanha campanha, bool? espelho, int? idCampanha)
        {
            return _campanhaDao.Gravar(campanha, espelho, idCampanha);
        }

        public Campanha RetornarInformacoesDaCampanha(int idCampanha)
        {
            return _campanhaDao.RetornarInformacoesDaCampanha(idCampanha);
        }

        public IEnumerable<Campanha> ListarTipoDaCampanha(bool? ativo)
        {
            return _campanhaDao.ListarTipoDaCampanha(-1, ativo);
        }

        public IEnumerable<string> RetornarMensagemDeRevendaHabilitada()
        {
            return _campanhaDao.RetornarMensagemDeRevendaHabilitada();
        }

        public int VincularUsuarioAhCampanha(int idUsuario, int idCampanha)
        {
            return _campanhaDao.VincularUsuarioAhCampanha(idUsuario, idCampanha);
        }
    }
}
