using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    public class Toc
    {
        public int FirstTrackNo = 0;
        public int LastTrackNo = 0;
        public readonly TocTrack[] Tracks = new TocTrack[100];

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = FirstTrackNo; i <= LastTrackNo; ++i)
            {
                sb.Append(i);
                sb.Append('\t');
                sb.Append(Tracks[i].Start);
                sb.Append('\t');
                sb.Append(Tracks[i].Length);
                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        public static bool operator==(Toc a, Toc b)
        {
            if ((object)a == null && (object)b == null)
                return true;

            if ((object)a == null || (object)b == null)
                return false;
            
            if (a.FirstTrackNo != b.FirstTrackNo)
                return false;

            if (a.LastTrackNo != b.LastTrackNo)
                return false;

            for (int i = a.FirstTrackNo; i <= a.LastTrackNo; ++i)
            {
                if (a.Tracks[i] != b.Tracks[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(Toc a, Toc b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Toc)
            {
                return this == (Toc)obj;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash * 16777619) ^ FirstTrackNo;
                hash = (hash * 16777619) ^ LastTrackNo;
                for (int i = FirstTrackNo; i <= LastTrackNo; ++i)
                {
                    hash = (hash * 16777619) ^ Tracks[i].GetHashCode();
                }
                return hash;
            }
        }
    }
}
