using System.Collections.Generic;
using System.Linq;

namespace Dnfeitosa.Enums.Language
{
    /// <summary>
    /// Implementation by Mozair 'MACSkeptic' Alves - http://github.com/MACSkeptic
    /// Originally published on http://github.com/MACSkeptic/MACSkeptic.Commons
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public class Switcher<T, TReturn>
        where T : IEnum
    {
        public Switcher()
        {
            Cases = new List<Case<T, TReturn>>();
        }

        private IList<Case<T, TReturn>> Cases { get; set; }
        private Case<T, TReturn> DefaultCase { get; set; }

        public Case<T, TReturn> When(T value)
        {
            var @case = new Case<T, TReturn> {Value = value, Owner = this};
            Cases.Add(@case);
            return @case;
        }

        public Case<T, TReturn> Default()
        {
            DefaultCase = new Case<T, TReturn> {Owner = this};
            return DefaultCase;
        }

        public TReturn ConsiderThisCase(T target)
        {
            var cases = Cases.Where(c => (IEnum)c.Value == (IEnum)target);
            return cases.Count() > 0
                       ? cases.First().Action.Invoke()
                       : DefaultCase.Action.Invoke();
        }
    }
}