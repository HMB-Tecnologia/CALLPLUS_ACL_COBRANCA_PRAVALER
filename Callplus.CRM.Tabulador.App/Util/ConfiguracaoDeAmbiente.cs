using System.Linq;
using System.Net;

namespace Callplus.CRM.Tabulador.App.Util
{
    public class ConfiguracaoDeAmbiente
    {

        public static string HostName => Dns.GetHostName();
        public static string Release { get; set; }

        public static string RetornarEnderecoIP()
        {
            IPAddress[] ipsLocais = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipLocalSelecionado = ipsLocais.FirstOrDefault(ipLocal => ipLocal.ToString().Contains(":") == false);
            return ipLocalSelecionado?.ToString() ?? "";
        }

        public static IPAddress RetornarIP()
        {
            IPAddress[] ipsLocais = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipLocalSelecionado = ipsLocais.FirstOrDefault(ipLocal => ipLocal.ToString().Contains(":") == false);
            return ipLocalSelecionado;
        }
    }
}
