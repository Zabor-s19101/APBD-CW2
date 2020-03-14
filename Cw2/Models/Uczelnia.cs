using System;
using System.Xml.Serialization;

namespace Cw2.Models {
    [XmlRoot("uczelnia")]
    public class Uczelnia {
        [XmlAttribute("createdAt")]
        public string CreatedAt { set; get; }
        [XmlAttribute("author")]
        public string Author { set; get; }
        [XmlArray("studenci"), XmlArrayItem("student")]
        public Student[] Studenci { set; get; }
        [XmlArray("activeStudies"), XmlArrayItem("studies")]
        public ActiveStudy[] ActiveStudies { set; get; }
    }
}