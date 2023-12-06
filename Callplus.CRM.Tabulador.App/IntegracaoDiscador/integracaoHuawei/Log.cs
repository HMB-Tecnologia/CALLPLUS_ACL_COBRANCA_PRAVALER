using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace v1Tabulare_z13.integracaoHuawei
{
    public static class Log
    {
        private static string diretorioDeLogs = AppDomain.CurrentDomain.BaseDirectory;
        private const string nomeArquivoLog = "CallPlus_Log_Execucao_";

        public static void RegistrarExecucao(string mensagem)
        {
            mensagem = "*" + DateTime.Now.ToString("HH:mm:ss:fff") + "* " + mensagem + Environment.NewLine;
            Console.Write(mensagem);

            try
            {
                if (!Directory.Exists(diretorioDeLogs)) Directory.CreateDirectory(diretorioDeLogs);

                using (TextWriter tw = new StreamWriter(Path.Combine(diretorioDeLogs, nomeArquivoLog + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true))
                {
                    tw.WriteLine(mensagem);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();
                }
            }
            catch (Exception e)
            {

            }
        }
        public static void RegistrarErro(string mensagem, DateTime data)
        {
            Console.WriteLine(mensagem);
            try
            {
                if (!Directory.Exists(diretorioDeLogs)) Directory.CreateDirectory(diretorioDeLogs);

                using (TextWriter tw = new StreamWriter(Path.Combine(diretorioDeLogs, nomeArquivoLog + "ERRO_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".txt"), true))
                {
                    tw.WriteLine(mensagem);
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();
                }

                Enviar_Email("suporte@hmbtecnologia.com", "suporte@hmbtecnologia.com", "ERRO MAPEAMENTO DIRETÓRIO - CALLPLUS RIANATEL", mensagem, data);
            }
            catch (Exception e)
            {

            }
        }

        private static void Enviar_Email(string remetente, string destinario, string assunto, string enviaMensagem, DateTime data)
        {
            MailMessage mensagemEmail = new MailMessage(remetente, ConfigurationManager.AppSettings["emailsLog"], string.Format("{0} - DATA PROCESSAMENTO: {1}", assunto, data.ToString("dd/MM/yyyy")), enviaMensagem);

            using (var smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("suporte@hmbtecnologia.com.br", "hmb@2017!@#");

                try
                {
                    smtp.Send(mensagemEmail);
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
