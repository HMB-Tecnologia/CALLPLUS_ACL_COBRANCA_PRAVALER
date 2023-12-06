using Callplus.CRM.Tabulador.Dominio.Dto;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class PesquisaService
    {
        private readonly PesquisaDao _dao;

        public PesquisaService()
        {
            _dao = new PesquisaDao();
        }

        public IEnumerable<PesquisaDoAtendimentoDto> CarregarPesquisaDoAtendimento(long idAtendimento)
        {
            return _dao.CarregarPesquisaDoAtendimento(idAtendimento);
        }

        public IEnumerable<OpcaoDaPerguntaDaPesquisa> ListarOpcaoDaPerguntaDaPesquisa(int idPergunta)
        {
            return _dao.ListarOpcaoDaPerguntaDaPesquisa(idPergunta);
        }

        public long GravarRespostaDoAtendimento(RespostaDaPesquisaDoAtendimento resposta)
        {
            return _dao.GravarRespostaDoAtendimento(resposta);
        }
    }
}
