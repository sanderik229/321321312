using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10Prakticheskaya
{
    internal class JSONConverter
    {
        public static T JSONDeserializer<T>(string path)
        {
            string json = File.ReadAllText(path);
            T List = JsonConvert.DeserializeObject<T>(json);
            return List;
        }
        public static void JSONSerializer<T>(string path, T list)
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, json);
        }
    }
}