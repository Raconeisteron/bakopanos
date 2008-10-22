using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bakopanos.NW.BusinessObjects;
using Microsoft.Practices.Unity;

namespace Bakopanos.NW.WinClient.ProductsModule.Views
{
    public partial class ProductListView : UserControl
    {
        public ProductListView(ProductListPresenter presenter,
            ProductAggregate productAggregate)
        {
            InitializeComponent();

            dataGridView1.DataSource = productAggregate.ProductList;
        }
    }
}
