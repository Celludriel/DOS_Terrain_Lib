using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib
{
    public class DosTerrainParser
    {
        public DosTerrain ReadDosTerrain(UInt32 width, UInt32 height, string filename)
        {
            UInt32 x = width / 2;
            UInt32 y = height / 2;

            if (!File.Exists(filename))
            {
                throw new Exception("Error: The terrain file: " + filename + " doesn't exist.");
            }

            DosTerrain terrain = new DosTerrain();
            terrain.Width = x;
            terrain.Height = y;

            byte[] dosBinaryFileContent = File.ReadAllBytes(filename);
            using (MemoryStream dosBinaryMemoryStream = new MemoryStream(dosBinaryFileContent))
            {
                using (BinaryReader dosBinaryReader = new BinaryReader(dosBinaryMemoryStream))
                {
                    ReadHeightMapData(x, y, terrain, dosBinaryReader);

                    char[] padding = dosBinaryReader.ReadChars(16);

                    terrain.Layers = new List<LayerType>();
                    terrain.Layers.Add(ReadLayerType(dosBinaryReader, true));

                    UInt32 textureLayers = dosBinaryReader.ReadUInt32();
                    if (textureLayers != 0)
                    {
                        for (int i = 0; i < textureLayers; i++)
                        {
                            terrain.Layers.Add(ReadLayerType(dosBinaryReader, false));
                        }
                    }

                    ReadToEndOfFile(dosBinaryReader);
                }
            }
            return terrain;
        }

        private static LayerType ReadLayerType(BinaryReader dosBinaryReader, bool background)
        {
            LayerType layer = new LayerType();
            if (!background)
            {
                layer.Index = dosBinaryReader.ReadUInt32();
            }
            Console.WriteLine("Start reading layer at index: " + layer.Index);

            UInt32 layerHeader = 0;
            List<Triangle> triangles = new List<Triangle>();
            
            layerHeader = dosBinaryReader.ReadUInt32();
            do
            {
                UInt32 maxTriangles = layerHeader / 12;
                Console.WriteLine("Reading " + maxTriangles + " triangles");                
                for (int i = 0; i < maxTriangles; i++)
                {
                    triangles.Add(ReadTriangle(dosBinaryReader));
                }
                layerHeader = dosBinaryReader.ReadUInt32();
            } while (layerHeader == 24576);
            layer.Triangles = triangles.ToArray();
           
            UInt32 maxIntensities = dosBinaryReader.ReadUInt32() / 4;
            Console.WriteLine("Reading " + maxIntensities + " intensities");
            layer.Intensities = new Intensity[maxIntensities];
            for (int i = 0; i < maxIntensities; i++)
            {
                Intensity intensity = ReadIntensity(dosBinaryReader);
                layer.Intensities[i] = intensity;
            }

            Console.WriteLine("End reading layer at index: " + layer.Index);
            return layer;
        }

        private static Intensity ReadIntensity(BinaryReader dosBinaryReader)
        {
            Intensity intensity = new Intensity();
            intensity.value1 = dosBinaryReader.ReadByte();
            intensity.value2 = dosBinaryReader.ReadByte();
            intensity.value3 = dosBinaryReader.ReadByte();
            intensity.value4 = dosBinaryReader.ReadByte();
            return intensity;
        }

        private static Triangle ReadTriangle(BinaryReader dosBinaryReader)
        {            
            Triangle triangle = new Triangle();
            triangle.Vertex1 = dosBinaryReader.ReadUInt32();
            triangle.Vertex2 = dosBinaryReader.ReadUInt32();
            triangle.Vertex3 = dosBinaryReader.ReadUInt32();
            return triangle;
        }

        private static void ReadHeightMapData(UInt32 x, UInt32 y, DosTerrain terrain, BinaryReader dosBinaryReader)
        {
            Console.WriteLine("Start reading heightmap data");
            UInt32 totalReads = 0;
            UInt32 heightmapSize = dosBinaryReader.ReadUInt32();
            UInt32 maxTiles = (x + 1) * (y + 1);
            Console.WriteLine("Reading height data for " + maxTiles + " Tiles");
            UInt32 calculatedSizeFromParameters = (maxTiles * 4);
            if (heightmapSize == calculatedSizeFromParameters)
            {
                terrain.HeightMapSize = heightmapSize;
                terrain.HeightMapData = new float[maxTiles];
                for (int i = 0; i < maxTiles; i++)
                {
                    terrain.HeightMapData[i] = dosBinaryReader.ReadSingle();
                    totalReads++;
                }
            }
            else
            {
                throw new Exception("Invalid width and height provided map size should be " + heightmapSize + " but is calculated to " + calculatedSizeFromParameters);
            }
            Console.WriteLine("End reading heightmap data " + totalReads + " values read");
        }

        private static void ReadToEndOfFile(BinaryReader dosBinaryReader)
        {
            // temporary since I'm writing this parser in parts I just want to know if I'm going out of bounds or something else worse
            int amountOfExtraReads = 0;
            while (true)
            {
                try
                {
                    dosBinaryReader.ReadUInt32();
                    amountOfExtraReads++;
                }
                catch (Exception)
                {
                    Console.WriteLine("End of File Reached, " + amountOfExtraReads + " extra reads");
                    break;
                }
            }
        }
    }
}
