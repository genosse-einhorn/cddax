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
    class FlacWriter : IFileWriter
    {
        private Process m_flacProcess;

        public string FilenameExtension
        {
            get { return "flac"; }
        }

        public void Begin(System.IO.FileStream stream, FileWriterMeta meta)
        {
            // we only want the filename, flac.exe will open it again for itself
            string filename = stream.Name;
            stream.Dispose();

            ArgumentsBuilder ab = new ArgumentsBuilder();

            ab.Add("-o");
            ab.Add(filename);
            ab.Add("-f");
            
            ab.Add("--best");

            ab.Add("--force-raw-format");
            ab.Add("--endian=little");
            ab.Add("--sign=signed");
            ab.Add("--sample-rate=44100");
            ab.Add("--bps=16");
            ab.Add("--channels=2");
            
            ab.Add("-T");
            ab.Add("TRACKNUMBER={0}", meta.TrackNo);
            ab.Add("-T");
            ab.Add("TRACKTOTAL={0}", meta.TotalTrackCount);

            if (!string.IsNullOrEmpty(meta.Artist))
            {
                ab.Add("-T");
                ab.Add("ARTIST={0}", meta.Artist);
            }

            if (!string.IsNullOrEmpty(meta.Title))
            {
                ab.Add("-T");
                ab.Add("TITLE={0}", meta.Title);
            }

            if (!string.IsNullOrEmpty(meta.Composer))
            {
                ab.Add("-T");
                ab.Add("COMPOSER={0}", meta.Composer);
            }

            if (!string.IsNullOrEmpty(meta.Isrc))
            {
                ab.Add("-T");
                ab.Add("ISRC={0}", meta.Isrc);
            }

            if (!string.IsNullOrEmpty(meta.AlbumTitle))
            {
                ab.Add("-T");
                ab.Add("ALBUM={0}", meta.AlbumTitle);
            }

            if (!string.IsNullOrEmpty(meta.Year))
            {
                ab.Add("-T");
                ab.Add("DATE={0}", meta.Year);
            }

            if (!string.IsNullOrEmpty(meta.DiscNo))
            {
                ab.Add("-T");
                ab.Add("DISCNUMBER={0}", meta.DiscNo);
            }

            ab.Add("-"); // read from stdin

            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = FlacExecutable;
            si.Arguments = ab.ToString();
            si.RedirectStandardError = true;
            si.RedirectStandardOutput = true;
            si.RedirectStandardInput = true;
            si.UseShellExecute = false;
            si.CreateNoWindow = true;

            m_flacProcess = Process.Start(si);
            m_flacProcess.BeginOutputReadLine();
            m_flacProcess.BeginErrorReadLine();

            m_flacProcess.OutputDataReceived += m_flacProcess_OutputDataReceived;
            m_flacProcess.ErrorDataReceived += m_flacProcess_ErrorDataReceived;
        }

        void m_flacProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (m_flacProcess != null && e.Data != null)
                Logger.Info("flac({0}) stderr: {1}", m_flacProcess.Id, e.Data);
        }

        void m_flacProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (m_flacProcess != null && e.Data != null)
                Logger.Info("flac({0}) stdout: {1}", m_flacProcess.Id, e.Data);
        }

        public void WriteData(byte[] buffer, int indexSample, int numSamples)
        {
            m_flacProcess.StandardInput.BaseStream.Write(buffer, indexSample * 4, numSamples * 4);
        }

        public void Finish()
        {
            if (m_flacProcess != null)
            {
                m_flacProcess.StandardInput.Close();
                m_flacProcess.WaitForExit();
                int ec = m_flacProcess.ExitCode;
                m_flacProcess.Dispose();
                m_flacProcess = null;

                if (ec != 0)
                    throw new FlacWriterException(ec);
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

        ~FlacWriter()
        {
            Dispose(false);
        }

        private static string s_flacExe;
        private static string FlacExecutable
        {
            get
            {
                if (string.IsNullOrEmpty(s_flacExe))
                {
                    if (OSHelper.IsWindows)
                    {
                        // try flac64.exe included with CddaX
                        string flac64 = Path.Combine(Path.GetDirectoryName(typeof(FlacWriter).Assembly.Location), "flac64.exe");
                        if (OSHelper.CanRun(flac64, "--version"))
                        {
                            s_flacExe = flac64;
                        }
                        else
                        {
                            // if that didn't work, try 32bit flac.exe
                            string flac32 = Path.Combine(Path.GetDirectoryName(typeof(FlacWriter).Assembly.Location), "flac.exe");
                            if (OSHelper.CanRun(flac32, "--version"))
                            {
                                s_flacExe = flac32;
                            }
                            else
                            {
                                // last resort: use whatever flac is on %PATH%
                                s_flacExe = "flac";
                            }
                        }
                    }
                    else
                    {
                        // use whatever flac is on $PATH
                        s_flacExe = "flac";
                    }
                }

                return s_flacExe;
            }
        }

        public class FlacWriterException : Exception
        {
            public int ExitCode { get; private set; }

            public FlacWriterException(int exit)
                : base(string.Format("flac.exe returned exit code {0}", exit))
            {
                ExitCode = exit;
            }
        }
    }
}
