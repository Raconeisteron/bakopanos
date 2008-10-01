using NW247.Model;

namespace NW247.Services
{
    public interface IProductsService
    {
        NorthwindDataSet.ProductsDataTable GetProducts();
        NorthwindDataSet.ProductsDataTable UpdateAll(NorthwindDataSet.ProductsDataTable products);
    }
}