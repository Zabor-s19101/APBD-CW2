using System.Xml.Serialization;

namespace Cw2.Models {
    [XmlRoot("activeStudy")]
    public class ActiveStudy {
        [XmlAttribute("name")]
        public string Name { set; get; }
        [XmlAttribute("numberOfStudents")]
        public int NumberOfStudents { set; get; }
    }
}