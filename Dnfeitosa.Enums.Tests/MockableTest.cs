using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dnfeitosa.Enums.Tests
{
    [TestClass]
    public class MockableTest
    {
        [TestMethod]
        public void ShouldBeAbleToCreateAProxyForAnEnum()
        {
            var factory = new MockFactory(MockBehavior.Strict);

            var @enum = factory.Create<Fixtures.Language>(MockBehavior.Strict, null, null);
            @enum.Setup(e => e.IetfTag).Returns("CZ");

            Assert.AreEqual("CZ", @enum.Object.IetfTag);
        }
    }
}
