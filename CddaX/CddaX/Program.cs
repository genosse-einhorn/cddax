using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace CddaX
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Version.Parse(FileVersionInfo.GetVersionInfo(typeof(int).Assembly.Location).ProductVersion) >= new Version(4, 7))
            {
                string configFile = typeof(Program).Assembly.Location + ".config.net47";
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", configFile);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
