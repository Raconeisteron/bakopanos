using System;

namespace UIComposition.Infrastructure.Services
{
    public interface IUnityService
    {
        void RegisterSingleton<TTo, TFrom>()
            where TFrom : TTo;

        T Resolve<T>();
    }
}