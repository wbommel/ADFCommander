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
        public string TextLineFormat { get; set; } = "{0,-25} {1,8} {2,8} {3,8} {4,5}";

        public byte[] Entry { get; private set; }
        public string FileName { get; private set; }
        public uint Size { get; private set; }
        public uint Position { get; private set; }
        public uint Unknown1 { get; private set; }
        public uint Unknown2 { get; private set; }
        #endregion

        #region ***** methods
        public string GetTextLine()
        {
            return string.Format(TextLineFormat, FileName, Size, Position, Unknown1, Unknown2);
        }

        public string GetHexLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= Entry.GetUpperBound(0); i++)
            {
                if (i == 44) { }
                sb.Append(Entry[i].ToString("X2"));
                sb.Append(" ");
            }
            return sb.ToString();
        }
        #endregion
    }
}
