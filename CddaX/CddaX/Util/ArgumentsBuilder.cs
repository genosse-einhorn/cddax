using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CddaX.Util
{
    class ArgumentsBuilder
    {
        private StringBuilder m_sb = new StringBuilder();

        public void Add(string arg)
        {
            if (RequiresQuotes(arg))
                AddRaw(QuotedArg(arg));
            else
                AddRaw(arg);
        }

        public void Add(string format, params object[] args)
        {
            Add(string.Format(format, args));
        }

        public void AddRaw(string arg)
        {
            if (m_sb.Length > 0)
                m_sb.Append(' ');

            m_sb.Append(arg);
        }

        public void AddRaw(string format, params object[] args)
        {
            AddRaw(string.Format(format, args));
        }

        private static bool RequiresQuotes(string arg)
        {
            for (int i = 0; i < arg.Length; ++i)
            {
                if (arg[i] == ' ' || arg[i] == '\t' || arg[i] == '\v' || arg[i] == '"')
                    return true;
            }

            return false;
        }

        public static string QuotedArg(string arg)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append('"');

            int numBackslashes = 0;
            for (int i = 0; i < arg.Length; ++i)
            {
                if (arg[i] == '\\')
                {
                    numBackslashes += 1;
                }
                else if (arg[i] == '"')
                {
                    sb.Append('\\', numBackslashes * 2 + 1);
                    sb.Append('"');
                    numBackslashes = 0;
                }
                else
                {
                    sb.Append('\\', numBackslashes);
                    sb.Append(arg[i]);
                    numBackslashes = 0;
                }
            }

            sb.Append('\\', numBackslashes * 2);
            sb.Append('"');
            return sb.ToString();
        }

        public override string ToString()
        {
            return m_sb.ToString();
        }
    }
}
