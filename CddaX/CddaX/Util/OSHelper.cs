using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using CddaX.Log;
using System.Windows.Forms;
using System.Diagnostics;

namespace CddaX.Util
{
    static class OSHelper
    {
        public static bool IsWindows
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT;
            }
        }

        public static bool IsVistaOrLater
        {
            get
            {
                return IsWindows && Environment.OSVersion.Version >= new Version(6, 0);
            }
        }

        public static bool IsWin10OrLater
        {
            get
            {
                return IsWindows && Environment.OSVersion.Version >= new Version(10, 0);
            }
        }

        public static bool CanRun(string exename, string args)
        {
            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = exename;
            si.Arguments = args;
            si.RedirectStandardError = true;
            si.RedirectStandardOutput = true;
            si.RedirectStandardInput = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;

            try
            {
                using (Process p = Process.Start(si))
                {
                    p.OutputDataReceived += (o, e) => { }; // discard any output
                    p.ErrorDataReceived += (o, e) => { };
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                    p.WaitForExit(30000);
                    return p.ExitCode == 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void LaunchUrl(IWin32Window window, string url)
        {
            try
            {
                if (IsWindows)
                {
                    Process.Start(url);
                }
                else
                {
                    // work around mono bug
                    ProcessStartInfo si = new ProcessStartInfo();
                    si.FileName = "xdg-open";
                    si.Arguments = ArgumentsBuilder.QuotedArg(url);
                    si.UseShellExecute = false;
                    Process.Start(si);
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e, "Trying to launch url '{0}', url");
                MessageBox.Show(window, e.Message, CddaX.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
