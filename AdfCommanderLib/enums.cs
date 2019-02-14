namespace AdfCommanderLib
{
    /// <summary>
    /// 
    /// </summary>
    public enum DiskType
    {
        Unknown = -1,
        DoubleDensity_DD = 1,
        HighDensity_HD = 2,
    }

    public enum AmigaDosFileSystemType
    {
        Unknown = -1,
        OFS = 1,
        FFS = 2,
    }

    public enum AmigaDosBlockType
    {
        Unknown = -1,
        RootBlock = 1,
        BitmapBlock,
        BitmapExtensionBlock,
        UserDirectoryBlock,
        FileHeaderBlock,
        FileExtensionBlock,
        DataBlock,
    }
}
