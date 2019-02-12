using System;
using System.IO;

namespace AdfCommanderLib
{
    public class AmigaDos
    {
        #region ***** declarations
        private AdfFile _adfFile;
        #endregion

        #region ***** constructor / terminator / disposer

        public AmigaDos(AdfFile adfFile)
        {
            if (adfFile == null) { throw new ArgumentException("No adf file object supplied."); }
            if (!adfFile.FileLoaded) { throw new ArgumentException("No adf file loaded."); }

            _adfFile = adfFile;

            _analyzeBootBlock();
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
                    _adfFile = null;
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
        private void _analyzeBootBlock()
        {
            byte[] bytBootBlock = _adfFile.GetSectors(0, 2);
            using (var br = new BinaryReader(new FileStream(_adfFile.FileName, FileMode.Open, FileAccess.Read)))
            {
                BootBlock bb = new BootBlock(br, _adfFile.Blocksize * 2);
            }
        }
        #endregion

        #region ***** methods
        #endregion

        #region ***** properties
        public DiskType FileDiskType { get { return _adfFile.FileDiskType; } }

        public AmigaDosType Type { get; private set; } = AmigaDosType.Unknown;
        #endregion
    }
}
