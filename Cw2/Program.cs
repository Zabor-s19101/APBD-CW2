using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Cw2.Models;

namespace Cw2 {
    class Program {
        static void Main(string[] args) {
            try {
                var daneFi = new FileInfo(@"dane.csv");
                var xmlFi = new FileInfo(@"data.xml");
                if (args.Length > 0 && args.Length != 3) {
                    throw new ArgumentOutOfRangeException();
                }
                if (args.Length == 3) {
                    daneFi = new FileInfo(args[0]);
                    if (!daneFi.Exists) {
                        throw new FileNotFoundException("Plik z danymi nie istnieje");
                    }
                    xmlFi = new FileInfo(args[1]);
                    if (xmlFi.Directory != null && !xmlFi.Directory.Exists) {
                        throw new ArgumentException("Podana ścieżka jest niepoprawna");
                    }
                    if (!args[2].Equals("xml")) {
                        throw new ArgumentException("Niewspierany format pliku wyjściowego");
                    }
                }
                var studenciDic = new Dictionary<string, Student>();
                var activeStudiesDic = new Dictionary<string, ActiveStudy>();
                var logFi = new FileInfo(@"log.txt");
                using (var logWriter = new StreamWriter(logFi.OpenWrite()))
                using (var daneReader = new StreamReader(daneFi.OpenRead())) {
                    string line;
                    while ((line = daneReader.ReadLine()) != null) {
                        try {
                            string[] studentInfo = line.Split(",");
                            if (studentInfo.Length != 9) {
                                throw new Exception(message: DateTime.Now.ToString(CultureInfo.CurrentCulture) +
                                                             " Niepoprawna ilość danych w wierszu: " + line);
                            }
                            if (studentInfo.Any(info => info.Trim().Length == 0)) {
                                throw new Exception(message: DateTime.Now.ToString(CultureInfo.CurrentCulture) +
                                                             " Puste dane w wierszu: " + line);
                            }
                            Student student = new Student {
                                IndexNumber = "s" + studentInfo[4],
                                FName = studentInfo[0],
                                LName = studentInfo[1],
                                Birthdate = DateTime.Parse(studentInfo[5]).ToShortDateString(),
                                Email = studentInfo[6],
                                MothersName = studentInfo[7],
                                FathersName = studentInfo[8],
                                StudiesNameAndMode = new Studies {
                                    Name = studentInfo[2],
                                    Mode = studentInfo[3]
                                }
                            };
                            string key = student.FName + " " + student.LName + " s" + student.IndexNumber;
                            if (studenciDic.ContainsKey(key)) {
                                throw new Exception(DateTime.Now.ToString(CultureInfo.CurrentCulture) +
                                                    " Student już istnieje: " + line);
                            }
                            studenciDic.Add(key, student);
                            key = student.StudiesNameAndMode.Name;
                            if (activeStudiesDic.ContainsKey(key)) {
                                activeStudiesDic[key].NumberOfStudents++;
                            } else {
                                activeStudiesDic.Add(key, new ActiveStudy {
                                    Name = key,
                                    NumberOfStudents = 1
                                });
                            }
                        } catch (Exception e) {
                            logWriter.WriteLine(e.Message);
                        }
                    }
                }
                var uczelnia = new Uczelnia {
                    CreatedAt = DateTime.Now.ToShortDateString(),
                    Author = "Mateusz Zaborowski",
                    Studenci = studenciDic.Values.ToArray(),
                    ActiveStudies = activeStudiesDic.Values.ToArray()
                };
                var xmlWriter = new StreamWriter(xmlFi.OpenWrite());
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                var xmlSerializer = new XmlSerializer(typeof(Uczelnia));
                xmlSerializer.Serialize(xmlWriter, uczelnia, xns);
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine(e.GetType() + ": " + e.Message);
            } catch (FileNotFoundException e) {
                Console.WriteLine(e.GetType() + " " + e.Message);
            } catch (ArgumentException e) {
                Console.WriteLine(e.GetType() + ": " + e.Message);
            }
        }
    }
}