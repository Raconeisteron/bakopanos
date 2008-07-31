using System.Collections.Generic;
using System.Configuration.Provider;

/// <summary>
/// base data access class
/// </summary>
public abstract class CatalogProvider : ProviderBase
{
    public abstract List<Item> GetChildItems(string parentCategoryId);
    public abstract List<Category> GetChildCategories(string parentCategoryId);
    public abstract Item GetItem(string itemId);
}