using System;
using Dnfeitosa.Enums.Frameworks.NHibernate2.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dnfeitosa.Enums.Frameworks.NHibernate2.Test.Types
{
    [TestClass]
    public class CustomPropertyEnumTypeTest
    {

        [TestMethod]
        public void Foo()
        {
            new LanguageType();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionWhenExpressionIsNotAMemberAccess()
        {
            new CustomPropertyEnumTypeFixture();
        }
    }

    public class CustomPropertyEnumTypeFixture : CustomPropertyEnumType<Tests.Fixtures.Language>
    {
        public CustomPropertyEnumTypeFixture()
            : base(exp => exp.ToString())
        {
        }
    }

    public class LanguageType : CustomPropertyEnumType<Tests.Fixtures.Language>
    {
        public LanguageType()
            : base(e => e.IetfTag)
        {
            
        }
    }
}
