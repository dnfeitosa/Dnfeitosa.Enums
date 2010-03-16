using System.Collections.Generic;
using System.Linq;

namespace Dnfeitosa.Enums
{
    public abstract class Enum<T> : IEnum
        where T : IEnum
    {
        // All required synchronization is done within registry.
        private static readonly Registry<T> Registry = new Registry<T>();
        private int _ordinal;
        private string _name;

        public int Ordinal
        {
            get
            {
                Registry.Normalize();
                return _ordinal;
            }
            internal set { _ordinal = value; }
        }

        public string Name
        {
            get
            {
                Registry.Normalize();
                return _name;
            }
            internal set { _name = value; }
        }

        protected Enum()
        {
            Registry.Add(this);
        }

        public override string ToString()
        {
            return Name;
        }

        public static T ValueOf(string name)
        {
            return Registry.ValueOf(name);
        }

        public static IEnumerable<T> Values()
        {
            return Registry.Values().Select(@enum => (T) @enum).ToList();
        }
    }
}