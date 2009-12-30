using System;

namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    public partial class ProductsView : View
    {
        public ProductsView()
        {
            InitializeComponent();

            buttonGetProducts.Click += delegate(object sender, EventArgs e)
                                           {
                                               GetProducts(sender, e);
                                               productModelBindingSource.DataSource = DataSource;
                                           };
        }

        public Action<object, EventArgs> GetProducts { private get; set; }
    }
}