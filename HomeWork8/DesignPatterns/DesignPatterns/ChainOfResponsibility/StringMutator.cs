using System;
using System.Collections.Generic;

namespace DesignPatterns.ChainOfResponsibility
{
    public abstract class StringMutator : IStringMutator
    {
        private List<IStringMutator> _listNexts = new List<IStringMutator>();

        public IStringMutator SetNext(IStringMutator next)
        {
            _listNexts.Add(next);
            return this;
        }

        public string Mutate(string str)
        {
            foreach(var next in _listNexts)
            {
                str = next.Mutate(str);
            }
            str = Operation(str);
            return str;
        }

        protected abstract string Operation(string str);
    }
}
