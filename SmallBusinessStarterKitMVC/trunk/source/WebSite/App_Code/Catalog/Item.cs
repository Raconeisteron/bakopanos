using System;

///<summary>
/// Class Item
/// Represents an item the small business sells
/// Typically, an Item could be a product or a service
///</summary>
public class Item
{
    private readonly string _id;
    private string _imageAltText;
    private string _imageUrl;
    private string _title;


    public Item(string id,
                bool visible,
                string title)
    {
        if (String.IsNullOrEmpty(id)) throw new ArgumentException(Messages.ItemIdUndefined);
        if (String.IsNullOrEmpty(title)) throw new ArgumentException(Messages.ItemTitleUndefined);
        _id = id;
        Visible = visible;
        _title = title;
    }

    public string Id
    {
        get { return String.IsNullOrEmpty(_id) ? String.Empty : _id; }
    }

    public bool Visible { get; set; }

    public string Title
    {
        get { return String.IsNullOrEmpty(_title) ? String.Empty : _title; }
        set
        {
            if (String.IsNullOrEmpty(value))
                throw new InvalidOperationException(Messages.ItemTitleIsNull);
            _title = value;
        }
    }

    public string Description { get; set; }

    public double Price { get; set; }

    public bool InStock { get; set; }

    public string ImageUrl
    {
        get { return String.IsNullOrEmpty(_imageUrl) ? String.Empty : _imageUrl; }
        set { _imageUrl = value; }
    }

    public string ImageAltText
    {
        get { return String.IsNullOrEmpty(_imageAltText) ? String.Empty : _imageAltText; }
        set { _imageAltText = value; }
    }
}