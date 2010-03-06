using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dnfeitosa.Enums.Tests
{
    [TestClass]
    public class EnumTest
    {
        sealed public class EnumFixture : Enum<EnumFixture>
        {
            public static readonly EnumFixture Value1 = new EnumFixture();
            public static readonly EnumFixture Value2 = new EnumFixture();
        }

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
    }
}