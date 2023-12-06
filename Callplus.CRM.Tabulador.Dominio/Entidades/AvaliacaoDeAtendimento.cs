using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class AvaliacaoDeAtendimento
    {
        public long id { get; set; }
        public int idTipoDeAvaliacaoDeAtendimento { get; set; }
        public int? idFormularioDeQualidade { get; set; }
        public long idAtendimento { get; set; }
        public int pontuacao { get; set; }
        public int idCriador { get; set; }        
        public DateTime dataCriacao { get; set; }        
        public string observacao { get; set; }
        public string Nome { get; set; }
        public int? idFeedback { get; set; }
    }
}
