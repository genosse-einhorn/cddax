using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.MetaStore
{
    public class TrackMeta
    {
        public int TrackNo { get; private set; }
        public CddaLib.BlockAddress Start { get; private set; }
        public CddaLib.BlockDelta Length { get; private set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Composer { get; set; }
        public string Isrc { get; set; }
        public bool Selected { get; set; }

        public TrackMeta(CddaLib.TocTrack tocTrack)
        {
            TrackNo = tocTrack.Index;
            Start = tocTrack.Start;
            Length = tocTrack.Length;
            Title = "";
            Artist = "";
            Composer = "";
            Isrc = "";
            Selected = true;
        }

        public void MergeCdText(CddaLib.CdTextTrackData cdTextData)
        {
            if (!string.IsNullOrEmpty(cdTextData.Title))
            {
                this.Title = cdTextData.Title;
            }

            if (!string.IsNullOrEmpty(cdTextData.Artist))
            {
                this.Artist = cdTextData.Artist;
            }

            if (!string.IsNullOrEmpty(cdTextData.Composer))
            {
                this.Composer = cdTextData.Composer;
            }

            if (!string.IsNullOrEmpty(cdTextData.Isrc) && string.IsNullOrEmpty(this.Isrc))
            {
                this.Isrc = cdTextData.Isrc;
            }
        }

        public void MergeMusicBrainz(MusicBrainz.Track mbTrack)
        {
            if (!string.IsNullOrEmpty(mbTrack.Title))
            {
                this.Title = mbTrack.Title;
            }

            if (mbTrack.Artists != null && mbTrack.Artists.Length > 0)
            {
                this.Artist = string.Join(", ", mbTrack.Artists);
            }

            if (mbTrack.Composers != null && mbTrack.Composers.Length > 0)
            {
                this.Composer = string.Join(", ", mbTrack.Composers);
            }

            if (mbTrack.Isrcs != null && mbTrack.Isrcs.Length > 0 && string.IsNullOrEmpty(this.Isrc))
            {
                this.Isrc = mbTrack.Isrcs[0];
            }
        }
    }
}
