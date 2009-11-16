using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace FunqWithUnity
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IPresentation, Presentation>();
            container.RegisterType<IApplicationServices, ApplicationServices>();
            container.RegisterType<IDataServices, DataServices>();

            var presentation = container.Resolve<IPresentation>();

            presentation.Show();

            Console.ReadLine();
        }
    }


    public interface IPresentation
    {
        void Show();
    }

    public class Presentation : IPresentation
    {
        IApplicationServices _appServices;

        public Presentation(IApplicationServices appServices)
        {
            Console.WriteLine("c'tor Presentation()");
            _appServices = appServices;
        }

        public void Show()
        {
            foreach(string item in _appServices.GetData("a"))
            {
                Console.WriteLine(item);
            }
        }

    }

    public interface IApplicationServices
    {
        List<string> GetData(string filter);
    }

    public class ApplicationServices : IApplicationServices
    {
        [Dependency]
        public IDataServices DataServices { private get; set; }

        public ApplicationServices()
        {
            Console.WriteLine("c'tor ApplicationServices()");
        }

        public List<string> GetData(string filter)
        {
            return (from item in DataServices.GetData()
                   where item.Contains(filter)
                   select item).ToList<string>();
        }

    }

    public interface IDataServices
    {
        List<string> GetData();
    }

    public class DataServices : IDataServices
    {
        public DataServices()
        {
            Console.WriteLine("c'tor DataServices()");
        }

        public List<string> GetData()
        {
            return new List<string> { "aa","a", "b","abc", "c" };
        }
    }    

}
