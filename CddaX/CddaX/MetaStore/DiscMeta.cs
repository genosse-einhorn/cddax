using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using CddaX.Log;

namespace CddaX.MetaStore
{
    public class DiscMeta
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Composer { get; set; }
        public string Year { get; set; }
        public string DiscNo { get; set; }
        public TrackMeta[] Tracks { get; private set; }
        public string Mcn { get; set; }
        public CddaLib.Toc Toc { get; private set; }
        public byte[] CoverBytes { get; set; }
        public Image CoverImage
        {
            get
            {
                if (CoverBytes != null)
                {
                    try
                    {
                        using (var s = new MemoryStream(CoverBytes))
                        {
                            return Image.FromStream(s);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Exception(e, "Loading Cover Image");
                    }
                }

                return null;
            }
        }

        public DiscMeta(CddaLib.Toc toc)
        {
            Toc = toc;
            
            int numAudioTracks = 0;
            for (int i = toc.FirstTrackNo; i <= toc.LastTrackNo; ++i)
            {
                if (toc.Tracks[i].IsAudioTrack)
                {
                    numAudioTracks++;
                }
            }

            Tracks = new TrackMeta[numAudioTracks];

            int k = 0;
            for (int i = toc.FirstTrackNo; i <= toc.LastTrackNo; ++i)
            {
                if (toc.Tracks[i].IsAudioTrack)
                {
                    Tracks[k++] = new TrackMeta(toc.Tracks[i]);
                }
            }
        }

        public void MergeCdText(CddaLib.CdTextData cdtext)
        {
            if (!string.IsNullOrEmpty(cdtext.DiscData.Title))
            {
                Title = cdtext.DiscData.Title;
            }

            if (!string.IsNullOrEmpty(cdtext.DiscData.Artist))
            {
                Artist = cdtext.DiscData.Artist;
            }

            if (!string.IsNullOrEmpty(cdtext.DiscData.Composer))
            {
                Composer = cdtext.DiscData.Composer;
            }

            foreach (TrackMeta t in Tracks)
            {
                if (t.TrackNo > 0 && t.TrackNo < 100)
                {
                    t.MergeCdText(cdtext.TrackData[t.TrackNo]);
                }
            }
        }

        public void MergeMusicBrainz(MusicBrainz.Release release)
        {
            if (!string.IsNullOrEmpty(release.ArtistsStr))
                Artist = release.ArtistsStr;

            if (!string.IsNullOrEmpty(release.Title))
                Title = release.Title;

            if (!string.IsNullOrEmpty(release.Date))
                Year = release.Date.Split('-')[0];

            MusicBrainz.Medium m = release.MediumForToc(Toc);
            if (m != null)
            {
                if (m.Position > 0)
                    DiscNo = m.Position.ToString();

                foreach (MusicBrainz.Track t in m.Tracks)
                {
                    foreach (TrackMeta tm in Tracks)
                    {
                        if (tm.TrackNo == t.Number)
                        {
                            tm.MergeMusicBrainz(t);
                        }
                    }
                }
            }
        }
    }
}
