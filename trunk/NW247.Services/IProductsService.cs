using NW247.Model;

namespace NW247.Services
{
    public interface IProductsService
    {
        NorthwindDataSet GetProducts();
        NorthwindDataSet UpdateAll(NorthwindDataSet products);
    }
}