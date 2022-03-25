using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using CddaX.Util;

namespace CddaX.CddaLib
{
    public class LinuxScsiHandle : IScsiHandle
    {
        private string m_drive;
        private int m_fd = -1;

        public LinuxScsiHandle(string drive)
        {
            m_drive = drive;
            
            // we will open it later in ExecScsiCommand
        }

        public void ExecScsiCommand(byte[] cdb, byte[] resultbuf)
        {
            if (m_fd == -1)
            {
                m_fd = open(m_drive, O_RDONLY | O_NONBLOCK);
                if (m_fd == -1)
                {
                    throw new Win32Exception();
                }
            }

            byte[] sensebuf = new byte[18];
            int status = 0;

            unsafe
            {
                fixed (byte *cdb_ptr = cdb)
                fixed (byte* result_ptr = resultbuf)
                fixed (byte *sense_ptr = sensebuf)
                {
                    sg_io_hdr* hdr = stackalloc sg_io_hdr[1];
                    for (int i = 0; i < sizeof(sg_io_hdr); ++i)
                        ((byte*)hdr)[i] = 0;

                    hdr->interface_id = 'S';
                    hdr->cmdp = (IntPtr)cdb_ptr;
                    hdr->cmd_len = (byte)cdb.Length;
                    hdr->dxferp = (IntPtr)result_ptr;
                    hdr->dxfer_len = (uint)resultbuf.Length;
                    hdr->dxfer_direction = SG_DXFER_FROM_DEV;
                    hdr->sbp = (IntPtr)sense_ptr;
                    hdr->mx_sb_len = (byte)sensebuf.Length;
                    hdr->timeout = 10000;

                    int ret = ioctl(m_fd, (UIntPtr)SG_IO, (IntPtr)hdr);
                    if (ret < 0)
                    {
                        throw new Win32Exception();
                    }

                    status = hdr->status;
                }
            }

            if (status != 0)
            {
                throw new ScsiException(sensebuf);
            }
        }

        public void EjectMedia()
        {
            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = "eject";
            si.Arguments = ArgumentsBuilder.QuotedArg(m_drive);
            si.UseShellExecute = false;
            Process.Start(si);
        }

        public void LoadMedia()
        {
            ArgumentsBuilder ab = new ArgumentsBuilder();
            ab.Add("-t");
            ab.Add(m_drive);

            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = "eject";
            si.Arguments = ab.ToString();
            si.UseShellExecute = false;
            Process.Start(si);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (m_fd != -1)
            {
                close(m_fd);
                m_fd = -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LinuxScsiHandle()
        {
            Dispose(false);
        }

        public static string[] CdDriveList()
        {
            List<string> l = new List<string>();

            if (File.Exists("/dev/cdrom"))
            {
                l.Add("/dev/cdrom");
            }

            // FIXME! only truly crazy people would have more than 20 CD drives
            for (int i = 0; i < 20; ++i)
            {
                string d = "/dev/sr" + i;
                if (File.Exists(d))
                {
                    l.Add(d);
                }
            }

            return l.ToArray();
        }

        [DllImport("libc", SetLastError = true)]
        private static extern int close(int fd);

        [DllImport("libc", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern int open(string filename, int flags);

        [DllImport("libc", SetLastError = true)]
        private static extern int ioctl(int fd, UIntPtr request, IntPtr data);

        private const int O_RDONLY = 0;
        private const int O_NONBLOCK = 0x800;

        [StructLayout(LayoutKind.Sequential)]
        private struct sg_io_hdr
        {
          public int interface_id;           /* [i] 'S' for SCSI generic (required) */
          public int dxfer_direction;        /* [i] data transfer direction  */
          public byte cmd_len;               /* [i] SCSI command length ( <= 16 bytes) */
          public byte mx_sb_len;             /* [i] max length to write to sbp */
          public ushort iovec_count;         /* [i] 0 implies no scatter gather */
          public uint dxfer_len;             /* [i] byte count of data transfer */
          public IntPtr dxferp;              /* [i], [*io] points to data transfer memory
				         or scatter gather list */
          public IntPtr cmdp;                /* [i], [*i] points to command to perform */
          public IntPtr sbp;                 /* [i], [*o] points to sense_buffer memory */
          public uint timeout;               /* [i] MAX_UINT->no timeout (unit: millisec) */
          public uint flags;                 /* [i] 0 -> default, see SG_FLAG... */
          public int pack_id;                /* [i->o] unused internally (normally) */
          public IntPtr usr_ptr;             /* [i->o] unused internally */
          public byte status;                /* [o] scsi status */
          public byte masked_status;         /* [o] shifted, masked scsi status */
          public byte msg_status;            /* [o] messaging level data (optional) */
          public byte sb_len_wr;             /* [o] byte count actually written to sbp */
          public ushort host_status;         /* [o] errors from host adapter */
          public ushort driver_status;       /* [o] errors from software driver */
          public int resid;                  /* [o] dxfer_len - actual_transferred */
          public uint duration;              /* [o] time taken by cmd (unit: millisec) */
          public uint info;                  /* [o] auxiliary information */
        }

        private const int SG_DXFER_FROM_DEV = -3;
        private const int SG_IO = 0x2285;
    }
}
