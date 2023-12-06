using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class ArquivoDePausa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataTermino { get; set; }
        public int idCriador { get; set; }
        public int idModificador { get; set; }
        public string nomeArquivo { get; set; }
        public string caminhoArquivo { get; set; }
        public string observacao { get; set; }
        public bool ativo { get; set; }
    }
}
