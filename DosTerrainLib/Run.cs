using DosTerrainLib.Helper;
using DosTerrainLib.Model;
using DosTerrainLib.Util;
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
            DosTerrainWriter writer = new DosTerrainWriter();
            DosTerrain terrain;
            String pathToTestFile;

            /*

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\21.data";
            terrain = parser.ReadDosTerrain(21, 21, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\63.data";
            terrain = parser.ReadDosTerrain(63, 63, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\64.data";
            terrain = parser.ReadDosTerrain(64, 64, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\128.data";
            terrain = parser.ReadDosTerrain(128, 128, pathToTestFile);
            
            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\192.data";
            terrain = parser.ReadDosTerrain(192, 192, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\254.data";
            terrain = parser.ReadDosTerrain(254, 254, pathToTestFile);

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\256.data";
            terrain = parser.ReadDosTerrain(256, 256, pathToTestFile);
            
            */          

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\320TWOTEX.data";
            String outputFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.data";
            String xmlOutput = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.xml";
            terrain = parser.ReadDosTerrain(320, 320, pathToTestFile);
            XMLWrite.WriteXML(terrain, xmlOutput);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 1, 0);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 2, 0);
            for(uint j = 0; j < 320;j = j+2){
                for (uint i = 0; i < 320; i++)
                {
                    terrain = TextureLayerEditor.SetIntensityOnLayerForCoordinate(terrain, i, j, 1, 255);
                }
            }
            writer.WriteDosTerrain(terrain, outputFile);

            /*

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\1000.data";
            terrain = parser.ReadDosTerrain(1000, 1000, pathToTestFile);*/

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
