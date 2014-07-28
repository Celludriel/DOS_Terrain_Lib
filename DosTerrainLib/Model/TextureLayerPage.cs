using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib.Model
{
    public class TextureLayerPage
    {
        public UInt32 PageNo;
        public UInt32 AmountOfLayers;
        public List<TextureLayerData> Data;
    }
}
