using System;
using System.Runtime.InteropServices;
using System.Text;

namespace v1Tabulare_z13.integracaoHuawei
{
    public class NativeWin32
    {
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_COMMAND = 0x0111;
        public const int WM_CLOSE = 0x0010;
        public const int BN_CLICKED = 0;
        public const int BM_CLICK = 0x00F5;

        private const int GWL_EXSTYLE = (-20);
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_APPWINDOW = 0x40000;

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public delegate int EnumWindowsProcDelegate(int hWnd, int lParam);

        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int FindWindow(
            string lpClassName, // class name 
            string lpWindowName // window name 
        );

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            int hWnd, // handle to destination window 
            uint Msg, // message 
            int wParam, // first message parameter 
            int lParam // second message parameter 
        );

        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);

        [DllImport("user32.dll")]
        public static extern bool BlockInput(bool block);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(int hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("User32.dll")]
        public static extern int SendMessage(int hWnd, int uMsg, int wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(
            int hWnd // handle to window
            );

        [DllImport("user32")]
        public static extern int EnumWindows(EnumWindowsProcDelegate lpEnumFunc, int lParam);

        [DllImport("User32.Dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);

        [DllImport("user32", EntryPoint = "GetWindowLongA")]
        public static extern int GetWindowLongPtr(int hwnd, int nIndex);

        [DllImport("user32")]
        public static extern int GetParent(int hwnd);

        [DllImport("user32")]
        public static extern int GetWindow(int hwnd, int wCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(int hWnd, ref RECT Rect);

        [DllImport("user32")]
        public static extern int IsWindowVisible(int hwnd);

        [DllImport("user32")]
        public static extern int GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern int GetDlgItem(int hWnd, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FindWindowEx(int hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
    }
}
