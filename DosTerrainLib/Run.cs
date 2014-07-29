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
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\320TWOTEX.data";
            String outputFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\320TWOTEXOUT.data";

            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(320, 320, pathToTestFile);

            DosTerrainWriter writer = new DosTerrainWriter();
            writer.WriteDosTerrain(terrain, outputFile);

            Dump dumper = new Dump();
            dumper.ReadToEndOfFile(pathToTestFile, "320TWOTEX.dump");
            dumper.ReadToEndOfFile(outputFile, "320TWOTEXOUT.dump");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
