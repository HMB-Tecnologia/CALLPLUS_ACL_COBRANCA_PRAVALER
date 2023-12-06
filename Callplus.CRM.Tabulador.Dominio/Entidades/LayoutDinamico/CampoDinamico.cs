namespace Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico
{
    public class CampoDinamico
    {
        public int IdCampo { get; set; }
        public string NomeDoCampo { get; set; }
        public int Ordem { get; set; }
        public int Tamanho { get; set; }
        public string Label { get; set; }
        public TipoExibicaoCampoDinamico TipoExibicao { get; set; }
        public string ValoresJson { get; set; }
        public bool SomenteLeitura { get; set; }
        public bool Habilitado { get; set; }
        public int Linha { get; set; }
        public int? TamanhoTexto { get; set; }
    }

    public enum TipoExibicaoCampoDinamico
    {
        TextBox = 1,
        ComboBox = 2
    }
}
