using System.Collections.Generic;
using System.Windows.Forms;

namespace Bakopanos.Framework.Composite
{
    public interface IPart
    {
        void Run();
    }

    public interface IViewInfo
    {
        string Caption { get; set; }
    }

    public interface IView<T> : IPart, IViewInfo
        where T : IPresenter
    {
        T Presenter { set; }
        Dictionary<string, Control> Placeholders { get; }
    }

    public interface IModule : IPart
    {
    }

    public interface IController : IPart
    {
    }

    public interface IPresenter : IPart
    {
    }
}