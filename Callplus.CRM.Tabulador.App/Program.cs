using System;
using System.Windows.Forms;
using Callplus.CRM.Tabulador.App.Operacao;
using Callplus.CRM.Tabulador.App.Login;

namespace Callplus.CRM.Tabulador.App
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ScriptAtendimentoForm());
            //Application.Run(new TesteForm());
           // Application.Run(new AtendimentoForm2(null));
            Application.Run(new LoginForm());
        }
    }
}
