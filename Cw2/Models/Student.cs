using System;
using System.Xml.Serialization;

namespace Cw2.Models {
    [XmlRoot("student")]
    public class Student {
        [XmlAttribute("indexNumber")]
        public string IndexNumber { get; set; }
        [XmlElement("fname")]
        public string FName { get; set; }
        [XmlElement("lname")]
        public string LName { get; set; }
        public string Birthdate { get; set; }
        [XmlElement("studies")]
        public Studies StudiesNameAndMode { get; set; }
    }
}