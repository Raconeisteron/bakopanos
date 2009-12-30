using DeadDevsSociety.UnityDependencyInjection.BusinessObjects;

namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    public class ProductModel
    {
        public ProductBo Product { get; set; }
        public string DisplayName { get; set; }
    }

    public static class ProductModelExt
    {
        public static ProductModel ToModel(this ProductBo product)
        {
            var model = new ProductModel
                            {
                                Product = product,
                                DisplayName = string.Format("{0}, {1}", product.FirstName, product.LastName)
                            };
            return model;
        }
    }
}