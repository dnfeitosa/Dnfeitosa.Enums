using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dnfeitosa.Enums.Tests
{
    [TestClass]
    public class EnumTest
    {
        [TestMethod]
        public void ShouldReturnConstantNameAsString()
        {
            Assert.AreEqual("Value1", EnumFixture.Value1.Name);
            Assert.AreEqual("Value2", EnumFixture.Value2.Name);
        }

        [TestMethod]
        public void ShouldReturnCorrectEnumFromItsName()
        {
            Assert.AreSame(EnumFixture.Value1, EnumFixture.ValueOf("Value1"));
            Assert.AreSame(EnumFixture.Value2, EnumFixture.ValueOf("Value2"));
        }

        [TestMethod]
        public void ShouldReturnAllDeclaredValues()
        {
            var values = EnumFixture.Values();

            Assert.AreEqual(2, values.Count());
            Assert.IsTrue(values.Contains(EnumFixture.Value1));
            Assert.IsTrue(values.Contains(EnumFixture.Value2));
        }

        [TestMethod]
        public void ShouldReturnOrdinalValueAsDeclarationOrder()
        {
            Assert.AreEqual(0, EnumFixture.Value1.Ordinal);
            Assert.AreEqual(1, EnumFixture.Value2.Ordinal);
        }

        [TestMethod]
        [ExpectedException(typeof(NoEnumConstException))]
        public void ShouldThrowExceptionWhenTriesToGetAnInexistentEnum()
        {
            EnumFixture.ValueOf("NonExistent");
        }

        [TestMethod]
        public void DefaultToStringMethodShouldReturnEnumName()
        {
            Assert.AreEqual("Value1", EnumFixture.Value1.ToString());
        }

        [TestMethod]
        public void ShouldProperlyBeSerializableAndDeserializable()
        {
            var formatter = new BinaryFormatter();
            using (var stream = GetStream())
            {
                formatter.Serialize(stream, EnumFixture.Value1);
                stream.Close();
            }

            EnumFixture deserialized;
            using (var stream = GetStream())
            {
                deserialized = (EnumFixture)formatter.Deserialize(stream);
            }

            Assert.AreEqual(EnumFixture.Value1, deserialized);
        }

        private FileStream GetStream()
        {
            var file = Path.GetTempPath() + "data.bin";
            return new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
    }

}