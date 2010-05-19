using System.Collections.Generic;
using System.Linq;
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

            Assert.AreEqual(3, values.Count());
            Assert.IsTrue(values.Contains(EnumFixture.Value1));
            Assert.IsTrue(values.Contains(EnumFixture.Value2));
            Assert.IsTrue(values.Contains(EnumFixture.Value3));
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
        public void ShouldReturnAnEnumByACustomProperty()
        {
            var language = Fixtures.Language.Where(l => l.IetfTag == "pl");
            Assert.AreEqual(Fixtures.Language.Pl, language);
        }

        [TestMethod] // Issue 5
        public void ShouldProperlyEvaluateReferenceComparison()
        {
            Assert.IsTrue(AnotherFixture.Value1 == AnotherFixture.Value1);
        }

        [TestMethod] // Issue 4
        public void ShouldDetectPresenceOfEnumOnDictionaries()
        {
            var dictionary = new Dictionary<EnumFixture, string>
                                 {
                                     {EnumFixture.Value1, EnumFixture.Value1.Name},
                                     {EnumFixture.Value2, EnumFixture.Value2.Name}
                                 };

            Assert.IsTrue(dictionary.ContainsKey(EnumFixture.Value1));
            Assert.IsTrue(dictionary.ContainsKey(EnumFixture.Value2));
            Assert.IsFalse(dictionary.ContainsKey(EnumFixture.Value3));
        }
    }
}