using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Serialization.Tests.ExampleModel
{
    [Serializable]
    [JsonObject]
    public class ClassB : ISerializable
    {
        public ClassB() { }

        public ClassB(string className, float floatValue, ClassC c)
        {
            ClassName = className;
            FloatValue = floatValue;
            C = c;
        }

        public ClassB(SerializationInfo info, StreamingContext context)
        {
            ClassName = info.GetString("ClassName");
            FloatValue = info.GetSingle("FloatValue");
            C = (ClassC) info.GetValue("refC", typeof(ClassC));
        }

        public ClassC C { get; set; }
        public string ClassName { get; set; }
        public float FloatValue { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ClassName", ClassName);
            info.AddValue("FloatValue", FloatValue);
            info.AddValue("refC", C);
        }
    }
}