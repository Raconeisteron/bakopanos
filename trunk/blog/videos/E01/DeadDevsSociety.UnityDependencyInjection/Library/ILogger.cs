namespace DeadDevsSociety.UnityDependencyInjection.Library
{
    public interface ILogger
    {
        void Information(string message, string category);
        void Error(string message, string category);
    }

  
}