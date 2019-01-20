using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfCommanderLib
{
    public class HlsDosDirEntry
    {
        #region ***** statics
        public static bool TryParse(byte[] entry, out HlsDosDirEntry hdde)
        {
            try
            {
                hdde = new HlsDosDirEntry(
                    entry,
                    Encoding.ASCII.GetString(entry).Substring(0, Encoding.ASCII.GetString(entry).IndexOf((char)0)),
                    SwapEndianness(BitConverter.ToUInt32(entry, 32)),
                    SwapEndianness(BitConverter.ToUInt32(entry, 36)),
                    SwapEndianness(BitConverter.ToUInt32(entry, 40)),
                    SwapEndianness(BitConverter.ToUInt16(entry, 44)));
                return true;
            }
            catch
            {
                hdde = null;
                return false;
            }
        }

        public static uint SwapEndianness(uint x)
        {
            return ((x & 0x000000ff) << 24) +  // First byte
                   ((x & 0x0000ff00) << 8) +   // Second byte
                   ((x & 0x00ff0000) >> 8) +   // Third byte
                   ((x & 0xff000000) >> 24);   // Fourth byte
        }
        #endregion

        #region ***** constructor / terminator
        public HlsDosDirEntry(byte[] entry, string fileName, uint size, uint position, uint unknown1, uint unknonw2)
        {
            Entry = entry;
            FileName = fileName;
            Size = size;
            Position = position;
            Unknown1 = unknown1;
            Unknown2 = unknonw2;
        }
        #endregion

        #region ***** private functions
        #endregion

        #region ***** properties
        public string TextLineFormat { get; set; } = "{0,-25} {1,8} {2,8} {3,8} {4,5}"
        
        public byte[] Entry { get; set; }
        public string FileName { get; set; }
        public uint Size { get; set; }
        public uint Position { get; set; }
        public uint Unknown1 { get; set; }
        public uint Unknown2 { get; set; }
        #endregion

        #region ***** methods
        public string GetTextLine()
        {

        }
        #endregion
    }
}
