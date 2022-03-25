using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CddaX.Util
{
    class CursorChanger : IDisposable
    {
        private Cursor m_oldCursor;

        public CursorChanger(Cursor newCursor)
        {
            m_oldCursor = Cursor.Current;
            Cursor.Current = newCursor;
        }

        public void Dispose()
        {
            if (m_oldCursor != null)
            {
                Cursor.Current = m_oldCursor;
                m_oldCursor = null;
            }
        }
    }
}
