using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib.Helper
{
    public class TextureLayerEditor
    {

        public static DosTerrain SetIntensityOnLayerForCoordinate(DosTerrain terrain, UInt32 x, UInt32 y, UInt32 layer, byte value)
        {
            TextureLayerPage page = FindPageForCoordinate(terrain, x, y);
            foreach (TextureLayerData data in page.Data)
            {
                if (data.TexturePosition == layer)
                {
                    UInt32 intensityIndex = CalculateIntensityIndex(terrain, x, y);
                    Intensity intensity = data.Intensities.ElementAt((int)intensityIndex);
                    SetIntensityTo(value, intensity);
                }
            }
            return terrain;
        }

        public static TextureLayerPage FindPageForCoordinate(DosTerrain terrain, UInt32 x, UInt32 y)
        {
            UInt32 pageIndex = CalculatePageIndex(terrain, x, y);
            return terrain.TextureLayerPages.ElementAt((int)pageIndex);
        }

        private static uint CalculateIntensityIndex(DosTerrain terrain, UInt32 x, UInt32 y)
        {
            if (terrain.Width < 64 && terrain.Height < 64)
            {
                return (x % (terrain.Width / 2)) + ((y % (terrain.Height / 2)) * (terrain.Width / 2));
            }
            else if (terrain.Width < 64 && terrain.Height >= 127)
            {
                return (x % (terrain.Width / 2)) + ((y % 32) * (terrain.Width / 2));
            }
            else if (terrain.Width >= 127 && terrain.Height < 64)
            {
                return (x % 32) + ((y % (terrain.Height / 2)) * 32);
            }
            else
            {
                return (x % 32) + ((y % 32) * 32);
            }
        }

        private static uint CalculatePageIndex(DosTerrain terrain, UInt32 x, UInt32 y)
        {
            if (terrain.Width < 127 && terrain.Height < 127)
            {
                return 0;
            }
            else if (terrain.Width < 127 && terrain.Height >= 127)
            {
                return Convert.ToUInt32((y / 64) - 1);
            }
            else if (terrain.Width >= 127 && terrain.Height < 127)
            {
                return Convert.ToUInt32((x / 64) - 1);
            }
            else
            {
                UInt32 maxWidthPages = (UInt32)Math.Floor(terrain.Width / 64.0);
                UInt32 maxHeightPages = (UInt32)Math.Floor(terrain.Height / 64.0);
                UInt32 maxWidth = maxWidthPages * 64;
                UInt32 maxHeight = maxHeightPages * 64;
                UInt32 xMod = (UInt32)((x / (double)terrain.Width) * maxWidth);
                UInt32 yMod = (UInt32)((y / (double)terrain.Height) * maxHeight);

                return Convert.ToUInt32((xMod / 64) + ((yMod / 64) * Math.Floor((maxWidth / 64.0))));
            }
        }

        public static DosTerrain SetLayerIntensitiesTo(DosTerrain terrain, UInt32 layer, byte value)
        {
            foreach (TextureLayerPage page in terrain.TextureLayerPages)
            {
                SetIntensitiesForTextureLayerPage(layer, value, page, terrain.BackGroundData.ElementAt((int)page.PageNo));
            }
            return terrain;
        }

        public static DosTerrain SetIntensitiesForTextureLayerPage(DosTerrain terrain, UInt32 pageIndex, UInt32 layer, byte value)
        {
            int maxPages = terrain.TextureLayerPages.Count;
            if (pageIndex < maxPages)
            {
                TextureLayerPage page = terrain.TextureLayerPages.ElementAt((int)pageIndex);
                SetIntensitiesForTextureLayerPage(layer, value, page, terrain.BackGroundData.ElementAt((int)pageIndex));
            }
            else
            {
                throw new Exception("Page does not exist");
            }
            return terrain;
        }

        private static void SetIntensitiesForTextureLayerPage(UInt32 layer, byte value, TextureLayerPage page, BackgroundData backGroundData)
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
                AddTextureLayerToPageIfNotExist(layer, value, page, backGroundData);
            }
        }

        public static void AddTextureLayerToPageIfNotExist(UInt32 layer, byte value, TextureLayerPage page, BackgroundData backGroundData)
        {

            TextureLayerData layerToAdd = null;
            foreach (TextureLayerData data in page.Data)
            {
                if (data.TexturePosition == layer)
                {
                    layerToAdd = data;
                }
            }

            if (layerToAdd == null)
            {
                layerToAdd = new TextureLayerData();
                layerToAdd.TexturePosition = layer;
                layerToAdd.TriangleBytes = backGroundData.TriangleBytes;
                layerToAdd.Triangles = new List<Triangle>();
                layerToAdd.Triangles.AddRange(backGroundData.Triangles);
                layerToAdd.IntensityBytes = layerToAdd.TriangleBytes / 6;
                layerToAdd.Intensities = new List<Intensity>();
                for (int i = 0; i < layerToAdd.IntensityBytes / 4; i++)
                {
                    Intensity intensity = new Intensity();
                    SetIntensityTo(value, intensity);
                    layerToAdd.Intensities.Add(intensity);
                }

                if (page.Data == null)
                {
                    page.Data = new List<TextureLayerData>();
                }
                page.Data.Add(layerToAdd);
                page.AmountOfLayers = (uint)page.Data.Count;
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
