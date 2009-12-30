using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DeadDevsSociety.UnityDependencyInjection.Business;

namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    public partial class ProductsForm : Form
    {
        private IEnumerable<ProductModel> _products;

        public ProductsForm()
        {
            InitializeComponent();            
        }      

        private void buttonGetProducts_Click(object sender, EventArgs e)
        {
            _products = new ProductsFacade().GetProducts(".");
            productModelBindingSource.DataSource = _products;
        }
    }
}
