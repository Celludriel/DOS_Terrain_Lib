using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosTerrainLib.Helper
{
    public class HeigthMapEditor
    {
        public static void SetHeightAt(DosTerrain terrain, UInt32 x, UInt32 y, float value)
        {
            uint index = GetIndexAt(terrain, x, y);
            terrain.HeightMapData[index] = value;
        }

        public static float GetHeightAt(DosTerrain terrain, UInt32 x, UInt32 y, float value)
        {
            uint index = GetIndexAt(terrain, x, y);
            return terrain.HeightMapData[index];
        }

        private static uint GetIndexAt(DosTerrain terrain, UInt32 x, UInt32 y)
        {
            UInt32 xBoundary = (terrain.Width - 1);
            UInt32 yBoundary = (terrain.Height - 1);
            if (x > xBoundary)
            {
                throw new Exception("X cannot exceed " + xBoundary);
            }

            if (y > yBoundary)
            {
                throw new Exception("Y cannot exceed " + yBoundary);
            }

            uint index = (y * terrain.Width + x);
            return index;
        }
    }
}
