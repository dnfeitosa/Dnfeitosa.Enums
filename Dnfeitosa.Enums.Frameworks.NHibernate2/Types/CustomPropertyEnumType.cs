using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Dnfeitosa.Enums.Frameworks.NHibernate2.Types
{
    public abstract class CustomPropertyEnumType<TEnumType> : IUserType
             where TEnumType : IEnum
    {
        public PropertyInfo EnumProperty { get; private set; }

        protected CustomPropertyEnumType(Expression<Func<TEnumType, object>> expression)
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Not a member access", "expression");
            }
            var memberExpression = expression.Body as MemberExpression;
            EnumProperty = (PropertyInfo)memberExpression.Member;
        }


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
            var enumProperty = typeof (TEnumType).GetProperties().FirstOrDefault(p => p == EnumProperty);
            return enumProperty == null
                ? null
                : enumProperty.GetV
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
