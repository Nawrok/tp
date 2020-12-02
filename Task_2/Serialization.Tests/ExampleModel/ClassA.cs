using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Serialization.Tests.ExampleModel
{
    [Serializable]
    [JsonObject]
    public class ClassA : ISerializable
    {
        public ClassA() { }

        public ClassA(string className, double doubleValue, ClassB b)
        {
            ClassName = className;
            DoubleValue = doubleValue;
            B = b;
        }

        public ClassA(SerializationInfo info, StreamingContext context)
        {
            ClassName = info.GetString("ClassName");
            DoubleValue = info.GetDouble("DoubleValue");
            B = (ClassB) info.GetValue("refB", typeof(ClassB));
        }

        public ClassB B { get; set; }
        public string ClassName { get; set; }
        public double DoubleValue { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ClassName", ClassName);
            info.AddValue("refB", B);
            info.AddValue("DoubleValue", DoubleValue);
        }
    }
}