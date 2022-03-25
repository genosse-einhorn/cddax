using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CddaX.Util
{
    public class MediaChangeNotificationEventArgs : EventArgs
    {
        public string Drive { get; private set; }

        public MediaChangeNotificationEventArgs(string drive)
        {
            this.Drive = drive;
        }
    }

    public class MediaChangeNotificationHelper : Component
    {
        [Category("Media Change Events")]
        public event EventHandler<MediaChangeNotificationEventArgs> MediumInserted;

        [Category("Media Change Events")]
        public event EventHandler<MediaChangeNotificationEventArgs> MediumRemoved;

        public MediaChangeNotificationHelper()
        {
        }

        protected virtual void OnMediumInserted(MediaChangeNotificationEventArgs args)
        {
            if (MediumInserted != null)
            {
                MediumInserted(this, args);
            }
        }

        private void OnMediumInsertedByUnitmask(int unitmask)
        {
            char[] drive = new char[] { '?', ':' };

            for (int i = 0; i < 26; ++i)
            {
                if ((unitmask & (1 << i)) != 0)
                {
                    drive[0] = (char)('A' + i);
                    OnMediumInserted(new MediaChangeNotificationEventArgs(new string(drive)));
                }
            }
        }

        protected virtual void OnMediumRemoved(MediaChangeNotificationEventArgs args)
        {
            if (MediumRemoved != null)
            {
                MediumRemoved(this, args);
            }
        }

        private void OnMediumRemovedByUnitmask(int unitmask)
        {
            char[] drive = new char[] { '?', ':' };

            for (int i = 0; i < 26; ++i)
            {
                if ((unitmask & (1 << i)) != 0)
                {
                    drive[0] = (char)('A' + i);
                    OnMediumRemoved(new MediaChangeNotificationEventArgs(new string(drive)));
                }
            }
        }

        public void ProcessMessage(ref Message m)
        {
            if (m.Msg != WM_DEVICECHANGE)
                return;
            
            if (m.WParam == (IntPtr)DBT_DEVICEARRIVAL)
            {
                int devicetype = Marshal.ReadInt32(m.LParam, 4);
                if (devicetype == DBT_DEVTYP_VOLUME)
                {
                    short flags = Marshal.ReadInt16(m.LParam, 16);
                    int unitmask = Marshal.ReadInt32(m.LParam, 12);

                    if ((flags & DBTF_MEDIA) != 0)
                    {
                        OnMediumInsertedByUnitmask(unitmask);
                    }
                }
            }
            else if (m.WParam == (IntPtr)DBT_DEVICEREMOVECOMPLETE)
            {
                int devicetype = Marshal.ReadInt32(m.LParam, 4);
                if (devicetype == DBT_DEVTYP_VOLUME)
                {
                    short flags = Marshal.ReadInt16(m.LParam, 16);
                    int unitmask = Marshal.ReadInt32(m.LParam, 12);

                    if ((flags & DBTF_MEDIA) != 0)
                    {
                        OnMediumRemovedByUnitmask(unitmask);
                    }
                }
            }
        }

        private const uint WM_DEVICECHANGE = 0x0219;

        private const uint DBT_DEVICEARRIVAL = 0x8000;
        private const uint DBT_DEVICEREMOVECOMPLETE = 0x8004;
        private const uint DBT_DEVTYP_VOLUME = 0x00000002;
        private const uint DBTF_MEDIA = 0x0001;
    }
}
