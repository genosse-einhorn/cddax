using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CddaX.Ripper
{
    interface IFileWriter : IDisposable
    {
        string FilenameExtension { get; }

        void Begin(FileStream stream, FileWriterMeta meta);
        void WriteData(byte[] buffer, int indexSample, int numSamples);
        void Finish();
    }
}
