using System;
using System.Xml.Serialization;

namespace Cw2.Models {
    [XmlRoot("student")]
    public class Student {
        [XmlAttribute("indexNumber")]
        public string IndexNumber { set; get; }
        [XmlElement("fname")]
        public string FName { set; get; }
        [XmlElement("lname")]
        public string LName { set; get; }
        [XmlElement("birthdate")]
        public string Birthdate { set; get; }
        [XmlElement("email")]
        public string Email { set; get; }
        [XmlElement("mothersName")]
        public string MothersName { set; get; }
        [XmlElement("fathersName")]
        public string FathersName { set; get; }
        [XmlElement("studies")]
        public Studies StudiesNameAndMode { set; get; }
    }
}