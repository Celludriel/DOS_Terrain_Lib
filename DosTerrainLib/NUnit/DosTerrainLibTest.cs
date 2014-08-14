using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DosTerrainLib.Model;
using System.IO;
using DosTerrainLib.Helper;

namespace DosTerrainLib.NUnit
{    
    [TestFixture]
    class DosTerrainLibTest
    {
        DosTerrainParser reader = new DosTerrainParser();
        DosTerrainWriter writer = new DosTerrainWriter();
        String rootPath = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\";

        [Test]
        public void parseWriteDimension10by10Test()
        {
            testDimension("10by10ONETEX", 10, 10);
        }

        [Test]
        public void testTextureEveryOneLine10by10Test()
        {
            testTextureEveryOneLine("10by10ONETEX", 10, 10);
        }

        [Test]
        public void parseWriteDimension40by63Test()
        {
            testDimension("40by63ONETEX", 40, 63);
        }

        [Test]
        public void testTextureEveryOneLine40by63Test()
        {
            testTextureEveryOneLine("40by63ONETEX", 40, 63);
        }

        [Test]
        public void parseWriteDimension63by40Test()
        {
            testDimension("63by40ONETEX", 63, 40);
        }

        [Test]
        public void testTextureEveryOneLine63by40Test()
        {
            testTextureEveryOneLine("63by40ONETEX", 63, 40);
        }

        [Test]
        public void parseWriteDimension63by63Test()
        {
            testDimension("63by63ONETEX", 63, 63);
        }

        [Test]
        public void testTextureEveryOneLine63by63Test()
        {
            testTextureEveryOneLine("63by63ONETEX", 63, 63);
        }

        [Test]
        public void parseWriteDimension120by63Test()
        {
            testDimension("120by63ONETEX", 120, 63);
        }

        [Test]
        public void testTextureEveryOneLine120by63Test()
        {
            testTextureEveryOneLine("120by63ONETEX", 120, 63);
        }

        [Test]
        public void parseWriteDimension63by120Test()
        {
            testDimension("63by120ONETEX", 63, 120);
        }

        [Test]
        public void testTextureEveryOneLine63by120Test()
        {
            testTextureEveryOneLine("63by120ONETEX", 63, 120);
        }

        [Test]
        public void parseWriteDimension100by120Test()
        {
            testDimension("100by120ONETEX", 100, 120);
        }

        [Test]
        public void testTextureEveryOneLine100by120Test()
        {
            testTextureEveryOneLine("100by120ONETEX", 100, 120);
        }

        [Test]
        public void parseWriteDimension120by100Test()
        {
            testDimension("120by100ONETEX", 120, 100);
        }

        [Test]
        public void testTextureEveryOneLine120by100Test()
        {
            testTextureEveryOneLine("120by100ONETEX", 120, 100);
        }

        [Test]
        public void parseWriteDimension120by120Test()
        {
            testDimension("120by120ONETEX", 120, 120);
        }

        [Test]
        public void testTextureEveryOneLine120by120Test()
        {
            testTextureEveryOneLine("120by120ONETEX", 120, 120);
        }

        [Test]
        public void parseWriteDimension128by128Test()
        {
            testDimension("128by128ONETEX", 128, 128);
        }

        [Test]
        public void testTextureEveryOneLine128by128Test()
        {
            testTextureEveryOneLine("128by128ONETEX", 128, 128);
        }

        [Test]
        public void parseWriteDimension128by320Test()
        {
            testDimension("128by320ONETEX", 128, 320);
        }

        [Test]
        public void testTextureEveryOneLine128by320Test()
        {
            testTextureEveryOneLine("128by320ONETEX", 128, 320);
        }

        [Test]
        public void parseWriteDimension320by128Test()
        {
            testDimension("320by128ONETEX", 320, 128);
        }

        [Test]
        public void testTextureEveryOneLine320by128Test()
        {
            testTextureEveryOneLine("320by128ONETEX", 320, 128);
        }

        [Test]
        public void parseWriteDimension1000by1000Test()
        {
            testDimension("1000by1000ONETEX", 1000, 1000);
        }

        [Test]
        public void testTextureEveryOneLine1000by1000Test()
        {
            testTextureEveryOneLine("1000by1000ONETEX", 1000, 1000);
        }

        private void testDimension(string file, uint x, uint y)
        {
            string fileIn = rootPath + file + ".data";
            string fileOut = rootPath + file + "_out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(x, y, fileIn);
                writer.WriteDosTerrain(terrain, fileOut);
                terrain = reader.ReadDosTerrain(x, y, fileOut);
            }
            catch (Exception ex)
            {
                Assert.Fail("Terrain with dimension x:" + x + ", y" + y +" couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    if (File.Exists(fileOut))
                    {
                        File.Delete(fileOut);
                    }
                }
            }
        }

        private void testTextureEveryOneLine(string file, uint x, uint y)
        {
            string fileIn = rootPath + file + ".data";
            string fileOut = rootPath + file + "_out.data";

            DosTerrainParser parser = new DosTerrainParser();
            DosTerrainWriter writer = new DosTerrainWriter();
            DosTerrain terrain;

            try
            {
                terrain = parser.ReadDosTerrain(x, y, fileIn);
                terrain = TextureLayerEditor.SetLayerIntensitiesTo(terrain, 1, 0);
                for (uint j = 0; j < y; j = j + 2)
                {
                    for (uint i = 0; i < x; i++)
                    {
                        terrain = TextureLayerEditor.SetIntensityOnLayerForCoordinate(terrain, i, j, 0, 255);
                    }
                }
                writer.WriteDosTerrain(terrain, fileOut);
                terrain = parser.ReadDosTerrain(x, y, fileOut);

            }
            catch (Exception ex)
            {
                Assert.Fail("Terrain with dimension x:" + x + ", y:" + y + " couldn't be textured every one line: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Cleaning up test");
                if (File.Exists(fileOut))
                {
                    File.Delete(fileOut);
                }
            }
        }
    }
}
