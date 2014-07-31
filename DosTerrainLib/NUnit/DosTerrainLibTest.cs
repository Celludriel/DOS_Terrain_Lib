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
        DosTerrainParser reader = new DosTerrainParser();
        DosTerrainWriter writer = new DosTerrainWriter();
        String rootPath = "C:\\Git\\Repos\\DOS_Terrain_Lib\\DosTerrainLib\\TestData\\";

        [Test]
        public void parseWriteDimension21By21Test()
        {
            string fileIn = "21.data";
            string fileOut = "21out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(21, 21, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(21, 21, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 21x21 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension63By63Test()
        {
            string fileIn = "63.data";
            string fileOut = "63out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(63, 63, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(63, 63, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 63x63 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension64By64Test()
        {
            string fileIn = "64.data";
            string fileOut = "64out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(64, 64, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(64, 64, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 64x64 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension127By127Test()
        {
            string fileIn = "127.data";
            string fileOut = "127out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(127, 127, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(127, 127, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 127x127 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension128By128Test()
        {
            string fileIn = "128.data";
            string fileOut = "128out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(128, 128, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(128, 128, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 128x128 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension192By192Test()
        {
            string fileIn = "192.data";
            string fileOut = "192out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(192, 192, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(192, 192, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 192x192 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension254By254Test()
        {
            string fileIn = "254.data";
            string fileOut = "254out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(254, 254, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(254, 254, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 254x254 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension256By256Test()
        {
            string fileIn = "256.data";
            string fileOut = "256out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(256, 256, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(256, 256, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 256x256 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension320By320Test()
        {
            string fileIn = "320.data";
            string fileOut = "320out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(320, 320, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(320, 320, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 320x320 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension320By320OneTextureTest()
        {
            string fileIn = "320ONETEX.data";
            string fileOut = "320ONETEXout.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(320, 320, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(320, 320, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 320x320 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension320By320TwoTexturesTest()
        {
            string fileIn = "320TWOTEX.data";
            string fileOut = "320TWOTEXout.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(320, 320, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(320, 320, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 320x320 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

        [Test]
        public void parseWriteDimension1000By1000Test()
        {
            string fileIn = "1000.data";
            string fileOut = "1000out.data";
            try
            {
                DosTerrain terrain = reader.ReadDosTerrain(1000, 1000, rootPath + fileIn);
                writer.WriteDosTerrain(terrain, rootPath + fileOut);
                terrain = reader.ReadDosTerrain(1000, 1000, rootPath + fileOut);
            }
            catch(Exception ex)
            {
                Assert.Fail("Terrain with dimension 1000x1000 couldn't be read or written: " + ex.Message);
            }
            finally
            {
                if (File.Exists(rootPath + fileOut))
                {
                    Console.WriteLine("Cleaning up test");
                    File.Delete(rootPath + fileOut);
                }
            }
        }

    }
}
