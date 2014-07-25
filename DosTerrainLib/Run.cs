using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib
{
    class Run
    {
        static void Main()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.data";

            DosTerrainParser parser = new DosTerrainParser();
            parser.readDosTerrain(100,100,pathToTestFile);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
