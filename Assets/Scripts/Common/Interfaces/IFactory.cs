using System;
namespace IndieCade
{
    public interface IFactory<T>
    {
        public T Make();
    }
}
