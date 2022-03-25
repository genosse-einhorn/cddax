using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CddaX.Util
{
    class IdleRunner
    {
        private EventHandler m_selfHandler;
        private Action m_callback;

        private IdleRunner(Action callback)
        {
            m_callback = callback;
            m_selfHandler = new EventHandler(HandleIdleEvent);
        }

        private void Hookup()
        {
            Application.Idle += m_selfHandler;
        }

        private void HandleIdleEvent(object sender, EventArgs e)
        {
            Application.Idle -= m_selfHandler;

            m_callback();
        }

        public static void DelayUntilIdle(Action callback)
        {
            var r = new IdleRunner(callback);
            r.Hookup();
        }
    }
}
