using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using NW247.Model;

namespace NW247.Module.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl, IProductsView
    {
        private readonly IProductsPresenter _Presenter;

        [Dependency]
        public NorthwindDataSet.ProductsDataTable Model
        {
            set { DataContext = value; }
        }

        public ProductsView(IProductsPresenter Presenter)
        {
            InitializeComponent();

            _Presenter = Presenter;            
        }

        private void buttonFirst_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.MoveFirst();
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.MovePrev();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.MoveNext();
        }

        private void buttonLast_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.MoveLast();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.Add();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.Delete();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.Cancel();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            _Presenter.Save();
        }
    }
}