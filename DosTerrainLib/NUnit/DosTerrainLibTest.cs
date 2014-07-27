using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DosTerrainLib.Model;
using System.IO;

namespace DosTerrainLib.NUnit
{    
    [TestFixture]
    class DosTerrainLibTest
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
            UInt32 triangleSizeInBytes = 64 * 64 * 24;
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            Assert.AreEqual(0, layer.Intensities.Length * 4);
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
            UInt32 triangleSizeInBytes = 32 * 32 * 24;
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            Assert.AreEqual(0, layer.Intensities.Length * 4);
        }

        [Test]
        public void parser64By64ExtraLayersTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_002.data";
            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(64, 64, pathToTestFile);

            UInt32 heightMapSizeInBytes = 1089 * 4;
            Assert.AreEqual(heightMapSizeInBytes, terrain.HeightMapSize);
            Assert.AreEqual(heightMapSizeInBytes, terrain.HeightMapData.Length * 4);

            Assert.AreEqual(3, terrain.Layers.Count);
            UInt32 triangleSizeInBytes = 32 * 32 * 24;
            
            LayerType layer = terrain.Layers.ElementAt(0);
            Assert.AreEqual(999, layer.Index);
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            Assert.AreEqual(0, layer.Intensities.Length * 4);

            triangleSizeInBytes = 32 * 32 * 24;
            UInt32 intensitiesInBytes = 32 * 32 * 4;
            
            layer = terrain.Layers.ElementAt(1);
            Assert.AreEqual(0, layer.Index);
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            Assert.AreEqual(intensitiesInBytes, layer.Intensities.Length * 4);

            layer = terrain.Layers.ElementAt(2);
            Assert.AreEqual(1, layer.Index);
            Assert.AreEqual(triangleSizeInBytes, layer.Triangles.Length * 12);
            Assert.AreEqual(intensitiesInBytes, layer.Intensities.Length * 4);
        }

        [Test]
        public void write100By100NoExtraLayersTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.data";
            String pathToOutputTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000_out.data";

            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(100, 100, pathToTestFile);

            DosTerrainWriter writer = new DosTerrainWriter();
            writer.WriteDosTerrain(terrain, pathToOutputTestFile);

            try
            {
                DosTerrain terrain2 = parser.ReadDosTerrain(100, 100, pathToOutputTestFile);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            finally
            {
                File.Delete(pathToOutputTestFile);
            }
        }

        [Test]
        public void write64By64NoExtraLayersTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_001.data";
            String pathToOutputTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_001_out.data";

            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(64, 64, pathToTestFile);

            DosTerrainWriter writer = new DosTerrainWriter();
            writer.WriteDosTerrain(terrain, pathToOutputTestFile);

            try
            {
                DosTerrain terrain2 = parser.ReadDosTerrain(64, 64, pathToOutputTestFile);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            finally
            {
                File.Delete(pathToOutputTestFile);
            }
        }

        [Test]
        public void write64By64ExtraLayersTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_002.data";
            String pathToOutputTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_002_out.data";

            DosTerrainParser parser = new DosTerrainParser();
            DosTerrain terrain = parser.ReadDosTerrain(64, 64, pathToTestFile);

            DosTerrainWriter writer = new DosTerrainWriter();
            writer.WriteDosTerrain(terrain, pathToOutputTestFile);

            try
            {
                DosTerrain terrain2 = parser.ReadDosTerrain(64, 64, pathToOutputTestFile);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            finally
            {
                File.Delete(pathToOutputTestFile);
            }
        }

        [Test]
        public void parseFileDoesNotExistExceptionTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\fake.data";
            DosTerrainParser parser = new DosTerrainParser();

            try
            {
                DosTerrain terrain = parser.ReadDosTerrain(100, 100, pathToTestFile);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Error: The terrain file: C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\fake.data doesn't exist.", e.Message);
            }
        }

        [Test]
        public void parseInvalidSizesGivenTest()
        {
            String pathToTestFile = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\Terrain_000.data";
            DosTerrainParser parser = new DosTerrainParser();

            try
            {
                DosTerrain terrain = parser.ReadDosTerrain(50, 100, pathToTestFile);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid width and height provided map size should be 10404 but is calculated to 5304", e.Message);
            }
        }
    }
}
