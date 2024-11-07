using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace biorand.app.Extensions
{
    public static class WindowExtension
    {
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        const int DWWMA_CAPTION_COLOR = 35;

        /// <summary>
        /// Apply a dark <b>Title Bar</b> on a Window.
        /// <br></br>
        /// <br><i>Note: Only work with <b>Windows 11</b>.</i></br>
        /// </summary>
        /// <param name="window"></param>
        public static void SetDarkTitleBar(this Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).EnsureHandle();
            int[] colorstr = new int[] { 0x202020 };
            DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, colorstr, 4);
        }
    }
}


