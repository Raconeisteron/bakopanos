// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
namespace UIComposition.Infrastructure.Services
{
    public interface IUnityService
    {
        void RegisterSingleton<TTo, TFrom>()
            where TFrom : TTo;

        T Resolve<T>();

        void RegisterInstance<T>(T instance);
    }
}