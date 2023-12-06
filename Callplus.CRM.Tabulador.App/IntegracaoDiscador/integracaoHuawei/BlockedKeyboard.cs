using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace v1Tabulare_z13.integracaoHuawei
{
    public class BlockedKeyboard
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int code, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int code, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13; // Tipo de hook que será usado
        const int WM_KEYDOWN = 0x100;  // Messagem usada para quando uma tecla for pressionada

        private static LowLevelKeyboardProc hook = hookProc;
        private static IntPtr hhook = IntPtr.Zero;

        public static void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hook, hInstance, 0); // Instala o hook para o teclado
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }

        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            { // Quando uma tecla for pressionada
                return (IntPtr)1; // Inibe o funcionamento
            }
            else
                return CallNextHookEx(hhook, code, (int)wParam, lParam); // Passa para o próximo evento
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UnHook(); // Ao fechar o form desintala o hook
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetHook(); // Ao iniciar instala o hook
        }
    }
}
