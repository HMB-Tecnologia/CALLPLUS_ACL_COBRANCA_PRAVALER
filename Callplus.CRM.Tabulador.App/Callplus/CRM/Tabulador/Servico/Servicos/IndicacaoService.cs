using System;
using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;

namespace Callplus.CRM.Tabulador.Servico.Servicos
{
    internal class IndicacaoService
    {
        private readonly IndicacaoDao _indicacaoDao;

        public IndicacaoService()
        {
            _indicacaoDao = new IndicacaoDao();
        }

        internal void SalvarIndicacao(Indicacao indicacao)
        {
            _indicacaoDao.GravarIndicacaoDoAtendimento(indicacao);
        }
    }
}