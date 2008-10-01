using Model;

namespace Services
{
    public interface IProductsService
    {
        NorthwindDataSet GetProducts();
        NorthwindDataSet UpdateAll(NorthwindDataSet products);
    }
}