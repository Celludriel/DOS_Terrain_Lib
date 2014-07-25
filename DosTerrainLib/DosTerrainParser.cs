using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib
{
    class DosTerrainParser
    {
        public DosTerrain readDosTerrain(UInt32 width, UInt32 height, string filename)
        {
            UInt32 x = width / 2;
            UInt32 y = height / 2;

            if (!File.Exists(filename))
            {
                throw new Exception("Error: The terrain file: " + filename + " doesn't exist.");
            }

            DosTerrain terrain = new DosTerrain();
            byte[] dosBinaryFileContent = File.ReadAllBytes(filename);
            using (MemoryStream dosBinaryMemoryStream = new MemoryStream(dosBinaryFileContent))
            {
                using (BinaryReader dosBinaryReader = new BinaryReader(dosBinaryMemoryStream))
                {
                    readHeightMapData(x, y, terrain, dosBinaryReader);
                    readToEndOfFile(dosBinaryReader);
                }
            }
            return terrain;
        }

        private static void readHeightMapData(UInt32 x, UInt32 y, DosTerrain terrain, BinaryReader dosBinaryReader)
        {
            UInt32 heightmapSize = dosBinaryReader.ReadUInt32();
            UInt32 maxTiles = (x + 1) * (y + 1);
            UInt32 calculatedSizeFromParameters = (maxTiles * 4);
            if (heightmapSize == calculatedSizeFromParameters)
            {
                terrain.HeightMapLength = heightmapSize;
                terrain.HeightMapData = new float[maxTiles];
                for (int i = 0; i < maxTiles; i++)
                {
                    terrain.HeightMapData[i] = dosBinaryReader.ReadSingle();
                }
            }
            else
            {
                throw new Exception("Invalid width and height provided map size should be " + heightmapSize + " but is calculated to " + calculatedSizeFromParameters);
            }
        }

        private static void readToEndOfFile(BinaryReader dosBinaryReader)
        {
            // temporary since I'm writing this parser in parts I just want to know if I'm going out of bounds or something else worse
            while (true)
            {
                try
                {
                    dosBinaryReader.ReadUInt32();
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
    }
}
