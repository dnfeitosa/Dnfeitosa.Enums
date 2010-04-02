using System.IO;
using System.Xml.Serialization;

namespace Dnfeitosa.Enums.Tests
{
    public class Converter
    {
        public string Convert<T>(T @object)
        {
            var serializer = new XmlSerializer(typeof (T));

            var writer = new StringWriter();
            serializer.Serialize(writer, @object);

            return writer.ToString();
        }

        public T Revert<T>(string value)
        {
            var serializer = new XmlSerializer(typeof (T));
            return (T) serializer.Deserialize(new StringReader(value));
        }
    }
}