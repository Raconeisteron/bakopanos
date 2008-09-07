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
            var worker = new BackgroundWorker();
            worker.DoWork += delegate { service = new ProductsServiceClient(); };
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();            
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            service.GetCategoriesCompleted +=
                delegate(object sender1, GetCategoriesCompletedEventArgs e1)
                    {
                        cmbCategory.ItemsSource = e1.Result.Categories;
                    };

            service.GetProductsCompleted +=
                delegate(object sender2, GetProductsCompletedEventArgs e2)
                    {
                        dataset = e2.Result;
                        DataContext = dataset.Products;
                        view = CollectionViewSource.GetDefaultView(dataset.Products) as CollectionView;
                    };

            service.GetCategoriesAsync(); 
            service.GetProductsAsync();  
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToFirst();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            view.MoveCurrentToLast();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
             if (view.CurrentPosition > 0) 
             { 
                 view.MoveCurrentToPrevious(); 
             } 

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
             if (view.CurrentPosition < view.Count - 1) 
             { 
                 view.MoveCurrentToNext();                 
             } 

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NWDataSet.ProductsRow row = dataset.Products.NewProductsRow();
            row.Product_Name = "<Name>";
            row.Discontinued = false;
            dataset.Products.AddProductsRow(row);
            view.MoveCurrentToLast();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
             if (dataset.HasChanges()) 
             { 
                 dataset.Merge(service.Update(dataset.GetChanges() as NWDataSet)); 
                 view.MoveCurrentToLast(); 
             } 

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            dataset.RejectChanges();
        }

    }
}