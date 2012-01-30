using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using ASPNET.StarterKit.Portal;
using DemoApp.View;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

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

            IUnityContainer container = new UnityContainer();

            // additional container initialization 
            container.RegisterInstance<IPortalServerUtility>(new PortalServerUtility());
            container.RegisterInstance<IPortalPrincipalUtility>(new PortalPrincipalUtility());
            container.RegisterInstance<IPortalCacheUtility>(new PortalCacheUtility());

            var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            section.Configure(container);

            var window = container.Resolve<MainWindowView>();

            // Create the ViewModel to which 
            // the main window binds.
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