using System.Linq;
using System.Net;

namespace CallplusUtil.Environment
{
    public class ConfiguracaoDeAmbiente
    {
        public static string HostName => Dns.GetHostName();

        public static string RetornarEnderecoIP()
        {
            IPAddress[] ipsLocais = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress ipLocalSelecionado = ipsLocais.FirstOrDefault(ipLocal => ipLocal.ToString().Contains(":") == false);
            return ipLocalSelecionado?.ToString() ?? "";
        }
    }
}
