﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DosTerrainLib.Model;

namespace DosTerrainLib.Model
{
    public class DosTerrain
    {
        public UInt32 Width;
        public UInt32 Height;
        public UInt32 HeightMapSize;
        public float[] HeightMapData;
        public List<BackgroundData> BackGroundData;
        public List<TextureLayerPage> TextureLayerPages;
    }
}
