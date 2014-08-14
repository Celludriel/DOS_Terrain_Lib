using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DosTerrainLib.Model
{
    public class TextureLayerData
    {
        public UInt32 TexturePosition;
        public UInt32 TriangleBytes;
        public List<Triangle> Triangles;
        public UInt32 IntensityBytes;
        public List<Intensity> Intensities;
    }
}
