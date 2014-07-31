using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib
{
    public class DosTerrainWriter
    {
        public void WriteDosTerrain(DosTerrain terrain, string outputFileName)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            {
                // writing of heightmap
                bw.Write(terrain.HeightMapSize);
                foreach(float value in terrain.HeightMapData){
                    bw.Write(value);
                }

                // writing of string padding
                char[] terrainPatchData = new char[] { 'T', 'e', 'r', 'r', 'a', 'i', 'n', 'P', 'a', 't', 'c', 'h', 'D', 'a', 't', 'a' };
                bw.Write(terrainPatchData);

                // writing of background layer data
                foreach (BackgroundData data in terrain.BackGroundData)
                {
                    bw.Write(data.BackgroundLayerByteSize);
                    foreach (Triangle triangle in data.Triangles)
                    {
                        bw.Write(triangle.Vertex1);
                        bw.Write(triangle.Vertex2);
                        bw.Write(triangle.Vertex3);
                    }              
                }
                
                foreach (TextureLayerPage textureLayerPage in terrain.TexturePages)
                {
                    bw.Write(textureLayerPage.PageNo);
                    bw.Write(textureLayerPage.AmountOfLayers);
                    foreach (TextureLayerData data in textureLayerPage.Data)
                    {
                        bw.Write(data.TexturePosition);                        
                        bw.Write(data.TriangleBytes);
                        foreach (Triangle triangle in data.Triangles)
                        {
                            bw.Write(triangle.Vertex1);
                            bw.Write(triangle.Vertex2);
                            bw.Write(triangle.Vertex3);
                        }

                        bw.Write(data.IntensityBytes);
                        foreach (Intensity intensity in data.Intensities)
                        {
                            bw.Write(intensity.value1);
                            bw.Write(intensity.value2);
                            bw.Write(intensity.value3);
                            bw.Write(intensity.value4);
                        }
                    }
                }
            }
        }
    }
}
