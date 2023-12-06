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
        public int idTipoDaCampanha { get; set; }
        public string Observacao { get; set; }
        public bool Principal { get; set; }        
        public bool HabilitaIndicacao { get; set; }
        public bool HabilitaComparadorDePlanos { get; set; }
        public bool HabilitaPesquisa { get; set; }
        public bool Selecionado { get; set; }

        public TipoDeDiscagem TipoDeDiscagem
        {
            get { return (TipoDeDiscagem)IdTipoDeDiscagem; }
        }

        public int IdTipoDeAuditoria { get; set; }
        public Tipos.TipoDeAuditoria TipoAuditoria
        {
            get { return (Tipos.TipoDeAuditoria)IdTipoDeAuditoria; }
        }

        public bool? HabilitaCadastroDeTelefones { get; set; }

        public bool? HabilitaCadastroManual { get; set; }
        public int? IdMailingCadastroManual { get; set; }

        public bool HabilitaCepExpress { get; set; }
        public int IDCampanhaDiscador { get; set; }
        public int IdModificador { get; set; }
        public bool HabilitaRevenda { get; set; }

       

        //VARIÁVEIS P/ DUPLICAR CAMPANHA
        public bool Aparelhos { get; set; }
        public bool VariaveisDoScript { get; set; }
        public bool CheckListVenda { get; set; }
        public bool PlanosComparacao { get; set; }
        public bool FormularioQualidade { get; set; }
        public bool FaqAtendimento { get; set; }
        public string idBancosDaCampanha { get; set; }
        public string idStatusDeAtendimento { get; set; }
        public string idStatusDeOferta { get; set; }
        public string idStatusDeAuditoria { get; set; }
        public string idFormasDePagamento { get; set; }
    }
}
