using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using Dnfeitosa.Enums.Helpers;

namespace Dnfeitosa.Enums.Frameworks.NHibernate2.Types
{
    public class EnumType<TEnumType> : IUserType
        where TEnumType : IEnum
    {
        public bool Equals(object x, object y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var enumName = (string)NHibernateUtil.String.NullSafeGet(rs, names[0]);
            var enumField = typeof(TEnumType).GetFieldNamed(enumName);
            return enumField == null
                ? null
                : enumField.GetValue(null);
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index);
                return;
            }

            var @enum = (IEnum)value;
            NHibernateUtil.String.NullSafeSet(cmd, @enum.Name, index);
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public SqlType[] SqlTypes
        {
            get
            {
                return new[] { new SqlType(DbType.String) };
            }
        }

        public Type ReturnedType
        {
            get { return typeof(TEnumType); }
        }

        public bool IsMutable
        {
            get { return false; }
        }

    }
}
