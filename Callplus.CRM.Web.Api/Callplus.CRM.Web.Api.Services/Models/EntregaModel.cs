using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Infra.Dados.Dao;
using Callplus.CRM.Tabulador.Dominio.Entidades;

namespace Callplus.CRM.Web.Api.Services.Models
{
    public class EntregaModel
    {
        public Entrega GravarStatus(List<GravarEntrega> e)
        {
            Entrega registro;
            RetornoEntrega registroInterno;
            List<RetornoEntrega> resultInterno = new List<RetornoEntrega>();

            registro = new Entrega();

            BluechipDAO dao = new BluechipDAO();
            StatusDeEntregaBluechip status;

            string codigo = "0";
            string mensagem = "";

            foreach (var item in e)
            {
                try
                {
                    status = new StatusDeEntregaBluechip();
                    status.cnpjCpfDes = item.cnpjCpfDes;
                    status.numeroPedido = item.numeroPedido;
                    status.numeroRemessa = item.numeroRemessa;
                    status.dataOcorrencia = item.dataOcorrencia;
                    status.nomeMotorista = item.nomeMotorista;
                    status.placaVeiculo = item.placaVeiculo;
                    status.codOcorrencia = item.codOcorrencia;
                    status.descOcorrencia = item.descOcorrencia;
                    status.iccid = item.iccid;

                    dao.GravarStatusDeEntregaBluechip(status);

                    codigo = "1";
                    mensagem = "Atualização recebida com sucesso";
                }
                catch(Exception ex)
                {
                    codigo = "3";
                    mensagem = ex.Message;
                }

                registroInterno = new RetornoEntrega();
                registroInterno.cnpjCpfDes = item.cnpjCpfDes;
                registroInterno.numeroPedido = item.numeroPedido;
                registroInterno.numeroRemessa = item.numeroRemessa;
                registroInterno.codMensagem = codigo;
                registroInterno.mensagem = mensagem;

                resultInterno.Add(registroInterno);
            }

            registro.entregas = resultInterno;

            return registro;
        }

        public Entrega RetornarErroToken(List<GravarEntrega> e)
        {
            Entrega registro;
            RetornoEntrega registroInterno;
            List<RetornoEntrega> resultInterno = new List<RetornoEntrega>();

            registro = new Entrega();

            foreach (var item in e)
            {
                registroInterno = new RetornoEntrega();
                registroInterno.cnpjCpfDes = item.cnpjCpfDes;
                registroInterno.numeroPedido = item.numeroPedido;
                registroInterno.numeroRemessa = item.numeroRemessa;
                registroInterno.codMensagem = "2";
                registroInterno.mensagem = "Token inválido";

                resultInterno.Add(registroInterno);
            }

            registro.entregas = resultInterno;

            return registro;
        }
    }
}