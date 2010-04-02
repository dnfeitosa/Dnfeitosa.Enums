using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dnfeitosa.Enums.Tests
{
    [TestClass]
    public class XmlSerializationTest
    {
        private XmlSerializer _xmlSerializer;

        [TestInitialize]
        public void Setup()
        {
            _xmlSerializer = new XmlSerializer();
        }

        [TestMethod]
        public void ShouldProperlySerializeAnEnumToXml()
        {
            var @object = new XmlFixture
                              {
                                  SomeEnum = EnumFixture.Value2,
                                  SomeValue = "Test"
                              };

            var @xml = _xmlSerializer.Serialize(@object);

            Assert.IsTrue(@xml.Contains("<SomeEnum>Value2</SomeEnum>"));
        }

        [TestMethod]
        public void ShouldProperlyDeserializeAnEnumFromAXml()
        {
            const string xml = @"<?xml version='1.0' ?><XmlFixture><SomeEnum>Value1</SomeEnum><SomeValue>Test</SomeValue></XmlFixture>";
            var @object = _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);
            _xmlSerializer.Deserialize<XmlFixture>(xml);

            Assert.IsTrue(@object.SomeEnum == EnumFixture.Value1);
        }
    }
}
