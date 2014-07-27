using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib.Model
{
    public class LayerType
    {
        public UInt32 Index = 999;
        public Triangle[] Triangles;
        public Intensity[] Intensities;
    }
}
