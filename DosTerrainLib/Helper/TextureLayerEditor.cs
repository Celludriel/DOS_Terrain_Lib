using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib.Helper
{
    public class TextureLayerEditor
    {

        public static DosTerrain SetIntensityOnLayerForCoordinate(DosTerrain terrain, UInt32 x, UInt32 y, UInt32 layer, byte value)
        {
            UInt32 pageIndex = Convert.ToUInt32((x / 64) + ((y / 64) * Math.Ceiling((terrain.Width / 64.0))));
            TextureLayerPage page = terrain.TexturePages.ElementAt((int)pageIndex);
            foreach (TextureLayerData data in page.Data)
            {
                if (data.TexturePosition == layer)
                {
                    UInt32 intensityIndex = (x % 32) + ((y % 32) * 32);
                    Intensity intensity = data.Intensities.ElementAt((int)intensityIndex);
                    SetIntensityTo(value, intensity);
                }
            }
            return terrain;
        }

        public static DosTerrain SetLayerIntensitiesTo(DosTerrain terrain, UInt32 layer, byte value)
        {
            foreach (TextureLayerPage page in terrain.TexturePages)
            {
                SetIntensitiesForTextureLayerPage(layer, value, page);
            }
            return terrain;
        }

        public static DosTerrain SetIntensitiesForTextureLayerPage(DosTerrain terrain, UInt32 pageIndex, UInt32 layer, byte value)
        {
            int maxPages = terrain.TexturePages.Count;
            if (pageIndex < maxPages)
            {
                TextureLayerPage page = terrain.TexturePages.ElementAt((int)pageIndex);
                SetIntensitiesForTextureLayerPage(layer, value, page);
            }
            else
            {
                throw new Exception("Page does not exist");
            }
            return terrain;
        }

        private static void SetIntensitiesForTextureLayerPage(UInt32 layer, byte value, TextureLayerPage page)
        {
            if (layer <= page.AmountOfLayers)
            {
                foreach (TextureLayerData data in page.Data)
                {
                    if (data.TexturePosition == (layer - 1))
                    {
                        SetIntensitiesTo(data.Intensities, value);
                    }
                }
            }
            else
            {
                throw new Exception("Layer does not exist");
            }
        }

        private static void SetIntensitiesTo(List<Intensity> data, byte value)
        {
            foreach (Intensity intensity in data)
            {
                SetIntensityTo(value, intensity);
            }
        }

        private static void SetIntensityTo(byte value, Intensity intensity)
        {
            intensity.value1 = value;
            intensity.value2 = value;
            intensity.value3 = value;
            intensity.value4 = value;
        }
    }
}
