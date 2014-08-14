using DosTerrainLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DosTerrainLib.Util
{
    public class XMLWrite
    {
        public static void WriteXML(DosTerrain terrain, String fileName)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(DosTerrain));

            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            writer.Serialize(file, terrain);
            file.Close();
        }
    }
}
