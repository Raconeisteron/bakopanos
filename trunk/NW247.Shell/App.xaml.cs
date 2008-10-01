using System.Windows;

namespace NW247.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var bootStrapper = new Bootstrapper();
            bootStrapper.Run();
        }
    }
}