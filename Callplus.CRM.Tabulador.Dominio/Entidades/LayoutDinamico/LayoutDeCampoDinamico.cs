using System;
using System.Collections.Generic;

namespace Callplus.CRM.Tabulador.Dominio.Entidades.LayoutDinamico
{
    public class LayoutDeCampoDinamico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public IList<LinhaDeCampoDinamico> Linhas;

        public LayoutDeCampoDinamico()
        {
            Linhas = new List<LinhaDeCampoDinamico>();
        }        
    }
}
