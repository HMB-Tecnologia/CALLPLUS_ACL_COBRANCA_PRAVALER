using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class EtapaDoChecklist
    {
        public int id { get; set; }
        public int idChecklist { get; set; }
        public int etapa { get; set; }
        public string descricaoRtf { get; set; }        
        public bool ativo { get; set; }
        public int idCriador { get; set; }
        public DateTime dataCriacao { get; set; }
        public int idModificador { get; set; }
        public DateTime dataModificacao { get; set; }
    }
}
