using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dnfeitosa.Enums.Tests
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void ShouldProperlyBeSerializableAndDeserializable()
        {
            var fixture = SerializeAndDeserialize(EnumFixture.Value1);

            Assert.IsNotNull(fixture.Name);
            Assert.IsNotNull(fixture.Ordinal);
            Assert.AreEqual(EnumFixture.Value1, fixture);
            Assert.IsTrue(EnumFixture.Value1 == fixture);
        }

        [TestMethod]
        public void ShouldPreserveCustomPropertiesOfASerializedEnum()
        {
            var language = SerializeAndDeserialize(Language.PtBr);

            Assert.IsNotNull(language.Country);
            Assert.IsNotNull(language.IetfTag);
            Assert.AreEqual(Language.PtBr, language);
            Assert.IsTrue(Language.PtBr == language);
       } 

        private T SerializeAndDeserialize<T>(T @enum)
        {
            T deserialized;
            using (var stream = new FileStream(Path.GetTempPath() + "data.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, @enum);
                stream.Position = 0;

                deserialized = (T)formatter.Deserialize(stream);
                stream.Close();
            }
            return deserialized;
        }
    }
}
