using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Serialization
{
    public class OurFormatter : Formatter
    {
        private string _fileContent = "";
        private bool _isFirstTime;

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
            _fileContent = reader.ReadToEnd();
            List<string> dataList = _fileContent.Split(Environment.NewLine.ToCharArray()).Where(s => s.Length != 0).ToList();

            foreach (string line in dataList)
            {
                List<string> entries = line.Split('|').ToList();
                Type entryClassType = Type.GetType(getSerializationMembers(entries)["classType"]) ?? throw new ArgumentNullException(nameof(entryClassType));
                SerializationInfo info = new SerializationInfo(entryClassType, new FormatterConverter());

                foreach (KeyValuePair<string, string> member in getSerializationMembers(entries).Skip(2))
                {
                    if (member.Key.StartsWith("🎅"))
                    {
                        info.AddValue(member.Key.Substring(2), null);
                    }
                    else
                    {
                        info.AddValue(member.Key, member.Value);
                    }
                }

                deserializedObjects.Add(Activator.CreateInstance(entryClassType, info, Context));
            }

            for (int i = 0; i < deserializedObjects.Count; i++)
            {
                for (int j = 0; j < deserializedObjects.Count; j++)
                {
                    foreach (PropertyInfo p in deserializedObjects[i].GetType().GetProperties())
                    {
                        if (p.PropertyType == deserializedObjects[j].GetType())
                        {
                            p.SetValue(deserializedObjects[i], deserializedObjects[j]);
                        }
                    }

                    foreach (FieldInfo f in deserializedObjects[i].GetType().GetFields())
                    {
                        if (f.FieldType == deserializedObjects[j].GetType())
                        {
                            f.SetValue(deserializedObjects[i], deserializedObjects[j]);
                        }
                    }
                }
            }

            return deserializedObjects[0];
        }

        public override void Serialize(Stream serializationStream, object graph)
        {
            if (graph is ISerializable data)
            {
                SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
                info.AddValue("id", IdGenerator.GetId(graph, out _isFirstTime));
                info.AddValue("classType", graph.GetType().FullName + ", " + info.AssemblyName.Split(',').First());
                data.GetObjectData(info, Context);

                foreach (SerializationEntry item in info)
                {
                    WriteMember(item.Name, item.Value);
                }

                _fileContent = _fileContent.Remove(_fileContent.Length - 1);
                _fileContent += Environment.NewLine;

                while (m_objectQueue.Count != 0)
                {
                    Serialize(serializationStream, m_objectQueue.Dequeue());
                }

                byte[] content = Encoding.UTF8.GetBytes(_fileContent);
                serializationStream.Write(content, 0, content.Length);
                _fileContent = "";
            }
            else
            {
                throw new ArgumentException($"Implementation of {graph} is necessary");
            }
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (memberType == typeof(string))
            {
                WriteString(obj, name);
            }
            else
            {
                _fileContent += "🎅" + name + ":" + IdGenerator.GetId(obj, out _isFirstTime) + "|";
                if (_isFirstTime)
                {
                    m_objectQueue.Enqueue(obj);
                }
            }
        }

        protected void WriteString(object str, string name)
        {
            _fileContent += name + ":" + (string) str + "|";
        }

        protected override void WriteDouble(double val, string name)
        {
            _fileContent += name + ":" + val.ToString("G", CultureInfo.InvariantCulture) + "|";
        }

        protected override void WriteInt64(long val, string name)
        {
            _fileContent += name + ":" + val + "|";
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            _fileContent += name + ":" + val.ToString("d", DateTimeFormatInfo.InvariantInfo) + "|";
        }

        protected override void WriteSingle(float val, string name)
        {
            _fileContent += name + ":" + val.ToString("0.00", CultureInfo.InvariantCulture) + "|";
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

        protected override void WriteDecimal(decimal val, string name)
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

        protected override void WriteSByte(sbyte val, string name)
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

        private static Dictionary<string, string> getSerializationMembers(IEnumerable<string> entries)
        {
            return entries.Select(entry => entry.Split(':').ToList()).ToDictionary(parts => parts[0], parts => parts[1]);
        }
    }
}