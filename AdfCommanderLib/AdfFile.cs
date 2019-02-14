using System;
using System.Diagnostics;
using System.IO;

namespace AdfCommanderLib
{
    public class AdfFile
    {
        #region ***** declarations
        private byte[] _bytMain;
        private byte[] _bytCompare;
        #endregion

        #region ***** constructor / terminator / disposer
        public AdfFile()
        {

        }
        public AdfFile(string fileName)
            : this()
        {
            FileName = fileName;
            _initAdfFile();
        }
        #endregion

        #region ***** private functions
        private void _initAdfFile()
        {
            //prerequisites
            if (FileLoaded) { FileLoaded = false; }
            if (!File.Exists(FileName)) { throw new FileNotFoundException(FileName); }

            try
            {
                using (var fsSource = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    //File too long?
                    if (fsSource.Length > int.MaxValue) { throw new FileLoadException("File is too big."); }

                    //vars
                    FileLength = (int)fsSource.Length;

                    //verify file length
                    if (FileLength % Blocksize != 0)
                    {
                        throw new InvalidDataException(string.Format("Invalid block size to file size ratio (bs:{0}, fs:{1}).", Blocksize, FileLength));
                    }
#if DEBUG
                    Debug.WriteLine(string.Format("Filename (bytes): {0} ({1})", FileName, FileLength));
#endif
                    //init arrays
                    if (_bytMain != null) { _bytMain = null; }
                    if (_bytCompare != null) { _bytCompare = null; }
                    _bytMain = new byte[FileLength];
                    _bytCompare = new byte[FileLength];

                    //check for known disk sizes
                    if (FileLength == 1760 * Blocksize) { FileDiskType = DiskType.DoubleDensity_DD; }
                    if (FileLength == 3520 * Blocksize) { FileDiskType = DiskType.HighDensity_HD; }

                    //read file to buffers
                    fsSource.Read(_bytMain, 0, FileLength);
                    fsSource.Seek(0, SeekOrigin.Begin);
                    fsSource.Read(_bytCompare, 0, FileLength);
                    fsSource.Close();

                    //init stream
                    if (FileMemoryStream != null)
                    {
                        FileMemoryStream.Dispose();
                        FileMemoryStream = null;
                    }
                    FileMemoryStream = new MemoryStream(_bytMain);

                    FileLoaded = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private byte[] _getSector(int sectorNumber)
        {
            var bytSector = new byte[Blocksize];

            Buffer.BlockCopy(_bytMain, sectorNumber * Blocksize, bytSector, 0, Blocksize);

            return bytSector;
        }

        private byte[] _getSectors(int startSector, int sectorCount)
        {
            var bytSector = new byte[sectorCount * Blocksize];

            Buffer.BlockCopy(_bytMain, startSector * Blocksize, bytSector, 0, sectorCount * Blocksize);

            return bytSector;
        }
        #endregion

        #region ***** methods
        public byte[] GetSector(int sectorNumber)
        {
            return _getSector(sectorNumber);
        }

        public byte[] GetSectors(int startSector, int count)
        {
            return _getSectors(startSector, count);
        }
        #endregion

        #region ***** properties
        public int Blocksize { get; private set; } = 512;

        public bool FileLoaded { get; private set; } = false;

        public string FileName { get; private set; } = string.Empty;

        public DiskType FileDiskType { get; private set; } = DiskType.Unknown;

        public int FileLength { get; private set; } = -1;

        public MemoryStream FileMemoryStream { get; private set; } = null;
        #endregion
    }
}
