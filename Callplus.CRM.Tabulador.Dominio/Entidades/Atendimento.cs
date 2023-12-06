using System;
using System.Collections.Generic;
using Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico;
using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Atendimento
    {
        public Atendimento()
        {
            CamposDoAtendimento = new List<ValorDeCampoDinamico>();
        }

        public long Id { get; set; }
        public long IdProspect { get; set; }
        public int IdOperador { get; set; }
        public int IdSupervisor { get; set; }
        public int IdOrigemAtendimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public string NumeroChamadaDiscador { get; set; }
        public string Ip { get; set; }
        public string Host { get; set; }
        public bool HouveAceiteDeOferta { get; set; }

        public IEnumerable<ValorDeCampoDinamico> CamposDoAtendimento;

        public int? idUsuarioPermissao { get; set; }

        //public OrigemDeAtendimento OrigemDeAtendimento
        //{
        //    get => (OrigemDeAtendimento)IdOrigemAtendimento;
        //    set => IdOrigemAtendimento = (int)value;
        //}
    }
}
