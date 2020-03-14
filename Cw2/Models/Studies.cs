using System.Xml.Serialization;

namespace Cw2.Models {
    [XmlRoot("studies")]
    public class Studies {
        [XmlElement("name")]
        public string Name { set; get; }
        [XmlElement("mode")]
        public string Mode { set; get; }
    }
}