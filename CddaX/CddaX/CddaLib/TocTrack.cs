using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    public struct TocTrack
    {
        public int Index; // 0-99

        public BlockAddress Start;
        public BlockAddress End;

        public byte Session;
        public byte Adr;
        public byte Control;

        public bool IsAudioTrack
        {
            get 
            {
                return (Control & 4) == 0;
            }
        }

        public BlockDelta Length
        {
            get
            {
                return BlockDelta.FromDifference(Start, End);
            }
        }

        public static bool operator==(TocTrack a, TocTrack b)
        {
            return a.Index == b.Index
                && a.Start == b.Start
                && a.End == b.End
                && a.Session == b.Session
                && a.Adr == b.Adr
                && a.Control == b.Control;
        }

        public static bool operator!=(TocTrack a, TocTrack b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is TocTrack)
            {
                return this == (TocTrack)obj;
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
                hash = (hash * 16777619) ^ Index;
                hash = (hash * 16777619) ^ Start.GetHashCode();
                hash = (hash * 16777619) ^ End.GetHashCode();
                hash = (hash * 16777619) ^ Session;
                hash = (hash * 16777619) ^ Adr;
                hash = (hash * 16777619) ^ Control;
                return hash;
            }
        }
    }
}
