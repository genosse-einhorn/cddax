using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.Ripper
{
    class PregapDetectingWriter : IFileWriter
    {
        private IFileWriter m_innerWriter = null;
        private bool m_pregapFinished = false;
        private int m_trackNo = 0;
        private int m_numPregapSamples = 0;
        private int m_numPostgapSamples = 0;

        public string FilenameExtension
        {
            get
            {
                return m_innerWriter.FilenameExtension; 
            }
        }

        public void Begin(System.IO.FileStream stream, FileWriterMeta meta)
        {
            m_trackNo = meta.TrackNo;
            m_numPregapSamples = 0;
            m_numPostgapSamples = 0;
            m_pregapFinished = false;

            m_innerWriter.Begin(stream, meta);
        }

        public void WriteData(byte[] buffer, int indexSample, int numSamples)
        {
            if (!m_pregapFinished)
            {
                for (int i = 0; i < numSamples; ++i)
                {
                    if (buffer[(indexSample + i) * 4] != 0
                        || buffer[(indexSample + i) * 4 + 1] != 0
                        || buffer[(indexSample + i) * 4 + 2] != 0
                        || buffer[(indexSample + i) * 4 + 3] != 0)
                    {
                        m_pregapFinished = true;
                        m_numPregapSamples += i;
                        indexSample += i;
                        numSamples -= i;

                        Log.Logger.Info("Track {0}: Found pre-gap {1} samples", m_trackNo, m_numPregapSamples);

                        break;
                    }
                }
            }

            if (m_pregapFinished)
            {
                // analyze for post-gap
                int thisBufferPostgap = 0;
                for (int i = 0; i < numSamples; ++i)
                {
                    if (buffer[(indexSample + numSamples - 1 - i) * 4] != 0
                        || buffer[(indexSample + numSamples - 1 - i) * 4 + 1] != 0
                        || buffer[(indexSample + numSamples - 1 - i) * 4 + 2] != 0
                        || buffer[(indexSample + numSamples - 1 - i) * 4 + 3] != 0)
                    {
                        // found data! emit ignored old postgap samples
                        while (m_numPostgapSamples >= s_postgapZeroBuf.Length / 4)
                        {
                            m_innerWriter.WriteData(s_postgapZeroBuf, 0, s_postgapZeroBuf.Length / 4);
                            m_numPostgapSamples -= s_postgapZeroBuf.Length / 4;
                        }
                        if (m_numPostgapSamples > 0)
                        {
                            m_innerWriter.WriteData(s_postgapZeroBuf, 0, m_numPostgapSamples);
                            m_numPostgapSamples = 0;
                        }

                        break;
                    }
                    thisBufferPostgap++;
                }
                m_numPostgapSamples += thisBufferPostgap;
                numSamples -= thisBufferPostgap;

                if (numSamples > 0)
                {
                    m_innerWriter.WriteData(buffer, indexSample, numSamples);
                }
            }
            else
            {
                m_numPregapSamples += numSamples;
            }
        }

        public void Finish()
        {
            Log.Logger.Info("Track {0}: Found post-gap {1} samples", m_trackNo, m_numPostgapSamples);

            m_innerWriter.Finish();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Finish();

                m_innerWriter.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PregapDetectingWriter()
        {
            Dispose(false);
        }

        public PregapDetectingWriter(IFileWriter baseWriter)
        {
            m_innerWriter = baseWriter;
        }

        private static readonly byte[] s_postgapZeroBuf = new byte[1000];
    }
}
