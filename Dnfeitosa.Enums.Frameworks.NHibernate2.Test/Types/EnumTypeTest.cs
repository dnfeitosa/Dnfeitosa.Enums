using System.Data;
using Dnfeitosa.Enums.Frameworks.NHibernate2.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dnfeitosa.Enums.Frameworks.NHibernate2.Test.Types
{
    [TestClass]
    public class EnumTypeTest
    {
        private EnumType<Tests.Fixtures.Language> _enumType;
        private MockFactory _mockFactory;

        [TestInitialize]
        public void Setup()
        {
            _mockFactory = new MockFactory(MockBehavior.Loose);
            _enumType = new EnumType<Tests.Fixtures.Language>();
        }

        [TestMethod]
        public void ShouldReturnNullWhenTheValueRetrievedFromDatabaseIsNotAnEnum()
        {
            var dataReader = GivenADataReaderThatReturns("someUnexpectedValue");

            var @enum = _enumType.NullSafeGet(dataReader.Object, new [] { "column_on_reader" }, new object());
            Assert.IsNull(@enum);
        }

        [TestMethod]
        public void ShouldReturnTheEnumCorrespondingToTheStoredName()
        {
            var dataReader = GivenADataReaderThatReturns(Tests.Fixtures.Language.Pl.Name);

            var @enum = _enumType.NullSafeGet(dataReader.Object, new[] { "column_on_reader" }, new object());
            Assert.IsNotNull(@enum);

            Assert.AreEqual(Tests.Fixtures.Language.Pl, @enum);
        }

        [TestMethod]
        public void ShouldStoreEnumsNameOnDatabase()
        {
            var dbCommand = _mockFactory.Create<IDbCommand>();
            var parameter = _mockFactory.Create<IDataParameter>(MockBehavior.Loose);
            dbCommand.SetupGet(cmd => cmd.Parameters[1]).Returns(parameter.Object);

            _enumType.NullSafeSet(dbCommand.Object, Tests.Fixtures.Language.Pl, 1);

            parameter.VerifySet(param => param.Value = Tests.Fixtures.Language.Pl.Name);
        }

        private Mock<IDataReader> GivenADataReaderThatReturns(string value)
        {
            var dataReader = _mockFactory.Create<IDataReader>();
            dataReader.Setup(reader => reader.GetOrdinal(It.IsAny<string>())).Returns(1);
            dataReader.Setup(reader => reader.IsDBNull(1)).Returns(false);
            dataReader.Setup(reader => reader[1]).Returns(value);
            return dataReader;
        }
    }
}
