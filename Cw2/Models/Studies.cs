using System.Xml.Serialization;

namespace Cw2.Models {
    [XmlRoot("studies")]
    public class Studies {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("mode")]
        public string Mode { get; set; }
    }
}