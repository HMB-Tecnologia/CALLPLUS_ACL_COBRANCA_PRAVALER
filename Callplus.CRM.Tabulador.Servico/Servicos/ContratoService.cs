using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System.Collections.Generic;
using System.Data;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
	public class ContratoService
	{
        private readonly ContratoDao _Dao;

        public ContratoService()
        {
            _Dao = new ContratoDao();
        }

        public List<Contrato> Listar(long id, bool baixado)
        {
            return _Dao.Listar(id, baixado);
        }

        public DataTable ListarExibicao(long id, bool baixado)
        {
            return _Dao.ListarExibicao(id, baixado);
        }

        public int Gravar(Contrato contrato)
        {
            return _Dao.Gravar(contrato);
        }

        public Contrato RetornarContrato(long id, bool baixado)
        {
            return _Dao.RetornarContrato(id, baixado);
        }
	}
}
