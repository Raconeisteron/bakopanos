﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using DemoApp.DataAccess;
using DemoApp.ViewModel;

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

            // Create the ViewModel to which 
            // the main window binds.
            const string path = "Data/customers.xml";

            var customerRepository =
                (ICustomerRepository)
                Activator.CreateInstance(
                    Type.GetType("DemoApp.DataAccess.Fake.CustomerRepository,DemoApp.DataAccess.Fake"),
                    new object[] {path});

            var projectRepository =
                (IProjectRepository)
                Activator.CreateInstance(
                    Type.GetType("DemoApp.DataAccess.Fake.ProjectRepository,DemoApp.DataAccess.Fake"));


            var viewModel = new MainWindowViewModel(customerRepository, projectRepository);

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