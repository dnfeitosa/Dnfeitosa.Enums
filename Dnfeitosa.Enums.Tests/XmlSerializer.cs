using System.IO;

namespace Dnfeitosa.Enums.Tests
{
    public class XmlSerializer
    {
        public string Serialize<T>(T @object)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof (T));

            var writer = new StringWriter();
            serializer.Serialize(writer, @object);

            return writer.ToString();
        }

        public T Deserialize<T>(string value)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof (T));
            return (T) serializer.Deserialize(new StringReader(value));
        }
    }
}