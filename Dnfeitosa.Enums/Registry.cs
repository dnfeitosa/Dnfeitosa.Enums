using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dnfeitosa.Enums
{
    /// <summary>
    /// This class is, we hope, thread-safe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Registry<T>
        where T : IEnum
    {
        private IDictionary<string, IEnum> _enums;

        private bool Normalized
        {
            get { return _enums != null; }
        }

        public T ValueOf(string name)
        {
            Normalize();
            // This check-then-act block is thread-safe because multiple executions of Normalize()
            // will always recreate the same '_enums' list. And the call in the first line of the method
            // guarantees that Normalize will be called at least once.
            if (!_enums.ContainsKey(name))
                throw new NoEnumConstException(typeof(T), name);

            return (T) _enums[name];
        }

        public IEnumerable<IEnum> Values()
        {
            Normalize();
            return _enums.Values.ToList();
        }

        internal void Normalize()
        {
            if (Normalized)
                return;

            var localEnums = new Dictionary<string, IEnum>();

            var ordinal = 0;
            var fields = typeof (T).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var fieldInfo in fields)
            {
                var value = fieldInfo.GetValue(this);
                if (!(value is Enum<T>))
                    continue;

                var instance = (Enum<T>) value;
                instance.Name = fieldInfo.Name;
                instance.Ordinal = ordinal++;
                localEnums.Add(fieldInfo.Name, instance);
            }

            _enums = localEnums;
        }
    }
}