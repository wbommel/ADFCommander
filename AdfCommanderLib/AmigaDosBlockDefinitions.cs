using System;
using System.IO;

namespace AdfCommanderLib
{
    public class BootBlock
    {
        public byte[] DiskType { get; private set; }
        public UInt32 Checksum { get; private set; }
        public UInt32 RootBlockPointer { get; private set; }
        public byte[] BootCode { get; private set; }

        public BootBlock(BinaryReader br, int size)
        {
            DiskType = br.ReadBytes(4, Endianness.Little);
            Checksum = br.ReadUInt32(Endianness.Big);
            RootBlockPointer = br.ReadUInt32(Endianness.Big);
            BootCode = br.ReadBytes(size - (3 * 4), Endianness.Big);
        }
    }

    public class BlockTypeStubb
    {
        public int PrimaryBlockTypeIdentifier { get; private set; }
        public int SecondaryBlockTypeIdentifier { get; private set; }
        public AmigaDosBlockType BlockType { get; private set; } = AmigaDosBlockType.Unknown;

        public BlockTypeStubb(BinaryReader br, int positionPrimary, int positionSecondary)
        {
            br.BaseStream.Seek(positionPrimary, SeekOrigin.Begin);
            PrimaryBlockTypeIdentifier = br.ReadInt32(Endianness.Big);
            br.BaseStream.Seek(positionSecondary, SeekOrigin.Begin);
            SecondaryBlockTypeIdentifier = br.ReadInt32(Endianness.Big);

            _recognizeBlockType();
        }

        private void _recognizeBlockType()
        {
            if (PrimaryBlockTypeIdentifier == 2 && SecondaryBlockTypeIdentifier == 1) { BlockType = AmigaDosBlockType.RootBlock; }
            if (PrimaryBlockTypeIdentifier == 2 && SecondaryBlockTypeIdentifier == 2) { BlockType = AmigaDosBlockType.UserDirectoryBlock; }
            if (PrimaryBlockTypeIdentifier == 2 && SecondaryBlockTypeIdentifier == -3) { BlockType = AmigaDosBlockType.FileHeaderBlock; }
            if (PrimaryBlockTypeIdentifier == 16 && SecondaryBlockTypeIdentifier == -3) { BlockType = AmigaDosBlockType.FileExtensionBlock; }
            if (PrimaryBlockTypeIdentifier == 8) { BlockType = AmigaDosBlockType.DataBlock; }
        }
    }

    public class RootBlock
    {
        public uint PrimaryBlockType { get; private set; }

    }
}
