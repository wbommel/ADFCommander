using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdfCommanderLib
{
    public class AdfFile
    {
        #region ***** declarations
        private string _strFilename = string.Empty;

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
            _strFilename = fileName;
            _initAdfFile();
        }
        #endregion

        #region ***** private functions
        private void _initAdfFile()
        {
            //prerequisites
            if (FileLoaded) { FileLoaded = false; }
            if (!File.Exists(_strFilename)) { throw new FileNotFoundException(_strFilename); }

            try
            {
                using (var fsSource = new FileStream(_strFilename, FileMode.Open, FileAccess.Read))
                {
                    //vars
                    int fileLength = (int)fsSource.Length;

                    //verify file length
                    if (fileLength % Blocksize != 0)
                    {
                        throw new InvalidDataException(string.Format("Invalid block size to file size ratio (bs:{0}, fs:{1}).", Blocksize, fileLength));
                    }
#if DEBUG
                    Debug.WriteLine(string.Format("Filename (bytes): {0} ({1})", _strFilename, fileLength));
#endif
                    //init arrays
                    if (_bytMain != null) { _bytMain = null; }
                    if (_bytCompare != null) { _bytCompare = null; }
                    _bytMain = new byte[fileLength];
                    _bytCompare = new byte[fileLength];

                    //check for known disk sizes
                    if (fileLength == 1760 * Blocksize) { FileDiskType = DiskType.DoubleDensity_DD; }
                    if (fileLength == 3520 * Blocksize) { FileDiskType = DiskType.HighDensity_HD; }

                    //read file to buffers
                    fsSource.Read(_bytMain, 0, fileLength);
                    fsSource.Seek(0, SeekOrigin.Begin);
                    fsSource.Read(_bytCompare, 0, fileLength);
                    fsSource.Close();

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

        public DiskType FileDiskType { get; private set; } = DiskType.Unknown;
        #endregion
    }
}
