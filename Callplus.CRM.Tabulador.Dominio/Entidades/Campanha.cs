using System;
using Callplus.CRM.Tabulador.Dominio.TipoDiscagem;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class Campanha
    {
        public bool? HabilitarContatoManual { get; set; }
        public int AfterCall { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool HabilitaDiscagemManual { get; set; }
        public bool HabilitaHistorico { get; set; }
        public int Id { get; set; }
        public int IdCriador { get; set; }
        public int IdModificador { get; set; }

        public int IdDiscador { get; set; }
        public int? IdLayoutCampoDinamico { get; set; }
        public int? IdLayoutCampoDinamicoBko { get; set; }
        public int? IdScriptApresentacao { get; set; }
        public int? IdScriptFinalizacao { get; set; }
        public int? IdStatusTabulacaoAutomatica { get; set; }
        public int? IdStatusTabulacaoAutomaticaVenda { get; set; }
        public int? MetaVenda { get; set; }
        public string EnderecoDeImportacaoDoMailing { get; set; }
        public int IdTipoDeDiscagem { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public bool Principal { get; set; }        
        public bool HabilitaIndicacao { get; set; }
        public bool HabilitaComparadorDePlanos { get; set; }
        public bool HabilitaPesquisa { get; set; }
        public bool Selecionado { get; set; }

        public TipoDeDiscagem TipoDeDiscagem { get; set; }

        public bool? HabilitaCadastroDeTelefones { get; set; }

        public bool? HabilitaCadastroManual { get; set; }
        public int? IdMailingCadastroManual { get; set; }
    }
}
