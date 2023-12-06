namespace Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico
{
    public class ConfiguracaoDeCampoDoLayoutDinamico
    {
        public int Id { get; set; }
        public int idCampo { get; set; }
        public string Label { get; set; }
        public int Linha { get; set; }
        public int Tamanho { get; set; }
        public int Ordem { get; set; }
        public int IdTipoExibicao { get; set; }
        public bool Habilitado { get; set; }
        public bool Visivel { get; set; }
        public bool SomenteLeitura { get; set; }
        public int? TamanhoTexto { get; set; }
        public string ValoresJason { get; set; }
        public int IdLayout { get; set; }
    }
}
