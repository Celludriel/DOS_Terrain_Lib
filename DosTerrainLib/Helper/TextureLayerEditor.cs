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
/*             using (FileStream fs = new FileStream("c:\\temp\\log.txt",FileMode.Append, FileAccess.Write)){
                 using (StreamWriter sw = new StreamWriter(fs))
                 { */
                    UInt32 pageIndex = CalculatePageIndex(terrain, x, y);
                    //sw.WriteLine("x: " + x + " y: " + y + " index: " + pageIndex);
                    TextureLayerPage page = terrain.TextureLayerPages.ElementAt((int)pageIndex);
                    foreach (TextureLayerData data in page.Data)
                    {
                        if (data.TexturePosition == layer)
                        {
                            UInt32 intensityIndex = CalculateIntensityIndex(terrain, x, y);
                            //sw.WriteLine("Intensity " + intensityIndex);
                            Intensity intensity = data.Intensities.ElementAt((int)intensityIndex);
                            SetIntensityTo(value, intensity);
                        }
                    }
                    return terrain;
/*                 }
             } */
        }

        private static uint CalculateIntensityIndex(DosTerrain terrain, UInt32 x, UInt32 y)
        {
            if (terrain.Width < 64 && terrain.Height < 64)
            {
                return (x % (terrain.Width / 2)) + ((y % (terrain.Height / 2)) * (terrain.Width / 2));
            }
            else if (terrain.Width < 64 && terrain.Height >= 127)
            {
                // don't know yet
                return (x % (terrain.Width / 2)) + ((y % 32) * (terrain.Width / 2));
            }
            else if (terrain.Width >= 127 && terrain.Height < 64)
            {
                // don't know yet
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
                AddTextureLayerToPage(layer, value, page, backGroundData);
            }
        }

        private static void AddTextureLayerToPage(UInt32 layer, byte value, TextureLayerPage page, BackgroundData backGroundData)
        {
            TextureLayerData data = new TextureLayerData();
            data.TexturePosition = layer - 1;
            data.TriangleBytes = backGroundData.TriangleBytes;
            data.Triangles = new List<Triangle>();
            data.Triangles.AddRange(backGroundData.Triangles);
            data.IntensityBytes = data.TriangleBytes / 6;
            data.Intensities = new List<Intensity>();
            for (int i = 0; i < data.IntensityBytes / 4; i++)
            {
                Intensity intensity = new Intensity();
                SetIntensityTo(value, intensity);
                data.Intensities.Add(intensity);
            }

            if (page.Data == null)
            {
                page.Data = new List<TextureLayerData>();
            }
            page.Data.Add(data);
            page.AmountOfLayers = page.AmountOfLayers + 1;
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
