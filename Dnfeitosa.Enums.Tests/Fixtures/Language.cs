using System;
using System.Runtime.Serialization;

namespace Dnfeitosa.Enums.Tests.Fixtures
{
    [Serializable]
    public class Language : Enum<Language>
    {
        public static readonly Language En = new Language("en", "United States");
        public static readonly Language Pl = new Language("pl", "Poland");
        public static readonly Language PtBr = new Language("pt-BR", "Brazil");

        public virtual string IetfTag { get; private set; }
        public virtual string Country { get; private set; }

        private Language(string ietf, string country)
        {
            IetfTag = ietf;
            Country = country;
        }

        protected Language(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
