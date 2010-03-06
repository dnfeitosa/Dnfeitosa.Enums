using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dnfeitosa.Enums
{
    internal class Registry<T>
        where T : IEnum
    {
        private bool _normalized;
        private readonly IList<IEnum> _instances = new List<IEnum>();
        private readonly IDictionary<string, IEnum> _enums = new Dictionary<string, IEnum>();

        public void Add(IEnum @enum)
        {
            _instances.Add(@enum);
        }

        public T ValueOf(string name)
        {
            Normalize();
            if (!_enums.ContainsKey(name))
            {
                throw new NoEnumConstException(typeof(T), name);
            }
            return (T) _enums[name];
        }

        public IEnumerable<IEnum> Values()
        {
            Normalize();
            return _enums.Values.ToList();
        }

        internal void Normalize()
        {
            if (_normalized)
            {
                return;
            }
            var ordinal = 0;
            var fields = typeof (T).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var instance in _instances)
            {
                var @enum = ((Enum<T>)instance);
                var field = fields.Where(f => f.GetValue(@enum).Equals(@enum)).First();
                @enum.Name = field.Name;
                @enum.Ordinal = ordinal++;
                _enums.Add(field.Name, instance);
            }
            _normalized = true;
        }
    }
}