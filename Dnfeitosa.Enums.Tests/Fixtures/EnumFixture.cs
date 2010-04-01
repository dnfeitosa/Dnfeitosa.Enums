using System;
using System.Runtime.Serialization;

namespace Dnfeitosa.Enums.Tests.Fixtures
{
    [Serializable]
    public class EnumFixture : Enum<EnumFixture>
    {
        public static readonly EnumFixture Value1 = new EnumFixture();
        public static readonly EnumFixture Value2 = new EnumFixture();

        protected EnumFixture()
        {

        }

        protected EnumFixture(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
