using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CddaX.CddaLib;
using System.Security.Cryptography;

namespace CddaX.MusicBrainz
{
    class DiscId
    {
        private DiscId() { }

        public static string FromToc(Toc toc)
        {
            int firstAudioTrack = 99;
            int lastAudioTrack = 0;
            int leadOutOffset = 0;

            for (int i = toc.FirstTrackNo; i <= toc.LastTrackNo; ++i)
            {
                if (!toc.Tracks[i].IsAudioTrack)
                    continue;
                if (i < 1)
                    continue; // ignore synthesized hidden track

                if (i < firstAudioTrack)
                    firstAudioTrack = i;
                if (i > lastAudioTrack)
                {
                    lastAudioTrack = i;
                    leadOutOffset = toc.Tracks[i].Start.Lba + toc.Tracks[i].Length.Lba + 150;
                }
            }

            StringBuilder sbTocList = new StringBuilder();
            sbTocList.AppendFormat("{0:X2}", firstAudioTrack);
            sbTocList.AppendFormat("{0:X2}", lastAudioTrack);
            sbTocList.AppendFormat("{0:X8}", leadOutOffset);

            for (int i = 1; i <= 99; ++i)
            {
                if (!toc.Tracks[i].IsAudioTrack || i < toc.FirstTrackNo || i > toc.LastTrackNo)
                {
                    sbTocList.Append("00000000");
                }
                else
                {
                    int offset = toc.Tracks[i].Start.Lba + 150;
                    sbTocList.AppendFormat("{0:X8}", offset);
                }
            }

            string tocList = sbTocList.ToString();
            byte[] sha1 = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(tocList));

            return MbBase64(sha1);
        }

        private static string MbBase64(byte[] buffer)
        {
            StringBuilder ret = new StringBuilder(buffer.Length + (buffer.Length + 2) / 3);
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789._";

            for (int i = 0; i < buffer.Length; i += 3)
            {
                byte b0 = buffer[i];
                byte b1 = (i + 1) < buffer.Length ? buffer[i + 1] : (byte)0;
                byte b2 = (i + 2) < buffer.Length ? buffer[i + 2] : (byte)0;

                int c0 = (b0 & 0xfc) >> 2;
                int c1 = ((b0 & 0x3) << 4) | ((b1 & 0xf0) >> 4);
                int c2 = ((b1 & 0xf) << 2) | ((b2 & 0xc0) >> 6);
                int c3 = b2 & 0x3f;

                ret.Append(alphabet[c0]);
                ret.Append(alphabet[c1]);
                ret.Append((i + 1) < buffer.Length ? alphabet[c2] : '-');
                ret.Append((i + 2) < buffer.Length ? alphabet[c3] : '-');
            }

            return ret.ToString();
        }
    }
}
