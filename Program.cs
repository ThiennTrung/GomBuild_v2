using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GomBuild_v2
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "MyUniqueAppMutex");

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        const int SW_RESTORE = 9;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //// Check if the application is already running
            //if (!mutex.WaitOne(TimeSpan.Zero, true))
            //{
            //    // Find the existing process
            //    Process currentProcess = Process.GetCurrentProcess();
            //    foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            //    {
            //        if (process.Id != currentProcess.Id)
            //        {
            //            // Bring the existing window to the front
            //            IntPtr hWnd = process.MainWindowHandle;
            //            if (hWnd != IntPtr.Zero)
            //            {
            //                ShowWindow(hWnd, SW_RESTORE);
            //                SetForegroundWindow(hWnd);
            //            }
            //            break;
            //        }
            //    }
            //    return; // Exit the new instance
            //}
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            // Release mutex on exit
            //mutex.ReleaseMutex();
        }
    }
}
