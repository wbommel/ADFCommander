using System;
using System.Collections.Generic;
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

        private byte[] _bytMain = new byte[901120];
        private byte[] _bytCompare = new byte[901120];
        #endregion  

        #region ***** constructor / terminator / disposer
        public AdfFile()
        {

        }
        public AdfFile(string fileName) : this()
        {
            _strFilename = fileName;
            _initAdfFile();
        }
        #endregion

        #region ***** private functions
        private void _initAdfFile()
        {
            //check prerequisites
            if (!File.Exists(_strFilename))
            {
                return;
            }

            try
            {
                using (var fsSource = new FileStream(_strFilename, FileMode.Open, FileAccess.Read))
                {
                    //not the right size
                    if (fsSource.Length != 901120) { return; }

                    //read file to buffers
                    fsSource.Read(_bytMain, 0, 901120);
                    fsSource.Seek(0, SeekOrigin.Begin);
                    fsSource.Read(_bytCompare, 0, 901120);
                    fsSource.Close();
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
            var bytSector = new byte[512];

            Buffer.BlockCopy(_bytMain, sectorNumber * 512, bytSector, 0, 512);

            return bytSector;
        }
        #endregion

        #region ***** methods

        public byte[] GetSector(int sectorNumber)
        {
            return _getSector(sectorNumber);
        }
        #endregion

        #region ***** properties
        #endregion
    }
}
