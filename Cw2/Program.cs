using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Cw2.Models;

namespace Cw2 {
    class Program {
        static void Main(string[] args) {
            var studenciList = new List<Student>();
            studenciList.Add(new Student {
                IndexNumber = "s19101",
                FName = "Mateusz",
                LName = "Zaborowski",
                StudiesNameAndMode = new Studies {
                    Name = "Computer Science",
                    Mode = "Dzienne"
                }
            });
            
            var uczelnia = new Uczelnia {
                CreatedAt = DateTime.Now.ToShortDateString(),
                Author = "Mateusz Zaborowski",
                Studenci = studenciList.ToArray()
            };
            
            var writer = new FileStream(@"data.xml", FileMode.Create);
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            var xmlSerializer = new XmlSerializer(typeof(Uczelnia));
            xmlSerializer.Serialize(writer, uczelnia, xns);
            writer.Close();
        }
    }
}