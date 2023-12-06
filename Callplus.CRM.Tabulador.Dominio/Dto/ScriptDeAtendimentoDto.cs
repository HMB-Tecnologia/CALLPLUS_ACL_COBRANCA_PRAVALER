namespace Callplus.CRM.Tabulador.Dominio.Dto
{
    public class ScriptDeAtendimentoDto
    {
        public int? IdResposta { get; set; }
        public int? IdEtapaResposta { get; set; }
        public bool? AtivoResposta { get; set; }
        public int? IdProximaEtapaResposta { get; set; }
        public string DescricaoResposta { get; set; }
        public int? IdEtapa { get; set; }
        public string TituloEtapa { get; set; }
        public string DescricaoHtmlEtapa { get; set; }
        public int? IdScriptEtapa { get; set; }
        public bool? AtivoEtapa { get; set; }
        public int IdScript { get; set; }
        public string NomeScript { get; set; }
        public int IdPrimeiraEtapaScript { get; set; }
        public string ObservacaoScript { get; set; }
        public bool AtivoScript { get; set; }
        public bool RespostaAutomatica { get; set; }
        public string Ddd { get; set; }
    }
}
