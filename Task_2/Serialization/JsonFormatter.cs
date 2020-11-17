using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Serialization
{
    public class JsonFormatter
    {
        public static void Serialize(object obj, Stream stream)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            });

            byte[] serializedContent = Encoding.UTF8.GetBytes(json);
            stream.Write(serializedContent, 0, serializedContent.Length);
        }

        public static T Deserialize<T>(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            string fileContent = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(fileContent, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            });
        }
    }
}
