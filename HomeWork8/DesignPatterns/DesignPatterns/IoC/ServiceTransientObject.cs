using System;
namespace DesignPatterns.IoC
{
    public class ServiceTransientObject<T> : IServiceObject
    {
        private Func<T> _factory;

        private Func<IServiceProvider, T> _factoryProvider;

        public ServiceTransientObject()
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new MemberAccessException($"{nameof(T)} doesn't have a public default constructor");
            }
            _factory = () => (T)constructor.Invoke(new object[] { });
        }

        public ServiceTransientObject(Func<T> factory)
        {
            _factory = factory;
        }

        public ServiceTransientObject(Func<IServiceProvider, T> factory)
        {
            _factoryProvider = factory;
        }

        public TObject GetInstance<TObject>(IServiceProvider provider)
        {
            if (!typeof(T).Equals(typeof(TObject)))
            {
                throw new ArgumentOutOfRangeException(nameof(TObject));
            }
            T instance;
            if (_factory != null)
            {
                instance = _factory.Invoke();
            }
            else if (_factoryProvider != null)
            {
                instance = _factoryProvider.Invoke(provider);
            }
            else
            {
                throw new ArgumentNullException();
            }
            return (TObject)Convert.ChangeType(instance, typeof(TObject));
        }
    }
}