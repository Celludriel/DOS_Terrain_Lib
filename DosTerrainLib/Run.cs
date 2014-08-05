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

            pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\1000by1000ONETEX.data";
            String outputFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000_out.data";
            String txtOutput = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.txt";
            String xmlOutput = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.xml";
            new Dump().ReadToEndOfFile(pathToTestFile, txtOutput);
            terrain = parser.ReadDosTerrain(1000, 1000, pathToTestFile);
            //XMLWrite.WriteXML(terrain, xmlOutput);
            /*terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 0, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 1, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 2, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 3, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 4, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 5, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 6, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 7, 255);
            terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 8, 255);*/
            
            for (uint j = 0; j < 1000; j++)
            {
                for (uint i = 0; i < 1000; i++)
                {
                    for (uint layer = 0; layer < 10; layer++)
                    {
                        TextureLayerPage page = TextureLayerEditor.FindPageForCoordinate(terrain, i, j);
                        TextureLayerEditor.AddTextureLayerToPageIfNotExist(layer, 0, page, terrain.BackGroundData.ElementAt((int)page.PageNo));
                        terrain = TextureLayerEditor.SetIntensityOnLayerForCoordinate(terrain, i, j, layer, 255);
                    }
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
