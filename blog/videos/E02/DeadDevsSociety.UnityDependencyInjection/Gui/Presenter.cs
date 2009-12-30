namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    public abstract class Presenter<T>
        where T : View
    {
        public T View { get; protected set; }
    }
}