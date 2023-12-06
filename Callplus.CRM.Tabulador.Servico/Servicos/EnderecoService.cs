using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    public class EnderecoService
    {
        private static EnderecoDao _enderecoDao;

        public EnderecoService()
        {
            _enderecoDao = new EnderecoDao();
        }

        public IEnumerable<Endereco> RetornarEndereco(string cep, string logradouro, string cidade, string uf, string bairro, string tipo)
        {
            return _enderecoDao.RetornarEnderecos(cep, logradouro, cidade, uf, bairro, tipo);
        }

        public IEnumerable<KeyValuePair<string, string>> RetornarCidade(string uf)
        {
            return _enderecoDao.RetornarCidade(uf);
        }

        public IEnumerable<KeyValuePair<string, string>> RetornarBairro(string uf, string cidade)
        {
            return _enderecoDao.RetornarBairro(uf, cidade);
        }

        public string RetornarUf(int ddd, int idcampanha)
        {
            return _enderecoDao.RetornarUf(ddd, idcampanha);
        }

        public List<string> VerificarSeCepEhElegivel(string cep, bool ehCepEntrega)
        {
            return _enderecoDao.VerificarSeCepEhElegivel(cep, ehCepEntrega);
        }
    }
}
