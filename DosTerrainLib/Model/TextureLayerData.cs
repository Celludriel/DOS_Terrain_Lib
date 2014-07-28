using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib.Model
{
    public class TextureLayerData
    {
        public UInt32 TexturePosition;
        public List<Triangle> Triangles;
        public List<Intensity> Intensities;
    }
}
