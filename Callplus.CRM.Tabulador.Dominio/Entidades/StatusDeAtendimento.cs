using System;
using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class StatusDeAtendimento
    {
        public int Id { get; set; }
        public int IdCriador { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdTipoDeStatusDeAtendimento { get; set; }
        public TipoStatusDeAtendimento TipoDeStatus
        {
            get { return (TipoStatusDeAtendimento) IdTipoDeStatusDeAtendimento; }
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Observacao { get; set; }

        public string TipoStatusDeAtendimento { get; set; }
        public string NomeCriador { get; set; }
        public string NomeModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public bool Selecionado { get; set; }
        public int idTipoDeAgendamento { get; set; }
        public bool tabulacaoAutomatica { get; set; }

    }
}
