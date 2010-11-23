using Dnfeitosa.Enums.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dnfeitosa.Enums.Helpers;

namespace Dnfeitosa.Enums.Tests.Helpers
{
    [TestClass]
    public class TypeExtensionTest
    {
        [TestMethod]
        public void ShouldReturnTrueWhenAGivenTypeIsASubtypeOfEnum()
        {
            Assert.IsFalse(typeof (string).IsAnEnumType());
            Assert.IsTrue(typeof (EnumFixture).IsAnEnumType());
        }
    }
}
