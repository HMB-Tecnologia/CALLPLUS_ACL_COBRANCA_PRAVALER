using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using mshtml;
using System.Runtime.InteropServices;
using System.Timers;
using System.Net.Mail;
using System.Net;
//using AxSHDocVw;
using SHDocVw;
using System.IO;
using Callplus.CRM.Tabulador.Servico.Servicos;
using System.Net.Sockets;

namespace v1Tabulare_z13.integracaoHuawei
{
    public class InputDados
    {
        #region Importar DLLs

        public const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle,
        IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd,
        int msg, int wParam, StringBuilder ClassName);

        [DllImport("user32")]
        public static extern bool ShowWindow(int hWnd, int nCmdShow);
        #endregion

        #region AutoIt

        public static NativeWin32.RECT RectMainWindow = new NativeWin32.RECT();

        public static void ConfigurarJanela(string title)
        {
            Log.RegistrarExecucao("TITULO DA JANELA: " + title);
            int iHandle = 0;

            //Thread.Sleep(1000);

            iHandle = NativeWin32.FindWindow(null, title);
            Log.RegistrarExecucao("HANDLE: " + iHandle);
            //Thread.Sleep(1000);

            NativeWin32.SetForegroundWindow(iHandle);

            //Thread.Sleep(1000);

            NativeWin32.MoveWindow(iHandle, 0, 0, 676, Screen.PrimaryScreen.Bounds.Height, true);

            RectMainWindow = new NativeWin32.RECT();
            Log.RegistrarExecucao("RECT MAIN WINDOW: " + RectMainWindow);

            NativeWin32.GetWindowRect(iHandle, ref RectMainWindow);

            //Thread.Sleep(2000);
        }

        #endregion AutoIt

        public static string GetURL(IntPtr intPtr, string programName, string url = "")
        {
            string temp = null;
            bool achouHuawei = false;

            //if (programName.Equals("chrome"))
            //{
            //    var hAddressBox = FindWindowEx(intPtr, IntPtr.Zero, "Chrome_OmniboxView", IntPtr.Zero);
            //    var sb = new StringBuilder(256);
            //    SendMessage(hAddressBox, 0x000D, 256, sb);
            //    temp = sb.ToString();
            //}

            if (programName.Equals("iexplore"))
            {
                SHDocVw.ShellWindows shellWindows = new ShellWindows();

                foreach (SHDocVw.IWebBrowser2 ie in shellWindows)
                {
                    if (ie.LocationURL == null || string.IsNullOrEmpty(ie.LocationURL))
                        continue;

                    //var urlHuawei = "10.64.0.150:8080/csp/mif/mainFrame.action";
                    var controller = new ProspectService();

                    var ip = GetLocalIPAddress();

                    controller.InserirLogHuawei(ip, ie.LocationURL.ToUpper());

                    var listaUrlHuawei = controller.RetornarUrlHuawei();

                    foreach (string urlDaLista in listaUrlHuawei)
                    {
                        if (ie.LocationURL.ToUpper().Contains(urlDaLista))
                        {
                            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ie.FullName);
                            if (fileNameWithoutExtension != null)
                            {
                                var filename = fileNameWithoutExtension.ToLower();
                                if (filename != null && filename.Equals("iexplore"))
                                {
                                    mshtml.HTMLDocument doc = (mshtml.HTMLDocument)ie.Document;
                                    if (doc.title != null)
                                    {
                                        temp += ie.LocationURL + "|" + doc.title + " ";
                                        achouHuawei = true;
                                        Log.RegistrarExecucao("TEMP: " + temp);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //if (listaUrlHuawei.Contains(ie.LocationURL.ToUpper()))
                    //{
                    //    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ie.FullName);
                    //    if (fileNameWithoutExtension != null)
                    //    {
                    //        var filename = fileNameWithoutExtension.ToLower();
                    //        if (filename.Equals("iexplore"))
                    //        {
                    //            mshtml.HTMLDocument doc = (mshtml.HTMLDocument)ie.Document;
                    //            temp += ie.LocationURL + "|" + doc.title + " ";
                    //            achouHuawei = true;
                    //        }
                    //    }
                    //}
                }
            }

            if (!achouHuawei)
            {
                throw new SystemException("Não foi possivel encontrar a tela do Huawei.");
            }

            return temp;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return null;
        }
    }
}