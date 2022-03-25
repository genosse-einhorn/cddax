using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CddaX.Ripper
{
    class WavFileWriter : IFileWriter
    {
        private FileStream m_file = null;
        private int m_samplesWritten = 0;

        public string FilenameExtension
        {
            get
            {
                return "wav";
            }
        }

        public void Begin(FileStream stream, FileWriterMeta meta)
        {
            m_file = stream;

            // write dummy header - we fill in the length on finish
            byte[] header = new byte[44];

            // RIFF header
            header[0] = (byte)'R';
            header[1] = (byte)'I';
            header[2] = (byte)'F';
            header[3] = (byte)'F';
            header[4] = 0xff; // total chunk size - fill in later
            header[5] = 0xff;
            header[6] = 0xff;
            header[7] = 0xff;
            header[8] = (byte)'W';
            header[9] = (byte)'A';
            header[10] = (byte)'V';
            header[11] = (byte)'E';
            // format header
            header[12] = (byte)'f';
            header[13] = (byte)'m';
            header[14] = (byte)'t';
            header[15] = (byte)' ';
            header[16] = 16; // format header size
            header[17] = 0;
            header[18] = 0;
            header[19] = 0;
            header[20] = 1; // audio format (1 = PCM)
            header[21] = 0;
            header[22] = 2; // num channels 
            header[23] = 0;
            header[24] = 0x44; // sample rate (44100)
            header[25] = 0xac;
            header[26] = 0;
            header[27] = 0;
            header[28] = 0x10; // byte rate (44100*4)
            header[29] = 0xb1;
            header[30] = 0x02;
            header[31] = 0;
            header[32] = 4; // block align
            header[33] = 0;
            header[34] = 16; // bits per sample
            header[35] = 0;
            // data chunk header
            header[36] = (byte)'d';
            header[37] = (byte)'a';
            header[38] = (byte)'t';
            header[39] = (byte)'a';
            header[40] = 0xff; // data chunk size - fill in later
            header[41] = 0xff;
            header[42] = 0xff;
            header[43] = 0xff;

            m_file.Write(header, 0, header.Length);
        }

        public void WriteData(byte[] buffer, int indexSample, int numSamples)
        {
            m_file.Write(buffer, indexSample * 4, numSamples * 4);
            m_samplesWritten += numSamples;
        }

        public void Finish()
        {
            if (m_file != null)
            {
                // write lengths into WAV header
                byte[] buf = new byte[4];

                m_file.Seek(4, SeekOrigin.Begin);
                int totalChunkLen = m_samplesWritten * 4 + 36;
                buf[0] = (byte)((totalChunkLen & 0x000000ff));
                buf[1] = (byte)((totalChunkLen & 0x0000ff00) >> 8);
                buf[2] = (byte)((totalChunkLen & 0x00ff0000) >> 16);
                buf[3] = (byte)((totalChunkLen & 0xff000000) >> 24);
                m_file.Write(buf, 0, buf.Length);

                m_file.Seek(40, SeekOrigin.Begin);
                int dataChunkLen = m_samplesWritten * 4;
                buf[0] = (byte)((dataChunkLen & 0x000000ff));
                buf[1] = (byte)((dataChunkLen & 0x0000ff00) >> 8);
                buf[2] = (byte)((dataChunkLen & 0x00ff0000) >> 16);
                buf[3] = (byte)((dataChunkLen & 0xff000000) >> 24);
                m_file.Write(buf, 0, buf.Length);

                // close the file
                m_file.Dispose();
                m_file = null;
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

        ~WavFileWriter()
        {
            Dispose(false);
        }
    }
}
