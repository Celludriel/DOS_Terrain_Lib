using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DosTerrainLib.Model;

namespace DosTerrainLib.Model
{
    class DosTerrain
    {
        public UInt32 HeightMapLength;
        public float[] HeightMapData;
        public List<LayerType> Layers;
    }
}
