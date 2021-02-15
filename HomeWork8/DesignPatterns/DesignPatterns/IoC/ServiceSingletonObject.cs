using System;
namespace DesignPatterns.IoC
{
    public class ServiceSingletonObject<T> : IServiceObject
    {
        private Func<T> _factory;

        private Func<IServiceProvider, T> _factoryProvider;

        private bool _isActive = false;

        private T _instance;

        public ServiceSingletonObject()
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new MemberAccessException($"{nameof(T)} doesn't have a public default constructor");
            }
            _factory = () => (T)constructor.Invoke(new object[] { });
        }

        public ServiceSingletonObject(T singltonInstace)
        {
            _instance = singltonInstace;
            _isActive = true;
        }

        public ServiceSingletonObject(Func<T> factory)
        {
            _factory = factory;
        }

        public ServiceSingletonObject(Func<IServiceProvider, T> factory)
        {
            _factoryProvider = factory;
        }

        public TObject GetInstance<TObject>(IServiceProvider provider)
        {
            if (!typeof(T).Equals(typeof(TObject)))
            {
                throw new ArgumentOutOfRangeException(nameof(TObject));
            }
            if (!_isActive)
            {
                if (_factory != null)
                {
                    _instance = _factory.Invoke();
                }
                else if (_factoryProvider != null)
                {
                    _instance = _factoryProvider.Invoke(provider);
                }
                else
                {
                    throw new ArgumentNullException();
                }
                _isActive = true;
            }
            return (TObject)Convert.ChangeType(_instance, typeof(TObject));
        }
    }
}

