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
            DosTerrainParser parser = new DosTerrainParser();

            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\128.data";
            DosTerrain terrain = parser.ReadDosTerrain(128, 128, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\192.data";
            terrain = parser.ReadDosTerrain(192, 192, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\256.data";
            terrain = parser.ReadDosTerrain(256, 256, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\320.data";
            terrain = parser.ReadDosTerrain(320, 320, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\1000.data";
            terrain = parser.ReadDosTerrain(1000, 1000, pathToTestFile);
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
