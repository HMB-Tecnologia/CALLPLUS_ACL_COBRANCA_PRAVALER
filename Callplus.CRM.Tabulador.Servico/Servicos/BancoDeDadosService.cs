using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{

	public class BancoDeDadosService
	{
		private readonly BancoDeDadosDao _dao;
		public BancoDeDadosService()
		{
			_dao = new BancoDeDadosDao();
		}

		public string RetornarNomeDoBanco()
		{
			return _dao.RetornarNomeDoBanco();
		}
	}
}
