using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdfCommanderLib.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var t = new AdfFile(@"u:\Emulation\Amiga\WinUAE4100_x64\DiskImages\games\HLS_NHL_2018_2019 - Kopie.adf");
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsFalse(true, e.Message);
            }
        }

        [TestMethod]
        public void ReadSector()
        {
            var t = new AdfFile(@"u:\Emulation\Amiga\WinUAE4100_x64\DiskImages\games\HLS_NHL_2018_2019 - Kopie.adf");
            var bytSector = t.GetSector(0);

            Assert.IsTrue(bytSector.Length == 512, System.Text.Encoding.ASCII.GetString(bytSector, 0, 25));
        }
    }
}
