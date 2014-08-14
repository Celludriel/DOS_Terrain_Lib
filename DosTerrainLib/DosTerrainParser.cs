using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            terrain.Width = width;
            terrain.Height = height;

            byte[] dosBinaryFileContent = File.ReadAllBytes(filename);
            using (MemoryStream dosBinaryMemoryStream = new MemoryStream(dosBinaryFileContent))
            {
                using (BinaryReader dosBinaryReader = new BinaryReader(dosBinaryMemoryStream))
                {
                    ReadHeightMapData(x, y, terrain, dosBinaryReader);

                    char[] padding = dosBinaryReader.ReadChars(16);

                    terrain.BackGroundData = ReadBackgroundLayer(dosBinaryReader, x, y);              
                    terrain.TextureLayerPages = ReadTexturePages(dosBinaryReader, x, y);

                    ReadToEndOfFile(dosBinaryReader);
                }
            }
            return terrain;
        }

        private static List<TextureLayerPage> ReadTexturePages(BinaryReader dosBinaryReader, UInt32 x, UInt32 y)
        {
            List<TextureLayerPage> textureLayerPages = new List<TextureLayerPage>();
            uint amountOfBigTiles = CalculatePages(x, y);

            for (int j = 0; j < amountOfBigTiles; j++)
            {
                TextureLayerPage textureLayerPage = new TextureLayerPage();
                textureLayerPage.PageNo = dosBinaryReader.ReadUInt32();                
                textureLayerPage.AmountOfLayers = dosBinaryReader.ReadUInt32();
                List<TextureLayerData> textureLayerDataList = new List<TextureLayerData>();
                for (int k = 0; k < textureLayerPage.AmountOfLayers; k++)
                {
                    TextureLayerData data = new TextureLayerData();

                    data.TexturePosition = dosBinaryReader.ReadUInt32();
                    data.TriangleBytes = dosBinaryReader.ReadUInt32();
                    UInt32 maxTriangles = data.TriangleBytes / 12;
                    List<Triangle> triangles = new List<Triangle>();
                    Console.WriteLine("Reading " + maxTriangles + " triangles");
                    for (int i = 0; i < maxTriangles; i++)
                    {
                        triangles.Add(ReadTriangle(dosBinaryReader));
                    }
                    data.Triangles = triangles;

                    data.IntensityBytes = dosBinaryReader.ReadUInt32();
                    List<Intensity> intensities = new List<Intensity>();
                    UInt32 maxIntensities = data.IntensityBytes / 4;
                    Console.WriteLine("Reading " + maxIntensities + " intensities");
                    for (int i = 0; i < maxIntensities; i++)
                    {
                        intensities.Add(ReadIntensity(dosBinaryReader));
                    }
                    data.Intensities = intensities;
                    textureLayerDataList.Add(data);
                }
                textureLayerPage.Data = textureLayerDataList;
                textureLayerPages.Add(textureLayerPage);
            }
            return textureLayerPages;
        }

        private static List<BackgroundData> ReadBackgroundLayer(BinaryReader dosBinaryReader, uint x, uint y)
        {
            List<BackgroundData> data = new List<BackgroundData>();
            uint amountOfBigTiles = CalculatePages(x, y);

            for(uint j=0;j < amountOfBigTiles;j++){
                BackgroundData backgroundData = new BackgroundData();
                List<Triangle> triangles = new List<Triangle>();
                backgroundData.TriangleBytes = dosBinaryReader.ReadUInt32();
                UInt32 maxTriangles = backgroundData.TriangleBytes / 12;
                Console.WriteLine("Reading " + maxTriangles + " triangles");                
                for (int i = 0; i < maxTriangles; i++)
                {
                    triangles.Add(ReadTriangle(dosBinaryReader));
                }
                backgroundData.Triangles = triangles.ToArray();                
                data.Add(backgroundData);
            }
            return data;
        }

        private static uint CalculatePages(uint x, uint y)
        {            
            if (x <= 32 && y <= 32)
            {
                return 1;
            }
            else
            {
                uint xFactor = CalculatePageFactor(x);
                uint yFactor = CalculatePageFactor(y);

                uint amountOfPages = xFactor * yFactor;
                if (amountOfPages == 0)
                {
                    amountOfPages = 1;
                }
                return amountOfPages;
            }
        }

        private static uint CalculatePageFactor(uint coord)
        {
            // Anything smaller the 32 is always 1 page so no calculation needed
            if (coord > 32)
            {
                if (coord % 2 != 0)
                {
                    ++coord;
                }
                return (UInt32)coord / 32;
            }
            else
            {
                return 1;
            }
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
            terrain.HeightMapSize = dosBinaryReader.ReadUInt32();
            Console.WriteLine("Heightmap size " + terrain.HeightMapSize);
            UInt32 maxTiles = (x + 1) * (y + 1);
            Console.WriteLine("Reading height data for " + maxTiles + " Tiles");
            UInt32 calculatedSizeFromParameters = (maxTiles * 4);
            if (terrain.HeightMapSize == calculatedSizeFromParameters)
            {                
                terrain.HeightMapData = new float[maxTiles];
                for (int i = 0; i < maxTiles; i++)
                {
                    terrain.HeightMapData[i] = dosBinaryReader.ReadSingle();
                    totalReads++;
                }
            }
            else
            {
                throw new Exception("Invalid width and height provided map size should be " + terrain.HeightMapSize + " but is calculated to " + calculatedSizeFromParameters);
            }
            Console.WriteLine("End reading heightmap data " + totalReads + " values read");
        }

        private static void ReadToEndOfFile(BinaryReader dosBinaryReader)
        {
            // temporary since I'm writing this parser in parts I just want to know if I'm going out of bounds or something else worse
            int amountOfExtraReads = 0;
            using (StreamWriter bw = new StreamWriter(File.Open("log.txt", FileMode.Create)))
            {
                while (true)
                {
                    try
                    {
                            bw.WriteLine(dosBinaryReader.ReadUInt32());
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
}