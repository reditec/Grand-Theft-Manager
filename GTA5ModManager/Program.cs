using System;
using System.Diagnostics;
using System.Windows.Forms;
using GTA5ModManager.Utilities;

namespace GTA5ModManager
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!Debugger.IsAttached)
            {
                ExceptionHandler.AddGlobalHandlers();

            }
                

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Tools.CheckForUpdates();

            Application.Run(new ManagerForm());

        }
    }
}