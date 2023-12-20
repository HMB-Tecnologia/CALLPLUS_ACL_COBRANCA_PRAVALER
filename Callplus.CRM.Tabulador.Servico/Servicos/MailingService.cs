using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
	public class MailingService
	{
		private readonly MailingDao _mailingDao;

		public MailingService()
		{
			_mailingDao = new MailingDao();
		}

		public IEnumerable<Mailing> Listar(int id, bool ativo)
		{
			return _mailingDao.Listar(id, ativo);
		}

		public DataTable Listar(int id, int idCampanha, string nome, bool ativo)
		{
			return _mailingDao.Listar(id, idCampanha, nome, ativo);
		}

		public int Gravar(Mailing mailing)
		{
			return _mailingDao.Gravar(mailing);
		}

		public bool VerificarSeExisteNomeDoMailing(string nome)
		{
			return _mailingDao.VerificarSeExisteNomeDoMailing(nome);
		}

		public DataTable ExportarMailing(int idMailing)
		{
			return _mailingDao.ExportarMailing(idMailing);
		}

		public bool VerificarSeMailingEstaProcessadoComSucesso(int idMailing)
		{
			return _mailingDao.VerificarSeMailingEstaProcessadoComSucesso(idMailing);
		}

		public void ExportarMailingDiscador(int idMailing)
		{
			_mailingDao.ExportarMailingDiscador(idMailing);
		}

		public IEnumerable<Mailing> Listar(int? id, int? idCampanha, bool? ativo)
		{
			return _mailingDao.Listar(id, idCampanha, ativo);
		}

		public string GravarArquivoDeMailingEMarcacoes(string sqlArquivoMailing, string sqlArquivoMarcacoes, int idMailing)
		{
			return _mailingDao.GravarArquivoDeMailingEMarcacoes(sqlArquivoMailing, sqlArquivoMarcacoes, idMailing);
		}


		//public List<Mailing> RetornoMailingsTrabalhadosDia(DateTime dataInicio, DateTime dataTermino, string codMailing, int idCampanha)
		//{
		//    try
		//    {
		//        var listaMailings = new List<Mailing>();

		//        var sSql = _mailingDao.RetornoMailingsTrabalhadosDia(dataInicio, dataTermino, codMailing, idCampanha);

		//        var dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql, 20 * 60); //Timeout para 5minutos

		//        foreach (DataRow linha in dataTable.Rows)
		//        {
		//            var mailing = new Mailing()
		//            {
		//                idCampanha = (int)linha["idCampanha"],
		//                nome = linha["nome"].ToString(),
		//            };

		//            listaMailings.Add(mailing);
		//        }

		//        return listaMailings;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}

		//public List<string> RetornoTelefoniaPorDiaIdMailing(DateTime dataInicio, DateTime dataTermino, string nomeArquivo, int idCampanha, string codMailing, int tipoExportacaoContatosNaoTrabalhados)
		//{
		//    try
		//    {
		//        var listaTelefonia = new List<string>();

		//        var sSql = _mailingDao.RetornoTelefoniaPorDiaIdMailing(dataInicio, dataTermino, nomeArquivo, idCampanha, codMailing, tipoExportacaoContatosNaoTrabalhados);

		//        var dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql, 20 * 60);

		//        foreach (DataRow linha in dataTable.Rows)
		//        {
		//            var builder = new StringBuilder();

		//            builder.Append(linha["a"]);

		//            listaTelefonia.Add(builder.ToString());
		//        }

		//        return listaTelefonia;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}

		//public List<string> RetornoOcorrenciasPorDiaIdMailing(DateTime dataInicio, DateTime dataTermino, string nomeArquivo, int idCampanha, string codMailing)
		//{
		//    try
		//    {
		//        var listaTelefonia = new List<string>();

		//        var sSql = _mailingDao.RetornoOcorrenciasPorDiaIdMailing(dataInicio, dataTermino, nomeArquivo, idCampanha, codMailing);

		//        var dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql, 20 * 60);

		//        foreach (DataRow linha in dataTable.Rows)
		//        {
		//            var builder = new StringBuilder();

		//            builder.Append(linha["a"]);

		//            listaTelefonia.Add(builder.ToString());
		//        }

		//        return listaTelefonia;
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}

		//public DataTable RetornarBaseB(string codMailing, string dataInicio, string dataTermino, bool todos, string nomeCampanha)
		//{
		//    string sSql = _mailingDao.RetornarBaseB(codMailing, dataInicio, dataTermino, todos, nomeCampanha);
		//}
	}
}
