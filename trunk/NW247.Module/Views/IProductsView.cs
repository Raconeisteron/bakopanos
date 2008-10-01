using System;
using Model;

namespace NW247.Module.Views
{
    public interface IProductsView
    {
        NorthwindDataSet.ProductsDataTable Model { get; set; }
        event EventHandler Save;
    }
}