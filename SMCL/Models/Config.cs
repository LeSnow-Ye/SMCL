using System.IO;
using System.Xml.Serialization;

namespace SMCL.Models
{
    public class Config
    {
        private static XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));

        public string Username { get; set; }
        public string Version { get; set; };
        public string Memory { get; set; } = "Auto";
        public string JavaPath { get; set; } = "Auto";
        public string ServerIp { get; set; }
        public string UpdateSource { get; set; }

        public void Save()
        {
            xmlSerializer.Serialize(new StreamWriter("SMCL.xml"), this);
        }

        public static Config Load()
        {
            Config config;
            try
            {
                config = xmlSerializer.Deserialize(new FileStream("SMCL.xml", FileMode.Open)) as Config;
            }
            catch
            {
                config = new Config();
                config.Save();
            }

            return config;
        }
    }
}