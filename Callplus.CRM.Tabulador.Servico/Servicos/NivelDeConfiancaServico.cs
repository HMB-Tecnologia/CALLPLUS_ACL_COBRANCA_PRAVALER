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
    public class NivelDeConfiancaServico
    {
        private readonly NivelDeConfiancaDao _dao;

        public NivelDeConfiancaServico()
        {
            _dao = new NivelDeConfiancaDao();
        }
        public DataTable Listar(long id, int idCampanha, DateTime dataInicial, DateTime dataFinal, int idAgente, int idAvaliador, decimal notaMinima, decimal notaMaxima)
        {
            return _dao.Listar(id, idCampanha, dataInicial, dataFinal, idAgente, idAvaliador, notaMinima, notaMaxima);
        }
        public DataTable ListarAgente(int idCampanha)
        {
            return _dao.ListarAgente(idCampanha);
        }
        public int ArquivoGravar(string titulo, int idAtualizador)
        {
            return _dao.ArquivoGravar(titulo, idAtualizador);
        }
        public int Gravar(int idArquivo, int idAgente, decimal nota, int idAtualizador)
        {
            return _dao.Gravar(idArquivo, idAgente, nota, idAtualizador);
        }
        public int Editar(int id, decimal nota,int idAgente, int idAtualizador)
        {
            return _dao.Editar(id, nota, idAgente, idAtualizador);
        }
        public DataTable ListarHistorico(int idAgente)
        {
            return _dao.ListarHistorico(idAgente);
        }
        public DataTable ListarHistoricoProducao(int idAgente)
        {
            return _dao.ListarHistoricoProducao(idAgente);
        }
    }
}
