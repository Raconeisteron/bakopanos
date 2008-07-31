using System.Collections.Generic;
using System.Configuration.Provider;

/// <summary>
/// base data access class
/// </summary>
public abstract class NewsProvider : ProviderBase
{
    public abstract List<NewsItem> GetAllNews();
    public abstract NewsItem GetNewsItem(string newsItemId);
}