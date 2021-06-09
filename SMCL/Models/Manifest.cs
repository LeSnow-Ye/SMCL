using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SMCL.Models
{
    public class Manifest
    {
        public List<ManifestItem> List;

        public Manifest(string uri)
        {
            this.Load(uri);
        }

        public void Load(string uri)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ManifestItem>));
                List = serializer.Deserialize(XmlReader.Create(uri)) as List<ManifestItem>;
            }
            catch (Exception e) { }
        }
    }
}