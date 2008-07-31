using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

/// <summary>
/// XML Data Layer for News Page
/// </summary>
public class XmlNewsProvider : NewsProvider
{
    private readonly string _xmlFile;
    private readonly string _xsdFile;

    /// <summary>
    /// Reads xml and xsd file names from the web.config file
    /// </summary>
    public XmlNewsProvider()
    {
        var sec = (ConfigurationManager.GetSection("SmallBusinessDataProviders")) as SmallBusinessDataProvidersSection;
        string xmlFile = sec.NewsProviders[sec.NewsProviderName].Parameters["dataFile"];
        string xsdFile = sec.NewsProviders[sec.NewsProviderName].Parameters["schemaFile"];

        _xmlFile = HttpContext.Current.Request.MapPath("~/App_Data/" + xmlFile);
        _xsdFile = HttpContext.Current.Request.MapPath("~/App_Data/schemas/" + xsdFile);
    }

    /// <summary>
    /// Returns all news items
    /// </summary>
    public override List<NewsItem> GetAllNews()
    {
        DataSet dataSet = Util.ReadAndValidateXml(_xmlFile, _xsdFile);
        var list = new List<NewsItem>();
        foreach (DataTable t in dataSet.Tables)
        {
            NewsItem curr;
            foreach (DataRow r in t.Rows)
            {
                curr = new NewsItem((string) r["id"], (bool) r["visible"], (string) r["title"]);
                curr.Date = (r["date"] is DBNull) ? DateTime.MinValue : (DateTime) r["date"];
                curr.Content = (r["content"] is DBNull) ? String.Empty : (string) r["content"];
                curr.ImageUrl = (r["imageUrl"] is DBNull) ? String.Empty : (string) r["imageUrl"];
                curr.ImageAltText = (r["imageAltText"] is DBNull) ? String.Empty : (string) r["imageAltText"];
                list.Add(curr);
            }
        }

        return list;
    }

    /// <summary>
    /// Returns a particular news item
    /// </summary>
    public override NewsItem GetNewsItem(string newsItemId)
    {
        List<NewsItem> newsItems = GetAllNews();

        foreach (NewsItem n in newsItems)
        {
            if (n.Id == newsItemId)
                return n;
        }
        return null;
    }
}