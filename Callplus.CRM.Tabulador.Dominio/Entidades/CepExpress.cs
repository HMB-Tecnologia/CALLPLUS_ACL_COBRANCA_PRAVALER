using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callplus.CRM.Tabulador.Dominio.Entidades
{
    public class CepExpress
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdCampanha { get; set; }
        public string NomeArquivo { get; set; }
        public int IdCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdModificador { get; set; }
        public DateTime DataModificacao { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        public int idStatusProcessamento { get; set; }
        public bool Ativo { get; set; }

    }
}
