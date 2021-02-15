using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceCollection : IServiceCollection
    {
        private Dictionary<Type, IServiceObject> _collection;

        public ServiceCollection()
        {
            _collection = new Dictionary<Type, IServiceObject>();
        }

        public IServiceCollection AddTransient<T>()
        {
            _collection.TryAdd(typeof(T), new ServiceTransientObject<T>());
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory)
        {
            _collection.TryAdd(typeof(T), new ServiceTransientObject<T>(factory));
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory)
        {
            _collection.TryAdd(typeof(T), new ServiceTransientObject<T>(factory));
            return this;
        }

        public IServiceCollection AddSingleton<T>()
        {
            _collection.TryAdd(typeof(T), new ServiceSingletonObject<T>());
            return this;
        }

        public IServiceCollection AddSingleton<T>(T service)
        {
            _collection.TryAdd(typeof(T), new ServiceSingletonObject<T>(service));
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory)
        {
            _collection.TryAdd(typeof(T), new ServiceSingletonObject<T>(factory));
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory)
        {
            _collection.TryAdd(typeof(T), new ServiceSingletonObject<T>(factory));
            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_collection);
        }
    }
}