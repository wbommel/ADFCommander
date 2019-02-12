﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
