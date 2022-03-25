using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CddaX.Ripper
{
    class FileUtils
    {
        private FileUtils() { }

        public static FileStream CreateFileExclusiveNumbered(string directory, string basename, string extension)
        {
            string name = Path.Combine(directory, string.Format("{0}.{1}", basename, extension));
            try
            {
                return new FileStream(name, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            }
            catch (IOException e)
            {
                int hr = Marshal.GetHRForException(e);
                if (hr != unchecked((int)0x80070050)) // ERROR_FILE_EXISTS
                    throw;
            }

            for (int i = 1; ; ++i)
            {
                name = string.Format("{0}\\{1} ({2}).{3}", directory, basename, i, extension);
                try
                {
                    return new FileStream(name, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                }
                catch (IOException e)
                {
                    int hr = Marshal.GetHRForException(e);
                    if (hr != unchecked((int)0x80070050))
                        throw;
                }
            }
        }

        public static void ReplaceInvalidFilenameChars(StringBuilder n)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            for (int i = 0; i < n.Length; ++i)
            {
                if (n[i] < 32
                    || n[i] == '\"'
                    || n[i] == '<'
                    || n[i] == '>'
                    || n[i] == '|'
                    || n[i] == ':'
                    || n[i] == '*'
                    || n[i] == '?'
                    || n[i] == '\\'
                    || n[i] == '/')
                {
                    n[i] = '_';
                }
            }
        }

        public static string FilenameWithInvalidCharsReplaced(string n)
        {
            StringBuilder sb = new StringBuilder(n);
            ReplaceInvalidFilenameChars(sb);
            return sb.ToString();
        }

        public static string SanitizedFileName(string name)
        {
            if (name == null)
            {
                return string.Empty;
            }

            // calculate leading spaces
            int leadingSpaces = 0;
            while (leadingSpaces < name.Length && name[leadingSpaces] == ' ')
            {
                leadingSpaces++;
            }

            // calculate trailing dots or spaces
            int trailingSpaces = 0;
            while (trailingSpaces < name.Length - leadingSpaces
                && (name[name.Length - trailingSpaces - 1] == ' '
                    || name[name.Length - trailingSpaces - 1] == '.'))
            {
                trailingSpaces++;
            }

            if (leadingSpaces + trailingSpaces == name.Length)
            {
                return string.Empty;
            }

            // split in basename and extension
            StringBuilder basename = new StringBuilder();
            StringBuilder extension = new StringBuilder(); ;
            int lastDotPos = name.LastIndexOf('.', name.Length - trailingSpaces - 1, name.Length - leadingSpaces - trailingSpaces);
            if (lastDotPos != -1)
            {
                basename.Append(name, leadingSpaces, lastDotPos - leadingSpaces);
                extension.Append(name, lastDotPos + 1, name.Length - lastDotPos - trailingSpaces - 1);
            }
            else
            {
                basename.Append(name, leadingSpaces, name.Length - leadingSpaces - trailingSpaces);
            }

            // replace invalid characters
            ReplaceInvalidFilenameChars(basename);
            ReplaceInvalidFilenameChars(extension);

            // the basename must not be one of the reserved names
            string nameUpper = basename.ToString().ToUpperInvariant();
            if (nameUpper == "CON"
                || nameUpper == "PRN"
                || nameUpper == "AUX"
                || nameUpper == "NUL"
                || nameUpper == "COM0"
                || nameUpper == "COM1"
                || nameUpper == "COM2"
                || nameUpper == "COM3"
                || nameUpper == "COM4"
                || nameUpper == "COM5"
                || nameUpper == "COM6"
                || nameUpper == "COM7"
                || nameUpper == "COM8"
                || nameUpper == "COM9"
                || nameUpper == "LPT0"
                || nameUpper == "LPT1"
                || nameUpper == "LPT2"
                || nameUpper == "LPT3"
                || nameUpper == "LPT4"
                || nameUpper == "LPT5"
                || nameUpper == "LPT6"
                || nameUpper == "LPT7"
                || nameUpper == "LPT8"
                || nameUpper == "LPT9")
            {
                // if it is, fix it by appending something
                basename.Append('_');
            }

            // now plug basename and extension back together
            if (extension.Length > 0)
            {
                basename.Append('.');
                basename.Append(extension);
            }

            return basename.ToString();
        }
    }
}
