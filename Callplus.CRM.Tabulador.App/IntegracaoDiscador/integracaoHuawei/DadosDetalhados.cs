using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v1Tabulare_z13.integracaoHuawei
{
    public class DadosDetalhados
    {
        public string sn { get; set; }
        public string duracao { get; set; }
        public string numeroChamador { get; set; }
        public string origem { get; set; }
        public string empresaChamada { get; set; }
        public string idioma { get; set; }
        public string idContato { get; set; }
        public string UVID { get; set; }
        public string ACW { get; set; }
        public string fila { get; set; }
        public string rastreioChamada { get; set; }
        public string SNReproducao { get; set; }
        public string vezesReproducao { get; set; }
        public string duracaoTotal { get; set; }
        public string modo { get; set; }
        public string numeroLinha { get; set; }
        public string skill { get; set; }
        public string protocolo { get; set; }
        public string tipo { get; set; }
        public string status { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string classificacao { get; set; }
        public string nome { get; set; }
        public string inadiplente { get; set; }
        public string migracaoCPC { get; set; }
        public string transStaffId { get; set; }

        public DadosDetalhados(string[] vet)
        {
            for (int i = 0; i < vet.Length; i++)
            {
                if (vet[i].ToUpper().Contains("SN") && string.IsNullOrEmpty(this.sn))
                {
                    this.sn = vet[i].Replace("SN", "");
                }
                else if (vet[i].ToUpper().Contains("DURAÇÃO"))
                {
                    this.duracao = vet[i].Replace("Duração", "");
                }
                else if (vet[i].ToUpper().Contains("NÚMERO CHAMADOR"))
                {
                    this.numeroChamador = vet[i].Replace("Número Chamador", "");
                }
                else if (vet[i].ToUpper().Contains("ORIGEM"))
                {
                    this.origem = vet[i].Replace("Origem", "");
                }
                else if (vet[i].ToUpper().Contains("EMPRESA CHAMADA"))
                {
                    this.empresaChamada = vet[i].Replace("Empresa Chamada", "");
                }
                else if (vet[i].ToUpper().Contains("IDIOMA"))
                {
                    this.idioma = vet[i].Replace("Idioma", "");
                }
                else if (vet[i].ToUpper().Contains("ID DE CONTATO"))
                {
                    this.idContato = vet[i].Replace("ID de Contato", "");
                }
                else if (vet[i].ToUpper().Contains("UVID"))
                {
                    this.UVID = vet[i].Replace("UVID", "");
                }
                else if (vet[i].ToUpper().Contains("ACW"))
                {
                    this.ACW = vet[i].Replace("ACW", "");
                }
                else if (vet[i].ToUpper().Contains("FILA"))
                {
                    this.fila = vet[i].Replace("Fila", "");
                }
                else if (vet[i].ToUpper().Contains("RASTREIO DE CHAMADA"))
                {
                    this.rastreioChamada = vet[i].Replace("Rastreio de Chamada", "");
                }
                else if (vet[i].ToUpper().Contains("SN DE REPRODUÇÃO"))
                {
                    this.SNReproducao = vet[i].Replace("SN de reprodução", "");
                }
                else if (vet[i].ToUpper().Contains("VEZES DE REPRODUÇÃO"))
                {
                    this.vezesReproducao = vet[i].Replace("Vezes de Reprodução", "");
                }
                else if (vet[i].ToUpper().Contains("DURAÇÃO TOTAL"))
                {
                    this.duracaoTotal = vet[i].Replace("Duração total", "");
                }
                else if (vet[i].ToUpper().Contains("MODO"))
                {
                    this.modo = vet[i].Replace("Modo", "");
                }
                else if (vet[i].ToUpper().Contains("NÚMERO LINHA"))
                {
                    this.numeroLinha = vet[i].Replace("Número Linha", "");
                }
                else if (vet[i].ToUpper().Contains("SKILL"))
                {
                    this.skill = vet[i].Replace("Skill", "");
                }
                else if (vet[i].ToUpper().Contains("PROTOCOLO"))
                {
                    this.protocolo = vet[i].Replace("Protocolo", "");
                }
                else if (vet[i].ToUpper().Contains("TIPO"))
                {
                    this.tipo = vet[i].Replace("Tipo", "");
                }
                else if (vet[i].ToUpper().Contains("STATUS"))
                {
                    this.status = vet[i].Replace("Status", "");
                }
                else if (vet[i].ToUpper().Contains("CPF"))
                {
                    this.CPF = vet[i].Replace("CPF", "");
                }
                else if (vet[i].ToUpper().Contains("CNPJ"))
                {
                    this.CNPJ = vet[i].Replace("CNPJ", "");
                }
                else if (vet[i].ToUpper().Contains("CLASSIFICACAO"))
                {
                    this.classificacao = vet[i].Replace("Classificacao", "");
                }
                else if (vet[i].ToUpper().Contains("NOME"))
                {
                    this.nome = vet[i].Replace("Nome", "");
                }
                else if (vet[i].ToUpper().Contains("INADIMPLENTE"))
                {
                    this.inadiplente = vet[i].Replace("Inadimplente", "");
                }
                else if (vet[i].ToUpper().Contains("MIGRADO CPC"))
                {
                    this.migracaoCPC = vet[i].Replace("Migrado CPC", "");
                }
                else if (vet[i].ToUpper().Contains("TRANSSTAFFID"))
                {
                    this.transStaffId = vet[i].Replace("TransStaffId", "");
                }
            }
        }
    }
}