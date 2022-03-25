using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CddaX.CddaLib;

namespace CddaX.MusicBrainz
{
    class BrowserUrl
    {
        private BrowserUrl() { }

        public static string AttachCdStub(Toc toc)
        {
            int audioTrackCount = 0;
            int firstAudioTrack = 99;
            int leadOutOffset = 0;

            for (int i = Math.Max(1, toc.FirstTrackNo); i <= toc.LastTrackNo; ++i)
            {
                if (!toc.Tracks[i].IsAudioTrack)
                    continue;

                audioTrackCount += 1;

                if (i < firstAudioTrack)
                    firstAudioTrack = i;
                
                leadOutOffset = toc.Tracks[i].Start.Lba + toc.Tracks[i].Length.Lba + 150;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("https://musicbrainz.org/cdtoc/attach?id=");
            sb.Append(DiscId.FromToc(toc));
            sb.Append("&tracks=");
            sb.Append(audioTrackCount);
            sb.Append("&toc=");
            sb.Append(firstAudioTrack);
            sb.Append("+");
            sb.Append(audioTrackCount);
            sb.Append("+");
            sb.Append(leadOutOffset);

            for (int i = Math.Max(1, toc.FirstTrackNo); i <= toc.LastTrackNo; ++i)
            {
                if (!toc.Tracks[i].IsAudioTrack)
                    continue;

                sb.Append("+");
                sb.Append(toc.Tracks[i].Start.Lba + 150);
            }

            return sb.ToString();
        }

        public static string ShowCdToc(Toc toc)
        {
            return string.Format("https://musicbrainz.org/cdtoc/{0}", DiscId.FromToc(toc));
        }

        public static string MbPrivacyPolicy
        {
            get
            {
                return "https://metabrainz.org/privacy";
            }
        }

        public static string CoverArtArchivePrivacyPolicy
        {
            get
            {
                return "https://archive.org/about/terms.php";
            }
        }
    }
}
