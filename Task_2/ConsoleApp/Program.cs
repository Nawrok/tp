using System;
using System.IO;
using Serialization;
using Serialization.Tests.ExampleModel;

namespace ConsoleApp
{
    public static class Program
    {
        private static ClassA _classA;
        private static ClassB _classB;
        private static ClassC _classC;

        private static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("SERIALIZATION TASK 2");
            Console.WriteLine("1. Serialize to JSON");
            Console.WriteLine("2. Deserialize from JSON");
            Console.WriteLine("3. Serialize to TXT");
            Console.WriteLine("4. Deserialize from TXT");
            Console.WriteLine("5. Exit");
            Console.Write(Environment.NewLine + "Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                {
                    InitModel();
                    Stream serializeStream = File.Open("example.json", FileMode.Create, FileAccess.ReadWrite);
                    JsonFormatter.Serialize(_classC, serializeStream);
                    serializeStream.Close();
                    break;
                }
                case "2":
                {
                    InitEmptyModel();
                    Stream deserializeStream = File.Open("example.json", FileMode.Open, FileAccess.ReadWrite);
                    _classC = JsonFormatter.Deserialize<ClassC>(deserializeStream);
                    deserializeStream.Close();
                    BindReferences();
                    DisplayModel();
                    break;
                }
                case "3":
                {
                    OurFormatter ourFormatter = new OurFormatter();
                    InitModel();
                    Stream serializeStream = File.Open("example.txt", FileMode.Create, FileAccess.ReadWrite);
                    ourFormatter.Serialize(serializeStream, _classC);
                    serializeStream.Close();
                    break;
                }
                case "4":
                {
                    InitEmptyModel();
                    OurFormatter ourFormatter = new OurFormatter();
                    Stream deserializeStream = File.Open("example.txt", FileMode.Open, FileAccess.ReadWrite);
                    _classC = (ClassC) ourFormatter.Deserialize(deserializeStream);
                    deserializeStream.Close();
                    BindReferences();
                    DisplayModel();
                    break;
                }
                case "5":
                {
                    break;
                }
            }
        }

        private static void InitModel()
        {
            _classA = new ClassA("Klasa A", 2.1, null);
            _classB = new ClassB("Klasa B", 3.7f, null);
            _classC = new ClassC("Klasa C", new DateTime(2020, 12, 24), null);
            _classA.B = _classB;
            _classB.C = _classC;
            _classC.A = _classA;
        }

        private static void InitEmptyModel()
        {
            _classA = new ClassA();
            _classB = new ClassB();
            _classC = new ClassC();
        }

        private static void BindReferences()
        {
            _classA = _classC.A;
            _classB = _classA.B;
        }

        private static void DisplayModel()
        {
            Console.Clear();
            Console.WriteLine("Class A");
            Console.WriteLine($"ClassName: {_classA.ClassName} | DoubleValue: {_classA.DoubleValue} | B: {_classA.B}");
            Console.WriteLine("Class B");
            Console.WriteLine($"ClassName: {_classB.ClassName} | FloatValue: {_classB.FloatValue} | C: {_classB.C}");
            Console.WriteLine("Class C");
            Console.WriteLine($"ClassName: {_classC.ClassName} | DateTimeValue: {_classC.DateTimeValue} | A: {_classC.A}");
        }
    }
}