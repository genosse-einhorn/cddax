using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    public struct BlockAddress
    {
        private int m_lba;

        private BlockAddress(int lba)
        {
            m_lba = lba;
        }

        public int Lba
        {
            get
            {
                return m_lba;
            }
            set
            {
                m_lba = value;
            }
        }

        public int Minute
        {
            get
            {
                return (m_lba + 150) / 75 / 60;
            }
        }

        public int Second
        {
            get
            {
                return ((m_lba + 150) / 75) % 60; 
            }
        }

        public int Frame
        {
            get
            {
                return (m_lba + 150) % 75;
            }
        }

        public static BlockAddress FromLba(int lba)
        {
            return new BlockAddress(lba);
        }

        public static BlockAddress FromMsf(int m, int s, int f)
        {
            return BlockAddress.FromLba(f + ((m * 60) + s) * 75 - 150);
        }

        public override string ToString()
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", Minute, Second, Frame);
        }

        public static bool operator <(BlockAddress a, BlockAddress b)
        {
            return a.Lba < b.Lba;
        }
        public static bool operator >(BlockAddress a, BlockAddress b)
        {
            return a.Lba > b.Lba;
        }
        public static bool operator <=(BlockAddress a, BlockAddress b)
        {
            return a.Lba <= b.Lba;
        }
        public static bool operator >=(BlockAddress a, BlockAddress b)
        {
            return a.Lba >= b.Lba;
        }
        public static bool operator ==(BlockAddress a, BlockAddress b)
        {
            return a.Lba == b.Lba;
        }
        public static bool operator !=(BlockAddress a, BlockAddress b)
        {
            return a.Lba != b.Lba;
        }
        public override int GetHashCode()
        {
            return m_lba;
        }
        public override bool Equals(object obj)
        {
            if (obj is BlockAddress)
            {
                return this == (BlockAddress)obj;
            }
            else
            {
                return false;
            }
        }
    }
}
