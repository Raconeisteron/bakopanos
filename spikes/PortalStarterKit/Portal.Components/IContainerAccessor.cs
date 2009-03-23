using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.Components
{
    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}
