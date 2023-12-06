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
    public class RetornoDeCepService
    {
        private RetornoDeCepDao _Dao;
        public RetornoDeCepService()
        {
            _Dao = new RetornoDeCepDao();
        }

        public IEnumerable<CepCorreios> RetornarEndereco(string cep, string uf, string cidade, string bairro, string logradouro, int tipodepesquisa)
        {
            return _Dao.RetornarEndereco(cep, uf, cidade, bairro, logradouro, tipodepesquisa);
        }

        public DataTable RetornarDataTableEndereco(string cep, string uf, string cidade, string bairro, string logradouro, int tipodepesquisa)
        {
            return _Dao.RetornarDataTableEndereco(cep, uf, cidade, bairro, logradouro, tipodepesquisa);
        }

        public IEnumerable<SiglaUfBrasil> ListarUfBrasil(bool ativo, string uf)
        {
            return _Dao.ListarUfBrasil(ativo, uf);
        }
    }
}
