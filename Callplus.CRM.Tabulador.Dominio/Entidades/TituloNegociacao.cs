namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class TituloNegociacao
    {
        public TituloNegociacao(long idTitulo, long idNegociacao)
        {
            IDTitulo = idTitulo;
            IDNegociacao = idNegociacao;
        }

        public long IDTitulo { get;  }
        public long IDNegociacao { get;  }
    }
}
