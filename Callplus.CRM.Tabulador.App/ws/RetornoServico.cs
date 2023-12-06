using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Callplus.ws
{
    public class RetornoServico
    {

        public RetornoServico(bool sucesso)
        {
            Sucesso = sucesso;


        }

        public RetornoServico(bool sucesso,string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
    }
}
