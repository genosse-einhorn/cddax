using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using CddaX.Log;
using CddaX.Util;
using System.IO;

namespace CddaX.Ripper
{
    class LameWriter : IFileWriter
    {
        private Process m_lameProcess;

        public string FilenameExtension
        {
            get { return "mp3"; }
        }

        public void Begin(System.IO.FileStream stream, FileWriterMeta meta)
        {
            // we only want the filename, lame.exe will open it again for itself
            string filename = stream.Name;
            stream.Dispose();

            ArgumentsBuilder ab = new ArgumentsBuilder();

            ab.Add("-r");
            
            ab.Add("-s");
            ab.Add("44.1");

            if (!string.IsNullOrEmpty(meta.Mp3Quality.LameParameter))
            {
                ab.AddRaw(meta.Mp3Quality.LameParameter);
            }

            ab.Add("--tn");
            ab.Add("{0}/{1}", meta.TrackNo, meta.TotalTrackCount);

            if (!string.IsNullOrEmpty(meta.Artist))
            {
                ab.Add("--ta");
                ab.Add(meta.Artist);
            }

            if (!string.IsNullOrEmpty(meta.Title))
            {
                ab.Add("--tt");
                ab.Add(meta.Title);
            }

            if (!string.IsNullOrEmpty(meta.Composer))
            {
                ab.Add("--tv");
                ab.Add("TCOM={0}", meta.Composer);
            }

            if (!string.IsNullOrEmpty(meta.Isrc))
            {
                ab.Add("--tv");
                ab.Add("TSRC={0}", meta.Isrc);
            }

            if (!string.IsNullOrEmpty(meta.AlbumTitle))
            {
                ab.Add("--tl");
                ab.Add(meta.AlbumTitle);
            }

            int year = 0;
            if (int.TryParse(meta.Year, out year) && year > 0 && year < 10000)
            {
                ab.Add("--ty");
                ab.Add("{0}", year);
            }

            ab.Add("-"); // read from stdin
            ab.Add(filename); // write to previously created file


            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = LameExecutable;
            si.Arguments = ab.ToString();
            si.RedirectStandardError = true;
            si.RedirectStandardOutput = true;
            si.RedirectStandardInput = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;

            m_lameProcess = Process.Start(si);
            m_lameProcess.OutputDataReceived += m_lameProcess_OutputDataReceived;
            m_lameProcess.ErrorDataReceived += m_lameProcess_ErrorDataReceived;
            m_lameProcess.BeginOutputReadLine();
            m_lameProcess.BeginErrorReadLine();
        }

        void m_lameProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (m_lameProcess != null && e.Data != null)
                Logger.Info("lame({0}) stderr: {1}", m_lameProcess.Id, e.Data);
        }

        void m_lameProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (m_lameProcess != null && e.Data != null)
                Logger.Info("lame({0}) stdout: {1}", m_lameProcess.Id, e.Data);
        }

        public void WriteData(byte[] buffer, int indexSample, int numSamples)
        {
            m_lameProcess.StandardInput.BaseStream.Write(buffer, indexSample * 4, numSamples * 4);
        }

        public void Finish()
        {
            if (m_lameProcess != null)
            {
                m_lameProcess.StandardInput.Close();
                m_lameProcess.WaitForExit();
                int ec = m_lameProcess.ExitCode;
                m_lameProcess.Dispose();
                m_lameProcess = null;

                if (ec != 0)
                    throw new LameWriterException(ec);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            Finish();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LameWriter()
        {
            Dispose(false);
        }

        private static string s_lameExe;
        private static string LameExecutable {
            get
            {
                if (string.IsNullOrEmpty(s_lameExe))
                {
                    if (OSHelper.IsWindows)
                    {
                        // try lame64.exe included with CddaX
                        string lame64 = Path.Combine(Path.GetDirectoryName(typeof(LameWriter).Assembly.Location), "lame64.exe");
                        if (OSHelper.CanRun(lame64, "--version"))
                        {
                            s_lameExe = lame64;
                        }
                        else
                        {
                            // if that didn't work, try 32bit lame.exe
                            string lame32 = Path.Combine(Path.GetDirectoryName(typeof(LameWriter).Assembly.Location), "lame.exe");
                            if (OSHelper.CanRun(lame32, "--version"))
                            {
                                s_lameExe = lame32;
                            }
                            else
                            {
                                // last resort: use whatever lame is on %PATH%
                                s_lameExe = "lame";
                            }
                        }
                    }
                    else
                    {
                        // use whatever lame is on $PATH
                        s_lameExe = "lame";
                    }
                }

                return s_lameExe;
            }
        }

        public class LameWriterException : Exception
        {
            public int ExitCode { get; private set; }

            public LameWriterException(int exit)
                : base(string.Format("lame.exe returned exit code {0}", exit))
            {
                ExitCode = exit;
            }
        }
    }
}
