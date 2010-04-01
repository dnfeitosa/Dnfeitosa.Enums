using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Dnfeitosa.Enums
{
    [Serializable]
    public abstract class Enum<T> : IEnum, ISerializable
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

        public static bool operator == (Enum<T> value1, Enum<T> value2)
        {
            if ((object)value1 == null && (object)value2 == null)
                return true;

            if ((object)value1 == null || (object)value2 == null)
                return false;

            return value1.Name == value2.Name;
        }
        
        public static bool operator != (Enum<T> value1, Enum<T> value2)
        {
            return !(value1 == value2);
        }

        protected Enum(SerializationInfo info, StreamingContext context)
        {
            var enumName = info.GetValue("EnumName", typeof (string)) as string;
            var @enum = ValueOf(enumName);

            foreach (var property in GetCustomProperties())
            {
                property.SetValue(this, property.GetValue(@enum, null), null);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EnumName", Name);
        }

        private PropertyInfo[] GetCustomProperties()
        {
            var excludes = new[] {""};
            return GetType()
                .GetProperties()
                .Where(prop => !excludes.Contains(prop.Name))
                .ToArray();
        }

        public bool Equals(Enum<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._ordinal == _ordinal && Equals(other._name, _name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Enum<T>)) return false;
            return Equals((Enum<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_ordinal*397) ^ (_name != null ? _name.GetHashCode() : 0);
            }
        }
    }
}