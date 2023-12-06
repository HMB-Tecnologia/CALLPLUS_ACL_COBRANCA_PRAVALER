using Callplus.CRM.Tabulador.Dominio.Tipos;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class FormaDePagamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

       public TipoFormaDePagamento  TipoFormaDePagamento
       {
           get { return (TipoFormaDePagamento) Id; }
       }
    }
}
