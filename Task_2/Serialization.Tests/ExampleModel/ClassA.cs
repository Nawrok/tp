using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Serialization.Tests.ExampleModel
{
    [Serializable]
    [JsonObject]
    internal class ClassA : ISerializable
    {
        public ClassB B;
        public string ClassName;
        public double DoubleValue;

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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ClassName", ClassName);
            info.AddValue("DoubleValue", DoubleValue);
            info.AddValue("refB", B);
        }
    }
}