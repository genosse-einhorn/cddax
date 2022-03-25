using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using CddaX.Util;
using System.ComponentModel;

namespace CddaX.CddaLib
{
    class WinNtScsiHandle : IScsiHandle
    {
        IntPtr m_handle = new IntPtr(INVALID_HANDLE_VALUE);

        public WinNtScsiHandle(string device)
        {
            m_handle = CreateFile(device,
                                  GENERIC_READ | GENERIC_WRITE,
                                  FILE_SHARE_READ | FILE_SHARE_WRITE,
                                  IntPtr.Zero,
                                  OPEN_EXISTING,
                                  FILE_ATTRIBUTE_NORMAL,
                                  IntPtr.Zero);
            if (m_handle.ToInt32() == INVALID_HANDLE_VALUE || m_handle.ToInt32() == 0) 
            {
                throw new Win32Exception();
            }
        }

        public unsafe void ExecScsiCommand(byte[] cdb, byte[] resultbuf)
        {
            fixed (byte* resultbuf_ptr = resultbuf)
            {
                SCSI_PASS_THROUGH_DIRECT_WITH_SENSE* sptd = stackalloc SCSI_PASS_THROUGH_DIRECT_WITH_SENSE[1];
                for (int i = 0; i < sizeof(SCSI_PASS_THROUGH_DIRECT_WITH_SENSE); ++i)
                    ((byte*)sptd)[i] = 0;
                sptd->Sptd.Length = (ushort)(sizeof(SCSI_PASS_THROUGH_DIRECT));
                sptd->Sptd.CdbLength = (byte)cdb.Length;
                sptd->Sptd.SenseInfoLength = 18;
                sptd->Sptd.DataIn = 1;
                sptd->Sptd.DataTransferLength = (uint)resultbuf.Length;
                sptd->Sptd.TimeOutValue = 30;
                sptd->Sptd.DataBuffer = (IntPtr)resultbuf_ptr;
                sptd->Sptd.SenseInfoOffset = (uint)(&sptd->SenseInfo[0] - (byte*)sptd);
                for (int i = 0; i < cdb.Length; ++i)
                    sptd->Sptd.Cdb[i] = cdb[i];

                uint dwBytesRet = 0;
                bool r = DeviceIoControl(m_handle,
                                         IOCTL_SCSI_PASS_THROUGH_DIRECT,
                                         (IntPtr)sptd,
                                         (uint)sizeof(SCSI_PASS_THROUGH_DIRECT_WITH_SENSE),
                                         (IntPtr)sptd,
                                         (uint)sizeof(SCSI_PASS_THROUGH_DIRECT_WITH_SENSE),
                                         out dwBytesRet,
                                         IntPtr.Zero);
                if (!r)
                {
                    throw new Win32Exception();
                }

                if (sptd->Sptd.ScsiStatus != 0)
                {
                    byte[] senseinfo = new byte[18];
                    for (int i = 0; i < 18; ++i)
                        senseinfo[i] = sptd->SenseInfo[i];
                    throw new ScsiException(senseinfo);
                }
            }
        }

        public void EjectMedia()
        {
            uint dwDummy = 0;

            bool r = DeviceIoControl(m_handle, 
                                     FSCTL_LOCK_VOLUME, 
                                     IntPtr.Zero, 0, 
                                     IntPtr.Zero, 0, 
                                     out dwDummy, 
                                     IntPtr.Zero);
            if (!r)
            {
                throw new Win32Exception();
            }

            r = DeviceIoControl(m_handle, 
                                FSCTL_DISMOUNT_VOLUME, 
                                IntPtr.Zero, 0, 
                                IntPtr.Zero, 0, 
                                out dwDummy, 
                                IntPtr.Zero);
            if (!r)
            {
                throw new Win32Exception();
            }

            unsafe
            {
                PREVENT_MEDIA_REMOVAL* pmv = stackalloc PREVENT_MEDIA_REMOVAL[1];
                pmv->PreventMediaRemoval = 0;

                r = DeviceIoControl(m_handle,
                                    IOCTL_STORAGE_MEDIA_REMOVAL,
                                    (IntPtr)pmv, 
                                    (uint)sizeof(PREVENT_MEDIA_REMOVAL),
                                    IntPtr.Zero, 0,
                                    out dwDummy,
                                    IntPtr.Zero);
            }
            if (!r)
            {
                throw new Win32Exception();
            }

            r = DeviceIoControl(m_handle,
                                IOCTL_STORAGE_EJECT_MEDIA,
                                IntPtr.Zero, 0,
                                IntPtr.Zero, 0,
                                out dwDummy,
                                IntPtr.Zero);
            if (!r)
            {
                throw new Win32Exception();
            }
        }

        public void LoadMedia()
        {
            uint dwDummy = 0;
            bool r = DeviceIoControl(m_handle,
                                     IOCTL_STORAGE_LOAD_MEDIA,
                                     IntPtr.Zero, 0,
                                     IntPtr.Zero, 0,
                                     out dwDummy,
                                     IntPtr.Zero);
            if (!r)
            {
                throw new Win32Exception();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            CloseHandle(m_handle);
            m_handle = new IntPtr(INVALID_HANDLE_VALUE);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~WinNtScsiHandle()
        {
            Dispose(false);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        const int INVALID_HANDLE_VALUE = -1;
        const uint GENERIC_READ = 0x80000000;
        const uint GENERIC_WRITE = 0x40000000;
        const uint OPEN_EXISTING = 3;
        const uint FILE_SHARE_READ = 1;
        const uint FILE_SHARE_WRITE = 2;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFile(string lpFileName, 
                                        uint   dwDesiredAccess,
                                        uint   dwShareMode, 
                                        IntPtr lpSecurityAttributes, 
                                        uint   dwCreationDisposition,
                                        uint   dwFlagsAndAttributes, 
                                        IntPtr hTemplateFile);

        const uint IOCTL_SCSI_PASS_THROUGH_DIRECT = 0x4D014;
        const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2d4808;
        const uint IOCTL_STORAGE_LOAD_MEDIA = 0x2d480c;
        const uint FSCTL_LOCK_VOLUME = 0x90018;
        const uint FSCTL_DISMOUNT_VOLUME = 0x90020;
        const uint IOCTL_STORAGE_MEDIA_REMOVAL = 0x2d4804;

        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern bool DeviceIoControl(IntPtr       hDevice,
                                           uint         dwIoControlCode,
                                           IntPtr       lpInBuffer,
                                           uint         nInBufferSize,
                                           IntPtr       lpOutBuffer,
                                           uint         nOutBufferSize,
                                           out uint     lpBytesReturned,
                                           IntPtr       lpOverlapped);

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct SCSI_PASS_THROUGH_DIRECT
        {
            public ushort Length;
            public byte ScsiStatus;
            public byte PathId;
            public byte TargetId;
            public byte Lun;
            public byte CdbLength;
            public byte SenseInfoLength;
            public byte DataIn;
            public uint DataTransferLength;
            public uint TimeOutValue;
            public IntPtr DataBuffer;
            public uint SenseInfoOffset;
            public fixed byte Cdb[16];
        }

        [StructLayout(LayoutKind.Sequential)]
        private unsafe struct SCSI_PASS_THROUGH_DIRECT_WITH_SENSE
        {
            public SCSI_PASS_THROUGH_DIRECT Sptd;
            public fixed byte SenseInfo[18];
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PREVENT_MEDIA_REMOVAL
        {
            public byte PreventMediaRemoval;
        }

        public static string[] CdDriveList()
        {
            List<string> r = new List<string>();

            for (char c = 'A'; c <= 'Z'; ++c)
            {
                if (GetDriveType(c + ":\\") == DriveType.CDROM)
                {
                    r.Add(c + ":");
                }
            }

            return r.ToArray();
        }

        [DllImport("kernel32.dll")]
        private static extern DriveType GetDriveType(string lpRootPathName);

        private enum DriveType : uint
        {
            Unknown = 0,    //DRIVE_UNKNOWN
            Error = 1,      //DRIVE_NO_ROOT_DIR
            Removable = 2,  //DRIVE_REMOVABLE
            Fixed = 3,      //DRIVE_FIXED
            Remote = 4,     //DRIVE_REMOTE
            CDROM = 5,      //DRIVE_CDROM
            RAMDisk = 6     //DRIVE_RAMDISK
        }
    }
}
