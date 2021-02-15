using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, IServiceObject> _collection;

        public ServiceProvider(Dictionary<Type, IServiceObject> collection)
        {
            _collection = collection;
        }

        public T GetService<T>()
        {
            foreach(var obj in _collection)
            {
                if (typeof(T) == obj.Key)
                {
                    return obj.Value.GetInstance<T>(this);
                }
            }
            return default(T);
        }
    }
}
