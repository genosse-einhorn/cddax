using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    public interface IScsiHandle : IDisposable
    {
        void ExecScsiCommand(byte[] cdb, byte[] resultbuf);
        void EjectMedia();
        void LoadMedia();
    }
}
