using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfCommanderLib
{
    public class HlsDos
    {
        #region ***** declarations
        #endregion

        #region ***** constructor / terminator / disposer

        public HlsDos(AdfFile adfFile)
        {
            if (adfFile == null) { throw new ArgumentException("No adf file object supplied."); }
            if (!adfFile.FileLoaded) { throw new ArgumentException("No adf file loaded."); }

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
        #endregion
    }
}
