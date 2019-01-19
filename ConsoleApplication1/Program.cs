using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new AdfCommanderLib.AdfFile(@"HLS_NHL_2018_2019 - Kopie.adf");

            if (!t.FileLoaded) { return; }

            var bytDir = t.GetSectors(0, 10);


            //read directory
            int idx = 42;//first directory entry
            while (bytDir[idx] != 0)
            {
                byte[] bytEntry = new byte[46];
                Buffer.BlockCopy(bytDir, idx, bytEntry, 0, 46);

                Console.WriteLine(System.Text.Encoding.ASCII.GetString(bytEntry));

                idx += 46;

            }


            Console.ReadKey();
        }
    }
}
