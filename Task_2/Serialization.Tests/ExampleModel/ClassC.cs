using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Serialization.Tests.ExampleModel
{
    [Serializable]
    [JsonObject]
    public class ClassC : ISerializable
    {
        public ClassC() { }

        public ClassC(string className, DateTime dateTimeValue, ClassA a)
        {
            ClassName = className;
            DateTimeValue = dateTimeValue;
            A = a;
        }

        public ClassC(SerializationInfo info, StreamingContext context)
        {
            ClassName = info.GetString("ClassName");
            DateTimeValue = info.GetDateTime("DateTimeValue");
            A = (ClassA) info.GetValue("refA", typeof(ClassA));
        }

        public ClassA A { get; set; }
        public string ClassName { get; set; }
        public DateTime DateTimeValue { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ClassName", ClassName);
            info.AddValue("DateTimeValue", DateTimeValue);
            info.AddValue("refA", A);
        }
    }
}