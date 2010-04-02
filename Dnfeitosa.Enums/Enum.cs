﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Dnfeitosa.Enums.Serialization;

namespace Dnfeitosa.Enums
{
    [Serializable]
    public abstract class Enum<T> : IEnum, ISerializable, IXmlSerializable
        where T : IEnum
    {
        // All required synchronization is done within registry.
        private static readonly Registry<T> Registry = new Registry<T>();
        private int? _ordinal;
        private string _name;

        public int Ordinal
        {
            get
            {
                if (_ordinal == null)
                    Registry.Normalize();
                return _ordinal ?? -1;
            }
            internal set { _ordinal = value; }
        }

        public string Name
        {
            get
            {
                if (_name == null)
                    Registry.Normalize();
                return _name;
            }
            internal set { _name = value; }
        }

        protected Enum()
        {
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

            return value1._name == value2._name && value1._ordinal == value2._ordinal;
        }
        
        public static bool operator != (Enum<T> value1, Enum<T> value2)
        {
            return !(value1 == value2);
        }

        protected Enum(SerializationInfo info, StreamingContext context)
        {
            var enumName = info.GetValue("EnumName", typeof (string)) as string;
            var @enum = ValueOf(enumName);

            new PropertiesCopier().Copy(@enum, this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EnumName", Name);
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
                return ((_ordinal ?? 0) * 397) ^ (_name != null ? _name.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/system.xml.serialization.ixmlserializable.getschema.aspx for more info
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var content = reader.ReadElementContentAsString();
            var @enum = ValueOf(content);

            new PropertiesCopier().Copy(@enum, this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(Name);
        }
    }
}