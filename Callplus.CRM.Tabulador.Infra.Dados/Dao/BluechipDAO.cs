using Callplus.CRM.Tabulador.Dominio.Entidades;
using Callplus.CRM.Tabulador.Infra.Dados.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Infra.Dados.Dao
{
    public class BluechipDAO : DaoBase
    {
        protected override IDbConnection Connection => ConnectionFactory.ObterConexao();

        public void GravarStatusDeEntregaBluechip(StatusDeEntregaBluechip status)
        {
            {
                var sql = "API_CRM_BLUECHIP_GRAVAR_STATUS_ENTREGA";

                var args = new
                {
                    Id = status.Id,
                    CnpjCpfDes = status.cnpjCpfDes,
                    NumeroPedido = status.numeroPedido,
                    NumeroRemessa = status.numeroRemessa,
                    DataOcorrencia = status.dataOcorrencia,
                    NomeMotorista = status.nomeMotorista,
                    PlacaVeiculo = status.placaVeiculo,
                    CodOcorrencia = status.codOcorrencia,
                    DescOcorrencia = status.descOcorrencia,
                    Iccid = status.iccid
                };

                var resultado = ExecuteProcedureScalar(sql, args);                
            }
        }
    }
}
