using System.Xml.Serialization;

namespace Dnfeitosa.Enums.Tests.Fixtures
{
    [XmlRoot]
    public class XmlFixture
    {
        public EnumFixture SomeEnum { get; set; }
        public string SomeValue { get; set; }
        public Language Language { get; set; }
    }
}
