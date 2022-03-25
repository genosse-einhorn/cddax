using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    public class CdTextData
    {
        public CdTextTrackData[] TrackData { get; private set; }

        public CdTextTrackData DiscData
        {
            get
            {
                return TrackData[0];
            }
        }

        public CdTextData()
        {
            TrackData = new CdTextTrackData[100];
        }
    }
}
