using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.Ripper
{
    public struct FileWriterMeta
    {
        public int TrackNo;
        public int TotalTrackCount;
        public string Artist;
        public string Title;
        public string Composer;
        public string Isrc;
        public string AlbumTitle;
        public string Year;
        public string DiscNo;
        public Mp3Quality Mp3Quality;
    }
}
