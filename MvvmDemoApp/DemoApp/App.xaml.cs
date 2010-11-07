using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using DemoApp.DataAccess;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;

namespace DemoApp
{
    public partial class App : Application
    {
        static App()
        {
            // This code is used to test the app when using other cultures.
            //
            //System.Threading.Thread.CurrentThread.CurrentCulture =
            //    System.Threading.Thread.CurrentThread.CurrentUICulture =
            //        new System.Globalization.CultureInfo("it-IT");


            // Ensure the current culture passed into bindings is the OS culture.
            // By default, WPF uses en-US as the culture, regardless of the system settings.
            //
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof (FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow();
            IUnityContainer container = new UnityContainer();

            // Create the ViewModel to which 
            // the main window binds.
            const string path = "Data/customers.xml";

            container.RegisterInstance(typeof(ICustomerRepository), Activator.CreateInstance(
                Type.GetType("DemoApp.DataAccess.Fake.CustomerRepository,DemoApp.DataAccess.Fake"), new object[] { path }));

            string sourceCodePath = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
            container.RegisterInstance(typeof(IProjectRepository),Activator.CreateInstance(
                                   Type.GetType("DemoApp.DataAccess.Fake.ProjectRepository,DemoApp.DataAccess.Fake"), new object[] { sourceCodePath, "*.csproj" }));

            var viewModel = container.Resolve<MainWindowViewModel>();

            // When the ViewModel asks to be closed, 
            // close the window.
            EventHandler handler = null;
            handler = delegate
                          {
                              viewModel.RequestClose -= handler;
                              window.Close();
                          };
            viewModel.RequestClose += handler;

            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down 
            // the element tree.
            window.DataContext = viewModel;

            window.Show();
        }
    }
}