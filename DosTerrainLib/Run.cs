using DosTerrainLib.Model;
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
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_001.data";
            String pathToOutputTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_001_out.data";

            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(64, 64, pathToTestFile);

            DosTerrainWriter writer = new DosTerrainWriter();
            writer.WriteDosTerrain(terrain, pathToOutputTestFile);

            terrain = parser.ReadDosTerrain(64, 64, pathToOutputTestFile);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
