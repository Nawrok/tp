using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Serialization
{
    public class OurFormatter : Formatter
    {
        public OurFormatter()
        {
            IdGenerator = new ObjectIDGenerator();
            Context = new StreamingContext(StreamingContextStates.File);
        }

        public sealed override StreamingContext Context { get; set; }
        private ObjectIDGenerator IdGenerator { get; }

        public override SerializationBinder Binder
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override ISurrogateSelector SurrogateSelector
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        
        public override object Deserialize(Stream serializationStream)
        {
            List<object> deserializedObjects = new List<object>();

            StreamReader reader = new StreamReader(serializationStream ?? throw new ArgumentNullException(nameof(serializationStream)));
            string fileContent = reader.ReadToEnd();
            List<string> dataList = fileContent.Split(Environment.NewLine.ToCharArray()).ToList();

            for (int i = 0; i < dataList.Count - 1; i++)
            {
                List<string> entity = dataList[i].Split('|').ToList();
                Type entityType = Type.GetType(entity[3] + ", " + entity[3].Split('.').ToList()[0]) ?? throw new ArgumentNullException(nameof(entityType));
                SerializationInfo info = new SerializationInfo(entityType, new FormatterConverter());

                for (int j = 4; j < entity.Count - 3; j++)
                {
                    info.AddValue(entity[j], entity[j + 1]);
                }

                info.AddValue(entity[entity.Count - 2], null);
                deserializedObjects.Add(Activator.CreateInstance(entityType, info, Context));
            }

            for (int i = 0; i < deserializedObjects.Count - 1; i++)
            {
                foreach (PropertyInfo propertyInfo in deserializedObjects[i].GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType == deserializedObjects[i + 1].GetType())
                    {
                        propertyInfo.SetValue(deserializedObjects[i], deserializedObjects[i + 1]);
                    }
                }
            }

            foreach (PropertyInfo propertyInfo in deserializedObjects[deserializedObjects.Count - 1].GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == deserializedObjects[0].GetType())
                {
                    propertyInfo.SetValue(deserializedObjects[deserializedObjects.Count - 1], deserializedObjects[0]);
                }
            }

            return deserializedObjects[0];
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            throw new NotImplementedException();
        }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteBoolean(bool val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt32(int val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt64(long val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }
    }
}