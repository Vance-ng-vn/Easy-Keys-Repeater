using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace easy_key_repeater
{
    class KeyBoardUtility
    {
        public struct Key
        {
            public string name;
            public uint wParam;
            public uint scancode;
        }
        private static Key generateKey(string name, uint wParam, uint scancode)
        {
            Key key;
            key.name = name;
            key.wParam = wParam;
            key.scancode = scancode;
            return key;
        }
        public static Key[] KeysList =
        {
            generateKey("ESC", (uint)Keys.Escape, 0x01),
            generateKey("F1", (uint)Keys.F1, 0x3B),
            generateKey("F2", (uint)Keys.F2, 0x3C),
            generateKey("F3", (uint)Keys.F3, 0x3D),
            generateKey("F4", (uint)Keys.F4, 0x3E),
            generateKey("F5", (uint)Keys.F5, 0x3F),
            generateKey("F6", (uint)Keys.F6, 0x40),
            generateKey("F7", (uint)Keys.F7, 0x41),
            generateKey("F8", (uint)Keys.F8, 0x42),
            generateKey("F9", (uint)Keys.F9, 0x43),
            generateKey("F10", (uint)Keys.F10, 0x44),
            generateKey("F11", (uint)Keys.F11, 0x57),
            generateKey("F12", (uint)Keys.F12, 0x58),
            generateKey("PrintScreen", (uint)Keys.PrintScreen, 0xE037),
            generateKey("Insert", (uint)Keys.Insert, 0xE052),
            generateKey("Delete", (uint)Keys.Delete, 0xE053),
            generateKey("PageUp", (uint)Keys.PageUp, 0xE049),
            generateKey("PageDown", (uint)Keys.PageDown, 0xE051),
            generateKey("Home", (uint)Keys.Home, 0xE047),
            generateKey("End", (uint)Keys.End, 0xE04F),
            generateKey("` (Grave)", (uint)Keys.Oem3, 0x5C),
            generateKey("1", (uint)Keys.D1, 0x02),
            generateKey("2", (uint)Keys.D2, 0x03),
            generateKey("3", (uint)Keys.D3, 0x04),
            generateKey("4", (uint)Keys.D4, 0x05),
            generateKey("5", (uint)Keys.D5, 0x06),
            generateKey("6", (uint)Keys.D6, 0x07),
            generateKey("7", (uint)Keys.D7, 0x08),
            generateKey("8", (uint)Keys.D8, 0x09),
            generateKey("9", (uint)Keys.D9, 0x0A),
            generateKey("0", (uint)Keys.D0, 0x0B),
            generateKey("- (Minus)", (uint)Keys.OemMinus, 0x0C),
            generateKey("= (Equals)", (uint)Keys.Oemplus, 0x0D),
            generateKey("BackSpace", (uint)Keys.Back, 0x0E),
            generateKey("Tab", (uint)Keys.Tab, 0x0F),
            generateKey("Q", (uint)Keys.Q, 0x10),
            generateKey("W", (uint)Keys.W, 0x11),
            generateKey("E", (uint)Keys.E, 0x12),
            generateKey("R", (uint)Keys.R, 0x13),
            generateKey("T", (uint)Keys.T, 0x14),
            generateKey("Y", (uint)Keys.Y, 0x15),
            generateKey("U", (uint)Keys.U, 0x16),
            generateKey("I", (uint)Keys.I, 0x17),
            generateKey("O", (uint)Keys.O, 0x18),
            generateKey("P", (uint)Keys.P, 0x19),
            generateKey("[ (BacketLeft)", (uint)Keys.Oem4, 0x5E),
            generateKey("] (BracketRight)", (uint)Keys.Oem6, 0x6F),
            generateKey(@"\ (BackSlash)", (uint)Keys.Oem5, 0x2B),

            generateKey("CapsLock", (uint)Keys.CapsLock, 0x3A),
            generateKey("A", (uint)Keys.A, 0x1E),
            generateKey("S", (uint)Keys.S, 0x1F),
            generateKey("D", (uint)Keys.D, 0x20),
            generateKey("F", (uint)Keys.F, 0x21),
            generateKey("G", (uint)Keys.G, 0x22),
            generateKey("H", (uint)Keys.H, 0x23),
            generateKey("J", (uint)Keys.J, 0x24),
            generateKey("K", (uint)Keys.K, 0x25),
            generateKey("L", (uint)Keys.L, 0x26),
            generateKey("; (Semicolon)", (uint)Keys.Oem1, 0x5A),
            generateKey("' (Apostrophe)", (uint)Keys.Oem7, 0x71),
            generateKey("Enter", (uint)Keys.Enter, 0x1C),

            generateKey("Shift (Left)", (uint)Keys.LShiftKey, 0x2A),
            generateKey("Z", (uint)Keys.Z, 0x2C),
            generateKey("X", (uint)Keys.X, 0x2D),
            generateKey("C", (uint)Keys.C, 0x2E),
            generateKey("V", (uint)Keys.V, 0x2F),
            generateKey("B", (uint)Keys.B, 0x30),
            generateKey("N", (uint)Keys.N, 0x31),
            generateKey("M", (uint)Keys.M, 0x32),
            generateKey("< (Comma)", (uint)Keys.Oemcomma, 0x33),
            generateKey("> (Preiod)", (uint)Keys.OemPeriod, 0x34),
            generateKey("/ (Slash)", (uint)Keys.Oem2, 0x5B),
            generateKey("Shift (Right)", (uint)Keys.RShiftKey, 0x36),

            generateKey("Control (Left)", (uint)Keys.LControlKey, 0x1D),
            generateKey("Alt (Left)", (uint)Keys.LMenu, 0x38),
            generateKey("Space", (uint)Keys.Space, 0x39),
            generateKey("Alt (Right)", (uint)Keys.RMenu, 0xE038),
            generateKey("Control (Right)", (uint)Keys.RControlKey, 0xE01D),
        };
        

        private const UInt32 WM_KEYDOWN = 0x0100;
        private const UInt32 WM_KEYUP = 0x0101;
        private const UInt32 WM_LBUTTONDOWN = 0x0201;
        private const UInt32 WM_LBUTTONUP = 0x0202;
        private const UInt32 WM_SETCURSOR = 0x0020;
        private const UInt32 WM_MOUSEMOVE = 0x0200;
        private const UInt32 WM_MOUSEACTIVE = 0x0021;


        public async void KeyPressWindow(IntPtr hWnd, uint VK_KEY, uint scancode) {
            //IntPtr hWnd, UInt32 uMsg, IntPtr wParam, IntPtr lParam
            User32.PostMessage(hWnd, WM_KEYDOWN, VK_KEY, KeyDownLParam(scancode));
            await TaskDelay.PutTaskDelay(50);
            User32.PostMessage(hWnd, WM_KEYUP, VK_KEY, KeyUpLParam(scancode));
        }
        public async void ClickWindow(IntPtr Parent_hWnd, IntPtr hWnd, Point point)
        {
            User32.SendMessage(hWnd, WM_SETCURSOR, (uint)hWnd, SetCursorLParam(WM_MOUSEMOVE, 1));
            await TaskDelay.PutTaskDelay(10);
            User32.PostMessage(hWnd, WM_MOUSEMOVE, 0, ClickLParam(point.X, point.Y));
            await TaskDelay.PutTaskDelay(20);
            User32.SendMessage(hWnd, WM_MOUSEACTIVE, (uint)Parent_hWnd, SetCursorLParam(WM_LBUTTONDOWN, 1));
            await TaskDelay.PutTaskDelay(10);
            User32.SendMessage(hWnd, WM_SETCURSOR, (uint)hWnd, SetCursorLParam(WM_LBUTTONDOWN, 1));
            await TaskDelay.PutTaskDelay(10);
            User32.PostMessage(hWnd, WM_LBUTTONDOWN, 1, ClickLParam(point.X, point.Y));
            await TaskDelay.PutTaskDelay(20);
            User32.PostMessage(hWnd, WM_MOUSEMOVE, 1, ClickLParam(point.X, point.Y));
            await TaskDelay.PutTaskDelay(20);
            User32.PostMessage(hWnd, WM_LBUTTONUP, 0, ClickLParam(point.X, point.Y));
            await TaskDelay.PutTaskDelay(20);
            User32.SendMessage(hWnd, WM_SETCURSOR, (uint)hWnd, SetCursorLParam(WM_MOUSEMOVE, 1));
            await TaskDelay.PutTaskDelay(10);
            User32.PostMessage(hWnd, WM_MOUSEMOVE, 0, ClickLParam(point.X, point.Y));
        }
        public static uint SetCursorLParam(uint WM_MOUSE, uint WM_MACHINE)
        {
            //test
            return (WM_MOUSE << 16) | (WM_MACHINE << 32); 
        }
        public static uint ClickLParam(int x, int y)
        {
            return (uint)((y << 16) | (x & 0xFFFF));
        }
        public bool KeyPressDesktop(int VK_KEY) {
            return false;
        }
        public uint KeyDownLParam(uint VK_KEY)
        {
            return (VK_KEY << 16);
        }
        public uint KeyUpLParam(uint VK_KEY)
        {
            uint previousState = 1;
            uint transition = 1;
            return (VK_KEY << 16) | (previousState << 30) | (transition << 31);
        }
    }

    class User32
    {
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, UInt32 uMsg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 uMsg, uint wParam, uint lParam);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point point);
    }
}
