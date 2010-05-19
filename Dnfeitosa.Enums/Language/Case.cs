using System;

namespace Dnfeitosa.Enums.Language
{
    public class Case<T, TReturn>
        where T : IEnum
    {
        internal Switcher<T, TReturn> Owner { get; set; }
        internal T Value { get; set; }
        internal Func<TReturn> Action { get; set; }

        public Switcher<T, TReturn> Do(Func<TReturn> action)
        {
            Action = action;
            return Owner;
        }
    }
}