using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.Log
{
    class Logger
    {
        private static Queue<string> m_log = new Queue<string>();
        private const int LOG_ENTRY_LIMIT = 200;

        private Logger()
        { }


        private static void Log(string m)
        {
            lock (m_log)
            {
                m_log.Enqueue(m);
                if (m_log.Count > LOG_ENTRY_LIMIT)
                    m_log.Dequeue();
            }
        }

        public static string LogAsString()
        {
            StringBuilder sb = new StringBuilder();
            lock (m_log)
            {
                foreach (string s in m_log)
                {
                    sb.AppendLine(s);
                }
            }
            return sb.ToString();
        }

        private static void Log(string level, string message)
        {
            Log(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} {1} {2}", DateTime.UtcNow, level, message));
        }

        public static void Info(string i)
        {
            Log("INFO", i);
        }

        public static void Info(string format, params object[] p)
        {
            Info(string.Format(format, p));
        }

        public static void Exception(Exception e, string m)
        {
            Log("EXCEPTION", string.Format("{0}: {1}", m, e));
        }

        public static void Exception(Exception e, string format, params object[] p)
        {
            Exception(e, string.Format(format, p));
        }
    }
}
