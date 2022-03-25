using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using CddaX.Util;

namespace CddaX.CddaLib
{
    static class ScsiHandle
    {
        public static IScsiHandle Create(string drive)
        {
            if (OSHelper.IsWindows)
            {
                return new WinNtScsiHandle("\\\\.\\" + drive);
            }
            else
            {
                return new LinuxScsiHandle(drive);
            }
        }

        public static string[] CdDriveList()
        {
            if (OSHelper.IsWindows)
            {
                return WinNtScsiHandle.CdDriveList();
            }
            else
            {
                return LinuxScsiHandle.CdDriveList();
            }
        }
    }
}
