using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
   public  class EnderecoDoProspect
    {
        public long Id { get; set; }
        public long IdProspect { get; set; }        
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string PontoDeReferencia { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
