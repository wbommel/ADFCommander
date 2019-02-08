using AdfCommanderLib;
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
            var t = new HlsDos(new AdfFile(@"HLS_NHL_2018_2019 - Kopie.adf"));

            Console.WriteLine(t.GetDirectory());

            var t2 = new HlsDos(new AdfFile(@"HDExampleDisk.adf"));



            Console.Write("press any key to end.");
            Console.ReadKey();
        }
    }
}
