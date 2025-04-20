using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure
{
    public class ObjectsCounter
    {
        public static int Count = 0;

        public static void Upgrade()
        {
            Count++;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(int));

            using (FileStream fs = new FileStream("count.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, Count);
            }
        }
    }
}
