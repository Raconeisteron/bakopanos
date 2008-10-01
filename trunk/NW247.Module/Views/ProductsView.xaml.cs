using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NW247.Model;
using NW247.Services;

namespace NW247.Module.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl, IProductsView
    {
        private CollectionView colView;
        private readonly IProductsService service;        

        public ProductsView(IProductsService service)
        {
            this.service = service;

            InitializeComponent();
        }

        #region IProductsView Members        

        public NorthwindDataSet.ProductsDataTable Model
        {
            get { return DataContext as NorthwindDataSet.ProductsDataTable; }
            set
            {
                DataContext = value;
                colView = CollectionViewSource.GetDefaultView(value) as CollectionView;
            }
        }

        #endregion

        private void buttonFirst_Click(object sender, RoutedEventArgs e)
        {
            colView.MoveCurrentToFirst();
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (colView.CurrentPosition > 0)
            {
                colView.MoveCurrentToPrevious();
            }
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (colView.CurrentPosition < colView.Count - 1)
            {
                colView.MoveCurrentToNext();
            }
        }

        private void buttonLast_Click(object sender, RoutedEventArgs e)
        {
            colView.MoveCurrentToLast();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Model != null)
            {
                NorthwindDataSet.ProductsRow row = Model.NewProductsRow();
                row.ProductName = "<Name>";
                row.Discontinued = false;
                Model.AddProductsRow(row);
                colView.MoveCurrentToLast();
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (colView.CurrentPosition > -1)
            {
                DataRow row = ((DataRowView) colView.CurrentItem).Row;
                row.Delete();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Model.RejectChanges();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Model.DataSet.HasChanges())
            {
                var changes = (NorthwindDataSet.ProductsDataTable)Model.GetChanges();
                service.UpdateAll(changes);
                Model.Merge(changes);
                colView.MoveCurrentToLast();
            }            
        }
    }
}