using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Serialization
{
    public static class JsonFormatter
    {
        public static void Serialize(object obj, Stream stream)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            byte[] content = Encoding.UTF8.GetBytes(json);
            stream.Write(content, 0, content.Length);
        }

        public static T Deserialize<T>(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            string fileContent = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(fileContent, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }
    }
}