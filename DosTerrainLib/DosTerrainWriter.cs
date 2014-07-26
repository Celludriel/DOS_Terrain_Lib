using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib
{
    class DosTerrainWriter
    {
        public void WriteDosTerrain(DosTerrain terrain, string outputFileName)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            {
                bw.Write(terrain.HeightMapSize);
                foreach(float value in terrain.HeightMapData){
                    bw.Write(value);
                }

                char[] terrainPatchData = new char[] { 'T', 'e', 'r', 'r', 'a', 'i', 'n', 'P', 'a', 't', 'c', 'h', 'D', 'a', 't', 'a' };
                bw.Write(terrainPatchData);

                int totalLayers = terrain.Layers.Count;
                foreach(LayerType layer in terrain.Layers){
                    WriteLayer(bw, totalLayers, layer);
                }                    
            }
        }

        private static void WriteLayer(BinaryWriter bw, int totalLayers, LayerType layer)
        {
            if (layer.Index != 999)
            {
                bw.Write(layer.Index);
            }

            Triangle[] triangles = layer.Triangles;
            bw.Write(Convert.ToUInt32(triangles.Length * 12));
            foreach (Triangle triangle in triangles)
            {
                bw.Write(triangle.Vertex1);
                bw.Write(triangle.Vertex2);
                bw.Write(triangle.Vertex3);
            }

            Intensity[] intensities = layer.Intensities;
            bw.Write(Convert.ToUInt32(intensities.Length * 4));
            foreach (Intensity intensity in intensities)
            {
                bw.Write(intensity.value1);
                bw.Write(intensity.value2);
                bw.Write(intensity.value3);
                bw.Write(intensity.value4);
            }

            if (layer.Index == 999)
            {
                bw.Write(totalLayers - 1);
            }
        }
    }
}
