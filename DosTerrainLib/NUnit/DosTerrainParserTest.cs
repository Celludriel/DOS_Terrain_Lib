using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DosTerrainLib.Model;

namespace DosTerrainLib.NUnit
{    
    [TestFixture]
    class DosTerrainParserTest
    {
        [Test]
        public void parser100By100NoExtraLayersTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.data";
            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(100, 100, pathToTestFile);

            UInt32 heightMapSizeInBytes = 2601 * 4;
            Assert.AreEqual(heightMapSizeInBytes, terrain.HeightMapSize);
            Assert.AreEqual(heightMapSizeInBytes, terrain.HeightMapData.Length * 4);

            Assert.AreEqual(1, terrain.Layers.Count);
            LayerType layer = terrain.Layers.ElementAt(0);
            Assert.AreEqual(999, layer.Index);
            UInt32 triangleSizeInBytes = 50 * 50 * 12;
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            UInt32 intensitiesSizeInBytes = 50 * 50 * 4;
            Assert.AreEqual(intensitiesSizeInBytes, layer.Intensities.Length * 4);
        }

        [Test]
        public void parser64By64NoExtraLayersTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_001.data";
            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(64, 64, pathToTestFile);

            UInt32 heightMapSizeInBytes = 1089 * 4;
            Assert.AreEqual(heightMapSizeInBytes, terrain.HeightMapSize);
            Assert.AreEqual(heightMapSizeInBytes, terrain.HeightMapData.Length * 4);

            Assert.AreEqual(1, terrain.Layers.Count);
            LayerType layer = terrain.Layers.ElementAt(0);
            Assert.AreEqual(999, layer.Index);
            UInt32 triangleSizeInBytes = 32 * 32 * 12;
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            UInt32 intensitiesSizeInBytes = 32 * 32 * 4;
            Assert.AreEqual(intensitiesSizeInBytes, layer.Intensities.Length * 4);
        }

    }
}
