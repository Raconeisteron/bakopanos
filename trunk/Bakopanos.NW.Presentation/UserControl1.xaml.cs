using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Bakopanos.NW.Application.ProductsServiceReference;

namespace Bakopanos.NW.Presentation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private ProductsServiceClient service;
        private CollectionView view;
        private NWDataSet dataset;

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var start = new ThreadStart(delegate
                                            {
                                                service = new ProductsServiceClient();
                                                service.GetProductsCompleted += service_GetProductsCompleted;
                                                service.GetProductsAsync();
                                            });
            new Thread(start).Start();            
        }

        void service_GetProductsCompleted(object sender, GetProductsCompletedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Background,
                              new Action(
                                  delegate
                                      {
                                          dataset = e.Result;
                                          DataContext = dataset.Products;                                          
                                      }
                                  ));
            
            view = CollectionViewSource.GetDefaultView(dataset.Products) as CollectionView;

                   
        }
    }
}