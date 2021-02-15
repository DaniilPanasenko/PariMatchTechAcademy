using System;
namespace DesignPatterns.IoC
{
    public interface IServiceObject
    {
        public T GetInstance<T>(IServiceProvider provider);
    }
}
