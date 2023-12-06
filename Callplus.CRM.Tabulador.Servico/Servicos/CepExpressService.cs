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
    public class CepExpressService
    {
        private readonly CepExpressDao _Dao;

        public CepExpressService()
        {
            _Dao = new CepExpressDao();
        }

        public IEnumerable<CepExpress> Listar(int idRegistro, int idCampanha, string nome, bool ativo)
        {
            return _Dao.Listar(idRegistro, idCampanha, nome, ativo);
        }

        public DataTable ListarExibicao(int idRegistro, int idCampanha, string nome, bool ativo)
        {
            return _Dao.ListarExibicao(idRegistro, idCampanha, nome, ativo);
        }

        public int Gravar(CepExpress cadastroCepExpress)
        {
            return _Dao.Gravar(cadastroCepExpress);
        }

        public CepExpress RetornarCepExpress(int id)
        {
            return _Dao.RetornarCepExpress(id);
        }

        public bool VerificarSeExisteNomeDoCadastroCep(string nome)
        {
            return _Dao.VerificarSeExisteNomeDoCadastroCep(nome);
        }
    }
}
