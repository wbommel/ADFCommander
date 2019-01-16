using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfCommanderLib
{
    public class AdfFile
    {
        #region ***** declarations
        private string _strFilename = string.Empty;

        private byte[] _bytMain = new byte[901119];
        private byte[] _bytCompare = new byte[901119];
        #endregion

        #region ***** constructor / terminator / disposer
        public AdfFile()
        {
            _initAdfFile();
        }
        public AdfFile(string fileName) : this()
        {
            _strFilename = fileName;
        }
        #endregion

        #region ***** private functions
        private void _initAdfFile()
        {
            if (!File.Exists(_strFilename))
            {
                return;
            }
        }
        #endregion

        #region ***** methods
        #endregion

        #region ***** properties
        #endregion
    }
}
