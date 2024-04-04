using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CheckCruise
{
    static class Program
    {

        private static string _appVersion;
        public static string AppVersion => _appVersion ??= GetAppVersion();

        static string GetAppVersion()
        {
            try
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            }
            catch
            {
                return "Unknown";
            }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }
}
