using System;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Notificacao
    {
        public int? Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int IdSupervisor { get; set; }
        public bool Ativo { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public bool Selecionado { get; set; }
    }
}
