using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfCommanderLib
{
    public class HlsDos
    {
        #region ***** declarations
        private AdfFile _adfFile;
        #endregion

        #region ***** constructor / terminator / disposer

        public HlsDos(AdfFile adfFile)
        {
            if (adfFile == null) { throw new ArgumentException("No adf file object supplied."); }
            if (!adfFile.FileLoaded) { throw new ArgumentException("No adf file loaded."); }

            _adfFile = adfFile;
        }

        #region dispose pattern (http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P)
        // some fields that require cleanup
        private bool _disposed = false; // to detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // clean up managed handles here
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
        #endregion

        #region ***** private functions
        private string _getDirectory()
        {
            /* The directory is contained in the first 10 sectors
             * starting at byte 42 with an entry siz eof 46 bytes */
            var bytDir = _adfFile.GetSectors(0, 10);
            var intStartOfDirectory = 42;
            var intEntrySize = 46;
            StringBuilder sbHex = new StringBuilder();
            StringBuilder sbText = new StringBuilder();

            //read directory
            int idx = intStartOfDirectory; //first directory entry
            while (bytDir[idx] != 0)
            {
                //get entry
                byte[] bytEntry = new byte[intEntrySize];
                Buffer.BlockCopy(bytDir, idx, bytEntry, 0, intEntrySize);

                //create hex representation
                for (int i = 0; i <= bytEntry.GetUpperBound(0); i++)
                {
                    if (i == 44) { }
                    sbHex.Append(bytEntry[i].ToString("X2"));
                    sbHex.Append(" ");
                }
                sbHex.Append(Environment.NewLine);

                //create text representation
                //Console.WriteLine(System.Text.Encoding.ASCII.GetString(bytEntry));
                sbText.Append(Encoding.ASCII.GetString(bytEntry).Substring(0, Encoding.ASCII.GetString(bytEntry).IndexOf((char)0)));
                sbText.Append(Environment.NewLine);

                //output to trace
                Trace.WriteLine(sbHex.ToString());
                Trace.WriteLine(sbText.ToString());

                //goto next entry
                idx += intEntrySize;
            }

            return sbHex.ToString();
        }

        private uint _swapEndianness(uint x)
        {
            return ((x & 0x000000ff) << 24) +  // First byte
                   ((x & 0x0000ff00) << 8) +   // Second byte
                   ((x & 0x00ff0000) >> 8) +   // Third byte
                   ((x & 0xff000000) >> 24);   // Fourth byte
        }
        #endregion

        #region ***** methods
        public string GetDirectory()
        {
            return _getDirectory();
        }
        #endregion
    }
}
