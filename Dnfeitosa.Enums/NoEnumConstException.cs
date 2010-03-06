using System;

namespace Dnfeitosa.Enums
{
    public class NoEnumConstException : Exception
    {
        public NoEnumConstException(Type type, string name)
            : base(string.Format("No enum const {0}.{1}", type.Name, name))
        {

        }
    }
}