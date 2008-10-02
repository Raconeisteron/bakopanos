using NW247.Model;

namespace NW247.Module.Views
{
    public interface IProductsView
    {
        NorthwindDataSet.ProductsDataTable Model { get; set; }
    }
}