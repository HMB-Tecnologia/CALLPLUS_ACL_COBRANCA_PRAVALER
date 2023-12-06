namespace Callplus.CRM.Tabulador.Dominio.Entidades.ScriptAtendimento
{
    public class RespostaDaEtapaDoScriptDeAtendimento
    {
        public int Id { get; set; }
        public int IdEtapaDoScriptDeAtendimento { get; set; }
        public int? IdProximaEtapaDoScriptDeAtendimento { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        
        public EtapaDoScriptDeAtendimento ProximaEtapa { get; set; }
        public bool RespostaAutomatica { get; set; }
    }
}
