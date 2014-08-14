using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DosTerrainLib
{
    public class Dump
    {
        public void ReadToEndOfFile(String filename, String output)
        {
            byte[] dosBinaryFileContent = File.ReadAllBytes(filename);
            using (MemoryStream dosBinaryMemoryStream = new MemoryStream(dosBinaryFileContent))
            {
                using (BinaryReader dosBinaryReader = new BinaryReader(dosBinaryMemoryStream))
                {
                    // temporary since I'm writing this parser in parts I just want to know if I'm going out of bounds or something else worse
                    int amountOfExtraReads = 0;
                    using (StreamWriter bw = new StreamWriter(File.Open(output, FileMode.Create)))
                    {
                        while (true)
                        {
                            try
                            {
                                bw.WriteLine(dosBinaryReader.ReadUInt32());
                                amountOfExtraReads++;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("End of File Reached, " + amountOfExtraReads + " extra reads");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
