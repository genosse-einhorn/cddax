using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.CddaLib
{
    public struct BlockDelta
    {
        private int m_lba;

        private BlockDelta(int lba)
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
                return m_lba / 75 / 60;
            }
        }

        public int Second
        {
            get
            {
                return (m_lba / 75) % 60; 
            }
        }

        public int Frame
        {
            get
            {
                return m_lba % 75;
            }
        }

        public static BlockDelta FromLba(int lba)
        {
            return new BlockDelta(lba);
        }

        public static BlockDelta FromMsf(int m, int s, int f)
        {
            return BlockDelta.FromLba(f + ((m * 60) + s) * 75);
        }

        public static BlockDelta FromDifference(BlockAddress start, BlockAddress end)
        {
            return BlockDelta.FromLba(end.Lba - start.Lba);
        }

        public override string ToString()
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", Minute, Second, Frame);
        }
    }
}
