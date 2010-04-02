using System.Xml.Serialization;
using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dnfeitosa.Enums.Tests
{
    [TestClass]
    public class XmlSerializationTest
    {
        private Converter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new Converter();
        }

        [TestMethod]
        public void ShouldProperlySerializeToXml()
        {
            var @object = new SomeObject {SomeEnumProperty = EnumFixture.Value2, SomeProperty = "Test"};

            var @string = _converter.Convert(@object);

            var revert = _converter.Revert<SomeObject>(@string);

            @string.GetType();
            revert.GetType();
        }
    }

    [XmlRoot]
    public class SomeObject
    {
        public EnumFixture SomeEnumProperty { get; set; }
        public string SomeProperty { get; set; }
    }
}
